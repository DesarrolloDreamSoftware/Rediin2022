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
        private EVSapCondicionesPago EVSapCondicionesPago
        {
            get
            {
                if (base.MSesion<EVSapCondicionesPago>() == null)
                    base.MSesion(new EVSapCondicionesPago());

                return base.MSesionAuto<EVSapCondicionesPago>();
            }
        }
        #endregion

        #region SapCondicionPago (SapCondicionesPago)

        #region Acciones
        /// <summary>
        /// Inicia sub funcion.
        /// </summary>
        public IActionResult SapCondicionPagoInicia()
        {
            //Configuracion de inicio
            if (String.IsNullOrWhiteSpace(EVSapCondicionesPago.SapCondicionPagoColOrden))
                EVSapCondicionesPago.SapCondicionPagoColOrden = nameof(ESapCondicionPago.SapCondicionPagoId);

            if (EVSapCondicionesPago.SapCondicionPagoReglas == null)
            {
                EVSapCondicionesPago.SapCondicionPagoReglas = NSapCondicionesPago.SapCondicionPagoReglas();
                base.MMensajesTemp = NSapCondicionesPago.Mensajes.ToString();
            }

            return RedirectToAction(nameof(SapCondicionPagoCon));
        }
        /// <summary>
        /// Consulta.
        /// </summary>
        [MValidaSeg(nameof(SapCondicionPagoInicia))]
        public IActionResult SapCondicionPagoCon()
        {
            base.MCargaFiltroPagYOrd(EVSapCondicionesPago.SapCondicionPagoFiltro,
                                     EVSapCondicionesPago.SapCondicionPagoPag,
                                     EVSapCondicionesPago.SapCondicionPagoColOrden,
                                     nameof(ESapCondicionPago));

            EVSapCondicionesPago.SapCondicionPagoPag = NSapCondicionesPago.SapCondicionPagoPag(EVSapCondicionesPago.SapCondicionPagoFiltro);
            base.MActualizaTamPag(EVSapCondicionesPago.SapCondicionPagoPag?.DatPag);

            ViewBag.Mensajes = base.MObtenMensajes(NSapCondicionesPago.Mensajes);
            ViewBag.Reglas = EVSapCondicionesPago.SapCondicionPagoReglas;
            ViewBag.DatPag = EVSapCondicionesPago.SapCondicionPagoPag?.DatPag;
            ViewBag.Orden = EVSapCondicionesPago.SapCondicionPagoColOrden;
            ViewBag.Filtro = EVSapCondicionesPago.SapCondicionPagoFiltro;
            ViewBag.Indice = EVSapCondicionesPago.SapCondicionPagoIndice;

            return View(nameof(SapCondicionPagoCon), EVSapCondicionesPago.SapCondicionPagoPag?.Pagina);
        }
        /// <summary>
        /// Consulta por id.
        /// </summary>
        public IActionResult SapCondicionPagoXId(Int32 indice)
        {
            EVSapCondicionesPago.Accion = MAccionesGen.Consulta;
            EVSapCondicionesPago.SapCondicionPagoIndice = indice;
            return SapCondicionPagoCaptura(EVSapCondicionesPago.SapCondicionPagoPag.Pagina[indice]);
        }
        /// <summary>
        /// Inserta.
        /// </summary>
        [MValidaSeg(nameof(SapCondicionPagoInserta))]
        public IActionResult SapCondicionPagoInsertaIni()
        {
            EVSapCondicionesPago.Accion = MAccionesGen.Inserta;
            return SapCondicionPagoInsertaCap(new ESapCondicionPago()
            {
                Activo = true
            });
        }
        /// <summary>
        /// Inserta.
        /// </summary>
        [ValidateAntiForgeryToken]
        [MValidaSeg(nameof(SapCondicionPagoInserta))]
        public IActionResult SapCondicionPagoInsertaCap(ESapCondicionPago sapCondicionPago)
        {
            return SapCondicionPagoCaptura(sapCondicionPago);
        }
        /// <summary>
        /// Inserta.
        /// </summary>
        [ValidateAntiForgeryToken]
        public IActionResult SapCondicionPagoInserta(ESapCondicionPago sapCondicionPago)
        {
            if (NSapCondicionesPago.SapCondicionPagoInserta(sapCondicionPago))
                return RedirectToAction(nameof(SapCondicionPagoCon));

            return SapCondicionPagoInsertaCap(sapCondicionPago);
        }
        /// <summary>
        /// Actualiza.
        /// </summary>
        [MValidaSeg(nameof(SapCondicionPagoActualiza))]
        public IActionResult SapCondicionPagoActualizaIni(Int32 indice)
        {
            EVSapCondicionesPago.Accion = MAccionesGen.Actualiza;
            EVSapCondicionesPago.SapCondicionPagoIndice = indice;
            EVSapCondicionesPago.SapCondicionPagoSel = EVSapCondicionesPago.SapCondicionPagoPag.Pagina[indice];
            return SapCondicionPagoActualizaCap(EVSapCondicionesPago.SapCondicionPagoSel);
        }
        /// <summary>
        /// Actualiza.
        /// </summary>
        [ValidateAntiForgeryToken]
        [MValidaSeg(nameof(SapCondicionPagoActualiza))]
        public IActionResult SapCondicionPagoActualizaCap(ESapCondicionPago sapCondicionPago)
        {
            return SapCondicionPagoCaptura(sapCondicionPago);
        }
        /// <summary>
        /// Actualiza.
        /// </summary>
        [ValidateAntiForgeryToken]
        public IActionResult SapCondicionPagoActualiza(ESapCondicionPago sapCondicionPago)
        {
            if (NSapCondicionesPago.SapCondicionPagoActualiza(sapCondicionPago))
                return RedirectToAction(nameof(SapCondicionPagoCon));

            return SapCondicionPagoActualizaCap(sapCondicionPago);
        }
        /// <summary>
        /// Elimina.
        /// </summary>
        public IActionResult SapCondicionPagoElimina(Int32 indice)
        {
            NSapCondicionesPago.SapCondicionPagoElimina(EVSapCondicionesPago.SapCondicionPagoPag.Pagina[indice]);
            base.MMensajesTemp = NSapCondicionesPago.Mensajes.ToString();
            return RedirectToAction(nameof(SapCondicionPagoCon));
        }
        /// <summary>
        /// Exportacion de datos.
        /// </summary>
        public async Task<IActionResult> SapCondicionPagoExporta()
        {
            EVSapCondicionesPago.SapCondicionPagoFiltro.ColumnaOrden = EVSapCondicionesPago.SapCondicionPagoColOrden;
            EVSapCondicionesPago.SapCondicionPagoFiltro.Columnas = new Dictionary<String, String>()
                                        {
                                            { nameof(ESapCondicionPago.Activo), String.Empty },
                                            { nameof(ESapCondicionPago.SapCondicionPagoId), String.Empty },
                                            { nameof(ESapCondicionPago.SapCondicionPagoNombre), String.Empty }
                                        };

            MEDatosArchivo vDA = NSapCondicionesPago.SapCondicionPagoExporta(EVSapCondicionesPago.SapCondicionPagoFiltro);
            EVSapCondicionesPago.SapCondicionPagoFiltro.Columnas = null;
            if (NSapCondicionesPago.Mensajes.Ok)
                return await base.MEnviaArchivoACliente(NSapCondicionesPago.Mensajes, vDA);

            base.MMensajesTemp = NSapCondicionesPago.Mensajes.ToString();
            return RedirectToAction(nameof(SapCondicionPagoCon));
        }
        #endregion

        #region Funciones
        /// <summary>
        /// Captura.
        /// </summary>
        private IActionResult SapCondicionPagoCaptura(ESapCondicionPago sapCondicionPago)
        {
            ViewBag.Mensajes = base.MObtenMensajes(NSapCondicionesPago.Mensajes);
            ViewBag.Accion = EVSapCondicionesPago.Accion;
            ViewBag.Reglas = EVSapCondicionesPago.SapCondicionPagoReglas;

            return ViewCap(nameof(SapCondicionPagoCaptura), sapCondicionPago);
        }
        #endregion

        #region Acciones de Paginacion Orden y Filtro
        /// <summary>
        /// Control de paginacion.
        /// </summary>
        [MValidaSeg(nameof(SapCondicionPagoInicia))]
        public IActionResult SapCondicionPagoPaginacion(MEDatosPaginador datPag)
        {
            EVSapCondicionesPago.SapCondicionPagoPag.DatPag = datPag;
            return RedirectToAction(nameof(SapCondicionPagoCon));
        }
        /// <summary>
        /// Control de orden.
        /// </summary>
        [MValidaSeg(nameof(SapCondicionPagoInicia))]
        public IActionResult SapCondicionPagoOrdena(String orden)
        {
            EVSapCondicionesPago.SapCondicionPagoColOrden = orden;
            return RedirectToAction(nameof(SapCondicionPagoCon));
        }
        /// <summary>
        /// Control de filtro.
        /// </summary>
        [MValidaSeg(nameof(SapCondicionPagoInicia))]
        public IActionResult SapCondicionPagoFiltra(ESapCondicionPagoFiltro filtro)
        {
            EVSapCondicionesPago.SapCondicionPagoFiltro = filtro;
            return RedirectToAction(nameof(SapCondicionPagoCon));
        }
        /// <summary>
        /// Limpia filtros.
        /// </summary>
        [MValidaSeg(nameof(SapCondicionPagoInicia))]
        public IActionResult SapCondicionPagoLimpiaFiltros()
        {
            EVSapCondicionesPago.SapCondicionPagoFiltro = new ESapCondicionPagoFiltro();
            return RedirectToAction(nameof(SapCondicionPagoCon));
        }
        #endregion

        #endregion
    }
}
