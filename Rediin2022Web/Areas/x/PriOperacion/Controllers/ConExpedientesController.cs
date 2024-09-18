using DSEntityNetX.Common.Casting;
using DSEntityNetX.Common.File;
using DSEntityNetX.Common.Pagination;
using DSEntityNetX.Common.Security;
using DSMetodNetX.Aplicacion;
using DSMetodNetX.Entidades;
using DSMetodNetX.Mvc.Seguridad;
using DSMetodNetX.Mvc.Seguridad.Correo;
using GroupDocs.Viewer.Options;
using MailKit.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MimeKit;
using Rediin2022.Aplicacion.PriCatalogos;
using Rediin2022.Aplicacion.PriOperacion;
using Rediin2022.Entidades.Idioma;
using Rediin2022.Entidades.PriCatalogos;
using Rediin2022.Entidades.PriClientes;
using Rediin2022.Entidades.PriOperacion;
using Sisegui2020.Aplicacion.PriSeguridad;
using Sisegui2020.Entidades.Idioma;
using Sisegui2020.Entidades.PriCatalogos;
using Sisegui2020.Entidades.PriSeguridad;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Rediin2022Web.Areas.PriOperacion.Controllers
{
	[Area("PriOperacion")]
	public class ConExpedientesController : MControllerMvcPri
	{
		#region Constructores
		public ConExpedientesController(INConExpedientes nConExpedientes,
										INProcesosOperativos nProcesosOperativos,
										INUsuarios nUsuarios,
										INExpedientes nExpedientes,
										//Para proveedores
										INPaises nPaises,
										INBancos nBancos,
										INSapSociedades nSapSociedades,
										INSapSociedadesGL nSapSociedadesGL,
										INSapGrupoCuentas nSapGrupoCuentas,
										INSapOrganizacionesCompra nSapOrganizacionesCompra,
										INSapTratamientos nSapTratamientos,
										INSapCuentasAsociadas nSapCuentasAsociadas,
										INSapGruposTesoreria nSapGruposTesoreria,
										INSapBancos nSapBancos,
										INSapCondicionesPago nSapCondicionesPago,
										INSapViasPago nSapViasPago,
										INSapGruposTolerancia nSapGruposTolerancia)
		{
			NConExpedientes = nConExpedientes;
			NProcesosOperativos = nProcesosOperativos;
			NUsuarios = nUsuarios;
			NExpedientes = nExpedientes;

			//Para proveedores
			NPaises = nPaises;
			NBancos = nBancos;
			NSapSociedades = nSapSociedades;
			NSapSociedadesGL = nSapSociedadesGL;
			NSapGrupoCuentas = nSapGrupoCuentas;
			NSapOrganizacionesCompra = nSapOrganizacionesCompra;
			NSapTratamientos = nSapTratamientos;
			NSapCuentasAsociadas = nSapCuentasAsociadas;
			NSapGruposTesoreria = nSapGruposTesoreria;
			NSapBancos = nSapBancos;
			NSapCondicionesPago = nSapCondicionesPago;
			NSapViasPago = nSapViasPago;
			NSapGruposTolerancia = nSapGruposTolerancia;
		}
		#endregion

		#region Propiedades
		private INConExpedientes NConExpedientes { get; set; }
		private INProcesosOperativos NProcesosOperativos { get; set; }
		private INUsuarios NUsuarios { get; set; }
		private INExpedientes NExpedientes { get; set; }
		private INPaises NPaises { get; set; } //Proveedores
		private INBancos NBancos { get; set; } //Proveedores

		private INSapSociedades NSapSociedades { get; set; } //Proveedores
		private INSapSociedadesGL NSapSociedadesGL { get; set; } //Proveedores
		private INSapGrupoCuentas NSapGrupoCuentas { get; set; } //Proveedores
		private INSapOrganizacionesCompra NSapOrganizacionesCompra { get; set; } //Proveedores
		private INSapTratamientos NSapTratamientos { get; set; } //Proveedores
		private INSapCuentasAsociadas NSapCuentasAsociadas { get; set; } //Proveedores
		private INSapGruposTesoreria NSapGruposTesoreria { get; set; } //Proveedores
		private INSapBancos NSapBancos { get; set; } //Proveedores
		private INSapCondicionesPago NSapCondicionesPago { get; set; } //Proveedores
		private INSapViasPago NSapViasPago { get; set; } //Proveedores
		private INSapGruposTolerancia NSapGruposTolerancia { get; set; } //Proveedores
		private EVConExpedientes EVConExpedientes
		{
			get
			{
				if (base.MSesion<EVConExpedientes>() == null)
					base.MSesion(new EVConExpedientes());

				return base.MSesionAuto<EVConExpedientes>();
			}
		}
		#endregion

		#region ConExpProcOperativo (Enc)

		#region Acciones
		public IActionResult ConExpProcOperativoInicia()
		{
			//Configuracion de inicio
			if (String.IsNullOrWhiteSpace(EVConExpedientes.ConExpProcOperativoColOrden))
				EVConExpedientes.ConExpProcOperativoColOrden = nameof(EConExpProcOperativo.Orden);

			return RedirectToAction(nameof(ConExpProcOperativoCon));
		}
		[MValidaSeg(nameof(ConExpProcOperativoInicia))]
		public IActionResult ConExpProcOperativoCon()
		{
			base.MCargaFiltroPagYOrd(EVConExpedientes.ConExpProcOperativoFiltro,
									 EVConExpedientes.ConExpProcOperativoPag,
									 EVConExpedientes.ConExpProcOperativoColOrden,
									 nameof(EConExpProcOperativo));

			EVConExpedientes.ConExpProcOperativoPag = NConExpedientes.ConExpProcOperativoPag(EVConExpedientes.ConExpProcOperativoFiltro);
			base.MActualizaTamPag(EVConExpedientes.ConExpProcOperativoPag?.DatPag);

			ViewBag.Mensajes = base.MObtenMensajes(NConExpedientes.Mensajes);
			ViewBag.DatPag = EVConExpedientes.ConExpProcOperativoPag?.DatPag;
			ViewBag.Orden = EVConExpedientes.ConExpProcOperativoColOrden;
			ViewBag.Filtro = EVConExpedientes.ConExpProcOperativoFiltro;
			ViewBag.Indice = EVConExpedientes.ConExpProcOperativoIndice;

			return View(nameof(ConExpProcOperativoCon), EVConExpedientes.ConExpProcOperativoPag?.Pagina);
		}
		#endregion

		#region Funciones
		#endregion

		#region Acciones de Paginacion Orden y Filtro
		[MValidaSeg(nameof(ConExpProcOperativoInicia))]
		public IActionResult ConExpProcOperativoPaginacion(MEDatosPaginador datPag)
		{
			EVConExpedientes.ConExpProcOperativoPag.DatPag = datPag;
			return RedirectToAction(nameof(ConExpProcOperativoCon));
		}
		[MValidaSeg(nameof(ConExpProcOperativoInicia))]
		public IActionResult ConExpProcOperativoOrdena(String orden)
		{
			EVConExpedientes.ConExpProcOperativoColOrden = orden;
			return RedirectToAction(nameof(ConExpProcOperativoCon));
		}
		[MValidaSeg(nameof(ConExpProcOperativoInicia))]
		public IActionResult ConExpProcOperativoFiltra(EConExpProcOperativoFiltro filtro)
		{
			EVConExpedientes.ConExpProcOperativoFiltro = filtro;
			return RedirectToAction(nameof(ConExpProcOperativoCon));
		}
		[MValidaSeg(nameof(ConExpProcOperativoInicia))]
		public IActionResult ConExpProcOperativoLimpiaFiltros()
		{
			EVConExpedientes.ConExpProcOperativoFiltro = new EConExpProcOperativoFiltro();
			return RedirectToAction(nameof(ConExpProcOperativoCon));
		}
		#endregion

		#endregion

		#region ConExpediente (Exp)

		#region Acciones
		public IActionResult ConExpedienteInicia(Int32 indice)
		{
			//Configuracion de inicio
			if (String.IsNullOrWhiteSpace(EVConExpedientes.ConExpedienteColOrden))
				EVConExpedientes.ConExpedienteColOrden = "-" + nameof(EConExpediente.ExpedienteId);

			if (indice >= 0)
			{
				EVConExpedientes.ConExpProcOperativoIndice = indice;
				EVConExpedientes.ConExpProcOperativoSel = EVConExpedientes.ConExpProcOperativoPag.Pagina[indice];
			}

			//Entidades adicionales
			EVConExpedientes.ProcOperColumnasCon =
				NProcesosOperativos.ProcesoOperativoColCT(EVConExpedientes.ConExpProcOperativoSel.ProcesoOperativoId);

			//Ordenar columnas para la captura
			if (EVConExpedientes.ProcOperColumnasCon != null)
			{
				EVConExpedientes.ProcOperColumnasCap =
					(from vCol in EVConExpedientes.ProcOperColumnasCon
					 where vCol.CapOrden > 0
					 orderby vCol.CapOrden
					 select vCol).ToList();
			}
			else
				EVConExpedientes.ProcOperColumnasCap = new List<EProcesoOperativoCol>();

			//EVConExpedientes.ProcOperColumnas = NProcesosOperativos.ProcesoOperativoColCT(procesoOperativoCol.ProcesoOperativoId);

			//Cargamos la informacion de los combos
			if (EVConExpedientes.ProcOperColumnasCap != null && EVConExpedientes.ProcOperColumnasCap.Count > 0)
			{
				foreach (EProcesoOperativoCol vCol in EVConExpedientes.ProcOperColumnasCap)
				{
					if (vCol.CapCmbProcesoOperativoId > 0)
						vCol.ElementosCmb = NConExpedientes.ConExpedienteCmb(vCol);
				}
			}

			//No config Proveedor
			EVConExpedientes.ParamProveedorProcesoOperativoId = base.MParametroSist<Int64>("RediinProveedorProcesoOperativoId");
			if (EVConExpedientes.ConExpProcOperativoSel.ProcesoOperativoId == EVConExpedientes.ParamProveedorProcesoOperativoId)
			{
				var vRelaciones = NExpedientes.RelacionProcesoOperativo(EVConExpedientes.ParamProveedorProcesoOperativoId);
				EVConExpedientes.ParamEstIdCaptura = base.MParametroSist<Int64>("RediinProveedorProcesoOperativoEstIdCaptura");
				EVConExpedientes.ParamEstIdAutorizado = base.MParametroSist<Int64>("RediinProveedorProcesoOperativoEstIdAutorizado");
				EVConExpedientes.ParamUrlRediinProveedores = base.MParametroSist<String>("RediinProveedorUrl");


				EVConExpedientes.ProveedorColumnaIdUsuario = UtilExpediente.ObtenRelacion(vRelaciones, nameof(EProveedor.UsuarioId)).ColumnaId;
				if (EVConExpedientes.ProveedorColumnaIdUsuario <= 0)
				{
					NConExpedientes.Mensajes.AddError($"No se configuro correctamente el usuarioId para un nuevo usuario.");
					return ConExpProcOperativoCon();
				}

				EVConExpedientes.ParamPerfilIdNvoUsr = base.MParametroSist<Int64>("RediinProveedorPerfilIdNvoUsr");
				if (EVConExpedientes.ParamPerfilIdNvoUsr <= 0)
				{
					NConExpedientes.Mensajes.AddError($"No se configuro correctamente el perfil para un nuevo usuario.");
					return ConExpProcOperativoCon();
				}
				EVConExpedientes.ParamProveedorColumnaIdNombre = UtilExpediente.ObtenRelacion(vRelaciones, nameof(EProveedor.NombreORazonSocial)).ColumnaId;
				EVConExpedientes.ParamProveedorColumnaIdCorreo = UtilExpediente.ObtenRelacion(vRelaciones, nameof(EProveedor.ContactoCorreoElectronico)).ColumnaId;
				if (!EVConExpedientes.ProcOperColumnasCon.Exists(e => e.ColumnaId == EVConExpedientes.ParamProveedorColumnaIdNombre))
				{
					NConExpedientes.Mensajes.AddError($"No se configuro correctamente la columna de nombre para este proceso operativo de proveedores [{EVConExpedientes.ParamProveedorColumnaIdNombre}].");
					return ConExpProcOperativoCon();
				}
				if (!EVConExpedientes.ProcOperColumnasCon.Exists(e => e.ColumnaId == EVConExpedientes.ParamProveedorColumnaIdCorreo))
				{
					NConExpedientes.Mensajes.AddError($"No se configuro correctamente la columna de correo para este proceso operativo de proveedores [{EVConExpedientes.ParamProveedorColumnaIdCorreo}].");
					return ConExpProcOperativoCon();
				}

				//Para catalogos
				EVConExpedientes.ParamProveedorColumnaIdPais = UtilExpediente.ObtenRelacion(vRelaciones, nameof(EProveedor.PaisId)).ColumnaId;
				EVConExpedientes.ParamProveedorColumnaIdEstado = UtilExpediente.ObtenRelacion(vRelaciones, nameof(EProveedor.EstadoId)).ColumnaId;
				EVConExpedientes.ParamProveedorColumnaIdMunicipio = UtilExpediente.ObtenRelacion(vRelaciones, nameof(EProveedor.MunicipioId)).ColumnaId;
				EVConExpedientes.ParamProveedorColumnaIdColonia = UtilExpediente.ObtenRelacion(vRelaciones, nameof(EProveedor.ColoniaId)).ColumnaId;
				List<MEElemento> vBancos = NBancos.BancoCmb();
				EVConExpedientes.CombosProveedores = new Dictionary<Int64, List<MEElemento>>()
				{
					{ EVConExpedientes.ParamProveedorColumnaIdPais, NPaises.PaisCmb() },
					{ EVConExpedientes.ParamProveedorColumnaIdEstado, null},
					{ EVConExpedientes.ParamProveedorColumnaIdMunicipio, null},
					{ EVConExpedientes.ParamProveedorColumnaIdColonia, null},
					{ UtilExpediente.ObtenRelacion(vRelaciones, nameof(EProveedor.BancoId)).ColumnaId, vBancos },
					{ UtilExpediente.ObtenRelacion(vRelaciones, nameof(EProveedor.BancoId2)).ColumnaId, vBancos },
					{ UtilExpediente.ObtenRelacion(vRelaciones, nameof(EProveedor.BancoId3)).ColumnaId, vBancos },
					{ UtilExpediente.ObtenRelacion(vRelaciones, nameof(EProveedor.SapSociedadId)).ColumnaId, NSapSociedades.SapSociedadCmb() },
					{ UtilExpediente.ObtenRelacion(vRelaciones, nameof(EProveedor.SapSociedadGLId)).ColumnaId, NSapSociedadesGL.SapSociedadGLCmb() },
					{ UtilExpediente.ObtenRelacion(vRelaciones, nameof(EProveedor.SapGrupoCuentaId)).ColumnaId, NSapGrupoCuentas.SapGrupoCuentaCmb() },
					{ UtilExpediente.ObtenRelacion(vRelaciones, nameof(EProveedor.SapOrganizacionCompraId)).ColumnaId, NSapOrganizacionesCompra.SapOrganizacionCompraCmb() },
					{ UtilExpediente.ObtenRelacion(vRelaciones, nameof(EProveedor.SapTratamientoId)).ColumnaId, NSapTratamientos.SapTratamientoCmb() },
					{ UtilExpediente.ObtenRelacion(vRelaciones, nameof(EProveedor.SapCuentaAsociadaId)).ColumnaId, NSapCuentasAsociadas.SapCuentaAsociadaCmb() },
					{ UtilExpediente.ObtenRelacion(vRelaciones, nameof(EProveedor.SapGrupoTesoreriaId)).ColumnaId, NSapGruposTesoreria.SapGrupoTesoreriaCmb() },
					{ UtilExpediente.ObtenRelacion(vRelaciones, nameof(EProveedor.SapBancoId)).ColumnaId, NSapBancos.SapBancoCmb() },
					{ UtilExpediente.ObtenRelacion(vRelaciones, nameof(EProveedor.SapCondicionPagoId)).ColumnaId, NSapCondicionesPago.SapCondicionPagoCmb() },
					{ UtilExpediente.ObtenRelacion(vRelaciones, nameof(EProveedor.SapViaPagoId)).ColumnaId, NSapViasPago.SapViaPagoCmb() },
					{ UtilExpediente.ObtenRelacion(vRelaciones, nameof(EProveedor.SapGrupoToleranciaId)).ColumnaId, NSapGruposTolerancia.SapGrupoToleranciaCmb() },
				};
			}
			//No config Proveedor

			return RedirectToAction(nameof(ConExpedienteCon));
		}
		[MValidaSeg(nameof(ConExpedienteInicia))]
		public IActionResult ConExpedienteCon()
		{
			EVConExpedientes.ConExpedienteFiltro.ProcesoOperativoId = EVConExpedientes.ConExpProcOperativoSel.ProcesoOperativoId;
			EVConExpedientes.ConExpedienteFiltro.ControlEstatus = EVConExpedientes.ConExpProcOperativoSel.ControlEstatus;
			base.MCargaFiltroPagYOrd(EVConExpedientes.ConExpedienteFiltro,
									 EVConExpedientes.ConExpedientePag,
									 EVConExpedientes.ConExpedienteColOrden,
									 nameof(EConExpediente));

			//Adi
			EVConExpedientes.ConExpedienteFiltro.ColumnaId =
				XString.XToInt64(EVConExpedientes.ConExpedienteFiltro.ColumnaOrden);
			if (EVConExpedientes.ConExpedienteFiltro.ColumnaId < 0)
				EVConExpedientes.ConExpedienteFiltro.ColumnaId *= -1;
			if (EVConExpedientes.ConExpedienteFiltro.ColumnaId > 0)
				EVConExpedientes.ConExpedienteFiltro.Ascendente =
					!EVConExpedientes.ConExpedienteFiltro.ColumnaOrden.StartsWith("-");

			EVConExpedientes.ConExpedientePag = NConExpedientes.ConExpedientePag(EVConExpedientes.ConExpedienteFiltro);
			base.MActualizaTamPag(EVConExpedientes.ConExpedientePag?.DatPag);

			ViewBag.Mensajes = base.MObtenMensajes(NConExpedientes.Mensajes);
			ViewBag.DatPag = EVConExpedientes.ConExpedientePag?.DatPag;
			ViewBag.Orden = EVConExpedientes.ConExpedienteColOrden;
			ViewBag.Filtro = EVConExpedientes.ConExpedienteFiltro;
			ViewBag.Indice = EVConExpedientes.ConExpedienteIndice;
			ViewBag.SelColGrupoId = EVConExpedientes.ConExpedienteSelColGrupoId;

			ViewBag.ProcesosOperativosEst =
				NProcesosOperativos.ProcesoOperativoEstCmb(EVConExpedientes.ConExpProcOperativoSel.ProcesoOperativoId); //Mod

			//Adi
			//ViewBag.ProcOperColumnas = EVConExpedientes.ProcOperColumnasCon;

			ViewBag.ProcOperColumnas =
				(from vCol in EVConExpedientes.ProcOperColumnasCon
				 where vCol.ConOrden > 0
				 orderby vCol.ConOrden
				 select vCol).ToList();

			ViewBag.ControlEstatus = EVConExpedientes.ConExpProcOperativoSel.ControlEstatus;

			return View(nameof(ConExpedienteCon), EVConExpedientes.ConExpedientePag?.Pagina);
		}

		public IActionResult ConExpedienteXId(Int32 indice)
		{
			EVConExpedientes.Accion = MAccionesGen.Consulta;
			EVConExpedientes.ConExpedienteIndice = indice;
			return ConExpedienteCaptura(EVConExpedientes.ConExpedientePag.Pagina[indice]);
		}
		[MValidaSeg(nameof(ConExpedienteInserta))]
		public IActionResult ConExpedienteInsertaIni()
		{
			EVConExpedientes.Accion = MAccionesGen.Inserta;
			return ConExpedienteInsertaCap(new EConExpediente());
		}
		[ValidateAntiForgeryToken]
		[MValidaSeg(nameof(ConExpedienteInserta))]
		public IActionResult ConExpedienteInsertaCap(EConExpediente conExpediente)
		{
			return ConExpedienteCaptura(conExpediente);
		}
		[ValidateAntiForgeryToken]
		[MValidaSeg(nameof(ConExpedienteInserta))]
		public IActionResult ConExpedienteInsertaCap2(IFormCollection conExp, Int64 PEMColumnaId)
		{
			EConExpediente conExpediente = ObtenExpediente(conExp);
			conExpediente.ProcesoOperativoId = EVConExpedientes.ConExpProcOperativoSel.ProcesoOperativoId; //Llave padre
			conExpediente.ControlEstatus = EVConExpedientes.ConExpProcOperativoSel.ControlEstatus;
			AjustaComboCascadaPEMProv(conExpediente, PEMColumnaId);
			return ConExpedienteInsertaCap(conExpediente);
		}
		[ValidateAntiForgeryToken]
		public IActionResult ConExpedienteInserta(IFormCollection conExp)
		{
			EConExpediente conExpediente = ObtenExpediente(conExp);
			conExpediente.ProcesoOperativoId = EVConExpedientes.ConExpProcOperativoSel.ProcesoOperativoId; //Llave padre
			conExpediente.ControlEstatus = EVConExpedientes.ConExpProcOperativoSel.ControlEstatus;
			//conExpediente.ProcesoOperativoEstId = 0L;

			if (EVConExpedientes.ConExpProcOperativoSel.ProcesoOperativoId == EVConExpedientes.ParamProveedorProcesoOperativoId)
			{
				String vNombre = ObtenValor(conExpediente, EVConExpedientes.ParamProveedorColumnaIdNombre).ToString();
				String vCorreo = ObtenValor(conExpediente, EVConExpedientes.ParamProveedorColumnaIdCorreo).ToString();
				if (String.IsNullOrWhiteSpace(vNombre))
					NExpedientes.Mensajes.AddError("El campo [Nombre o raz�n social] es obligatorio.");
				if (String.IsNullOrWhiteSpace(vNombre))
					NExpedientes.Mensajes.AddError("El campo [Correo] es obligatorio.");
				if (!NExpedientes.Mensajes.Ok)
					return ConExpedienteInsertaCap(conExpediente);

				conExpediente.ExpedienteId = NConExpedientes.ConExpedienteInserta(conExpediente);
				if (!NConExpedientes.Mensajes.Ok)
					return ConExpedienteInsertaCap(conExpediente);

				EClave vCve = CreaUsuario(conExpediente, out EUsuario vUsuario);
				if (NExpedientes.Mensajes.Ok)
				{
					foreach (var vValor in conExpediente.Valores)
					{
						if (vValor.ColumnaId == EVConExpedientes.ProveedorColumnaIdUsuario)
							EstableceValor(vValor, TiposColumna.Entero, vCve.UsuarioId.ToString());
					}
					NConExpedientes.ConExpedienteActualiza(conExpediente);

					EnviaCorreo(vUsuario.CorreoElectronico,
								"Su usuario de Rediin Proveedores ha sido creado.",
								String.Format("Bienvenido a Rediin Proveedores.<br/><br/>Su usuario es {0}<br/>Su contrase�a es {1}<br/><br/>La URL donde puede acceder a sus sistema es:<br/>{2}",
										vUsuario.Usuario, vCve.ClaveVerif, EVConExpedientes.ParamUrlRediinProveedores));
				}

				MMensajesTemp = NExpedientes.Mensajes.ToString();
				return RedirectToAction(nameof(ConExpedienteCon));
			}
			else
			{
				NConExpedientes.ConExpedienteInserta(conExpediente);
				if (NConExpedientes.Mensajes.Ok)
					return RedirectToAction(nameof(ConExpedienteCon));

				return ConExpedienteInsertaCap(conExpediente);
			}
		}
		[MValidaSeg(nameof(ConExpedienteActualiza))]
		public IActionResult ConExpedienteActualizaIni(Int32 indice)
		{
			EVConExpedientes.Accion = MAccionesGen.Actualiza;
			EVConExpedientes.ConExpedienteIndice = indice;
			EVConExpedientes.ConExpedienteSel = EVConExpedientes.ConExpedientePag.Pagina[indice];
			return ConExpedienteActualizaCap(EVConExpedientes.ConExpedienteSel);
		}
		[ValidateAntiForgeryToken]
		[MValidaSeg(nameof(ConExpedienteActualiza))]
		public IActionResult ConExpedienteActualizaCap(EConExpediente conExpediente)
		{
			return ConExpedienteCaptura(conExpediente);
		}
		[ValidateAntiForgeryToken]
		[MValidaSeg(nameof(ConExpedienteActualiza))]
		public IActionResult ConExpedienteActualizaCap2(IFormCollection conExp, Int64 PEMColumnaId)
		{
			EConExpediente conExpediente = ObtenExpediente(conExp);
			conExpediente.ProcesoOperativoId = EVConExpedientes.ConExpProcOperativoSel.ProcesoOperativoId; //Llave padre
			conExpediente.ProcesoOperativoEstId = EVConExpedientes.ConExpedienteSel.ProcesoOperativoEstId;
			AjustaComboCascadaPEMProv(conExpediente, PEMColumnaId);
			return ConExpedienteActualizaCap(conExpediente);
		}
		[ValidateAntiForgeryToken]
		public IActionResult ConExpedienteActualiza(IFormCollection conExp)
		{
			EConExpediente conExpediente = ObtenExpediente(conExp);
			conExpediente.ProcesoOperativoId = EVConExpedientes.ConExpProcOperativoSel.ProcesoOperativoId; //Llave padre
			conExpediente.ProcesoOperativoEstId = EVConExpedientes.ConExpedienteSel.ProcesoOperativoEstId;
			if (NConExpedientes.ConExpedienteActualiza(conExpediente))
				return RedirectToAction(nameof(ConExpedienteCon));

			return ConExpedienteActualizaCap(conExpediente);
		}
		public IActionResult ConExpedienteElimina(Int32 indice)
		{
			NConExpedientes.ConExpedienteElimina(EVConExpedientes.ConExpedientePag.Pagina[indice]);
			base.MMensajesTemp = NConExpedientes.Mensajes.ToString();
			return RedirectToAction(nameof(ConExpedienteCon));
		}
		/// <summary>
		/// Acci�n personalizada CambioEstatus.
		/// </summary>
		[MValidaSeg(nameof(ConExpedienteInicia))]
		public IActionResult ConExpedienteCambioEstatusIni(Int32 indice, Int64 procesoOperativoEstIdSig)
		{
			EVConExpedientes.ConExpedienteIndice = indice;
			EVConExpedientes.ConExpedienteSel = EVConExpedientes.ConExpedientePag.Pagina[indice];

			EConExpedienteCambioEstatus vConExpedienteCambioEstatus = new EConExpedienteCambioEstatus();
			vConExpedienteCambioEstatus.ProcesoOperativoEstId = procesoOperativoEstIdSig; //Adi
			vConExpedienteCambioEstatus.Comentarios = String.Empty;

			//Adi
			if (EVConExpedientes.ConExpProcOperativoSel.ProcesoOperativoId == EVConExpedientes.ParamProveedorProcesoOperativoId &&
			   procesoOperativoEstIdSig == EVConExpedientes.ParamEstIdCaptura)
				return ConExpedienteCambioEstatusCap(vConExpedienteCambioEstatus);
			else
				return ConExpedienteCambioEstatus(vConExpedienteCambioEstatus);
		}
		/// <summary>
		/// Acci�n personalizada CambioEstatus.
		/// </summary>
		[ValidateAntiForgeryToken]
		[MValidaSeg(nameof(ConExpedienteInicia))]
		public IActionResult ConExpedienteCambioEstatusCap(EConExpedienteCambioEstatus conExpedienteCambioEstatus)
		{
			ViewBag.CambioEstatus = true;
			ViewBag.CAESMensajes = base.MObtenMensajes(NConExpedientes.Mensajes);
			ViewBag.ConExpedienteCambioEstatus = conExpedienteCambioEstatus;

			return ConExpedienteCon();
		}

		/// <summary>
		/// Acci�n personalizada CambioEstatus.
		/// </summary>
		[MValidaSeg(nameof(ConExpedienteInicia))]
		public IActionResult ConExpedienteCambioEstatus(EConExpedienteCambioEstatus conExpedienteCambioEstatus)
		{
			conExpedienteCambioEstatus.ExpedienteId = EVConExpedientes.ConExpedienteSel.ExpedienteId;
			//Eli conExpedienteCambioEstatus.ProcesoOperativoEstId = EVConExpedientes.ConExpedienteSel.ProcesoOperativoEstId;
			if (NConExpedientes.ConExpedienteCambioEstatus(conExpedienteCambioEstatus))
			{
				//Adi
				if (EVConExpedientes.ConExpProcOperativoSel.ProcesoOperativoId == EVConExpedientes.ParamProveedorProcesoOperativoId)
				{
					String vCorreo = ObtenValor(EVConExpedientes.ConExpedienteSel, EVConExpedientes.ParamProveedorColumnaIdCorreo).ToString();
					String vProveedor = ObtenValor(EVConExpedientes.ConExpedienteSel, EVConExpedientes.ParamProveedorColumnaIdNombre).ToString();
					if (conExpedienteCambioEstatus.ProcesoOperativoEstId == EVConExpedientes.ParamEstIdCaptura)
					{
						EnviaCorreo(vCorreo,
									"Seguimiento en Portal de Rediin Proveedores",
									$"Estimado {vProveedor}:<br/><br/>Su alta como proveedor tiene las siguientes observaciones:<br/>{conExpedienteCambioEstatus.Comentarios}");
					}
					else if (conExpedienteCambioEstatus.ProcesoOperativoEstId == EVConExpedientes.ParamEstIdAutorizado)
					{
						EnviaCorreo(vCorreo,
									"Seguimiento en Portal de Rediin Proveedores",
									$"Estimado {vProveedor}:<br/><br/>Su alta como proveedor ha sido satisfactoria.");
					}
				}

				return RedirectToAction(nameof(ConExpedienteCon));
			}

			//Adi
			if (EVConExpedientes.ConExpProcOperativoSel.ProcesoOperativoId == EVConExpedientes.ParamProveedorProcesoOperativoId &&
			   conExpedienteCambioEstatus.ProcesoOperativoEstId == EVConExpedientes.ParamEstIdCaptura)
				return ConExpedienteCambioEstatusCap(conExpedienteCambioEstatus);
			else
			{
				base.MMensajesTemp = NConExpedientes.Mensajes.ToString();
				return RedirectToAction(nameof(ConExpedienteCon));
			}
		}
		#endregion

		#region Funciones
		private IActionResult ConExpedienteCaptura(EConExpediente conExpediente)
		{
			ViewBag.Mensajes = base.MObtenMensajes(NConExpedientes.Mensajes);
			ViewBag.Accion = EVConExpedientes.Accion;
			ViewBag.Reglas = EVConExpedientes.ConExpedienteReglas;
			//Adi
			ViewBag.ProcOperColumnas = EVConExpedientes.ProcOperColumnasCap;
			ViewBag.ParamProveedorProcesoOperativoId = EVConExpedientes.ParamProveedorProcesoOperativoId;

			conExpediente.ProcesoOperativoId = EVConExpedientes.ConExpProcOperativoSel.ProcesoOperativoId;
			if (EVConExpedientes.ConExpProcOperativoSel.ProcesoOperativoId == EVConExpedientes.ParamProveedorProcesoOperativoId)
			{
				ViewBag.EVConExpedientes = EVConExpedientes;
				EVConExpedientes.CombosProveedores[EVConExpedientes.ParamProveedorColumnaIdEstado] = null;
				EVConExpedientes.CombosProveedores[EVConExpedientes.ParamProveedorColumnaIdMunicipio] = null;
				EVConExpedientes.CombosProveedores[EVConExpedientes.ParamProveedorColumnaIdColonia] = null;

				Int64 vPaisId = XObject.ToInt64(ObtenValor(conExpediente, EVConExpedientes.ParamProveedorColumnaIdPais));
				if (vPaisId > 0)
				{
					EVConExpedientes.CombosProveedores[EVConExpedientes.ParamProveedorColumnaIdEstado] =
						NPaises.EstadoCmb(vPaisId);
					Int64 vEstadoId = XObject.ToInt64(ObtenValor(conExpediente, EVConExpedientes.ParamProveedorColumnaIdEstado));
					if (vEstadoId > 0)
					{
						EVConExpedientes.CombosProveedores[EVConExpedientes.ParamProveedorColumnaIdMunicipio] =
							NPaises.MunicipioCmb(vEstadoId);
						Int64 vMunicipio = XObject.ToInt64(ObtenValor(conExpediente, EVConExpedientes.ParamProveedorColumnaIdMunicipio));
						if (vMunicipio > 0)
						{
							EVConExpedientes.CombosProveedores[EVConExpedientes.ParamProveedorColumnaIdColonia] =
								NPaises.ColoniaCmb(vMunicipio);
						}
					}
				}
			}

			return ViewCap(nameof(ConExpedienteCaptura), conExpediente);
		}
		private void AjustaComboCascadaPEMProv(EConExpediente conExpediente, Int64 columnaId)
		{
			if (columnaId == EVConExpedientes.ParamProveedorColumnaIdPais)
			{
				foreach (var vVal in conExpediente.Valores)
				{
					if (vVal.ColumnaId == EVConExpedientes.ParamProveedorColumnaIdEstado ||
					   vVal.ColumnaId == EVConExpedientes.ParamProveedorColumnaIdMunicipio ||
					   vVal.ColumnaId == EVConExpedientes.ParamProveedorColumnaIdColonia)
						EstableceValor(vVal, TiposColumna.Entero, "0");
				}
			}
			else if (columnaId == EVConExpedientes.ParamProveedorColumnaIdEstado)
			{
				foreach (var vVal in conExpediente.Valores)
				{
					if (vVal.ColumnaId == EVConExpedientes.ParamProveedorColumnaIdMunicipio ||
						vVal.ColumnaId == EVConExpedientes.ParamProveedorColumnaIdColonia)
						EstableceValor(vVal, TiposColumna.Entero, "0");
				}
			}
			else if (columnaId == EVConExpedientes.ParamProveedorColumnaIdMunicipio)
			{
				foreach (var vVal in conExpediente.Valores)
				{
					if (vVal.ColumnaId == EVConExpedientes.ParamProveedorColumnaIdColonia)
						EstableceValor(vVal, TiposColumna.Entero, "0");
				}
			}
		}
		private void EstableceValor(EConExpValores valor, TiposColumna tipo, String cadena)
		{
			if (tipo == TiposColumna.Entero || tipo == TiposColumna.Importe)
				valor.ValorNumerico = XObject.ToDecimal(cadena);
			else if (tipo == TiposColumna.Fecha || tipo == TiposColumna.FechaYHora || tipo == TiposColumna.Hora)
				valor.ValorFecha = XObject.ToDateTime(cadena);
			else if (tipo == TiposColumna.Boleano)
				valor.ValorTexto = (cadena == "true" ? "1" : String.Empty);
			else
				valor.ValorTexto = cadena;
		}
		public EConExpediente ObtenExpediente(IFormCollection conExp)
		{
			EConExpediente conExpediente = new EConExpediente();

			EConExpValores vVal = null;
			if (conExp.ContainsKey(nameof(EConExpediente.ExpedienteId)))
				conExpediente.ExpedienteId = XObject.ToInt64(conExp[nameof(EConExpediente.ExpedienteId)].ToString());
			foreach (EProcesoOperativoCol vCol in EVConExpedientes.ProcOperColumnasCon)
			{
				vVal = new EConExpValores();
				vVal.ExpedienteId = conExpediente.ExpedienteId;
				vVal.ColumnaId = vCol.ColumnaId;
				if (conExp.ContainsKey(vCol.ColumnaId.ToString()))
					EstableceValor(vVal, vCol.Tipo, conExp[vCol.ColumnaId.ToString()]);
				conExpediente.Valores.Add(vVal);
			}

			return conExpediente;
		}
		//No config Proveedor
		private EClave CreaUsuario(EConExpediente conExpediente, out EUsuario usuario)
		{
			String vProveedor = ObtenValor(conExpediente, EVConExpedientes.ParamProveedorColumnaIdNombre).ToString();
			String[] vNombres = vProveedor.Split(" ");

			usuario = new EUsuario();
			usuario.CorreoElectronico = ObtenValor(conExpediente, EVConExpedientes.ParamProveedorColumnaIdCorreo).ToString();
			usuario.EstablecimientoId = base.EVDatosPortal.UsuarioSesion.EstablecimientoId;
			usuario.PerfilId = EVConExpedientes.ParamPerfilIdNvoUsr;

			if (vNombres.Length >= 3)
			{
				usuario.ApellidoMaterno = vNombres[vNombres.Length - 1];
				usuario.ApellidoPaterno = vNombres[vNombres.Length - 2];
				usuario.Nombre = String.Empty;
				for (int i = 0; i < vNombres.Length - 2; i++)
					usuario.Nombre += (i > 0 ? " " : String.Empty) + vNombres[i];

				usuario.Usuario = $"{usuario.Nombre[0]}{usuario.ApellidoPaterno}".ToLower();
			}
			else if (vNombres.Length >= 2)
			{
				usuario.ApellidoMaterno = "S/N.";
				usuario.ApellidoPaterno = vNombres[vNombres.Length - 2];
				usuario.Nombre = String.Empty;
				for (int i = 0; i < vNombres.Length - 2; i++)
					usuario.Nombre += (i > 0 ? " " : String.Empty) + vNombres[i];

				usuario.Usuario = $"{usuario.Nombre[0]}{usuario.ApellidoPaterno}".ToLower();
			}
			else
			{
				usuario.ApellidoPaterno = "S/N.";
				usuario.ApellidoMaterno = "S/N.";
				usuario.Usuario = $"{usuario.Nombre.Trim().Replace(" ", "")}".ToLower();
			}

			usuario.Usuario += (DateTime.Now.Year - 2000).ToString();
			usuario.Usuario += DateTime.Now.DayOfYear.ToString();

			try
			{
				return NUsuarios.UsuarioInsertaAuto(usuario);
			}
			catch (Exception e)
			{
				NUsuarios.Mensajes.AddError(e.Message);
				return null;
			}
		}
		private void EnviaCorreo(String correoDestino, String subject, String body)
		{
			var vCorreo = base.ServidorCorreo("RediinProveedoresMail");
			vCorreo.To.Add(vCorreo.NewUser("Cliente", correoDestino));
			vCorreo.Send(subject, body);
		}
		private Object ObtenValor(EConExpediente conExpediente, Int64 columnaId)
		{
			return UtilExpediente.ObtenValor(EVConExpedientes.ProcOperColumnasCon,
											 conExpediente,
											 columnaId);
		}
		//No config Proveedor
		#endregion

		#region Acciones de Paginacion Orden y Filtro
		[MValidaSeg(nameof(ConExpedienteInicia))]
		public IActionResult ConExpedientePaginacion(MEDatosPaginador datPag)
		{
			EVConExpedientes.ConExpedientePag.DatPag = datPag;
			return RedirectToAction(nameof(ConExpedienteCon));
		}
		[MValidaSeg(nameof(ConExpedienteInicia))]
		public IActionResult ConExpedientePaginacionSigPag()
		{
			EVConExpedientes.ConExpedientePag.DatPag.CurrentPage += 1;
			return RedirectToAction(nameof(ConExpedienteCon));
		}
		[MValidaSeg(nameof(ConExpedienteInicia))]
		public IActionResult ConExpedientePaginacionAntPag()
		{
			if (EVConExpedientes.ConExpedientePag.DatPag.CurrentPage > 1)
				EVConExpedientes.ConExpedientePag.DatPag.CurrentPage -= 1;

			return RedirectToAction(nameof(ConExpedienteCon));
		}

		[MValidaSeg(nameof(ConExpedienteInicia))]
		public IActionResult ConExpedienteOrdena(String orden)
		{
			EVConExpedientes.ConExpedienteColOrden = orden;
			return RedirectToAction(nameof(ConExpedienteCon));
		}
		[MValidaSeg(nameof(ConExpedienteInicia))]
		public IActionResult ConExpedienteFiltra(EConExpedienteFiltro filtro)
		{
			EVConExpedientes.ConExpedienteFiltro = filtro;
			return RedirectToAction(nameof(ConExpedienteCon));
		}
		[MValidaSeg(nameof(ConExpedienteInicia))]
		public IActionResult ConExpedienteLimpiaFiltros()
		{
			EVConExpedientes.ConExpedienteFiltro = new EConExpedienteFiltro();
			return RedirectToAction(nameof(ConExpedienteCon));
		}
		[MValidaSeg(nameof(ConExpedienteInicia))]
		public IActionResult ConExpedienteSelCol(String selColGrupoId)
		{
			EVConExpedientes.ConExpedienteSelColGrupoId = selColGrupoId;
			return RedirectToAction(nameof(ConExpedienteCon));
		}
		#endregion

		#endregion

		#region ConExpedienteObjeto (Objs)

		#region Acciones
		public IActionResult ConExpedienteObjetoInicia(Int32 indice)
		{
			//Configuracion de inicio
			if (String.IsNullOrWhiteSpace(EVConExpedientes.ConExpedienteObjetoColOrden))
				EVConExpedientes.ConExpedienteObjetoColOrden = nameof(EConExpedienteObjeto.ExpedienteObjetoId);

			if (EVConExpedientes.ConExpedienteObjetoReglas == null)
			{
				EVConExpedientes.ConExpedienteObjetoReglas = NConExpedientes.ConExpedienteObjetoReglas();
				base.MMensajesTemp = NConExpedientes.Mensajes.ToString();
			}

			if (indice >= 0)
			{
				EVConExpedientes.ConExpedienteIndice = indice;
				EVConExpedientes.ConExpedienteSel = EVConExpedientes.ConExpedientePag.Pagina[indice];
			}

			return RedirectToAction(nameof(ConExpedienteObjetoCon));
		}
		[MValidaSeg(nameof(ConExpedienteObjetoInicia))]
		public IActionResult ConExpedienteObjetoCon()
		{
			EVConExpedientes.ConExpedienteObjetoFiltro.ExpedienteId = EVConExpedientes.ConExpedienteSel.ExpedienteId;
			base.MCargaFiltroPagYOrd(EVConExpedientes.ConExpedienteObjetoFiltro,
									 EVConExpedientes.ConExpedienteObjetoPag,
									 EVConExpedientes.ConExpedienteObjetoColOrden,
									 nameof(EConExpedienteObjeto));

			EVConExpedientes.ConExpedienteObjetoPag = NConExpedientes.ConExpedienteObjetoPag(EVConExpedientes.ConExpedienteObjetoFiltro);
			base.MActualizaTamPag(EVConExpedientes.ConExpedienteObjetoPag?.DatPag);

			ViewBag.Mensajes = base.MObtenMensajes(NConExpedientes.Mensajes);
			ViewBag.DatPag = EVConExpedientes.ConExpedienteObjetoPag?.DatPag;
			ViewBag.Orden = EVConExpedientes.ConExpedienteObjetoColOrden;
			ViewBag.Filtro = EVConExpedientes.ConExpedienteObjetoFiltro;
			ViewBag.Indice = EVConExpedientes.ConExpedienteObjetoIndice;

			return View(nameof(ConExpedienteObjetoCon), EVConExpedientes.ConExpedienteObjetoPag?.Pagina);
		}
		public IActionResult ConExpedienteObjetoXId(Int32 indice)
		{
			EVConExpedientes.Accion = MAccionesGen.Consulta;
			EVConExpedientes.ConExpedienteObjetoIndice = indice;
			return ConExpedienteObjetoCaptura(EVConExpedientes.ConExpedienteObjetoPag.Pagina[indice]);
		}
		[MValidaSeg(nameof(ConExpedienteObjetoInserta))]
		public IActionResult ConExpedienteObjetoInsertaIni()
		{
			EVConExpedientes.Accion = MAccionesGen.Inserta;
			return ConExpedienteObjetoInsertaCap(new EConExpedienteObjeto()
			{
				Activo = true
			});
		}
		[ValidateAntiForgeryToken]
		[MValidaSeg(nameof(ConExpedienteObjetoInserta))]
		public IActionResult ConExpedienteObjetoInsertaCap(EConExpedienteObjeto conExpedienteObjeto)
		{
			return ConExpedienteObjetoCaptura(conExpedienteObjeto);
		}
		[ValidateAntiForgeryToken]
		public IActionResult ConExpedienteObjetoInserta(EConExpedienteObjeto conExpedienteObjeto,
														IFormFile archivoFisico)
		{
			//Adi
			if (archivoFisico == null)
			{
				NConExpedientes.Mensajes.AddError("No ha seleccionado un archivo.");
				return ConExpedienteObjetoInsertaCap(conExpedienteObjeto);
			}

			//Subimos el archivo
			String vEntidad = "Expendientes";
			String vExtension = Path.GetExtension(archivoFisico.FileName);
			if (String.IsNullOrWhiteSpace(conExpedienteObjeto.ArchivoNombre))
				conExpedienteObjeto.ArchivoNombre = Path.GetFileName(archivoFisico.FileName);
			if (!conExpedienteObjeto.ArchivoNombre.EndsWith(vExtension))
				conExpedienteObjeto.ArchivoNombre += vExtension;

			String vRutaBase = MValorConfig<String>("DirBD");
			conExpedienteObjeto.Ruta =
				Path.Combine(vRutaBase,
							 vEntidad,
							 EVConExpedientes.ConExpedienteSel.ExpedienteId.ToString());

			String vRutaYNombre = Path.Combine(conExpedienteObjeto.Ruta, conExpedienteObjeto.ArchivoNombre);

			if (!System.IO.Directory.Exists(Path.Combine(vRutaBase, vEntidad)))
				System.IO.Directory.CreateDirectory(Path.Combine(vRutaBase, vEntidad));

			if (!System.IO.Directory.Exists(conExpedienteObjeto.Ruta))
				System.IO.Directory.CreateDirectory(conExpedienteObjeto.Ruta);

			if (System.IO.File.Exists(vRutaYNombre))
			{
				NConExpedientes.Mensajes.AddError("EL nombre del arhivo ya existe, no se puede insertar.");
				return ConExpedienteObjetoInsertaCap(conExpedienteObjeto);
			}

			base.MRecibeArchivoDeCliente(NConExpedientes.Mensajes,
										 archivoFisico,
										 vRutaYNombre);

			if (!NConExpedientes.Mensajes.Ok)
				return ConExpedienteObjetoInsertaCap(conExpedienteObjeto);
			//Fin Adi

			conExpedienteObjeto.ExpedienteId = EVConExpedientes.ConExpedienteSel.ExpedienteId; //Llave padre
			NConExpedientes.ConExpedienteObjetoInserta(conExpedienteObjeto);
			if (NConExpedientes.Mensajes.Ok)
			{
				return RedirectToAction(nameof(ConExpedienteObjetoCon));
			}

			return ConExpedienteObjetoInsertaCap(conExpedienteObjeto);
		}
		public IActionResult ConExpedienteObjetoElimina(Int32 indice)
		{
			NConExpedientes.ConExpedienteObjetoElimina(EVConExpedientes.ConExpedienteObjetoPag.Pagina[indice]);
			base.MMensajesTemp = NConExpedientes.Mensajes.ToString();
			return RedirectToAction(nameof(ConExpedienteObjetoCon));
		}
		/// <summary>
		/// Acci�n personalizada Descarga.
		/// </summary>
		public IActionResult ConExpedienteObjetoDescarga(Int32 indice)
		{

			EConExpedienteObjeto vObj = EVConExpedientes.ConExpedienteObjetoPag.Pagina[indice];
			//return base.MEnviaArchivoACliente(NConExpedientes.Mensajes, Path.Combine(vObj.Ruta, vObj.ArchivoNombre));

			//         Response.Clear();
			//if (vObj.ArchivoNombre.EndsWith("xlsb"))
			//	Response.ContentType = "application/vnd.ms-excel";
			//         else
			//	Response.ContentType = "application/pdf";
			//Response.Headers.Add("Content-Disposition", "attachment; filename=documento" + Path.GetExtension(vObj.ArchivoNombre));
			//Response.Headers.Add("Content-Disposition", "inline; filename=documento" + Path.GetExtension(vObj.ArchivoNombre));

			String vCont;
			if (vObj.ArchivoNombre.EndsWith("xlsb"))
				vCont = "application/vnd.ms-excel";
			else
				vCont = "application/pdf";

			using MemoryStream vMS = new MemoryStream();
			using FileStream vFS = new FileStream(Path.Combine(vObj.Ruta, vObj.ArchivoNombre), FileMode.Open);
			vFS.CopyTo(vMS);
			//return vMS.ToArray();
			return File(vMS.ToArray(), vCont, "Archivo" + Path.GetExtension(vObj.ArchivoNombre));

			//NConExpedientes.ConExpedienteObjetoDescarga();
			//base.MMensajesTemp = NConExpedientes.Mensajes.ToString();
			//return RedirectToAction(nameof(ConExpedienteObjetoCon));
		}
		[MValidaSeg(nameof(ConExpedienteObjetoDescarga))]
		public IActionResult ConExpedienteObjetoDescarga2(Int32 indice)
		{
			Int32 totalPaginas = 0;
			EConExpedienteObjeto vObj = EVConExpedientes.ConExpedienteObjetoPag.Pagina[indice];
			String imageFilesPath = Path.Combine(Path.Combine(vObj.Ruta, "temp"), "page-{0}.png");
			using GroupDocs.Viewer.Viewer v = new GroupDocs.Viewer.Viewer(Path.Combine(vObj.Ruta, vObj.ArchivoNombre));
			GroupDocs.Viewer.Results.ViewInfo i = v.GetViewInfo(GroupDocs.Viewer.Options.ViewInfoOptions.ForPngView(false));
			totalPaginas = i.Pages.Count;
			GroupDocs.Viewer.Options.PngViewOptions o = new PngViewOptions(imageFilesPath);
			v.View(o);
			return new JsonResult(totalPaginas);
		}
		[MValidaSeg(nameof(ConExpedienteObjetoDescarga))]
		public IActionResult ConExpedienteObjetoDescargaImg(Int32 indice, Int32 pagina)
		{
			EConExpedienteObjeto vObj = EVConExpedientes.ConExpedienteObjetoPag.Pagina[indice];
			String vRutaImgTemp = Path.Combine(vObj.Ruta, "temp", $"page-{pagina}.png");
			using MemoryStream vMS = new MemoryStream();
			using FileStream vFS = new FileStream(vRutaImgTemp, FileMode.Open);
			vFS.CopyTo(vMS);
			return File(vMS.ToArray(), "image/png");
		}

		/// <summary>
		/// Acci�n personalizada SelArchivo.
		/// </summary>
		[MValidaSeg(nameof(ConExpedienteObjetoInicia))]
		public IActionResult ConExpedienteObjetoSelArchivoIni(Int32 indice)
		{
			EVConExpedientes.ConExpedienteObjetoIndice = indice;
			EVConExpedientes.ConExpedienteObjetoSel = EVConExpedientes.ConExpedienteObjetoPag.Pagina[indice];
			EConExpedienteObjetoSelArchivo vConExpedienteObjetoSelArchivo = new EConExpedienteObjetoSelArchivo();
			vConExpedienteObjetoSelArchivo.ExpedienteObjetoId = EVConExpedientes.ConExpedienteObjetoSel.ExpedienteObjetoId;
			vConExpedienteObjetoSelArchivo.ArchivoNombre = String.Empty;
			return ConExpedienteObjetoSelArchivoCap(vConExpedienteObjetoSelArchivo);
		}
		/// <summary>
		/// Acci�n personalizada SelArchivo.
		/// </summary>
		[ValidateAntiForgeryToken]
		[MValidaSeg(nameof(ConExpedienteObjetoInicia))]
		public IActionResult ConExpedienteObjetoSelArchivoCap(EConExpedienteObjetoSelArchivo conExpedienteObjetoSelArchivo)
		{
			ViewBag.SelArchivo = true;
			ViewBag.SEARMensajes = base.MObtenMensajes(NConExpedientes.Mensajes);
			ViewBag.ConExpedienteObjetoSelArchivo = conExpedienteObjetoSelArchivo;

			return ConExpedienteObjetoCon();
		}
		/// <summary>
		/// Acci�n personalizada SelArchivo.
		/// </summary>
		[ValidateAntiForgeryToken]
		[MValidaSeg(nameof(ConExpedienteObjetoInicia))]
		public IActionResult ConExpedienteObjetoSelArchivo(EConExpedienteObjetoSelArchivo conExpedienteObjetoSelArchivo,
														   IFormFile archivoFisico)
		{

			//Adi
			if (archivoFisico == null)
			{
				NConExpedientes.Mensajes.AddError("No ha seleccionado un archivo.");
				return ConExpedienteObjetoSelArchivoCap(conExpedienteObjetoSelArchivo);
			}

			//Subimos el archivo
			String vEntidad = "Expendientes";
			String vExtension = Path.GetExtension(archivoFisico.FileName);
			if (String.IsNullOrWhiteSpace(conExpedienteObjetoSelArchivo.ArchivoNombre))
				conExpedienteObjetoSelArchivo.ArchivoNombre = Path.GetFileName(archivoFisico.FileName);
			if (!conExpedienteObjetoSelArchivo.ArchivoNombre.EndsWith(vExtension))
				conExpedienteObjetoSelArchivo.ArchivoNombre += vExtension;

			String vRutaBase = MValorConfig<String>("DirBD");
			conExpedienteObjetoSelArchivo.Ruta =
				Path.Combine(vRutaBase,
							 vEntidad,
							 EVConExpedientes.ConExpedienteSel.ExpedienteId.ToString());

			String vRutaYNombre = Path.Combine(conExpedienteObjetoSelArchivo.Ruta, conExpedienteObjetoSelArchivo.ArchivoNombre);

			if (!System.IO.Directory.Exists(Path.Combine(vRutaBase, vEntidad)))
				System.IO.Directory.CreateDirectory(Path.Combine(vRutaBase, vEntidad));

			if (!System.IO.Directory.Exists(conExpedienteObjetoSelArchivo.Ruta))
				System.IO.Directory.CreateDirectory(conExpedienteObjetoSelArchivo.Ruta);

			if (System.IO.File.Exists(vRutaYNombre))
			{
				NConExpedientes.Mensajes.AddError("EL nombre del arhivo ya existe, no se puede insertar.");
				return ConExpedienteObjetoSelArchivoCap(conExpedienteObjetoSelArchivo);
			}

			base.MRecibeArchivoDeCliente(NConExpedientes.Mensajes,
										 archivoFisico,
										 vRutaYNombre);


			if (!NConExpedientes.Mensajes.Ok)
				return ConExpedienteObjetoSelArchivoCap(conExpedienteObjetoSelArchivo);
			//Fin Adi

			conExpedienteObjetoSelArchivo.ExpedienteId = EVConExpedientes.ConExpedienteObjetoSel.ExpedienteId;
			//conExpedienteObjetoSelArchivo.Ruta = EVConExpedientes.ConExpedienteObjetoSel.Ruta;
			if (NConExpedientes.ConExpedienteObjetoSelArchivo(conExpedienteObjetoSelArchivo))
			{
				return RedirectToAction(nameof(ConExpedienteObjetoCon));
			}

			return ConExpedienteObjetoSelArchivoCap(conExpedienteObjetoSelArchivo);
		}
		#endregion

		#region Funciones
		private IActionResult ConExpedienteObjetoCaptura(EConExpedienteObjeto conExpedienteObjeto)
		{
			ViewBag.Mensajes = base.MObtenMensajes(NConExpedientes.Mensajes);
			ViewBag.Accion = EVConExpedientes.Accion;
			ViewBag.Reglas = EVConExpedientes.ConExpedienteObjetoReglas;

			ViewBag.ProcesosOperativosObjetos = NProcesosOperativos.ProcesoOperativoObjetoCmb(EVConExpedientes.ConExpedienteSel.ProcesoOperativoId);

			return ViewCap(nameof(ConExpedienteObjetoCaptura), conExpedienteObjeto);
		}
		#endregion

		#region Acciones de Paginacion Orden y Filtro
		[MValidaSeg(nameof(ConExpedienteObjetoInicia))]
		public IActionResult ConExpedienteObjetoPaginacion(MEDatosPaginador datPag)
		{
			EVConExpedientes.ConExpedienteObjetoPag.DatPag = datPag;
			return RedirectToAction(nameof(ConExpedienteObjetoCon));
		}
		[MValidaSeg(nameof(ConExpedienteObjetoInicia))]
		public IActionResult ConExpedienteObjetoOrdena(String orden)
		{
			EVConExpedientes.ConExpedienteObjetoColOrden = orden;
			return RedirectToAction(nameof(ConExpedienteObjetoCon));
		}
		[MValidaSeg(nameof(ConExpedienteObjetoInicia))]
		public IActionResult ConExpedienteObjetoFiltra(EConExpedienteObjetoFiltro filtro)
		{
			EVConExpedientes.ConExpedienteObjetoFiltro = filtro;
			return RedirectToAction(nameof(ConExpedienteObjetoCon));
		}
		[MValidaSeg(nameof(ConExpedienteObjetoInicia))]
		public IActionResult ConExpedienteObjetoLimpiaFiltros()
		{
			EVConExpedientes.ConExpedienteObjetoFiltro = new EConExpedienteObjetoFiltro();
			return RedirectToAction(nameof(ConExpedienteObjetoCon));
		}
		#endregion

		#endregion

		#region ExpedienteEstatu (ExpeEsta)

		#region Acciones
		public IActionResult ExpedienteEstatuInicia(Int32 indice)
		{
			//Configuracion de inicio
			if (String.IsNullOrWhiteSpace(EVConExpedientes.ExpedienteEstatuColOrden))
				EVConExpedientes.ExpedienteEstatuColOrden = "-" + nameof(EExpedienteEstatu.FechaCreacion);

			if (indice >= 0)
			{
				EVConExpedientes.ConExpedienteIndice = indice;
				EVConExpedientes.ConExpedienteSel = EVConExpedientes.ConExpedientePag.Pagina[indice];
			}

			return RedirectToAction(nameof(ExpedienteEstatuCon));
		}
		[MValidaSeg(nameof(ExpedienteEstatuInicia))]
		public IActionResult ExpedienteEstatuCon()
		{
			EVConExpedientes.ExpedienteEstatuFiltro.ExpedienteId = EVConExpedientes.ConExpedienteSel.ExpedienteId;
			base.MCargaFiltroPagYOrd(EVConExpedientes.ExpedienteEstatuFiltro,
									 EVConExpedientes.ExpedienteEstatuPag,
									 EVConExpedientes.ExpedienteEstatuColOrden,
									 nameof(EExpedienteEstatu));

			EVConExpedientes.ExpedienteEstatuPag = NConExpedientes.ExpedienteEstatuPag(EVConExpedientes.ExpedienteEstatuFiltro);
			base.MActualizaTamPag(EVConExpedientes.ExpedienteEstatuPag?.DatPag);

			ViewBag.Mensajes = base.MObtenMensajes(NConExpedientes.Mensajes);
			ViewBag.DatPag = EVConExpedientes.ExpedienteEstatuPag?.DatPag;
			ViewBag.Orden = EVConExpedientes.ExpedienteEstatuColOrden;
			ViewBag.Indice = EVConExpedientes.ExpedienteEstatuIndice;

			return View(nameof(ExpedienteEstatuCon), EVConExpedientes.ExpedienteEstatuPag?.Pagina);
		}
		/// <summary>
		/// Consulta por id.
		/// </summary>
		[MValidaSeg(nameof(ExpedienteEstatuInicia))]
		public IActionResult ExpedienteEstatuXId(Int32 indice)
		{
			EVConExpedientes.Accion = MAccionesGen.Consulta;
			EVConExpedientes.ExpedienteEstatuIndice = indice;
			return ExpedienteEstatuCaptura(EVConExpedientes.ExpedienteEstatuPag.Pagina[indice]);
		}
		#endregion

		#region Funciones
		/// <summary>
		/// Captura.
		/// </summary>
		private IActionResult ExpedienteEstatuCaptura(EExpedienteEstatu expedienteEstatu)
		{
			ViewBag.Mensajes = base.MObtenMensajes(NConExpedientes.Mensajes);
			ViewBag.Accion = EVConExpedientes.Accion;

			return ViewCap(nameof(ExpedienteEstatuCaptura), expedienteEstatu);
		}
		#endregion

		#region Acciones de Paginacion Orden y Filtro
		[MValidaSeg(nameof(ExpedienteEstatuInicia))]
		public IActionResult ExpedienteEstatuPaginacion(MEDatosPaginador datPag)
		{
			EVConExpedientes.ExpedienteEstatuPag.DatPag = datPag;
			return RedirectToAction(nameof(ExpedienteEstatuCon));
		}
		[MValidaSeg(nameof(ExpedienteEstatuInicia))]
		public IActionResult ExpedienteEstatuOrdena(String orden)
		{
			EVConExpedientes.ExpedienteEstatuColOrden = orden;
			return RedirectToAction(nameof(ExpedienteEstatuCon));
		}
		#endregion

		#endregion
	}
}
