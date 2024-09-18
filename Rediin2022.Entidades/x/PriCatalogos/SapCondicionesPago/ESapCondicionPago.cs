
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
        [MDAMain] public String SapCondicionPagoId { get; set; } = String.Empty;
        [MDAMain] public String SapCondicionPagoNombre { get; set; } = String.Empty;
        [MDAMain] public Boolean Activo { get; set; } = false;
        #endregion
    }
}
