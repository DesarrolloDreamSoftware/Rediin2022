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
    public class NRSapCondicionesPago : MNegRemoto, INSapCondicionesPago
    {
        #region Constructores
        /// <summary>
        /// APL Para conexion con un API.
        /// </summary>
        public NRSapCondicionesPago(IMApiCteNeg api) : base(api)
        {
        }
        #endregion

        #region Funciones

        #region SapCondicionPago (SapCondicionesPago)
        /// <summary>
        /// Consulta paginada de la entidad SapCondicionPago.
        /// </summary>
        public async Task<ESapCondicionPagoPag> SapCondicionPagoPag(ESapCondicionPagoFiltro sapCondicionPagoFiltro)
        {
            return await CallAsync<ESapCondicionPagoPag>(NomFn(), sapCondicionPagoFiltro);
        }
        /// <summary>
        /// Consulta por id de la entidad SapCondicionPago.
        /// </summary>
        public async Task<ESapCondicionPago> SapCondicionPagoXId(String sapCondicionPagoId)
        {
            return await CallAsync<ESapCondicionPago>(NomFn(),
                                                      sapCondicionPagoId);
        }
        /// <summary>
        /// Consulta para combos de la entidad SapCondicionPago.
        /// </summary>
        public async Task<List<MEElemento>> SapCondicionPagoCmb()
        {
            return await CallAsync<List<MEElemento>>(NomFn());
        }
        /// <summary>
        /// Permite insertar la entidad SapCondicionPago.
        /// </summary>
        public async Task<Boolean> SapCondicionPagoInserta(ESapCondicionPago sapCondicionPago)
        {
            return await CallAsync<Boolean>(NomFn(), sapCondicionPago);
        }
        /// <summary>
        /// Permite actualizar la entidad SapCondicionPago.
        /// </summary>
        public async Task<Boolean> SapCondicionPagoActualiza(ESapCondicionPago sapCondicionPago)
        {
            return await CallAsync<Boolean>(NomFn(), sapCondicionPago);
        }
        /// <summary>
        /// Permite eliminar la entidad SapCondicionPago.
        /// </summary>
        public async Task<Boolean> SapCondicionPagoElimina(ESapCondicionPago sapCondicionPago)
        {
            return await CallAsync<Boolean>(NomFn(), sapCondicionPago);
        }
        /// <summary>
        /// Exporta datos a Excel.
        /// </summary>
        public async Task<String> SapCondicionPagoExporta(ESapCondicionPagoFiltro sapCondicionPagoFiltro)
        {
            return await CallAsync<String>(NomFn(),
                                           sapCondicionPagoFiltro);
        }
        /// <summary>
        /// Reglas de negocio de la entidad SapCondicionPago.
        /// </summary>
        public async Task<List<MEReglaNeg>> SapCondicionPagoReglas()
        {
            return await CallAsync<List<MEReglaNeg>>(NomFn());
        }
        #endregion

        #endregion
    }
}
