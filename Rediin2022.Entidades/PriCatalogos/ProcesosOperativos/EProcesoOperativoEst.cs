using DSEntityNetX.DataAccess;
using DSMetodNetX.Entidades;
using System;

namespace Rediin2022.Entidades.PriCatalogos
{
    [Serializable]
    public class EProcesoOperativoEst: MEntidad
    {
        #region Propiedades
        //Columnas principales
        [XMain] public Int64 ProcesoOperativoId { get; set; } = 0L;
        [XMain] public Int64 ProcesoOperativoEstId { get; set; } = 0L;
        [XMain] public String EstatusNombre { get; set; } = String.Empty;
        [XMain] public Int16 Orden { get; set; } = 0;
        [XMain] public Boolean PermiteModificar { get; set; } = false;
        [XMain] public Boolean Activo { get; set; } = false;
        #endregion
    }
}
