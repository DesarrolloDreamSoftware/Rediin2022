using DSMetodNetX.Entidades;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rediin2022.Entidades.PriCatalogos;

/// <summary>
/// Interfaz de negocio.
/// </summary>
public interface INSapGruposTesoreria : IMCtrMensajes
{
    #region Funciones

    #region SapGrupoTesoreria (SapGruposTesoreria)
    /// <summary>
    /// Consulta paginada de la entidad SapGrupoTesoreria.
    /// </summary>
    Task<ESapGrupoTesoreriaPag> SapGrupoTesoreriaPag(ESapGrupoTesoreriaFiltro sapGrupoTesoreriaFiltro);
    /// <summary>
    /// Consulta por id de la entidad SapGrupoTesoreria.
    /// </summary>
    Task<ESapGrupoTesoreria> SapGrupoTesoreriaXId(String sapGrupoTesoreriaId);
    /// <summary>
    /// Consulta para combos de la entidad SapGrupoTesoreria.
    /// </summary>
    Task<List<MEElemento>> SapGrupoTesoreriaCmb();
    /// <summary>
    /// Permite insertar la entidad SapGrupoTesoreria.
    /// </summary>
    Task<Boolean> SapGrupoTesoreriaInserta(ESapGrupoTesoreria sapGrupoTesoreria);
    /// <summary>
    /// Permite actualizar la entidad SapGrupoTesoreria.
    /// </summary>
    Task<Boolean> SapGrupoTesoreriaActualiza(ESapGrupoTesoreria sapGrupoTesoreria);
    /// <summary>
    /// Permite eliminar la entidad SapGrupoTesoreria.
    /// </summary>
    Task<Boolean> SapGrupoTesoreriaElimina(ESapGrupoTesoreria sapGrupoTesoreria);
    /// <summary>
    /// Exporta datos a Excel.
    /// </summary>
    Task<string> SapGrupoTesoreriaExporta(ESapGrupoTesoreriaFiltro sapGrupoTesoreriaFiltro);
    /// <summary>
    /// Reglas de negocio de la entidad SapGrupoTesoreria.
    /// </summary>
    Task<List<MEReglaNeg>> SapGrupoTesoreriaReglas();
    #endregion

    #endregion
}
