using DSMetodNetX.Entidades;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rediin2022.Entidades.PriCatalogos;

/// <summary>
/// Interfaz de negocio.
/// </summary>
public interface INSapGruposTolerancia : IMCtrMensajes
{
    #region Funciones

    #region SapGrupoTolerancia (SapGruposTolerancia)
    /// <summary>
    /// Consulta paginada de la entidad SapGrupoTolerancia.
    /// </summary>
    Task<ESapGrupoToleranciaPag> SapGrupoToleranciaPag(ESapGrupoToleranciaFiltro sapGrupoToleranciaFiltro);
    /// <summary>
    /// Consulta por id de la entidad SapGrupoTolerancia.
    /// </summary>
    Task<ESapGrupoTolerancia> SapGrupoToleranciaXId(String sapGrupoToleranciaId);
    /// <summary>
    /// Consulta para combos de la entidad SapGrupoTolerancia.
    /// </summary>
    Task<List<MEElemento>> SapGrupoToleranciaCmb();
    /// <summary>
    /// Permite insertar la entidad SapGrupoTolerancia.
    /// </summary>
    Task<Boolean> SapGrupoToleranciaInserta(ESapGrupoTolerancia sapGrupoTolerancia);
    /// <summary>
    /// Permite actualizar la entidad SapGrupoTolerancia.
    /// </summary>
    Task<Boolean> SapGrupoToleranciaActualiza(ESapGrupoTolerancia sapGrupoTolerancia);
    /// <summary>
    /// Permite eliminar la entidad SapGrupoTolerancia.
    /// </summary>
    Task<Boolean> SapGrupoToleranciaElimina(ESapGrupoTolerancia sapGrupoTolerancia);
    /// <summary>
    /// Exporta datos a Excel.
    /// </summary>
    Task<string> SapGrupoToleranciaExporta(ESapGrupoToleranciaFiltro sapGrupoToleranciaFiltro);
    /// <summary>
    /// Reglas de negocio de la entidad SapGrupoTolerancia.
    /// </summary>
    Task<List<MEReglaNeg>> SapGrupoToleranciaReglas();
    #endregion

    #endregion
}
