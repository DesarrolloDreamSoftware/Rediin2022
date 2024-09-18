
using DSMetodNetX.Entidades;
using System;

namespace Rediin2022.Entidades.PriCatalogos
{
    [Serializable]
    public class EAutorizacion: MEntidad
    {
        #region Propiedades
        //Columnas principales
        [MDAMain] public Int64 EstablecimientoId { get; set; } = 0L;
        [MDAMain] public Int64 AutorizacionId { get; set; } = 0L;
        [MDAMain] public String AutorizacionNombre { get; set; } = String.Empty;
        [MDAMain] public Int64 ProcesoOperativoId { get; set; } = 0L;

        //Columnas vista
        public String ProcesoOperativoNombre { get; set; } = String.Empty;
        #endregion
    }
}
