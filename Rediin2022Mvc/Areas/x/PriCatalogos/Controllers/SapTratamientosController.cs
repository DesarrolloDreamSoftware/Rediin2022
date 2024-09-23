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
    public class SapTratamientosController : MControllerMvcPri
    {
        #region Constructores
        /// <summary>
        /// Controlador MVC.
        /// </summary>
        public SapTratamientosController(INSapTratamientos nSapTratamientos)
        {
            NSapTratamientos = nSapTratamientos;
        }
        #endregion

        #region Propiedades
        /// <summary>
        /// Negocio SapTratamientos.
        /// </summary>
        private INSapTratamientos NSapTratamientos { get; set; }
        /// <summary>
        /// Entidad de variables.
        /// </summary>
        private EVSapTratamientos EVSapTratamientos
        {
            get
            {
                if (base.MSesion<EVSapTratamientos>() == null)
                    base.MSesion(new EVSapTratamientos());

                return base.MSesionAuto<EVSapTratamientos>();
            }
        }
        #endregion

        #region SapTratamiento (SapTratamientos)

        #region Acciones
        /// <summary>
        /// Inicia sub funcion.
        /// </summary>
        public IActionResult SapTratamientoInicia()
        {
            //Configuracion de inicio
            if (String.IsNullOrWhiteSpace(EVSapTratamientos.SapTratamientoColOrden))
                EVSapTratamientos.SapTratamientoColOrden = nameof(ESapTratamiento.SapTratamientoId);

            if (EVSapTratamientos.SapTratamientoReglas == null)
                EVSapTratamientos.SapTratamientoReglas = NSapTratamientos.SapTratamientoReglas();

            return RedirectToAction(nameof(SapTratamientoCon));
        }
        /// <summary>
        /// Consulta.
        /// </summary>
        [MValidaSeg(nameof(SapTratamientoInicia))]
        public IActionResult SapTratamientoCon()
        {
            base.MCargaFiltroPagYOrd(EVSapTratamientos.SapTratamientoFiltro,
                                     EVSapTratamientos.SapTratamientoPag,
                                     EVSapTratamientos.SapTratamientoColOrden,
                                     nameof(ESapTratamiento));

            EVSapTratamientos.SapTratamientoPag = NSapTratamientos.SapTratamientoPag(EVSapTratamientos.SapTratamientoFiltro);
            base.MActualizaTamPag(EVSapTratamientos.SapTratamientoPag?.DatPag);

            ViewBag.Mensajes = NSapTratamientos.Mensajes.Copy();
            ViewBag.Reglas = EVSapTratamientos.SapTratamientoReglas;
            ViewBag.DatPag = EVSapTratamientos.SapTratamientoPag?.DatPag;
            ViewBag.Orden = EVSapTratamientos.SapTratamientoColOrden;
            ViewBag.Filtro = EVSapTratamientos.SapTratamientoFiltro;
            ViewBag.Indice = EVSapTratamientos.SapTratamientoIndice;

            return View(nameof(SapTratamientoCon), EVSapTratamientos.SapTratamientoPag?.Pagina);
        }
        /// <summary>
        /// Consulta por id.
        /// </summary>
        public IActionResult SapTratamientoXId(Int32 indice)
        {
            EVSapTratamientos.Accion = MAccionesGen.Consulta;
            EVSapTratamientos.SapTratamientoIndice = indice;
            return SapTratamientoCaptura(EVSapTratamientos.SapTratamientoPag.Pagina[indice]);
        }
        /// <summary>
        /// Inserta.
        /// </summary>
        [MValidaSeg(nameof(SapTratamientoInserta))]
        public IActionResult SapTratamientoInsertaIni()
        {
            EVSapTratamientos.Accion = MAccionesGen.Inserta;
            return SapTratamientoInsertaCap(new ESapTratamiento()
            {
                Activo = true
            });
        }
        /// <summary>
        /// Inserta.
        /// </summary>
        [ValidateAntiForgeryToken]
        [MValidaSeg(nameof(SapTratamientoInserta))]
        public IActionResult SapTratamientoInsertaCap(ESapTratamiento sapTratamiento)
        {
            return SapTratamientoCaptura(sapTratamiento);
        }
        /// <summary>
        /// Inserta.
        /// </summary>
        [ValidateAntiForgeryToken]
        public IActionResult SapTratamientoInserta(ESapTratamiento sapTratamiento)
        {
            if (NSapTratamientos.SapTratamientoInserta(sapTratamiento))
                return RedirectToAction(nameof(SapTratamientoCon));

            return SapTratamientoInsertaCap(sapTratamiento);
        }
        /// <summary>
        /// Actualiza.
        /// </summary>
        [MValidaSeg(nameof(SapTratamientoActualiza))]
        public IActionResult SapTratamientoActualizaIni(Int32 indice)
        {
            EVSapTratamientos.Accion = MAccionesGen.Actualiza;
            EVSapTratamientos.SapTratamientoIndice = indice;
            EVSapTratamientos.SapTratamientoSel = EVSapTratamientos.SapTratamientoPag.Pagina[indice];
            return SapTratamientoActualizaCap(EVSapTratamientos.SapTratamientoSel);
        }
        /// <summary>
        /// Actualiza.
        /// </summary>
        [ValidateAntiForgeryToken]
        [MValidaSeg(nameof(SapTratamientoActualiza))]
        public IActionResult SapTratamientoActualizaCap(ESapTratamiento sapTratamiento)
        {
            return SapTratamientoCaptura(sapTratamiento);
        }
        /// <summary>
        /// Actualiza.
        /// </summary>
        [ValidateAntiForgeryToken]
        public IActionResult SapTratamientoActualiza(ESapTratamiento sapTratamiento)
        {
            if (NSapTratamientos.SapTratamientoActualiza(sapTratamiento))
                return RedirectToAction(nameof(SapTratamientoCon));

            return SapTratamientoActualizaCap(sapTratamiento);
        }
        /// <summary>
        /// Elimina.
        /// </summary>
        public IActionResult SapTratamientoElimina(Int32 indice)
        {
            NSapTratamientos.SapTratamientoElimina(EVSapTratamientos.SapTratamientoPag.Pagina[indice]);
            return RedirectToAction(nameof(SapTratamientoCon));
        }
        /// <summary>
        /// Exportacion de datos.
        /// </summary>
        public async Task<IActionResult> SapTratamientoExporta()
        {
            EVSapTratamientos.SapTratamientoFiltro.ColumnaOrden = EVSapTratamientos.SapTratamientoColOrden;
            EVSapTratamientos.SapTratamientoFiltro.Columnas = new Dictionary<String, String>()
                                   {
                                       { nameof(ESapTratamiento.Activo), String.Empty },
                                       { nameof(ESapTratamiento.SapTratamientoId), String.Empty },
                                       { nameof(ESapTratamiento.SapTratamientoNombre), String.Empty }
                                   };

            MEDatosArchivo vDA = NSapTratamientos.SapTratamientoExporta(EVSapTratamientos.SapTratamientoFiltro);
            EVSapTratamientos.SapTratamientoFiltro.Columnas = null;
            if (NSapTratamientos.Mensajes.Ok)
                return await base.MEnviaArchivoACliente(NSapTratamientos.Mensajes, vDA);

            return RedirectToAction(nameof(SapTratamientoCon));
        }
        #endregion

        #region Funciones
        /// <summary>
        /// Captura.
        /// </summary>
        private IActionResult SapTratamientoCaptura(ESapTratamiento sapTratamiento)
        {
            ViewBag.Mensajes = NSapTratamientos.Mensajes.Copy();
            ViewBag.Accion = EVSapTratamientos.Accion;
            ViewBag.Reglas = EVSapTratamientos.SapTratamientoReglas;

            return ViewCap(nameof(SapTratamientoCaptura), sapTratamiento);
        }
        #endregion

        #region Acciones de Paginacion Orden y Filtro
        /// <summary>
        /// Control de paginacion.
        /// </summary>
        [MValidaSeg(nameof(SapTratamientoInicia))]
        public IActionResult SapTratamientoPaginacion(MEDatosPaginador datPag)
        {
            EVSapTratamientos.SapTratamientoPag.DatPag = datPag;
            return RedirectToAction(nameof(SapTratamientoCon));
        }
        /// <summary>
        /// Control de orden.
        /// </summary>
        [MValidaSeg(nameof(SapTratamientoInicia))]
        public IActionResult SapTratamientoOrdena(String orden)
        {
            EVSapTratamientos.SapTratamientoColOrden = orden;
            return RedirectToAction(nameof(SapTratamientoCon));
        }
        /// <summary>
        /// Control de filtro.
        /// </summary>
        [MValidaSeg(nameof(SapTratamientoInicia))]
        public IActionResult SapTratamientoFiltra(ESapTratamientoFiltro filtro)
        {
            EVSapTratamientos.SapTratamientoFiltro = filtro;
            return RedirectToAction(nameof(SapTratamientoCon));
        }
        /// <summary>
        /// Limpia filtros.
        /// </summary>
        [MValidaSeg(nameof(SapTratamientoInicia))]
        public IActionResult SapTratamientoLimpiaFiltros()
        {
            EVSapTratamientos.SapTratamientoFiltro = new ESapTratamientoFiltro();
            return RedirectToAction(nameof(SapTratamientoCon));
        }
        #endregion

        #endregion
    }
}
