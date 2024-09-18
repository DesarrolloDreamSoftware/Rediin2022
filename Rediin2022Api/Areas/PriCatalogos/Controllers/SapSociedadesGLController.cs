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
    public class SapSociedadesGLController : MControllerApiPri, INSapSociedadesGL
    {
        #region Contructores
        /// <summary>
        /// API que expone el negocio.
        /// </summary>
        public SapSociedadesGLController(INSapSociedadesGL nSapSociedadesGL)
        {
            NSapSociedadesGL = nSapSociedadesGL;
        }
        #endregion

        #region Propiedades
        /// <summary>
        /// Negocio.
        /// </summary>
        public INSapSociedadesGL NSapSociedadesGL { get; }
        /// <summary>
        /// Control de mensajes.
        /// </summary>
        public IMMensajes Mensajes
        {
            get { return NSapSociedadesGL.Mensajes; }
        }
        #endregion

        #region Funciones

        #region SapSociedadGL (SapSociedadesGL)
        /// <summary>
        /// Consulta paginada de la entidad SapSociedadGL.
        /// </summary>
        public async Task<ESapSociedadGLPag> SapSociedadGLPag(ESapSociedadGLFiltro sapSociedadGLFiltro)
        {
            return await NSapSociedadesGL.SapSociedadGLPag(sapSociedadGLFiltro);
        }
        /// <summary>
        /// Consulta por id de la entidad SapSociedadGL.
        /// </summary>
        [HttpGet("{sapSociedadGLId}")]
        public async Task<ESapSociedadGL> SapSociedadGLXId(String sapSociedadGLId)
        {
            return await NSapSociedadesGL.SapSociedadGLXId(sapSociedadGLId);
        }
        /// <summary>
        /// Consulta para combos de la entidad SapSociedadGL.
        /// </summary>
        public async Task<List<MEElemento>> SapSociedadGLCmb()
        {
            return await NSapSociedadesGL.SapSociedadGLCmb();
        }
        /// <summary>
        /// Permite insertar la entidad SapSociedadGL.
        /// </summary>
        public async Task<Boolean> SapSociedadGLInserta(ESapSociedadGL sapSociedadGL)
        {
            return await NSapSociedadesGL.SapSociedadGLInserta(sapSociedadGL);
        }
        /// <summary>
        /// Permite actualizar la entidad SapSociedadGL.
        /// </summary>
        public async Task<Boolean> SapSociedadGLActualiza(ESapSociedadGL sapSociedadGL)
        {
            return await NSapSociedadesGL.SapSociedadGLActualiza(sapSociedadGL);
        }
        /// <summary>
        /// Permite eliminar la entidad SapSociedadGL.
        /// </summary>
        public async Task<Boolean> SapSociedadGLElimina(ESapSociedadGL sapSociedadGL)
        {
            return await NSapSociedadesGL.SapSociedadGLElimina(sapSociedadGL);
        }
        /// <summary>
        /// Exporta datos a Excel.
        /// </summary>
        public async Task<string> SapSociedadGLExporta(ESapSociedadGLFiltro sapSociedadGLFiltro)
        {
            return await NSapSociedadesGL.SapSociedadGLExporta(sapSociedadGLFiltro);
        }
        /// <summary>
        /// Reglas de negocio de la entidad SapSociedadGL.
        /// </summary>
        public async Task<List<MEReglaNeg>> SapSociedadGLReglas()
        {
            return await NSapSociedadesGL.SapSociedadGLReglas();
        }
        #endregion

        #endregion
    }
}
