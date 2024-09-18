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
    public class SapGruposToleranciaController : MControllerMvcPri
    {
        #region Constructores
        /// <summary>
        /// Controlador MVC.
        /// </summary>
        public SapGruposToleranciaController(INSapGruposTolerancia nSapGruposTolerancia)
        {
            NSapGruposTolerancia = nSapGruposTolerancia;
        }
        #endregion

        #region Propiedades
        /// <summary>
        /// Negocio SapGruposTolerancia.
        /// </summary>
        private INSapGruposTolerancia NSapGruposTolerancia { get; set; }
        /// <summary>
        /// Entidad de variables.
        /// </summary>
        private EVSapGruposTolerancia EV
        {
            get { return base.MEVCtrl<EVSapGruposTolerancia>(); }
        }
        #endregion

        #region SapGrupoTolerancia (SapGruposTolerancia)

        #region Acciones
        /// <summary>
        /// Inicia sub funcion.
        /// </summary>
        public async Task<IActionResult> SapGrupoToleranciaInicia()
        {
            //Configuracion de inicio
            await Servicios.Gen.InicializaSF(EV.SapGrupoTolerancia, nameof(ESapGrupoTolerancia.SapGrupoToleranciaId),
                async () => await NSapGruposTolerancia.SapGrupoToleranciaReglas());

            return RedirectToAction(nameof(SapGrupoToleranciaCon));
        }
        /// <summary>
        /// Consulta.
        /// </summary>
        [MValidaSeg(nameof(SapGrupoToleranciaInicia))]
        public async Task<IActionResult> SapGrupoToleranciaCon()
        {
            await Servicios.Pag.CargaPagOrdYFil(EV.SapGrupoTolerancia);
            EV.SapGrupoTolerancia.Pag = await NSapGruposTolerancia.SapGrupoToleranciaPag(EV.SapGrupoTolerancia.Filtro);
            await Servicios.Pag.ActTamPag(EV.SapGrupoTolerancia);

            ViewBag.Mensajes = NSapGruposTolerancia.Mensajes;
            ViewBag.EV = EV;

            return View(nameof(SapGrupoToleranciaCon), EV.SapGrupoTolerancia.Pag?.Pagina);
        }
        /// <summary>
        /// Consulta por id.
        /// </summary>
        public async Task<IActionResult> SapGrupoToleranciaXId(Int32 indice)
        {
            EV.Accion = MAccionesGen.Consulta;
            EV.SapGrupoTolerancia.Indice = indice;
            return await SapGrupoToleranciaCaptura(EV.SapGrupoTolerancia.Pag.Pagina[indice]);
        }
        /// <summary>
        /// Inserta.
        /// </summary>
        [MValidaSeg(nameof(SapGrupoToleranciaInserta))]
        public async Task<IActionResult> SapGrupoToleranciaInsertaIni()
        {
            EV.Accion = MAccionesGen.Inserta;
            return await SapGrupoToleranciaInsertaCap(new ESapGrupoTolerancia()
            {
                Activo = true
            });
        }
        /// <summary>
        /// Inserta.
        /// </summary>
        [ValidateAntiForgeryToken]
        [MValidaSeg(nameof(SapGrupoToleranciaInserta))]
        public async Task<IActionResult> SapGrupoToleranciaInsertaCap(ESapGrupoTolerancia sapGrupoTolerancia)
        {
            return await SapGrupoToleranciaCaptura(sapGrupoTolerancia);
        }
        /// <summary>
        /// Inserta.
        /// </summary>
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SapGrupoToleranciaInserta(ESapGrupoTolerancia sapGrupoTolerancia)
        {
            if (await NSapGruposTolerancia.SapGrupoToleranciaInserta(sapGrupoTolerancia))
                return RedirectToAction(nameof(SapGrupoToleranciaCon));

            return await SapGrupoToleranciaInsertaCap(sapGrupoTolerancia);
        }
        /// <summary>
        /// Actualiza.
        /// </summary>
        [MValidaSeg(nameof(SapGrupoToleranciaActualiza))]
        public async Task<IActionResult> SapGrupoToleranciaActualizaIni(Int32 indice)
        {
            EV.Accion = MAccionesGen.Actualiza;
            EV.SapGrupoTolerancia.Indice = indice;
            EV.SapGrupoTolerancia.Sel = EV.SapGrupoTolerancia.Pag.Pagina[indice];
            return await SapGrupoToleranciaActualizaCap(EV.SapGrupoTolerancia.Sel);
        }
        /// <summary>
        /// Actualiza.
        /// </summary>
        [ValidateAntiForgeryToken]
        [MValidaSeg(nameof(SapGrupoToleranciaActualiza))]
        public async Task<IActionResult> SapGrupoToleranciaActualizaCap(ESapGrupoTolerancia sapGrupoTolerancia)
        {
            return await SapGrupoToleranciaCaptura(sapGrupoTolerancia);
        }
        /// <summary>
        /// Actualiza.
        /// </summary>
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SapGrupoToleranciaActualiza(ESapGrupoTolerancia sapGrupoTolerancia)
        {
            if (await NSapGruposTolerancia.SapGrupoToleranciaActualiza(sapGrupoTolerancia))
                return RedirectToAction(nameof(SapGrupoToleranciaCon));

            return await SapGrupoToleranciaActualizaCap(sapGrupoTolerancia);
        }
        /// <summary>
        /// Elimina.
        /// </summary>
        public async Task<IActionResult> SapGrupoToleranciaElimina(Int32 indice)
        {
            await NSapGruposTolerancia.SapGrupoToleranciaElimina(EV.SapGrupoTolerancia.Pag.Pagina[indice]);
            return RedirectToAction(nameof(SapGrupoToleranciaCon));
        }
        /// <summary>
        /// Exportacion de datos.
        /// </summary>
        public async Task<IActionResult> SapGrupoToleranciaExporta()
        {
            EV.SapGrupoTolerancia.Filtro.ColumnaOrden = EV.SapGrupoTolerancia.ColOrden;
            EV.SapGrupoTolerancia.Filtro.Columnas = new Dictionary<String, String>()
                                           {
                                               { nameof(ESapGrupoTolerancia.Activo), String.Empty },
                                               { nameof(ESapGrupoTolerancia.SapGrupoToleranciaId), String.Empty },
                                               { nameof(ESapGrupoTolerancia.SapGrupoToleranciaNombre), String.Empty }
                                           };

            String vRutaYNombreArchivo = await NSapGruposTolerancia.SapGrupoToleranciaExporta(EV.SapGrupoTolerancia.Filtro);
            EV.SapGrupoTolerancia.Filtro.Columnas = null;
            if (NSapGruposTolerancia.Mensajes.Ok)
                return await MUtilMvc.DescargaArchivo(await Servicios.Archivos.DescargaArchivoTemp(vRutaYNombreArchivo));

            return RedirectToAction(nameof(SapGrupoToleranciaCon));
        }
        #endregion

        #region Funciones
        /// <summary>
        /// Captura.
        /// </summary>
        private async Task<IActionResult> SapGrupoToleranciaCaptura(ESapGrupoTolerancia sapGrupoTolerancia)
        {
            ViewBag.Mensajes = NSapGruposTolerancia.Mensajes.Copy();
            ViewBag.EV = EV;

            return await Task.FromResult(ViewCap(nameof(SapGrupoToleranciaCaptura), sapGrupoTolerancia));
        }
        #endregion

        #region Acciones de Paginacion Orden y Filtro
        /// <summary>
        /// Control de paginacion.
        /// </summary>
        [MValidaSeg(nameof(SapGrupoToleranciaInicia))]
        public IActionResult SapGrupoToleranciaPaginacion(MEDatosPaginador datPag)
        {
            EV.SapGrupoTolerancia.Pag.DatPag = datPag;
            return RedirectToAction(nameof(SapGrupoToleranciaCon));
        }
        /// <summary>
        /// Control de orden.
        /// </summary>
        [MValidaSeg(nameof(SapGrupoToleranciaInicia))]
        public IActionResult SapGrupoToleranciaOrdena(String orden)
        {
            EV.SapGrupoTolerancia.ColOrden = orden;
            return RedirectToAction(nameof(SapGrupoToleranciaCon));
        }
        /// <summary>
        /// Control de filtro.
        /// </summary>
        [MValidaSeg(nameof(SapGrupoToleranciaInicia))]
        public IActionResult SapGrupoToleranciaFiltra(ESapGrupoToleranciaFiltro filtro)
        {
            EV.SapGrupoTolerancia.Filtro = filtro;
            return RedirectToAction(nameof(SapGrupoToleranciaCon));
        }
        /// <summary>
        /// Limpia filtros.
        /// </summary>
        [MValidaSeg(nameof(SapGrupoToleranciaInicia))]
        public IActionResult SapGrupoToleranciaLimpiaFiltros()
        {
            EV.SapGrupoTolerancia.Filtro = new ESapGrupoToleranciaFiltro();
            return RedirectToAction(nameof(SapGrupoToleranciaCon));
        }
        #endregion

        #endregion
    }
}
