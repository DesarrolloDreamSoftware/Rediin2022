
using DSMetodNetX.Entidades;
using System;

namespace Rediin2022.Entidades.PriCatalogos
{
    /// <summary>
    /// Entidad.
    /// </summary>
    [Serializable]
    [MDAErrorDuplicado(-1, nameof(IdentificacionNombre))]
    public class EIdentificacion: MEntidad
    {
        #region Propiedades
        //Columnas principales
        [MDAMain] public Int64 IdentificacionId { get; set; } = 0L;
        [MDAMain] public String IdentificacionNombre { get; set; } = String.Empty;
        [MDAMain] public Boolean Activo { get; set; } = false;
        #endregion
    }
}
