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
    public class NRBancos : MNegRemoto, INBancos
    {
        #region Constructores
        /// <summary>
        /// APL Para conexion con un API.
        /// </summary>
        public NRBancos(IMApiCteNeg api) : base(api)
        {
        }
        #endregion

        #region Funciones

        #region Banco (Bancos)
        /// <summary>
        /// Consulta paginada de la entidad Banco.
        /// </summary>
        public async Task<EBancoPag> BancoPag(EBancoFiltro bancoFiltro)
        {
            return await CallAsync<EBancoPag>(NomFn(), bancoFiltro);
        }
        /// <summary>
        /// Consulta por id de la entidad Banco.
        /// </summary>
        public async Task<EBanco> BancoXId(Int64 bancoId)
        {
            return await CallAsync<EBanco>(NomFn(),
                                           bancoId);
        }
        /// <summary>
        /// Consulta para combos de la entidad Banco.
        /// </summary>
        public async Task<List<MEElemento>> BancoCmb()
        {
            return await CallAsync<List<MEElemento>>(NomFn());
        }
        /// <summary>
        /// Permite insertar la entidad Banco.
        /// </summary>
        public async Task<Int64> BancoInserta(EBanco banco)
        {
            return await CallAsync<Int64>(NomFn(), banco);
        }
        /// <summary>
        /// Permite actualizar la entidad Banco.
        /// </summary>
        public async Task<Boolean> BancoActualiza(EBanco banco)
        {
            return await CallAsync<Boolean>(NomFn(), banco);
        }
        /// <summary>
        /// Permite eliminar la entidad Banco.
        /// </summary>
        public async Task<Boolean> BancoElimina(EBanco banco)
        {
            return await CallAsync<Boolean>(NomFn(), banco);
        }
        /// <summary>
        /// Exporta datos a Excel.
        /// </summary>
        public async Task<String> BancoExporta(EBancoFiltro bancoFiltro)
        {
            return await CallAsync<String>(NomFn(),
                                           bancoFiltro);
        }
        /// <summary>
        /// Reglas de negocio de la entidad Banco.
        /// </summary>
        public async Task<List<MEReglaNeg>> BancoReglas()
        {
            return await CallAsync<List<MEReglaNeg>>(NomFn());
        }
        #endregion

        #endregion
    }
}
