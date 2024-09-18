using DSEntityNetX.Business;
using DSEntityNetX.Common.Casting;
using DSEntityNetX.DataAccess;
using DSMetodNetX.AccesoDatos;
using DSMetodNetX.Comun;
using DSMetodNetX.Entidades;

using DSMetodNetX.Negocio;
using Rediin2022.AccesoDatos.PriCatalogos;
using Rediin2022.Entidades.Idioma;
using Rediin2022.Entidades.PriCatalogos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rediin2022.Negocio.PriCatalogos
{
    /// <summary>
    /// Negocio.
    /// </summary>
    public class NIdentificaciones : RIdentificaciones, INIdentificaciones
    {
        #region Variables
        /// <summary>
        /// Reglas de negocio.
        /// </summary>
        private IMReglasNeg<EIdentificacion> _identificacionReglas = null;
        #endregion

        #region Constructores
        /// <summary>
        /// Negocio.
        /// </summary>
        public NIdentificaciones(IMConexionEntidad conexion)
            : base(conexion)
        {
        }
        #endregion

        #region Funciones

        #region Identificacion (Identificaciones)
        /// <summary>
        /// Esta funcion valida e inserta un registro en la base de datos.
        /// </summary>
        public new async Task<Int64> IdentificacionInserta(EIdentificacion identificacion)
        {
            //Validacion
            if (!IdentificacionValida(identificacion))
                return 0L;

            //Persistencia
            return await base.IdentificacionInserta(identificacion);
        }
        /// <summary>
        /// Valida y actualiza un registro en la base de datos.
        /// </summary>
        public new async Task<Boolean> IdentificacionActualiza(EIdentificacion identificacion)
        {
            //Validacion
            if (!IdentificacionValida(identificacion))
                return false;

            //Persistencia
            return await base.IdentificacionActualiza(identificacion);
        }
        /// <summary>
        /// Elimina un registro de la base de datos.
        /// </summary>
        /// <returns></returns>
        public new async Task<Boolean> IdentificacionElimina(EIdentificacion identificacion)
        {
            //Validacion
            IdentificacionReglasNeg().ValidateProperty(identificacion, e => e.IdentificacionId);
            if (!Mensajes.Ok)
                return false;

            //Persistencia
            return await base.IdentificacionElimina(identificacion);
        }
        /// <summary>
        /// Reglas de negocio.
        /// </summary>
        public async Task<List<MEReglaNeg>> IdentificacionReglas()
        {
            return await Task.Run(() => IdentificacionReglasNeg().Rules);
        }
        /// <summary>
        /// Validacion para inserta y actualiza.
        /// </summary>
        private Boolean IdentificacionValida(EIdentificacion identificacion)
        {
            Mensajes.Initialize();
            if (!IdentificacionReglasNeg().Validate(identificacion))
                return false;

            //Validaciones adicionales

            return Mensajes.Ok;
        }
        /// <summary>
        /// Crea las reglas de negocio.
        /// </summary>
        private IMReglasNeg<EIdentificacion> IdentificacionReglasNeg()
        {
            if (_identificacionReglas != null)
                return _identificacionReglas;

            _identificacionReglas = Validaciones.CreaReglasNeg<EIdentificacion>(Mensajes);
            _identificacionReglas.AddSL(e => e.IdentificacionId, 0L, Validaciones._int64Max, false); // Consecutivo
            _identificacionReglas.AddSL(e => e.IdentificacionNombre, 2, 120);
            _identificacionReglas.AddSL(e => e.Activo);

            return _identificacionReglas;
        }
        #endregion

        #endregion
    }
}
