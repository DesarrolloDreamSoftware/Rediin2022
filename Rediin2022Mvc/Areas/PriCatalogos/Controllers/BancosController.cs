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
    public class BancosController : MControllerMvcPri
    {
        #region Constructores
        /// <summary>
        /// Controlador MVC.
        /// </summary>
        public BancosController(INBancos nBancos)
        {
            NBancos = nBancos;
        }
        #endregion

        #region Propiedades
        /// <summary>
        /// Negocio Bancos.
        /// </summary>
        private INBancos NBancos { get; set; }
        /// <summary>
        /// Entidad de variables.
        /// </summary>
        private EVBancos EV
        {
            get { return base.MEVCtrl<EVBancos>(); }
        }
        #endregion

        #region Banco (Bancos)

        #region Acciones
        /// <summary>
        /// Inicia sub funcion.
        /// </summary>
        public async Task<IActionResult> BancoInicia()
        {
            //Configuracion de inicio
            await Servicios.Gen.InicializaSF(EV.Banco, "-" + nameof(EBanco.BancoId),
                async () => await NBancos.BancoReglas());

            return RedirectToAction(nameof(BancoCon));
        }
        /// <summary>
        /// Consulta.
        /// </summary>
        [MValidaSeg(nameof(BancoInicia))]
        public async Task<IActionResult> BancoCon()
        {
            await Servicios.Pag.CargaPagOrdYFil(EV.Banco);
            EV.Banco.Pag = await NBancos.BancoPag(EV.Banco.Filtro);
            await Servicios.Pag.ActTamPag(EV.Banco);

            ViewBag.Mensajes = NBancos.Mensajes;
            ViewBag.EV = EV;

            return View(nameof(BancoCon), EV.Banco.Pag?.Pagina);
        }
        /// <summary>
        /// Consulta por id.
        /// </summary>
        public async Task<IActionResult> BancoXId(Int32 indice)
        {
            EV.Accion = MAccionesGen.Consulta;
            EV.Banco.Indice = indice;
            return await BancoCaptura(EV.Banco.Pag.Pagina[indice]);
        }
        /// <summary>
        /// Inserta.
        /// </summary>
        [MValidaSeg(nameof(BancoInserta))]
        public async Task<IActionResult> BancoInsertaIni()
        {
            EV.Accion = MAccionesGen.Inserta;
            return await BancoInsertaCap(new EBanco()
            {
                Activo = true
            });
        }
        /// <summary>
        /// Inserta.
        /// </summary>
        [ValidateAntiForgeryToken]
        [MValidaSeg(nameof(BancoInserta))]
        public async Task<IActionResult> BancoInsertaCap(EBanco banco)
        {
            return await BancoCaptura(banco);
        }
        /// <summary>
        /// Inserta.
        /// </summary>
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BancoInserta(EBanco banco)
        {
            await NBancos.BancoInserta(banco);
            if (NBancos.Mensajes.Ok)
                return RedirectToAction(nameof(BancoCon));

            return await BancoInsertaCap(banco);
        }
        /// <summary>
        /// Actualiza.
        /// </summary>
        [MValidaSeg(nameof(BancoActualiza))]
        public async Task<IActionResult> BancoActualizaIni(Int32 indice)
        {
            EV.Accion = MAccionesGen.Actualiza;
            EV.Banco.Indice = indice;
            EV.Banco.Sel = EV.Banco.Pag.Pagina[indice];
            return await BancoActualizaCap(EV.Banco.Sel);
        }
        /// <summary>
        /// Actualiza.
        /// </summary>
        [ValidateAntiForgeryToken]
        [MValidaSeg(nameof(BancoActualiza))]
        public async Task<IActionResult> BancoActualizaCap(EBanco banco)
        {
            return await BancoCaptura(banco);
        }
        /// <summary>
        /// Actualiza.
        /// </summary>
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BancoActualiza(EBanco banco)
        {
            if (await NBancos.BancoActualiza(banco))
                return RedirectToAction(nameof(BancoCon));

            return await BancoActualizaCap(banco);
        }
        /// <summary>
        /// Elimina.
        /// </summary>
        public async Task<IActionResult> BancoElimina(Int32 indice)
        {
            await NBancos.BancoElimina(EV.Banco.Pag.Pagina[indice]);
            return RedirectToAction(nameof(BancoCon));
        }
        /// <summary>
        /// Exportacion de datos.
        /// </summary>
        public async Task<IActionResult> BancoExporta()
        {
            EV.Banco.Filtro.ColumnaOrden = EV.Banco.ColOrden;
            EV.Banco.Filtro.Columnas = new Dictionary<String, String>()
                 {
                     { nameof(EBanco.Activo), String.Empty },
                     { nameof(EBanco.BancoId), String.Empty },
                     { nameof(EBanco.BancoNombre), String.Empty }
                 };

            String vRutaYNombreArchivo = await NBancos.BancoExporta(EV.Banco.Filtro);
            EV.Banco.Filtro.Columnas = null;
            if (NBancos.Mensajes.Ok)
                return await MUtilMvc.DescargaArchivo(await Servicios.Archivos.DescargaArchivoTemp(vRutaYNombreArchivo));

            return RedirectToAction(nameof(BancoCon));
        }
        #endregion

        #region Funciones
        /// <summary>
        /// Captura.
        /// </summary>
        private async Task<IActionResult> BancoCaptura(EBanco banco)
        {
            ViewBag.Mensajes = NBancos.Mensajes;
            ViewBag.EV = EV;

            return await Task.FromResult(ViewCap(nameof(BancoCaptura), banco));
        }
        #endregion

        #region Acciones de Paginacion Orden y Filtro
        /// <summary>
        /// Control de paginacion.
        /// </summary>
        [MValidaSeg(nameof(BancoInicia))]
        public IActionResult BancoPaginacion(MEDatosPaginador datPag)
        {
            EV.Banco.Pag.DatPag = datPag;
            return RedirectToAction(nameof(BancoCon));
        }
        /// <summary>
        /// Control de orden.
        /// </summary>
        [MValidaSeg(nameof(BancoInicia))]
        public IActionResult BancoOrdena(String orden)
        {
            EV.Banco.ColOrden = orden;
            return RedirectToAction(nameof(BancoCon));
        }
        /// <summary>
        /// Control de filtro.
        /// </summary>
        [MValidaSeg(nameof(BancoInicia))]
        public IActionResult BancoFiltra(EBancoFiltro filtro)
        {
            EV.Banco.Filtro = filtro;
            return RedirectToAction(nameof(BancoCon));
        }
        /// <summary>
        /// Limpia filtros.
        /// </summary>
        [MValidaSeg(nameof(BancoInicia))]
        public IActionResult BancoLimpiaFiltros()
        {
            EV.Banco.Filtro = new EBancoFiltro();
            return RedirectToAction(nameof(BancoCon));
        }
        #endregion

        #endregion
    }
}
