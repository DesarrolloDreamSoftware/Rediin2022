using DSMetodNetX.Entidades;
using Rediin2022.Entidades.PriCatalogos;
using System;
using System.Collections.Generic;

namespace Rediin2022.Aplicacion.PriCatalogos;

/// <summary>
/// Entidad de variables.
/// </summary>
[Serializable]
public class EVRegimenesFiscales
{
    #region Propiedades
    /// <summary>
    /// Control de acciones Inserta, Actualiza y Elimina.
    /// </summary>
    public MAccionesGen Accion { get; set; } = MAccionesGen.Ninguna;

    //RegimenFiscal (RegimenesFiscales)
    /// <summary>
    /// Variables de RegimenFiscal(RegimenesFiscales).
    /// </summary>
    public MEVSF<ERegimenFiscal, ERegimenFiscalPag, ERegimenFiscalFiltro> RegimenFiscal { get; set; } = new();
    #endregion
}
