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
using Rediin2022.Entidades.Idioma;
using Rediin2022.Entidades.PriCatalogos;
using Sisegui2020.Entidades.PriSeguridad;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Rediin2022Web.Areas.PriCatalogos.Controllers
{
    [Area("PriCatalogos")]
    public class AutorizacionesController : MControllerMvcPri
    {
        #region Constructores
        public AutorizacionesController(INAutorizaciones nAutorizaciones,
                                        INProcesosOperativos nProcesosOperativos,
                                        INUsuarios nUsuarios)
        {
            NAutorizaciones = nAutorizaciones;
            NProcesosOperativos = nProcesosOperativos;
            NUsuarios = nUsuarios;
        }
        #endregion

        #region Propiedades
        private INAutorizaciones NAutorizaciones { get; set; }
        private INProcesosOperativos NProcesosOperativos { get; set; }
        private INUsuarios NUsuarios { get; set; }
        private EVAutorizaciones EVAutorizaciones
        {
            get
            {
                if (base.MSesion<EVAutorizaciones>() == null)
                    base.MSesion(new EVAutorizaciones());

                return base.MSesionAuto<EVAutorizaciones>();
            }
        }
        #endregion

        #region Autorizacion (Autorizaciones)

        #region Acciones
        public IActionResult AutorizacionInicia()
        {
            //Configuracion de inicio
            if (String.IsNullOrWhiteSpace(EVAutorizaciones.AutorizacionColOrden))
                EVAutorizaciones.AutorizacionColOrden = nameof(EAutorizacion.AutorizacionId);

            if (EVAutorizaciones.AutorizacionReglas == null)
            {
                EVAutorizaciones.AutorizacionReglas = NAutorizaciones.AutorizacionReglas();
                base.MMensajesTemp = NAutorizaciones.Mensajes.ToString();
            }

            return RedirectToAction(nameof(AutorizacionCon));
        }
        [MValidaSeg(nameof(AutorizacionInicia))]
        public IActionResult AutorizacionCon()
        {
            base.MCargaFiltroPagYOrd(EVAutorizaciones.AutorizacionFiltro,
                                     EVAutorizaciones.AutorizacionPag,
                                     EVAutorizaciones.AutorizacionColOrden,
                                     nameof(EAutorizacion));

            EVAutorizaciones.AutorizacionPag = NAutorizaciones.AutorizacionPag(EVAutorizaciones.AutorizacionFiltro);
            base.MActualizaTamPag(EVAutorizaciones.AutorizacionPag?.DatPag);

            ViewBag.Mensajes = base.MObtenMensajes(NAutorizaciones.Mensajes);
            ViewBag.Reglas = EVAutorizaciones.AutorizacionReglas;
            ViewBag.DatPag = EVAutorizaciones.AutorizacionPag?.DatPag;
            ViewBag.Orden = EVAutorizaciones.AutorizacionColOrden;
            ViewBag.Filtro = EVAutorizaciones.AutorizacionFiltro;
            ViewBag.Indice = EVAutorizaciones.AutorizacionIndice;

            ViewBag.ProcesosOperativos = NProcesosOperativos.ProcesoOperativoCmb();

            return View(nameof(AutorizacionCon), EVAutorizaciones.AutorizacionPag?.Pagina);
        }
        public IActionResult AutorizacionXId(Int32 indice)
        {
            EVAutorizaciones.Accion = MAccionesGen.Consulta;
            EVAutorizaciones.AutorizacionIndice = indice;
            return AutorizacionCaptura(EVAutorizaciones.AutorizacionPag.Pagina[indice]);
        }
        [MValidaSeg(nameof(AutorizacionInserta))]
        public IActionResult AutorizacionInsertaIni()
        {
            EVAutorizaciones.Accion = MAccionesGen.Inserta;
            return AutorizacionInsertaCap(new EAutorizacion());
        }
        [ValidateAntiForgeryToken]
        [MValidaSeg(nameof(AutorizacionInserta))]
        public IActionResult AutorizacionInsertaCap(EAutorizacion autorizacion)
        {
            return AutorizacionCaptura(autorizacion);
        }
        [ValidateAntiForgeryToken]
        public IActionResult AutorizacionInserta(EAutorizacion autorizacion)
        {
            NAutorizaciones.AutorizacionInserta(autorizacion);
            if (NAutorizaciones.Mensajes.Ok)
                return RedirectToAction(nameof(AutorizacionCon));

            return AutorizacionInsertaCap(autorizacion);
        }
        [MValidaSeg(nameof(AutorizacionActualiza))]
        public IActionResult AutorizacionActualizaIni(Int32 indice)
        {
            EVAutorizaciones.Accion = MAccionesGen.Actualiza;
            EVAutorizaciones.AutorizacionIndice = indice;
            EVAutorizaciones.AutorizacionSel = EVAutorizaciones.AutorizacionPag.Pagina[indice];
            return AutorizacionActualizaCap(EVAutorizaciones.AutorizacionSel);
        }
        [ValidateAntiForgeryToken]
        [MValidaSeg(nameof(AutorizacionActualiza))]
        public IActionResult AutorizacionActualizaCap(EAutorizacion autorizacion)
        {
            return AutorizacionCaptura(autorizacion);
        }
        [ValidateAntiForgeryToken]
        public IActionResult AutorizacionActualiza(EAutorizacion autorizacion)
        {
            if (NAutorizaciones.AutorizacionActualiza(autorizacion))
                return RedirectToAction(nameof(AutorizacionCon));

            return AutorizacionActualizaCap(autorizacion);
        }
        public IActionResult AutorizacionElimina(Int32 indice)
        {
            NAutorizaciones.AutorizacionElimina(EVAutorizaciones.AutorizacionPag.Pagina[indice]);
            base.MMensajesTemp = NAutorizaciones.Mensajes.ToString();
            return RedirectToAction(nameof(AutorizacionCon));
        }
        #endregion

        #region Funciones
        private IActionResult AutorizacionCaptura(EAutorizacion autorizacion)
        {
            ViewBag.Mensajes = base.MObtenMensajes(NAutorizaciones.Mensajes);
            ViewBag.Accion = EVAutorizaciones.Accion;
            ViewBag.Reglas = EVAutorizaciones.AutorizacionReglas;

            ViewBag.ProcesosOperativos = NProcesosOperativos.ProcesoOperativoCmb();

            return ViewCap(nameof(AutorizacionCaptura), autorizacion);
        }
        #endregion

        #region Acciones de Paginacion Orden y Filtro
        [MValidaSeg(nameof(AutorizacionInicia))]
        public IActionResult AutorizacionPaginacion(MEDatosPaginador datPag)
        {
            EVAutorizaciones.AutorizacionPag.DatPag = datPag;
            return RedirectToAction(nameof(AutorizacionCon));
        }
        [MValidaSeg(nameof(AutorizacionInicia))]
        public IActionResult AutorizacionOrdena(String orden)
        {
            EVAutorizaciones.AutorizacionColOrden = orden;
            return RedirectToAction(nameof(AutorizacionCon));
        }
        [MValidaSeg(nameof(AutorizacionInicia))]
        public IActionResult AutorizacionFiltra(EAutorizacionFiltro filtro)
        {
            EVAutorizaciones.AutorizacionFiltro = filtro;
            return RedirectToAction(nameof(AutorizacionCon));
        }
        [MValidaSeg(nameof(AutorizacionInicia))]
        public IActionResult AutorizacionLimpiaFiltros()
        {
            EVAutorizaciones.AutorizacionFiltro = new EAutorizacionFiltro();
            return RedirectToAction(nameof(AutorizacionCon));
        }
        #endregion

        #endregion

        #region AutorizacionUsuario (AutorizacionesUsuarios)

        #region Acciones
        public IActionResult AutorizacionUsuarioInicia(Int32 indice)
        {
            //Configuracion de inicio
            if (String.IsNullOrWhiteSpace(EVAutorizaciones.AutorizacionUsuarioColOrden))
                EVAutorizaciones.AutorizacionUsuarioColOrden = nameof(EAutorizacionUsuario.AutorizacionUsuarioId);

            if (EVAutorizaciones.AutorizacionUsuarioReglas == null)
            {
                EVAutorizaciones.AutorizacionUsuarioReglas = NAutorizaciones.AutorizacionUsuarioReglas();
                base.MMensajesTemp = NAutorizaciones.Mensajes.ToString();
            }

            if (indice >= 0)
            {
                EVAutorizaciones.AutorizacionIndice = indice;
                EVAutorizaciones.AutorizacionSel = EVAutorizaciones.AutorizacionPag.Pagina[indice];
            }

            return RedirectToAction(nameof(AutorizacionUsuarioCon));
        }
        [MValidaSeg(nameof(AutorizacionUsuarioInicia))]
        public IActionResult AutorizacionUsuarioCon()
        {
            EVAutorizaciones.AutorizacionUsuarioFiltro.AutorizacionId = EVAutorizaciones.AutorizacionSel.AutorizacionId;
            base.MCargaFiltroPagYOrd(EVAutorizaciones.AutorizacionUsuarioFiltro,
                                     EVAutorizaciones.AutorizacionUsuarioPag,
                                     EVAutorizaciones.AutorizacionUsuarioColOrden,
                                     nameof(EAutorizacionUsuario));

            EVAutorizaciones.AutorizacionUsuarioPag = NAutorizaciones.AutorizacionUsuarioPag(EVAutorizaciones.AutorizacionUsuarioFiltro);
            base.MActualizaTamPag(EVAutorizaciones.AutorizacionUsuarioPag?.DatPag);

            ViewBag.Mensajes = base.MObtenMensajes(NAutorizaciones.Mensajes);
            ViewBag.Reglas = EVAutorizaciones.AutorizacionUsuarioReglas;
            ViewBag.DatPag = EVAutorizaciones.AutorizacionUsuarioPag?.DatPag;
            ViewBag.Orden = EVAutorizaciones.AutorizacionUsuarioColOrden;
            ViewBag.Filtro = EVAutorizaciones.AutorizacionUsuarioFiltro;
            ViewBag.Indice = EVAutorizaciones.AutorizacionUsuarioIndice;

            return View(nameof(AutorizacionUsuarioCon), EVAutorizaciones.AutorizacionUsuarioPag?.Pagina);
        }
        public IActionResult AutorizacionUsuarioXId(Int32 indice)
        {
            EVAutorizaciones.Accion = MAccionesGen.Consulta;
            EVAutorizaciones.AutorizacionUsuarioIndice = indice;
            return AutorizacionUsuarioCaptura(EVAutorizaciones.AutorizacionUsuarioPag.Pagina[indice]);
        }
        [MValidaSeg(nameof(AutorizacionUsuarioInserta))]
        public IActionResult AutorizacionUsuarioInsertaIni()
        {
            EVAutorizaciones.Accion = MAccionesGen.Inserta;
            return AutorizacionUsuarioInsertaCap(new EAutorizacionUsuario());
        }
        [ValidateAntiForgeryToken]
        [MValidaSeg(nameof(AutorizacionUsuarioInserta))]
        public IActionResult AutorizacionUsuarioInsertaCap(EAutorizacionUsuario autorizacionUsuario)
        {
            return AutorizacionUsuarioCaptura(autorizacionUsuario);
        }
        [ValidateAntiForgeryToken]
        public IActionResult AutorizacionUsuarioInserta(EAutorizacionUsuario autorizacionUsuario)
        {
            autorizacionUsuario.AutorizacionId = EVAutorizaciones.AutorizacionSel.AutorizacionId; //Llave padre
            NAutorizaciones.AutorizacionUsuarioInserta(autorizacionUsuario);
            if (NAutorizaciones.Mensajes.Ok)
                return RedirectToAction(nameof(AutorizacionUsuarioCon));

            return AutorizacionUsuarioInsertaCap(autorizacionUsuario);
        }
        [MValidaSeg(nameof(AutorizacionUsuarioActualiza))]
        public IActionResult AutorizacionUsuarioActualizaIni(Int32 indice)
        {
            EVAutorizaciones.Accion = MAccionesGen.Actualiza;
            EVAutorizaciones.AutorizacionUsuarioIndice = indice;
            EVAutorizaciones.AutorizacionUsuarioSel = EVAutorizaciones.AutorizacionUsuarioPag.Pagina[indice];
            return AutorizacionUsuarioActualizaCap(EVAutorizaciones.AutorizacionUsuarioSel);
        }
        [ValidateAntiForgeryToken]
        [MValidaSeg(nameof(AutorizacionUsuarioActualiza))]
        public IActionResult AutorizacionUsuarioActualizaCap(EAutorizacionUsuario autorizacionUsuario)
        {
            return AutorizacionUsuarioCaptura(autorizacionUsuario);
        }
        [ValidateAntiForgeryToken]
        public IActionResult AutorizacionUsuarioActualiza(EAutorizacionUsuario autorizacionUsuario)
        {
            autorizacionUsuario.AutorizacionId = EVAutorizaciones.AutorizacionSel.AutorizacionId; //Llave padre
            if (NAutorizaciones.AutorizacionUsuarioActualiza(autorizacionUsuario))
                return RedirectToAction(nameof(AutorizacionUsuarioCon));

            return AutorizacionUsuarioActualizaCap(autorizacionUsuario);
        }
        public IActionResult AutorizacionUsuarioElimina(Int32 indice)
        {
            NAutorizaciones.AutorizacionUsuarioElimina(EVAutorizaciones.AutorizacionUsuarioPag.Pagina[indice]);
            base.MMensajesTemp = NAutorizaciones.Mensajes.ToString();
            return RedirectToAction(nameof(AutorizacionUsuarioCon));
        }
        #endregion

        #region Funciones
        private IActionResult AutorizacionUsuarioCaptura(EAutorizacionUsuario autorizacionUsuario)
        {
            ViewBag.Mensajes = base.MObtenMensajes(NAutorizaciones.Mensajes);
            ViewBag.Accion = EVAutorizaciones.Accion;
            ViewBag.Reglas = EVAutorizaciones.AutorizacionUsuarioReglas;

            ViewBag.ProcesosOperativosEst = NProcesosOperativos.ProcesoOperativoEstCmb(EVAutorizaciones.AutorizacionSel.ProcesoOperativoId);

            return ViewCap(nameof(AutorizacionUsuarioCaptura), autorizacionUsuario);
        }
        #endregion

        #region Acciones de Paginacion Orden y Filtro
        [MValidaSeg(nameof(AutorizacionUsuarioInicia))]
        public IActionResult AutorizacionUsuarioPaginacion(MEDatosPaginador datPag)
        {
            EVAutorizaciones.AutorizacionUsuarioPag.DatPag = datPag;
            return RedirectToAction(nameof(AutorizacionUsuarioCon));
        }
        [MValidaSeg(nameof(AutorizacionUsuarioInicia))]
        public IActionResult AutorizacionUsuarioOrdena(String orden)
        {
            EVAutorizaciones.AutorizacionUsuarioColOrden = orden;
            return RedirectToAction(nameof(AutorizacionUsuarioCon));
        }
        [MValidaSeg(nameof(AutorizacionUsuarioInicia))]
        public IActionResult AutorizacionUsuarioFiltra(EAutorizacionUsuarioFiltro filtro)
        {
            EVAutorizaciones.AutorizacionUsuarioFiltro = filtro;
            return RedirectToAction(nameof(AutorizacionUsuarioCon));
        }
        [MValidaSeg(nameof(AutorizacionUsuarioInicia))]
        public IActionResult AutorizacionUsuarioLimpiaFiltros()
        {
            EVAutorizaciones.AutorizacionUsuarioFiltro = new EAutorizacionUsuarioFiltro();
            return RedirectToAction(nameof(AutorizacionUsuarioCon));
        }
        #endregion

        #endregion

        #region Combos Filtro
        [MValidaSeg(nameof(AutorizacionUsuarioInicia))]
        public IActionResult OpcUsuarioId(String usuarioId)
        {
            return Ok(MUtilPres.ElementosAHtml(NUsuarios.UsuarioCmb(EVAutorizaciones.AutorizacionSel.EstablecimientoId,
                                                                    usuarioId)));
        }
        #endregion
    }
}
