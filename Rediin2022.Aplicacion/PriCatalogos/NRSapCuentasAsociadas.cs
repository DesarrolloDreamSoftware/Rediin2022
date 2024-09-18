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
    public class NRSapCuentasAsociadas : MNegRemoto, INSapCuentasAsociadas
    {
        #region Constructores
        /// <summary>
        /// APL Para conexion con un API.
        /// </summary>
        public NRSapCuentasAsociadas(IMApiCteNeg api) : base(api)
        {
        }
        #endregion

        #region Funciones

        #region SapCuentaAsociada (SapCuentasAsociadas)
        /// <summary>
        /// Consulta paginada de la entidad SapCuentaAsociada.
        /// </summary>
        public async Task<ESapCuentaAsociadaPag> SapCuentaAsociadaPag(ESapCuentaAsociadaFiltro sapCuentaAsociadaFiltro)
        {
            return await CallAsync<ESapCuentaAsociadaPag>(NomFn(), sapCuentaAsociadaFiltro);
        }
        /// <summary>
        /// Consulta por id de la entidad SapCuentaAsociada.
        /// </summary>
        public async Task<ESapCuentaAsociada> SapCuentaAsociadaXId(String sapCuentaAsociadaId)
        {
            return await CallAsync<ESapCuentaAsociada>(NomFn(),
                                                       sapCuentaAsociadaId);
        }
        /// <summary>
        /// Consulta para combos de la entidad SapCuentaAsociada.
        /// </summary>
        public async Task<List<MEElemento>> SapCuentaAsociadaCmb()
        {
            return await CallAsync<List<MEElemento>>(NomFn());
        }
        /// <summary>
        /// Permite insertar la entidad SapCuentaAsociada.
        /// </summary>
        public async Task<Boolean> SapCuentaAsociadaInserta(ESapCuentaAsociada sapCuentaAsociada)
        {
            return await CallAsync<Boolean>(NomFn(), sapCuentaAsociada);
        }
        /// <summary>
        /// Permite actualizar la entidad SapCuentaAsociada.
        /// </summary>
        public async Task<Boolean> SapCuentaAsociadaActualiza(ESapCuentaAsociada sapCuentaAsociada)
        {
            return await CallAsync<Boolean>(NomFn(), sapCuentaAsociada);
        }
        /// <summary>
        /// Permite eliminar la entidad SapCuentaAsociada.
        /// </summary>
        public async Task<Boolean> SapCuentaAsociadaElimina(ESapCuentaAsociada sapCuentaAsociada)
        {
            return await CallAsync<Boolean>(NomFn(), sapCuentaAsociada);
        }
        /// <summary>
        /// Exporta datos a Excel.
        /// </summary>
        public async Task<String> SapCuentaAsociadaExporta(ESapCuentaAsociadaFiltro sapCuentaAsociadaFiltro)
        {
            return await CallAsync<String>(NomFn(),
                                           sapCuentaAsociadaFiltro);
        }
        /// <summary>
        /// Reglas de negocio de la entidad SapCuentaAsociada.
        /// </summary>
        public async Task<List<MEReglaNeg>> SapCuentaAsociadaReglas()
        {
            return await CallAsync<List<MEReglaNeg>>(NomFn());
        }
        #endregion

        #endregion
    }
}
