using DSEntityNetX.Common.Casting;
using DSEntityNetX.Common.File;
using DSEntityNetX.Common.Pagination;
using DSMetodNetX.Aplicacion;
using DSMetodNetX.Entidades;
using DSMetodNetX.Mvc;
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
    public class SapCondicionesPagoController : MControllerMvcPri
    {
        #region Constructores
        /// <summary>
        /// Controlador MVC.
        /// </summary>
        public SapCondicionesPagoController(INSapCondicionesPago nSapCondicionesPago)
        {
            NSapCondicionesPago = nSapCondicionesPago;
        }
        #endregion

        #region Propiedades
        /// <summary>
        /// Negocio SapCondicionesPago.
        /// </summary>
        private INSapCondicionesPago NSapCondicionesPago { get; set; }
        /// <summary>
        /// Entidad de variables.
        /// </summary>
        private EVSapCondicionesPago EV
        {
            get { return base.MEVCtrl<EVSapCondicionesPago>(); }
        }
        #endregion

        #region SapCondicionPago (SapCondicionesPago)

        #region Acciones
        /// <summary>
        /// Inicia sub funcion.
        /// </summary>
        public async Task<IActionResult> SapCondicionPagoInicia()
        {
            //Configuracion de inicio
            await Servicios.Gen.InicializaSF(EV.SapCondicionPago, nameof(ESapCondicionPago.SapCondicionPagoId),
                async () => await NSapCondicionesPago.SapCondicionPagoReglas());

            return RedirectToAction(nameof(SapCondicionPagoCon));
        }
        /// <summary>
        /// Consulta.
        /// </summary>
        [MValidaSeg(nameof(SapCondicionPagoInicia))]
        public async Task<IActionResult> SapCondicionPagoCon()
        {
            await Servicios.Pag.CargaPagOrdYFil(EV.SapCondicionPago);
            EV.SapCondicionPago.Pag = await NSapCondicionesPago.SapCondicionPagoPag(EV.SapCondicionPago.Filtro);
            await Servicios.Pag.ActTamPag(EV.SapCondicionPago);

            ViewBag.Mensajes = NSapCondicionesPago.Mensajes;
            ViewBag.EV = EV;

            return View(nameof(SapCondicionPagoCon), EV.SapCondicionPago.Pag?.Pagina);
        }
        /// <summary>
        /// Consulta por id.
        /// </summary>
        public async Task<IActionResult> SapCondicionPagoXId(Int32 indice)
        {
            EV.Accion = MAccionesGen.Consulta;
            EV.SapCondicionPago.Indice = indice;
            return await SapCondicionPagoCaptura(EV.SapCondicionPago.Pag.Pagina[indice]);
        }
        /// <summary>
        /// Inserta.
        /// </summary>
        [MValidaSeg(nameof(SapCondicionPagoInserta))]
        public async Task<IActionResult> SapCondicionPagoInsertaIni()
        {
            EV.Accion = MAccionesGen.Inserta;
            return await SapCondicionPagoInsertaCap(new ESapCondicionPago()
            {
                Activo = true
            });
        }
        /// <summary>
        /// Inserta.
        /// </summary>
        [ValidateAntiForgeryToken]
        [MValidaSeg(nameof(SapCondicionPagoInserta))]
        public async Task<IActionResult> SapCondicionPagoInsertaCap(ESapCondicionPago sapCondicionPago)
        {
            return await SapCondicionPagoCaptura(sapCondicionPago);
        }
        /// <summary>
        /// Inserta.
        /// </summary>
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SapCondicionPagoInserta(ESapCondicionPago sapCondicionPago)
        {
            if (await NSapCondicionesPago.SapCondicionPagoInserta(sapCondicionPago))
                return RedirectToAction(nameof(SapCondicionPagoCon));

            return await SapCondicionPagoInsertaCap(sapCondicionPago);
        }
        /// <summary>
        /// Actualiza.
        /// </summary>
        [MValidaSeg(nameof(SapCondicionPagoActualiza))]
        public async Task<IActionResult> SapCondicionPagoActualizaIni(Int32 indice)
        {
            EV.Accion = MAccionesGen.Actualiza;
            EV.SapCondicionPago.Indice = indice;
            EV.SapCondicionPago.Sel = EV.SapCondicionPago.Pag.Pagina[indice];
            return await SapCondicionPagoActualizaCap(EV.SapCondicionPago.Sel);
        }
        /// <summary>
        /// Actualiza.
        /// </summary>
        [ValidateAntiForgeryToken]
        [MValidaSeg(nameof(SapCondicionPagoActualiza))]
        public async Task<IActionResult> SapCondicionPagoActualizaCap(ESapCondicionPago sapCondicionPago)
        {
            return await SapCondicionPagoCaptura(sapCondicionPago);
        }
        /// <summary>
        /// Actualiza.
        /// </summary>
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SapCondicionPagoActualiza(ESapCondicionPago sapCondicionPago)
        {
            if (await NSapCondicionesPago.SapCondicionPagoActualiza(sapCondicionPago))
                return RedirectToAction(nameof(SapCondicionPagoCon));

            return await SapCondicionPagoActualizaCap(sapCondicionPago);
        }
        /// <summary>
        /// Elimina.
        /// </summary>
        public async Task<IActionResult> SapCondicionPagoElimina(Int32 indice)
        {
            await NSapCondicionesPago.SapCondicionPagoElimina(EV.SapCondicionPago.Pag.Pagina[indice]);
            return RedirectToAction(nameof(SapCondicionPagoCon));
        }
        /// <summary>
        /// Exportacion de datos.
        /// </summary>
        public async Task<IActionResult> SapCondicionPagoExporta()
        {
            EV.SapCondicionPago.Filtro.ColumnaOrden = EV.SapCondicionPago.ColOrden;
            EV.SapCondicionPago.Filtro.Columnas = new Dictionary<String, String>()
                                        {
                                            { nameof(ESapCondicionPago.Activo), String.Empty },
                                            { nameof(ESapCondicionPago.SapCondicionPagoId), String.Empty },
                                            { nameof(ESapCondicionPago.SapCondicionPagoNombre), String.Empty }
                                        };

            String vRutaYNombreArchivo = await NSapCondicionesPago.SapCondicionPagoExporta(EV.SapCondicionPago.Filtro);
            EV.SapCondicionPago.Filtro.Columnas = null;
            if (NSapCondicionesPago.Mensajes.Ok)
                return await MUtilMvc.DescargaArchivo(await Servicios.Archivos.DescargaArchivoTemp(vRutaYNombreArchivo));

            return RedirectToAction(nameof(SapCondicionPagoCon));
        }
        #endregion

        #region Funciones
        /// <summary>
        /// Captura.
        /// </summary>
        private async Task<IActionResult> SapCondicionPagoCaptura(ESapCondicionPago sapCondicionPago)
        {
            ViewBag.Mensajes = NSapCondicionesPago.Mensajes;
            ViewBag.EV = EV;

            return await Task.FromResult(ViewCap(nameof(SapCondicionPagoCaptura), sapCondicionPago));
        }
        #endregion

        #region Acciones de Paginacion Orden y Filtro
        /// <summary>
        /// Control de paginacion.
        /// </summary>
        [MValidaSeg(nameof(SapCondicionPagoInicia))]
        public IActionResult SapCondicionPagoPaginacion(MEDatosPaginador datPag)
        {
            EV.SapCondicionPago.Pag.DatPag = datPag;
            return RedirectToAction(nameof(SapCondicionPagoCon));
        }
        /// <summary>
        /// Control de orden.
        /// </summary>
        [MValidaSeg(nameof(SapCondicionPagoInicia))]
        public IActionResult SapCondicionPagoOrdena(String orden)
        {
            EV.SapCondicionPago.ColOrden = orden;
            return RedirectToAction(nameof(SapCondicionPagoCon));
        }
        /// <summary>
        /// Control de filtro.
        /// </summary>
        [MValidaSeg(nameof(SapCondicionPagoInicia))]
        public IActionResult SapCondicionPagoFiltra(ESapCondicionPagoFiltro filtro)
        {
            EV.SapCondicionPago.Filtro = filtro;
            return RedirectToAction(nameof(SapCondicionPagoCon));
        }
        /// <summary>
        /// Limpia filtros.
        /// </summary>
        [MValidaSeg(nameof(SapCondicionPagoInicia))]
        public IActionResult SapCondicionPagoLimpiaFiltros()
        {
            EV.SapCondicionPago.Filtro = new ESapCondicionPagoFiltro();
            return RedirectToAction(nameof(SapCondicionPagoCon));
        }
        #endregion

        #endregion
    }
}
