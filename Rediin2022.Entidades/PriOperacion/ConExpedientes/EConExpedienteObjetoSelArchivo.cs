using DSEntityNetX.DataAccess;
using DSMetodNetX.Entidades;
using System;

namespace Rediin2022.Entidades.PriOperacion
{
    /// <summary>
    /// Entidad para accion personalizada SelArchivo.
    /// </summary>
    [Serializable]
    public class EConExpedienteObjetoSelArchivo
    {
        #region Propiedades
        //Columnas Captura
        [XMain] public Int64 ExpedienteId { get; set; } = 0L; //[Actualizar en Bd]
        [XMain] public Int64 ExpedienteObjetoId { get; set; } = 0L;
        [XMain] public String ArchivoNombre { get; set; } = String.Empty;
        [XMain] public String Ruta { get; set; } = String.Empty; //[Actualizar en Bd]
        #endregion
    }
}
