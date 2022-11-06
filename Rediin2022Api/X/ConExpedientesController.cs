using DSMetodNetX.Api.Seguridad;
using DSMetodNetX.Entidades;
using DSMetodNetX.Negocio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Rediin2022.Entidades.PriOperacion;
using System;
using System.Collections.Generic;

namespace Rediin2022Api.Areas.PriOperacion.Controllers
{
    [Route("ApiV1/PriOperacion/[controller]/[action]")]
    public class ConExpedientesController : MControllerApiPri, INConExpedientes
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
        public EConExpProcOperativoPag ConExpProcOperativoPag(EConExpProcOperativoFiltro conExpProcOperativoFiltro)
        {
            return NConExpedientes.ConExpProcOperativoPag(conExpProcOperativoFiltro);
        }
        #endregion

        #region ConExpediente (Exp)
        /// <summary>
        /// Consulta paginada de la entidad ConExpediente.
        /// </summary>
        public EConExpedientePag ConExpedientePag(EConExpedienteFiltro conExpedienteFiltro)
        {
            return NConExpedientes.ConExpedientePag(conExpedienteFiltro);
        }
        /// <summary>
        /// Permite insertar la entidad ConExpediente.
        /// </summary>
        public Int64 ConExpedienteInserta(EConExpediente conExpediente)
        {
            return NConExpedientes.ConExpedienteInserta(conExpediente);
        }
        /// <summary>
        /// Permite actualizar la entidad ConExpediente.
        /// </summary>
        public Boolean ConExpedienteActualiza(EConExpediente conExpediente)
        {
            return NConExpedientes.ConExpedienteActualiza(conExpediente);
        }
        /// <summary>
        /// Permite eliminar la entidad ConExpediente.
        /// </summary>
        public Boolean ConExpedienteElimina(EConExpediente conExpediente)
        {
            return NConExpedientes.ConExpedienteElimina(conExpediente);
        }
        /// <summary>
        /// Accion personalizada CambioEstatusUno.
        /// </summary>
        public Boolean ConExpedienteCambioEstatusUno(EConExpedienteCambioEstatusUno conExpedienteCambioEstatusUno)
        {
            return NConExpedientes.ConExpedienteCambioEstatusUno(conExpedienteCambioEstatusUno);
        }
        /// <summary>
        /// Accion personalizada CambioEstatusDos.
        /// </summary>
        public Boolean ConExpedienteCambioEstatusDos(EConExpedienteCambioEstatusDos conExpedienteCambioEstatusDos)
        {
            return NConExpedientes.ConExpedienteCambioEstatusDos(conExpedienteCambioEstatusDos);
        }
        /// <summary>
        /// Reglas de negocio de la entidad ConExpediente.
        /// </summary>
        public List<MEReglaNeg> ConExpedienteReglas()
        {
            return NConExpedientes.ConExpedienteReglas();
        }
        #endregion

        #region ConExpedienteObjeto (Objs)
        /// <summary>
        /// Consulta paginada de la entidad ConExpedienteObjeto.
        /// </summary>
        public EConExpedienteObjetoPag ConExpedienteObjetoPag(EConExpedienteObjetoFiltro conExpedienteObjetoFiltro)
        {
            return NConExpedientes.ConExpedienteObjetoPag(conExpedienteObjetoFiltro);
        }
        /// <summary>
        /// Permite insertar la entidad ConExpedienteObjeto.
        /// </summary>
        public Int64 ConExpedienteObjetoInserta(EConExpedienteObjeto conExpedienteObjeto)
        {
            return NConExpedientes.ConExpedienteObjetoInserta(conExpedienteObjeto);
        }
        /// <summary>
        /// Permite eliminar la entidad ConExpedienteObjeto.
        /// </summary>
        public Boolean ConExpedienteObjetoElimina(EConExpedienteObjeto conExpedienteObjeto)
        {
            return NConExpedientes.ConExpedienteObjetoElimina(conExpedienteObjeto);
        }
        /// <summary>
        /// Accion personalizada AgregarArchivo.
        /// </summary>
        public Boolean ConExpedienteObjetoAgregarArchivo()
        {
            return NConExpedientes.ConExpedienteObjetoAgregarArchivo();
        }
        /// <summary>
        /// Reglas de negocio de la entidad ConExpedienteObjeto.
        /// </summary>
        public List<MEReglaNeg> ConExpedienteObjetoReglas()
        {
            return NConExpedientes.ConExpedienteObjetoReglas();
        }
        #endregion

        #region ExpedienteEstatu (ExpeEsta)
        /// <summary>
        /// Consulta paginada de la entidad ExpedienteEstatu.
        /// </summary>
        public EExpedienteEstatuPag ExpedienteEstatuPag(EExpedienteEstatuFiltro expedienteEstatuFiltro)
        {
            return NConExpedientes.ExpedienteEstatuPag(expedienteEstatuFiltro);
        }
        #endregion

        #endregion
    }
}
