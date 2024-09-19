using DSMetodNetX.Entidades;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rediin2022.Entidades.PriCatalogos;

/// <summary>
/// Interfaz de negocio.
/// </summary>
public interface INIncoterms : IMCtrMensajes
{
    #region Funciones

    #region Incoterm (Incoterms)
    /// <summary>
    /// Consulta paginada de la entidad Incoterm.
    /// </summary>
    Task<EIncotermPag> IncotermPag(EIncotermFiltro incotermFiltro);
    /// <summary>
    /// Consulta por id de la entidad Incoterm.
    /// </summary>
    Task<EIncoterm> IncotermXId(Int64 incotermId);
    /// <summary>
    /// Consulta para combos de la entidad Incoterm.
    /// </summary>
    Task<List<MEElemento>> IncotermCmb();
    /// <summary>
    /// Permite insertar la entidad Incoterm.
    /// </summary>
    Task<Int64> IncotermInserta(EIncoterm incoterm);
    /// <summary>
    /// Permite actualizar la entidad Incoterm.
    /// </summary>
    Task<Boolean> IncotermActualiza(EIncoterm incoterm);
    /// <summary>
    /// Permite eliminar la entidad Incoterm.
    /// </summary>
    Task<Boolean> IncotermElimina(EIncoterm incoterm);
    /// <summary>
    /// Reglas de negocio de la entidad Incoterm.
    /// </summary>
    Task<List<MEReglaNeg>> IncotermReglas();
    #endregion

    #endregion
}
