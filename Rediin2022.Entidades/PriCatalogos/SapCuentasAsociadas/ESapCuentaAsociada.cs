using DSEntityNetX.DataAccess;
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
        [XMain] public String SapCuentaAsociadaId { get; set; } = String.Empty;
        [XMain] public String SapCuentaAsociadaNombre { get; set; } = String.Empty;
        [XMain] public Boolean Activo { get; set; } = false;
        #endregion
    }
}
