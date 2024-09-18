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
        public async Task<ESapSociedadPag> SapSociedadPag(ESapSociedadFiltro sapSociedadFiltro)
        {
            return await NSapSociedades.SapSociedadPag(sapSociedadFiltro);
        }
        /// <summary>
        /// Consulta por id de la entidad SapSociedad.
        /// </summary>
        [HttpGet("{sapSociedadId}")]
        public async Task<ESapSociedad> SapSociedadXId(String sapSociedadId)
        {
            return await NSapSociedades.SapSociedadXId(sapSociedadId);
        }
        /// <summary>
        /// Consulta para combos de la entidad SapSociedad.
        /// </summary>
        public async Task<List<MEElemento>> SapSociedadCmb()
        {
            return await NSapSociedades.SapSociedadCmb();
        }
        /// <summary>
        /// Permite insertar la entidad SapSociedad.
        /// </summary>
        public async Task<Boolean> SapSociedadInserta(ESapSociedad sapSociedad)
        {
            return await NSapSociedades.SapSociedadInserta(sapSociedad);
        }
        /// <summary>
        /// Permite actualizar la entidad SapSociedad.
        /// </summary>
        public async Task<Boolean> SapSociedadActualiza(ESapSociedad sapSociedad)
        {
            return await NSapSociedades.SapSociedadActualiza(sapSociedad);
        }
        /// <summary>
        /// Permite eliminar la entidad SapSociedad.
        /// </summary>
        public async Task<Boolean> SapSociedadElimina(ESapSociedad sapSociedad)
        {
            return await NSapSociedades.SapSociedadElimina(sapSociedad);
        }
        /// <summary>
        /// Exporta datos a Excel.
        /// </summary>
        public async Task<string> SapSociedadExporta(ESapSociedadFiltro sapSociedadFiltro)
        {
            return await NSapSociedades.SapSociedadExporta(sapSociedadFiltro);
        }
        /// <summary>
        /// Reglas de negocio de la entidad SapSociedad.
        /// </summary>
        public async Task<List<MEReglaNeg>> SapSociedadReglas()
        {
            return await NSapSociedades.SapSociedadReglas();
        }
        #endregion

        #endregion
    }
}
