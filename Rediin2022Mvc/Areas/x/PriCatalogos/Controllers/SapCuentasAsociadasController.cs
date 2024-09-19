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
    public class SapCuentasAsociadasController : MControllerMvcPri
    {
        #region Constructores
        /// <summary>
        /// Controlador MVC.
        /// </summary>
        public SapCuentasAsociadasController(INSapCuentasAsociadas nSapCuentasAsociadas)
        {
            NSapCuentasAsociadas = nSapCuentasAsociadas;
        }
        #endregion

        #region Propiedades
        /// <summary>
        /// Negocio SapCuentasAsociadas.
        /// </summary>
        private INSapCuentasAsociadas NSapCuentasAsociadas { get; set; }
        /// <summary>
        /// Entidad de variables.
        /// </summary>
        private EVSapCuentasAsociadas EVSapCuentasAsociadas
        {
            get
            {
                if (base.MSesion<EVSapCuentasAsociadas>() == null)
                    base.MSesion(new EVSapCuentasAsociadas());

                return base.MSesionAuto<EVSapCuentasAsociadas>();
            }
        }
        #endregion

        #region SapCuentaAsociada (SapCuentasAsociadas)

        #region Acciones
        /// <summary>
        /// Inicia sub funcion.
        /// </summary>
        public IActionResult SapCuentaAsociadaInicia()
        {
            //Configuracion de inicio
            if (String.IsNullOrWhiteSpace(EVSapCuentasAsociadas.SapCuentaAsociadaColOrden))
                EVSapCuentasAsociadas.SapCuentaAsociadaColOrden = nameof(ESapCuentaAsociada.SapCuentaAsociadaId);

            if (EVSapCuentasAsociadas.SapCuentaAsociadaReglas == null)
                EVSapCuentasAsociadas.SapCuentaAsociadaReglas = NSapCuentasAsociadas.SapCuentaAsociadaReglas();

            return RedirectToAction(nameof(SapCuentaAsociadaCon));
        }
        /// <summary>
        /// Consulta.
        /// </summary>
        [MValidaSeg(nameof(SapCuentaAsociadaInicia))]
        public IActionResult SapCuentaAsociadaCon()
        {
            base.MCargaFiltroPagYOrd(EVSapCuentasAsociadas.SapCuentaAsociadaFiltro,
                                     EVSapCuentasAsociadas.SapCuentaAsociadaPag,
                                     EVSapCuentasAsociadas.SapCuentaAsociadaColOrden,
                                     nameof(ESapCuentaAsociada));

            EVSapCuentasAsociadas.SapCuentaAsociadaPag = NSapCuentasAsociadas.SapCuentaAsociadaPag(EVSapCuentasAsociadas.SapCuentaAsociadaFiltro);
            base.MActualizaTamPag(EVSapCuentasAsociadas.SapCuentaAsociadaPag?.DatPag);

            ViewBag.Mensajes = NSapCuentasAsociadas.Mensajes.Copy();
            ViewBag.Reglas = EVSapCuentasAsociadas.SapCuentaAsociadaReglas;
            ViewBag.DatPag = EVSapCuentasAsociadas.SapCuentaAsociadaPag?.DatPag;
            ViewBag.Orden = EVSapCuentasAsociadas.SapCuentaAsociadaColOrden;
            ViewBag.Filtro = EVSapCuentasAsociadas.SapCuentaAsociadaFiltro;
            ViewBag.Indice = EVSapCuentasAsociadas.SapCuentaAsociadaIndice;

            return View(nameof(SapCuentaAsociadaCon), EVSapCuentasAsociadas.SapCuentaAsociadaPag?.Pagina);
        }
        /// <summary>
        /// Consulta por id.
        /// </summary>
        public IActionResult SapCuentaAsociadaXId(Int32 indice)
        {
            EVSapCuentasAsociadas.Accion = MAccionesGen.Consulta;
            EVSapCuentasAsociadas.SapCuentaAsociadaIndice = indice;
            return SapCuentaAsociadaCaptura(EVSapCuentasAsociadas.SapCuentaAsociadaPag.Pagina[indice]);
        }
        /// <summary>
        /// Inserta.
        /// </summary>
        [MValidaSeg(nameof(SapCuentaAsociadaInserta))]
        public IActionResult SapCuentaAsociadaInsertaIni()
        {
            EVSapCuentasAsociadas.Accion = MAccionesGen.Inserta;
            return SapCuentaAsociadaInsertaCap(new ESapCuentaAsociada()
            {
                Activo = true
            });
        }
        /// <summary>
        /// Inserta.
        /// </summary>
        [ValidateAntiForgeryToken]
        [MValidaSeg(nameof(SapCuentaAsociadaInserta))]
        public IActionResult SapCuentaAsociadaInsertaCap(ESapCuentaAsociada sapCuentaAsociada)
        {
            return SapCuentaAsociadaCaptura(sapCuentaAsociada);
        }
        /// <summary>
        /// Inserta.
        /// </summary>
        [ValidateAntiForgeryToken]
        public IActionResult SapCuentaAsociadaInserta(ESapCuentaAsociada sapCuentaAsociada)
        {
            if (NSapCuentasAsociadas.SapCuentaAsociadaInserta(sapCuentaAsociada))
                return RedirectToAction(nameof(SapCuentaAsociadaCon));

            return SapCuentaAsociadaInsertaCap(sapCuentaAsociada);
        }
        /// <summary>
        /// Actualiza.
        /// </summary>
        [MValidaSeg(nameof(SapCuentaAsociadaActualiza))]
        public IActionResult SapCuentaAsociadaActualizaIni(Int32 indice)
        {
            EVSapCuentasAsociadas.Accion = MAccionesGen.Actualiza;
            EVSapCuentasAsociadas.SapCuentaAsociadaIndice = indice;
            EVSapCuentasAsociadas.SapCuentaAsociadaSel = EVSapCuentasAsociadas.SapCuentaAsociadaPag.Pagina[indice];
            return SapCuentaAsociadaActualizaCap(EVSapCuentasAsociadas.SapCuentaAsociadaSel);
        }
        /// <summary>
        /// Actualiza.
        /// </summary>
        [ValidateAntiForgeryToken]
        [MValidaSeg(nameof(SapCuentaAsociadaActualiza))]
        public IActionResult SapCuentaAsociadaActualizaCap(ESapCuentaAsociada sapCuentaAsociada)
        {
            return SapCuentaAsociadaCaptura(sapCuentaAsociada);
        }
        /// <summary>
        /// Actualiza.
        /// </summary>
        [ValidateAntiForgeryToken]
        public IActionResult SapCuentaAsociadaActualiza(ESapCuentaAsociada sapCuentaAsociada)
        {
            if (NSapCuentasAsociadas.SapCuentaAsociadaActualiza(sapCuentaAsociada))
                return RedirectToAction(nameof(SapCuentaAsociadaCon));

            return SapCuentaAsociadaActualizaCap(sapCuentaAsociada);
        }
        /// <summary>
        /// Elimina.
        /// </summary>
        public IActionResult SapCuentaAsociadaElimina(Int32 indice)
        {
            NSapCuentasAsociadas.SapCuentaAsociadaElimina(EVSapCuentasAsociadas.SapCuentaAsociadaPag.Pagina[indice]);
            return RedirectToAction(nameof(SapCuentaAsociadaCon));
        }
        /// <summary>
        /// Exportacion de datos.
        /// </summary>
        public async Task<IActionResult> SapCuentaAsociadaExporta()
        {
            EVSapCuentasAsociadas.SapCuentaAsociadaFiltro.ColumnaOrden = EVSapCuentasAsociadas.SapCuentaAsociadaColOrden;
            EVSapCuentasAsociadas.SapCuentaAsociadaFiltro.Columnas = new Dictionary<String, String>()
                                          {
                                              { nameof(ESapCuentaAsociada.Activo), String.Empty },
                                              { nameof(ESapCuentaAsociada.SapCuentaAsociadaId), String.Empty },
                                              { nameof(ESapCuentaAsociada.SapCuentaAsociadaNombre), String.Empty }
                                          };

            MEDatosArchivo vDA = NSapCuentasAsociadas.SapCuentaAsociadaExporta(EVSapCuentasAsociadas.SapCuentaAsociadaFiltro);
            EVSapCuentasAsociadas.SapCuentaAsociadaFiltro.Columnas = null;
            if (NSapCuentasAsociadas.Mensajes.Ok)
                return await base.MEnviaArchivoACliente(NSapCuentasAsociadas.Mensajes, vDA);

            return RedirectToAction(nameof(SapCuentaAsociadaCon));
        }
        #endregion

        #region Funciones
        /// <summary>
        /// Captura.
        /// </summary>
        private IActionResult SapCuentaAsociadaCaptura(ESapCuentaAsociada sapCuentaAsociada)
        {
            ViewBag.Mensajes = NSapCuentasAsociadas.Mensajes.Copy();
            ViewBag.Accion = EVSapCuentasAsociadas.Accion;
            ViewBag.Reglas = EVSapCuentasAsociadas.SapCuentaAsociadaReglas;

            return ViewCap(nameof(SapCuentaAsociadaCaptura), sapCuentaAsociada);
        }
        #endregion

        #region Acciones de Paginacion Orden y Filtro
        /// <summary>
        /// Control de paginacion.
        /// </summary>
        [MValidaSeg(nameof(SapCuentaAsociadaInicia))]
        public IActionResult SapCuentaAsociadaPaginacion(MEDatosPaginador datPag)
        {
            EVSapCuentasAsociadas.SapCuentaAsociadaPag.DatPag = datPag;
            return RedirectToAction(nameof(SapCuentaAsociadaCon));
        }
        /// <summary>
        /// Control de orden.
        /// </summary>
        [MValidaSeg(nameof(SapCuentaAsociadaInicia))]
        public IActionResult SapCuentaAsociadaOrdena(String orden)
        {
            EVSapCuentasAsociadas.SapCuentaAsociadaColOrden = orden;
            return RedirectToAction(nameof(SapCuentaAsociadaCon));
        }
        /// <summary>
        /// Control de filtro.
        /// </summary>
        [MValidaSeg(nameof(SapCuentaAsociadaInicia))]
        public IActionResult SapCuentaAsociadaFiltra(ESapCuentaAsociadaFiltro filtro)
        {
            EVSapCuentasAsociadas.SapCuentaAsociadaFiltro = filtro;
            return RedirectToAction(nameof(SapCuentaAsociadaCon));
        }
        /// <summary>
        /// Limpia filtros.
        /// </summary>
        [MValidaSeg(nameof(SapCuentaAsociadaInicia))]
        public IActionResult SapCuentaAsociadaLimpiaFiltros()
        {
            EVSapCuentasAsociadas.SapCuentaAsociadaFiltro = new ESapCuentaAsociadaFiltro();
            return RedirectToAction(nameof(SapCuentaAsociadaCon));
        }
        #endregion

        #endregion
    }
}
