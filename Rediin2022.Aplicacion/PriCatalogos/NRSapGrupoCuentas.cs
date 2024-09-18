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
    public class NRSapGrupoCuentas : MNegRemoto, INSapGrupoCuentas
    {
        #region Constructores
        /// <summary>
        /// APL Para conexion con un API.
        /// </summary>
        public NRSapGrupoCuentas(IMApiCteNeg api) : base(api)
        {
        }
        #endregion

        #region Funciones

        #region SapGrupoCuenta (SapGrupoCuentas)
        /// <summary>
        /// Consulta paginada de la entidad SapGrupoCuenta.
        /// </summary>
        public async Task<ESapGrupoCuentaPag> SapGrupoCuentaPag(ESapGrupoCuentaFiltro sapGrupoCuentaFiltro)
        {
            return await CallAsync<ESapGrupoCuentaPag>(NomFn(), sapGrupoCuentaFiltro);
        }
        /// <summary>
        /// Consulta por id de la entidad SapGrupoCuenta.
        /// </summary>
        public async Task<ESapGrupoCuenta> SapGrupoCuentaXId(String sapGrupoCuentaId)
        {
            return await CallAsync<ESapGrupoCuenta>(NomFn(),
                                                    sapGrupoCuentaId);
        }
        /// <summary>
        /// Consulta para combos de la entidad SapGrupoCuenta.
        /// </summary>
        public async Task<List<MEElemento>> SapGrupoCuentaCmb()
        {
            return await CallAsync<List<MEElemento>>(NomFn());
        }
        /// <summary>
        /// Permite insertar la entidad SapGrupoCuenta.
        /// </summary>
        public async Task<Boolean> SapGrupoCuentaInserta(ESapGrupoCuenta sapGrupoCuenta)
        {
            return await CallAsync<Boolean>(NomFn(), sapGrupoCuenta);
        }
        /// <summary>
        /// Permite actualizar la entidad SapGrupoCuenta.
        /// </summary>
        public async Task<Boolean> SapGrupoCuentaActualiza(ESapGrupoCuenta sapGrupoCuenta)
        {
            return await CallAsync<Boolean>(NomFn(), sapGrupoCuenta);
        }
        /// <summary>
        /// Permite eliminar la entidad SapGrupoCuenta.
        /// </summary>
        public async Task<Boolean> SapGrupoCuentaElimina(ESapGrupoCuenta sapGrupoCuenta)
        {
            return await CallAsync<Boolean>(NomFn(), sapGrupoCuenta);
        }
        /// <summary>
        /// Exporta datos a Excel.
        /// </summary>
        public async Task<String> SapGrupoCuentaExporta(ESapGrupoCuentaFiltro sapGrupoCuentaFiltro)
        {
            return await CallAsync<String>(NomFn(),
                                           sapGrupoCuentaFiltro);
        }
        /// <summary>
        /// Reglas de negocio de la entidad SapGrupoCuenta.
        /// </summary>
        public async Task<List<MEReglaNeg>> SapGrupoCuentaReglas()
        {
            return await CallAsync<List<MEReglaNeg>>(NomFn());
        }
        #endregion

        #endregion
    }
}
