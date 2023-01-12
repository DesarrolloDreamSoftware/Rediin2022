using DSEntityNetX.DataAccess;
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
        [XMain] public String SapSociedadId { get; set; } = String.Empty;
        [XMain] public String SapSociedadNombre { get; set; } = String.Empty;
        [XMain] public Boolean Activo { get; set; } = false;
        #endregion
    }
}
