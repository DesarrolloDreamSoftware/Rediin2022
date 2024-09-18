
using DSMetodNetX.Entidades;
using System;

namespace Rediin2022.Entidades.PriOperacion
{
    [Serializable]
    public class EConExpProcOperativo: MEntidad
    {
        #region Propiedades
        //Columnas principales
        [MDAMain] public Int64 EstablecimientoId { get; set; } = 0L;
        [MDAMain] public Int64 ProcesoOperativoId { get; set; } = 0L;
        [MDAMain] public String ProcesoOperativoNombre { get; set; } = String.Empty;
        [MDAMain] public Int16 Orden { get; set; } = 0;
        [MDAMain] public Boolean ControlEstatus { get; set; } = false;
        [MDAMain] public Boolean Activo { get; set; } = false;
        #endregion
    }
}
