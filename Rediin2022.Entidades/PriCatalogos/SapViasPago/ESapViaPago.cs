using DSEntityNetX.DataAccess;
using DSMetodNetX.Entidades;
using System;

namespace Rediin2022.Entidades.PriCatalogos
{
    /// <summary>
    /// Entidad.
    /// </summary>
    [Serializable]
    public class ESapViaPago: MEntidad
    {
        #region Propiedades
        //Columnas principales
        [XMain] public String SapViaPagoId { get; set; } = String.Empty;
        [XMain] public String SapViaPagoNombre { get; set; } = String.Empty;
        [XMain] public Boolean Activo { get; set; } = false;
        #endregion
    }
}
