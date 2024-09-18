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
    public class SapSociedadesController : MControllerApiPri, INSapSociedades
    {
        #region Contructores
        /// <summary>
        /// API que expone el negocio.
        /// </summary>
        public SapSociedadesController(INSapSociedades nSapSociedades)
        {
            NSapSociedades = nSapSociedades;
        }
        #endregion

        #region Propiedades
        /// <summary>
        /// Negocio.
        /// </summary>
        public INSapSociedades NSapSociedades { get; }
        /// <summary>
        /// Control de mensajes.
        /// </summary>
        public IMMensajes Mensajes
        {
            get { return NSapSociedades.Mensajes; }
        }
        #endregion

        #region Funciones

        #region SapSociedad (SapSociedades)
        /// <summary>
        /// Consulta paginada de la entidad SapSociedad.
        /// </summary>
        public ESapSociedadPag SapSociedadPag(ESapSociedadFiltro sapSociedadFiltro)
        {
            return NSapSociedades.SapSociedadPag(sapSociedadFiltro);
        }
        /// <summary>
        /// Consulta por id de la entidad SapSociedad.
        /// </summary>
        [HttpGet("{sapSociedadId}")]
        public ESapSociedad SapSociedadXId(String sapSociedadId)
        {
            return NSapSociedades.SapSociedadXId(sapSociedadId);
        }
        /// <summary>
        /// Consulta para combos de la entidad SapSociedad.
        /// </summary>
        public List<MEElemento> SapSociedadCmb()
        {
            return NSapSociedades.SapSociedadCmb();
        }
        /// <summary>
        /// Permite insertar la entidad SapSociedad.
        /// </summary>
        public Boolean SapSociedadInserta(ESapSociedad sapSociedad)
        {
            return NSapSociedades.SapSociedadInserta(sapSociedad);
        }
        /// <summary>
        /// Permite actualizar la entidad SapSociedad.
        /// </summary>
        public Boolean SapSociedadActualiza(ESapSociedad sapSociedad)
        {
            return NSapSociedades.SapSociedadActualiza(sapSociedad);
        }
        /// <summary>
        /// Permite eliminar la entidad SapSociedad.
        /// </summary>
        public Boolean SapSociedadElimina(ESapSociedad sapSociedad)
        {
            return NSapSociedades.SapSociedadElimina(sapSociedad);
        }
        /// <summary>
        /// Exporta datos a Excel.
        /// </summary>
        public MEDatosArchivo SapSociedadExporta(ESapSociedadFiltro sapSociedadFiltro)
        {
            return NSapSociedades.SapSociedadExporta(sapSociedadFiltro);
        }
        /// <summary>
        /// Reglas de negocio de la entidad SapSociedad.
        /// </summary>
        public List<MEReglaNeg> SapSociedadReglas()
        {
            return NSapSociedades.SapSociedadReglas();
        }
        #endregion

        #endregion
    }
}
