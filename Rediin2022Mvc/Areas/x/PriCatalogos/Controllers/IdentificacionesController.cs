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
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Rediin2022Web.Areas.PriCatalogos.Controllers
{
    /// <summary>
    /// Controlador MVC.
    /// </summary>
    [Area("PriCatalogos")]
    public class IdentificacionesController : MControllerMvcPri
    {
        #region Constructores
        /// <summary>
        /// Controlador MVC.
        /// </summary>
        public IdentificacionesController(INIdentificaciones nIdentificaciones)
        {
            NIdentificaciones = nIdentificaciones;
        }
        #endregion

        #region Propiedades
        /// <summary>
        /// Negocio Identificaciones.
        /// </summary>
        private INIdentificaciones NIdentificaciones { get; set; }
        /// <summary>
        /// Entidad de variables.
        /// </summary>
        private EVIdentificaciones EVIdentificaciones
        {
            get
            {
                if (base.MSesion<EVIdentificaciones>() == null)
                    base.MSesion(new EVIdentificaciones());

                return base.MSesionAuto<EVIdentificaciones>();
            }
        }
        #endregion

        #region Identificacion (Identificaciones)

        #region Acciones
        /// <summary>
        /// Inicia sub funcion.
        /// </summary>
        public IActionResult IdentificacionInicia()
        {
            //Configuracion de inicio
            if (String.IsNullOrWhiteSpace(EVIdentificaciones.IdentificacionColOrden))
                EVIdentificaciones.IdentificacionColOrden = "-" + nameof(EIdentificacion.IdentificacionId);

            if (EVIdentificaciones.IdentificacionReglas == null)
            {
                EVIdentificaciones.IdentificacionReglas = NIdentificaciones.IdentificacionReglas();
                base.MMensajesTemp = NIdentificaciones.Mensajes.ToString();
            }

            return RedirectToAction(nameof(IdentificacionCon));
        }
        /// <summary>
        /// Consulta.
        /// </summary>
        [MValidaSeg(nameof(IdentificacionInicia))]
        public IActionResult IdentificacionCon()
        {
            base.MCargaFiltroPagYOrd(EVIdentificaciones.IdentificacionFiltro,
                                     EVIdentificaciones.IdentificacionPag,
                                     EVIdentificaciones.IdentificacionColOrden,
                                     nameof(EIdentificacion));

            EVIdentificaciones.IdentificacionPag = NIdentificaciones.IdentificacionPag(EVIdentificaciones.IdentificacionFiltro);
            base.MActualizaTamPag(EVIdentificaciones.IdentificacionPag?.DatPag);

            ViewBag.Mensajes = base.MObtenMensajes(NIdentificaciones.Mensajes);
            ViewBag.Reglas = EVIdentificaciones.IdentificacionReglas;
            ViewBag.DatPag = EVIdentificaciones.IdentificacionPag?.DatPag;
            ViewBag.Orden = EVIdentificaciones.IdentificacionColOrden;
            ViewBag.Filtro = EVIdentificaciones.IdentificacionFiltro;
            ViewBag.Indice = EVIdentificaciones.IdentificacionIndice;

            return View(nameof(IdentificacionCon), EVIdentificaciones.IdentificacionPag?.Pagina);
        }
        /// <summary>
        /// Consulta por id.
        /// </summary>
        public IActionResult IdentificacionXId(Int32 indice)
        {
            EVIdentificaciones.Accion = MAccionesGen.Consulta;
            EVIdentificaciones.IdentificacionIndice = indice;
            return IdentificacionCaptura(EVIdentificaciones.IdentificacionPag.Pagina[indice]);
        }
        /// <summary>
        /// Inserta.
        /// </summary>
        [MValidaSeg(nameof(IdentificacionInserta))]
        public IActionResult IdentificacionInsertaIni()
        {
            EVIdentificaciones.Accion = MAccionesGen.Inserta;
            return IdentificacionInsertaCap(new EIdentificacion()
            {
                Activo = true
            });
        }
        /// <summary>
        /// Inserta.
        /// </summary>
        [ValidateAntiForgeryToken]
        [MValidaSeg(nameof(IdentificacionInserta))]
        public IActionResult IdentificacionInsertaCap(EIdentificacion identificacion)
        {
            return IdentificacionCaptura(identificacion);
        }
        /// <summary>
        /// Inserta.
        /// </summary>
        [ValidateAntiForgeryToken]
        public IActionResult IdentificacionInserta(EIdentificacion identificacion)
        {
            NIdentificaciones.IdentificacionInserta(identificacion);
            if (NIdentificaciones.Mensajes.Ok)
                return RedirectToAction(nameof(IdentificacionCon));

            return IdentificacionInsertaCap(identificacion);
        }
        /// <summary>
        /// Actualiza.
        /// </summary>
        [MValidaSeg(nameof(IdentificacionActualiza))]
        public IActionResult IdentificacionActualizaIni(Int32 indice)
        {
            EVIdentificaciones.Accion = MAccionesGen.Actualiza;
            EVIdentificaciones.IdentificacionIndice = indice;
            EVIdentificaciones.IdentificacionSel = EVIdentificaciones.IdentificacionPag.Pagina[indice];
            return IdentificacionActualizaCap(EVIdentificaciones.IdentificacionSel);
        }
        /// <summary>
        /// Actualiza.
        /// </summary>
        [ValidateAntiForgeryToken]
        [MValidaSeg(nameof(IdentificacionActualiza))]
        public IActionResult IdentificacionActualizaCap(EIdentificacion identificacion)
        {
            return IdentificacionCaptura(identificacion);
        }
        /// <summary>
        /// Actualiza.
        /// </summary>
        [ValidateAntiForgeryToken]
        public IActionResult IdentificacionActualiza(EIdentificacion identificacion)
        {
            if (NIdentificaciones.IdentificacionActualiza(identificacion))
                return RedirectToAction(nameof(IdentificacionCon));

            return IdentificacionActualizaCap(identificacion);
        }
        /// <summary>
        /// Elimina.
        /// </summary>
        public IActionResult IdentificacionElimina(Int32 indice)
        {
            NIdentificaciones.IdentificacionElimina(EVIdentificaciones.IdentificacionPag.Pagina[indice]);
            base.MMensajesTemp = NIdentificaciones.Mensajes.ToString();
            return RedirectToAction(nameof(IdentificacionCon));
        }
        #endregion

        #region Funciones
        /// <summary>
        /// Captura.
        /// </summary>
        private IActionResult IdentificacionCaptura(EIdentificacion identificacion)
        {
            ViewBag.Mensajes = base.MObtenMensajes(NIdentificaciones.Mensajes);
            ViewBag.Accion = EVIdentificaciones.Accion;
            ViewBag.Reglas = EVIdentificaciones.IdentificacionReglas;

            return ViewCap(nameof(IdentificacionCaptura), identificacion);
        }
        #endregion

        #region Acciones de Paginacion Orden y Filtro
        /// <summary>
        /// Control de paginacion.
        /// </summary>
        [MValidaSeg(nameof(IdentificacionInicia))]
        public IActionResult IdentificacionPaginacion(MEDatosPaginador datPag)
        {
            EVIdentificaciones.IdentificacionPag.DatPag = datPag;
            return RedirectToAction(nameof(IdentificacionCon));
        }
        /// <summary>
        /// Control de orden.
        /// </summary>
        [MValidaSeg(nameof(IdentificacionInicia))]
        public IActionResult IdentificacionOrdena(String orden)
        {
            EVIdentificaciones.IdentificacionColOrden = orden;
            return RedirectToAction(nameof(IdentificacionCon));
        }
        /// <summary>
        /// Control de filtro.
        /// </summary>
        [MValidaSeg(nameof(IdentificacionInicia))]
        public IActionResult IdentificacionFiltra(EIdentificacionFiltro filtro)
        {
            EVIdentificaciones.IdentificacionFiltro = filtro;
            return RedirectToAction(nameof(IdentificacionCon));
        }
        /// <summary>
        /// Limpia filtros.
        /// </summary>
        [MValidaSeg(nameof(IdentificacionInicia))]
        public IActionResult IdentificacionLimpiaFiltros()
        {
            EVIdentificaciones.IdentificacionFiltro = new EIdentificacionFiltro();
            return RedirectToAction(nameof(IdentificacionCon));
        }
        #endregion

        #endregion
    }
}
