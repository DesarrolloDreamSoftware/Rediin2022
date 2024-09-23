using DSMetodNetX.Entidades;
using System;

namespace Rediin2022.Entidades.PriCatalogos;

/// <summary>
/// Entidad.
/// </summary>
[Serializable]
[MDAConsec(nameof(RegimenFiscaId))]
public class ERegimenFiscal: MEntidad
{
    #region Propiedades
    //Columnas principales
    [MDAMain] public Int64 RegimenFiscaId { get; set; } = 0L;
    [MDAMain] public String RegimenFiscalClave { get; set; } = String.Empty;
    [MDAMain] public String RegimenFiscalNombre { get; set; } = String.Empty;
    [MDAMain] public Boolean Activo { get; set; } = false;
    #endregion
}
