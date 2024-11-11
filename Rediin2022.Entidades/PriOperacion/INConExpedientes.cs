using DSMetodNetX.Entidades;

using Rediin2022.Entidades.PriCatalogos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rediin2022.Entidades.PriOperacion;

public interface INConExpedientes : IMCtrMensajes
{
    #region Funciones

    #region ConExpProcOperativo (Enc)
    /// <summary>
    /// Consulta paginada de la entidad ConExpProcOperativo.
    /// </summary>
    Task<EConExpProcOperativoPag> ConExpProcOperativoPag(EConExpProcOperativoFiltro conExpProcOperativoFiltro);

    #endregion

    #region ConExpediente (Exp)
    /// <summary>
    /// Consulta paginada de la entidad ConExpediente.
    /// </summary>
    Task<EConExpedientePag> ConExpedientePag(EConExpedienteFiltro conExpedienteFiltro);
    /// <summary>
    /// Consulta por id de la entidad ConExpediente.
    /// </summary>
    /// <param name="expedienteId"></param>
    /// <returns></returns>
    Task<EConExpediente> ConExpedienteXId(Int64 expedienteId);
    /// <summary>
    /// Permite insertar la entidad ConExpediente.
    /// </summary>
    Task<Int64> ConExpedienteInserta(EConExpediente conExpediente);
    /// <summary>
    /// Permite actualizar la entidad ConExpediente.
    /// </summary>
    Task<Boolean> ConExpedienteActualiza(EConExpediente conExpediente);
    /// <summary>
    /// Permite eliminar la entidad ConExpediente.
    /// </summary>
    Task<Boolean> ConExpedienteElimina(EConExpediente conExpediente);
    /// <summary>
    /// Accion personalizada CambioEstatus.
    /// </summary>
    Task<Boolean> ConExpedienteCambioEstatus(EConExpedienteCambioEstatus conExpedienteCambioEstatus);
    /// <summary>
    /// Reglas de negocio de la entidad ConExpediente.
    /// </summary>
    Task<List<MEReglaNeg>> ConExpedienteReglas();
    ///// <summary>
    ///// Consulta para los combos que se capturan.
    ///// </summary>
    //Task<List<MEElemento>> ConExpedienteCmb(EProcesoOperativoCol procesoOperativoCol);
    #endregion

    #region ConExpedienteObjeto (Objs)
    /// <summary>
    /// Consulta paginada de la entidad ConExpedienteObjeto.
    /// </summary>
    Task<EConExpedienteObjetoPag> ConExpedienteObjetoPag(EConExpedienteObjetoFiltro conExpedienteObjetoFiltro);
    /// <summary>
    /// Permite insertar la entidad ConExpedienteObjeto.
    /// </summary>
    Task<Int64> ConExpedienteObjetoInserta(EConExpedienteObjeto conExpedienteObjeto);
    /// <summary>
    /// Permite actualizar la entidad ConExpedienteObjeto.
    /// </summary>
    /// <param name="conExpedienteObjeto"></param>
    /// <returns></returns>
    Task<Boolean> ConExpedienteObjetoActualiza(EConExpedienteObjeto conExpedienteObjeto);
    /// <summary>
    /// Permite eliminar la entidad ConExpedienteObjeto.
    /// </summary>
    Task<Boolean> ConExpedienteObjetoElimina(EConExpedienteObjeto conExpedienteObjeto);
    //Eli
    ///// <summary>
    ///// Accion personalizada Descarga.
    ///// </summary>
    //    Task<Boolean> ConExpedienteObjetoDescarga();
    /// <summary>
    /// Accion personalizada SelArchivo.
    /// </summary>
    Task<Boolean> ConExpedienteObjetoSelArchivo(EConExpedienteObjetoSelArchivo conExpedienteObjetoSelArchivo);
    /// <summary>
    /// Reglas de negocio de la entidad ConExpedienteObjeto.
    /// </summary>
    Task<List<MEReglaNeg>> ConExpedienteObjetoReglas();

    ///// <summary>
    ///// Reglas de negocio de la entidad ConExpedienteObjeto.
    ///// </summary>
    //    Task<Byte[]> ConExpedienteDescarga(String entidad, Int32 expedienteId, String nombreArchivo);
    #endregion

    #region ExpedienteEstatu (ExpeEsta)
    /// <summary>
    /// Consulta paginada de la entidad ExpedienteEstatu.
    /// </summary>
    Task<EExpedienteEstatuPag> ExpedienteEstatuPag(EExpedienteEstatuFiltro expedienteEstatuFiltro);
    /// <summary>
    /// Consulta del utlimo estatus del expediente.
    /// </summary>
    /// <param name="expedienteId"></param>
    /// <returns></returns>
    Task<EExpedienteEstatu> ExpedienteEstatusUltimo(Int64 expedienteId);
    #endregion

    #endregion
}
