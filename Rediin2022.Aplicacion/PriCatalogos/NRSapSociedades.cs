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
    public class NRSapSociedades : MNegRemoto, INSapSociedades
    {
        #region Constructores
        /// <summary>
        /// APL Para conexion con un API.
        /// </summary>
        public NRSapSociedades(IMApiCteNeg api) : base(api)
        {
        }
        #endregion

        #region Funciones

        #region SapSociedad (SapSociedades)
        /// <summary>
        /// Consulta paginada de la entidad SapSociedad.
        /// </summary>
        public async Task<ESapSociedadPag> SapSociedadPag(ESapSociedadFiltro sapSociedadFiltro)
        {
            return await CallAsync<ESapSociedadPag>(NomFn(), sapSociedadFiltro);
        }
        /// <summary>
        /// Consulta por id de la entidad SapSociedad.
        /// </summary>
        public async Task<ESapSociedad> SapSociedadXId(String sapSociedadId)
        {
            return await CallAsync<ESapSociedad>(NomFn(),
                                                 sapSociedadId);
        }
        /// <summary>
        /// Consulta para combos de la entidad SapSociedad.
        /// </summary>
        public async Task<List<MEElemento>> SapSociedadCmb()
        {
            return await CallAsync<List<MEElemento>>(NomFn());
        }
        /// <summary>
        /// Permite insertar la entidad SapSociedad.
        /// </summary>
        public async Task<Boolean> SapSociedadInserta(ESapSociedad sapSociedad)
        {
            return await CallAsync<Boolean>(NomFn(), sapSociedad);
        }
        /// <summary>
        /// Permite actualizar la entidad SapSociedad.
        /// </summary>
        public async Task<Boolean> SapSociedadActualiza(ESapSociedad sapSociedad)
        {
            return await CallAsync<Boolean>(NomFn(), sapSociedad);
        }
        /// <summary>
        /// Permite eliminar la entidad SapSociedad.
        /// </summary>
        public async Task<Boolean> SapSociedadElimina(ESapSociedad sapSociedad)
        {
            return await CallAsync<Boolean>(NomFn(), sapSociedad);
        }
        /// <summary>
        /// Exporta datos a Excel.
        /// </summary>
        public async Task<String> SapSociedadExporta(ESapSociedadFiltro sapSociedadFiltro)
        {
            return await CallAsync<String>(NomFn(),
                                           sapSociedadFiltro);
        }
        /// <summary>
        /// Reglas de negocio de la entidad SapSociedad.
        /// </summary>
        public async Task<List<MEReglaNeg>> SapSociedadReglas()
        {
            return await CallAsync<List<MEReglaNeg>>(NomFn());
        }
        #endregion

        #endregion
    }
}
