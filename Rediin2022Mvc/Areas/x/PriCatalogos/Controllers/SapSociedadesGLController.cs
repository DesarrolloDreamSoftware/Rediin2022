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
        private EVSapSociedadesGL EVSapSociedadesGL
        {
            get
            {
                if (base.MSesion<EVSapSociedadesGL>() == null)
                    base.MSesion(new EVSapSociedadesGL());

                return base.MSesionAuto<EVSapSociedadesGL>();
            }
        }
        #endregion

        #region SapSociedadGL (SapSociedadesGL)

        #region Acciones
        /// <summary>
        /// Inicia sub funcion.
        /// </summary>
        public IActionResult SapSociedadGLInicia()
        {
            //Configuracion de inicio
            if (String.IsNullOrWhiteSpace(EVSapSociedadesGL.SapSociedadGLColOrden))
                EVSapSociedadesGL.SapSociedadGLColOrden = nameof(ESapSociedadGL.SapSociedadGLId);

            if (EVSapSociedadesGL.SapSociedadGLReglas == null)
                EVSapSociedadesGL.SapSociedadGLReglas = NSapSociedadesGL.SapSociedadGLReglas();

            return RedirectToAction(nameof(SapSociedadGLCon));
        }
        /// <summary>
        /// Consulta.
        /// </summary>
        [MValidaSeg(nameof(SapSociedadGLInicia))]
        public IActionResult SapSociedadGLCon()
        {
            base.MCargaFiltroPagYOrd(EVSapSociedadesGL.SapSociedadGLFiltro,
                                     EVSapSociedadesGL.SapSociedadGLPag,
                                     EVSapSociedadesGL.SapSociedadGLColOrden,
                                     nameof(ESapSociedadGL));

            EVSapSociedadesGL.SapSociedadGLPag = NSapSociedadesGL.SapSociedadGLPag(EVSapSociedadesGL.SapSociedadGLFiltro);
            base.MActualizaTamPag(EVSapSociedadesGL.SapSociedadGLPag?.DatPag);

            ViewBag.Mensajes = NSapSociedadesGL.Mensajes.Copy();
            ViewBag.Reglas = EVSapSociedadesGL.SapSociedadGLReglas;
            ViewBag.DatPag = EVSapSociedadesGL.SapSociedadGLPag?.DatPag;
            ViewBag.Orden = EVSapSociedadesGL.SapSociedadGLColOrden;
            ViewBag.Filtro = EVSapSociedadesGL.SapSociedadGLFiltro;
            ViewBag.Indice = EVSapSociedadesGL.SapSociedadGLIndice;

            return View(nameof(SapSociedadGLCon), EVSapSociedadesGL.SapSociedadGLPag?.Pagina);
        }
        /// <summary>
        /// Consulta por id.
        /// </summary>
        public IActionResult SapSociedadGLXId(Int32 indice)
        {
            EVSapSociedadesGL.Accion = MAccionesGen.Consulta;
            EVSapSociedadesGL.SapSociedadGLIndice = indice;
            return SapSociedadGLCaptura(EVSapSociedadesGL.SapSociedadGLPag.Pagina[indice]);
        }
        /// <summary>
        /// Inserta.
        /// </summary>
        [MValidaSeg(nameof(SapSociedadGLInserta))]
        public IActionResult SapSociedadGLInsertaIni()
        {
            EVSapSociedadesGL.Accion = MAccionesGen.Inserta;
            return SapSociedadGLInsertaCap(new ESapSociedadGL()
            {
                Activo = true
            });
        }
        /// <summary>
        /// Inserta.
        /// </summary>
        [ValidateAntiForgeryToken]
        [MValidaSeg(nameof(SapSociedadGLInserta))]
        public IActionResult SapSociedadGLInsertaCap(ESapSociedadGL sapSociedadGL)
        {
            return SapSociedadGLCaptura(sapSociedadGL);
        }
        /// <summary>
        /// Inserta.
        /// </summary>
        [ValidateAntiForgeryToken]
        public IActionResult SapSociedadGLInserta(ESapSociedadGL sapSociedadGL)
        {
            if (NSapSociedadesGL.SapSociedadGLInserta(sapSociedadGL))
                return RedirectToAction(nameof(SapSociedadGLCon));

            return SapSociedadGLInsertaCap(sapSociedadGL);
        }
        /// <summary>
        /// Actualiza.
        /// </summary>
        [MValidaSeg(nameof(SapSociedadGLActualiza))]
        public IActionResult SapSociedadGLActualizaIni(Int32 indice)
        {
            EVSapSociedadesGL.Accion = MAccionesGen.Actualiza;
            EVSapSociedadesGL.SapSociedadGLIndice = indice;
            EVSapSociedadesGL.SapSociedadGLSel = EVSapSociedadesGL.SapSociedadGLPag.Pagina[indice];
            return SapSociedadGLActualizaCap(EVSapSociedadesGL.SapSociedadGLSel);
        }
        /// <summary>
        /// Actualiza.
        /// </summary>
        [ValidateAntiForgeryToken]
        [MValidaSeg(nameof(SapSociedadGLActualiza))]
        public IActionResult SapSociedadGLActualizaCap(ESapSociedadGL sapSociedadGL)
        {
            return SapSociedadGLCaptura(sapSociedadGL);
        }
        /// <summary>
        /// Actualiza.
        /// </summary>
        [ValidateAntiForgeryToken]
        public IActionResult SapSociedadGLActualiza(ESapSociedadGL sapSociedadGL)
        {
            if (NSapSociedadesGL.SapSociedadGLActualiza(sapSociedadGL))
                return RedirectToAction(nameof(SapSociedadGLCon));

            return SapSociedadGLActualizaCap(sapSociedadGL);
        }
        /// <summary>
        /// Elimina.
        /// </summary>
        public IActionResult SapSociedadGLElimina(Int32 indice)
        {
            NSapSociedadesGL.SapSociedadGLElimina(EVSapSociedadesGL.SapSociedadGLPag.Pagina[indice]);
            return RedirectToAction(nameof(SapSociedadGLCon));
        }
        /// <summary>
        /// Exportacion de datos.
        /// </summary>
        public async Task<IActionResult> SapSociedadGLExporta()
        {
            EVSapSociedadesGL.SapSociedadGLFiltro.ColumnaOrden = EVSapSociedadesGL.SapSociedadGLColOrden;
            EVSapSociedadesGL.SapSociedadGLFiltro.Columnas = new Dictionary<String, String>()
                                  {
                                      { nameof(ESapSociedadGL.Activo), String.Empty },
                                      { nameof(ESapSociedadGL.SapSociedadGLId), String.Empty },
                                      { nameof(ESapSociedadGL.SapSociedadGLNombre), String.Empty }
                                  };

            MEDatosArchivo vDA = NSapSociedadesGL.SapSociedadGLExporta(EVSapSociedadesGL.SapSociedadGLFiltro);
            EVSapSociedadesGL.SapSociedadGLFiltro.Columnas = null;
            if (NSapSociedadesGL.Mensajes.Ok)
                return await base.MEnviaArchivoACliente(NSapSociedadesGL.Mensajes, vDA);

            return RedirectToAction(nameof(SapSociedadGLCon));
        }
        #endregion

        #region Funciones
        /// <summary>
        /// Captura.
        /// </summary>
        private IActionResult SapSociedadGLCaptura(ESapSociedadGL sapSociedadGL)
        {
            ViewBag.Mensajes = NSapSociedadesGL.Mensajes.Copy();
            ViewBag.Accion = EVSapSociedadesGL.Accion;
            ViewBag.Reglas = EVSapSociedadesGL.SapSociedadGLReglas;

            return ViewCap(nameof(SapSociedadGLCaptura), sapSociedadGL);
        }
        #endregion

        #region Acciones de Paginacion Orden y Filtro
        /// <summary>
        /// Control de paginacion.
        /// </summary>
        [MValidaSeg(nameof(SapSociedadGLInicia))]
        public IActionResult SapSociedadGLPaginacion(MEDatosPaginador datPag)
        {
            EVSapSociedadesGL.SapSociedadGLPag.DatPag = datPag;
            return RedirectToAction(nameof(SapSociedadGLCon));
        }
        /// <summary>
        /// Control de orden.
        /// </summary>
        [MValidaSeg(nameof(SapSociedadGLInicia))]
        public IActionResult SapSociedadGLOrdena(String orden)
        {
            EVSapSociedadesGL.SapSociedadGLColOrden = orden;
            return RedirectToAction(nameof(SapSociedadGLCon));
        }
        /// <summary>
        /// Control de filtro.
        /// </summary>
        [MValidaSeg(nameof(SapSociedadGLInicia))]
        public IActionResult SapSociedadGLFiltra(ESapSociedadGLFiltro filtro)
        {
            EVSapSociedadesGL.SapSociedadGLFiltro = filtro;
            return RedirectToAction(nameof(SapSociedadGLCon));
        }
        /// <summary>
        /// Limpia filtros.
        /// </summary>
        [MValidaSeg(nameof(SapSociedadGLInicia))]
        public IActionResult SapSociedadGLLimpiaFiltros()
        {
            EVSapSociedadesGL.SapSociedadGLFiltro = new ESapSociedadGLFiltro();
            return RedirectToAction(nameof(SapSociedadGLCon));
        }
        #endregion

        #endregion
    }
}
