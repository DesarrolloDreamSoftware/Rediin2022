using DSEntityNetX.DataAccess;
using DSMetodNetX.Entidades;
using System;

namespace Rediin2022.Entidades.PriOperacion
{
    [Serializable]
    public class EConExpedienteCambioEstatus
    {
        #region Propiedades
        //Columnas Captura
        [XMain] public String Comentarios { get; set; } = String.Empty;

        [XMain] public Int64 ExpedienteId { get; set; } = 0L; //[Actualizar en Bd]
        [XMain] public Int64 ProcesoOperativoEstId { get; set; } = 0L; //[Actualizar en Bd]
        #endregion
    }
}