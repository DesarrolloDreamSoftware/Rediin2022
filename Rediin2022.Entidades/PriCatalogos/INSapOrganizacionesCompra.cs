using DSMetodNetX.Entidades;
using DSMetodNetX.Negocio;
using System;
using System.Collections.Generic;

namespace Rediin2022.Entidades.PriCatalogos
{
    /// <summary>
    /// Interfaz de negocio.
    /// </summary>
    public interface INSapOrganizacionesCompra : IMCtrMensajes
    {
        #region Funciones

        #region SapOrganizacionCompra (SapOrganizacionesCompra)
        /// <summary>
        /// Consulta paginada de la entidad SapOrganizacionCompra.
        /// </summary>
        ESapOrganizacionCompraPag SapOrganizacionCompraPag(ESapOrganizacionCompraFiltro sapOrganizacionCompraFiltro);
        /// <summary>
        /// Consulta por id de la entidad SapOrganizacionCompra.
        /// </summary>
        ESapOrganizacionCompra SapOrganizacionCompraXId(String sapOrganizacionCompraId);
        /// <summary>
        /// Consulta para combos de la entidad SapOrganizacionCompra.
        /// </summary>
        List<MEElemento> SapOrganizacionCompraCmb();
        /// <summary>
        /// Permite insertar la entidad SapOrganizacionCompra.
        /// </summary>
        Boolean SapOrganizacionCompraInserta(ESapOrganizacionCompra sapOrganizacionCompra);
        /// <summary>
        /// Permite actualizar la entidad SapOrganizacionCompra.
        /// </summary>
        Boolean SapOrganizacionCompraActualiza(ESapOrganizacionCompra sapOrganizacionCompra);
        /// <summary>
        /// Permite eliminar la entidad SapOrganizacionCompra.
        /// </summary>
        Boolean SapOrganizacionCompraElimina(ESapOrganizacionCompra sapOrganizacionCompra);
        /// <summary>
        /// Exporta datos a Excel.
        /// </summary>
        MEDatosArchivo SapOrganizacionCompraExporta(ESapOrganizacionCompraFiltro sapOrganizacionCompraFiltro);
        /// <summary>
        /// Reglas de negocio de la entidad SapOrganizacionCompra.
        /// </summary>
        List<MEReglaNeg> SapOrganizacionCompraReglas();
        #endregion

        #endregion
    }
}
