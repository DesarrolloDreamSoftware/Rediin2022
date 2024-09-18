
using DSMetodNetX.Entidades;
using System;

namespace Rediin2022.Entidades.PriOperacion
{
    [Serializable]
    public class EExpedienteEstatu: MEntidadBase
    {
        #region Propiedades
        //Columnas principales
        [MDAMain] public Int64 ExpedienteId { get; set; } = 0L;
        [MDAMain] public Int64 ExpedienteEstatusId { get; set; } = 0L;
        [MDAMain] public Int64 ProcesoOperativoEstId { get; set; } = 0L;
		[MDAMain] public String Comentarios { get; set; } = String.Empty;

		//Columnas vista
		public String EstatusNombre { get; set; } = String.Empty;
        public String Nombre { get; set; } = String.Empty;
        public String ApellidoPaterno { get; set; } = String.Empty;
        public String ApellidoMaterno { get; set; } = String.Empty;
        #endregion
    }
}
