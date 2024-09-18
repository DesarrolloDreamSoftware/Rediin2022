
using DSMetodNetX.Entidades;
using System;

namespace Rediin2022.Entidades.PriCatalogos
{
    /// <summary>
    /// Entidad.
    /// </summary>
    [Serializable]
    public class ESapCuentaAsociada: MEntidad
    {
        #region Propiedades
        //Columnas principales
        [MDAMain] public String SapCuentaAsociadaId { get; set; } = String.Empty;
        [MDAMain] public String SapCuentaAsociadaNombre { get; set; } = String.Empty;
        [MDAMain] public Boolean Activo { get; set; } = false;
        #endregion
    }
}
