using DSMetodNetX.Entidades;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rediin2022.Entidades.PriCatalogos;

public interface INAutorizaciones : IMCtrMensajes
{
    #region Funciones

    #region Autorizacion (Autorizaciones)
    /// <summary>
    /// Consulta paginada de la entidad Autorizacion.
    /// </summary>
    Task<EAutorizacionPag> AutorizacionPag(EAutorizacionFiltro autorizacionFiltro);
    /// <summary>
    /// Consulta por id de la entidad Autorizacion.
    /// </summary>
    Task<EAutorizacion> AutorizacionXId(Int64 autorizacionId);
    /// <summary>
    /// Permite insertar la entidad Autorizacion.
    /// </summary>
    Task<Int64> AutorizacionInserta(EAutorizacion autorizacion);
    /// <summary>
    /// Permite actualizar la entidad Autorizacion.
    /// </summary>
    Task<Boolean> AutorizacionActualiza(EAutorizacion autorizacion);
    /// <summary>
    /// Permite eliminar la entidad Autorizacion.
    /// </summary>
    Task<Boolean> AutorizacionElimina(EAutorizacion autorizacion);
    /// <summary>
    /// Reglas de negocio de la entidad Autorizacion.
    /// </summary>
    Task<List<MEReglaNeg>> AutorizacionReglas();
    #endregion

    #region AutorizacionUsuario (AutorizacionesUsuarios)
    /// <summary>
    /// Consulta paginada de la entidad AutorizacionUsuario.
    /// </summary>
    Task<EAutorizacionUsuarioPag> AutorizacionUsuarioPag(EAutorizacionUsuarioFiltro autorizacionUsuarioFiltro);
    /// <summary>
    /// Consulta por id de la entidad AutorizacionUsuario.
    /// </summary>
    Task<EAutorizacionUsuario> AutorizacionUsuarioXId(Int64 autorizacionUsuarioId);
    /// <summary>
    /// Permite insertar la entidad AutorizacionUsuario.
    /// </summary>
    Task<Int64> AutorizacionUsuarioInserta(EAutorizacionUsuario autorizacionUsuario);
    /// <summary>
    /// Permite actualizar la entidad AutorizacionUsuario.
    /// </summary>
    Task<Boolean> AutorizacionUsuarioActualiza(EAutorizacionUsuario autorizacionUsuario);
    /// <summary>
    /// Permite eliminar la entidad AutorizacionUsuario.
    /// </summary>
    Task<Boolean> AutorizacionUsuarioElimina(EAutorizacionUsuario autorizacionUsuario);
    /// <summary>
    /// Reglas de negocio de la entidad AutorizacionUsuario.
    /// </summary>
    Task<List<MEReglaNeg>> AutorizacionUsuarioReglas();
    #endregion

    #endregion
}
