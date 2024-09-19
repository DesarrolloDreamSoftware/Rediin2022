using DSMetodNetX.Entidades;
using System;
using System.Collections.Generic;

namespace Rediin2022.Entidades.PriCatalogos;

/// <summary>
/// Entidad de filtro.
/// </summary>
[Serializable]
public class ERegimenFiscalFiltro : MEFiltro
{
    #region Propiedades
    //Columnas principales
    public String FilRegimenFiscalNombre { get; set; } = String.Empty;
    public Boolean FiltraFilActivo { get; set; } = false;
    public Boolean FilActivo { get; set; } = false;
    #endregion
}
