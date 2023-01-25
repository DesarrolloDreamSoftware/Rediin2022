using DSEntityNetX.DataAccess;
using DSMetodNetX.Entidades;
using System;

namespace Rediin2022.Entidades.PriCatalogos
{
    /// <summary>
    /// Entidad.
    /// </summary>
    [Serializable]
    public class ESapGrupoTesoreria: MEntidad
    {
        #region Propiedades
        //Columnas principales
        [XMain] public String SapGrupoTesoreriaId { get; set; } = String.Empty;
        [XMain] public String SapGrupoTesoreriaNombre { get; set; } = String.Empty;
        [XMain] public Boolean Activo { get; set; } = false;
        #endregion
    }
}
