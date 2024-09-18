
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
        [MDAMain] public Int64 ExpedienteId { get; set; } = 0L; //[Actualizar en Bd]
        [MDAMain] public Int64 ExpedienteObjetoId { get; set; } = 0L;
        [MDAMain] public String ArchivoNombre { get; set; } = String.Empty;
        [MDAMain] public String Ruta { get; set; } = String.Empty; //[Actualizar en Bd]
        #endregion
    }
}
