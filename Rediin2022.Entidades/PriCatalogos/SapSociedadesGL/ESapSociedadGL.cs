
using DSMetodNetX.Entidades;
using System;

namespace Rediin2022.Entidades.PriCatalogos
{
    /// <summary>
    /// Entidad.
    /// </summary>
    [Serializable]
    [MDAErrorDuplicado(-1, nameof(SapSociedadGLNombre))]
    public class ESapSociedadGL: MEntidad
    {
        #region Propiedades
        //Columnas principales
        [MDAMain] public String SapSociedadGLId { get; set; } = String.Empty;
        [MDAMain] public String SapSociedadGLNombre { get; set; } = String.Empty;
        [MDAMain] public Boolean Activo { get; set; } = false;
        #endregion
    }
}
