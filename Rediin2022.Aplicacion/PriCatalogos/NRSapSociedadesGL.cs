using DSMetodNetX.Aplicacion;
using DSMetodNetX.Entidades;

using Rediin2022.Entidades.PriCatalogos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rediin2022.Aplicacion.PriCatalogos
{
    /// <summary>
    /// APL Para conexion con un API.
    /// </summary>
    public class NRSapSociedadesGL : MNegRemoto, INSapSociedadesGL
    {
        #region Constructores
        /// <summary>
        /// APL Para conexion con un API.
        /// </summary>
        public NRSapSociedadesGL(IMApiCteNeg api) : base(api)
        {
        }
        #endregion

        #region Funciones

        #region SapSociedadGL (SapSociedadesGL)
        /// <summary>
        /// Consulta paginada de la entidad SapSociedadGL.
        /// </summary>
        public async Task<ESapSociedadGLPag> SapSociedadGLPag(ESapSociedadGLFiltro sapSociedadGLFiltro)
        {
            return await CallAsync<ESapSociedadGLPag>(NomFn(), sapSociedadGLFiltro);
        }
        /// <summary>
        /// Consulta por id de la entidad SapSociedadGL.
        /// </summary>
        public async Task<ESapSociedadGL> SapSociedadGLXId(String sapSociedadGLId)
        {
            return await CallAsync<ESapSociedadGL>(NomFn(),
                                                   sapSociedadGLId);
        }
        /// <summary>
        /// Consulta para combos de la entidad SapSociedadGL.
        /// </summary>
        public async Task<List<MEElemento>> SapSociedadGLCmb()
        {
            return await CallAsync<List<MEElemento>>(NomFn());
        }
        /// <summary>
        /// Permite insertar la entidad SapSociedadGL.
        /// </summary>
        public async Task<Boolean> SapSociedadGLInserta(ESapSociedadGL sapSociedadGL)
        {
            return await CallAsync<Boolean>(NomFn(), sapSociedadGL);
        }
        /// <summary>
        /// Permite actualizar la entidad SapSociedadGL.
        /// </summary>
        public async Task<Boolean> SapSociedadGLActualiza(ESapSociedadGL sapSociedadGL)
        {
            return await CallAsync<Boolean>(NomFn(), sapSociedadGL);
        }
        /// <summary>
        /// Permite eliminar la entidad SapSociedadGL.
        /// </summary>
        public async Task<Boolean> SapSociedadGLElimina(ESapSociedadGL sapSociedadGL)
        {
            return await CallAsync<Boolean>(NomFn(), sapSociedadGL);
        }
        /// <summary>
        /// Exporta datos a Excel.
        /// </summary>
        public async Task<String> SapSociedadGLExporta(ESapSociedadGLFiltro sapSociedadGLFiltro)
        {
            return await CallAsync<String>(NomFn(),
                                           sapSociedadGLFiltro);
        }
        /// <summary>
        /// Reglas de negocio de la entidad SapSociedadGL.
        /// </summary>
        public async Task<List<MEReglaNeg>> SapSociedadGLReglas()
        {
            return await CallAsync<List<MEReglaNeg>>(NomFn());
        }
        #endregion

        #endregion
    }
}
