using DSEntityNetX.DataAccess;
using DSMetodNetX.Entidades;
using System;

namespace Rediin2022.Entidades.PriCatalogos
{
    [Serializable]
    public class EAutorizacion: MEntidad
    {
        #region Propiedades
        //Columnas principales
        [XMain] public Int64 EstablecimientoId { get; set; } = 0L;
        [XMain] public Int64 AutorizacionId { get; set; } = 0L;
        [XMain] public String AutorizacionNombre { get; set; } = String.Empty;
        [XMain] public Int64 ProcesoOperativoId { get; set; } = 0L;

        //Columnas vista
        public String ProcesoOperativoNombre { get; set; } = String.Empty;
        #endregion
    }
}
