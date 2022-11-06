using DSEntityNetX.Common.Casting;
using DSEntityNetX.DataAccess;
using DSMetodNetX.AccesoDatos;
using DSMetodNetX.Entidades;
using DSMetodNetX.Idioma;
using Rediin2022.Entidades.Idioma;
using Rediin2022.Entidades.PriCatalogos;
using System;
using System.Collections.Generic;

namespace Rediin2022.AccesoDatos.PriCatalogos
{
    [Serializable]
    public class RAutorizaciones : MRepositorio
    {
        #region Constructores
        public RAutorizaciones(IMConexionEntidad conexion)
            : base(conexion)
        {
        }
        #endregion

        #region Funciones

        #region Autorizacion (Autorizaciones)
        /// <summary>
        /// Consulta paginada de la entidad Autorizacion.
        /// </summary>
        public EAutorizacionPag AutorizacionPag(EAutorizacionFiltro autorizacionFiltro)
        {
            EAutorizacionPag vAutorizacionPag = new EAutorizacionPag();

            _conexion.AddParamIn(MMetaDatos.establecimientoId, _conexion.UsuarioSesion.EstablecimientoId);
            _conexion.AddParamFilterTL(autorizacionFiltro);
            _conexion.LoadEntity<EAutorizacionPag>("NCAutorizacionesCP", vAutorizacionPag);
            if (!_mensajes.Ok)
                return vAutorizacionPag;

            base.MProcesaDatPag(autorizacionFiltro, vAutorizacionPag);

            _conexion.AddParamIn(MMetaDatos.establecimientoId, _conexion.UsuarioSesion.EstablecimientoId);
            _conexion.AddParamFilterPag(autorizacionFiltro);
            vAutorizacionPag.Pagina = _conexion.LoadEntities<EAutorizacion>("NCAutorizacionesCP");

            return vAutorizacionPag;
        }
        /// <summary>
        /// Consulta por id de la entidad Autorizacion.
        /// </summary>
        public EAutorizacion AutorizacionXId(Int64 autorizacionId)
        {
            _conexion.AddParamIn(nameof(autorizacionId), autorizacionId);
            return _conexion.LoadEntity<EAutorizacion>("NCAutorizacionesCI");
        }
        /// <summary>
        /// Permite insertar la entidad Autorizacion.
        /// </summary>
        protected Int64 AutorizacionInserta(EAutorizacion autorizacion)
        {
            _conexion.AddParamEntity(autorizacion, MAccionesBd.Inserta);
            Int64 vResultado = _conexion.ExecuteScalarVal("NCAutorizacionesIAE",
                                                          MensajesXId.AutorizacionNombre);
            return vResultado;
        }
        /// <summary>
        /// Permite actualizar la entidad Autorizacion.
        /// </summary>
        protected Boolean AutorizacionActualiza(EAutorizacion autorizacion)
        {
            _conexion.AddParamEntity(autorizacion, MAccionesBd.Actualiza);
            _conexion.ExecuteScalarVal("NCAutorizacionesIAE",
                                       MensajesXId.AutorizacionNombre);
            return _mensajes.Ok;
        }
        /// <summary>
        /// Permite eliminar la entidad Autorizacion.
        /// </summary>
        protected Boolean AutorizacionElimina(EAutorizacion autorizacion)
        {
            _conexion.AddParamEntity(autorizacion, MAccionesBd.Elimina);
            return _conexion.ExecuteNonQueryRet("NCAutorizacionesIAE");
        }
        #endregion

        #region AutorizacionUsuario (AutorizacionesUsuarios)
        /// <summary>
        /// Consulta paginada de la entidad AutorizacionUsuario.
        /// </summary>
        public EAutorizacionUsuarioPag AutorizacionUsuarioPag(EAutorizacionUsuarioFiltro autorizacionUsuarioFiltro)
        {
            EAutorizacionUsuarioPag vAutorizacionUsuarioPag = new EAutorizacionUsuarioPag();

            _conexion.AddParamFilterTL(autorizacionUsuarioFiltro);
            _conexion.LoadEntity<EAutorizacionUsuarioPag>("NCAutorizacionesUsuariosCP", vAutorizacionUsuarioPag);
            if (!_mensajes.Ok)
                return vAutorizacionUsuarioPag;

            base.MProcesaDatPag(autorizacionUsuarioFiltro, vAutorizacionUsuarioPag);

            _conexion.AddParamFilterPag(autorizacionUsuarioFiltro);
            vAutorizacionUsuarioPag.Pagina = _conexion.LoadEntities<EAutorizacionUsuario>("NCAutorizacionesUsuariosCP");

            return vAutorizacionUsuarioPag;
        }
        /// <summary>
        /// Consulta por id de la entidad AutorizacionUsuario.
        /// </summary>
        public EAutorizacionUsuario AutorizacionUsuarioXId(Int64 autorizacionUsuarioId)
        {
            _conexion.AddParamIn(nameof(autorizacionUsuarioId), autorizacionUsuarioId);
            return _conexion.LoadEntity<EAutorizacionUsuario>("NCAutorizacionesUsuariosCI");
        }
        /// <summary>
        /// Permite insertar la entidad AutorizacionUsuario.
        /// </summary>
        protected Int64 AutorizacionUsuarioInserta(EAutorizacionUsuario autorizacionUsuario)
        {
            _conexion.AddParamEntity(autorizacionUsuario, MAccionesBd.Inserta);
            Int64 vResultado = _conexion.ExecuteScalar<Int64>("NCAutorizacionesUsuariosIAE");
            return vResultado;
        }
        /// <summary>
        /// Permite actualizar la entidad AutorizacionUsuario.
        /// </summary>
        protected Boolean AutorizacionUsuarioActualiza(EAutorizacionUsuario autorizacionUsuario)
        {
            _conexion.AddParamEntity(autorizacionUsuario, MAccionesBd.Actualiza);
            _conexion.ExecuteScalar("NCAutorizacionesUsuariosIAE");
            return _mensajes.Ok;
        }
        /// <summary>
        /// Permite eliminar la entidad AutorizacionUsuario.
        /// </summary>
        protected Boolean AutorizacionUsuarioElimina(EAutorizacionUsuario autorizacionUsuario)
        {
            _conexion.AddParamEntity(autorizacionUsuario, MAccionesBd.Elimina);
            return _conexion.ExecuteNonQueryRet("NCAutorizacionesUsuariosIAE");
        }
        #endregion

        #endregion
    }
}
