using DSEntityNetX.Business.Rules;
using DSEntityNetX.Common.Casting;
using DSEntityNetX.Common.File;
using DSEntityNetX.Entities.Common;
using DSEntityNetX.Entities.Language;
using DSEntityNetX.Entities.Rules;
using DSEntityNetX.Mvc.Session;
using DSMetodNetX.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Rediin2022.Comun.PriOperacion;
using Rediin2022.Entidades.PriCatalogos;
using Rediin2022.Entidades.PriClientes;
using Rediin2022.Entidades.PriOperacion;
using RediinProvMedix2022Mvc.Models;
using Sisegui2020.Entidades.PriCatalogos;
using Sisegui2020.Entidades.PriSeguridad;
using System.Text.Json;

namespace RediinProvMedix2022Mvc.Areas.PriProveedores.Controllers
{
	[Area("PriProveedores")]
	[Authorize]
	public class ProveedoresController : Controller
	{
		#region Constructor
		public ProveedoresController(INExpedientes nExpedientes,
									 INExpedientesProveedor nExpedientesProveedor,
									 INParametrosSistema nParametrosSistema,
									 INConExpedientes nConExpedientes,
									 INBancos nBancos,
									 INIdentificaciones nIdentificaciones,
									 INPaises nPaises,
									 INEstablecimientos nEstablecimientos)
		{
			NExpedientes = nExpedientes;
			NExpedientesProveedor = nExpedientesProveedor;
			NParametrosSistema = nParametrosSistema;
			NConExpedientes = nConExpedientes;
			NBancos = nBancos;
			NIdentificaciones = nIdentificaciones;
			NPaises = nPaises;
			NEstablecimientos = nEstablecimientos;
		}
		#endregion

		#region Porpiedades
		public INPaises NPaises { get; set; }
		public INBancos NBancos { get; set; }
		public INIdentificaciones NIdentificaciones { get; set; }
		public INConExpedientes NConExpedientes { get; set; }
		public INExpedientesProveedor NExpedientesProveedor { get; set; }
		public INParametrosSistema NParametrosSistema { get; set; }
		public INExpedientes NExpedientes { get; set; }
		public INEstablecimientos NEstablecimientos { get; set; }

		public EVProveedor EV { get; set; }
        #endregion

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (EV == null)
                EV = new EVProveedor(HttpContext);
            base.OnActionExecuting(context);
        }
        public async Task<IActionResult> CapturaProveedorInicia()
		{
			EEstablecimiento vEstablecimiento =
				await NEstablecimientos.EstablecimientoXId(EV.EstablecimientoId, null);
			if (vEstablecimiento != null)
				EV.EstablecimientoRfc = vEstablecimiento.Rfc;
			else
				EV.EstablecimientoRfc = string.Empty;

			EV.ProcOper = await ParametroSistema(ProveedorParametrosSistema.RediinProveedorProcesoOperativoId);
			EV.EstatusIdCaptura = await ParametroSistema(ProveedorParametrosSistema.RediinProveedorProcesoOperativoEstIdCaptura);
			EV.EstatusIdRevision = await ParametroSistema(ProveedorParametrosSistema.RediinProveedorProcesoOperativoEstIdRevision);

			EDatosProveedor vProveedorXUsuario = await NExpedientesProveedor.ProveedorXUsuario(EV.ProcOper, EV.UsuarioId);
			if (vProveedorXUsuario == null || string.IsNullOrWhiteSpace(vProveedorXUsuario.Proveedor))
				return View("SinDatos");

			EV.Proveedor = JsonSerializer.Deserialize<EProveedorMontePio>(vProveedorXUsuario.Proveedor) ?? new();
			EV.ProveedorReglas = vProveedorXUsuario.ReglasNegocio;

			EConExpedienteObjetoFiltro vObjetoFiltro = new();
			vObjetoFiltro.ExpedienteId = EV.Proveedor?.ExpedienteId ?? 0;
			vObjetoFiltro.DatPag = new MEDatosPaginador()
			{
				PageSize = Int32.MaxValue,
				StartLine = 1,
				CurrentPage = 1
			};

			EV.Objetos = await NConExpedientes.ConExpedienteObjetoPag(vObjetoFiltro);

			return await ProveedorActualizaIni(0);
		}

