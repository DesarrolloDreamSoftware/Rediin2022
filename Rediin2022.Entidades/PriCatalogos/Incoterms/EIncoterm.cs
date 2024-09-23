using DSMetodNetX.Entidades;
using System;

namespace Rediin2022.Entidades.PriCatalogos;

/// <summary>
/// Entidad.
/// </summary>
[Serializable]
[MDAConsec(nameof(IncotermId))]
[MDAErrorDuplicado(-1, nameof(IncotermNombre))]
public class EIncoterm: MEntidad
{
    #region Propiedades
    //Columnas principales
    [MDAMain] public Int64 IncotermId { get; set; } = 0L;
    [MDAMain] public String IncotermClave { get; set; } = String.Empty;
    [MDAMain] public String IncotermNombre { get; set; } = String.Empty;
    [MDAMain] public Boolean Activo { get; set; } = false;
    #endregion
}
