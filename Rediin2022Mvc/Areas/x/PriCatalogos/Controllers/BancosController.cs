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
        private EVBancos EVBancos
        {
            get
            {
                if (base.MSesion<EVBancos>() == null)
                    base.MSesion(new EVBancos());

                return base.MSesionAuto<EVBancos>();
            }
        }
        #endregion

        #region Banco (Bancos)

        #region Acciones
        /// <summary>
        /// Inicia sub funcion.
        /// </summary>
        public IActionResult BancoInicia()
        {
            //Configuracion de inicio
            if (String.IsNullOrWhiteSpace(EVBancos.BancoColOrden))
                EVBancos.BancoColOrden = "-" + nameof(EBanco.BancoId);

            if (EVBancos.BancoReglas == null)
            {
                EVBancos.BancoReglas = NBancos.BancoReglas();
                base.MMensajesTemp = NBancos.Mensajes.ToString();
            }

            return RedirectToAction(nameof(BancoCon));
        }
        /// <summary>
        /// Consulta.
        /// </summary>
        [MValidaSeg(nameof(BancoInicia))]
        public IActionResult BancoCon()
        {
            base.MCargaFiltroPagYOrd(EVBancos.BancoFiltro,
                                     EVBancos.BancoPag,
                                     EVBancos.BancoColOrden,
                                     nameof(EBanco));

            EVBancos.BancoPag = NBancos.BancoPag(EVBancos.BancoFiltro);
            base.MActualizaTamPag(EVBancos.BancoPag?.DatPag);

            ViewBag.Mensajes = base.MObtenMensajes(NBancos.Mensajes);
            ViewBag.Reglas = EVBancos.BancoReglas;
            ViewBag.DatPag = EVBancos.BancoPag?.DatPag;
            ViewBag.Orden = EVBancos.BancoColOrden;
            ViewBag.Filtro = EVBancos.BancoFiltro;
            ViewBag.Indice = EVBancos.BancoIndice;

            return View(nameof(BancoCon), EVBancos.BancoPag?.Pagina);
        }
        /// <summary>
        /// Consulta por id.
        /// </summary>
        public IActionResult BancoXId(Int32 indice)
        {
            EVBancos.Accion = MAccionesGen.Consulta;
            EVBancos.BancoIndice = indice;
            return BancoCaptura(EVBancos.BancoPag.Pagina[indice]);
        }
        /// <summary>
        /// Inserta.
        /// </summary>
        [MValidaSeg(nameof(BancoInserta))]
        public IActionResult BancoInsertaIni()
        {
            EVBancos.Accion = MAccionesGen.Inserta;
            return BancoInsertaCap(new EBanco()
            {
                Activo = true
            });
        }
        /// <summary>
        /// Inserta.
        /// </summary>
        [ValidateAntiForgeryToken]
        [MValidaSeg(nameof(BancoInserta))]
        public IActionResult BancoInsertaCap(EBanco banco)
        {
            return BancoCaptura(banco);
        }
        /// <summary>
        /// Inserta.
        /// </summary>
        [ValidateAntiForgeryToken]
        public IActionResult BancoInserta(EBanco banco)
        {
            NBancos.BancoInserta(banco);
            if (NBancos.Mensajes.Ok)
                return RedirectToAction(nameof(BancoCon));

            return BancoInsertaCap(banco);
        }
        /// <summary>
        /// Actualiza.
        /// </summary>
        [MValidaSeg(nameof(BancoActualiza))]
        public IActionResult BancoActualizaIni(Int32 indice)
        {
            EVBancos.Accion = MAccionesGen.Actualiza;
            EVBancos.BancoIndice = indice;
            EVBancos.BancoSel = EVBancos.BancoPag.Pagina[indice];
            return BancoActualizaCap(EVBancos.BancoSel);
        }
        /// <summary>
        /// Actualiza.
        /// </summary>
        [ValidateAntiForgeryToken]
        [MValidaSeg(nameof(BancoActualiza))]
        public IActionResult BancoActualizaCap(EBanco banco)
        {
            return BancoCaptura(banco);
        }
        /// <summary>
        /// Actualiza.
        /// </summary>
        [ValidateAntiForgeryToken]
        public IActionResult BancoActualiza(EBanco banco)
        {
            if (NBancos.BancoActualiza(banco))
                return RedirectToAction(nameof(BancoCon));

            return BancoActualizaCap(banco);
        }
        /// <summary>
        /// Elimina.
        /// </summary>
        public IActionResult BancoElimina(Int32 indice)
        {
            NBancos.BancoElimina(EVBancos.BancoPag.Pagina[indice]);
            base.MMensajesTemp = NBancos.Mensajes.ToString();
            return RedirectToAction(nameof(BancoCon));
        }
        /// <summary>
        /// Exportacion de datos.
        /// </summary>
        public async Task<IActionResult> BancoExporta()
        {
            EVBancos.BancoFiltro.ColumnaOrden = EVBancos.BancoColOrden;
            EVBancos.BancoFiltro.Columnas = new Dictionary<String, String>()
                 {
                     { nameof(EBanco.Activo), String.Empty },
                     { nameof(EBanco.BancoId), String.Empty },
                     { nameof(EBanco.BancoNombre), String.Empty }
                 };

            MEDatosArchivo vDA = NBancos.BancoExporta(EVBancos.BancoFiltro);
            EVBancos.BancoFiltro.Columnas = null;
            if (NBancos.Mensajes.Ok)
                return await base.MEnviaArchivoACliente(NBancos.Mensajes, vDA);

            base.MMensajesTemp = NBancos.Mensajes.ToString();
            return RedirectToAction(nameof(BancoCon));
        }
        #endregion

        #region Funciones
        /// <summary>
        /// Captura.
        /// </summary>
        private IActionResult BancoCaptura(EBanco banco)
        {
            ViewBag.Mensajes = base.MObtenMensajes(NBancos.Mensajes);
            ViewBag.Accion = EVBancos.Accion;
            ViewBag.Reglas = EVBancos.BancoReglas;

            return ViewCap(nameof(BancoCaptura), banco);
        }
        #endregion

        #region Acciones de Paginacion Orden y Filtro
        /// <summary>
        /// Control de paginacion.
        /// </summary>
        [MValidaSeg(nameof(BancoInicia))]
        public IActionResult BancoPaginacion(MEDatosPaginador datPag)
        {
            EVBancos.BancoPag.DatPag = datPag;
            return RedirectToAction(nameof(BancoCon));
        }
        /// <summary>
        /// Control de orden.
        /// </summary>
        [MValidaSeg(nameof(BancoInicia))]
        public IActionResult BancoOrdena(String orden)
        {
            EVBancos.BancoColOrden = orden;
            return RedirectToAction(nameof(BancoCon));
        }
        /// <summary>
        /// Control de filtro.
        /// </summary>
        [MValidaSeg(nameof(BancoInicia))]
        public IActionResult BancoFiltra(EBancoFiltro filtro)
        {
            EVBancos.BancoFiltro = filtro;
            return RedirectToAction(nameof(BancoCon));
        }
        /// <summary>
        /// Limpia filtros.
        /// </summary>
        [MValidaSeg(nameof(BancoInicia))]
        public IActionResult BancoLimpiaFiltros()
        {
            EVBancos.BancoFiltro = new EBancoFiltro();
            return RedirectToAction(nameof(BancoCon));
        }
        #endregion

        #endregion
    }
}
