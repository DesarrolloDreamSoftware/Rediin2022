using DSMetodNetX.AccesoDatos;
using DSMetodNetX.Comun;
using Rediin2022.Entidades.PriClientes;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Rediin2022.AccesoDatos.PriClientes
{
    public class RExpedientes : MRepositorio
    {
        #region Variables
        /// <summary>
        /// Conexión.
        /// </summary>
        protected IMConexionEntidad _conexion;
        #endregion

        #region Constructores
        public RExpedientes(IMConexionEntidad conexion)
            : base(conexion)
        {
            _conexion = conexion;
        }
        #endregion

        #region Funciones

        #endregion
    }
}
