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
    public class SapSociedadesGLController : MControllerMvcPri
    {
        #region Constructores
        /// <summary>
        /// Controlador MVC.
        /// </summary>
        public SapSociedadesGLController(INSapSociedadesGL nSapSociedadesGL)
        {
            NSapSociedadesGL = nSapSociedadesGL;
        }
        #endregion

        #region Propiedades
        /// <summary>
        /// Negocio SapSociedadesGL.
        /// </summary>
        private INSapSociedadesGL NSapSociedadesGL { get; set; }
        /// <summary>
        /// Entidad de variables.
        /// </summary>
        private EVSapSociedadesGL EV
        {
            get { return base.MEVCtrl<EVSapSociedadesGL>(); }
        }
        #endregion

        #region SapSociedadGL (SapSociedadesGL)

        #region Acciones
        /// <summary>
        /// Inicia sub funcion.
        /// </summary>
        public async Task<IActionResult> SapSociedadGLInicia()
        {
            //Configuracion de inicio
            await Servicios.Gen.InicializaSF(EV.SapSociedadGL, nameof(ESapSociedadGL.SapSociedadGLId),
                async () => await NSapSociedadesGL.SapSociedadGLReglas());

            return RedirectToAction(nameof(SapSociedadGLCon));
        }
        /// <summary>
        /// Consulta.
        /// </summary>
        [MValidaSeg(nameof(SapSociedadGLInicia))]
        public async Task<IActionResult> SapSociedadGLCon()
        {
            await Servicios.Pag.CargaPagOrdYFil(EV.SapSociedadGL);
            EV.SapSociedadGL.Pag = await NSapSociedadesGL.SapSociedadGLPag(EV.SapSociedadGL.Filtro);
            await Servicios.Pag.ActTamPag(EV.SapSociedadGL);

            ViewBag.Mensajes = NSapSociedadesGL.Mensajes;
            ViewBag.EV = EV;

            return View(nameof(SapSociedadGLCon), EV.SapSociedadGL.Pag?.Pagina);
        }
        /// <summary>
        /// Consulta por id.
        /// </summary>
        public async Task<IActionResult> SapSociedadGLXId(Int32 indice)
        {
            EV.Accion = MAccionesGen.Consulta;
            EV.SapSociedadGL.Indice = indice;
            return await SapSociedadGLCaptura(EV.SapSociedadGL.Pag.Pagina[indice]);
        }
        /// <summary>
        /// Inserta.
        /// </summary>
        [MValidaSeg(nameof(SapSociedadGLInserta))]
        public async Task<IActionResult> SapSociedadGLInsertaIni()
        {
            EV.Accion = MAccionesGen.Inserta;
            return await SapSociedadGLInsertaCap(new ESapSociedadGL()
            {
                Activo = true
            });
        }
        /// <summary>
        /// Inserta.
        /// </summary>
        [ValidateAntiForgeryToken]
        [MValidaSeg(nameof(SapSociedadGLInserta))]
        public async Task<IActionResult> SapSociedadGLInsertaCap(ESapSociedadGL sapSociedadGL)
        {
            return await SapSociedadGLCaptura(sapSociedadGL);
        }
        /// <summary>
        /// Inserta.
        /// </summary>
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SapSociedadGLInserta(ESapSociedadGL sapSociedadGL)
        {
            if (await NSapSociedadesGL.SapSociedadGLInserta(sapSociedadGL))
                return RedirectToAction(nameof(SapSociedadGLCon));

            return await SapSociedadGLInsertaCap(sapSociedadGL);
        }
        /// <summary>
        /// Actualiza.
        /// </summary>
        [MValidaSeg(nameof(SapSociedadGLActualiza))]
        public async Task<IActionResult> SapSociedadGLActualizaIni(Int32 indice)
        {
            EV.Accion = MAccionesGen.Actualiza;
            EV.SapSociedadGL.Indice = indice;
            EV.SapSociedadGL.Sel = EV.SapSociedadGL.Pag.Pagina[indice];
            return await SapSociedadGLActualizaCap(EV.SapSociedadGL.Sel);
        }
        /// <summary>
        /// Actualiza.
        /// </summary>
        [ValidateAntiForgeryToken]
        [MValidaSeg(nameof(SapSociedadGLActualiza))]
        public async Task<IActionResult> SapSociedadGLActualizaCap(ESapSociedadGL sapSociedadGL)
        {
            return await SapSociedadGLCaptura(sapSociedadGL);
        }
        /// <summary>
        /// Actualiza.
        /// </summary>
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SapSociedadGLActualiza(ESapSociedadGL sapSociedadGL)
        {
            if (await NSapSociedadesGL.SapSociedadGLActualiza(sapSociedadGL))
                return RedirectToAction(nameof(SapSociedadGLCon));

            return await SapSociedadGLActualizaCap(sapSociedadGL);
        }
        /// <summary>
        /// Elimina.
        /// </summary>
        public async Task<IActionResult> SapSociedadGLElimina(Int32 indice)
        {
            await NSapSociedadesGL.SapSociedadGLElimina(EV.SapSociedadGL.Pag.Pagina[indice]);
            return RedirectToAction(nameof(SapSociedadGLCon));
        }
        /// <summary>
        /// Exportacion de datos.
        /// </summary>
        public async Task<IActionResult> SapSociedadGLExporta()
        {
            EV.SapSociedadGL.Filtro.ColumnaOrden = EV.SapSociedadGL.ColOrden;
            EV.SapSociedadGL.Filtro.Columnas = new Dictionary<String, String>()
                                  {
                                      { nameof(ESapSociedadGL.Activo), String.Empty },
                                      { nameof(ESapSociedadGL.SapSociedadGLId), String.Empty },
                                      { nameof(ESapSociedadGL.SapSociedadGLNombre), String.Empty }
                                  };

            String vRutaYNombreArchivo = await NSapSociedadesGL.SapSociedadGLExporta(EV.SapSociedadGL.Filtro);
            EV.SapSociedadGL.Filtro.Columnas = null;
            if (NSapSociedadesGL.Mensajes.Ok)
                return await MUtilMvc.DescargaArchivo(await Servicios.Archivos.DescargaArchivoTemp(vRutaYNombreArchivo));

            return RedirectToAction(nameof(SapSociedadGLCon));
        }
        #endregion

        #region Funciones
        /// <summary>
        /// Captura.
        /// </summary>
        private async Task<IActionResult> SapSociedadGLCaptura(ESapSociedadGL sapSociedadGL)
        {
            ViewBag.Mensajes = NSapSociedadesGL.Mensajes.Copy();
            ViewBag.EV = EV;

            return await Task.FromResult(ViewCap(nameof(SapSociedadGLCaptura), sapSociedadGL));
        }
        #endregion

        #region Acciones de Paginacion Orden y Filtro
        /// <summary>
        /// Control de paginacion.
        /// </summary>
        [MValidaSeg(nameof(SapSociedadGLInicia))]
        public IActionResult SapSociedadGLPaginacion(MEDatosPaginador datPag)
        {
            EV.SapSociedadGL.Pag.DatPag = datPag;
            return RedirectToAction(nameof(SapSociedadGLCon));
        }
        /// <summary>
        /// Control de orden.
        /// </summary>
        [MValidaSeg(nameof(SapSociedadGLInicia))]
        public IActionResult SapSociedadGLOrdena(String orden)
        {
            EV.SapSociedadGL.ColOrden = orden;
            return RedirectToAction(nameof(SapSociedadGLCon));
        }
        /// <summary>
        /// Control de filtro.
        /// </summary>
        [MValidaSeg(nameof(SapSociedadGLInicia))]
        public IActionResult SapSociedadGLFiltra(ESapSociedadGLFiltro filtro)
        {
            EV.SapSociedadGL.Filtro = filtro;
            return RedirectToAction(nameof(SapSociedadGLCon));
        }
        /// <summary>
        /// Limpia filtros.
        /// </summary>
        [MValidaSeg(nameof(SapSociedadGLInicia))]
        public IActionResult SapSociedadGLLimpiaFiltros()
        {
            EV.SapSociedadGL.Filtro = new ESapSociedadGLFiltro();
            return RedirectToAction(nameof(SapSociedadGLCon));
        }
        #endregion

        #endregion
    }
}
