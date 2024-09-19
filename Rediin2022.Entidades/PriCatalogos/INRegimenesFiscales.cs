using DSMetodNetX.Entidades;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rediin2022.Entidades.PriCatalogos;

/// <summary>
/// Interfaz de negocio.
/// </summary>
public interface INRegimenesFiscales : IMCtrMensajes
{
    #region Funciones

    #region RegimenFiscal (RegimenesFiscales)
    /// <summary>
    /// Consulta paginada de la entidad RegimenFiscal.
    /// </summary>
    Task<ERegimenFiscalPag> RegimenFiscalPag(ERegimenFiscalFiltro regimenFiscalFiltro);
    /// <summary>
    /// Consulta por id de la entidad RegimenFiscal.
    /// </summary>
    Task<ERegimenFiscal> RegimenFiscalXId(Int64 regimenFiscaId);
    /// <summary>
    /// Consulta para combos de la entidad RegimenFiscal.
    /// </summary>
    Task<List<MEElemento>> RegimenFiscalCmb();
    /// <summary>
    /// Permite insertar la entidad RegimenFiscal.
    /// </summary>
    Task<Boolean> RegimenFiscalInserta(ERegimenFiscal regimenFiscal);
    /// <summary>
    /// Permite actualizar la entidad RegimenFiscal.
    /// </summary>
    Task<Boolean> RegimenFiscalActualiza(ERegimenFiscal regimenFiscal);
    /// <summary>
    /// Permite eliminar la entidad RegimenFiscal.
    /// </summary>
    Task<Boolean> RegimenFiscalElimina(ERegimenFiscal regimenFiscal);
    /// <summary>
    /// Reglas de negocio de la entidad RegimenFiscal.
    /// </summary>
    Task<List<MEReglaNeg>> RegimenFiscalReglas();
    #endregion

    #endregion
}
