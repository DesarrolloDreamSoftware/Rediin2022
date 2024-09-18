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
        private EVSapViasPago EVSapViasPago
        {
            get
            {
                if (base.MSesion<EVSapViasPago>() == null)
                    base.MSesion(new EVSapViasPago());

                return base.MSesionAuto<EVSapViasPago>();
            }
        }
        #endregion

        #region SapViaPago (SapViasPago)

        #region Acciones
        /// <summary>
        /// Inicia sub funcion.
        /// </summary>
        public IActionResult SapViaPagoInicia()
        {
            //Configuracion de inicio
            if (String.IsNullOrWhiteSpace(EVSapViasPago.SapViaPagoColOrden))
                EVSapViasPago.SapViaPagoColOrden = nameof(ESapViaPago.SapViaPagoId);

            if (EVSapViasPago.SapViaPagoReglas == null)
                EVSapViasPago.SapViaPagoReglas = NSapViasPago.SapViaPagoReglas();

            return RedirectToAction(nameof(SapViaPagoCon));
        }
        /// <summary>
        /// Consulta.
        /// </summary>
        [MValidaSeg(nameof(SapViaPagoInicia))]
        public IActionResult SapViaPagoCon()
        {
            base.MCargaFiltroPagYOrd(EVSapViasPago.SapViaPagoFiltro,
                                     EVSapViasPago.SapViaPagoPag,
                                     EVSapViasPago.SapViaPagoColOrden,
                                     nameof(ESapViaPago));

            EVSapViasPago.SapViaPagoPag = NSapViasPago.SapViaPagoPag(EVSapViasPago.SapViaPagoFiltro);
            base.MActualizaTamPag(EVSapViasPago.SapViaPagoPag?.DatPag);

            ViewBag.Mensajes = NSapViasPago.Mensajes.Copy();
            ViewBag.Reglas = EVSapViasPago.SapViaPagoReglas;
            ViewBag.DatPag = EVSapViasPago.SapViaPagoPag?.DatPag;
            ViewBag.Orden = EVSapViasPago.SapViaPagoColOrden;
            ViewBag.Filtro = EVSapViasPago.SapViaPagoFiltro;
            ViewBag.Indice = EVSapViasPago.SapViaPagoIndice;

            return View(nameof(SapViaPagoCon), EVSapViasPago.SapViaPagoPag?.Pagina);
        }
        /// <summary>
        /// Consulta por id.
        /// </summary>
        public IActionResult SapViaPagoXId(Int32 indice)
        {
            EVSapViasPago.Accion = MAccionesGen.Consulta;
            EVSapViasPago.SapViaPagoIndice = indice;
            return SapViaPagoCaptura(EVSapViasPago.SapViaPagoPag.Pagina[indice]);
        }
        /// <summary>
        /// Inserta.
        /// </summary>
        [MValidaSeg(nameof(SapViaPagoInserta))]
        public IActionResult SapViaPagoInsertaIni()
        {
            EVSapViasPago.Accion = MAccionesGen.Inserta;
            return SapViaPagoInsertaCap(new ESapViaPago()
            {
                Activo = true
            });
        }
        /// <summary>
        /// Inserta.
        /// </summary>
        [ValidateAntiForgeryToken]
        [MValidaSeg(nameof(SapViaPagoInserta))]
        public IActionResult SapViaPagoInsertaCap(ESapViaPago sapViaPago)
        {
            return SapViaPagoCaptura(sapViaPago);
        }
        /// <summary>
        /// Inserta.
        /// </summary>
        [ValidateAntiForgeryToken]
        public IActionResult SapViaPagoInserta(ESapViaPago sapViaPago)
        {
            if (NSapViasPago.SapViaPagoInserta(sapViaPago))
                return RedirectToAction(nameof(SapViaPagoCon));

            return SapViaPagoInsertaCap(sapViaPago);
        }
        /// <summary>
        /// Actualiza.
        /// </summary>
        [MValidaSeg(nameof(SapViaPagoActualiza))]
        public IActionResult SapViaPagoActualizaIni(Int32 indice)
        {
            EVSapViasPago.Accion = MAccionesGen.Actualiza;
            EVSapViasPago.SapViaPagoIndice = indice;
            EVSapViasPago.SapViaPagoSel = EVSapViasPago.SapViaPagoPag.Pagina[indice];
            return SapViaPagoActualizaCap(EVSapViasPago.SapViaPagoSel);
        }
        /// <summary>
        /// Actualiza.
        /// </summary>
        [ValidateAntiForgeryToken]
        [MValidaSeg(nameof(SapViaPagoActualiza))]
        public IActionResult SapViaPagoActualizaCap(ESapViaPago sapViaPago)
        {
            return SapViaPagoCaptura(sapViaPago);
        }
        /// <summary>
        /// Actualiza.
        /// </summary>
        [ValidateAntiForgeryToken]
        public IActionResult SapViaPagoActualiza(ESapViaPago sapViaPago)
        {
            if (NSapViasPago.SapViaPagoActualiza(sapViaPago))
                return RedirectToAction(nameof(SapViaPagoCon));

            return SapViaPagoActualizaCap(sapViaPago);
        }
        /// <summary>
        /// Elimina.
        /// </summary>
        public IActionResult SapViaPagoElimina(Int32 indice)
        {
            NSapViasPago.SapViaPagoElimina(EVSapViasPago.SapViaPagoPag.Pagina[indice]);
            return RedirectToAction(nameof(SapViaPagoCon));
        }
        /// <summary>
        /// Exportacion de datos.
        /// </summary>
        public async Task<IActionResult> SapViaPagoExporta()
        {
            EVSapViasPago.SapViaPagoFiltro.ColumnaOrden = EVSapViasPago.SapViaPagoColOrden;
            EVSapViasPago.SapViaPagoFiltro.Columnas = new Dictionary<String, String>()
                           {
                               { nameof(ESapViaPago.Activo), String.Empty },
                               { nameof(ESapViaPago.SapViaPagoId), String.Empty },
                               { nameof(ESapViaPago.SapViaPagoNombre), String.Empty }
                           };

            MEDatosArchivo vDA = NSapViasPago.SapViaPagoExporta(EVSapViasPago.SapViaPagoFiltro);
            EVSapViasPago.SapViaPagoFiltro.Columnas = null;
            if (NSapViasPago.Mensajes.Ok)
                return await base.MEnviaArchivoACliente(NSapViasPago.Mensajes, vDA);

            return RedirectToAction(nameof(SapViaPagoCon));
        }
        #endregion

        #region Funciones
        /// <summary>
        /// Captura.
        /// </summary>
        private IActionResult SapViaPagoCaptura(ESapViaPago sapViaPago)
        {
            ViewBag.Mensajes = NSapViasPago.Mensajes.Copy();
            ViewBag.Accion = EVSapViasPago.Accion;
            ViewBag.Reglas = EVSapViasPago.SapViaPagoReglas;

            return ViewCap(nameof(SapViaPagoCaptura), sapViaPago);
        }
        #endregion

        #region Acciones de Paginacion Orden y Filtro
        /// <summary>
        /// Control de paginacion.
        /// </summary>
        [MValidaSeg(nameof(SapViaPagoInicia))]
        public IActionResult SapViaPagoPaginacion(MEDatosPaginador datPag)
        {
            EVSapViasPago.SapViaPagoPag.DatPag = datPag;
            return RedirectToAction(nameof(SapViaPagoCon));
        }
        /// <summary>
        /// Control de orden.
        /// </summary>
        [MValidaSeg(nameof(SapViaPagoInicia))]
        public IActionResult SapViaPagoOrdena(String orden)
        {
            EVSapViasPago.SapViaPagoColOrden = orden;
            return RedirectToAction(nameof(SapViaPagoCon));
        }
        /// <summary>
        /// Control de filtro.
        /// </summary>
        [MValidaSeg(nameof(SapViaPagoInicia))]
        public IActionResult SapViaPagoFiltra(ESapViaPagoFiltro filtro)
        {
            EVSapViasPago.SapViaPagoFiltro = filtro;
            return RedirectToAction(nameof(SapViaPagoCon));
        }
        /// <summary>
        /// Limpia filtros.
        /// </summary>
        [MValidaSeg(nameof(SapViaPagoInicia))]
        public IActionResult SapViaPagoLimpiaFiltros()
        {
            EVSapViasPago.SapViaPagoFiltro = new ESapViaPagoFiltro();
            return RedirectToAction(nameof(SapViaPagoCon));
        }
        #endregion

        #endregion
    }
}
