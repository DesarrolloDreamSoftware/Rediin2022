using DSEntityNetX.DataAccess;
using DSMetodNetX.Entidades;
using System;

namespace Rediin2022.Entidades.PriCatalogos
{
    /// <summary>
    /// Entidad.
    /// </summary>
    [Serializable]
    public class ESapCondicionPago: MEntidad
    {
        #region Propiedades
        //Columnas principales
        [XMain] public String SapCondicionPagoId { get; set; } = String.Empty;
        [XMain] public String SapCondicionPagoNombre { get; set; } = String.Empty;
        [XMain] public Boolean Activo { get; set; } = false;
        #endregion
    }
}
