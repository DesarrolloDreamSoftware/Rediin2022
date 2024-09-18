
using DSMetodNetX.Entidades;
using System;

namespace Rediin2022.Entidades.PriCatalogos
{
    /// <summary>
    /// Entidad.
    /// </summary>
    [Serializable]
    public class ESapTratamiento: MEntidad
    {
        #region Propiedades
        //Columnas principales
        [MDAMain] public String SapTratamientoId { get; set; } = String.Empty;
        [MDAMain] public String SapTratamientoNombre { get; set; } = String.Empty;
        [MDAMain] public Boolean Activo { get; set; } = false;
        #endregion
    }
}
