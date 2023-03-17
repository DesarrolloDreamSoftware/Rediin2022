using DSMetodNetX.Entidades;
using DSMetodNetX.Negocio;
using Rediin2022.Entidades.PriCatalogos;
using System;
using System.Collections.Generic;

namespace Rediin2022.Entidades.PriOperacion
{
	public interface INConExpedientes : IMCtrMensajes
	{
		#region Funciones

		#region ConExpProcOperativo (Enc)
		/// <summary>
		/// Consulta paginada de la entidad ConExpProcOperativo.
		/// </summary>
		EConExpProcOperativoPag ConExpProcOperativoPag(EConExpProcOperativoFiltro conExpProcOperativoFiltro);

		#endregion

		#region ConExpediente (Exp)
		/// <summary>
		/// Consulta paginada de la entidad ConExpediente.
		/// </summary>
		EConExpedientePag ConExpedientePag(EConExpedienteFiltro conExpedienteFiltro);
		/// <summary>
		/// Consulta por id de la entidad ConExpediente.
		/// </summary>
		/// <param name="expedienteId"></param>
		/// <returns></returns>
		EConExpediente ConExpedienteXId(Int64 expedienteId);
		/// <summary>
		/// Permite insertar la entidad ConExpediente.
		/// </summary>
		Int64 ConExpedienteInserta(EConExpediente conExpediente);
		/// <summary>
		/// Permite actualizar la entidad ConExpediente.
		/// </summary>
		Boolean ConExpedienteActualiza(EConExpediente conExpediente);
		/// <summary>
		/// Permite eliminar la entidad ConExpediente.
		/// </summary>
		Boolean ConExpedienteElimina(EConExpediente conExpediente);
		/// <summary>
		/// Accion personalizada CambioEstatus.
		/// </summary>
		Boolean ConExpedienteCambioEstatus(EConExpedienteCambioEstatus conExpedienteCambioEstatus);
		/// <summary>
		/// Reglas de negocio de la entidad ConExpediente.
		/// </summary>
		List<MEReglaNeg> ConExpedienteReglas();
		/// <summary>
		/// Consulta para los combos que se capturan.
		/// </summary>
		List<MEElemento> ConExpedienteCmb(EProcesoOperativoCol procesoOperativoCol);
		#endregion

		#region ConExpedienteObjeto (Objs)
		/// <summary>
		/// Consulta paginada de la entidad ConExpedienteObjeto.
		/// </summary>
		EConExpedienteObjetoPag ConExpedienteObjetoPag(EConExpedienteObjetoFiltro conExpedienteObjetoFiltro);
		/// <summary>
		/// Permite insertar la entidad ConExpedienteObjeto.
		/// </summary>
		Int64 ConExpedienteObjetoInserta(EConExpedienteObjeto conExpedienteObjeto);
		/// <summary>
		/// Permite actualizar la entidad ConExpedienteObjeto.
		/// </summary>
		/// <param name="conExpedienteObjeto"></param>
		/// <returns></returns>
		Boolean ConExpedienteObjetoActualiza(EConExpedienteObjeto conExpedienteObjeto);
		/// <summary>
		/// Permite eliminar la entidad ConExpedienteObjeto.
		/// </summary>
		Boolean ConExpedienteObjetoElimina(EConExpedienteObjeto conExpedienteObjeto);
		//Eli
		///// <summary>
		///// Accion personalizada Descarga.
		///// </summary>
		//Boolean ConExpedienteObjetoDescarga();
		/// <summary>
		/// Accion personalizada SelArchivo.
		/// </summary>
		Boolean ConExpedienteObjetoSelArchivo(EConExpedienteObjetoSelArchivo conExpedienteObjetoSelArchivo);
		/// <summary>
		/// Reglas de negocio de la entidad ConExpedienteObjeto.
		/// </summary>
		List<MEReglaNeg> ConExpedienteObjetoReglas();

		///// <summary>
		///// Reglas de negocio de la entidad ConExpedienteObjeto.
		///// </summary>
		//Byte[] ConExpedienteDescarga(String entidad, Int32 expedienteId, String nombreArchivo);
		#endregion

		#region ExpedienteEstatu (ExpeEsta)
		/// <summary>
		/// Consulta paginada de la entidad ExpedienteEstatu.
		/// </summary>
		EExpedienteEstatuPag ExpedienteEstatuPag(EExpedienteEstatuFiltro expedienteEstatuFiltro);
		/// <summary>
		/// Consulta del utlimo estatus del expediente.
		/// </summary>
		/// <param name="expedienteId"></param>
		/// <returns></returns>
		EExpedienteEstatu ExpedienteEstatusUltimo(Int64 expedienteId);
		#endregion

		#endregion
	}
}