		public async Task<IActionResult> ProveedorActualizaIni(Int32 pagina)
		{
			EProveedorMontePio vProveedor = EV.Proveedor;
			EConExpedienteObjetoPag vDocumentacion = EV.Objetos;
			return await ProveedorActualizaCap(vProveedor, pagina);
		}
		public async Task<IActionResult> ProveedorActualizaCap(EProveedorMontePio proveedor, [FromQuery] Int32 pagina)
		{
			List<MEReglaNeg> vProveedorReglas = EV.ProveedorReglas;
			EConExpedienteObjetoPag vDocumentacion = EV.Objetos;

			ViewBag.EstablecimientoNombre = EV.EstablecimientoNombre;
			ViewBag.EstablecimientoRfc = EV.EstablecimientoRfc;

			ViewBag.Reglas = vProveedorReglas;
			ViewBag.Bancos = await NBancos.BancoCmb();
			ViewBag.Identificaciones = await NIdentificaciones.IdentificacionCmb();

			ViewBag.Colonia = null;
			ViewBag.Municipio = null;
			ViewBag.Estados = await NPaises.EstadoCmb(1);
			if (proveedor.EstadoId > 0)
			{
				ViewBag.Municipio = await NPaises.MunicipioCmb(proveedor.EstadoId);
				if (proveedor.MunicipioId > 0)
					ViewBag.Colonia = await NPaises.ColoniaCmb(proveedor.MunicipioId);
			}

			ViewBag.EsCaptura = EV.EstatusIdCaptura == proveedor.ProcesoOperativoEstId;

			ViewBag.Documentos = vDocumentacion.Pagina;
			ViewBag.Pagina = pagina;
			return View("CapturaProveedor", proveedor);
		}
		public async Task<IActionResult> ProveedorActualiza(EProveedorMontePio proveedor, Int32 pagina)
		{
			NExpedientes.Mensajes.Initialize();
			//Carga de otros datos de combos
			proveedor.PaisId = 1;

			//proveedor.Pais = String.Empty;
			//proveedor.Estado = String.Empty;
			//proveedor.Municipio = String.Empty;
			//proveedor.Colonia = String.Empty;
			//proveedor.Banco = String.Empty;

			//JRD PENDIENTE 18/09/2024
			//proveedor.Identificacion = String.Empty;
			//proveedor.PoderNotarialIdentificacion = String.Empty;

			//proveedor.Pais = NPaises.PaisXId(1)?.PaisNombre ?? String.Empty;
			//if (proveedor.EstadoId > 0)
			//	proveedor.Estado = NPaises.EstadoXId(proveedor.EstadoId)?.EstadoNombre ?? String.Empty;
			//if (proveedor.MunicipioId > 0)
			//	proveedor.Municipio = NPaises.MunicipioXId(proveedor.MunicipioId)?.MunicipioNombre ?? String.Empty;
			//if (proveedor.ColoniaId > 0)
			//	proveedor.Colonia = NPaises.ColoniaXId(proveedor.ColoniaId)?.ColoniaNombre ?? String.Empty;
			//if (proveedor.BancoId > 0)
			//	proveedor.Banco = NBancos.BancoXId(proveedor.BancoId)?.BancoNombre ?? String.Empty;
			//if (proveedor.BancoId2 > 0)
			//	proveedor.Banco2 = NBancos.BancoXId(proveedor.BancoId2)?.BancoNombre ?? String.Empty;
			//if (proveedor.BancoId3 > 0)
			//	proveedor.Banco3 = NBancos.BancoXId(proveedor.BancoId3)?.BancoNombre ?? String.Empty;
			//if (proveedor.IdentificacionId > 0)
			//	proveedor.Identificacion = NIdentificaciones.IdentificacionXId(proveedor.IdentificacionId)?.IdentificacionNombre ?? String.Empty;
			//if (proveedor.PoderNotarialIdentificacionId > 0)
			//	proveedor.PoderNotarialIdentificacion = NIdentificaciones.IdentificacionXId(proveedor.PoderNotarialIdentificacionId)?.IdentificacionNombre ?? String.Empty;

			//Actualizacion
			if (await NExpedientesProveedor.ProveedorActualiza(new EString() { StringValue = JsonSerializer.Serialize(proveedor) }))
			{
				EDatosProveedor vProveedorXUsuario = await NExpedientesProveedor.ProveedorXUsuario(proveedor.ProcesoOperativoId, proveedor.UsuarioId);
				EV.ProveedorReglas = vProveedorXUsuario.ReglasNegocio;
				if (string.IsNullOrWhiteSpace(vProveedorXUsuario.Proveedor))
					EV.Proveedor = JsonSerializer.Deserialize<EProveedorMontePio>(vProveedorXUsuario.Proveedor) ?? new();
				else
					EV.Proveedor = new();

				NExpedientes.Mensajes.AddOk("Datos guardados correctamente.");
			}

			ViewBag.Mensajes = NExpedientes.Mensajes;
			return await ProveedorActualizaCap(proveedor, pagina);
		}

