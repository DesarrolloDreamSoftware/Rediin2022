using DSMetodNetX.Entidades;
using System;

namespace Rediin2022.Entidades.PriCatalogos;

/// <summary>
/// Entidad.
/// </summary>
[Serializable]
[MDAConsec(nameof(ModeloId))]
[MDAErrorDuplicado(-1, nameof(ModeloNombre))]
public class EModelo: MEntidad
{
    #region Propiedades
    //Columnas principales
    [MDAMain] public Int64 ModeloId { get; set; } = 0L;
    [MDAMain] public String ModeloNombre { get; set; } = String.Empty;
    [MDAMain] public TipoCaptura TipoCapturaId { get; set; } = TipoCaptura.Ninguno;
    [MDAMain] public Boolean Activo { get; set; } = false;
    #endregion
}
