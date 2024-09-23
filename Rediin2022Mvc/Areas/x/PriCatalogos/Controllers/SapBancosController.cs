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
        private EVSapBancos EVSapBancos
        {
            get
            {
                if (base.MSesion<EVSapBancos>() == null)
                    base.MSesion(new EVSapBancos());

                return base.MSesionAuto<EVSapBancos>();
            }
        }
        #endregion

        #region SapBanco (SapBancos)

        #region Acciones
        /// <summary>
        /// Inicia sub funcion.
        /// </summary>
        public IActionResult SapBancoInicia()
        {
            //Configuracion de inicio
            if (String.IsNullOrWhiteSpace(EVSapBancos.SapBancoColOrden))
                EVSapBancos.SapBancoColOrden = nameof(ESapBanco.SapBancoId);

            if (EVSapBancos.SapBancoReglas == null)
                EVSapBancos.SapBancoReglas = NSapBancos.SapBancoReglas();

            return RedirectToAction(nameof(SapBancoCon));
        }
        /// <summary>
        /// Consulta.
        /// </summary>
        [MValidaSeg(nameof(SapBancoInicia))]
        public IActionResult SapBancoCon()
        {
            base.MCargaFiltroPagYOrd(EVSapBancos.SapBancoFiltro,
                                     EVSapBancos.SapBancoPag,
                                     EVSapBancos.SapBancoColOrden,
                                     nameof(ESapBanco));

            EVSapBancos.SapBancoPag = NSapBancos.SapBancoPag(EVSapBancos.SapBancoFiltro);
            base.MActualizaTamPag(EVSapBancos.SapBancoPag?.DatPag);

            ViewBag.Mensajes = NSapBancos.Mensajes.Copy();
            ViewBag.Reglas = EVSapBancos.SapBancoReglas;
            ViewBag.DatPag = EVSapBancos.SapBancoPag?.DatPag;
            ViewBag.Orden = EVSapBancos.SapBancoColOrden;
            ViewBag.Filtro = EVSapBancos.SapBancoFiltro;
            ViewBag.Indice = EVSapBancos.SapBancoIndice;

            return View(nameof(SapBancoCon), EVSapBancos.SapBancoPag?.Pagina);
        }
        /// <summary>
        /// Consulta por id.
        /// </summary>
        public IActionResult SapBancoXId(Int32 indice)
        {
            EVSapBancos.Accion = MAccionesGen.Consulta;
            EVSapBancos.SapBancoIndice = indice;
            return SapBancoCaptura(EVSapBancos.SapBancoPag.Pagina[indice]);
        }
        /// <summary>
        /// Inserta.
        /// </summary>
        [MValidaSeg(nameof(SapBancoInserta))]
        public IActionResult SapBancoInsertaIni()
        {
            EVSapBancos.Accion = MAccionesGen.Inserta;
            return SapBancoInsertaCap(new ESapBanco()
            {
                Activo = true
            });
        }
        /// <summary>
        /// Inserta.
        /// </summary>
        [ValidateAntiForgeryToken]
        [MValidaSeg(nameof(SapBancoInserta))]
        public IActionResult SapBancoInsertaCap(ESapBanco sapBanco)
        {
            return SapBancoCaptura(sapBanco);
        }
        /// <summary>
        /// Inserta.
        /// </summary>
        [ValidateAntiForgeryToken]
        public IActionResult SapBancoInserta(ESapBanco sapBanco)
        {
            if (NSapBancos.SapBancoInserta(sapBanco))
                return RedirectToAction(nameof(SapBancoCon));

            return SapBancoInsertaCap(sapBanco);
        }
        /// <summary>
        /// Actualiza.
        /// </summary>
        [MValidaSeg(nameof(SapBancoActualiza))]
        public IActionResult SapBancoActualizaIni(Int32 indice)
        {
            EVSapBancos.Accion = MAccionesGen.Actualiza;
            EVSapBancos.SapBancoIndice = indice;
            EVSapBancos.SapBancoSel = EVSapBancos.SapBancoPag.Pagina[indice];
            return SapBancoActualizaCap(EVSapBancos.SapBancoSel);
        }
        /// <summary>
        /// Actualiza.
        /// </summary>
        [ValidateAntiForgeryToken]
        [MValidaSeg(nameof(SapBancoActualiza))]
        public IActionResult SapBancoActualizaCap(ESapBanco sapBanco)
        {
            return SapBancoCaptura(sapBanco);
        }
        /// <summary>
        /// Actualiza.
        /// </summary>
        [ValidateAntiForgeryToken]
        public IActionResult SapBancoActualiza(ESapBanco sapBanco)
        {
            if (NSapBancos.SapBancoActualiza(sapBanco))
                return RedirectToAction(nameof(SapBancoCon));

            return SapBancoActualizaCap(sapBanco);
        }
        /// <summary>
        /// Elimina.
        /// </summary>
        public IActionResult SapBancoElimina(Int32 indice)
        {
            NSapBancos.SapBancoElimina(EVSapBancos.SapBancoPag.Pagina[indice]);
            return RedirectToAction(nameof(SapBancoCon));
        }
        /// <summary>
        /// Exportacion de datos.
        /// </summary>
        public async Task<IActionResult> SapBancoExporta()
        {
            EVSapBancos.SapBancoFiltro.ColumnaOrden = EVSapBancos.SapBancoColOrden;
            EVSapBancos.SapBancoFiltro.Columnas = new Dictionary<String, String>()
                       {
                           { nameof(ESapBanco.Activo), String.Empty },
                           { nameof(ESapBanco.SapBancoId), String.Empty },
                           { nameof(ESapBanco.SapBancoNombre), String.Empty }
                       };

            MEDatosArchivo vDA = NSapBancos.SapBancoExporta(EVSapBancos.SapBancoFiltro);
            EVSapBancos.SapBancoFiltro.Columnas = null;
            if (NSapBancos.Mensajes.Ok)
                return await base.MEnviaArchivoACliente(NSapBancos.Mensajes, vDA);

            return RedirectToAction(nameof(SapBancoCon));
        }
        #endregion

        #region Funciones
        /// <summary>
        /// Captura.
        /// </summary>
        private IActionResult SapBancoCaptura(ESapBanco sapBanco)
        {
            ViewBag.Mensajes = NSapBancos.Mensajes.Copy();
            ViewBag.Accion = EVSapBancos.Accion;
            ViewBag.Reglas = EVSapBancos.SapBancoReglas;

            return ViewCap(nameof(SapBancoCaptura), sapBanco);
        }
        #endregion

        #region Acciones de Paginacion Orden y Filtro
        /// <summary>
        /// Control de paginacion.
        /// </summary>
        [MValidaSeg(nameof(SapBancoInicia))]
        public IActionResult SapBancoPaginacion(MEDatosPaginador datPag)
        {
            EVSapBancos.SapBancoPag.DatPag = datPag;
            return RedirectToAction(nameof(SapBancoCon));
        }
        /// <summary>
        /// Control de orden.
        /// </summary>
        [MValidaSeg(nameof(SapBancoInicia))]
        public IActionResult SapBancoOrdena(String orden)
        {
            EVSapBancos.SapBancoColOrden = orden;
            return RedirectToAction(nameof(SapBancoCon));
        }
        /// <summary>
        /// Control de filtro.
        /// </summary>
        [MValidaSeg(nameof(SapBancoInicia))]
        public IActionResult SapBancoFiltra(ESapBancoFiltro filtro)
        {
            EVSapBancos.SapBancoFiltro = filtro;
            return RedirectToAction(nameof(SapBancoCon));
        }
        /// <summary>
        /// Limpia filtros.
        /// </summary>
        [MValidaSeg(nameof(SapBancoInicia))]
        public IActionResult SapBancoLimpiaFiltros()
        {
            EVSapBancos.SapBancoFiltro = new ESapBancoFiltro();
            return RedirectToAction(nameof(SapBancoCon));
        }
        #endregion

        #endregion
    }
}
