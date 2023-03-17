using DSMetodNetX.Api.Seguridad;
using DSMetodNetX.Aplicacion;
using DSMetodNetX.Entidades;
using DSMetodNetX.Negocio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Rediin2022.Entidades.PriCatalogos;
using Rediin2022.Entidades.PriOperacion;
using System;
using System.Collections.Generic;
using System.IO;

namespace Rediin2022Api.Areas.PriOperacion.Controllers
{
	[Route("ApiV1/PriOperacion/[controller]/[action]")]
	public class ConExpedientesController : MControllerApiPub, INConExpedientes
	{
		#region Contructores
		public ConExpedientesController(INConExpedientes nConExpedientes)
		{
			NConExpedientes = nConExpedientes;
		}
		#endregion

		#region Propiedades
		public INConExpedientes NConExpedientes { get; }
		public IMMensajes Mensajes
		{
			get { return NConExpedientes.Mensajes; }
		}
		#endregion

		#region Funciones

		#region ConExpProcOperativo (Enc)
		/// <summary>
		/// Consulta paginada de la entidad ConExpProcOperativo.
		/// </summary>
		[HttpPost]
		public EConExpProcOperativoPag ConExpProcOperativoPag(EConExpProcOperativoFiltro conExpProcOperativoFiltro)
		{
			return NConExpedientes.ConExpProcOperativoPag(conExpProcOperativoFiltro);
		}
		#endregion

		#region ConExpediente (Exp)
		/// <summary>
		/// Consulta paginada de la entidad ConExpediente.
		/// </summary>
		[HttpPost]
		public EConExpedientePag ConExpedientePag(EConExpedienteFiltro conExpedienteFiltro)
		{
			return NConExpedientes.ConExpedientePag(conExpedienteFiltro);
		}
		/// <summary>
		/// Consulta por id de la entidad ConExpediente.
		/// </summary>
		/// <param name="expedienteId"></param>
		/// <returns></returns>
		[HttpGet("{expedienteId}")]
		public EConExpediente ConExpedienteXId(Int64 expedienteId)
		{
			return NConExpedientes.ConExpedienteXId(expedienteId);
		}
		/// <summary>
		/// Permite insertar la entidad ConExpediente.
		/// </summary>
		[HttpPost]
		public Int64 ConExpedienteInserta(EConExpediente conExpediente)
		{
			return NConExpedientes.ConExpedienteInserta(conExpediente);
		}
		/// <summary>
		/// Permite actualizar la entidad ConExpediente.
		/// </summary>
		[HttpPost]
		public Boolean ConExpedienteActualiza(EConExpediente conExpediente)
		{
			return NConExpedientes.ConExpedienteActualiza(conExpediente);
		}
		/// <summary>
		/// Permite eliminar la entidad ConExpediente.
		/// </summary>
		[HttpPost]
		public Boolean ConExpedienteElimina(EConExpediente conExpediente)
		{
			return NConExpedientes.ConExpedienteElimina(conExpediente);
		}
		/// <summary>
		/// Accion personalizada CambioEstatus.
		/// </summary>
		[HttpPost]
		public Boolean ConExpedienteCambioEstatus(EConExpedienteCambioEstatus conExpedienteCambioEstatus)
		{
			return NConExpedientes.ConExpedienteCambioEstatus(conExpedienteCambioEstatus);
		}
		/// <summary>
		/// Reglas de negocio de la entidad ConExpediente.
		/// </summary>
		[HttpGet]
		public List<MEReglaNeg> ConExpedienteReglas()
		{
			return NConExpedientes.ConExpedienteReglas();
		}
		/// <summary>
		/// Consulta para los combos que se capturan.
		/// </summary>
		[HttpPost]
		public List<MEElemento> ConExpedienteCmb(EProcesoOperativoCol procesoOperativoCol)
		{
			return NConExpedientes.ConExpedienteCmb(procesoOperativoCol);
		}
		#endregion

