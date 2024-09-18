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
    public class NRSapGruposTesoreria : MNegRemoto, INSapGruposTesoreria
    {
        #region Constructores
        /// <summary>
        /// APL Para conexion con un API.
        /// </summary>
        public NRSapGruposTesoreria(IMApiCteNeg api) : base(api)
        {
        }
        #endregion

        #region Funciones

        #region SapGrupoTesoreria (SapGruposTesoreria)
        /// <summary>
        /// Consulta paginada de la entidad SapGrupoTesoreria.
        /// </summary>
        public async Task<ESapGrupoTesoreriaPag> SapGrupoTesoreriaPag(ESapGrupoTesoreriaFiltro sapGrupoTesoreriaFiltro)
        {
            return await CallAsync<ESapGrupoTesoreriaPag>(NomFn(), sapGrupoTesoreriaFiltro);
        }
        /// <summary>
        /// Consulta por id de la entidad SapGrupoTesoreria.
        /// </summary>
        public async Task<ESapGrupoTesoreria> SapGrupoTesoreriaXId(String sapGrupoTesoreriaId)
        {
            return await CallAsync<ESapGrupoTesoreria>(NomFn(),
                                                       sapGrupoTesoreriaId);
        }
        /// <summary>
        /// Consulta para combos de la entidad SapGrupoTesoreria.
        /// </summary>
        public async Task<List<MEElemento>> SapGrupoTesoreriaCmb()
        {
            return await CallAsync<List<MEElemento>>(NomFn());
        }
        /// <summary>
        /// Permite insertar la entidad SapGrupoTesoreria.
        /// </summary>
        public async Task<Boolean> SapGrupoTesoreriaInserta(ESapGrupoTesoreria sapGrupoTesoreria)
        {
            return await CallAsync<Boolean>(NomFn(), sapGrupoTesoreria);
        }
        /// <summary>
        /// Permite actualizar la entidad SapGrupoTesoreria.
        /// </summary>
        public async Task<Boolean> SapGrupoTesoreriaActualiza(ESapGrupoTesoreria sapGrupoTesoreria)
        {
            return await CallAsync<Boolean>(NomFn(), sapGrupoTesoreria);
        }
        /// <summary>
        /// Permite eliminar la entidad SapGrupoTesoreria.
        /// </summary>
        public async Task<Boolean> SapGrupoTesoreriaElimina(ESapGrupoTesoreria sapGrupoTesoreria)
        {
            return await CallAsync<Boolean>(NomFn(), sapGrupoTesoreria);
        }
        /// <summary>
        /// Exporta datos a Excel.
        /// </summary>
        public async Task<String> SapGrupoTesoreriaExporta(ESapGrupoTesoreriaFiltro sapGrupoTesoreriaFiltro)
        {
            return await CallAsync<String>(NomFn(),
                                           sapGrupoTesoreriaFiltro);
        }
        /// <summary>
        /// Reglas de negocio de la entidad SapGrupoTesoreria.
        /// </summary>
        public async Task<List<MEReglaNeg>> SapGrupoTesoreriaReglas()
        {
            return await CallAsync<List<MEReglaNeg>>(NomFn());
        }
        #endregion

        #endregion
    }
}
