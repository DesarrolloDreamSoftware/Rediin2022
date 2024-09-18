
using DSMetodNetX.Entidades;
using System;

namespace Rediin2022.Entidades.PriCatalogos
{
    /// <summary>
    /// Entidad.
    /// </summary>
    [Serializable]
    public class ESapOrganizacionCompra: MEntidad
    {
        #region Propiedades
        //Columnas principales
        [MDAMain] public String SapOrganizacionCompraId { get; set; } = String.Empty;
        [MDAMain] public String SapOrganizacionCompraNombre { get; set; } = String.Empty;
        [MDAMain] public Boolean Activo { get; set; } = false;
        #endregion
    }
}
