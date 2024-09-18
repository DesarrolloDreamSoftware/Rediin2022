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
        public ESapTratamientoPag SapTratamientoPag(ESapTratamientoFiltro sapTratamientoFiltro)
        {
            return NSapTratamientos.SapTratamientoPag(sapTratamientoFiltro);
        }
        /// <summary>
        /// Consulta por id de la entidad SapTratamiento.
        /// </summary>
        [HttpGet("{sapTratamientoId}")]
        public ESapTratamiento SapTratamientoXId(String sapTratamientoId)
        {
            return NSapTratamientos.SapTratamientoXId(sapTratamientoId);
        }
        /// <summary>
        /// Consulta para combos de la entidad SapTratamiento.
        /// </summary>
        public List<MEElemento> SapTratamientoCmb()
        {
            return NSapTratamientos.SapTratamientoCmb();
        }
        /// <summary>
        /// Permite insertar la entidad SapTratamiento.
        /// </summary>
        public Boolean SapTratamientoInserta(ESapTratamiento sapTratamiento)
        {
            return NSapTratamientos.SapTratamientoInserta(sapTratamiento);
        }
        /// <summary>
        /// Permite actualizar la entidad SapTratamiento.
        /// </summary>
        public Boolean SapTratamientoActualiza(ESapTratamiento sapTratamiento)
        {
            return NSapTratamientos.SapTratamientoActualiza(sapTratamiento);
        }
        /// <summary>
        /// Permite eliminar la entidad SapTratamiento.
        /// </summary>
        public Boolean SapTratamientoElimina(ESapTratamiento sapTratamiento)
        {
            return NSapTratamientos.SapTratamientoElimina(sapTratamiento);
        }
        /// <summary>
        /// Exporta datos a Excel.
        /// </summary>
        public MEDatosArchivo SapTratamientoExporta(ESapTratamientoFiltro sapTratamientoFiltro)
        {
            return NSapTratamientos.SapTratamientoExporta(sapTratamientoFiltro);
        }
        /// <summary>
        /// Reglas de negocio de la entidad SapTratamiento.
        /// </summary>
        public List<MEReglaNeg> SapTratamientoReglas()
        {
            return NSapTratamientos.SapTratamientoReglas();
        }
        #endregion

        #endregion
    }
}
