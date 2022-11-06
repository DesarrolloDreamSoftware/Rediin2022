using DSEntityNetX.DataAccess;
using DSMetodNetX.Entidades;
using System;

namespace Rediin2022.Entidades.PriOperacion
{
    [Serializable]
    public class EExpedienteEstatu: MEntidadBase
    {
        #region Propiedades
        //Columnas principales
        [XMain] public Int64 ExpedienteId { get; set; } = 0L;
        [XMain] public Int64 ExpedienteEstatusId { get; set; } = 0L;
        [XMain] public Int64 ProcesoOperativoEstId { get; set; } = 0L;

        //Columnas vista
        public String EstatusNombre { get; set; } = String.Empty;
        public String Nombre { get; set; } = String.Empty;
        public String ApellidoPaterno { get; set; } = String.Empty;
        public String ApellidoMaterno { get; set; } = String.Empty;
        #endregion
    }
}
