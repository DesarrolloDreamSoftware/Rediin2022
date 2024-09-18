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
    public class NRSapBancos : MNegRemoto, INSapBancos
    {
        #region Constructores
        /// <summary>
        /// APL Para conexion con un API.
        /// </summary>
        public NRSapBancos(IMApiCteNeg api) : base(api)
        {
        }
        #endregion

        #region Funciones

        #region SapBanco (SapBancos)
        /// <summary>
        /// Consulta paginada de la entidad SapBanco.
        /// </summary>
        public async Task<ESapBancoPag> SapBancoPag(ESapBancoFiltro sapBancoFiltro)
        {
            return await CallAsync<ESapBancoPag>(NomFn(), sapBancoFiltro);
        }
        /// <summary>
        /// Consulta por id de la entidad SapBanco.
        /// </summary>
        public async Task<ESapBanco> SapBancoXId(String sapBancoId)
        {
            return await CallAsync<ESapBanco>(NomFn(),
                                              sapBancoId);
        }
        /// <summary>
        /// Consulta para combos de la entidad SapBanco.
        /// </summary>
        public async Task<List<MEElemento>> SapBancoCmb()
        {
            return await CallAsync<List<MEElemento>>(NomFn());
        }
        /// <summary>
        /// Permite insertar la entidad SapBanco.
        /// </summary>
        public async Task<Boolean> SapBancoInserta(ESapBanco sapBanco)
        {
            return await CallAsync<Boolean>(NomFn(), sapBanco);
        }
        /// <summary>
        /// Permite actualizar la entidad SapBanco.
        /// </summary>
        public async Task<Boolean> SapBancoActualiza(ESapBanco sapBanco)
        {
            return await CallAsync<Boolean>(NomFn(), sapBanco);
        }
        /// <summary>
        /// Permite eliminar la entidad SapBanco.
        /// </summary>
        public async Task<Boolean> SapBancoElimina(ESapBanco sapBanco)
        {
            return await CallAsync<Boolean>(NomFn(), sapBanco);
        }
        /// <summary>
        /// Exporta datos a Excel.
        /// </summary>
        public async Task<String> SapBancoExporta(ESapBancoFiltro sapBancoFiltro)
        {
            return await CallAsync<String>(NomFn(),
                                           sapBancoFiltro);
        }
        /// <summary>
        /// Reglas de negocio de la entidad SapBanco.
        /// </summary>
        public async Task<List<MEReglaNeg>> SapBancoReglas()
        {
            return await CallAsync<List<MEReglaNeg>>(NomFn());
        }
        #endregion

        #endregion
    }
}
