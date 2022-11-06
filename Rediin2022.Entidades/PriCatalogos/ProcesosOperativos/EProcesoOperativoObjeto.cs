using DSEntityNetX.DataAccess;
using DSMetodNetX.Entidades;
using System;

namespace Rediin2022.Entidades.PriCatalogos
{
    [Serializable]
    public class EProcesoOperativoObjeto: MEntidad
    {
        #region Propiedades
        //Columnas principales
        [XMain] public Int64 ProcesoOperativoId { get; set; } = 0L;
        [XMain] public Int64 ProcesoOperativoObjetoId { get; set; } = 0L;
        [XMain] public String ProcesoOperativoObjetoNombre { get; set; } = String.Empty;
        [XMain] public Int32 Cantidad { get; set; } = 0;
        [XMain] public Int16 Orden { get; set; } = 0;
        [XMain] public Boolean Obligatorio { get; set; } = false;
        [XMain] public Int16 DiasVencimiento { get; set; } = 0;
        [XMain] public Boolean Activo { get; set; } = false;
        #endregion
    }
}
