using DSMetodNetX.Api.Seguridad;
using DSMetodNetX.Entidades;
using DSMetodNetX.Negocio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Rediin2022.Entidades.PriCatalogos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rediin2022Api.Areas.PriCatalogos.Controllers
{
    /// <summary>
    /// API que expone el negocio.
    /// </summary>
    [Route("ApiV1/PriCatalogos/[controller]/[action]")]
    public class SapTratamientosController : MControllerApiPri, INSapTratamientos
    {
        #region Contructores
        /// <summary>
        /// API que expone el negocio.
        /// </summary>
        public SapTratamientosController(INSapTratamientos nSapTratamientos)
        {
            NSapTratamientos = nSapTratamientos;
        }
        #endregion

        #region Propiedades
        /// <summary>
        /// Negocio.
        /// </summary>
        public INSapTratamientos NSapTratamientos { get; }
        /// <summary>
        /// Control de mensajes.
        /// </summary>
        public IMMensajes Mensajes
        {
            get { return NSapTratamientos.Mensajes; }
        }
        #endregion

        #region Funciones

        #region SapTratamiento (SapTratamientos)
        /// <summary>
        /// Consulta paginada de la entidad SapTratamiento.
        /// </summary>
        public async Task<ESapTratamientoPag> SapTratamientoPag(ESapTratamientoFiltro sapTratamientoFiltro)
        {
            return await NSapTratamientos.SapTratamientoPag(sapTratamientoFiltro);
        }
        /// <summary>
        /// Consulta por id de la entidad SapTratamiento.
        /// </summary>
        [HttpGet("{sapTratamientoId}")]
        public async Task<ESapTratamiento> SapTratamientoXId(String sapTratamientoId)
        {
            return await NSapTratamientos.SapTratamientoXId(sapTratamientoId);
        }
        /// <summary>
        /// Consulta para combos de la entidad SapTratamiento.
        /// </summary>
        public async Task<List<MEElemento>> SapTratamientoCmb()
        {
            return await NSapTratamientos.SapTratamientoCmb();
        }
        /// <summary>
        /// Permite insertar la entidad SapTratamiento.
        /// </summary>
        public async Task<Boolean> SapTratamientoInserta(ESapTratamiento sapTratamiento)
        {
            return await NSapTratamientos.SapTratamientoInserta(sapTratamiento);
        }
        /// <summary>
        /// Permite actualizar la entidad SapTratamiento.
        /// </summary>
        public async Task<Boolean> SapTratamientoActualiza(ESapTratamiento sapTratamiento)
        {
            return await NSapTratamientos.SapTratamientoActualiza(sapTratamiento);
        }
        /// <summary>
        /// Permite eliminar la entidad SapTratamiento.
        /// </summary>
        public async Task<Boolean> SapTratamientoElimina(ESapTratamiento sapTratamiento)
        {
            return await NSapTratamientos.SapTratamientoElimina(sapTratamiento);
        }
        /// <summary>
        /// Exporta datos a Excel.
        /// </summary>
        public async Task<string> SapTratamientoExporta(ESapTratamientoFiltro sapTratamientoFiltro)
        {
            return await NSapTratamientos.SapTratamientoExporta(sapTratamientoFiltro);
        }
        /// <summary>
        /// Reglas de negocio de la entidad SapTratamiento.
        /// </summary>
        public async Task<List<MEReglaNeg>> SapTratamientoReglas()
        {
            return await NSapTratamientos.SapTratamientoReglas();
        }
        #endregion

        #endregion
    }
}
