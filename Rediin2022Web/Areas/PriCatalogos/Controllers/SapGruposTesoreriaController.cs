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
    public class SapGruposTesoreriaController : MControllerMvcPri
    {
        #region Constructores
        /// <summary>
        /// Controlador MVC.
        /// </summary>
        public SapGruposTesoreriaController(INSapGruposTesoreria nSapGruposTesoreria)
        {
            NSapGruposTesoreria = nSapGruposTesoreria;
        }
        #endregion

        #region Propiedades
        /// <summary>
        /// Negocio SapGruposTesoreria.
        /// </summary>
        private INSapGruposTesoreria NSapGruposTesoreria { get; set; }
        /// <summary>
        /// Entidad de variables.
        /// </summary>
        private EVSapGruposTesoreria EV
        {
            get { return base.MEVCtrl<EVSapGruposTesoreria>(); }
        }
        #endregion

        #region SapGrupoTesoreria (SapGruposTesoreria)

        #region Acciones
        /// <summary>
        /// Inicia sub funcion.
        /// </summary>
        public async Task<IActionResult> SapGrupoTesoreriaInicia()
        {
            //Configuracion de inicio
            await Servicios.Gen.InicializaSF(EV.SapGrupoTesoreria, nameof(ESapGrupoTesoreria.SapGrupoTesoreriaId),
                async () => await NSapGruposTesoreria.SapGrupoTesoreriaReglas());

            return RedirectToAction(nameof(SapGrupoTesoreriaCon));
        }
        /// <summary>
        /// Consulta.
        /// </summary>
        [MValidaSeg(nameof(SapGrupoTesoreriaInicia))]
        public async Task<IActionResult> SapGrupoTesoreriaCon()
        {
            await Servicios.Pag.CargaPagOrdYFil(EV.SapGrupoTesoreria);
            EV.SapGrupoTesoreria.Pag = await NSapGruposTesoreria.SapGrupoTesoreriaPag(EV.SapGrupoTesoreria.Filtro);
            await Servicios.Pag.ActTamPag(EV.SapGrupoTesoreria);

            ViewBag.Mensajes = NSapGruposTesoreria.Mensajes;
            ViewBag.EV = EV;

            return View(nameof(SapGrupoTesoreriaCon), EV.SapGrupoTesoreria.Pag?.Pagina);
        }
        /// <summary>
        /// Consulta por id.
        /// </summary>
        public async Task<IActionResult> SapGrupoTesoreriaXId(Int32 indice)
        {
            EV.Accion = MAccionesGen.Consulta;
            EV.SapGrupoTesoreria.Indice = indice;
            return await SapGrupoTesoreriaCaptura(EV.SapGrupoTesoreria.Pag.Pagina[indice]);
        }
        /// <summary>
        /// Inserta.
        /// </summary>
        [MValidaSeg(nameof(SapGrupoTesoreriaInserta))]
        public async Task<IActionResult> SapGrupoTesoreriaInsertaIni()
        {
            EV.Accion = MAccionesGen.Inserta;
            return await SapGrupoTesoreriaInsertaCap(new ESapGrupoTesoreria()
            {
                Activo = true
            });
        }
        /// <summary>
        /// Inserta.
        /// </summary>
        [ValidateAntiForgeryToken]
        [MValidaSeg(nameof(SapGrupoTesoreriaInserta))]
        public async Task<IActionResult> SapGrupoTesoreriaInsertaCap(ESapGrupoTesoreria sapGrupoTesoreria)
        {
            return await SapGrupoTesoreriaCaptura(sapGrupoTesoreria);
        }
        /// <summary>
        /// Inserta.
        /// </summary>
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SapGrupoTesoreriaInserta(ESapGrupoTesoreria sapGrupoTesoreria)
        {
            if (await NSapGruposTesoreria.SapGrupoTesoreriaInserta(sapGrupoTesoreria))
                return RedirectToAction(nameof(SapGrupoTesoreriaCon));

            return await SapGrupoTesoreriaInsertaCap(sapGrupoTesoreria);
        }
        /// <summary>
        /// Actualiza.
        /// </summary>
        [MValidaSeg(nameof(SapGrupoTesoreriaActualiza))]
        public async Task<IActionResult> SapGrupoTesoreriaActualizaIni(Int32 indice)
        {
            EV.Accion = MAccionesGen.Actualiza;
            EV.SapGrupoTesoreria.Indice = indice;
            EV.SapGrupoTesoreria.Sel = EV.SapGrupoTesoreria.Pag.Pagina[indice];
            return await SapGrupoTesoreriaActualizaCap(EV.SapGrupoTesoreria.Sel);
        }
        /// <summary>
        /// Actualiza.
        /// </summary>
        [ValidateAntiForgeryToken]
        [MValidaSeg(nameof(SapGrupoTesoreriaActualiza))]
        public async Task<IActionResult> SapGrupoTesoreriaActualizaCap(ESapGrupoTesoreria sapGrupoTesoreria)
        {
            return await SapGrupoTesoreriaCaptura(sapGrupoTesoreria);
        }
        /// <summary>
        /// Actualiza.
        /// </summary>
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SapGrupoTesoreriaActualiza(ESapGrupoTesoreria sapGrupoTesoreria)
        {
            if (await NSapGruposTesoreria.SapGrupoTesoreriaActualiza(sapGrupoTesoreria))
                return RedirectToAction(nameof(SapGrupoTesoreriaCon));

            return await SapGrupoTesoreriaActualizaCap(sapGrupoTesoreria);
        }
        /// <summary>
        /// Elimina.
        /// </summary>
        public async Task<IActionResult> SapGrupoTesoreriaElimina(Int32 indice)
        {
            await NSapGruposTesoreria.SapGrupoTesoreriaElimina(EV.SapGrupoTesoreria.Pag.Pagina[indice]);
            return RedirectToAction(nameof(SapGrupoTesoreriaCon));
        }
        /// <summary>
        /// Exportacion de datos.
        /// </summary>
        public async Task<IActionResult> SapGrupoTesoreriaExporta()
        {
            EV.SapGrupoTesoreria.Filtro.ColumnaOrden = EV.SapGrupoTesoreria.ColOrden;
            EV.SapGrupoTesoreria.Filtro.Columnas = new Dictionary<String, String>()
                                         {
                                             { nameof(ESapGrupoTesoreria.Activo), String.Empty },
                                             { nameof(ESapGrupoTesoreria.SapGrupoTesoreriaId), String.Empty },
                                             { nameof(ESapGrupoTesoreria.SapGrupoTesoreriaNombre), String.Empty }
                                         };

            String vRutaYNombreArchivo = await NSapGruposTesoreria.SapGrupoTesoreriaExporta(EV.SapGrupoTesoreria.Filtro);
            EV.SapGrupoTesoreria.Filtro.Columnas = null;
            if (NSapGruposTesoreria.Mensajes.Ok)
                return await MUtilMvc.DescargaArchivo(await Servicios.Archivos.DescargaArchivoTemp(vRutaYNombreArchivo));

            return RedirectToAction(nameof(SapGrupoTesoreriaCon));
        }
        #endregion

        #region Funciones
        /// <summary>
        /// Captura.
        /// </summary>
        private async Task<IActionResult> SapGrupoTesoreriaCaptura(ESapGrupoTesoreria sapGrupoTesoreria)
        {
            ViewBag.Mensajes = NSapGruposTesoreria.Mensajes.Copy();
            ViewBag.EV = EV;

            return await Task.FromResult(ViewCap(nameof(SapGrupoTesoreriaCaptura), sapGrupoTesoreria));
        }
        #endregion

        #region Acciones de Paginacion Orden y Filtro
        /// <summary>
        /// Control de paginacion.
        /// </summary>
        [MValidaSeg(nameof(SapGrupoTesoreriaInicia))]
        public IActionResult SapGrupoTesoreriaPaginacion(MEDatosPaginador datPag)
        {
            EV.SapGrupoTesoreria.Pag.DatPag = datPag;
            return RedirectToAction(nameof(SapGrupoTesoreriaCon));
        }
        /// <summary>
        /// Control de orden.
        /// </summary>
        [MValidaSeg(nameof(SapGrupoTesoreriaInicia))]
        public IActionResult SapGrupoTesoreriaOrdena(String orden)
        {
            EV.SapGrupoTesoreria.ColOrden = orden;
            return RedirectToAction(nameof(SapGrupoTesoreriaCon));
        }
        /// <summary>
        /// Control de filtro.
        /// </summary>
        [MValidaSeg(nameof(SapGrupoTesoreriaInicia))]
        public IActionResult SapGrupoTesoreriaFiltra(ESapGrupoTesoreriaFiltro filtro)
        {
            EV.SapGrupoTesoreria.Filtro = filtro;
            return RedirectToAction(nameof(SapGrupoTesoreriaCon));
        }
        /// <summary>
        /// Limpia filtros.
        /// </summary>
        [MValidaSeg(nameof(SapGrupoTesoreriaInicia))]
        public IActionResult SapGrupoTesoreriaLimpiaFiltros()
        {
            EV.SapGrupoTesoreria.Filtro = new ESapGrupoTesoreriaFiltro();
            return RedirectToAction(nameof(SapGrupoTesoreriaCon));
        }
        #endregion

        #endregion
    }
}
