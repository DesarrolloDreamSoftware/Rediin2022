
using DSMetodNetX.Entidades;
using System;

namespace Rediin2022.Entidades.PriCatalogos
{
    /// <summary>
    /// Entidad.
    /// </summary>
    [Serializable]
    [MDAErrorDuplicado(-1, nameof(SapViaPagoNombre))]
    public class ESapViaPago: MEntidad
    {
        #region Propiedades
        //Columnas principales
        [MDAMain] public String SapViaPagoId { get; set; } = String.Empty;
        [MDAMain] public String SapViaPagoNombre { get; set; } = String.Empty;
        [MDAMain] public Boolean Activo { get; set; } = false;
        #endregion
    }
}
