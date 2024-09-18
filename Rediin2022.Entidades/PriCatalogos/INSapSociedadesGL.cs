using DSMetodNetX.Entidades;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rediin2022.Entidades.PriCatalogos;

/// <summary>
/// Interfaz de negocio.
/// </summary>
public interface INSapSociedadesGL : IMCtrMensajes
{
    #region Funciones

    #region SapSociedadGL (SapSociedadesGL)
    /// <summary>
    /// Consulta paginada de la entidad SapSociedadGL.
    /// </summary>
    Task<ESapSociedadGLPag> SapSociedadGLPag(ESapSociedadGLFiltro sapSociedadGLFiltro);
    /// <summary>
    /// Consulta por id de la entidad SapSociedadGL.
    /// </summary>
    Task<ESapSociedadGL> SapSociedadGLXId(String sapSociedadGLId);
    /// <summary>
    /// Consulta para combos de la entidad SapSociedadGL.
    /// </summary>
    Task<List<MEElemento>> SapSociedadGLCmb();
    /// <summary>
    /// Permite insertar la entidad SapSociedadGL.
    /// </summary>
    Task<Boolean> SapSociedadGLInserta(ESapSociedadGL sapSociedadGL);
    /// <summary>
    /// Permite actualizar la entidad SapSociedadGL.
    /// </summary>
    Task<Boolean> SapSociedadGLActualiza(ESapSociedadGL sapSociedadGL);
    /// <summary>
    /// Permite eliminar la entidad SapSociedadGL.
    /// </summary>
    Task<Boolean> SapSociedadGLElimina(ESapSociedadGL sapSociedadGL);
    /// <summary>
    /// Exporta datos a Excel.
    /// </summary>
    Task<string> SapSociedadGLExporta(ESapSociedadGLFiltro sapSociedadGLFiltro);
    /// <summary>
    /// Reglas de negocio de la entidad SapSociedadGL.
    /// </summary>
    Task<List<MEReglaNeg>> SapSociedadGLReglas();
    #endregion

    #endregion
}
