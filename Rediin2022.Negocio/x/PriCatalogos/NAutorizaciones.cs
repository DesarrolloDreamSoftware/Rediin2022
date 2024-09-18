using DSEntityNetX.Business;
using DSEntityNetX.Common.Casting;
using DSEntityNetX.DataAccess;
using DSMetodNetX.AccesoDatos;
using DSMetodNetX.Entidades;
using DSMetodNetX.Idioma;
using DSMetodNetX.Negocio;
using Rediin2022.AccesoDatos.PriCatalogos;
using Rediin2022.Entidades.Idioma;
using Rediin2022.Entidades.PriCatalogos;
using System;
using System.Collections.Generic;

namespace Rediin2022.Negocio.PriCatalogos
{
    public class NAutorizaciones : RAutorizaciones, INAutorizaciones
    {
        #region Variables
        private IMReglasNeg<EAutorizacion> _autorizacionReglas = null;
        private IMReglasNeg<EAutorizacionUsuario> _autorizacionUsuarioReglas = null;
        #endregion

        #region Constructores
        public NAutorizaciones(IMConexionEntidad conexion)
            : base(conexion)
        {
        }
        #endregion

        #region Funciones

        #region Autorizacion (Autorizaciones)
        /// <summary>
        /// Esta funcion valida e inserta un registro en la base de datos.
        /// </summary>
        public new Int64 AutorizacionInserta(EAutorizacion autorizacion)
        {
            autorizacion.EstablecimientoId = _conexion.UsuarioSesion.EstablecimientoId;

            //Validacion
            if (!AutorizacionValida(autorizacion))
                return 0L;

            //Persistencia
            return base.AutorizacionInserta(autorizacion);
        }
        /// <summary>
        /// Valida y actualiza un registro en la base de datos.
        /// </summary>
        public new Boolean AutorizacionActualiza(EAutorizacion autorizacion)
        {
            autorizacion.EstablecimientoId = _conexion.UsuarioSesion.EstablecimientoId;

            //Validacion
            if (!AutorizacionValida(autorizacion))
                return false;

            //Persistencia
            return base.AutorizacionActualiza(autorizacion);
        }
        /// <summary>
        /// Elimina un registro de la base de datos.
        /// </summary>
        /// <returns></returns>
        public new Boolean AutorizacionElimina(EAutorizacion autorizacion)
        {
            //Validacion
            AutorizacionReglasNeg().ValidateProperty(autorizacion, e => e.AutorizacionId);
            if (!_mensajes.Ok)
                return false;

            //Persistencia
            return base.AutorizacionElimina(autorizacion);
        }
        public List<MEReglaNeg> AutorizacionReglas()
        {
            return AutorizacionReglasNeg().Rules;
        }
        private Boolean AutorizacionValida(EAutorizacion autorizacion)
        {
            _mensajes.Initialize();
            if (!AutorizacionReglasNeg().Validate(autorizacion))
                return false;

            //Validaciones adicionales

            return _mensajes.Ok;
        }
        private IMReglasNeg<EAutorizacion> AutorizacionReglasNeg()
        {
            if (_autorizacionReglas != null)
                return _autorizacionReglas;

            _autorizacionReglas = Validaciones.CreaReglasNeg<EAutorizacion>(_mensajes);
            _autorizacionReglas.AddSL(e => e.AutorizacionId, 0L, Validaciones._int64Max, false); // Consecutivo
            _autorizacionReglas.AddSL(e => e.AutorizacionNombre, 2, 50);
            _autorizacionReglas.AddSL(e => e.ProcesoOperativoId, 0L, Validaciones._int64Max).MessageForRange = MMensajesXId.ValidaSeleccion;

            return _autorizacionReglas;
        }
        #endregion

        #region AutorizacionUsuario (AutorizacionesUsuarios)
        /// <summary>
        /// Esta funcion valida e inserta un registro en la base de datos.
        /// </summary>
        public new Int64 AutorizacionUsuarioInserta(EAutorizacionUsuario autorizacionUsuario)
        {
            //Validacion
            if (!AutorizacionUsuarioValida(autorizacionUsuario))
                return 0L;

            //Persistencia
            return base.AutorizacionUsuarioInserta(autorizacionUsuario);
        }
        /// <summary>
        /// Valida y actualiza un registro en la base de datos.
        /// </summary>
        public new Boolean AutorizacionUsuarioActualiza(EAutorizacionUsuario autorizacionUsuario)
        {
            //Validacion
            if (!AutorizacionUsuarioValida(autorizacionUsuario))
                return false;

            //Persistencia
            return base.AutorizacionUsuarioActualiza(autorizacionUsuario);
        }
        /// <summary>
        /// Elimina un registro de la base de datos.
        /// </summary>
        /// <returns></returns>
        public new Boolean AutorizacionUsuarioElimina(EAutorizacionUsuario autorizacionUsuario)
        {
            //Validacion
            AutorizacionUsuarioReglasNeg().ValidateProperty(autorizacionUsuario, e => e.AutorizacionUsuarioId);
            if (!_mensajes.Ok)
                return false;

            //Persistencia
            return base.AutorizacionUsuarioElimina(autorizacionUsuario);
        }
        public List<MEReglaNeg> AutorizacionUsuarioReglas()
        {
            return AutorizacionUsuarioReglasNeg().Rules;
        }
        private Boolean AutorizacionUsuarioValida(EAutorizacionUsuario autorizacionUsuario)
        {
            _mensajes.Initialize();
            if (!AutorizacionUsuarioReglasNeg().Validate(autorizacionUsuario))
                return false;

            //Validaciones adicionales

            return _mensajes.Ok;
        }
        private IMReglasNeg<EAutorizacionUsuario> AutorizacionUsuarioReglasNeg()
        {
            if (_autorizacionUsuarioReglas != null)
                return _autorizacionUsuarioReglas;

            _autorizacionUsuarioReglas = Validaciones.CreaReglasNeg<EAutorizacionUsuario>(_mensajes);
            _autorizacionUsuarioReglas.AddSL(e => e.AutorizacionUsuarioId, 0L, Validaciones._int64Max, false); // Consecutivo
            _autorizacionUsuarioReglas.AddSL(e => e.ProcesoOperativoEstId, 0L, Validaciones._int64Max).MessageForRange = MMensajesXId.ValidaSeleccion;
            _autorizacionUsuarioReglas.AddSL(e => e.UsuarioId, 0L, Validaciones._int64Max);

            return _autorizacionUsuarioReglas;
        }
        #endregion

        #endregion
    }
}
