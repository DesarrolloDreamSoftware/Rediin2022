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
    public class SapBancosController : MControllerMvcPri
    {
        #region Constructores
        /// <summary>
        /// Controlador MVC.
        /// </summary>
        public SapBancosController(INSapBancos nSapBancos)
        {
            NSapBancos = nSapBancos;
        }
        #endregion

        #region Propiedades
        /// <summary>
        /// Negocio SapBancos.
        /// </summary>
        private INSapBancos NSapBancos { get; set; }
        /// <summary>
        /// Entidad de variables.
        /// </summary>
        private EVSapBancos EV
        {
            get { return base.MEVCtrl<EVSapBancos>(); }
        }
        #endregion

        #region SapBanco (SapBancos)

        #region Acciones
        /// <summary>
        /// Inicia sub funcion.
        /// </summary>
        public async Task<IActionResult> SapBancoInicia()
        {
            //Configuracion de inicio
            await Servicios.Gen.InicializaSF(EV.SapBanco, nameof(ESapBanco.SapBancoId),
                async () => await NSapBancos.SapBancoReglas());

            return RedirectToAction(nameof(SapBancoCon));
        }
        /// <summary>
        /// Consulta.
        /// </summary>
        [MValidaSeg(nameof(SapBancoInicia))]
        public async Task<IActionResult> SapBancoCon()
        {
            await Servicios.Pag.CargaPagOrdYFil(EV.SapBanco);
            EV.SapBanco.Pag = await NSapBancos.SapBancoPag(EV.SapBanco.Filtro);
            await Servicios.Pag.ActTamPag(EV.SapBanco);

            ViewBag.Mensajes = NSapBancos.Mensajes;
            ViewBag.EV = EV;

            return View(nameof(SapBancoCon), EV.SapBanco.Pag?.Pagina);
        }
        /// <summary>
        /// Consulta por id.
        /// </summary>
        public async Task<IActionResult> SapBancoXId(Int32 indice)
        {
            EV.Accion = MAccionesGen.Consulta;
            EV.SapBanco.Indice = indice;
            return await SapBancoCaptura(EV.SapBanco.Pag.Pagina[indice]);
        }
        /// <summary>
        /// Inserta.
        /// </summary>
        [MValidaSeg(nameof(SapBancoInserta))]
        public async Task<IActionResult> SapBancoInsertaIni()
        {
            EV.Accion = MAccionesGen.Inserta;
            return await SapBancoInsertaCap(new ESapBanco()
            {
                Activo = true
            });
        }
        /// <summary>
        /// Inserta.
        /// </summary>
        [ValidateAntiForgeryToken]
        [MValidaSeg(nameof(SapBancoInserta))]
        public async Task<IActionResult> SapBancoInsertaCap(ESapBanco sapBanco)
        {
            return await SapBancoCaptura(sapBanco);
        }
        /// <summary>
        /// Inserta.
        /// </summary>
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SapBancoInserta(ESapBanco sapBanco)
        {
            if (await NSapBancos.SapBancoInserta(sapBanco))
                return RedirectToAction(nameof(SapBancoCon));

            return await SapBancoInsertaCap(sapBanco);
        }
        /// <summary>
        /// Actualiza.
        /// </summary>
        [MValidaSeg(nameof(SapBancoActualiza))]
        public async Task<IActionResult> SapBancoActualizaIni(Int32 indice)
        {
            EV.Accion = MAccionesGen.Actualiza;
            EV.SapBanco.Indice = indice;
            EV.SapBanco.Sel = EV.SapBanco.Pag.Pagina[indice];
            return await SapBancoActualizaCap(EV.SapBanco.Sel);
        }
        /// <summary>
        /// Actualiza.
        /// </summary>
        [ValidateAntiForgeryToken]
        [MValidaSeg(nameof(SapBancoActualiza))]
        public async Task<IActionResult> SapBancoActualizaCap(ESapBanco sapBanco)
        {
            return await SapBancoCaptura(sapBanco);
        }
        /// <summary>
        /// Actualiza.
        /// </summary>
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SapBancoActualiza(ESapBanco sapBanco)
        {
            if (await NSapBancos.SapBancoActualiza(sapBanco))
                return RedirectToAction(nameof(SapBancoCon));

            return await SapBancoActualizaCap(sapBanco);
        }
        /// <summary>
        /// Elimina.
        /// </summary>
        public async Task<IActionResult> SapBancoElimina(Int32 indice)
        {
            await NSapBancos.SapBancoElimina(EV.SapBanco.Pag.Pagina[indice]);
            return RedirectToAction(nameof(SapBancoCon));
        }
        /// <summary>
        /// Exportacion de datos.
        /// </summary>
        public async Task<IActionResult> SapBancoExporta()
        {
            EV.SapBanco.Filtro.ColumnaOrden = EV.SapBanco.ColOrden;
            EV.SapBanco.Filtro.Columnas = new Dictionary<String, String>()
                       {
                           { nameof(ESapBanco.Activo), String.Empty },
                           { nameof(ESapBanco.SapBancoId), String.Empty },
                           { nameof(ESapBanco.SapBancoNombre), String.Empty }
                       };

            string vRutaYNombreArchivo = await NSapBancos.SapBancoExporta(EV.SapBanco.Filtro);
            EV.SapBanco.Filtro.Columnas = null;
            if (NSapBancos.Mensajes.Ok)
                return await MUtilMvc.DescargaArchivo(await Servicios.Archivos.DescargaArchivoTemp(vRutaYNombreArchivo));

            return RedirectToAction(nameof(SapBancoCon));
        }
        #endregion

        #region Funciones
        /// <summary>
        /// Captura.
        /// </summary>
        private async Task<IActionResult> SapBancoCaptura(ESapBanco sapBanco)
        {
            ViewBag.Mensajes = NSapBancos.Mensajes;
            ViewBag.EV = EV;

            return await Task.FromResult(ViewCap(nameof(SapBancoCaptura), sapBanco));
        }
        #endregion

        #region Acciones de Paginacion Orden y Filtro
        /// <summary>
        /// Control de paginacion.
        /// </summary>
        [MValidaSeg(nameof(SapBancoInicia))]
        public IActionResult SapBancoPaginacion(MEDatosPaginador datPag)
        {
            EV.SapBanco.Pag.DatPag = datPag;
            return RedirectToAction(nameof(SapBancoCon));
        }
        /// <summary>
        /// Control de orden.
        /// </summary>
        [MValidaSeg(nameof(SapBancoInicia))]
        public IActionResult SapBancoOrdena(String orden)
        {
            EV.SapBanco.ColOrden = orden;
            return RedirectToAction(nameof(SapBancoCon));
        }
        /// <summary>
        /// Control de filtro.
        /// </summary>
        [MValidaSeg(nameof(SapBancoInicia))]
        public IActionResult SapBancoFiltra(ESapBancoFiltro filtro)
        {
            EV.SapBanco.Filtro = filtro;
            return RedirectToAction(nameof(SapBancoCon));
        }
        /// <summary>
        /// Limpia filtros.
        /// </summary>
        [MValidaSeg(nameof(SapBancoInicia))]
        public IActionResult SapBancoLimpiaFiltros()
        {
            EV.SapBanco.Filtro = new ESapBancoFiltro();
            return RedirectToAction(nameof(SapBancoCon));
        }
        #endregion

        #endregion
    }
}
