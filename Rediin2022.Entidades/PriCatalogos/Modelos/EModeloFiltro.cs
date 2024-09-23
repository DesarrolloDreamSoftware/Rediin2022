using DSMetodNetX.Entidades;
using System;
using System.Collections.Generic;

namespace Rediin2022.Entidades.PriCatalogos;

/// <summary>
/// Entidad de filtro.
/// </summary>
[Serializable]
public class EModeloFiltro : MEFiltro
{
    #region Propiedades
    //Columnas principales
    public String FilModeloNombre { get; set; } = String.Empty;
    public TipoCaptura FilTipoCapturaId { get; set; } = TipoCaptura.Ninguno;
    public Boolean FiltraFilActivo { get; set; } = false;
    public Boolean FilActivo { get; set; } = false;
    #endregion
}
