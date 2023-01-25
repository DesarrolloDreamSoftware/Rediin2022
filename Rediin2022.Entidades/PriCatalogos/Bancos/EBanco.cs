using DSEntityNetX.DataAccess;
using DSMetodNetX.Entidades;
using System;

namespace Rediin2022.Entidades.PriCatalogos
{
    /// <summary>
    /// Entidad.
    /// </summary>
    [Serializable]
    public class EBanco: MEntidad
    {
        #region Propiedades
        //Columnas principales
        [XMain] public Int64 BancoId { get; set; } = 0L;
        [XMain] public String BancoNombre { get; set; } = String.Empty;
        [XMain] public Boolean Activo { get; set; } = false;
        #endregion
    }
}
