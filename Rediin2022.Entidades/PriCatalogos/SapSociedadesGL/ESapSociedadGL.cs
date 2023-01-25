using DSEntityNetX.DataAccess;
using DSMetodNetX.Entidades;
using System;

namespace Rediin2022.Entidades.PriCatalogos
{
    /// <summary>
    /// Entidad.
    /// </summary>
    [Serializable]
    public class ESapSociedadGL: MEntidad
    {
        #region Propiedades
        //Columnas principales
        [XMain] public String SapSociedadGLId { get; set; } = String.Empty;
        [XMain] public String SapSociedadGLNombre { get; set; } = String.Empty;
        [XMain] public Boolean Activo { get; set; } = false;
        #endregion
    }
}
