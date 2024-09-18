
using DSMetodNetX.Entidades;
using System;

namespace Rediin2022.Entidades.PriCatalogos
{
    /// <summary>
    /// Entidad.
    /// </summary>
    [Serializable]
    [MDAErrorDuplicado(-1, nameof(BancoNombre))]
    public class EBanco : MEntidad
    {
        #region Propiedades
        //Columnas principales
        [MDAMain] public Int64 BancoId { get; set; } = 0L;
        [MDAMain] public String BancoNombre { get; set; } = String.Empty;
        [MDAMain] public Boolean Activo { get; set; } = false;
        #endregion
    }
}
