using DSEntityNetX.DataAccess;
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
        [XMain] public String SapTratamientoId { get; set; } = String.Empty;
        [XMain] public String SapTratamientoNombre { get; set; } = String.Empty;
        [XMain] public Boolean Activo { get; set; } = false;
        #endregion
    }
}