		#region ConExpedienteObjeto (Objs)
		/// <summary>
		/// Consulta paginada de la entidad ConExpedienteObjeto.
		/// </summary>
		[HttpPost]
		public EConExpedienteObjetoPag ConExpedienteObjetoPag(EConExpedienteObjetoFiltro conExpedienteObjetoFiltro)
		{
			return NConExpedientes.ConExpedienteObjetoPag(conExpedienteObjetoFiltro);
		}
		/// <summary>
		/// Permite insertar la entidad ConExpedienteObjeto.
		/// </summary>
		[HttpPost]
		public Int64 ConExpedienteObjetoInserta(EConExpedienteObjeto conExpedienteObjeto)
		{
			return NConExpedientes.ConExpedienteObjetoInserta(conExpedienteObjeto);
		}
		/// <summary>
		/// Permite actualizar la entidad ConExpedienteObjeto.
		/// </summary>
		/// <param name="conExpedienteObjeto"></param>
		/// <returns></returns>
		[HttpPost]
		public Boolean ConExpedienteObjetoActualiza(EConExpedienteObjeto conExpedienteObjeto)
		{
			return NConExpedientes.ConExpedienteObjetoActualiza(conExpedienteObjeto);
		}
		/// <summary>
		/// Permite eliminar la entidad ConExpedienteObjeto.
		/// </summary>
		[HttpPost]
		public Boolean ConExpedienteObjetoElimina(EConExpedienteObjeto conExpedienteObjeto)
		{
			return NConExpedientes.ConExpedienteObjetoElimina(conExpedienteObjeto);
		}
		//Eli
		///// <summary>
		///// Accion personalizada Descarga.
		///// </summary>
		//public Boolean ConExpedienteObjetoDescarga()
		//{
		//    return NConExpedientes.ConExpedienteObjetoDescarga();
		//}

		/// <summary>
		/// Accion personalizada SelArchivo.
		/// </summary>
		[HttpPost]
		public Boolean ConExpedienteObjetoSelArchivo(EConExpedienteObjetoSelArchivo conExpedienteObjetoSelArchivo)
		{
			return NConExpedientes.ConExpedienteObjetoSelArchivo(conExpedienteObjetoSelArchivo);
		}
		/// <summary>
		/// Reglas de negocio de la entidad ConExpedienteObjeto.
		/// </summary>
		[HttpGet]
		public List<MEReglaNeg> ConExpedienteObjetoReglas()
		{
			return NConExpedientes.ConExpedienteObjetoReglas();
		}
		///// <summary>
		///// Reglas de negocio de la entidad ConExpedienteObjeto.
		///// </summary>
		//[HttpGet("{entidad}/{expedienteId}/{nombreArchivo}")]
		//public Byte[] ConExpedienteDescarga(String entidad, Int32 expedienteId, String nombreArchivo)
		//{
		//	String vRutaConArchivo = Path.Combine(base.MValorConfig<String>(MConfigIds.dirBD), entidad, expedienteId.ToString(), nombreArchivo);
		//	using MemoryStream vMS = new MemoryStream();
		//	using FileStream vFS = new FileStream(vRutaConArchivo, FileMode.Open);
		//	vFS.CopyTo(vMS);
		//	return vMS.GetBuffer();
		//}
		#endregion

		#region ExpedienteEstatu (ExpeEsta)
		/// <summary>
		/// Consulta paginada de la entidad ExpedienteEstatu.
		/// </summary>
		[HttpPost]
		public EExpedienteEstatuPag ExpedienteEstatuPag(EExpedienteEstatuFiltro expedienteEstatuFiltro)
		{
			return NConExpedientes.ExpedienteEstatuPag(expedienteEstatuFiltro);
		}
		/// <summary>
		/// Consulta del utlimo estatus del expediente.
		/// </summary>
		/// <param name="expedienteId"></param>
		/// <returns></returns>
		[HttpGet("{expedienteId}")]
		public EExpedienteEstatu ExpedienteEstatusUltimo(Int64 expedienteId)
		{
			return NConExpedientes.ExpedienteEstatusUltimo(expedienteId);
		}
		#endregion

		#endregion
	}
}
