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
    public class SapOrganizacionesCompraController : MControllerMvcPri
    {
        #region Constructores
        /// <summary>
        /// Controlador MVC.
        /// </summary>
        public SapOrganizacionesCompraController(INSapOrganizacionesCompra nSapOrganizacionesCompra)
        {
            NSapOrganizacionesCompra = nSapOrganizacionesCompra;
        }
        #endregion

        #region Propiedades
        /// <summary>
        /// Negocio SapOrganizacionesCompra.
        /// </summary>
        private INSapOrganizacionesCompra NSapOrganizacionesCompra { get; set; }
        /// <summary>
        /// Entidad de variables.
        /// </summary>
        private EVSapOrganizacionesCompra EV
        {
            get { return base.MEVCtrl<EVSapOrganizacionesCompra>(); }
        }
        #endregion

        #region SapOrganizacionCompra (SapOrganizacionesCompra)

        #region Acciones
        /// <summary>
        /// Inicia sub funcion.
        /// </summary>
        public async Task<IActionResult> SapOrganizacionCompraInicia()
        {
            //Configuracion de inicio
            await Servicios.Gen.InicializaSF(EV.SapOrganizacionCompra, nameof(ESapOrganizacionCompra.SapOrganizacionCompraId),
                async () => await NSapOrganizacionesCompra.SapOrganizacionCompraReglas());

            return RedirectToAction(nameof(SapOrganizacionCompraCon));
        }
        /// <summary>
        /// Consulta.
        /// </summary>
        [MValidaSeg(nameof(SapOrganizacionCompraInicia))]
        public async Task<IActionResult> SapOrganizacionCompraCon()
        {
            await Servicios.Pag.CargaPagOrdYFil(EV.SapOrganizacionCompra);
            EV.SapOrganizacionCompra.Pag = await NSapOrganizacionesCompra.SapOrganizacionCompraPag(EV.SapOrganizacionCompra.Filtro);
            await Servicios.Pag.ActTamPag(EV.SapOrganizacionCompra);

            ViewBag.Mensajes = NSapOrganizacionesCompra.Mensajes;
            ViewBag.EV = EV;

            return View(nameof(SapOrganizacionCompraCon), EV.SapOrganizacionCompra.Pag?.Pagina);
        }
        /// <summary>
        /// Consulta por id.
        /// </summary>
        public async Task<IActionResult> SapOrganizacionCompraXId(Int32 indice)
        {
            EV.Accion = MAccionesGen.Consulta;
            EV.SapOrganizacionCompra.Indice = indice;
            return await SapOrganizacionCompraCaptura(EV.SapOrganizacionCompra.Pag.Pagina[indice]);
        }
        /// <summary>
        /// Inserta.
        /// </summary>
        [MValidaSeg(nameof(SapOrganizacionCompraInserta))]
        public async Task<IActionResult> SapOrganizacionCompraInsertaIni()
        {
            EV.Accion = MAccionesGen.Inserta;
            return await SapOrganizacionCompraInsertaCap(new ESapOrganizacionCompra()
            {
                Activo = true
            });
        }
        /// <summary>
        /// Inserta.
        /// </summary>
        [ValidateAntiForgeryToken]
        [MValidaSeg(nameof(SapOrganizacionCompraInserta))]
        public async Task<IActionResult> SapOrganizacionCompraInsertaCap(ESapOrganizacionCompra sapOrganizacionCompra)
        {
            return await SapOrganizacionCompraCaptura(sapOrganizacionCompra);
        }
        /// <summary>
        /// Inserta.
        /// </summary>
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SapOrganizacionCompraInserta(ESapOrganizacionCompra sapOrganizacionCompra)
        {
            if (await NSapOrganizacionesCompra.SapOrganizacionCompraInserta(sapOrganizacionCompra))
                return RedirectToAction(nameof(SapOrganizacionCompraCon));

            return await SapOrganizacionCompraInsertaCap(sapOrganizacionCompra);
        }
        /// <summary>
        /// Actualiza.
        /// </summary>
        [MValidaSeg(nameof(SapOrganizacionCompraActualiza))]
        public async Task<IActionResult> SapOrganizacionCompraActualizaIni(Int32 indice)
        {
            EV.Accion = MAccionesGen.Actualiza;
            EV.SapOrganizacionCompra.Indice = indice;
            EV.SapOrganizacionCompra.Sel = EV.SapOrganizacionCompra.Pag.Pagina[indice];
            return await SapOrganizacionCompraActualizaCap(EV.SapOrganizacionCompra.Sel);
        }
        /// <summary>
        /// Actualiza.
        /// </summary>
        [ValidateAntiForgeryToken]
        [MValidaSeg(nameof(SapOrganizacionCompraActualiza))]
        public async Task<IActionResult> SapOrganizacionCompraActualizaCap(ESapOrganizacionCompra sapOrganizacionCompra)
        {
            return await SapOrganizacionCompraCaptura(sapOrganizacionCompra);
        }
        /// <summary>
        /// Actualiza.
        /// </summary>
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SapOrganizacionCompraActualiza(ESapOrganizacionCompra sapOrganizacionCompra)
        {
            if (await NSapOrganizacionesCompra.SapOrganizacionCompraActualiza(sapOrganizacionCompra))
                return RedirectToAction(nameof(SapOrganizacionCompraCon));

            return await SapOrganizacionCompraActualizaCap(sapOrganizacionCompra);
        }
        /// <summary>
        /// Elimina.
        /// </summary>
        public async Task<IActionResult> SapOrganizacionCompraElimina(Int32 indice)
        {
            await NSapOrganizacionesCompra.SapOrganizacionCompraElimina(EV.SapOrganizacionCompra.Pag.Pagina[indice]);
            return RedirectToAction(nameof(SapOrganizacionCompraCon));
        }
        /// <summary>
        /// Exportacion de datos.
        /// </summary>
        public async Task<IActionResult> SapOrganizacionCompraExporta()
        {
            EV.SapOrganizacionCompra.Filtro.ColumnaOrden = EV.SapOrganizacionCompra.ColOrden;
            EV.SapOrganizacionCompra.Filtro.Columnas = new Dictionary<String, String>()
                                                  {
                                                      { nameof(ESapOrganizacionCompra.Activo), String.Empty },
                                                      { nameof(ESapOrganizacionCompra.SapOrganizacionCompraId), String.Empty },
                                                      { nameof(ESapOrganizacionCompra.SapOrganizacionCompraNombre), String.Empty }
                                                  };

            String vRutaYNombreArchivo = await NSapOrganizacionesCompra.SapOrganizacionCompraExporta(EV.SapOrganizacionCompra.Filtro);
            EV.SapOrganizacionCompra.Filtro.Columnas = null;
            if (NSapOrganizacionesCompra.Mensajes.Ok)
                return await MUtilMvc.DescargaArchivo(await Servicios.Archivos.DescargaArchivoTemp(vRutaYNombreArchivo));

            return RedirectToAction(nameof(SapOrganizacionCompraCon));
        }
        #endregion

        #region Funciones
        /// <summary>
        /// Captura.
        /// </summary>
        private async Task<IActionResult> SapOrganizacionCompraCaptura(ESapOrganizacionCompra sapOrganizacionCompra)
        {
            ViewBag.Mensajes = NSapOrganizacionesCompra.Mensajes.Copy();
            ViewBag.EV = EV;

            return await Task.FromResult(ViewCap(nameof(SapOrganizacionCompraCaptura), sapOrganizacionCompra));
        }
        #endregion

        #region Acciones de Paginacion Orden y Filtro
        /// <summary>
        /// Control de paginacion.
        /// </summary>
        [MValidaSeg(nameof(SapOrganizacionCompraInicia))]
        public IActionResult SapOrganizacionCompraPaginacion(MEDatosPaginador datPag)
        {
            EV.SapOrganizacionCompra.Pag.DatPag = datPag;
            return RedirectToAction(nameof(SapOrganizacionCompraCon));
        }
        /// <summary>
        /// Control de orden.
        /// </summary>
        [MValidaSeg(nameof(SapOrganizacionCompraInicia))]
        public IActionResult SapOrganizacionCompraOrdena(String orden)
        {
            EV.SapOrganizacionCompra.ColOrden = orden;
            return RedirectToAction(nameof(SapOrganizacionCompraCon));
        }
        /// <summary>
        /// Control de filtro.
        /// </summary>
        [MValidaSeg(nameof(SapOrganizacionCompraInicia))]
        public IActionResult SapOrganizacionCompraFiltra(ESapOrganizacionCompraFiltro filtro)
        {
            EV.SapOrganizacionCompra.Filtro = filtro;
            return RedirectToAction(nameof(SapOrganizacionCompraCon));
        }
        /// <summary>
        /// Limpia filtros.
        /// </summary>
        [MValidaSeg(nameof(SapOrganizacionCompraInicia))]
        public IActionResult SapOrganizacionCompraLimpiaFiltros()
        {
            EV.SapOrganizacionCompra.Filtro = new ESapOrganizacionCompraFiltro();
            return RedirectToAction(nameof(SapOrganizacionCompraCon));
        }
        #endregion

        #endregion
    }
}
