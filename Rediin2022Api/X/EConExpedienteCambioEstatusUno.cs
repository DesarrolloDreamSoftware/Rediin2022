using DSEntityNetX.DataAccess;
using DSMetodNetX.Entidades;
using System;

namespace Rediin2022.Entidades.PriOperacion
{
    [Serializable]
    public class EConExpedienteCambioEstatusUno
    {
        #region Propiedades
        [XMain] public Int64 ExpedienteId { get; set; } = 0L; //[Actualizar en Bd]
        [XMain] public Int64 ProcesoOperativoEstId { get; set; } = 0L; //[Actualizar en Bd]
        #endregion
    }
}
