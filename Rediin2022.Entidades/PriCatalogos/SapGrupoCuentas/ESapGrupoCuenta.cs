
using DSMetodNetX.Entidades;
using System;

namespace Rediin2022.Entidades.PriCatalogos
{
    /// <summary>
    /// Entidad.
    /// </summary>
    [Serializable]
    [MDAErrorDuplicado(-1, nameof(SapGrupoCuentaNombre))]
    public class ESapGrupoCuenta: MEntidad
    {
        #region Propiedades
        //Columnas principales
        [MDAMain] public String SapGrupoCuentaId { get; set; } = String.Empty;
        [MDAMain] public String SapGrupoCuentaNombre { get; set; } = String.Empty;
        [MDAMain] public Boolean Activo { get; set; } = false;
        #endregion
    }
}
