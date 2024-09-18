using DSMetodNetX.Entidades;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rediin2022.Entidades.PriCatalogos;

/// <summary>
/// Interfaz de negocio.
/// </summary>
public interface INIdentificaciones : IMCtrMensajes
{
    #region Funciones

    #region Identificacion (Identificaciones)
    /// <summary>
    /// Consulta paginada de la entidad Identificacion.
    /// </summary>
    Task<EIdentificacionPag> IdentificacionPag(EIdentificacionFiltro identificacionFiltro);
    /// <summary>
    /// Consulta por id de la entidad Identificacion.
    /// </summary>
    Task<EIdentificacion> IdentificacionXId(Int64 identificacionId);
    /// <summary>
    /// Consulta para combos de la entidad Identificacion.
    /// </summary>
    Task<List<MEElemento>> IdentificacionCmb();
    /// <summary>
    /// Permite insertar la entidad Identificacion.
    /// </summary>
    Task<Int64> IdentificacionInserta(EIdentificacion identificacion);
    /// <summary>
    /// Permite actualizar la entidad Identificacion.
    /// </summary>
    Task<Boolean> IdentificacionActualiza(EIdentificacion identificacion);
    /// <summary>
    /// Permite eliminar la entidad Identificacion.
    /// </summary>
    Task<Boolean> IdentificacionElimina(EIdentificacion identificacion);
    /// <summary>
    /// Reglas de negocio de la entidad Identificacion.
    /// </summary>
    Task<List<MEReglaNeg>> IdentificacionReglas();
    #endregion

    #endregion
}
