using DSMetodNetX.Entidades;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rediin2022.Entidades.PriCatalogos;

/// <summary>
/// Interfaz de negocio.
/// </summary>
public interface INSapBancos : IMCtrMensajes
{
    #region Funciones

    #region SapBanco (SapBancos)
    /// <summary>
    /// Consulta paginada de la entidad SapBanco.
    /// </summary>
    Task<ESapBancoPag> SapBancoPag(ESapBancoFiltro sapBancoFiltro);
    /// <summary>
    /// Consulta por id de la entidad SapBanco.
    /// </summary>
    Task<ESapBanco> SapBancoXId(String sapBancoId);
    /// <summary>
    /// Consulta para combos de la entidad SapBanco.
    /// </summary>
    Task<List<MEElemento>> SapBancoCmb();
    /// <summary>
    /// Permite insertar la entidad SapBanco.
    /// </summary>
    Task<Boolean> SapBancoInserta(ESapBanco sapBanco);
    /// <summary>
    /// Permite actualizar la entidad SapBanco.
    /// </summary>
    Task<Boolean> SapBancoActualiza(ESapBanco sapBanco);
    /// <summary>
    /// Permite eliminar la entidad SapBanco.
    /// </summary>
    Task<Boolean> SapBancoElimina(ESapBanco sapBanco);
    /// <summary>
    /// Exporta datos a Excel.
    /// </summary>
    Task<string> SapBancoExporta(ESapBancoFiltro sapBancoFiltro);
    /// <summary>
    /// Reglas de negocio de la entidad SapBanco.
    /// </summary>
    Task<List<MEReglaNeg>> SapBancoReglas();
    #endregion

    #endregion
}
