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
        private EVSapGrupoCuentas EV
        {
            get { return base.MEVCtrl<EVSapGrupoCuentas>(); }
        }
        #endregion

        #region SapGrupoCuenta (SapGrupoCuentas)

        #region Acciones
        /// <summary>
        /// Inicia sub funcion.
        /// </summary>
        public async Task<IActionResult> SapGrupoCuentaInicia()
        {
            //Configuracion de inicio
            await Servicios.Gen.InicializaSF(EV.SapGrupoCuenta, nameof(ESapGrupoCuenta.SapGrupoCuentaId),
                async () => await NSapGrupoCuentas.SapGrupoCuentaReglas());

            return RedirectToAction(nameof(SapGrupoCuentaCon));
        }
        /// <summary>
        /// Consulta.
        /// </summary>
        [MValidaSeg(nameof(SapGrupoCuentaInicia))]
        public async Task<IActionResult> SapGrupoCuentaCon()
        {
            await Servicios.Pag.CargaPagOrdYFil(EV.SapGrupoCuenta);
            EV.SapGrupoCuenta.Pag = await NSapGrupoCuentas.SapGrupoCuentaPag(EV.SapGrupoCuenta.Filtro);
            await Servicios.Pag.ActTamPag(EV.SapGrupoCuenta);

            ViewBag.Mensajes = NSapGrupoCuentas.Mensajes;
            ViewBag.EV = EV;

            return View(nameof(SapGrupoCuentaCon), EV.SapGrupoCuenta.Pag?.Pagina);
        }
        /// <summary>
        /// Consulta por id.
        /// </summary>
        public async Task<IActionResult> SapGrupoCuentaXId(Int32 indice)
        {
            EV.Accion = MAccionesGen.Consulta;
            EV.SapGrupoCuenta.Indice = indice;
            return await SapGrupoCuentaCaptura(EV.SapGrupoCuenta.Pag.Pagina[indice]);
        }
        /// <summary>
        /// Inserta.
        /// </summary>
        [MValidaSeg(nameof(SapGrupoCuentaInserta))]
        public async Task<IActionResult> SapGrupoCuentaInsertaIni()
        {
            EV.Accion = MAccionesGen.Inserta;
            return await SapGrupoCuentaInsertaCap(new ESapGrupoCuenta()
            {
                Activo = true
            });
        }
        /// <summary>
        /// Inserta.
        /// </summary>
        [ValidateAntiForgeryToken]
        [MValidaSeg(nameof(SapGrupoCuentaInserta))]
        public async Task<IActionResult> SapGrupoCuentaInsertaCap(ESapGrupoCuenta sapGrupoCuenta)
        {
            return await SapGrupoCuentaCaptura(sapGrupoCuenta);
        }
        /// <summary>
        /// Inserta.
        /// </summary>
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SapGrupoCuentaInserta(ESapGrupoCuenta sapGrupoCuenta)
        {
            if (await NSapGrupoCuentas.SapGrupoCuentaInserta(sapGrupoCuenta))
                return RedirectToAction(nameof(SapGrupoCuentaCon));

            return await SapGrupoCuentaInsertaCap(sapGrupoCuenta);
        }
        /// <summary>
        /// Actualiza.
        /// </summary>
        [MValidaSeg(nameof(SapGrupoCuentaActualiza))]
        public async Task<IActionResult> SapGrupoCuentaActualizaIni(Int32 indice)
        {
            EV.Accion = MAccionesGen.Actualiza;
            EV.SapGrupoCuenta.Indice = indice;
            EV.SapGrupoCuenta.Sel = EV.SapGrupoCuenta.Pag.Pagina[indice];
            return await SapGrupoCuentaActualizaCap(EV.SapGrupoCuenta.Sel);
        }
        /// <summary>
        /// Actualiza.
        /// </summary>
        [ValidateAntiForgeryToken]
        [MValidaSeg(nameof(SapGrupoCuentaActualiza))]
        public async Task<IActionResult> SapGrupoCuentaActualizaCap(ESapGrupoCuenta sapGrupoCuenta)
        {
            return await SapGrupoCuentaCaptura(sapGrupoCuenta);
        }
        /// <summary>
        /// Actualiza.
        /// </summary>
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SapGrupoCuentaActualiza(ESapGrupoCuenta sapGrupoCuenta)
        {
            if (await NSapGrupoCuentas.SapGrupoCuentaActualiza(sapGrupoCuenta))
                return RedirectToAction(nameof(SapGrupoCuentaCon));

            return await SapGrupoCuentaActualizaCap(sapGrupoCuenta);
        }
        /// <summary>
        /// Elimina.
        /// </summary>
        public async Task<IActionResult> SapGrupoCuentaElimina(Int32 indice)
        {
            await NSapGrupoCuentas.SapGrupoCuentaElimina(EV.SapGrupoCuenta.Pag.Pagina[indice]);
            return RedirectToAction(nameof(SapGrupoCuentaCon));
        }
        /// <summary>
        /// Exportacion de datos.
        /// </summary>
        public async Task<IActionResult> SapGrupoCuentaExporta()
        {
            EV.SapGrupoCuenta.Filtro.ColumnaOrden = EV.SapGrupoCuenta.ColOrden;
            EV.SapGrupoCuenta.Filtro.Columnas = new Dictionary<String, String>()
                                   {
                                       { nameof(ESapGrupoCuenta.Activo), String.Empty },
                                       { nameof(ESapGrupoCuenta.SapGrupoCuentaId), String.Empty },
                                       { nameof(ESapGrupoCuenta.SapGrupoCuentaNombre), String.Empty }
                                   };

            String vRutaYNombreArchivo = await NSapGrupoCuentas.SapGrupoCuentaExporta(EV.SapGrupoCuenta.Filtro);
            EV.SapGrupoCuenta.Filtro.Columnas = null;
            if (NSapGrupoCuentas.Mensajes.Ok)
                return await MUtilMvc.DescargaArchivo(await Servicios.Archivos.DescargaArchivoTemp(vRutaYNombreArchivo));

            return RedirectToAction(nameof(SapGrupoCuentaCon));
        }
        #endregion

        #region Funciones
        /// <summary>
        /// Captura.
        /// </summary>
        private async Task<IActionResult> SapGrupoCuentaCaptura(ESapGrupoCuenta sapGrupoCuenta)
        {
            ViewBag.Mensajes = NSapGrupoCuentas.Mensajes.Copy();
            ViewBag.EV = EV;

            return await Task.FromResult(ViewCap(nameof(SapGrupoCuentaCaptura), sapGrupoCuenta));
        }
        #endregion

        #region Acciones de Paginacion Orden y Filtro
        /// <summary>
        /// Control de paginacion.
        /// </summary>
        [MValidaSeg(nameof(SapGrupoCuentaInicia))]
        public IActionResult SapGrupoCuentaPaginacion(MEDatosPaginador datPag)
        {
            EV.SapGrupoCuenta.Pag.DatPag = datPag;
            return RedirectToAction(nameof(SapGrupoCuentaCon));
        }
        /// <summary>
        /// Control de orden.
        /// </summary>
        [MValidaSeg(nameof(SapGrupoCuentaInicia))]
        public IActionResult SapGrupoCuentaOrdena(String orden)
        {
            EV.SapGrupoCuenta.ColOrden = orden;
            return RedirectToAction(nameof(SapGrupoCuentaCon));
        }
        /// <summary>
        /// Control de filtro.
        /// </summary>
        [MValidaSeg(nameof(SapGrupoCuentaInicia))]
        public IActionResult SapGrupoCuentaFiltra(ESapGrupoCuentaFiltro filtro)
        {
            EV.SapGrupoCuenta.Filtro = filtro;
            return RedirectToAction(nameof(SapGrupoCuentaCon));
        }
        /// <summary>
        /// Limpia filtros.
        /// </summary>
        [MValidaSeg(nameof(SapGrupoCuentaInicia))]
        public IActionResult SapGrupoCuentaLimpiaFiltros()
        {
            EV.SapGrupoCuenta.Filtro = new ESapGrupoCuentaFiltro();
            return RedirectToAction(nameof(SapGrupoCuentaCon));
        }
        #endregion

        #endregion
    }
}
