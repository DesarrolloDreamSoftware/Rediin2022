using DSEntityNetX.Common.Casting;
using DSMetodNetX.Aplicacion;
using DSMetodNetX.Entidades;
using DSMetodNetX.Negocio;
using Rediin2022.Entidades.PriCatalogos;
using Rediin2022.Entidades.PriClientes;
using Rediin2022.Entidades.PriClientes.Expedientes;
using Rediin2022.Entidades.PriOperacion;
using Sisegui2020.Entidades.PriUtilerias;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;

namespace Rediin2022.Aplicacion.PriClientes
{
	public class APLExpedientes : MAplicacion, INExpedientes
	{
		#region Constructores
		public APLExpedientes(IMApiCliente api) : base(api)
		{
		}
		#endregion

		#region Funciones

		#region Funciones para el cliente
		public Int64 ExpedienteInserta(EExpediente expediente)
		{
			return Call<Int64>(NomFn(), expediente);
		}
        public Boolean ExpedienteActualiza(EExpediente expediente)
		{
            return Call<bool>(NomFn(), expediente);
        }
        public Boolean ExpedienteElimina(Int64 expedienteId)
		{
			return Call<Boolean>(NomFn(), expedienteId);
		}
		public Int64 ObjetoInserta(EExpedienteObjeto expedienteObjeto)
		{
			return Call<Int64>(NomFn(), expedienteObjeto);
		}
        /// <summary>
        /// Sube el documento y modifica su nombre.
		/// Es necesario cargar los campos ExpedienteId, ExpedienteObjetoId, ArchivoNombre y Archivo
        /// </summary>
        /// <param name="documento"></param>
        /// <returns></returns>
        public Boolean ObjetoActualiza(EExpedienteObjeto expedienteObjeto)
        {
            return Call<Boolean>(NomFn(), expedienteObjeto);
        }
        /// <summary>
        /// Descargar solo el documento
        /// </summary>
        /// <param name="expendienteId"></param>
        /// <param name="archivoNombre"></param>
        /// <returns></returns>
        public EDocumento ObjectoDescargaDocto(Int64 expendienteId, String archivoNombre)
        {
			return Call<EDocumento>(NomFn(), expendienteId, archivoNombre);
        }
        /// <summary>
        /// Listados para cargar los combos con expedientes de procesos operativos que son catalogos
        /// </summary>
        /// <param name="expendienteDatCmb"></param>
        /// <returns></returns>
        public List<MEElemento> ConExpedienteCmb(EExpendienteDatCmb expendienteDatCmb)
		{
			return Call<List<MEElemento>>(NomFn(), expendienteDatCmb);
		}
		#endregion

		//No config Proveedor
		#region Funciones especificas para un proc operativo
		/// <summary>
		/// Regresa los datos del proveedor segun el usuario autentificado 
		/// para el proceso operativo especifico de proveedores.
		/// </summary>
		/// <param name="usuarioId"></param>
		/// <returns></returns>
		public EDatosProveedor ProveedorXUsuario(Int64 procesoOperativoIdProveedor, 
										    Int64 usuarioId)
		{
			return Call<EDatosProveedor>(NomFn(),
							   		     procesoOperativoIdProveedor, 
									     usuarioId);
		}
        /// <summary>
        /// Actualiza el proveedor.
        /// </summary>
        /// <param name="proveedor"></param>
        /// <returns></returns>
        public Boolean ProveedorActualiza(EProveedor proveedor)
        {
            return Call<Boolean>(NomFn(), proveedor);
        }
        /// <summary>
        /// Pasa el expediente al siguiente estatus.
        /// </summary>
        /// <param name="conExpedienteCambioEstatus"></param>
        /// <returns></returns>
        public Boolean ProveedorCambioEstatus(EConExpedienteCambioEstatus conExpedienteCambioEstatus)
		{
			return Call<Boolean>(NomFn(), conExpedienteCambioEstatus);
		}
		/// <summary>
		/// Relaciones de las ColumnaId con una Propiedad para los procesos operativos que se fijan.
		/// </summary>
		/// <param name="procesoOperativoId"></param>
		/// <returns></returns>
		public List<ERelacionProcOper> RelacionProcesoOperativo(Int64 procesoOperativoId)
		{
			return Call<List<ERelacionProcOper>>(NomFn(), procesoOperativoId);
		}
		#endregion
		//No config Proveedor

		#endregion
	}
}