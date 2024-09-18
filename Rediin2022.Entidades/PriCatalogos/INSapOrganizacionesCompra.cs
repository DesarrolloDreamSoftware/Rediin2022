using DSMetodNetX.Entidades;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rediin2022.Entidades.PriCatalogos;

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
    Task<ESapOrganizacionCompraPag> SapOrganizacionCompraPag(ESapOrganizacionCompraFiltro sapOrganizacionCompraFiltro);
    /// <summary>
    /// Consulta por id de la entidad SapOrganizacionCompra.
    /// </summary>
    Task<ESapOrganizacionCompra> SapOrganizacionCompraXId(String sapOrganizacionCompraId);
    /// <summary>
    /// Consulta para combos de la entidad SapOrganizacionCompra.
    /// </summary>
    Task<List<MEElemento>> SapOrganizacionCompraCmb();
    /// <summary>
    /// Permite insertar la entidad SapOrganizacionCompra.
    /// </summary>
    Task<Boolean> SapOrganizacionCompraInserta(ESapOrganizacionCompra sapOrganizacionCompra);
    /// <summary>
    /// Permite actualizar la entidad SapOrganizacionCompra.
    /// </summary>
    Task<Boolean> SapOrganizacionCompraActualiza(ESapOrganizacionCompra sapOrganizacionCompra);
    /// <summary>
    /// Permite eliminar la entidad SapOrganizacionCompra.
    /// </summary>
    Task<Boolean> SapOrganizacionCompraElimina(ESapOrganizacionCompra sapOrganizacionCompra);
    /// <summary>
    /// Exporta datos a Excel.
    /// </summary>
    Task<string> SapOrganizacionCompraExporta(ESapOrganizacionCompraFiltro sapOrganizacionCompraFiltro);
    /// <summary>
    /// Reglas de negocio de la entidad SapOrganizacionCompra.
    /// </summary>
    Task<List<MEReglaNeg>> SapOrganizacionCompraReglas();
    #endregion

    #endregion
}
