using DSEntityNetX.DataAccess;
using DSMetodNetX.Entidades;
using System;

namespace Rediin2022.Entidades.PriOperacion
{
    [Serializable]
    public class EConExpediente: MEntidad
    {
        #region Propiedades
        //Columnas principales
        [XMain] public Int64 ProcesoOperativoId { get; set; } = 0L;
        [XMain] public Int64 ExpedienteId { get; set; } = 0L;
        [XMain(true)] public Int64 ProcesoOperativoEstId { get; set; } = 0L;

        //Columnas vista
        public String EstatusNombre { get; set; } = String.Empty;
        public Boolean PermiteModificar { get; set; } = false;

        public Boolean ControlEstatus { get; set; } = false; //[Para visible si]

        //Columnas permite (acciones por registro)
        public Boolean PermiteActualiza { get; set; } = false;
        public Boolean PermiteIrAExpedienteEstatu { get; set; } = false;
        #endregion
    }
}
