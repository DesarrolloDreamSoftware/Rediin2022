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
        private EVSapGruposTolerancia EVSapGruposTolerancia
        {
            get
            {
                if (base.MSesion<EVSapGruposTolerancia>() == null)
                    base.MSesion(new EVSapGruposTolerancia());

                return base.MSesionAuto<EVSapGruposTolerancia>();
            }
        }
        #endregion

        #region SapGrupoTolerancia (SapGruposTolerancia)

        #region Acciones
        /// <summary>
        /// Inicia sub funcion.
        /// </summary>
        public IActionResult SapGrupoToleranciaInicia()
        {
            //Configuracion de inicio
            if (String.IsNullOrWhiteSpace(EVSapGruposTolerancia.SapGrupoToleranciaColOrden))
                EVSapGruposTolerancia.SapGrupoToleranciaColOrden = nameof(ESapGrupoTolerancia.SapGrupoToleranciaId);

            if (EVSapGruposTolerancia.SapGrupoToleranciaReglas == null)
                EVSapGruposTolerancia.SapGrupoToleranciaReglas = NSapGruposTolerancia.SapGrupoToleranciaReglas();

            return RedirectToAction(nameof(SapGrupoToleranciaCon));
        }
        /// <summary>
        /// Consulta.
        /// </summary>
        [MValidaSeg(nameof(SapGrupoToleranciaInicia))]
        public IActionResult SapGrupoToleranciaCon()
        {
            base.MCargaFiltroPagYOrd(EVSapGruposTolerancia.SapGrupoToleranciaFiltro,
                                     EVSapGruposTolerancia.SapGrupoToleranciaPag,
                                     EVSapGruposTolerancia.SapGrupoToleranciaColOrden,
                                     nameof(ESapGrupoTolerancia));

            EVSapGruposTolerancia.SapGrupoToleranciaPag = NSapGruposTolerancia.SapGrupoToleranciaPag(EVSapGruposTolerancia.SapGrupoToleranciaFiltro);
            base.MActualizaTamPag(EVSapGruposTolerancia.SapGrupoToleranciaPag?.DatPag);

            ViewBag.Mensajes = NSapGruposTolerancia.Mensajes.Copy();
            ViewBag.Reglas = EVSapGruposTolerancia.SapGrupoToleranciaReglas;
            ViewBag.DatPag = EVSapGruposTolerancia.SapGrupoToleranciaPag?.DatPag;
            ViewBag.Orden = EVSapGruposTolerancia.SapGrupoToleranciaColOrden;
            ViewBag.Filtro = EVSapGruposTolerancia.SapGrupoToleranciaFiltro;
            ViewBag.Indice = EVSapGruposTolerancia.SapGrupoToleranciaIndice;

            return View(nameof(SapGrupoToleranciaCon), EVSapGruposTolerancia.SapGrupoToleranciaPag?.Pagina);
        }
        /// <summary>
        /// Consulta por id.
        /// </summary>
        public IActionResult SapGrupoToleranciaXId(Int32 indice)
        {
            EVSapGruposTolerancia.Accion = MAccionesGen.Consulta;
            EVSapGruposTolerancia.SapGrupoToleranciaIndice = indice;
            return SapGrupoToleranciaCaptura(EVSapGruposTolerancia.SapGrupoToleranciaPag.Pagina[indice]);
        }
        /// <summary>
        /// Inserta.
        /// </summary>
        [MValidaSeg(nameof(SapGrupoToleranciaInserta))]
        public IActionResult SapGrupoToleranciaInsertaIni()
        {
            EVSapGruposTolerancia.Accion = MAccionesGen.Inserta;
            return SapGrupoToleranciaInsertaCap(new ESapGrupoTolerancia()
            {
                Activo = true
            });
        }
        /// <summary>
        /// Inserta.
        /// </summary>
        [ValidateAntiForgeryToken]
        [MValidaSeg(nameof(SapGrupoToleranciaInserta))]
        public IActionResult SapGrupoToleranciaInsertaCap(ESapGrupoTolerancia sapGrupoTolerancia)
        {
            return SapGrupoToleranciaCaptura(sapGrupoTolerancia);
        }
        /// <summary>
        /// Inserta.
        /// </summary>
        [ValidateAntiForgeryToken]
        public IActionResult SapGrupoToleranciaInserta(ESapGrupoTolerancia sapGrupoTolerancia)
        {
            if (NSapGruposTolerancia.SapGrupoToleranciaInserta(sapGrupoTolerancia))
                return RedirectToAction(nameof(SapGrupoToleranciaCon));

            return SapGrupoToleranciaInsertaCap(sapGrupoTolerancia);
        }
        /// <summary>
        /// Actualiza.
        /// </summary>
        [MValidaSeg(nameof(SapGrupoToleranciaActualiza))]
        public IActionResult SapGrupoToleranciaActualizaIni(Int32 indice)
        {
            EVSapGruposTolerancia.Accion = MAccionesGen.Actualiza;
            EVSapGruposTolerancia.SapGrupoToleranciaIndice = indice;
            EVSapGruposTolerancia.SapGrupoToleranciaSel = EVSapGruposTolerancia.SapGrupoToleranciaPag.Pagina[indice];
            return SapGrupoToleranciaActualizaCap(EVSapGruposTolerancia.SapGrupoToleranciaSel);
        }
        /// <summary>
        /// Actualiza.
        /// </summary>
        [ValidateAntiForgeryToken]
        [MValidaSeg(nameof(SapGrupoToleranciaActualiza))]
        public IActionResult SapGrupoToleranciaActualizaCap(ESapGrupoTolerancia sapGrupoTolerancia)
        {
            return SapGrupoToleranciaCaptura(sapGrupoTolerancia);
        }
        /// <summary>
        /// Actualiza.
        /// </summary>
        [ValidateAntiForgeryToken]
        public IActionResult SapGrupoToleranciaActualiza(ESapGrupoTolerancia sapGrupoTolerancia)
        {
            if (NSapGruposTolerancia.SapGrupoToleranciaActualiza(sapGrupoTolerancia))
                return RedirectToAction(nameof(SapGrupoToleranciaCon));

            return SapGrupoToleranciaActualizaCap(sapGrupoTolerancia);
        }
        /// <summary>
        /// Elimina.
        /// </summary>
        public IActionResult SapGrupoToleranciaElimina(Int32 indice)
        {
            NSapGruposTolerancia.SapGrupoToleranciaElimina(EVSapGruposTolerancia.SapGrupoToleranciaPag.Pagina[indice]);
            return RedirectToAction(nameof(SapGrupoToleranciaCon));
        }
        /// <summary>
        /// Exportacion de datos.
        /// </summary>
        public async Task<IActionResult> SapGrupoToleranciaExporta()
        {
            EVSapGruposTolerancia.SapGrupoToleranciaFiltro.ColumnaOrden = EVSapGruposTolerancia.SapGrupoToleranciaColOrden;
            EVSapGruposTolerancia.SapGrupoToleranciaFiltro.Columnas = new Dictionary<String, String>()
                                           {
                                               { nameof(ESapGrupoTolerancia.Activo), String.Empty },
                                               { nameof(ESapGrupoTolerancia.SapGrupoToleranciaId), String.Empty },
                                               { nameof(ESapGrupoTolerancia.SapGrupoToleranciaNombre), String.Empty }
                                           };

            MEDatosArchivo vDA = NSapGruposTolerancia.SapGrupoToleranciaExporta(EVSapGruposTolerancia.SapGrupoToleranciaFiltro);
            EVSapGruposTolerancia.SapGrupoToleranciaFiltro.Columnas = null;
            if (NSapGruposTolerancia.Mensajes.Ok)
                return await base.MEnviaArchivoACliente(NSapGruposTolerancia.Mensajes, vDA);

            return RedirectToAction(nameof(SapGrupoToleranciaCon));
        }
        #endregion

        #region Funciones
        /// <summary>
        /// Captura.
        /// </summary>
        private IActionResult SapGrupoToleranciaCaptura(ESapGrupoTolerancia sapGrupoTolerancia)
        {
            ViewBag.Mensajes = NSapGruposTolerancia.Mensajes.Copy();
            ViewBag.Accion = EVSapGruposTolerancia.Accion;
            ViewBag.Reglas = EVSapGruposTolerancia.SapGrupoToleranciaReglas;

            return ViewCap(nameof(SapGrupoToleranciaCaptura), sapGrupoTolerancia);
        }
        #endregion

        #region Acciones de Paginacion Orden y Filtro
        /// <summary>
        /// Control de paginacion.
        /// </summary>
        [MValidaSeg(nameof(SapGrupoToleranciaInicia))]
        public IActionResult SapGrupoToleranciaPaginacion(MEDatosPaginador datPag)
        {
            EVSapGruposTolerancia.SapGrupoToleranciaPag.DatPag = datPag;
            return RedirectToAction(nameof(SapGrupoToleranciaCon));
        }
        /// <summary>
        /// Control de orden.
        /// </summary>
        [MValidaSeg(nameof(SapGrupoToleranciaInicia))]
        public IActionResult SapGrupoToleranciaOrdena(String orden)
        {
            EVSapGruposTolerancia.SapGrupoToleranciaColOrden = orden;
            return RedirectToAction(nameof(SapGrupoToleranciaCon));
        }
        /// <summary>
        /// Control de filtro.
        /// </summary>
        [MValidaSeg(nameof(SapGrupoToleranciaInicia))]
        public IActionResult SapGrupoToleranciaFiltra(ESapGrupoToleranciaFiltro filtro)
        {
            EVSapGruposTolerancia.SapGrupoToleranciaFiltro = filtro;
            return RedirectToAction(nameof(SapGrupoToleranciaCon));
        }
        /// <summary>
        /// Limpia filtros.
        /// </summary>
        [MValidaSeg(nameof(SapGrupoToleranciaInicia))]
        public IActionResult SapGrupoToleranciaLimpiaFiltros()
        {
            EVSapGruposTolerancia.SapGrupoToleranciaFiltro = new ESapGrupoToleranciaFiltro();
            return RedirectToAction(nameof(SapGrupoToleranciaCon));
        }
        #endregion

        #endregion
    }
}
