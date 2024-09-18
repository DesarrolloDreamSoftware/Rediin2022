
using DSMetodNetX.Entidades;
using System;

namespace Rediin2022.Entidades.PriCatalogos
{
    [Serializable]
    [MDAErrorDuplicado(-1, nameof(EstatusNombre))]
    public class EProcesoOperativoEst: MEntidad
    {
        #region Propiedades
        //Columnas principales
        [MDAMain] public Int64 ProcesoOperativoId { get; set; } = 0L;
        [MDAMain] public Int64 ProcesoOperativoEstId { get; set; } = 0L;
        [MDAMain] public String EstatusNombre { get; set; } = String.Empty;
        [MDAMain] public Int16 Orden { get; set; } = 0;
        [MDAMain] public Boolean PermiteModificar { get; set; } = false;
        [MDAMain] public Boolean Activo { get; set; } = false;
        #endregion
    }
}
