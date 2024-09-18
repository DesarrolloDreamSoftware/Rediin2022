
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
        [MDAMain] public String SapGrupoTesoreriaId { get; set; } = String.Empty;
        [MDAMain] public String SapGrupoTesoreriaNombre { get; set; } = String.Empty;
        [MDAMain] public Boolean Activo { get; set; } = false;
        #endregion
    }
}
