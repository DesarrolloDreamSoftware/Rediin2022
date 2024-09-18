
using DSMetodNetX.Entidades;
using System;

namespace Rediin2022.Entidades.PriCatalogos
{
    [Serializable]
    [MDAErrorDuplicado(-1, nameof(ProcesoOperativoEstIdSig))]
    public class EProcesoOperativoEstSec: MEntidadBase
    {
        #region Propiedades
        //Columnas principales
        [MDAMain] public Int64 ProcesoOperativoEstId { get; set; } = 0L;
        [MDAMain] public Int64 ProcesoOperativoEstSecId { get; set; } = 0L;
        [MDAMain] public Int64 ProcesoOperativoEstIdSig { get; set; } = 0L;

        //Columnas vista
        public String EstatusNombre { get; set; } = String.Empty;
        public Int16 Orden { get; set; } = 0;
        public String EstatusNombreSig { get; set; } = String.Empty;
        #endregion
    }
}
