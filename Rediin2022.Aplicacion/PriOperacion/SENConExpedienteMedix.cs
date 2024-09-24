using DSMetodNetX.Entidades;
using DSMetodNetX.Entidades.Config;
using DSMetodNetX.Entidades.Correo;
using Rediin2022.Comun.PriOperacion;
using Rediin2022.Entidades.PriCatalogos;
using Rediin2022.Entidades.PriClientes;
using Rediin2022.Entidades.PriOperacion;
using Sisegui2020.Entidades.PriSeguridad;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Rediin2022.Aplicacion.PriOperacion
{
	public class SENConExpedienteMedix : ISENConExpedienteProv
	{
		public SENConExpedienteMedix(IMSrvPrivado servicios,
									 INConExpedientes nConExpedientes,
									 INExpedientes nExpedientes,
									 INExpedientesProveedor nExpedientesProveedor,
									 INUsuarios nUsuarios,
									 IConfig config,
									 HttpClient httpClient)
		{
			Servicios = servicios;
			Config = config;
			NConExpedientes = nConExpedientes;
			NExpedientes = nExpedientes;
			NExpedientesProveedor = nExpedientesProveedor;
			NUsuarios = nUsuarios;
			HttpClient = httpClient;
		}
		private IMMensajes Mensajes
		{
			get { return NConExpedientes.Mensajes; }
		}

		private EVConExpedientes EV { get; set; }

		private IConfig Config { get; set; }
		private HttpClient HttpClient { get; set; }

		private IMSrvPrivado Servicios { get; set; }
		private INConExpedientes NConExpedientes { get; set; }
		private INExpedientes NExpedientes { get; set; }
		private INExpedientesProveedor NExpedientesProveedor { get; set; }
		private INUsuarios NUsuarios { get; set; }

		public async Task<Boolean> Inicia()
		{
			EV.Medix = new();
			EV.Medix.ProcesoOperativoIdProveedor = await Servicios.ParamSist.Param<Int64>("RediinProveedorProcesoOperativoId");
			EV.Medix.ParamEstIdCaptura = await Servicios.ParamSist.Param<Int64>("RediinProveedorProcesoOperativoEstIdCaptura");
			EV.Medix.ParamEstIdAutorizado = await Servicios.ParamSist.Param<Int64>("RediinProveedorProcesoOperativoEstIdAutorizado");
			EV.Medix.ParamUrlRediinProveedores = await Servicios.ParamSist.Param<String>("RediinProveedorUrl");

			EV.Medix.ColumnaIdUsuario = UtilExpediente.ObtenRelacion(EV.ProcOperColumnasCap, nameof(EProveedorMedix.UsuarioId)).ColumnaId;
			if (EV.Medix.ColumnaIdUsuario <= 0)
			{
				NConExpedientes.Mensajes.AddError($"No se configuro correctamente el usuarioId para un nuevo usuario.");
				return false;
			}

			EV.Medix.ParamPerfilIdNvoUsr = await Servicios.ParamSist.Param<Int64>("RediinProveedorPerfilIdNvoUsr");
			if (EV.Medix.ParamPerfilIdNvoUsr <= 0)
			{
				NConExpedientes.Mensajes.AddError($"No se configuro correctamente el perfil para un nuevo usuario.");
				return false;
			}

			EV.Medix.ParamColumnaIdNombre = UtilExpediente.ObtenRelacion(EV.ProcOperColumnasCap, nameof(EProveedorMedix.NombreORazonSocial)).ColumnaId;
			if (!EV.ProcOperColumnasCon.Exists(e => e.ColumnaId == EV.Medix.ParamColumnaIdNombre))
			{
				NConExpedientes.Mensajes.AddError($"No se configuro correctamente la columna de nombre para este proceso operativo de proveedores [{EV.Medix.ParamColumnaIdNombre}].");
				return false;
			}
			EV.Medix.ParamColumnaIdCorreo = UtilExpediente.ObtenRelacion(EV.ProcOperColumnasCap, nameof(EProveedorMedix.CorreoElectronico1)).ColumnaId;
			//EV.Medix.ParamColumnaIdCorreo = UtilExpediente.ObtenRelacion(EV.ProcOperColumnasCap, nameof(EProveedorMedix.ContactoCorreoElectronico)).ColumnaId;
			if (!EV.ProcOperColumnasCon.Exists(e => e.ColumnaId == EV.Medix.ParamColumnaIdCorreo))
			{
				NConExpedientes.Mensajes.AddError($"No se configuro correctamente la columna de correo para este proceso operativo de proveedores [{EV.Medix.ParamColumnaIdCorreo}].");
				return false;
			}

			EV.Medix.ApiSapUsuario = Config.Valor<String>("MedixApiSapUsuario");
			EV.Medix.ApiSapPwd = Config.Valor<String>("MedixApiSapPwd");
			EV.Medix.ApiSapUrl = Config.Valor<String>("MedixApiSapUrl");

			return true;
		}
		public async Task<Boolean> Inserta(EConExpediente conExpediente)
		{
			String vNombre = ObtenValor(conExpediente, EV.Medix.ParamColumnaIdNombre).ToString();
			String vCorreo = ObtenValor(conExpediente, EV.Medix.ParamColumnaIdCorreo).ToString();
			if (String.IsNullOrWhiteSpace(vNombre))
				NExpedientes.Mensajes.AddError("El campo [Nombre o razón social] es obligatorio.");
			if (String.IsNullOrWhiteSpace(vNombre))
				NExpedientes.Mensajes.AddError("El campo [Correo] es obligatorio.");
			if (!NExpedientes.Mensajes.Ok)
				return false;

			conExpediente.ExpedienteId = await NConExpedientes.ConExpedienteInserta(conExpediente);
			if (!NConExpedientes.Mensajes.Ok)
				return false;

			//JRD VERIFICAR
			var vResultado = await CreaUsuario(conExpediente);
			EClave vCve = vResultado.Item1;
			EUsuario vUsuario = vResultado.Item2;

			if (NExpedientes.Mensajes.Ok)
			{
				foreach (var vValor in conExpediente.Valores)
				{
					if (vValor.ColumnaId == EV.Medix.ColumnaIdUsuario)
						UtilExpediente.EstableceValor(vValor, TiposColumna.Entero, vCve.UsuarioId.ToString());
				}
				await NConExpedientes.ConExpedienteActualiza(conExpediente);

				EnviaCorreo(vUsuario.CorreoElectronico,
							"Su usuario de Rediin Proveedores ha sido creado.",
							String.Format("Bienvenido a Rediin Proveedores.<br/><br/>Su usuario es {0}<br/>Su contraseña es {1}<br/><br/>La URL donde puede acceder a sus sistema es:<br/>{2}",
									vUsuario.Usuario, vCve.ClaveVerif, EV.Medix.ParamUrlRediinProveedores));
			}

			return true;
		}
		public void CambioEstatus(EConExpedienteCambioEstatus conExpedienteCambioEstatus)
		{
			String vCorreo = ObtenValor(EV.ConExpediente.Sel, EV.Medix.ParamColumnaIdCorreo).ToString();
			String vProveedor = ObtenValor(EV.ConExpediente.Sel, EV.Medix.ParamColumnaIdNombre).ToString();
			if (conExpedienteCambioEstatus.ProcesoOperativoEstId == EV.Medix.ParamEstIdCaptura)
			{
				EnviaCorreo(vCorreo,
							"Seguimiento en Portal de Rediin Proveedores",
							$"Estimado {vProveedor}:<br/><br/>Su alta como proveedor tiene las siguientes observaciones:<br/>{conExpedienteCambioEstatus.Comentarios}");
			}
			else if (conExpedienteCambioEstatus.ProcesoOperativoEstId == EV.Medix.ParamEstIdAutorizado)
			{
				EnviaCorreo(vCorreo,
							"Seguimiento en Portal de Rediin Proveedores",
							$"Estimado {vProveedor}:<br/><br/>Su alta como proveedor ha sido satisfactoria.");

				ActualizaAPI();
			}
		}
		public async void ActualizaAPI()
		{
			EMedixApi vApiBase = new();
			//Lenamos el proveedor
			vApiBase.proveedor.calle = "";


			//Convertimos el proveedor a base64
			string vApiBaseJson = JsonSerializer.Serialize(vApiBase);
			byte[] vApiBaseBytes = Encoding.UTF8.GetBytes(vApiBaseJson);
			string vApiBaseBase64 = Convert.ToBase64String(vApiBaseBytes);

			//Preparamos la entidad de envio
			EMedixApiEnviar vEnvio = new();
			vEnvio.solicitud.idApp = EV.Medix.ApiSapUsuario;
			vEnvio.solicitud.pwdApp = EV.Medix.ApiSapPwd;
			vEnvio.solicitud.enc_request = vApiBaseBase64;

			//Llamamos al API
			HttpResponseMessage vRes = await HttpClient.PostAsJsonAsync(EV.Medix.ApiSapUrl, vEnvio);
			if (vRes.IsSuccessStatusCode)
			{
				EMedixApiRespuesta vApiRespuesta = await vRes.Content.ReadFromJsonAsync<EMedixApiRespuesta>();
			}
		}

		public bool ValidaEstatus(long procesoOperativoEstId)
		{
			return EV.ConExpProcOperativo.Sel.ProcesoOperativoId == EV.Medix.ProcesoOperativoIdProveedor &&
					procesoOperativoEstId == EV.Medix.ParamEstIdCaptura;
		}

		private async void EnviaCorreo(String correoDestino, String subject, String body)
		{
			//JRD REVISAR QUE ESTE BIEN
			IMCorreo vCorreo = await Servicios.ServCorreo.ServCorreo("RediinProveedoresMail");
			vCorreo.To.Add(vCorreo.CreateUser("Cliente", correoDestino));
			vCorreo.Send(subject, body);

			//var vCorreo = base.ServidorCorreo("RediinProveedoresMail");
			//vCorreo.To.Add(vCorreo.NewUser("Cliente", correoDestino));
			//vCorreo.Send(subject, body);
		}
		private async Task<(EClave, EUsuario)> CreaUsuario(EConExpediente conExpediente)
		{
			EUsuario usuario = new();
			String vProveedor = ObtenValor(conExpediente, EV.Medix.ParamColumnaIdNombre).ToString();

			usuario.CorreoElectronico = ObtenValor(conExpediente, EV.Medix.ParamColumnaIdCorreo).ToString();
			usuario.EstablecimientoId = Servicios.EVDatosPortal.UsuarioSesion.EstablecimientoId;
			usuario.PerfilId = EV.Medix.ParamPerfilIdNvoUsr;
			UtilProveedorEspecif.SeparaNombreUsuario(vProveedor, usuario);
			usuario.Usuario += (DateTime.Now.Year - 2000).ToString();
			usuario.Usuario += DateTime.Now.DayOfYear.ToString();

			try
			{
				return (await NUsuarios.UsuarioInsertaAuto(usuario), usuario);
			}
			catch (Exception e)
			{
				NUsuarios.Mensajes.AddError(e.Message);
				return (null, null);
			}
		}
		private Object ObtenValor(EConExpediente conExpediente, Int64 columnaId)
		{
			return UtilExpediente.ObtenValor(EV.ProcOperColumnasCon,
											 conExpediente,
											 columnaId);
		}
	}
}
