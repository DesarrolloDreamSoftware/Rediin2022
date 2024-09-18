
using DSMetodNetX.Entidades;
using System;

namespace Rediin2022.Entidades.PriCatalogos
{
    /// <summary>
    /// Entidad.
    /// </summary>
    [Serializable]
    [MDAErrorDuplicado(-1, nameof(SapGrupoToleranciaNombre))]
    public class ESapGrupoTolerancia: MEntidad
    {
        #region Propiedades
        //Columnas principales
        [MDAMain] public String SapGrupoToleranciaId { get; set; } = String.Empty;
        [MDAMain] public String SapGrupoToleranciaNombre { get; set; } = String.Empty;
        [MDAMain] public Boolean Activo { get; set; } = false;
        #endregion
    }
}
