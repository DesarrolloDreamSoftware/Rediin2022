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
    public class NRSapOrganizacionesCompra : MNegRemoto, INSapOrganizacionesCompra
    {
        #region Constructores
        /// <summary>
        /// APL Para conexion con un API.
        /// </summary>
        public NRSapOrganizacionesCompra(IMApiCteNeg api) : base(api)
        {
        }
        #endregion

        #region Funciones

        #region SapOrganizacionCompra (SapOrganizacionesCompra)
        /// <summary>
        /// Consulta paginada de la entidad SapOrganizacionCompra.
        /// </summary>
        public async Task<ESapOrganizacionCompraPag> SapOrganizacionCompraPag(ESapOrganizacionCompraFiltro sapOrganizacionCompraFiltro)
        {
            return await CallAsync<ESapOrganizacionCompraPag>(NomFn(), sapOrganizacionCompraFiltro);
        }
        /// <summary>
        /// Consulta por id de la entidad SapOrganizacionCompra.
        /// </summary>
        public async Task<ESapOrganizacionCompra> SapOrganizacionCompraXId(String sapOrganizacionCompraId)
        {
            return await CallAsync<ESapOrganizacionCompra>(NomFn(),
                                                           sapOrganizacionCompraId);
        }
        /// <summary>
        /// Consulta para combos de la entidad SapOrganizacionCompra.
        /// </summary>
        public async Task<List<MEElemento>> SapOrganizacionCompraCmb()
        {
            return await CallAsync<List<MEElemento>>(NomFn());
        }
        /// <summary>
        /// Permite insertar la entidad SapOrganizacionCompra.
        /// </summary>
        public async Task<Boolean> SapOrganizacionCompraInserta(ESapOrganizacionCompra sapOrganizacionCompra)
        {
            return await CallAsync<Boolean>(NomFn(), sapOrganizacionCompra);
        }
        /// <summary>
        /// Permite actualizar la entidad SapOrganizacionCompra.
        /// </summary>
        public async Task<Boolean> SapOrganizacionCompraActualiza(ESapOrganizacionCompra sapOrganizacionCompra)
        {
            return await CallAsync<Boolean>(NomFn(), sapOrganizacionCompra);
        }
        /// <summary>
        /// Permite eliminar la entidad SapOrganizacionCompra.
        /// </summary>
        public async Task<Boolean> SapOrganizacionCompraElimina(ESapOrganizacionCompra sapOrganizacionCompra)
        {
            return await CallAsync<Boolean>(NomFn(), sapOrganizacionCompra);
        }
        /// <summary>
        /// Exporta datos a Excel.
        /// </summary>
        public async Task<String> SapOrganizacionCompraExporta(ESapOrganizacionCompraFiltro sapOrganizacionCompraFiltro)
        {
            return await CallAsync<String>(NomFn(),
                                           sapOrganizacionCompraFiltro);
        }
        /// <summary>
        /// Reglas de negocio de la entidad SapOrganizacionCompra.
        /// </summary>
        public async Task<List<MEReglaNeg>> SapOrganizacionCompraReglas()
        {
            return await CallAsync<List<MEReglaNeg>>(NomFn());
        }
        #endregion

        #endregion
    }
}
