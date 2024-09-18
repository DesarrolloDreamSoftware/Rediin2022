
using DSMetodNetX.Entidades;
using System;

namespace Rediin2022.Entidades.PriCatalogos
{
    [Serializable]
    public class EProcesoOperativoObjeto: MEntidad
    {
        #region Propiedades
        //Columnas principales
        [MDAMain] public Int64 ProcesoOperativoId { get; set; } = 0L;
        [MDAMain] public Int64 ProcesoOperativoObjetoId { get; set; } = 0L;
        [MDAMain] public String ProcesoOperativoObjetoNombre { get; set; } = String.Empty;
        [MDAMain] public Int32 Cantidad { get; set; } = 0;
        [MDAMain] public Int16 Orden { get; set; } = 0;
        [MDAMain] public Boolean Obligatorio { get; set; } = false;
        [MDAMain] public Int16 DiasVencimiento { get; set; } = 0;
        [MDAMain] public Boolean Activo { get; set; } = false;
        #endregion
    }
}
