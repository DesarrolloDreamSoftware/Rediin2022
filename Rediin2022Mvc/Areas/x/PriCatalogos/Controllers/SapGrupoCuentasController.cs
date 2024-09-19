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
    public class SapGrupoCuentasController : MControllerMvcPri
    {
        #region Constructores
        /// <summary>
        /// Controlador MVC.
        /// </summary>
        public SapGrupoCuentasController(INSapGrupoCuentas nSapGrupoCuentas)
        {
            NSapGrupoCuentas = nSapGrupoCuentas;
        }
        #endregion

        #region Propiedades
        /// <summary>
        /// Negocio SapGrupoCuentas.
        /// </summary>
        private INSapGrupoCuentas NSapGrupoCuentas { get; set; }
        /// <summary>
        /// Entidad de variables.
        /// </summary>
        private EVSapGrupoCuentas EVSapGrupoCuentas
        {
            get
            {
                if (base.MSesion<EVSapGrupoCuentas>() == null)
                    base.MSesion(new EVSapGrupoCuentas());

                return base.MSesionAuto<EVSapGrupoCuentas>();
            }
        }
        #endregion

        #region SapGrupoCuenta (SapGrupoCuentas)

        #region Acciones
        /// <summary>
        /// Inicia sub funcion.
        /// </summary>
        public IActionResult SapGrupoCuentaInicia()
        {
            //Configuracion de inicio
            if (String.IsNullOrWhiteSpace(EVSapGrupoCuentas.SapGrupoCuentaColOrden))
                EVSapGrupoCuentas.SapGrupoCuentaColOrden = nameof(ESapGrupoCuenta.SapGrupoCuentaId);

            if (EVSapGrupoCuentas.SapGrupoCuentaReglas == null)
                EVSapGrupoCuentas.SapGrupoCuentaReglas = NSapGrupoCuentas.SapGrupoCuentaReglas();

            return RedirectToAction(nameof(SapGrupoCuentaCon));
        }
        /// <summary>
        /// Consulta.
        /// </summary>
        [MValidaSeg(nameof(SapGrupoCuentaInicia))]
        public IActionResult SapGrupoCuentaCon()
        {
            base.MCargaFiltroPagYOrd(EVSapGrupoCuentas.SapGrupoCuentaFiltro,
                                     EVSapGrupoCuentas.SapGrupoCuentaPag,
                                     EVSapGrupoCuentas.SapGrupoCuentaColOrden,
                                     nameof(ESapGrupoCuenta));

            EVSapGrupoCuentas.SapGrupoCuentaPag = NSapGrupoCuentas.SapGrupoCuentaPag(EVSapGrupoCuentas.SapGrupoCuentaFiltro);
            base.MActualizaTamPag(EVSapGrupoCuentas.SapGrupoCuentaPag?.DatPag);

            ViewBag.Mensajes = NSapGrupoCuentas.Mensajes.Copy();
            ViewBag.Reglas = EVSapGrupoCuentas.SapGrupoCuentaReglas;
            ViewBag.DatPag = EVSapGrupoCuentas.SapGrupoCuentaPag?.DatPag;
            ViewBag.Orden = EVSapGrupoCuentas.SapGrupoCuentaColOrden;
            ViewBag.Filtro = EVSapGrupoCuentas.SapGrupoCuentaFiltro;
            ViewBag.Indice = EVSapGrupoCuentas.SapGrupoCuentaIndice;

            return View(nameof(SapGrupoCuentaCon), EVSapGrupoCuentas.SapGrupoCuentaPag?.Pagina);
        }
        /// <summary>
        /// Consulta por id.
        /// </summary>
        public IActionResult SapGrupoCuentaXId(Int32 indice)
        {
            EVSapGrupoCuentas.Accion = MAccionesGen.Consulta;
            EVSapGrupoCuentas.SapGrupoCuentaIndice = indice;
            return SapGrupoCuentaCaptura(EVSapGrupoCuentas.SapGrupoCuentaPag.Pagina[indice]);
        }
        /// <summary>
        /// Inserta.
        /// </summary>
        [MValidaSeg(nameof(SapGrupoCuentaInserta))]
        public IActionResult SapGrupoCuentaInsertaIni()
        {
            EVSapGrupoCuentas.Accion = MAccionesGen.Inserta;
            return SapGrupoCuentaInsertaCap(new ESapGrupoCuenta()
            {
                Activo = true
            });
        }
        /// <summary>
        /// Inserta.
        /// </summary>
        [ValidateAntiForgeryToken]
        [MValidaSeg(nameof(SapGrupoCuentaInserta))]
        public IActionResult SapGrupoCuentaInsertaCap(ESapGrupoCuenta sapGrupoCuenta)
        {
            return SapGrupoCuentaCaptura(sapGrupoCuenta);
        }
        /// <summary>
        /// Inserta.
        /// </summary>
        [ValidateAntiForgeryToken]
        public IActionResult SapGrupoCuentaInserta(ESapGrupoCuenta sapGrupoCuenta)
        {
            if (NSapGrupoCuentas.SapGrupoCuentaInserta(sapGrupoCuenta))
                return RedirectToAction(nameof(SapGrupoCuentaCon));

            return SapGrupoCuentaInsertaCap(sapGrupoCuenta);
        }
        /// <summary>
        /// Actualiza.
        /// </summary>
        [MValidaSeg(nameof(SapGrupoCuentaActualiza))]
        public IActionResult SapGrupoCuentaActualizaIni(Int32 indice)
        {
            EVSapGrupoCuentas.Accion = MAccionesGen.Actualiza;
            EVSapGrupoCuentas.SapGrupoCuentaIndice = indice;
            EVSapGrupoCuentas.SapGrupoCuentaSel = EVSapGrupoCuentas.SapGrupoCuentaPag.Pagina[indice];
            return SapGrupoCuentaActualizaCap(EVSapGrupoCuentas.SapGrupoCuentaSel);
        }
        /// <summary>
        /// Actualiza.
        /// </summary>
        [ValidateAntiForgeryToken]
        [MValidaSeg(nameof(SapGrupoCuentaActualiza))]
        public IActionResult SapGrupoCuentaActualizaCap(ESapGrupoCuenta sapGrupoCuenta)
        {
            return SapGrupoCuentaCaptura(sapGrupoCuenta);
        }
        /// <summary>
        /// Actualiza.
        /// </summary>
        [ValidateAntiForgeryToken]
        public IActionResult SapGrupoCuentaActualiza(ESapGrupoCuenta sapGrupoCuenta)
        {
            if (NSapGrupoCuentas.SapGrupoCuentaActualiza(sapGrupoCuenta))
                return RedirectToAction(nameof(SapGrupoCuentaCon));

            return SapGrupoCuentaActualizaCap(sapGrupoCuenta);
        }
        /// <summary>
        /// Elimina.
        /// </summary>
        public IActionResult SapGrupoCuentaElimina(Int32 indice)
        {
            NSapGrupoCuentas.SapGrupoCuentaElimina(EVSapGrupoCuentas.SapGrupoCuentaPag.Pagina[indice]);
            return RedirectToAction(nameof(SapGrupoCuentaCon));
        }
        /// <summary>
        /// Exportacion de datos.
        /// </summary>
        public async Task<IActionResult> SapGrupoCuentaExporta()
        {
            EVSapGrupoCuentas.SapGrupoCuentaFiltro.ColumnaOrden = EVSapGrupoCuentas.SapGrupoCuentaColOrden;
            EVSapGrupoCuentas.SapGrupoCuentaFiltro.Columnas = new Dictionary<String, String>()
                                   {
                                       { nameof(ESapGrupoCuenta.Activo), String.Empty },
                                       { nameof(ESapGrupoCuenta.SapGrupoCuentaId), String.Empty },
                                       { nameof(ESapGrupoCuenta.SapGrupoCuentaNombre), String.Empty }
                                   };

            MEDatosArchivo vDA = NSapGrupoCuentas.SapGrupoCuentaExporta(EVSapGrupoCuentas.SapGrupoCuentaFiltro);
            EVSapGrupoCuentas.SapGrupoCuentaFiltro.Columnas = null;
            if (NSapGrupoCuentas.Mensajes.Ok)
                return await base.MEnviaArchivoACliente(NSapGrupoCuentas.Mensajes, vDA);

            return RedirectToAction(nameof(SapGrupoCuentaCon));
        }
        #endregion

        #region Funciones
        /// <summary>
        /// Captura.
        /// </summary>
        private IActionResult SapGrupoCuentaCaptura(ESapGrupoCuenta sapGrupoCuenta)
        {
            ViewBag.Mensajes = NSapGrupoCuentas.Mensajes.Copy();
            ViewBag.Accion = EVSapGrupoCuentas.Accion;
            ViewBag.Reglas = EVSapGrupoCuentas.SapGrupoCuentaReglas;

            return ViewCap(nameof(SapGrupoCuentaCaptura), sapGrupoCuenta);
        }
        #endregion

        #region Acciones de Paginacion Orden y Filtro
        /// <summary>
        /// Control de paginacion.
        /// </summary>
        [MValidaSeg(nameof(SapGrupoCuentaInicia))]
        public IActionResult SapGrupoCuentaPaginacion(MEDatosPaginador datPag)
        {
            EVSapGrupoCuentas.SapGrupoCuentaPag.DatPag = datPag;
            return RedirectToAction(nameof(SapGrupoCuentaCon));
        }
        /// <summary>
        /// Control de orden.
        /// </summary>
        [MValidaSeg(nameof(SapGrupoCuentaInicia))]
        public IActionResult SapGrupoCuentaOrdena(String orden)
        {
            EVSapGrupoCuentas.SapGrupoCuentaColOrden = orden;
            return RedirectToAction(nameof(SapGrupoCuentaCon));
        }
        /// <summary>
        /// Control de filtro.
        /// </summary>
        [MValidaSeg(nameof(SapGrupoCuentaInicia))]
        public IActionResult SapGrupoCuentaFiltra(ESapGrupoCuentaFiltro filtro)
        {
            EVSapGrupoCuentas.SapGrupoCuentaFiltro = filtro;
            return RedirectToAction(nameof(SapGrupoCuentaCon));
        }
        /// <summary>
        /// Limpia filtros.
        /// </summary>
        [MValidaSeg(nameof(SapGrupoCuentaInicia))]
        public IActionResult SapGrupoCuentaLimpiaFiltros()
        {
            EVSapGrupoCuentas.SapGrupoCuentaFiltro = new ESapGrupoCuentaFiltro();
            return RedirectToAction(nameof(SapGrupoCuentaCon));
        }
        #endregion

        #endregion
    }
}
