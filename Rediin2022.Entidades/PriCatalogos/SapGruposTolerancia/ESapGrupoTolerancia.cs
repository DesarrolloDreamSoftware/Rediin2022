using DSEntityNetX.DataAccess;
using DSMetodNetX.Entidades;
using System;

namespace Rediin2022.Entidades.PriCatalogos
{
    /// <summary>
    /// Entidad.
    /// </summary>
    [Serializable]
    public class ESapGrupoTolerancia: MEntidad
    {
        #region Propiedades
        //Columnas principales
        [XMain] public String SapGrupoToleranciaId { get; set; } = String.Empty;
        [XMain] public String SapGrupoToleranciaNombre { get; set; } = String.Empty;
        [XMain] public Boolean Activo { get; set; } = false;
        #endregion
    }
}
