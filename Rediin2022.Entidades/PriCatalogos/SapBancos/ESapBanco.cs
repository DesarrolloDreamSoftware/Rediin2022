using DSEntityNetX.DataAccess;
using DSMetodNetX.Entidades;
using System;

namespace Rediin2022.Entidades.PriCatalogos
{
    /// <summary>
    /// Entidad.
    /// </summary>
    [Serializable]
    public class ESapBanco: MEntidad
    {
        #region Propiedades
        //Columnas principales
        [XMain] public String SapBancoId { get; set; } = String.Empty;
        [XMain] public String SapBancoNombre { get; set; } = String.Empty;
        [XMain] public Boolean Activo { get; set; } = false;
        #endregion
    }
}
