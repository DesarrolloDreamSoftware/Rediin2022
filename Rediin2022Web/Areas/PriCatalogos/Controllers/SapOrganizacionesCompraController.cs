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
        private EVSapOrganizacionesCompra EVSapOrganizacionesCompra
        {
            get
            {
                if (base.MSesion<EVSapOrganizacionesCompra>() == null)
                    base.MSesion(new EVSapOrganizacionesCompra());

                return base.MSesionAuto<EVSapOrganizacionesCompra>();
            }
        }
        #endregion

        #region SapOrganizacionCompra (SapOrganizacionesCompra)

        #region Acciones
        /// <summary>
        /// Inicia sub funcion.
        /// </summary>
        public IActionResult SapOrganizacionCompraInicia()
        {
            //Configuracion de inicio
            if (String.IsNullOrWhiteSpace(EVSapOrganizacionesCompra.SapOrganizacionCompraColOrden))
                EVSapOrganizacionesCompra.SapOrganizacionCompraColOrden = nameof(ESapOrganizacionCompra.SapOrganizacionCompraId);

            if (EVSapOrganizacionesCompra.SapOrganizacionCompraReglas == null)
                EVSapOrganizacionesCompra.SapOrganizacionCompraReglas = NSapOrganizacionesCompra.SapOrganizacionCompraReglas();

            return RedirectToAction(nameof(SapOrganizacionCompraCon));
        }
        /// <summary>
        /// Consulta.
        /// </summary>
        [MValidaSeg(nameof(SapOrganizacionCompraInicia))]
        public IActionResult SapOrganizacionCompraCon()
        {
            base.MCargaFiltroPagYOrd(EVSapOrganizacionesCompra.SapOrganizacionCompraFiltro,
                                     EVSapOrganizacionesCompra.SapOrganizacionCompraPag,
                                     EVSapOrganizacionesCompra.SapOrganizacionCompraColOrden,
                                     nameof(ESapOrganizacionCompra));

            EVSapOrganizacionesCompra.SapOrganizacionCompraPag = NSapOrganizacionesCompra.SapOrganizacionCompraPag(EVSapOrganizacionesCompra.SapOrganizacionCompraFiltro);
            base.MActualizaTamPag(EVSapOrganizacionesCompra.SapOrganizacionCompraPag?.DatPag);

            ViewBag.Mensajes = NSapOrganizacionesCompra.Mensajes.Copy();
            ViewBag.Reglas = EVSapOrganizacionesCompra.SapOrganizacionCompraReglas;
            ViewBag.DatPag = EVSapOrganizacionesCompra.SapOrganizacionCompraPag?.DatPag;
            ViewBag.Orden = EVSapOrganizacionesCompra.SapOrganizacionCompraColOrden;
            ViewBag.Filtro = EVSapOrganizacionesCompra.SapOrganizacionCompraFiltro;
            ViewBag.Indice = EVSapOrganizacionesCompra.SapOrganizacionCompraIndice;

            return View(nameof(SapOrganizacionCompraCon), EVSapOrganizacionesCompra.SapOrganizacionCompraPag?.Pagina);
        }
        /// <summary>
        /// Consulta por id.
        /// </summary>
        public IActionResult SapOrganizacionCompraXId(Int32 indice)
        {
            EVSapOrganizacionesCompra.Accion = MAccionesGen.Consulta;
            EVSapOrganizacionesCompra.SapOrganizacionCompraIndice = indice;
            return SapOrganizacionCompraCaptura(EVSapOrganizacionesCompra.SapOrganizacionCompraPag.Pagina[indice]);
        }
        /// <summary>
        /// Inserta.
        /// </summary>
        [MValidaSeg(nameof(SapOrganizacionCompraInserta))]
        public IActionResult SapOrganizacionCompraInsertaIni()
        {
            EVSapOrganizacionesCompra.Accion = MAccionesGen.Inserta;
            return SapOrganizacionCompraInsertaCap(new ESapOrganizacionCompra()
            {
                Activo = true
            });
        }
        /// <summary>
        /// Inserta.
        /// </summary>
        [ValidateAntiForgeryToken]
        [MValidaSeg(nameof(SapOrganizacionCompraInserta))]
        public IActionResult SapOrganizacionCompraInsertaCap(ESapOrganizacionCompra sapOrganizacionCompra)
        {
            return SapOrganizacionCompraCaptura(sapOrganizacionCompra);
        }
        /// <summary>
        /// Inserta.
        /// </summary>
        [ValidateAntiForgeryToken]
        public IActionResult SapOrganizacionCompraInserta(ESapOrganizacionCompra sapOrganizacionCompra)
        {
            if (NSapOrganizacionesCompra.SapOrganizacionCompraInserta(sapOrganizacionCompra))
                return RedirectToAction(nameof(SapOrganizacionCompraCon));

            return SapOrganizacionCompraInsertaCap(sapOrganizacionCompra);
        }
        /// <summary>
        /// Actualiza.
        /// </summary>
        [MValidaSeg(nameof(SapOrganizacionCompraActualiza))]
        public IActionResult SapOrganizacionCompraActualizaIni(Int32 indice)
        {
            EVSapOrganizacionesCompra.Accion = MAccionesGen.Actualiza;
            EVSapOrganizacionesCompra.SapOrganizacionCompraIndice = indice;
            EVSapOrganizacionesCompra.SapOrganizacionCompraSel = EVSapOrganizacionesCompra.SapOrganizacionCompraPag.Pagina[indice];
            return SapOrganizacionCompraActualizaCap(EVSapOrganizacionesCompra.SapOrganizacionCompraSel);
        }
        /// <summary>
        /// Actualiza.
        /// </summary>
        [ValidateAntiForgeryToken]
        [MValidaSeg(nameof(SapOrganizacionCompraActualiza))]
        public IActionResult SapOrganizacionCompraActualizaCap(ESapOrganizacionCompra sapOrganizacionCompra)
        {
            return SapOrganizacionCompraCaptura(sapOrganizacionCompra);
        }
        /// <summary>
        /// Actualiza.
        /// </summary>
        [ValidateAntiForgeryToken]
        public IActionResult SapOrganizacionCompraActualiza(ESapOrganizacionCompra sapOrganizacionCompra)
        {
            if (NSapOrganizacionesCompra.SapOrganizacionCompraActualiza(sapOrganizacionCompra))
                return RedirectToAction(nameof(SapOrganizacionCompraCon));

            return SapOrganizacionCompraActualizaCap(sapOrganizacionCompra);
        }
        /// <summary>
        /// Elimina.
        /// </summary>
        public IActionResult SapOrganizacionCompraElimina(Int32 indice)
        {
            NSapOrganizacionesCompra.SapOrganizacionCompraElimina(EVSapOrganizacionesCompra.SapOrganizacionCompraPag.Pagina[indice]);
            return RedirectToAction(nameof(SapOrganizacionCompraCon));
        }
        /// <summary>
        /// Exportacion de datos.
        /// </summary>
        public async Task<IActionResult> SapOrganizacionCompraExporta()
        {
            EVSapOrganizacionesCompra.SapOrganizacionCompraFiltro.ColumnaOrden = EVSapOrganizacionesCompra.SapOrganizacionCompraColOrden;
            EVSapOrganizacionesCompra.SapOrganizacionCompraFiltro.Columnas = new Dictionary<String, String>()
                                                  {
                                                      { nameof(ESapOrganizacionCompra.Activo), String.Empty },
                                                      { nameof(ESapOrganizacionCompra.SapOrganizacionCompraId), String.Empty },
                                                      { nameof(ESapOrganizacionCompra.SapOrganizacionCompraNombre), String.Empty }
                                                  };

            MEDatosArchivo vDA = NSapOrganizacionesCompra.SapOrganizacionCompraExporta(EVSapOrganizacionesCompra.SapOrganizacionCompraFiltro);
            EVSapOrganizacionesCompra.SapOrganizacionCompraFiltro.Columnas = null;
            if (NSapOrganizacionesCompra.Mensajes.Ok)
                return await base.MEnviaArchivoACliente(NSapOrganizacionesCompra.Mensajes, vDA);

            return RedirectToAction(nameof(SapOrganizacionCompraCon));
        }
        #endregion

        #region Funciones
        /// <summary>
        /// Captura.
        /// </summary>
        private IActionResult SapOrganizacionCompraCaptura(ESapOrganizacionCompra sapOrganizacionCompra)
        {
            ViewBag.Mensajes = NSapOrganizacionesCompra.Mensajes.Copy();
            ViewBag.Accion = EVSapOrganizacionesCompra.Accion;
            ViewBag.Reglas = EVSapOrganizacionesCompra.SapOrganizacionCompraReglas;

            return ViewCap(nameof(SapOrganizacionCompraCaptura), sapOrganizacionCompra);
        }
        #endregion

        #region Acciones de Paginacion Orden y Filtro
        /// <summary>
        /// Control de paginacion.
        /// </summary>
        [MValidaSeg(nameof(SapOrganizacionCompraInicia))]
        public IActionResult SapOrganizacionCompraPaginacion(MEDatosPaginador datPag)
        {
            EVSapOrganizacionesCompra.SapOrganizacionCompraPag.DatPag = datPag;
            return RedirectToAction(nameof(SapOrganizacionCompraCon));
        }
        /// <summary>
        /// Control de orden.
        /// </summary>
        [MValidaSeg(nameof(SapOrganizacionCompraInicia))]
        public IActionResult SapOrganizacionCompraOrdena(String orden)
        {
            EVSapOrganizacionesCompra.SapOrganizacionCompraColOrden = orden;
            return RedirectToAction(nameof(SapOrganizacionCompraCon));
        }
        /// <summary>
        /// Control de filtro.
        /// </summary>
        [MValidaSeg(nameof(SapOrganizacionCompraInicia))]
        public IActionResult SapOrganizacionCompraFiltra(ESapOrganizacionCompraFiltro filtro)
        {
            EVSapOrganizacionesCompra.SapOrganizacionCompraFiltro = filtro;
            return RedirectToAction(nameof(SapOrganizacionCompraCon));
        }
        /// <summary>
        /// Limpia filtros.
        /// </summary>
        [MValidaSeg(nameof(SapOrganizacionCompraInicia))]
        public IActionResult SapOrganizacionCompraLimpiaFiltros()
        {
            EVSapOrganizacionesCompra.SapOrganizacionCompraFiltro = new ESapOrganizacionCompraFiltro();
            return RedirectToAction(nameof(SapOrganizacionCompraCon));
        }
        #endregion

        #endregion
    }
}