		public async Task<IActionResult> SubeArchivoIni(Int32 indice)
		{
			EV.Indice = indice;
			ViewBag.SubeArchivo = true;
			return await ProveedorActualizaIni(3);
		}
		public async Task<IActionResult> SubeArchivo(IFormFile archivo)
		{
			Int32 indice = EV.Indice;
			if (archivo == null || archivo.Length == 0)
			{
				NExpedientes.Mensajes.AddError("El archivo esta vacio, no se puede continuar.");
				ViewBag.MensajesSA = NExpedientes.Mensajes;
				return await SubeArchivoIni(indice);
				//return ProveedorActualizaIni(3);
			}

			EConExpedienteObjetoPag vDocumentacion = EV.Objetos;
			EConExpedienteObjeto vExpedienteObjeto = vDocumentacion.Pagina[indice];

			EExpedienteObjeto vExpObj = new();
			vExpObj.ExpedienteId = vExpedienteObjeto.ExpedienteId;
			vExpObj.ExpedienteObjetoId = vExpedienteObjeto.ExpedienteObjetoId;
			String vNombre = vExpedienteObjeto.ProcesoOperativoObjetoNombre + Path.GetExtension(archivo.FileName);
			vExpedienteObjeto.ArchivoNombre = vNombre;
			vExpObj.ArchivoNombre = vNombre;
			vExpObj.ProcesoOperativoObjetoId = 1; //Solo debe ser mayor que cero.
			vExpObj.Activo = true;

			try
			{
				using MemoryStream vMS = new MemoryStream();
				archivo.CopyTo(vMS);
				vExpObj.Archivo = vMS.ToArray();
				if (await NExpedientes.ObjetoActualiza(vExpObj))
				{
					EConExpedienteObjetoFiltro vObjetoFiltro = new();
					vObjetoFiltro.ExpedienteId = vExpedienteObjeto.ExpedienteId;
					vObjetoFiltro.DatPag = new MEDatosPaginador()
					{
						PageSize = Int32.MaxValue,
						StartLine = 1,
						CurrentPage = 1
					};

					EV.Objetos = await NConExpedientes.ConExpedienteObjetoPag(vObjetoFiltro);

					NExpedientes.Mensajes.AddOk("El archivo se subió correctamente.");
					ViewBag.Mensajes = NExpedientes.Mensajes;
				}
			}
			catch (Exception ex)
			{
				NExpedientes.Mensajes.AddError(ex.Message);
			}

			return await ProveedorActualizaIni(3);
		}
		public async Task<IActionResult> EliminaArchivo(Int32 indice)
		{
			EConExpedienteObjetoPag vDocumentacion = EV.Objetos;
			EConExpedienteObjeto vExpedienteObjeto = vDocumentacion.Pagina[indice];

			EExpedienteObjeto vExpObj = new();
			vExpObj.ExpedienteId = vExpedienteObjeto.ExpedienteId;
			vExpObj.ExpedienteObjetoId = vExpedienteObjeto.ExpedienteObjetoId;
			vExpObj.Ruta = String.Empty;
			vExpObj.ArchivoNombre = String.Empty;
			vExpObj.ProcesoOperativoObjetoId = 1; //Solo debe ser mayor que cero.
			vExpObj.Activo = true;
			vExpObj.Eliminar = true;

			if (await NExpedientes.ObjetoActualiza(vExpObj))
			{
				EConExpedienteObjetoFiltro vObjetoFiltro = new();
				vObjetoFiltro.ExpedienteId = vExpedienteObjeto.ExpedienteId;
				vObjetoFiltro.DatPag = new MEDatosPaginador()
				{
					PageSize = Int32.MaxValue,
					StartLine = 1,
					CurrentPage = 1
				};

				EV.Objetos = await NConExpedientes.ConExpedienteObjetoPag(vObjetoFiltro);
			}

			return await ProveedorActualizaIni(3);
		}
		public async Task<IActionResult> DescargaArchivo(Int32 indice)
		{
			EConExpedienteObjetoPag vDocumentacion = EV.Objetos;
			EConExpedienteObjeto expedienteObjeto = vDocumentacion.Pagina[indice];
			EDocumento documento = await NExpedientes.ObjectoDescargaDocto(expedienteObjeto.ExpedienteId, expedienteObjeto.ArchivoNombre);
			if (documento == null)
				return await ProveedorActualizaIni(3);

			return File(documento.Documento,
						XUtilFile.GetMimeType(Path.GetExtension(documento.ArchivoNombre)),
						documento.ArchivoNombre);
		}
		public async Task<IActionResult> CopiaActaConstitutiva(EProveedorMontePio proveedor)
		{
			proveedor.PoderNotarialNotarioNombre = proveedor.NotarioNombre;
			proveedor.PoderNotarialNumEscritura = proveedor.NumeroEscritura;
			proveedor.PoderNotarialFechaEscritura = proveedor.FechaEscritura;
			proveedor.PoderNotarialRepresentanteLegal = proveedor.RepresentanteLegal;
			proveedor.PoderNotarialIdentificacionId = proveedor.IdentificacionId;
			proveedor.PoderNotarialNumIdentificacion = proveedor.NumIdentificacion;
			EV.Proveedor = proveedor;

			return await ProveedorActualizaCap(proveedor, 4);
		}
		public async Task<IActionResult> RevisionExpediente(EProveedorMontePio proveedor)
		{
			await ProveedorActualiza(proveedor, 0);

			//Validacion
			NExpedientes.Mensajes.Initialize();
			EProveedorMontePio vProveedor = EV.Proveedor;
			IXBusinessRules<EProveedorMontePio, MEReglaNeg> vReglas =
				new XBusinessRules<EProveedorMontePio, MEReglaNeg>(NExpedientes.Mensajes, EV.ProveedorReglas);

			foreach (var vReg in vReglas.Rules)
			{
				if (vReg.Property == nameof(EProveedorMontePio.NumeroInterior) ||
					vReg.Property == nameof(EProveedorMontePio.BancoId2) ||
					vReg.Property == nameof(EProveedorMontePio.Cuenta2) ||
					vReg.Property == nameof(EProveedorMontePio.CuentaClabe2) ||
					vReg.Property == nameof(EProveedorMontePio.BancoId3) ||
					vReg.Property == nameof(EProveedorMontePio.Cuenta3) ||
					vReg.Property == nameof(EProveedorMontePio.CuentaClabe3) ||
					vReg.Property == nameof(EProveedorMontePio.SapSociedadId) ||
					vReg.Property == nameof(EProveedorMontePio.SapSociedadGLId) ||
					vReg.Property == nameof(EProveedorMontePio.SapGrupoCuentaId) ||
					vReg.Property == nameof(EProveedorMontePio.SapOrganizacionCompraId) ||
					vReg.Property == nameof(EProveedorMontePio.SapTratamientoId) ||
					vReg.Property == nameof(EProveedorMontePio.SapCuentaAsociadaId) ||
					vReg.Property == nameof(EProveedorMontePio.SapGrupoTesoreriaId) ||
					vReg.Property == nameof(EProveedorMontePio.SapBancoId) ||
					vReg.Property == nameof(EProveedorMontePio.SapCondicionPagoId) ||
					vReg.Property == nameof(EProveedorMontePio.SapViaPagoId) ||
					vReg.Property == nameof(EProveedorMontePio.SapGrupoToleranciaId))
					continue;

				vReg.MessageForExpr = XLanguageXId.MessageForExpr;
				vReg.MessageForRange = XLanguageXId.MessageForRange;
				vReg.MessageForRequired = XLanguageXId.MessageForRequired;
				vReg.Required = true;
			}

			vReglas.Validate(vProveedor);

			EConExpedienteObjetoPag vDocumentacion = EV.Objetos;
			foreach (var vDoc in vDocumentacion.Pagina)
			{
				if (String.IsNullOrWhiteSpace(vDoc.ArchivoNombre))
					NExpedientes.Mensajes.AddError($"Falta documento: {vDoc.ProcesoOperativoObjetoNombre}");
			}

			if (!NExpedientes.Mensajes.Ok)
			{
				ViewBag.Mensajes = NExpedientes.Mensajes;
				return await ProveedorActualizaCap(vProveedor, 0);
			}

			//Actualizacion
			await NConExpedientes.ConExpedienteCambioEstatus(new EConExpedienteCambioEstatus()
			{
				ExpedienteId = vProveedor.ExpedienteId,
				ProcesoOperativoEstId = EV.EstatusIdRevision
			});
			EDatosProveedor vProveedorXUsuario = 
				await NExpedientesProveedor.ProveedorXUsuario(vProveedor.ProcesoOperativoId,
															  vProveedor.UsuarioId);
			EV.ProveedorReglas = vProveedorXUsuario.ReglasNegocio;
			if (!string.IsNullOrWhiteSpace(vProveedorXUsuario.Proveedor))
				EV.Proveedor = JsonSerializer.Deserialize<EProveedorMontePio>(vProveedorXUsuario.Proveedor) ?? new();
			else
				EV.Proveedor = new();

			return await ProveedorActualizaCap(vProveedor, 0);
		}

		private async Task<Int64> ParametroSistema(ProveedorParametrosSistema nombreParametro)
		{
			return await UtilProveedorEspecif.ParamSistemaInt64(NParametrosSistema, nombreParametro);
		}
	}
}
