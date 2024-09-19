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
        private EVSapGruposTesoreria EVSapGruposTesoreria
        {
            get
            {
                if (base.MSesion<EVSapGruposTesoreria>() == null)
                    base.MSesion(new EVSapGruposTesoreria());

                return base.MSesionAuto<EVSapGruposTesoreria>();
            }
        }
        #endregion

        #region SapGrupoTesoreria (SapGruposTesoreria)

        #region Acciones
        /// <summary>
        /// Inicia sub funcion.
        /// </summary>
        public IActionResult SapGrupoTesoreriaInicia()
        {
            //Configuracion de inicio
            if (String.IsNullOrWhiteSpace(EVSapGruposTesoreria.SapGrupoTesoreriaColOrden))
                EVSapGruposTesoreria.SapGrupoTesoreriaColOrden = nameof(ESapGrupoTesoreria.SapGrupoTesoreriaId);

            if (EVSapGruposTesoreria.SapGrupoTesoreriaReglas == null)
                EVSapGruposTesoreria.SapGrupoTesoreriaReglas = NSapGruposTesoreria.SapGrupoTesoreriaReglas();

            return RedirectToAction(nameof(SapGrupoTesoreriaCon));
        }
        /// <summary>
        /// Consulta.
        /// </summary>
        [MValidaSeg(nameof(SapGrupoTesoreriaInicia))]
        public IActionResult SapGrupoTesoreriaCon()
        {
            base.MCargaFiltroPagYOrd(EVSapGruposTesoreria.SapGrupoTesoreriaFiltro,
                                     EVSapGruposTesoreria.SapGrupoTesoreriaPag,
                                     EVSapGruposTesoreria.SapGrupoTesoreriaColOrden,
                                     nameof(ESapGrupoTesoreria));

            EVSapGruposTesoreria.SapGrupoTesoreriaPag = NSapGruposTesoreria.SapGrupoTesoreriaPag(EVSapGruposTesoreria.SapGrupoTesoreriaFiltro);
            base.MActualizaTamPag(EVSapGruposTesoreria.SapGrupoTesoreriaPag?.DatPag);

            ViewBag.Mensajes = NSapGruposTesoreria.Mensajes.Copy();
            ViewBag.Reglas = EVSapGruposTesoreria.SapGrupoTesoreriaReglas;
            ViewBag.DatPag = EVSapGruposTesoreria.SapGrupoTesoreriaPag?.DatPag;
            ViewBag.Orden = EVSapGruposTesoreria.SapGrupoTesoreriaColOrden;
            ViewBag.Filtro = EVSapGruposTesoreria.SapGrupoTesoreriaFiltro;
            ViewBag.Indice = EVSapGruposTesoreria.SapGrupoTesoreriaIndice;

            return View(nameof(SapGrupoTesoreriaCon), EVSapGruposTesoreria.SapGrupoTesoreriaPag?.Pagina);
        }
        /// <summary>
        /// Consulta por id.
        /// </summary>
        public IActionResult SapGrupoTesoreriaXId(Int32 indice)
        {
            EVSapGruposTesoreria.Accion = MAccionesGen.Consulta;
            EVSapGruposTesoreria.SapGrupoTesoreriaIndice = indice;
            return SapGrupoTesoreriaCaptura(EVSapGruposTesoreria.SapGrupoTesoreriaPag.Pagina[indice]);
        }
        /// <summary>
        /// Inserta.
        /// </summary>
        [MValidaSeg(nameof(SapGrupoTesoreriaInserta))]
        public IActionResult SapGrupoTesoreriaInsertaIni()
        {
            EVSapGruposTesoreria.Accion = MAccionesGen.Inserta;
            return SapGrupoTesoreriaInsertaCap(new ESapGrupoTesoreria()
            {
                Activo = true
            });
        }
        /// <summary>
        /// Inserta.
        /// </summary>
        [ValidateAntiForgeryToken]
        [MValidaSeg(nameof(SapGrupoTesoreriaInserta))]
        public IActionResult SapGrupoTesoreriaInsertaCap(ESapGrupoTesoreria sapGrupoTesoreria)
        {
            return SapGrupoTesoreriaCaptura(sapGrupoTesoreria);
        }
        /// <summary>
        /// Inserta.
        /// </summary>
        [ValidateAntiForgeryToken]
        public IActionResult SapGrupoTesoreriaInserta(ESapGrupoTesoreria sapGrupoTesoreria)
        {
            if (NSapGruposTesoreria.SapGrupoTesoreriaInserta(sapGrupoTesoreria))
                return RedirectToAction(nameof(SapGrupoTesoreriaCon));

            return SapGrupoTesoreriaInsertaCap(sapGrupoTesoreria);
        }
        /// <summary>
        /// Actualiza.
        /// </summary>
        [MValidaSeg(nameof(SapGrupoTesoreriaActualiza))]
        public IActionResult SapGrupoTesoreriaActualizaIni(Int32 indice)
        {
            EVSapGruposTesoreria.Accion = MAccionesGen.Actualiza;
            EVSapGruposTesoreria.SapGrupoTesoreriaIndice = indice;
            EVSapGruposTesoreria.SapGrupoTesoreriaSel = EVSapGruposTesoreria.SapGrupoTesoreriaPag.Pagina[indice];
            return SapGrupoTesoreriaActualizaCap(EVSapGruposTesoreria.SapGrupoTesoreriaSel);
        }
        /// <summary>
        /// Actualiza.
        /// </summary>
        [ValidateAntiForgeryToken]
        [MValidaSeg(nameof(SapGrupoTesoreriaActualiza))]
        public IActionResult SapGrupoTesoreriaActualizaCap(ESapGrupoTesoreria sapGrupoTesoreria)
        {
            return SapGrupoTesoreriaCaptura(sapGrupoTesoreria);
        }
        /// <summary>
        /// Actualiza.
        /// </summary>
        [ValidateAntiForgeryToken]
        public IActionResult SapGrupoTesoreriaActualiza(ESapGrupoTesoreria sapGrupoTesoreria)
        {
            if (NSapGruposTesoreria.SapGrupoTesoreriaActualiza(sapGrupoTesoreria))
                return RedirectToAction(nameof(SapGrupoTesoreriaCon));

            return SapGrupoTesoreriaActualizaCap(sapGrupoTesoreria);
        }
        /// <summary>
        /// Elimina.
        /// </summary>
        public IActionResult SapGrupoTesoreriaElimina(Int32 indice)
        {
            NSapGruposTesoreria.SapGrupoTesoreriaElimina(EVSapGruposTesoreria.SapGrupoTesoreriaPag.Pagina[indice]);
            return RedirectToAction(nameof(SapGrupoTesoreriaCon));
        }
        /// <summary>
        /// Exportacion de datos.
        /// </summary>
        public async Task<IActionResult> SapGrupoTesoreriaExporta()
        {
            EVSapGruposTesoreria.SapGrupoTesoreriaFiltro.ColumnaOrden = EVSapGruposTesoreria.SapGrupoTesoreriaColOrden;
            EVSapGruposTesoreria.SapGrupoTesoreriaFiltro.Columnas = new Dictionary<String, String>()
                                         {
                                             { nameof(ESapGrupoTesoreria.Activo), String.Empty },
                                             { nameof(ESapGrupoTesoreria.SapGrupoTesoreriaId), String.Empty },
                                             { nameof(ESapGrupoTesoreria.SapGrupoTesoreriaNombre), String.Empty }
                                         };

            MEDatosArchivo vDA = NSapGruposTesoreria.SapGrupoTesoreriaExporta(EVSapGruposTesoreria.SapGrupoTesoreriaFiltro);
            EVSapGruposTesoreria.SapGrupoTesoreriaFiltro.Columnas = null;
            if (NSapGruposTesoreria.Mensajes.Ok)
                return await base.MEnviaArchivoACliente(NSapGruposTesoreria.Mensajes, vDA);

            return RedirectToAction(nameof(SapGrupoTesoreriaCon));
        }
        #endregion

        #region Funciones
        /// <summary>
        /// Captura.
        /// </summary>
        private IActionResult SapGrupoTesoreriaCaptura(ESapGrupoTesoreria sapGrupoTesoreria)
        {
            ViewBag.Mensajes = NSapGruposTesoreria.Mensajes.Copy();
            ViewBag.Accion = EVSapGruposTesoreria.Accion;
            ViewBag.Reglas = EVSapGruposTesoreria.SapGrupoTesoreriaReglas;

            return ViewCap(nameof(SapGrupoTesoreriaCaptura), sapGrupoTesoreria);
        }
        #endregion

        #region Acciones de Paginacion Orden y Filtro
        /// <summary>
        /// Control de paginacion.
        /// </summary>
        [MValidaSeg(nameof(SapGrupoTesoreriaInicia))]
        public IActionResult SapGrupoTesoreriaPaginacion(MEDatosPaginador datPag)
        {
            EVSapGruposTesoreria.SapGrupoTesoreriaPag.DatPag = datPag;
            return RedirectToAction(nameof(SapGrupoTesoreriaCon));
        }
        /// <summary>
        /// Control de orden.
        /// </summary>
        [MValidaSeg(nameof(SapGrupoTesoreriaInicia))]
        public IActionResult SapGrupoTesoreriaOrdena(String orden)
        {
            EVSapGruposTesoreria.SapGrupoTesoreriaColOrden = orden;
            return RedirectToAction(nameof(SapGrupoTesoreriaCon));
        }
        /// <summary>
        /// Control de filtro.
        /// </summary>
        [MValidaSeg(nameof(SapGrupoTesoreriaInicia))]
        public IActionResult SapGrupoTesoreriaFiltra(ESapGrupoTesoreriaFiltro filtro)
        {
            EVSapGruposTesoreria.SapGrupoTesoreriaFiltro = filtro;
            return RedirectToAction(nameof(SapGrupoTesoreriaCon));
        }
        /// <summary>
        /// Limpia filtros.
        /// </summary>
        [MValidaSeg(nameof(SapGrupoTesoreriaInicia))]
        public IActionResult SapGrupoTesoreriaLimpiaFiltros()
        {
            EVSapGruposTesoreria.SapGrupoTesoreriaFiltro = new ESapGrupoTesoreriaFiltro();
            return RedirectToAction(nameof(SapGrupoTesoreriaCon));
        }
        #endregion

        #endregion
    }
}
