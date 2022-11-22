using DSEntityNetX.Common.Casting;
using DSEntityNetX.Common.File;
using DSEntityNetX.Common.Pagination;
using DSMetodNetX.Aplicacion;
using DSMetodNetX.Entidades;
using DSMetodNetX.Mvc.Seguridad;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Rediin2022.Aplicacion.PriCatalogos;
using Rediin2022.Aplicacion.PriOperacion;
using Rediin2022.Entidades.Idioma;
using Rediin2022.Entidades.PriCatalogos;
using Rediin2022.Entidades.PriClientes;
using Rediin2022.Entidades.PriOperacion;
using Sisegui2020.Entidades.Idioma;
using Sisegui2020.Entidades.PriSeguridad;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
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
										INExpedientes nExpedientes)
		{
			NConExpedientes = nConExpedientes;
			NProcesosOperativos = nProcesosOperativos;
			NUsuarios = nUsuarios;
			NExpedientes = nExpedientes;
		}
		#endregion

		#region Propiedades
		private INConExpedientes NConExpedientes { get; set; }
		private INProcesosOperativos NProcesosOperativos { get; set; }
		private INUsuarios NUsuarios { get; set; }
		private INExpedientes NExpedientes { get; set; }
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
			//Permisos
			EVConExpedientes.PermisoMostrarCatalogos = base.MPermisoIdXOpcion("MostrarCatalogos") > 0;

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

			EVConExpedientes.ConExpProcOperativoFiltro.PermisoMostrarCatalogos =
				EVConExpedientes.PermisoMostrarCatalogos;

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
				EVConExpedientes.ConExpedienteColOrden = nameof(EConExpediente.ExpedienteId);

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
				//EVConExpedientes.ParamProveedorColumnaIdNombre = base.MParametroSist<Int64>("RediinProveedorColumnaIdNombre");
				//EVConExpedientes.ParamProveedorColumnaIdCorreo = base.MParametroSist<Int64>("RediinProveedorColumnaIdCorreo");

				EVConExpedientes.ParamProveedorColumnaIdNombre = UtilExpediente.ObtenRelacion(vRelaciones, "Nombre").ColumnaId;
				EVConExpedientes.ParamProveedorColumnaIdCorreo = UtilExpediente.ObtenRelacion(vRelaciones, "Correo").ColumnaId;
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
		public IActionResult ConExpedienteInserta(IFormCollection conExp)
		{
			EConExpediente conExpediente = ObtenExpediente(conExp);
			conExpediente.ProcesoOperativoId = EVConExpedientes.ConExpProcOperativoSel.ProcesoOperativoId; //Llave padre
			conExpediente.ControlEstatus = EVConExpedientes.ConExpProcOperativoSel.ControlEstatus;
			//conExpediente.ProcesoOperativoEstId = 0L;
			NConExpedientes.ConExpedienteInserta(conExpediente);
			if (NConExpedientes.Mensajes.Ok)
			{
				//No config Proveedor
				if (EVConExpedientes.ConExpProcOperativoSel.ProcesoOperativoId == EVConExpedientes.ParamProveedorProcesoOperativoId)
					EnviaCorreo(conExpediente, CreaUsuario(conExpediente, out EUsuario usuario), usuario);

				//No config Proveedor

				return RedirectToAction(nameof(ConExpedienteCon));
			}

			return ConExpedienteInsertaCap(conExpediente);
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
		/// Acción personalizada CambioEstatus.
		/// </summary>
		[MValidaSeg(nameof(ConExpedienteInicia))]
		public IActionResult ConExpedienteCambioEstatus(Int32 indice, Int64 procesoOperativoEstIdSig)
		{
			EConExpedienteCambioEstatus vConExpedienteCambioEstatus = new EConExpedienteCambioEstatus();
			vConExpedienteCambioEstatus.ExpedienteId = EVConExpedientes.ConExpedientePag.Pagina[indice].ExpedienteId;
			vConExpedienteCambioEstatus.ProcesoOperativoEstId = procesoOperativoEstIdSig; //Mod EVConExpedientes.ConExpedientePag.Pagina[indice].ProcesoOperativoEstId;
			NConExpedientes.ConExpedienteCambioEstatus(vConExpedienteCambioEstatus);
			base.MMensajesTemp = NConExpedientes.Mensajes.ToString();
			return RedirectToAction(nameof(ConExpedienteCon));
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

			return ViewCap(nameof(ConExpedienteCaptura), conExpediente);
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
			usuario = new EUsuario();
			usuario.Nombre = ObtenValor(conExpediente, EVConExpedientes.ParamProveedorColumnaIdNombre).ToString();
			usuario.CorreoElectronico = ObtenValor(conExpediente, EVConExpedientes.ParamProveedorColumnaIdCorreo).ToString();

			String[] vNombres = usuario.Nombre.Split(" ");
			if (vNombres.Length >= 3)
			{
				usuario.ApellidoMaterno = vNombres[vNombres.Length - 1];
				usuario.ApellidoPaterno = vNombres[vNombres.Length - 2];
				usuario.Nombre = String.Empty;
				for (int i = 0; i < vNombres.Length - 2; i++)
					usuario.Nombre += (i > 0 ? " " : String.Empty) + vNombres[i];
			}

			return NUsuarios.UsuarioInsertaAuto(usuario);
		}
		private void EnviaCorreo(EConExpediente conExpediente, EClave clave, EUsuario usuario)
		{

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
		/// Acción personalizada Descarga.
		/// </summary>
		public Task<IActionResult> ConExpedienteObjetoDescarga(Int32 indice)
		{
			EConExpedienteObjeto vObj = EVConExpedientes.ConExpedienteObjetoPag.Pagina[indice];
			return base.MEnviaArchivoACliente(NConExpedientes.Mensajes, Path.Combine(vObj.Ruta, vObj.ArchivoNombre));
			//NConExpedientes.ConExpedienteObjetoDescarga();
			//base.MMensajesTemp = NConExpedientes.Mensajes.ToString();
			//return RedirectToAction(nameof(ConExpedienteObjetoCon));
		}
		/// <summary>
		/// Acción personalizada SelArchivo.
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
		/// Acción personalizada SelArchivo.
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
		/// Acción personalizada SelArchivo.
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
		#endregion

		#region Funciones
		private IActionResult ExpedienteEstatuCaptura(EExpedienteEstatu expedienteEstatu)
		{
			ViewBag.Mensajes = base.MObtenMensajes(NConExpedientes.Mensajes);
			ViewBag.Accion = EVConExpedientes.Accion;

			ViewBag.ProcesosOperativosEst = NProcesosOperativos.ProcesoOperativoEstCmb(EVConExpedientes.ConExpedienteSel.ProcesoOperativoId);

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
