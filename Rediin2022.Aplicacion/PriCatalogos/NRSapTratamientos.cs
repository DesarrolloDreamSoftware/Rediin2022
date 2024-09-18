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
    public class NRSapTratamientos : MNegRemoto, INSapTratamientos
    {
        #region Constructores
        /// <summary>
        /// APL Para conexion con un API.
        /// </summary>
        public NRSapTratamientos(IMApiCteNeg api) : base(api)
        {
        }
        #endregion

        #region Funciones

        #region SapTratamiento (SapTratamientos)
        /// <summary>
        /// Consulta paginada de la entidad SapTratamiento.
        /// </summary>
        public async Task<ESapTratamientoPag> SapTratamientoPag(ESapTratamientoFiltro sapTratamientoFiltro)
        {
            return await CallAsync<ESapTratamientoPag>(NomFn(), sapTratamientoFiltro);
        }
        /// <summary>
        /// Consulta por id de la entidad SapTratamiento.
        /// </summary>
        public async Task<ESapTratamiento> SapTratamientoXId(String sapTratamientoId)
        {
            return await CallAsync<ESapTratamiento>(NomFn(),
                                                    sapTratamientoId);
        }
        /// <summary>
        /// Consulta para combos de la entidad SapTratamiento.
        /// </summary>
        public async Task<List<MEElemento>> SapTratamientoCmb()
        {
            return await CallAsync<List<MEElemento>>(NomFn());
        }
        /// <summary>
        /// Permite insertar la entidad SapTratamiento.
        /// </summary>
        public async Task<Boolean> SapTratamientoInserta(ESapTratamiento sapTratamiento)
        {
            return await CallAsync<Boolean>(NomFn(), sapTratamiento);
        }
        /// <summary>
        /// Permite actualizar la entidad SapTratamiento.
        /// </summary>
        public async Task<Boolean> SapTratamientoActualiza(ESapTratamiento sapTratamiento)
        {
            return await CallAsync<Boolean>(NomFn(), sapTratamiento);
        }
        /// <summary>
        /// Permite eliminar la entidad SapTratamiento.
        /// </summary>
        public async Task<Boolean> SapTratamientoElimina(ESapTratamiento sapTratamiento)
        {
            return await CallAsync<Boolean>(NomFn(), sapTratamiento);
        }
        /// <summary>
        /// Exporta datos a Excel.
        /// </summary>
        public async Task<String> SapTratamientoExporta(ESapTratamientoFiltro sapTratamientoFiltro)
        {
            return await CallAsync<String>(NomFn(),
                                           sapTratamientoFiltro);
        }
        /// <summary>
        /// Reglas de negocio de la entidad SapTratamiento.
        /// </summary>
        public async Task<List<MEReglaNeg>> SapTratamientoReglas()
        {
            return await CallAsync<List<MEReglaNeg>>(NomFn());
        }
        #endregion

        #endregion
    }
}
