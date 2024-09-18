using DSMetodNetX.AccesoDatos;
using DSMetodNetX.Comun;
using Rediin2022.Entidades.PriClientes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rediin2022.AccesoDatos.PriClientes
{
    public class RExpendientes : MRepositorio
    {
        #region Variables
        /// <summary>
        /// Conexión.
        /// </summary>
        private IMConexionEntidad _conexion;
        #endregion

        #region Constructores
        public RExpendientes(IMConexionEntidad conexion)
            : base(conexion)
        {
            _conexion = conexion;
        }
        #endregion

        #region Funciones

        //No config Proveedor
        #region Funciones especificas para un proc operativo
        protected async Task<Int64> ProveedorExpedienteId(Int64 usuarioId,
                                                          Int64 procesoOperativoId,
                                                          Int64 columnaIdUsuario)
        {
            _conexion.AddParamIn(usuarioId);
            _conexion.AddParamIn(procesoOperativoId);
            _conexion.AddParamIn(columnaIdUsuario);
            return await _conexion.ExecuteScalarAsync<Int64>("NTProveedorExpedienteId");
        }
        /// <summary>
        /// Relaciones de las ColumnaId con una Propiedad para los procesos operativos que se fijan.
        /// </summary>
        /// <param name="procesoOperativoId"></param>
        /// <returns></returns>

        public async Task<List<ERelacionProcOper>> RelacionProcesoOperativo(Int64 procesoOperativoId)
        {
            _conexion.AddParamIn(procesoOperativoId);
            return await _conexion.LoadEntitiesAsync<ERelacionProcOper>("NTProveedorRelacionProcOper");
        }
        #endregion
        //No config Proveedor

        #endregion
    }
}
