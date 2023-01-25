using DSEntityNetX.DataAccess;
using DSMetodNetX.Entidades;
using System;

namespace Rediin2022.Entidades.PriCatalogos
{
    /// <summary>
    /// Entidad.
    /// </summary>
    [Serializable]
    public class EIdentificacion: MEntidad
    {
        #region Propiedades
        //Columnas principales
        [XMain] public Int64 IdentificacionId { get; set; } = 0L;
        [XMain] public String IdentificacionNombre { get; set; } = String.Empty;
        [XMain] public Boolean Activo { get; set; } = false;
        #endregion
    }
}
