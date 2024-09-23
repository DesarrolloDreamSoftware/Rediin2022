using DSMetodNetX.Entidades;
using Rediin2022.Entidades.PriCatalogos;
using System;
using System.Collections.Generic;

namespace Rediin2022.Aplicacion.PriCatalogos;

/// <summary>
/// Entidad de variables.
/// </summary>
[Serializable]
public class EVIncoterms
{
    #region Propiedades
    /// <summary>
    /// Control de acciones Inserta, Actualiza y Elimina.
    /// </summary>
    public MAccionesGen Accion { get; set; } = MAccionesGen.Ninguna;

    //Incoterm (Incoterms)
    /// <summary>
    /// Variables de Incoterm(Incoterms).
    /// </summary>
    public MEVSF<EIncoterm, EIncotermPag, EIncotermFiltro> Incoterm { get; set; } = new();
    #endregion
}
