using DSMetodNetX.Aplicacion;
using DSMetodNetX.Entidades;
using DSMetodNetX.Negocio;
using Rediin2022.Entidades.PriCatalogos;
using Rediin2022.Entidades.PriOperacion;
using System;
using System.Collections.Generic;

namespace Rediin2022.Aplicacion.PriOperacion
{
	public class APLConExpedientes : MAplicacion, INConExpedientes
	{
		#region Constructores
		public APLConExpedientes(IMApiCliente api) : base(api)
		{
		}
		#endregion

		#region Funciones

		#region ConExpProcOperativo (Enc)
		/// <summary>
		/// Consulta paginada de la entidad ConExpProcOperativo.
		/// </summary>
		public EConExpProcOperativoPag ConExpProcOperativoPag(EConExpProcOperativoFiltro conExpProcOperativoFiltro)
		{
			return Call<EConExpProcOperativoPag>(NomFn(), conExpProcOperativoFiltro);
		}
		#endregion

		#region ConExpediente (Exp)
		/// <summary>
		/// Consulta paginada de la entidad ConExpediente.
		/// </summary>
		public EConExpedientePag ConExpedientePag(EConExpedienteFiltro conExpedienteFiltro)
		{
			return Call<EConExpedientePag>(NomFn(), conExpedienteFiltro);
		}
		/// <summary>
		/// Consulta por id de la entidad ConExpediente.
		/// </summary>
		/// <param name="expedienteId"></param>
		/// <returns></returns>
		public EConExpediente ConExpedienteXId(Int64 expedienteId)
		{
			return Call<EConExpediente>(NomFn(), expedienteId);
		}
		/// <summary>
		/// Permite insertar la entidad ConExpediente.
		/// </summary>
		public Int64 ConExpedienteInserta(EConExpediente conExpediente)
		{
			return Call<Int64>(NomFn(), conExpediente);
		}
		/// <summary>
		/// Permite actualizar la entidad ConExpediente.
		/// </summary>
		public Boolean ConExpedienteActualiza(EConExpediente conExpediente)
		{
			return Call<Boolean>(NomFn(), conExpediente);
		}
		/// <summary>
		/// Permite eliminar la entidad ConExpediente.
		/// </summary>
		public Boolean ConExpedienteElimina(EConExpediente conExpediente)
		{
			return Call<Boolean>(NomFn(), conExpediente);
		}
		/// <summary>
		/// Acci�n personalizada CambioEstatus.
		/// </summary>
		public Boolean ConExpedienteCambioEstatus(EConExpedienteCambioEstatus conExpedienteCambioEstatus)
		{
			return Call<Boolean>(NomFn(), conExpedienteCambioEstatus);
		}
		/// <summary>
		/// Reglas de negocio de la entidad ConExpediente.
		/// </summary>
		public List<MEReglaNeg> ConExpedienteReglas()
		{
			return Call<List<MEReglaNeg>>(NomFn());
		}
		/// <summary>
		/// Consulta para los combos que se capturan.
		/// </summary>
		public List<MEElemento> ConExpedienteCmb(EProcesoOperativoCol procesoOperativoCol)
		{
			return Call<List<MEElemento>>(NomFn(), procesoOperativoCol);
		}
		#endregion

		#region ConExpedienteObjeto (Objs)
		/// <summary>
		/// Consulta paginada de la entidad ConExpedienteObjeto.
		/// </summary>
		public EConExpedienteObjetoPag ConExpedienteObjetoPag(EConExpedienteObjetoFiltro conExpedienteObjetoFiltro)
		{
			return Call<EConExpedienteObjetoPag>(NomFn(), conExpedienteObjetoFiltro);
		}
		/// <summary>
		/// Permite insertar la entidad ConExpedienteObjeto.
		/// </summary>
		public Int64 ConExpedienteObjetoInserta(EConExpedienteObjeto conExpedienteObjeto)
		{
			return Call<Int64>(NomFn(), conExpedienteObjeto);
		}
        /// <summary>
        /// Permite actualizar la entidad ConExpedienteObjeto.
        /// </summary>
        public Boolean ConExpedienteObjetoActualiza(EConExpedienteObjeto conExpedienteObjeto)
        {
            return Call<Boolean>(NomFn(), conExpedienteObjeto);
        }
        /// <summary>
        /// Permite eliminar la entidad ConExpedienteObjeto.
        /// </summary>
        public Boolean ConExpedienteObjetoElimina(EConExpedienteObjeto conExpedienteObjeto)
		{
			return Call<Boolean>(NomFn(), conExpedienteObjeto);
		}
		//Eli
		///// <summary>
		///// Acci�n personalizada Descarga.
		///// </summary>
		//public Boolean ConExpedienteObjetoDescarga()
		//{
		//    return Call<Boolean>(NomFn());
		//}
		/// <summary>
		/// Acci�n personalizada SelArchivo.
		/// </summary>
		public Boolean ConExpedienteObjetoSelArchivo(EConExpedienteObjetoSelArchivo conExpedienteObjetoSelArchivo)
		{
			return Call<Boolean>(NomFn(), conExpedienteObjetoSelArchivo);
		}
		/// <summary>
		/// Reglas de negocio de la entidad ConExpedienteObjeto.
		/// </summary>
		public List<MEReglaNeg> ConExpedienteObjetoReglas()
		{
			return Call<List<MEReglaNeg>>(NomFn());
		}
		#endregion

		#region ExpedienteEstatu (ExpeEsta)
		/// <summary>
		/// Consulta paginada de la entidad ExpedienteEstatu.
		/// </summary>
		public EExpedienteEstatuPag ExpedienteEstatuPag(EExpedienteEstatuFiltro expedienteEstatuFiltro)
		{
			return Call<EExpedienteEstatuPag>(NomFn(), expedienteEstatuFiltro);
		}
		/// <summary>
		/// Consulta del utlimo estatus del expediente.
		/// </summary>
		/// <param name="expedienteId"></param>
		/// <returns></returns>
		public EExpedienteEstatu ExpedienteEstatusUltimo(Int64 expedienteId)
		{
			return Call<EExpedienteEstatu>(NomFn(), expedienteId);
		}
		#endregion

		#endregion
	}
}
