using DSEntityNetX.DataAccess;
using DSMetodNetX.Entidades;
using System;

namespace Rediin2022.Entidades.PriOperacion
{
    [Serializable]
    public class EConExpProcOperativo: MEntidad
    {
        #region Propiedades
        //Columnas principales
        [XMain] public Int64 EstablecimientoId { get; set; } = 0L;
        [XMain] public Int64 ProcesoOperativoId { get; set; } = 0L;
        [XMain] public String ProcesoOperativoNombre { get; set; } = String.Empty;
        [XMain] public Int16 Orden { get; set; } = 0;
        [XMain] public Boolean ControlEstatus { get; set; } = false;
        [XMain] public Boolean Activo { get; set; } = false;
        #endregion
    }
}
