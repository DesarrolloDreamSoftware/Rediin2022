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
    public class SapSociedadesController : MControllerMvcPri
    {
        #region Constructores
        /// <summary>
        /// Controlador MVC.
        /// </summary>
        public SapSociedadesController(INSapSociedades nSapSociedades)
        {
            NSapSociedades = nSapSociedades;
        }
        #endregion

        #region Propiedades
        /// <summary>
        /// Negocio SapSociedades.
        /// </summary>
        private INSapSociedades NSapSociedades { get; set; }
        /// <summary>
        /// Entidad de variables.
        /// </summary>
        private EVSapSociedades EV
        {
            get { return base.MEVCtrl<EVSapSociedades>(); }
        }
        #endregion

        #region SapSociedad (SapSociedades)

        #region Acciones
        /// <summary>
        /// Inicia sub funcion.
        /// </summary>
        public async Task<IActionResult> SapSociedadInicia()
        {
            //Configuracion de inicio
            await Servicios.Gen.InicializaSF(EV.SapSociedad, nameof(ESapSociedad.SapSociedadId),
                async () => await NSapSociedades.SapSociedadReglas());

            return RedirectToAction(nameof(SapSociedadCon));
        }
        /// <summary>
        /// Consulta.
        /// </summary>
        [MValidaSeg(nameof(SapSociedadInicia))]
        public async Task<IActionResult> SapSociedadCon()
        {
            await Servicios.Pag.CargaPagOrdYFil(EV.SapSociedad);
            EV.SapSociedad.Pag = await NSapSociedades.SapSociedadPag(EV.SapSociedad.Filtro);
            await Servicios.Pag.ActTamPag(EV.SapSociedad);

            ViewBag.Mensajes = NSapSociedades.Mensajes;
            ViewBag.EV = EV;

            return View(nameof(SapSociedadCon), EV.SapSociedad.Pag?.Pagina);
        }
        /// <summary>
        /// Consulta por id.
        /// </summary>
        public async Task<IActionResult> SapSociedadXId(Int32 indice)
        {
            EV.Accion = MAccionesGen.Consulta;
            EV.SapSociedad.Indice = indice;
            return await SapSociedadCaptura(EV.SapSociedad.Pag.Pagina[indice]);
        }
        /// <summary>
        /// Inserta.
        /// </summary>
        [MValidaSeg(nameof(SapSociedadInserta))]
        public async Task<IActionResult> SapSociedadInsertaIni()
        {
            EV.Accion = MAccionesGen.Inserta;
            return await SapSociedadInsertaCap(new ESapSociedad()
            {
                Activo = true
            });
        }
        /// <summary>
        /// Inserta.
        /// </summary>
        [ValidateAntiForgeryToken]
        [MValidaSeg(nameof(SapSociedadInserta))]
        public async Task<IActionResult> SapSociedadInsertaCap(ESapSociedad sapSociedad)
        {
            return await SapSociedadCaptura(sapSociedad);
        }
        /// <summary>
        /// Inserta.
        /// </summary>
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SapSociedadInserta(ESapSociedad sapSociedad)
        {
            if (await NSapSociedades.SapSociedadInserta(sapSociedad))
                return RedirectToAction(nameof(SapSociedadCon));

            return await SapSociedadInsertaCap(sapSociedad);
        }
        /// <summary>
        /// Actualiza.
        /// </summary>
        [MValidaSeg(nameof(SapSociedadActualiza))]
        public async Task<IActionResult> SapSociedadActualizaIni(Int32 indice)
        {
            EV.Accion = MAccionesGen.Actualiza;
            EV.SapSociedad.Indice = indice;
            EV.SapSociedad.Sel = EV.SapSociedad.Pag.Pagina[indice];
            return await SapSociedadActualizaCap(EV.SapSociedad.Sel);
        }
        /// <summary>
        /// Actualiza.
        /// </summary>
        [ValidateAntiForgeryToken]
        [MValidaSeg(nameof(SapSociedadActualiza))]
        public async Task<IActionResult> SapSociedadActualizaCap(ESapSociedad sapSociedad)
        {
            return await SapSociedadCaptura(sapSociedad);
        }
        /// <summary>
        /// Actualiza.
        /// </summary>
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SapSociedadActualiza(ESapSociedad sapSociedad)
        {
            if (await NSapSociedades.SapSociedadActualiza(sapSociedad))
                return RedirectToAction(nameof(SapSociedadCon));

            return await SapSociedadActualizaCap(sapSociedad);
        }
        /// <summary>
        /// Elimina.
        /// </summary>
        public async Task<IActionResult> SapSociedadElimina(Int32 indice)
        {
            await NSapSociedades.SapSociedadElimina(EV.SapSociedad.Pag.Pagina[indice]);
            return RedirectToAction(nameof(SapSociedadCon));
        }
        /// <summary>
        /// Exportacion de datos.
        /// </summary>
        public async Task<IActionResult> SapSociedadExporta()
        {
            EV.SapSociedad.Filtro.ColumnaOrden = EV.SapSociedad.ColOrden;
            EV.SapSociedad.Filtro.Columnas = new Dictionary<String, String>()
                              {
                                  { nameof(ESapSociedad.Activo), String.Empty },
                                  { nameof(ESapSociedad.SapSociedadId), String.Empty },
                                  { nameof(ESapSociedad.SapSociedadNombre), String.Empty }
                              };

            String vRutaYNombreArchivo = await NSapSociedades.SapSociedadExporta(EV.SapSociedad.Filtro);
            EV.SapSociedad.Filtro.Columnas = null;
            if (NSapSociedades.Mensajes.Ok)
                return await MUtilMvc.DescargaArchivo(await Servicios.Archivos.DescargaArchivoTemp(vRutaYNombreArchivo));

            return RedirectToAction(nameof(SapSociedadCon));
        }
        #endregion

        #region Funciones
        /// <summary>
        /// Captura.
        /// </summary>
        private async Task<IActionResult> SapSociedadCaptura(ESapSociedad sapSociedad)
        {
            ViewBag.Mensajes = NSapSociedades.Mensajes.Copy();
            ViewBag.EV = EV;

            return await Task.FromResult(ViewCap(nameof(SapSociedadCaptura), sapSociedad));
        }
        #endregion

        #region Acciones de Paginacion Orden y Filtro
        /// <summary>
        /// Control de paginacion.
        /// </summary>
        [MValidaSeg(nameof(SapSociedadInicia))]
        public IActionResult SapSociedadPaginacion(MEDatosPaginador datPag)
        {
            EV.SapSociedad.Pag.DatPag = datPag;
            return RedirectToAction(nameof(SapSociedadCon));
        }
        /// <summary>
        /// Control de orden.
        /// </summary>
        [MValidaSeg(nameof(SapSociedadInicia))]
        public IActionResult SapSociedadOrdena(String orden)
        {
            EV.SapSociedad.ColOrden = orden;
            return RedirectToAction(nameof(SapSociedadCon));
        }
        /// <summary>
        /// Control de filtro.
        /// </summary>
        [MValidaSeg(nameof(SapSociedadInicia))]
        public IActionResult SapSociedadFiltra(ESapSociedadFiltro filtro)
        {
            EV.SapSociedad.Filtro = filtro;
            return RedirectToAction(nameof(SapSociedadCon));
        }
        /// <summary>
        /// Limpia filtros.
        /// </summary>
        [MValidaSeg(nameof(SapSociedadInicia))]
        public IActionResult SapSociedadLimpiaFiltros()
        {
            EV.SapSociedad.Filtro = new ESapSociedadFiltro();
            return RedirectToAction(nameof(SapSociedadCon));
        }
        #endregion

        #endregion
    }
}
