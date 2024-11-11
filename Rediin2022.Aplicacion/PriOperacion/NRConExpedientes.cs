using DSMetodNetX.Aplicacion;
using DSMetodNetX.Entidades;

using Rediin2022.Entidades.PriCatalogos;
using Rediin2022.Entidades.PriOperacion;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rediin2022.Aplicacion.PriOperacion
{
    public class NRConExpedientes : MNegRemoto, INConExpedientes
    {
        #region Constructores
        public NRConExpedientes(IMApiCteNeg api) : base(api)
        {
        }
        #endregion

        #region Funciones

        #region ConExpProcOperativo (Enc)
        /// <summary>
        /// Consulta paginada de la entidad ConExpProcOperativo.
        /// </summary>
        public async Task<EConExpProcOperativoPag> ConExpProcOperativoPag(EConExpProcOperativoFiltro conExpProcOperativoFiltro)
        {
            return await CallAsync<EConExpProcOperativoPag>(NomFn(), conExpProcOperativoFiltro);
        }
        #endregion

        #region ConExpediente (Exp)
        /// <summary>
        /// Consulta paginada de la entidad ConExpediente.
        /// </summary>
        public async Task<EConExpedientePag> ConExpedientePag(EConExpedienteFiltro conExpedienteFiltro)
        {
            return await CallAsync<EConExpedientePag>(NomFn(), conExpedienteFiltro);
        }
        /// <summary>
        /// Consulta por id de la entidad ConExpediente.
        /// </summary>
        /// <param name="expedienteId"></param>
        /// <returns></returns>
        public async Task<EConExpediente> ConExpedienteXId(Int64 expedienteId)
        {
            return await CallAsync<EConExpediente>(NomFn(), expedienteId);
        }
        /// <summary>
        /// Permite insertar la entidad ConExpediente.
        /// </summary>
        public async Task<Int64> ConExpedienteInserta(EConExpediente conExpediente)
        {
            return await CallAsync<Int64>(NomFn(), conExpediente);
        }
        /// <summary>
        /// Permite actualizar la entidad ConExpediente.
        /// </summary>
        public async Task<Boolean> ConExpedienteActualiza(EConExpediente conExpediente)
        {
            return await CallAsync<Boolean>(NomFn(), conExpediente);
        }
        /// <summary>
        /// Permite eliminar la entidad ConExpediente.
        /// </summary>
        public async Task<Boolean> ConExpedienteElimina(EConExpediente conExpediente)
        {
            return await CallAsync<Boolean>(NomFn(), conExpediente);
        }
        /// <summary>
        /// Acción personalizada CambioEstatus.
        /// </summary>
        public async Task<Boolean> ConExpedienteCambioEstatus(EConExpedienteCambioEstatus conExpedienteCambioEstatus)
        {
            return await CallAsync<Boolean>(NomFn(), conExpedienteCambioEstatus);
        }
        /// <summary>
        /// Reglas de negocio de la entidad ConExpediente.
        /// </summary>
        public async Task<List<MEReglaNeg>> ConExpedienteReglas()
        {
            return await CallAsync<List<MEReglaNeg>>(NomFn());
        }
        ///// <summary>
        ///// Consulta para los combos que se capturan.
        ///// </summary>
        //public async Task<List<MEElemento>> ConExpedienteCmb(EProcesoOperativoCol procesoOperativoCol)
        //{
        //    return await CallAsync<List<MEElemento>>(NomFn(), procesoOperativoCol);
        //}
        #endregion

        #region ConExpedienteObjeto (Objs)
        /// <summary>
        /// Consulta paginada de la entidad ConExpedienteObjeto.
        /// </summary>
        public async Task<EConExpedienteObjetoPag> ConExpedienteObjetoPag(EConExpedienteObjetoFiltro conExpedienteObjetoFiltro)
        {
            return await CallAsync<EConExpedienteObjetoPag>(NomFn(), conExpedienteObjetoFiltro);
        }
        /// <summary>
        /// Permite insertar la entidad ConExpedienteObjeto.
        /// </summary>
        public async Task<Int64> ConExpedienteObjetoInserta(EConExpedienteObjeto conExpedienteObjeto)
        {
            return await CallAsync<Int64>(NomFn(), conExpedienteObjeto);
        }
        /// <summary>
        /// Permite actualizar la entidad ConExpedienteObjeto.
        /// </summary>
        public async Task<Boolean> ConExpedienteObjetoActualiza(EConExpedienteObjeto conExpedienteObjeto)
        {
            return await CallAsync<Boolean>(NomFn(), conExpedienteObjeto);
        }
        /// <summary>
        /// Permite eliminar la entidad ConExpedienteObjeto.
        /// </summary>
        public async Task<Boolean> ConExpedienteObjetoElimina(EConExpedienteObjeto conExpedienteObjeto)
        {
            return await CallAsync<Boolean>(NomFn(), conExpedienteObjeto);
        }
        //Eli
        ///// <summary>
        ///// Acción personalizada Descarga.
        ///// </summary>
        //public Boolean ConExpedienteObjetoDescarga()
        //{
        //    return await CallAsync<Boolean>(NomFn());
        //}
        /// <summary>
        /// Acción personalizada SelArchivo.
        /// </summary>
        public async Task<Boolean> ConExpedienteObjetoSelArchivo(EConExpedienteObjetoSelArchivo conExpedienteObjetoSelArchivo)
        {
            return await CallAsync<Boolean>(NomFn(), conExpedienteObjetoSelArchivo);
        }
        /// <summary>
        /// Reglas de negocio de la entidad ConExpedienteObjeto.
        /// </summary>
        public async Task<List<MEReglaNeg>> ConExpedienteObjetoReglas()
        {
            return await CallAsync<List<MEReglaNeg>>(NomFn());
        }
        #endregion

        #region ExpedienteEstatu (ExpeEsta)
        /// <summary>
        /// Consulta paginada de la entidad ExpedienteEstatu.
        /// </summary>
        public async Task<EExpedienteEstatuPag> ExpedienteEstatuPag(EExpedienteEstatuFiltro expedienteEstatuFiltro)
        {
            return await CallAsync<EExpedienteEstatuPag>(NomFn(), expedienteEstatuFiltro);
        }
        /// <summary>
        /// Consulta del utlimo estatus del expediente.
        /// </summary>
        /// <param name="expedienteId"></param>
        /// <returns></returns>
        public async Task<EExpedienteEstatu> ExpedienteEstatusUltimo(Int64 expedienteId)
        {
            return await CallAsync<EExpedienteEstatu>(NomFn(), expedienteId);
        }
        #endregion

        #endregion
    }
}
