using DSMetodNetX.Entidades;
using Rediin2022.Entidades.PriCatalogos;
using System;
using System.Collections.Generic;

namespace Rediin2022.Aplicacion.PriCatalogos;

/// <summary>
/// Entidad de variables.
/// </summary>
[Serializable]
public class EVModelos
{
    #region Propiedades
    /// <summary>
    /// Control de acciones Inserta, Actualiza y Elimina.
    /// </summary>
    public MAccionesGen Accion { get; set; } = MAccionesGen.Ninguna;

    //Modelo (Modelos)
    /// <summary>
    /// Variables de Modelo(Modelos).
    /// </summary>
    public MEVSF<EModelo, EModeloPag, EModeloFiltro> Modelo { get; set; } = new();
    #endregion
}
