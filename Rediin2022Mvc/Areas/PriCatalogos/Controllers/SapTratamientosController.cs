using DSEntityNetX.Common.Casting;
using DSEntityNetX.Common.File;
using DSEntityNetX.Common.Pagination;
using DSMetodNetX.Aplicacion;
using DSMetodNetX.Entidades;
using DSMetodNetX.Mvc;
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

namespace Rediin2022Mvc.Areas.PriCatalogos.Controllers
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
        private EVSapTratamientos EV
        {
            get { return base.MEVCtrl<EVSapTratamientos>(); }
        }
        #endregion

        #region SapTratamiento (SapTratamientos)

        #region Acciones
        /// <summary>
        /// Inicia sub funcion.
        /// </summary>
        public async Task<IActionResult> SapTratamientoInicia()
        {
            //Configuracion de inicio
            await Servicios.Gen.InicializaSF(EV.SapTratamiento, nameof(ESapTratamiento.SapTratamientoId),
                async () => await NSapTratamientos.SapTratamientoReglas());

            return RedirectToAction(nameof(SapTratamientoCon));
        }
        /// <summary>
        /// Consulta.
        /// </summary>
        [MValidaSeg(nameof(SapTratamientoInicia))]
        public async Task<IActionResult> SapTratamientoCon()
        {
            await Servicios.Pag.CargaPagOrdYFil(EV.SapTratamiento);
            EV.SapTratamiento.Pag = await NSapTratamientos.SapTratamientoPag(EV.SapTratamiento.Filtro);
            await Servicios.Pag.ActTamPag(EV.SapTratamiento);

            ViewBag.Mensajes = NSapTratamientos.Mensajes;
            ViewBag.EV = EV;

            return View(nameof(SapTratamientoCon), EV.SapTratamiento.Pag?.Pagina);
        }
        /// <summary>
        /// Consulta por id.
        /// </summary>
        public async Task<IActionResult> SapTratamientoXId(Int32 indice)
        {
            EV.Accion = MAccionesGen.Consulta;
            EV.SapTratamiento.Indice = indice;
            return await SapTratamientoCaptura(EV.SapTratamiento.Pag.Pagina[indice]);
        }
        /// <summary>
        /// Inserta.
        /// </summary>
        [MValidaSeg(nameof(SapTratamientoInserta))]
        public async Task<IActionResult> SapTratamientoInsertaIni()
        {
            EV.Accion = MAccionesGen.Inserta;
            return await SapTratamientoInsertaCap(new ESapTratamiento()
            {
                Activo = true
            });
        }
        /// <summary>
        /// Inserta.
        /// </summary>
        [ValidateAntiForgeryToken]
        [MValidaSeg(nameof(SapTratamientoInserta))]
        public async Task<IActionResult> SapTratamientoInsertaCap(ESapTratamiento sapTratamiento)
        {
            return await SapTratamientoCaptura(sapTratamiento);
        }
        /// <summary>
        /// Inserta.
        /// </summary>
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SapTratamientoInserta(ESapTratamiento sapTratamiento)
        {
            if (await NSapTratamientos.SapTratamientoInserta(sapTratamiento))
                return RedirectToAction(nameof(SapTratamientoCon));

            return await SapTratamientoInsertaCap(sapTratamiento);
        }
        /// <summary>
        /// Actualiza.
        /// </summary>
        [MValidaSeg(nameof(SapTratamientoActualiza))]
        public async Task<IActionResult> SapTratamientoActualizaIni(Int32 indice)
        {
            EV.Accion = MAccionesGen.Actualiza;
            EV.SapTratamiento.Indice = indice;
            EV.SapTratamiento.Sel = EV.SapTratamiento.Pag.Pagina[indice];
            return await SapTratamientoActualizaCap(EV.SapTratamiento.Sel);
        }
        /// <summary>
        /// Actualiza.
        /// </summary>
        [ValidateAntiForgeryToken]
        [MValidaSeg(nameof(SapTratamientoActualiza))]
        public async Task<IActionResult> SapTratamientoActualizaCap(ESapTratamiento sapTratamiento)
        {
            return await SapTratamientoCaptura(sapTratamiento);
        }
        /// <summary>
        /// Actualiza.
        /// </summary>
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SapTratamientoActualiza(ESapTratamiento sapTratamiento)
        {
            if (await NSapTratamientos.SapTratamientoActualiza(sapTratamiento))
                return RedirectToAction(nameof(SapTratamientoCon));

            return await SapTratamientoActualizaCap(sapTratamiento);
        }
        /// <summary>
        /// Elimina.
        /// </summary>
        public async Task<IActionResult> SapTratamientoElimina(Int32 indice)
        {
            await NSapTratamientos.SapTratamientoElimina(EV.SapTratamiento.Pag.Pagina[indice]);
            return RedirectToAction(nameof(SapTratamientoCon));
        }
        /// <summary>
        /// Exportacion de datos.
        /// </summary>
        public async Task<IActionResult> SapTratamientoExporta()
        {
            EV.SapTratamiento.Filtro.ColumnaOrden = EV.SapTratamiento.ColOrden;
            EV.SapTratamiento.Filtro.Columnas = new Dictionary<String, String>()
                                   {
                                       { nameof(ESapTratamiento.Activo), String.Empty },
                                       { nameof(ESapTratamiento.SapTratamientoId), String.Empty },
                                       { nameof(ESapTratamiento.SapTratamientoNombre), String.Empty }
                                   };

            String vRutaYNombreArchivo = await NSapTratamientos.SapTratamientoExporta(EV.SapTratamiento.Filtro);
            EV.SapTratamiento.Filtro.Columnas = null;
            if (NSapTratamientos.Mensajes.Ok)
                return await MUtilMvc.DescargaArchivo(await Servicios.Archivos.DescargaArchivoTemp(vRutaYNombreArchivo));

            return RedirectToAction(nameof(SapTratamientoCon));
        }
        #endregion

        #region Funciones
        /// <summary>
        /// Captura.
        /// </summary>
        private async Task<IActionResult> SapTratamientoCaptura(ESapTratamiento sapTratamiento)
        {
            ViewBag.Mensajes = NSapTratamientos.Mensajes.Copy();
            ViewBag.EV = EV;

            return await Task.FromResult(ViewCap(nameof(SapTratamientoCaptura), sapTratamiento));
        }
        #endregion

        #region Acciones de Paginacion Orden y Filtro
        /// <summary>
        /// Control de paginacion.
        /// </summary>
        [MValidaSeg(nameof(SapTratamientoInicia))]
        public IActionResult SapTratamientoPaginacion(MEDatosPaginador datPag)
        {
            EV.SapTratamiento.Pag.DatPag = datPag;
            return RedirectToAction(nameof(SapTratamientoCon));
        }
        /// <summary>
        /// Control de orden.
        /// </summary>
        [MValidaSeg(nameof(SapTratamientoInicia))]
        public IActionResult SapTratamientoOrdena(String orden)
        {
            EV.SapTratamiento.ColOrden = orden;
            return RedirectToAction(nameof(SapTratamientoCon));
        }
        /// <summary>
        /// Control de filtro.
        /// </summary>
        [MValidaSeg(nameof(SapTratamientoInicia))]
        public IActionResult SapTratamientoFiltra(ESapTratamientoFiltro filtro)
        {
            EV.SapTratamiento.Filtro = filtro;
            return RedirectToAction(nameof(SapTratamientoCon));
        }
        /// <summary>
        /// Limpia filtros.
        /// </summary>
        [MValidaSeg(nameof(SapTratamientoInicia))]
        public IActionResult SapTratamientoLimpiaFiltros()
        {
            EV.SapTratamiento.Filtro = new ESapTratamientoFiltro();
            return RedirectToAction(nameof(SapTratamientoCon));
        }
        #endregion

        #endregion
    }
}
