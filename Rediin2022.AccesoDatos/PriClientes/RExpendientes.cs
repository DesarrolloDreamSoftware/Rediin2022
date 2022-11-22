using DSMetodNetX.AccesoDatos;
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
		#region Constructores
		public RExpendientes(IMConexionEntidad conexionEntidad)
			:base(conexionEntidad)
		{ }
		#endregion

		#region Funciones

		//No config Proveedor
		#region Funciones especificas para un proc operativo
		protected Int64 ProveedorExpedienteId(Int64 usuarioId,
											  Int64 procesoOperativoId,
											  Int64 columnaIdUsuario)
		{
			_conexion.AddParamIn(nameof(usuarioId), usuarioId);
			_conexion.AddParamIn(nameof(procesoOperativoId), procesoOperativoId);
			_conexion.AddParamIn(nameof(columnaIdUsuario), columnaIdUsuario);
			return _conexion.ExecuteScalar<Int64>("NTProveedorExpedienteId");
		}
		/// <summary>
		/// Relaciones de las ColumnaId con una Propiedad para los procesos operativos que se fijan.
		/// </summary>
		/// <param name="procesoOperativoId"></param>
		/// <returns></returns>

		public List<ERelacionProcOper> RelacionProcesoOperativo(Int64 procesoOperativoId)
		{
			_conexion.AddParamIn(nameof(procesoOperativoId), procesoOperativoId);
			return _conexion.LoadEntities<ERelacionProcOper>("NTProveedorRelacionProcOper");
		}
		#endregion
		//No config Proveedor

		#endregion
	}
}
