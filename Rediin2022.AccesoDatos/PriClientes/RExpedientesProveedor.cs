using DSMetodNetX.AccesoDatos;
using DSMetodNetX.Comun;
using Rediin2022.Entidades.PriClientes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rediin2022.AccesoDatos.PriClientes
{
    public class RExpedientesProveedor : MRepositorio
    {
        #region Variables
        /// <summary>
        /// Conexión.
        /// </summary>
        protected IMConexionEntidad _conexion;
        #endregion

        #region Constructores
        public RExpedientesProveedor(IMConexionEntidad conexion)
            : base(conexion)
        {
            _conexion = conexion;
        }
        #endregion

        #region Funciones

        #region Proveedor
        protected async Task<Int64> ProveedorExpedienteId(Int64 usuarioId,
                                                          Int64 procesoOperativoId)
        {
            _conexion.AddParamIn(usuarioId);
            _conexion.AddParamIn(procesoOperativoId);
            return await _conexion.ExecuteScalarAsync<Int64>("NTProveedorExpedienteId");
        }
        #endregion

        #region Monte Pio
        #endregion

        #region Medix
        #endregion

        #endregion
    }
}
