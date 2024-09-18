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
        private EVSapCuentasAsociadas EV
        {
            get { return base.MEVCtrl<EVSapCuentasAsociadas>(); }
        }
        #endregion

        #region SapCuentaAsociada (SapCuentasAsociadas)

        #region Acciones
        /// <summary>
        /// Inicia sub funcion.
        /// </summary>
        public async Task<IActionResult> SapCuentaAsociadaInicia()
        {
            //Configuracion de inicio
            await Servicios.Gen.InicializaSF(EV.SapCuentaAsociada, nameof(ESapCuentaAsociada.SapCuentaAsociadaId),
                async () => await NSapCuentasAsociadas.SapCuentaAsociadaReglas());

            return RedirectToAction(nameof(SapCuentaAsociadaCon));
        }
        /// <summary>
        /// Consulta.
        /// </summary>
        [MValidaSeg(nameof(SapCuentaAsociadaInicia))]
        public async Task<IActionResult> SapCuentaAsociadaCon()
        {
            await Servicios.Pag.CargaPagOrdYFil(EV.SapCuentaAsociada);
            EV.SapCuentaAsociada.Pag = await NSapCuentasAsociadas.SapCuentaAsociadaPag(EV.SapCuentaAsociada.Filtro);
            await Servicios.Pag.ActTamPag(EV.SapCuentaAsociada);

            ViewBag.Mensajes = NSapCuentasAsociadas.Mensajes.Copy();
            ViewBag.EV = EV;

            return View(nameof(SapCuentaAsociadaCon), EV.SapCuentaAsociada.Pag?.Pagina);
        }
        /// <summary>
        /// Consulta por id.
        /// </summary>
        public async Task<IActionResult> SapCuentaAsociadaXId(Int32 indice)
        {
            EV.Accion = MAccionesGen.Consulta;
            EV.SapCuentaAsociada.Indice = indice;
            return await SapCuentaAsociadaCaptura(EV.SapCuentaAsociada.Pag.Pagina[indice]);
        }
        /// <summary>
        /// Inserta.
        /// </summary>
        [MValidaSeg(nameof(SapCuentaAsociadaInserta))]
        public async Task<IActionResult> SapCuentaAsociadaInsertaIni()
        {
            EV.Accion = MAccionesGen.Inserta;
            return await SapCuentaAsociadaInsertaCap(new ESapCuentaAsociada()
            {
                Activo = true
            });
        }
        /// <summary>
        /// Inserta.
        /// </summary>
        [ValidateAntiForgeryToken]
        [MValidaSeg(nameof(SapCuentaAsociadaInserta))]
        public async Task<IActionResult> SapCuentaAsociadaInsertaCap(ESapCuentaAsociada sapCuentaAsociada)
        {
            return await SapCuentaAsociadaCaptura(sapCuentaAsociada);
        }
        /// <summary>
        /// Inserta.
        /// </summary>
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SapCuentaAsociadaInserta(ESapCuentaAsociada sapCuentaAsociada)
        {
            if (await NSapCuentasAsociadas.SapCuentaAsociadaInserta(sapCuentaAsociada))
                return RedirectToAction(nameof(SapCuentaAsociadaCon));

            return await SapCuentaAsociadaInsertaCap(sapCuentaAsociada);
        }
        /// <summary>
        /// Actualiza.
        /// </summary>
        [MValidaSeg(nameof(SapCuentaAsociadaActualiza))]
        public async Task<IActionResult> SapCuentaAsociadaActualizaIni(Int32 indice)
        {
            EV.Accion = MAccionesGen.Actualiza;
            EV.SapCuentaAsociada.Indice = indice;
            EV.SapCuentaAsociada.Sel = EV.SapCuentaAsociada.Pag.Pagina[indice];
            return await SapCuentaAsociadaActualizaCap(EV.SapCuentaAsociada.Sel);
        }
        /// <summary>
        /// Actualiza.
        /// </summary>
        [ValidateAntiForgeryToken]
        [MValidaSeg(nameof(SapCuentaAsociadaActualiza))]
        public async Task<IActionResult> SapCuentaAsociadaActualizaCap(ESapCuentaAsociada sapCuentaAsociada)
        {
            return await SapCuentaAsociadaCaptura(sapCuentaAsociada);
        }
        /// <summary>
        /// Actualiza.
        /// </summary>
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SapCuentaAsociadaActualiza(ESapCuentaAsociada sapCuentaAsociada)
        {
            if (await NSapCuentasAsociadas.SapCuentaAsociadaActualiza(sapCuentaAsociada))
                return RedirectToAction(nameof(SapCuentaAsociadaCon));

            return await SapCuentaAsociadaActualizaCap(sapCuentaAsociada);
        }
        /// <summary>
        /// Elimina.
        /// </summary>
        public async Task<IActionResult> SapCuentaAsociadaElimina(Int32 indice)
        {
            await NSapCuentasAsociadas.SapCuentaAsociadaElimina(EV.SapCuentaAsociada.Pag.Pagina[indice]);
            return RedirectToAction(nameof(SapCuentaAsociadaCon));
        }
        /// <summary>
        /// Exportacion de datos.
        /// </summary>
        public async Task<IActionResult> SapCuentaAsociadaExporta()
        {
            EV.SapCuentaAsociada.Filtro.ColumnaOrden = EV.SapCuentaAsociada.ColOrden;
            EV.SapCuentaAsociada.Filtro.Columnas = new Dictionary<String, String>()
                                          {
                                              { nameof(ESapCuentaAsociada.Activo), String.Empty },
                                              { nameof(ESapCuentaAsociada.SapCuentaAsociadaId), String.Empty },
                                              { nameof(ESapCuentaAsociada.SapCuentaAsociadaNombre), String.Empty }
                                          };

            String vRutaYNombreArchivo = await NSapCuentasAsociadas.SapCuentaAsociadaExporta(EV.SapCuentaAsociada.Filtro);
            EV.SapCuentaAsociada.Filtro.Columnas = null;
            if (NSapCuentasAsociadas.Mensajes.Ok)
                return await MUtilMvc.DescargaArchivo(await Servicios.Archivos.DescargaArchivoTemp(vRutaYNombreArchivo));

            return RedirectToAction(nameof(SapCuentaAsociadaCon));
        }
        #endregion

        #region Funciones
        /// <summary>
        /// Captura.
        /// </summary>
        private async Task<IActionResult> SapCuentaAsociadaCaptura(ESapCuentaAsociada sapCuentaAsociada)
        {
            ViewBag.Mensajes = NSapCuentasAsociadas.Mensajes.Copy();
            ViewBag.EV = EV;

            return await Task.FromResult(ViewCap(nameof(SapCuentaAsociadaCaptura), sapCuentaAsociada));
        }
        #endregion

        #region Acciones de Paginacion Orden y Filtro
        /// <summary>
        /// Control de paginacion.
        /// </summary>
        [MValidaSeg(nameof(SapCuentaAsociadaInicia))]
        public IActionResult SapCuentaAsociadaPaginacion(MEDatosPaginador datPag)
        {
            EV.SapCuentaAsociada.Pag.DatPag = datPag;
            return RedirectToAction(nameof(SapCuentaAsociadaCon));
        }
        /// <summary>
        /// Control de orden.
        /// </summary>
        [MValidaSeg(nameof(SapCuentaAsociadaInicia))]
        public IActionResult SapCuentaAsociadaOrdena(String orden)
        {
            EV.SapCuentaAsociada.ColOrden = orden;
            return RedirectToAction(nameof(SapCuentaAsociadaCon));
        }
        /// <summary>
        /// Control de filtro.
        /// </summary>
        [MValidaSeg(nameof(SapCuentaAsociadaInicia))]
        public IActionResult SapCuentaAsociadaFiltra(ESapCuentaAsociadaFiltro filtro)
        {
            EV.SapCuentaAsociada.Filtro = filtro;
            return RedirectToAction(nameof(SapCuentaAsociadaCon));
        }
        /// <summary>
        /// Limpia filtros.
        /// </summary>
        [MValidaSeg(nameof(SapCuentaAsociadaInicia))]
        public IActionResult SapCuentaAsociadaLimpiaFiltros()
        {
            EV.SapCuentaAsociada.Filtro = new ESapCuentaAsociadaFiltro();
            return RedirectToAction(nameof(SapCuentaAsociadaCon));
        }
        #endregion

        #endregion
    }
}
