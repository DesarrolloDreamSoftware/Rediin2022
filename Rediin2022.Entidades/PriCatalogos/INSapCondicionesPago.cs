using DSMetodNetX.Entidades;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rediin2022.Entidades.PriCatalogos;

/// <summary>
/// Interfaz de negocio.
/// </summary>
public interface INSapCondicionesPago : IMCtrMensajes
{
    #region Funciones

    #region SapCondicionPago (SapCondicionesPago)
    /// <summary>
    /// Consulta paginada de la entidad SapCondicionPago.
    /// </summary>
    Task<ESapCondicionPagoPag> SapCondicionPagoPag(ESapCondicionPagoFiltro sapCondicionPagoFiltro);
    /// <summary>
    /// Consulta por id de la entidad SapCondicionPago.
    /// </summary>
    Task<ESapCondicionPago> SapCondicionPagoXId(String sapCondicionPagoId);
    /// <summary>
    /// Consulta para combos de la entidad SapCondicionPago.
    /// </summary>
    Task<List<MEElemento>> SapCondicionPagoCmb();
    /// <summary>
    /// Permite insertar la entidad SapCondicionPago.
    /// </summary>
    Task<Boolean> SapCondicionPagoInserta(ESapCondicionPago sapCondicionPago);
    /// <summary>
    /// Permite actualizar la entidad SapCondicionPago.
    /// </summary>
    Task<Boolean> SapCondicionPagoActualiza(ESapCondicionPago sapCondicionPago);
    /// <summary>
    /// Permite eliminar la entidad SapCondicionPago.
    /// </summary>
    Task<Boolean> SapCondicionPagoElimina(ESapCondicionPago sapCondicionPago);
    /// <summary>
    /// Exporta datos a Excel.
    /// </summary>
    Task<String> SapCondicionPagoExporta(ESapCondicionPagoFiltro sapCondicionPagoFiltro);
    /// <summary>
    /// Reglas de negocio de la entidad SapCondicionPago.
    /// </summary>
    Task<List<MEReglaNeg>> SapCondicionPagoReglas();
    #endregion

    #endregion
}
