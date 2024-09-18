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
    public class NRSapViasPago : MNegRemoto, INSapViasPago
    {
        #region Constructores
        /// <summary>
        /// APL Para conexion con un API.
        /// </summary>
        public NRSapViasPago(IMApiCteNeg api) : base(api)
        {
        }
        #endregion

        #region Funciones

        #region SapViaPago (SapViasPago)
        /// <summary>
        /// Consulta paginada de la entidad SapViaPago.
        /// </summary>
        public async Task<ESapViaPagoPag> SapViaPagoPag(ESapViaPagoFiltro sapViaPagoFiltro)
        {
            return await CallAsync<ESapViaPagoPag>(NomFn(), sapViaPagoFiltro);
        }
        /// <summary>
        /// Consulta por id de la entidad SapViaPago.
        /// </summary>
        public async Task<ESapViaPago> SapViaPagoXId(String sapViaPagoId)
        {
            return await CallAsync<ESapViaPago>(NomFn(),
                                                sapViaPagoId);
        }
        /// <summary>
        /// Consulta para combos de la entidad SapViaPago.
        /// </summary>
        public async Task<List<MEElemento>> SapViaPagoCmb()
        {
            return await CallAsync<List<MEElemento>>(NomFn());
        }
        /// <summary>
        /// Permite insertar la entidad SapViaPago.
        /// </summary>
        public async Task<Boolean> SapViaPagoInserta(ESapViaPago sapViaPago)
        {
            return await CallAsync<Boolean>(NomFn(), sapViaPago);
        }
        /// <summary>
        /// Permite actualizar la entidad SapViaPago.
        /// </summary>
        public async Task<Boolean> SapViaPagoActualiza(ESapViaPago sapViaPago)
        {
            return await CallAsync<Boolean>(NomFn(), sapViaPago);
        }
        /// <summary>
        /// Permite eliminar la entidad SapViaPago.
        /// </summary>
        public async Task<Boolean> SapViaPagoElimina(ESapViaPago sapViaPago)
        {
            return await CallAsync<Boolean>(NomFn(), sapViaPago);
        }
        /// <summary>
        /// Exporta datos a Excel.
        /// </summary>
        public async Task<String> SapViaPagoExporta(ESapViaPagoFiltro sapViaPagoFiltro)
        {
            return await CallAsync<String>(NomFn(),
                                           sapViaPagoFiltro);
        }
        /// <summary>
        /// Reglas de negocio de la entidad SapViaPago.
        /// </summary>
        public async Task<List<MEReglaNeg>> SapViaPagoReglas()
        {
            return await CallAsync<List<MEReglaNeg>>(NomFn());
        }
        #endregion

        #endregion
    }
}
