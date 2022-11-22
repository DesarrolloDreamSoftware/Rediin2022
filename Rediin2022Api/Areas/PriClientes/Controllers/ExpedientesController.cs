using DSMetodNetX.Api.Seguridad;
using DSMetodNetX.Entidades;
using DSMetodNetX.Mvc;
using DSMetodNetX.Negocio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Rediin2022.Entidades.Idioma;
using Rediin2022.Entidades.PriCatalogos;
using Rediin2022.Entidades.PriClientes;
using Rediin2022.Entidades.PriClientes.Expedientes;
using Rediin2022.Entidades.PriOperacion;
using Rediin2022.Negocio.PriClientes;
using Rediin2022.Negocio.PriOperacion;
using System;
using System.Collections.Generic;
using System.IO;

namespace Rediin2022Api.Areas.PriClientes.Controllers
{
	[Route("ApiV1/PriClientes/[controller]/[action]")]
	public class ExpedientesController : MControllerApiPri, INExpedientes
	{
		#region Contructores
		public ExpedientesController(INExpedientes nExpedientes)
		{
			NExpedientes = nExpedientes;
		}
		#endregion

		#region Propiedades
		public INExpedientes NExpedientes { get; }
		public IMMensajes Mensajes
		{
			get { return NExpedientes.Mensajes; }
		}
		#endregion

		#region Funciones

		#region Funciones para el cliente
		public Int64 ExpedienteInserta(EExpediente expediente)
		{
			return NExpedientes.ExpedienteInserta(expediente);
		}
		public Boolean ExpedienteElimina(Int64 expedienteId)
		{
			return NExpedientes.ExpedienteElimina(expedienteId);
		}
		public Int64 ObjetoInserta(EExpedienteObjeto expedienteObjeto)
		{
			//Calculamos la ruta
			String vEntidad = "Expendientes";
			String vRutaBase = MValorConfig<String>("DirBD");
			expedienteObjeto.Ruta =
				Path.Combine(vRutaBase,
							 vEntidad,
							 expedienteObjeto.ExpedienteId.ToString());

			String vRutaYNombre = Path.Combine(expedienteObjeto.Ruta, expedienteObjeto.ArchivoNombre);

			if (!System.IO.Directory.Exists(Path.Combine(vRutaBase, vEntidad)))
				System.IO.Directory.CreateDirectory(Path.Combine(vRutaBase, vEntidad));

			if (!System.IO.Directory.Exists(expedienteObjeto.Ruta))
				System.IO.Directory.CreateDirectory(expedienteObjeto.Ruta);

			if (System.IO.File.Exists(vRutaYNombre))
			{
				Mensajes.AddError("EL nombre del arhivo ya existe, no se puede insertar.");
				return 0L;
			}

			//Guardamos en bd
			Int64 vExpedienteObjetoId = NExpedientes.ObjetoInserta(expedienteObjeto);
			if (!Mensajes.Ok)
				return 0L;

			//Subimos el archivo
			using var vMS = new MemoryStream(expedienteObjeto.Archivo);
			MUtilMvc.RecibeArchivoDeCliente(HttpContext.Request,
											NExpedientes.Mensajes,
											new FormFile(vMS, 0, vMS.Length, String.Empty, expedienteObjeto.ArchivoNombre),
											vRutaYNombre);

			if (!Mensajes.Ok)
			{
				Mensajes.AddError("No se puedo insertar el archivo.");
				return 0L;
			}

			return vExpedienteObjetoId;
		}
		public List<MEElemento> ConExpedienteCmb(EExpendienteDatCmb expendienteDatCmb)
		{
			return NExpedientes.ConExpedienteCmb(expendienteDatCmb);
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
		[HttpGet("{procesoOperativoIdProveedor}/{usuarioId}")]
		public EDatosProveedor ProveedorXUsuario(Int64 procesoOperativoIdProveedor,
								   		         Int64 usuarioId)
		{
			return NExpedientes.ProveedorXUsuario(procesoOperativoIdProveedor, 
												  usuarioId);
		}	
		/// <summary>
		/// Pasa el expediente al siguiente estatus.
		/// </summary>
		/// <param name="conExpedienteCambioEstatus"></param>
		/// <returns></returns>
		public Boolean ProveedorCambioEstatus(EConExpedienteCambioEstatus conExpedienteCambioEstatus)
		{
			return NExpedientes.ProveedorCambioEstatus(conExpedienteCambioEstatus);
		}
		/// <summary>
		/// Relaciones de las ColumnaId con una Propiedad para los procesos operativos que se fijan.
		/// </summary>
		/// <param name="procesoOperativoId"></param>
		/// <returns></returns>
		[HttpGet("{procesoOperativoId}")]
		public List<ERelacionProcOper> RelacionProcesoOperativo(Int64 procesoOperativoId)
		{
			return NExpedientes.RelacionProcesoOperativo(procesoOperativoId);
		}
		#endregion
		//No config Proveedor

		#endregion
	}
}
