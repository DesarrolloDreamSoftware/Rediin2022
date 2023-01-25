using DSEntityNetX.DataAccess;
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
        [XMain] public String SapOrganizacionCompraId { get; set; } = String.Empty;
        [XMain] public String SapOrganizacionCompraNombre { get; set; } = String.Empty;
        [XMain] public Boolean Activo { get; set; } = false;
        #endregion
    }
}
