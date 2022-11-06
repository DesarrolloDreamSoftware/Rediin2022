using DSMetodNetX.Entidades;
using DSMetodNetX.Negocio;
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
        /// Accion personalizada CambioEstatusUno.
        /// </summary>
        Boolean ConExpedienteCambioEstatusUno(EConExpedienteCambioEstatusUno conExpedienteCambioEstatusUno);
        /// <summary>
        /// Accion personalizada CambioEstatusDos.
        /// </summary>
        Boolean ConExpedienteCambioEstatusDos(EConExpedienteCambioEstatusDos conExpedienteCambioEstatusDos);
        /// <summary>
        /// Reglas de negocio de la entidad ConExpediente.
        /// </summary>
        List<MEReglaNeg> ConExpedienteReglas();
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
        /// Permite eliminar la entidad ConExpedienteObjeto.
        /// </summary>
        Boolean ConExpedienteObjetoElimina(EConExpedienteObjeto conExpedienteObjeto);
        /// <summary>
        /// Accion personalizada AgregarArchivo.
        /// </summary>
        Boolean ConExpedienteObjetoAgregarArchivo();
        /// <summary>
        /// Reglas de negocio de la entidad ConExpedienteObjeto.
        /// </summary>
        List<MEReglaNeg> ConExpedienteObjetoReglas();
        #endregion

        #region ExpedienteEstatu (ExpeEsta)
        /// <summary>
        /// Consulta paginada de la entidad ExpedienteEstatu.
        /// </summary>
        EExpedienteEstatuPag ExpedienteEstatuPag(EExpedienteEstatuFiltro expedienteEstatuFiltro);
        #endregion

        #endregion
    }
}
