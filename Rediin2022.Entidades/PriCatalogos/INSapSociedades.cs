using DSMetodNetX.Entidades;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rediin2022.Entidades.PriCatalogos;

/// <summary>
/// Interfaz de negocio.
/// </summary>
public interface INSapSociedades : IMCtrMensajes
{
    #region Funciones

    #region SapSociedad (SapSociedades)
    /// <summary>
    /// Consulta paginada de la entidad SapSociedad.
    /// </summary>
    Task<ESapSociedadPag> SapSociedadPag(ESapSociedadFiltro sapSociedadFiltro);
    /// <summary>
    /// Consulta por id de la entidad SapSociedad.
    /// </summary>
    Task<ESapSociedad> SapSociedadXId(String sapSociedadId);
    /// <summary>
    /// Consulta para combos de la entidad SapSociedad.
    /// </summary>
    Task<List<MEElemento>> SapSociedadCmb();
    /// <summary>
    /// Permite insertar la entidad SapSociedad.
    /// </summary>
    Task<Boolean> SapSociedadInserta(ESapSociedad sapSociedad);
    /// <summary>
    /// Permite actualizar la entidad SapSociedad.
    /// </summary>
    Task<Boolean> SapSociedadActualiza(ESapSociedad sapSociedad);
    /// <summary>
    /// Permite eliminar la entidad SapSociedad.
    /// </summary>
    Task<Boolean> SapSociedadElimina(ESapSociedad sapSociedad);
    /// <summary>
    /// Exporta datos a Excel.
    /// </summary>
    Task<string> SapSociedadExporta(ESapSociedadFiltro sapSociedadFiltro);
    /// <summary>
    /// Reglas de negocio de la entidad SapSociedad.
    /// </summary>
    Task<List<MEReglaNeg>> SapSociedadReglas();
    #endregion

    #endregion
}
