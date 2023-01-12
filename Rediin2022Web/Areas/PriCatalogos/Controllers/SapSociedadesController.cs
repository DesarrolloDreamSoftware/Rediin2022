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
        private EVSapSociedades EVSapSociedades
        {
            get
            {
                if (base.MSesion<EVSapSociedades>() == null)
                    base.MSesion(new EVSapSociedades());

                return base.MSesionAuto<EVSapSociedades>();
            }
        }
        #endregion

        #region SapSociedad (SapSociedades)

        #region Acciones
        /// <summary>
        /// Inicia sub funcion.
        /// </summary>
        public IActionResult SapSociedadInicia()
        {
            //Configuracion de inicio
            if (String.IsNullOrWhiteSpace(EVSapSociedades.SapSociedadColOrden))
                EVSapSociedades.SapSociedadColOrden = nameof(ESapSociedad.SapSociedadId);

            if (EVSapSociedades.SapSociedadReglas == null)
                EVSapSociedades.SapSociedadReglas = NSapSociedades.SapSociedadReglas();

            return RedirectToAction(nameof(SapSociedadCon));
        }
        /// <summary>
        /// Consulta.
        /// </summary>
        [MValidaSeg(nameof(SapSociedadInicia))]
        public IActionResult SapSociedadCon()
        {
            base.MCargaFiltroPagYOrd(EVSapSociedades.SapSociedadFiltro,
                                     EVSapSociedades.SapSociedadPag,
                                     EVSapSociedades.SapSociedadColOrden,
                                     nameof(ESapSociedad));

            EVSapSociedades.SapSociedadPag = NSapSociedades.SapSociedadPag(EVSapSociedades.SapSociedadFiltro);
            base.MActualizaTamPag(EVSapSociedades.SapSociedadPag?.DatPag);

            ViewBag.Mensajes = NSapSociedades.Mensajes.Copy();
            ViewBag.Reglas = EVSapSociedades.SapSociedadReglas;
            ViewBag.DatPag = EVSapSociedades.SapSociedadPag?.DatPag;
            ViewBag.Orden = EVSapSociedades.SapSociedadColOrden;
            ViewBag.Filtro = EVSapSociedades.SapSociedadFiltro;
            ViewBag.Indice = EVSapSociedades.SapSociedadIndice;

            return View(nameof(SapSociedadCon), EVSapSociedades.SapSociedadPag?.Pagina);
        }
        /// <summary>
        /// Consulta por id.
        /// </summary>
        public IActionResult SapSociedadXId(Int32 indice)
        {
            EVSapSociedades.Accion = MAccionesGen.Consulta;
            EVSapSociedades.SapSociedadIndice = indice;
            return SapSociedadCaptura(EVSapSociedades.SapSociedadPag.Pagina[indice]);
        }
        /// <summary>
        /// Inserta.
        /// </summary>
        [MValidaSeg(nameof(SapSociedadInserta))]
        public IActionResult SapSociedadInsertaIni()
        {
            EVSapSociedades.Accion = MAccionesGen.Inserta;
            return SapSociedadInsertaCap(new ESapSociedad()
            {
                Activo = true
            });
        }
        /// <summary>
        /// Inserta.
        /// </summary>
        [ValidateAntiForgeryToken]
        [MValidaSeg(nameof(SapSociedadInserta))]
        public IActionResult SapSociedadInsertaCap(ESapSociedad sapSociedad)
        {
            return SapSociedadCaptura(sapSociedad);
        }
        /// <summary>
        /// Inserta.
        /// </summary>
        [ValidateAntiForgeryToken]
        public IActionResult SapSociedadInserta(ESapSociedad sapSociedad)
        {
            if (NSapSociedades.SapSociedadInserta(sapSociedad))
                return RedirectToAction(nameof(SapSociedadCon));

            return SapSociedadInsertaCap(sapSociedad);
        }
        /// <summary>
        /// Actualiza.
        /// </summary>
        [MValidaSeg(nameof(SapSociedadActualiza))]
        public IActionResult SapSociedadActualizaIni(Int32 indice)
        {
            EVSapSociedades.Accion = MAccionesGen.Actualiza;
            EVSapSociedades.SapSociedadIndice = indice;
            EVSapSociedades.SapSociedadSel = EVSapSociedades.SapSociedadPag.Pagina[indice];
            return SapSociedadActualizaCap(EVSapSociedades.SapSociedadSel);
        }
        /// <summary>
        /// Actualiza.
        /// </summary>
        [ValidateAntiForgeryToken]
        [MValidaSeg(nameof(SapSociedadActualiza))]
        public IActionResult SapSociedadActualizaCap(ESapSociedad sapSociedad)
        {
            return SapSociedadCaptura(sapSociedad);
        }
        /// <summary>
        /// Actualiza.
        /// </summary>
        [ValidateAntiForgeryToken]
        public IActionResult SapSociedadActualiza(ESapSociedad sapSociedad)
        {
            if (NSapSociedades.SapSociedadActualiza(sapSociedad))
                return RedirectToAction(nameof(SapSociedadCon));

            return SapSociedadActualizaCap(sapSociedad);
        }
        /// <summary>
        /// Elimina.
        /// </summary>
        public IActionResult SapSociedadElimina(Int32 indice)
        {
            NSapSociedades.SapSociedadElimina(EVSapSociedades.SapSociedadPag.Pagina[indice]);
            return RedirectToAction(nameof(SapSociedadCon));
        }
        /// <summary>
        /// Exportacion de datos.
        /// </summary>
        public async Task<IActionResult> SapSociedadExporta()
        {
            EVSapSociedades.SapSociedadFiltro.ColumnaOrden = EVSapSociedades.SapSociedadColOrden;
            EVSapSociedades.SapSociedadFiltro.Columnas = new Dictionary<String, String>()
                              {
                                  { nameof(ESapSociedad.Activo), String.Empty },
                                  { nameof(ESapSociedad.SapSociedadId), String.Empty },
                                  { nameof(ESapSociedad.SapSociedadNombre), String.Empty }
                              };

            MEDatosArchivo vDA = NSapSociedades.SapSociedadExporta(EVSapSociedades.SapSociedadFiltro);
            EVSapSociedades.SapSociedadFiltro.Columnas = null;
            if (NSapSociedades.Mensajes.Ok)
                return await base.MEnviaArchivoACliente(NSapSociedades.Mensajes, vDA);

            return RedirectToAction(nameof(SapSociedadCon));
        }
        #endregion

        #region Funciones
        /// <summary>
        /// Captura.
        /// </summary>
        private IActionResult SapSociedadCaptura(ESapSociedad sapSociedad)
        {
            ViewBag.Mensajes = NSapSociedades.Mensajes.Copy();
            ViewBag.Accion = EVSapSociedades.Accion;
            ViewBag.Reglas = EVSapSociedades.SapSociedadReglas;

            return ViewCap(nameof(SapSociedadCaptura), sapSociedad);
        }
        #endregion

        #region Acciones de Paginacion Orden y Filtro
        /// <summary>
        /// Control de paginacion.
        /// </summary>
        [MValidaSeg(nameof(SapSociedadInicia))]
        public IActionResult SapSociedadPaginacion(MEDatosPaginador datPag)
        {
            EVSapSociedades.SapSociedadPag.DatPag = datPag;
            return RedirectToAction(nameof(SapSociedadCon));
        }
        /// <summary>
        /// Control de orden.
        /// </summary>
        [MValidaSeg(nameof(SapSociedadInicia))]
        public IActionResult SapSociedadOrdena(String orden)
        {
            EVSapSociedades.SapSociedadColOrden = orden;
            return RedirectToAction(nameof(SapSociedadCon));
        }
        /// <summary>
        /// Control de filtro.
        /// </summary>
        [MValidaSeg(nameof(SapSociedadInicia))]
        public IActionResult SapSociedadFiltra(ESapSociedadFiltro filtro)
        {
            EVSapSociedades.SapSociedadFiltro = filtro;
            return RedirectToAction(nameof(SapSociedadCon));
        }
        /// <summary>
        /// Limpia filtros.
        /// </summary>
        [MValidaSeg(nameof(SapSociedadInicia))]
        public IActionResult SapSociedadLimpiaFiltros()
        {
            EVSapSociedades.SapSociedadFiltro = new ESapSociedadFiltro();
            return RedirectToAction(nameof(SapSociedadCon));
        }
        #endregion

        #endregion
    }
}
