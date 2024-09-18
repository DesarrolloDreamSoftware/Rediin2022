using DSMetodNetX.Entidades;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rediin2022.Entidades.PriCatalogos;

/// <summary>
/// Interfaz de negocio.
/// </summary>
public interface INSapTratamientos : IMCtrMensajes
{
    #region Funciones

    #region SapTratamiento (SapTratamientos)
    /// <summary>
    /// Consulta paginada de la entidad SapTratamiento.
    /// </summary>
    Task<ESapTratamientoPag> SapTratamientoPag(ESapTratamientoFiltro sapTratamientoFiltro);
    /// <summary>
    /// Consulta por id de la entidad SapTratamiento.
    /// </summary>
    Task<ESapTratamiento> SapTratamientoXId(String sapTratamientoId);
    /// <summary>
    /// Consulta para combos de la entidad SapTratamiento.
    /// </summary>
    Task<List<MEElemento>> SapTratamientoCmb();
    /// <summary>
    /// Permite insertar la entidad SapTratamiento.
    /// </summary>
    Task<Boolean> SapTratamientoInserta(ESapTratamiento sapTratamiento);
    /// <summary>
    /// Permite actualizar la entidad SapTratamiento.
    /// </summary>
    Task<Boolean> SapTratamientoActualiza(ESapTratamiento sapTratamiento);
    /// <summary>
    /// Permite eliminar la entidad SapTratamiento.
    /// </summary>
    Task<Boolean> SapTratamientoElimina(ESapTratamiento sapTratamiento);
    /// <summary>
    /// Exporta datos a Excel.
    /// </summary>
    Task<string> SapTratamientoExporta(ESapTratamientoFiltro sapTratamientoFiltro);
    /// <summary>
    /// Reglas de negocio de la entidad SapTratamiento.
    /// </summary>
    Task<List<MEReglaNeg>> SapTratamientoReglas();
    #endregion

    #endregion
}
