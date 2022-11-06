using DSMetodNetX.Entidades;
using DSMetodNetX.Negocio;
using System;
using System.Collections.Generic;

namespace Rediin2022.Entidades.PriCatalogos
{
    public interface INAutorizaciones : IMCtrMensajes
    {
        #region Funciones

        #region Autorizacion (Autorizaciones)
        /// <summary>
        /// Consulta paginada de la entidad Autorizacion.
        /// </summary>
        EAutorizacionPag AutorizacionPag(EAutorizacionFiltro autorizacionFiltro);
        /// <summary>
        /// Consulta por id de la entidad Autorizacion.
        /// </summary>
        EAutorizacion AutorizacionXId(Int64 autorizacionId);
        /// <summary>
        /// Permite insertar la entidad Autorizacion.
        /// </summary>
        Int64 AutorizacionInserta(EAutorizacion autorizacion);
        /// <summary>
        /// Permite actualizar la entidad Autorizacion.
        /// </summary>
        Boolean AutorizacionActualiza(EAutorizacion autorizacion);
        /// <summary>
        /// Permite eliminar la entidad Autorizacion.
        /// </summary>
        Boolean AutorizacionElimina(EAutorizacion autorizacion);
        /// <summary>
        /// Reglas de negocio de la entidad Autorizacion.
        /// </summary>
        List<MEReglaNeg> AutorizacionReglas();
        #endregion

        #region AutorizacionUsuario (AutorizacionesUsuarios)
        /// <summary>
        /// Consulta paginada de la entidad AutorizacionUsuario.
        /// </summary>
        EAutorizacionUsuarioPag AutorizacionUsuarioPag(EAutorizacionUsuarioFiltro autorizacionUsuarioFiltro);
        /// <summary>
        /// Consulta por id de la entidad AutorizacionUsuario.
        /// </summary>
        EAutorizacionUsuario AutorizacionUsuarioXId(Int64 autorizacionUsuarioId);
        /// <summary>
        /// Permite insertar la entidad AutorizacionUsuario.
        /// </summary>
        Int64 AutorizacionUsuarioInserta(EAutorizacionUsuario autorizacionUsuario);
        /// <summary>
        /// Permite actualizar la entidad AutorizacionUsuario.
        /// </summary>
        Boolean AutorizacionUsuarioActualiza(EAutorizacionUsuario autorizacionUsuario);
        /// <summary>
        /// Permite eliminar la entidad AutorizacionUsuario.
        /// </summary>
        Boolean AutorizacionUsuarioElimina(EAutorizacionUsuario autorizacionUsuario);
        /// <summary>
        /// Reglas de negocio de la entidad AutorizacionUsuario.
        /// </summary>
        List<MEReglaNeg> AutorizacionUsuarioReglas();
        #endregion

        #endregion
    }
}
