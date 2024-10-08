﻿using DSMetodNetX.Entidades;
using Rediin2022.Entidades.PriCatalogos;
using Rediin2022.Entidades.PriClientes.Expedientes;
using Rediin2022.Entidades.PriOperacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rediin2022.Entidades.PriClientes
{
	public interface INExpedientes : IMCtrMensajes
	{
		#region Funciones para el cliente
		Int64 ExpedienteInserta(EExpediente expediente);
		Boolean ExpedienteElimina(Int64 expedienteId);
		Boolean ExpedienteActualiza(EExpediente expediente);
        Int64 ObjetoInserta(EExpedienteObjeto expedienteObjeto);
        /// <summary>
        /// Sube el documento y modifica su nombre.
		/// Es necesario cargar los campos ExpedienteId, ExpedienteObjetoId, ArchivoNombre y Archivo.
        /// </summary>
        /// <param name="documento"></param>
        /// <returns></returns>
        Boolean ObjetoActualiza(EExpedienteObjeto expedienteObjeto);
        /// <summary>
        /// Descargar solo el documento
        /// </summary>
        /// <param name="expendienteId"></param>
        /// <param name="archivoNombre"></param>
        /// <returns></returns>
        EDocumento ObjectoDescargaDocto(Int64 expendienteId, String archivoNombre);		
        /// <summary>
        /// Listados para cargar los combos con expedientes de procesos operativos que son catalogos
        /// </summary>
        /// <param name="expendienteDatCmb"></param>
        /// <returns></returns>
        List<MEElemento> ConExpedienteCmb(EExpendienteDatCmb expendienteDatCmb);		
		#endregion

		//No config Proveedor
		#region Funciones especificas para un proc operativo
		/// <summary>
		/// Regresa los datos del proveedor segun el usuario autentificado 
		/// para el proceso operativo especifico de proveedores.
		/// </summary>
		/// <param name="usuarioId"></param>
		/// <returns></returns>
		EDatosProveedor ProveedorXUsuario(Int64 procesoOperativoIdProveedor, Int64 usuarioId);
		/// <summary>
		/// Actualiza el proveedor.
		/// </summary>
		/// <param name="proveedor"></param>
		/// <returns></returns>
		Boolean ProveedorActualiza(EProveedor proveedor);
		/// <summary>
		/// Pasa el expediente al siguiente estatus.
		/// </summary>
		/// <param name="conExpedienteCambioEstatus"></param>
		/// <returns></returns>
		Boolean ProveedorCambioEstatus(EConExpedienteCambioEstatus conExpedienteCambioEstatus);
		/// <summary>
		/// Relaciones de las ColumnaId con una Propiedad para los procesos operativos que se fijan.
		/// </summary>
		/// <param name="procesoOperativoId"></param>
		/// <returns></returns>
		List<ERelacionProcOper> RelacionProcesoOperativo(Int64 procesoOperativoId);
		#endregion
		//No config Proveedor
	}
}
