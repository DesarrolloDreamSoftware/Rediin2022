
using DSMetodNetX.Entidades;
using System;

namespace Rediin2022.Entidades.PriCatalogos
{
    /// <summary>
    /// Entidad.
    /// </summary>
    [Serializable]
    public class ESapSociedad: MEntidad
    {
        #region Propiedades
        //Columnas principales
        [MDAMain] public String SapSociedadId { get; set; } = String.Empty;
        [MDAMain] public String SapSociedadNombre { get; set; } = String.Empty;
        [MDAMain] public Boolean Activo { get; set; } = false;
        #endregion
    }
}
