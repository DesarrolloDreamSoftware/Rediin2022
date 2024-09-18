using DSMetodNetX.Aplicacion;
using DSMetodNetX.Entidades;
using DSMetodNetX.Negocio;
using Rediin2022.Entidades.PriCatalogos;
using System;
using System.Collections.Generic;

namespace Rediin2022.Aplicacion.PriCatalogos
{
    /// <summary>
    /// APL Para conexion con un API.
    /// </summary>
    public class APLSapOrganizacionesCompra : MAplicacion, INSapOrganizacionesCompra
    {
        #region Constructores
        /// <summary>
        /// APL Para conexion con un API.
        /// </summary>
        public APLSapOrganizacionesCompra(IMApiCliente api) : base(api)
        {
        }
        #endregion

        #region Funciones

        #region SapOrganizacionCompra (SapOrganizacionesCompra)
        /// <summary>
        /// Consulta paginada de la entidad SapOrganizacionCompra.
        /// </summary>
        public ESapOrganizacionCompraPag SapOrganizacionCompraPag(ESapOrganizacionCompraFiltro sapOrganizacionCompraFiltro)
        {
            return Call<ESapOrganizacionCompraPag>(NomFn(), sapOrganizacionCompraFiltro);
        }
        /// <summary>
        /// Consulta por id de la entidad SapOrganizacionCompra.
        /// </summary>
        public ESapOrganizacionCompra SapOrganizacionCompraXId(String sapOrganizacionCompraId)
        {
            return Call<ESapOrganizacionCompra>(NomFn(),
                                                sapOrganizacionCompraId);
        }
        /// <summary>
        /// Consulta para combos de la entidad SapOrganizacionCompra.
        /// </summary>
        public List<MEElemento> SapOrganizacionCompraCmb()
        {
            return Call<List<MEElemento>>(NomFn());
        }
        /// <summary>
        /// Permite insertar la entidad SapOrganizacionCompra.
        /// </summary>
        public Boolean SapOrganizacionCompraInserta(ESapOrganizacionCompra sapOrganizacionCompra)
        {
            return Call<Boolean>(NomFn(), sapOrganizacionCompra);
        }
        /// <summary>
        /// Permite actualizar la entidad SapOrganizacionCompra.
        /// </summary>
        public Boolean SapOrganizacionCompraActualiza(ESapOrganizacionCompra sapOrganizacionCompra)
        {
            return Call<Boolean>(NomFn(), sapOrganizacionCompra);
        }
        /// <summary>
        /// Permite eliminar la entidad SapOrganizacionCompra.
        /// </summary>
        public Boolean SapOrganizacionCompraElimina(ESapOrganizacionCompra sapOrganizacionCompra)
        {
            return Call<Boolean>(NomFn(), sapOrganizacionCompra);
        }
        /// <summary>
        /// Exporta datos a Excel.
        /// </summary>
        public MEDatosArchivo SapOrganizacionCompraExporta(ESapOrganizacionCompraFiltro sapOrganizacionCompraFiltro)
        {
            return Call<MEDatosArchivo>(NomFn(),
                                        sapOrganizacionCompraFiltro);
        }
        /// <summary>
        /// Reglas de negocio de la entidad SapOrganizacionCompra.
        /// </summary>
        public List<MEReglaNeg> SapOrganizacionCompraReglas()
        {
            return Call<List<MEReglaNeg>>(NomFn());
        }
        #endregion

        #endregion
    }
}
