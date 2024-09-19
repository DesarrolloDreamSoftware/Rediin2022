using DSMetodNetX.Entidades;
using System;
using System.Collections.Generic;

namespace Rediin2022.Entidades.PriCatalogos;

/// <summary>
/// Entidad de filtro.
/// </summary>
[Serializable]
public class EIncotermFiltro : MEFiltro
{
    #region Propiedades
    //Columnas principales
    public String FilIncotermNombre { get; set; } = String.Empty;
    public Boolean FiltraFilActivo { get; set; } = false;
    public Boolean FilActivo { get; set; } = false;
    #endregion
}
