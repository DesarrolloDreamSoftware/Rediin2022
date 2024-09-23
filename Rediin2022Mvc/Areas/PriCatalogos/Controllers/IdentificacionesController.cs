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

namespace Rediin2022Mvc.Areas.PriCatalogos.Controllers
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
        private EVIdentificaciones EV
        {
            get { return base.MEVCtrl<EVIdentificaciones>(); }
        }
        #endregion

        #region Identificacion (Identificaciones)

        #region Acciones
        /// <summary>
        /// Inicia sub funcion.
        /// </summary>
        public async Task<IActionResult> IdentificacionInicia()
        {
            //Configuracion de inicio
            await Servicios.Gen.InicializaSF(EV.Identificacion, "-" + nameof(EIdentificacion.IdentificacionId),
                async () => await NIdentificaciones.IdentificacionReglas());

            return RedirectToAction(nameof(IdentificacionCon));
        }
        /// <summary>
        /// Consulta.
        /// </summary>
        [MValidaSeg(nameof(IdentificacionInicia))]
        public async Task<IActionResult> IdentificacionCon()
        {
            await Servicios.Pag.CargaPagOrdYFil(EV.Identificacion);
            EV.Identificacion.Pag = await NIdentificaciones.IdentificacionPag(EV.Identificacion.Filtro);
            await Servicios.Pag.ActTamPag(EV.Identificacion);

            ViewBag.Mensajes = NIdentificaciones.Mensajes;
            ViewBag.EV = EV;

            return View(nameof(IdentificacionCon), EV.Identificacion.Pag?.Pagina);
        }
        /// <summary>
        /// Consulta por id.
        /// </summary>
        public async Task<IActionResult> IdentificacionXId(Int32 indice)
        {
            EV.Accion = MAccionesGen.Consulta;
            EV.Identificacion.Indice = indice;
            return await IdentificacionCaptura(EV.Identificacion.Pag.Pagina[indice]);
        }
        /// <summary>
        /// Inserta.
        /// </summary>
        [MValidaSeg(nameof(IdentificacionInserta))]
        public async Task<IActionResult> IdentificacionInsertaIni()
        {
            EV.Accion = MAccionesGen.Inserta;
            return await IdentificacionInsertaCap(new EIdentificacion()
            {
                Activo = true
            });
        }
        /// <summary>
        /// Inserta.
        /// </summary>
        [ValidateAntiForgeryToken]
        [MValidaSeg(nameof(IdentificacionInserta))]
        public async Task<IActionResult> IdentificacionInsertaCap(EIdentificacion identificacion)
        {
            return await IdentificacionCaptura(identificacion);
        }
        /// <summary>
        /// Inserta.
        /// </summary>
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> IdentificacionInserta(EIdentificacion identificacion)
        {
            await NIdentificaciones.IdentificacionInserta(identificacion);
            if (NIdentificaciones.Mensajes.Ok)
                return RedirectToAction(nameof(IdentificacionCon));

            return await IdentificacionInsertaCap(identificacion);
        }
        /// <summary>
        /// Actualiza.
        /// </summary>
        [MValidaSeg(nameof(IdentificacionActualiza))]
        public async Task<IActionResult> IdentificacionActualizaIni(Int32 indice)
        {
            EV.Accion = MAccionesGen.Actualiza;
            EV.Identificacion.Indice = indice;
            EV.Identificacion.Sel = EV.Identificacion.Pag.Pagina[indice];
            return await IdentificacionActualizaCap(EV.Identificacion.Sel);
        }
        /// <summary>
        /// Actualiza.
        /// </summary>
        [ValidateAntiForgeryToken]
        [MValidaSeg(nameof(IdentificacionActualiza))]
        public async Task<IActionResult> IdentificacionActualizaCap(EIdentificacion identificacion)
        {
            return await IdentificacionCaptura(identificacion);
        }
        /// <summary>
        /// Actualiza.
        /// </summary>
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> IdentificacionActualiza(EIdentificacion identificacion)
        {
            if (await NIdentificaciones.IdentificacionActualiza(identificacion))
                return RedirectToAction(nameof(IdentificacionCon));

            return await IdentificacionActualizaCap(identificacion);
        }
        /// <summary>
        /// Elimina.
        /// </summary>
        public async Task<IActionResult> IdentificacionElimina(Int32 indice)
        {
            await NIdentificaciones.IdentificacionElimina(EV.Identificacion.Pag.Pagina[indice]);
            return RedirectToAction(nameof(IdentificacionCon));
        }
        #endregion

        #region Funciones
        /// <summary>
        /// Captura.
        /// </summary>
        private async Task<IActionResult> IdentificacionCaptura(EIdentificacion identificacion)
        {
            ViewBag.Mensajes = NIdentificaciones.Mensajes;
            ViewBag.EV = EV;

            return await Task.FromResult(ViewCap(nameof(IdentificacionCaptura), identificacion));
        }
        #endregion

        #region Acciones de Paginacion Orden y Filtro
        /// <summary>
        /// Control de paginacion.
        /// </summary>
        [MValidaSeg(nameof(IdentificacionInicia))]
        public IActionResult IdentificacionPaginacion(MEDatosPaginador datPag)
        {
            EV.Identificacion.Pag.DatPag = datPag;
            return RedirectToAction(nameof(IdentificacionCon));
        }
        /// <summary>
        /// Control de orden.
        /// </summary>
        [MValidaSeg(nameof(IdentificacionInicia))]
        public IActionResult IdentificacionOrdena(String orden)
        {
            EV.Identificacion.ColOrden = orden;
            return RedirectToAction(nameof(IdentificacionCon));
        }
        /// <summary>
        /// Control de filtro.
        /// </summary>
        [MValidaSeg(nameof(IdentificacionInicia))]
        public IActionResult IdentificacionFiltra(EIdentificacionFiltro filtro)
        {
            EV.Identificacion.Filtro = filtro;
            return RedirectToAction(nameof(IdentificacionCon));
        }
        /// <summary>
        /// Limpia filtros.
        /// </summary>
        [MValidaSeg(nameof(IdentificacionInicia))]
        public IActionResult IdentificacionLimpiaFiltros()
        {
            EV.Identificacion.Filtro = new EIdentificacionFiltro();
            return RedirectToAction(nameof(IdentificacionCon));
        }
        #endregion

        #endregion
    }
}
