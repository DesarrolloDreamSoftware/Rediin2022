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
    public class SapViasPagoController : MControllerMvcPri
    {
        #region Constructores
        /// <summary>
        /// Controlador MVC.
        /// </summary>
        public SapViasPagoController(INSapViasPago nSapViasPago)
        {
            NSapViasPago = nSapViasPago;
        }
        #endregion

        #region Propiedades
        /// <summary>
        /// Negocio SapViasPago.
        /// </summary>
        private INSapViasPago NSapViasPago { get; set; }
        /// <summary>
        /// Entidad de variables.
        /// </summary>
        private EVSapViasPago EV
        {
            get { return base.MEVCtrl<EVSapViasPago>(); }
        }
        #endregion

        #region SapViaPago (SapViasPago)

        #region Acciones
        /// <summary>
        /// Inicia sub funcion.
        /// </summary>
        public async Task<IActionResult> SapViaPagoInicia()
        {
            //Configuracion de inicio
            await Servicios.Gen.InicializaSF(EV.SapViaPago, nameof(ESapViaPago.SapViaPagoId),
                async () => await NSapViasPago.SapViaPagoReglas());

            return RedirectToAction(nameof(SapViaPagoCon));
        }
        /// <summary>
        /// Consulta.
        /// </summary>
        [MValidaSeg(nameof(SapViaPagoInicia))]
        public async Task<IActionResult> SapViaPagoCon()
        {
            await Servicios.Pag.CargaPagOrdYFil(EV.SapViaPago);
            EV.SapViaPago.Pag = await NSapViasPago.SapViaPagoPag(EV.SapViaPago.Filtro);
            await Servicios.Pag.ActTamPag(EV.SapViaPago);

            ViewBag.Mensajes = NSapViasPago.Mensajes;
            ViewBag.EV = EV;

            return View(nameof(SapViaPagoCon), EV.SapViaPago.Pag?.Pagina);
        }
        /// <summary>
        /// Consulta por id.
        /// </summary>
        public async Task<IActionResult> SapViaPagoXId(Int32 indice)
        {
            EV.Accion = MAccionesGen.Consulta;
            EV.SapViaPago.Indice = indice;
            return await SapViaPagoCaptura(EV.SapViaPago.Pag.Pagina[indice]);
        }
        /// <summary>
        /// Inserta.
        /// </summary>
        [MValidaSeg(nameof(SapViaPagoInserta))]
        public async Task<IActionResult> SapViaPagoInsertaIni()
        {
            EV.Accion = MAccionesGen.Inserta;
            return await SapViaPagoInsertaCap(new ESapViaPago()
            {
                Activo = true
            });
        }
        /// <summary>
        /// Inserta.
        /// </summary>
        [ValidateAntiForgeryToken]
        [MValidaSeg(nameof(SapViaPagoInserta))]
        public async Task<IActionResult> SapViaPagoInsertaCap(ESapViaPago sapViaPago)
        {
            return await SapViaPagoCaptura(sapViaPago);
        }
        /// <summary>
        /// Inserta.
        /// </summary>
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SapViaPagoInserta(ESapViaPago sapViaPago)
        {
            if (await NSapViasPago.SapViaPagoInserta(sapViaPago))
                return RedirectToAction(nameof(SapViaPagoCon));

            return await SapViaPagoInsertaCap(sapViaPago);
        }
        /// <summary>
        /// Actualiza.
        /// </summary>
        [MValidaSeg(nameof(SapViaPagoActualiza))]
        public async Task<IActionResult> SapViaPagoActualizaIni(Int32 indice)
        {
            EV.Accion = MAccionesGen.Actualiza;
            EV.SapViaPago.Indice = indice;
            EV.SapViaPago.Sel = EV.SapViaPago.Pag.Pagina[indice];
            return await SapViaPagoActualizaCap(EV.SapViaPago.Sel);
        }
        /// <summary>
        /// Actualiza.
        /// </summary>
        [ValidateAntiForgeryToken]
        [MValidaSeg(nameof(SapViaPagoActualiza))]
        public async Task<IActionResult> SapViaPagoActualizaCap(ESapViaPago sapViaPago)
        {
            return await SapViaPagoCaptura(sapViaPago);
        }
        /// <summary>
        /// Actualiza.
        /// </summary>
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SapViaPagoActualiza(ESapViaPago sapViaPago)
        {
            if (await NSapViasPago.SapViaPagoActualiza(sapViaPago))
                return RedirectToAction(nameof(SapViaPagoCon));

            return await SapViaPagoActualizaCap(sapViaPago);
        }
        /// <summary>
        /// Elimina.
        /// </summary>
        public async Task<IActionResult> SapViaPagoElimina(Int32 indice)
        {
            await NSapViasPago.SapViaPagoElimina(EV.SapViaPago.Pag.Pagina[indice]);
            return RedirectToAction(nameof(SapViaPagoCon));
        }
        /// <summary>
        /// Exportacion de datos.
        /// </summary>
        public async Task<IActionResult> SapViaPagoExporta()
        {
            EV.SapViaPago.Filtro.ColumnaOrden = EV.SapViaPago.ColOrden;
            EV.SapViaPago.Filtro.Columnas = new Dictionary<String, String>()
                           {
                               { nameof(ESapViaPago.Activo), String.Empty },
                               { nameof(ESapViaPago.SapViaPagoId), String.Empty },
                               { nameof(ESapViaPago.SapViaPagoNombre), String.Empty }
                           };

            String vRutaYNombreArchivo = await NSapViasPago.SapViaPagoExporta(EV.SapViaPago.Filtro);
            EV.SapViaPago.Filtro.Columnas = null;
            if (NSapViasPago.Mensajes.Ok)
                return await MUtilMvc.DescargaArchivo(await Servicios.Archivos.DescargaArchivoTemp(vRutaYNombreArchivo));

            return RedirectToAction(nameof(SapViaPagoCon));
        }
        #endregion

        #region Funciones
        /// <summary>
        /// Captura.
        /// </summary>
        private async Task<IActionResult> SapViaPagoCaptura(ESapViaPago sapViaPago)
        {
            ViewBag.Mensajes = NSapViasPago.Mensajes.Copy();
            ViewBag.EV = EV;

            return await Task.FromResult(ViewCap(nameof(SapViaPagoCaptura), sapViaPago));
        }
        #endregion

        #region Acciones de Paginacion Orden y Filtro
        /// <summary>
        /// Control de paginacion.
        /// </summary>
        [MValidaSeg(nameof(SapViaPagoInicia))]
        public IActionResult SapViaPagoPaginacion(MEDatosPaginador datPag)
        {
            EV.SapViaPago.Pag.DatPag = datPag;
            return RedirectToAction(nameof(SapViaPagoCon));
        }
        /// <summary>
        /// Control de orden.
        /// </summary>
        [MValidaSeg(nameof(SapViaPagoInicia))]
        public IActionResult SapViaPagoOrdena(String orden)
        {
            EV.SapViaPago.ColOrden = orden;
            return RedirectToAction(nameof(SapViaPagoCon));
        }
        /// <summary>
        /// Control de filtro.
        /// </summary>
        [MValidaSeg(nameof(SapViaPagoInicia))]
        public IActionResult SapViaPagoFiltra(ESapViaPagoFiltro filtro)
        {
            EV.SapViaPago.Filtro = filtro;
            return RedirectToAction(nameof(SapViaPagoCon));
        }
        /// <summary>
        /// Limpia filtros.
        /// </summary>
        [MValidaSeg(nameof(SapViaPagoInicia))]
        public IActionResult SapViaPagoLimpiaFiltros()
        {
            EV.SapViaPago.Filtro = new ESapViaPagoFiltro();
            return RedirectToAction(nameof(SapViaPagoCon));
        }
        #endregion

        #endregion
    }
}
