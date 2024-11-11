using DSMetodNetX.Entidades;
using Rediin2022.Entidades.PriClientes.Expedientes;
using Rediin2022.Entidades.PriOperacion;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rediin2022.Entidades.PriClientes;

public interface INExpedientes : IMCtrMensajes
{
    #region Funciones para el cliente
    Task<Int64> ExpedienteInserta(EExpediente expediente);
    Task<Boolean> ExpedienteElimina(Int64 expedienteId);
    Task<Boolean> ExpedienteActualiza(EExpediente expediente);
    Task<Int64> ObjetoInserta(EExpedienteObjeto expedienteObjeto);
    /// <summary>
    /// Sube el documento y modifica su nombre.
    /// Es necesario cargar los campos ExpedienteId, ExpedienteObjetoId, ArchivoNombre y Archivo.
    /// </summary>
    /// <param name="documento"></param>
    /// <returns></returns>
    Task<Boolean> ObjetoActualiza(EExpedienteObjeto expedienteObjeto);
    /// <summary>
    /// Descargar solo el documento
    /// </summary>
    /// <param name="expendienteId"></param>
    /// <param name="archivoNombre"></param>
    /// <returns></returns>
    Task<EDocumento> ObjectoDescargaDocto(Int64 expendienteId, String archivoNombre);
    ///// <summary>
    ///// Listados para cargar los combos con expedientes de procesos operativos que son catalogos
    ///// </summary>
    ///// <param name="expendienteDatCmb"></param>
    ///// <returns></returns>
    //Task<List<MEElemento>> ConExpedienteCmb(EExpendienteDatCmb expendienteDatCmb);
    #endregion
}
