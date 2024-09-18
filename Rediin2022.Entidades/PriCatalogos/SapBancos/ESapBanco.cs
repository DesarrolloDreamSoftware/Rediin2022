
using DSMetodNetX.Entidades;
using System;

namespace Rediin2022.Entidades.PriCatalogos
{
    /// <summary>
    /// Entidad.
    /// </summary>
    [Serializable]
    [MDAErrorDuplicado(-1,  nameof(SapBancoNombre))]
    public class ESapBanco: MEntidad
    {
        #region Propiedades
        //Columnas principales
        [MDAMain] public String SapBancoId { get; set; } = String.Empty;
        [MDAMain] public String SapBancoNombre { get; set; } = String.Empty;
        [MDAMain] public Boolean Activo { get; set; } = false;
        #endregion
    }
}
