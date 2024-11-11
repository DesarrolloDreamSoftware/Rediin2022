using DSMetodNetX.Api.Seguridad;
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
using System.Threading.Tasks;

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
        public async Task<EConExpProcOperativoPag> ConExpProcOperativoPag(EConExpProcOperativoFiltro conExpProcOperativoFiltro)
        {
            return await NConExpedientes.ConExpProcOperativoPag(conExpProcOperativoFiltro);
        }
        #endregion

        #region ConExpediente (Exp)
        /// <summary>
        /// Consulta paginada de la entidad ConExpediente.
        /// </summary>
        [HttpPost]
        public async Task<EConExpedientePag> ConExpedientePag(EConExpedienteFiltro conExpedienteFiltro)
        {
            return await NConExpedientes.ConExpedientePag(conExpedienteFiltro);
        }
        /// <summary>
        /// Consulta por id de la entidad ConExpediente.
        /// </summary>
        /// <param name="expedienteId"></param>
        /// <returns></returns>
        [HttpGet("{expedienteId}")]
        public async Task<EConExpediente> ConExpedienteXId(Int64 expedienteId)
        {
            return await NConExpedientes.ConExpedienteXId(expedienteId);
        }
        /// <summary>
        /// Permite insertar la entidad ConExpediente.
        /// </summary>
        [HttpPost]
        public async Task<Int64> ConExpedienteInserta(EConExpediente conExpediente)
        {
            return await NConExpedientes.ConExpedienteInserta(conExpediente);
        }
        /// <summary>
        /// Permite actualizar la entidad ConExpediente.
        /// </summary>
        [HttpPost]
        public async Task<Boolean> ConExpedienteActualiza(EConExpediente conExpediente)
        {
            return await NConExpedientes.ConExpedienteActualiza(conExpediente);
        }
        /// <summary>
        /// Permite eliminar la entidad ConExpediente.
        /// </summary>
        [HttpPost]
        public async Task<Boolean> ConExpedienteElimina(EConExpediente conExpediente)
        {
            return await NConExpedientes.ConExpedienteElimina(conExpediente);
        }
        /// <summary>
        /// Accion personalizada CambioEstatus.
        /// </summary>
        [HttpPost]
        public async Task<Boolean> ConExpedienteCambioEstatus(EConExpedienteCambioEstatus conExpedienteCambioEstatus)
        {
            return await NConExpedientes.ConExpedienteCambioEstatus(conExpedienteCambioEstatus);
        }
        /// <summary>
        /// Reglas de negocio de la entidad ConExpediente.
        /// </summary>
        [HttpGet]
        public async Task<List<MEReglaNeg>> ConExpedienteReglas()
        {
            return await NConExpedientes.ConExpedienteReglas();
        }
        ///// <summary>
        ///// Consulta para los combos que se capturan.
        ///// </summary>
        //[HttpPost]
        //public async Task<List<MEElemento>> ConExpedienteCmb(EProcesoOperativoCol procesoOperativoCol)
        //{
        //    return await NConExpedientes.ConExpedienteCmb(procesoOperativoCol);
        //}
        #endregion

        #region ConExpedienteObjeto (Objs)
        /// <summary>
        /// Consulta paginada de la entidad ConExpedienteObjeto.
        /// </summary>
        [HttpPost]
        public async Task<EConExpedienteObjetoPag> ConExpedienteObjetoPag(EConExpedienteObjetoFiltro conExpedienteObjetoFiltro)
        {
            return await NConExpedientes.ConExpedienteObjetoPag(conExpedienteObjetoFiltro);
        }
        /// <summary>
        /// Permite insertar la entidad ConExpedienteObjeto.
        /// </summary>
        [HttpPost]
        public async Task<Int64> ConExpedienteObjetoInserta(EConExpedienteObjeto conExpedienteObjeto)
        {
            return await NConExpedientes.ConExpedienteObjetoInserta(conExpedienteObjeto);
        }
        /// <summary>
        /// Permite actualizar la entidad ConExpedienteObjeto.
        /// </summary>
        /// <param name="conExpedienteObjeto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Boolean> ConExpedienteObjetoActualiza(EConExpedienteObjeto conExpedienteObjeto)
        {
            return await NConExpedientes.ConExpedienteObjetoActualiza(conExpedienteObjeto);
        }
        /// <summary>
        /// Permite eliminar la entidad ConExpedienteObjeto.
        /// </summary>
        [HttpPost]
        public async Task<Boolean> ConExpedienteObjetoElimina(EConExpedienteObjeto conExpedienteObjeto)
        {
            return await NConExpedientes.ConExpedienteObjetoElimina(conExpedienteObjeto);
        }
        //Eli
        ///// <summary>
        ///// Accion personalizada Descarga.
        ///// </summary>
        //public Boolean ConExpedienteObjetoDescarga()
        //{
        //    return await NConExpedientes.ConExpedienteObjetoDescarga();
        //}

        /// <summary>
        /// Accion personalizada SelArchivo.
        /// </summary>
        [HttpPost]
        public async Task<Boolean> ConExpedienteObjetoSelArchivo(EConExpedienteObjetoSelArchivo conExpedienteObjetoSelArchivo)
        {
            return await NConExpedientes.ConExpedienteObjetoSelArchivo(conExpedienteObjetoSelArchivo);
        }
        /// <summary>
        /// Reglas de negocio de la entidad ConExpedienteObjeto.
        /// </summary>
        [HttpGet]
        public async Task<List<MEReglaNeg>> ConExpedienteObjetoReglas()
        {
            return await NConExpedientes.ConExpedienteObjetoReglas();
        }
        ///// <summary>
        ///// Reglas de negocio de la entidad ConExpedienteObjeto.
        ///// </summary>
        //[HttpGet("{entidad}/{expedienteId}/{nombreArchivo}")]
        //public Byte[] ConExpedienteDescarga(String entidad, Int32 expedienteId, String nombreArchivo)
        //{
        //    String vRutaConArchivo = Path.Combine(base.MValorConfig<String>(MConfigIds.dirBD), entidad, expedienteId.ToString(), nombreArchivo);
        //    using MemoryStream vMS = new MemoryStream();
        //    using FileStream vFS = new FileStream(vRutaConArchivo, FileMode.Open);
        //    vFS.CopyTo(vMS);
        //    return await vMS.GetBuffer();
        //}
        #endregion

        #region ExpedienteEstatu (ExpeEsta)
        /// <summary>
        /// Consulta paginada de la entidad ExpedienteEstatu.
        /// </summary>
        [HttpPost]
        public async Task<EExpedienteEstatuPag> ExpedienteEstatuPag(EExpedienteEstatuFiltro expedienteEstatuFiltro)
        {
            return await NConExpedientes.ExpedienteEstatuPag(expedienteEstatuFiltro);
        }
        /// <summary>
        /// Consulta del utlimo estatus del expediente.
        /// </summary>
        /// <param name="expedienteId"></param>
        /// <returns></returns>
        [HttpGet("{expedienteId}")]
        public async Task<EExpedienteEstatu> ExpedienteEstatusUltimo(Int64 expedienteId)
        {
            return await NConExpedientes.ExpedienteEstatusUltimo(expedienteId);
        }
        #endregion

        #endregion
    }
}
