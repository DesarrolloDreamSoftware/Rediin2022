using DSMetodNetX.Aplicacion;
using DSMetodNetX.Comun;
using DSMetodNetX.Entidades;
using Rediin2022.Entidades.PriCatalogos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rediin2022.Aplicacion.PriCatalogos;

/// <summary>
/// Negocio cliente para conexion con un API.
/// </summary>
public class NRRegimenesFiscales : MNegRemoto, INRegimenesFiscales
{
    #region Constructores
    /// <summary>
    /// Negocio cliente para conexion con un API.
    /// </summary>
    public NRRegimenesFiscales(IMApiCteNeg api) : base(api)
    {
    }
    #endregion

    #region Funciones

    #region RegimenFiscal (RegimenesFiscales)
    /// <summary>
    /// Consulta paginada de la entidad RegimenFiscal.
    /// </summary>
    public async Task<ERegimenFiscalPag> RegimenFiscalPag(ERegimenFiscalFiltro regimenFiscalFiltro)
    {
        return await CallAsync<ERegimenFiscalPag>(NomFn(), regimenFiscalFiltro);
    }
    /// <summary>
    /// Consulta por id de la entidad RegimenFiscal.
    /// </summary>
    public async Task<ERegimenFiscal> RegimenFiscalXId(Int64 regimenFiscaId)
    {
        return await CallAsync<ERegimenFiscal>(NomFn(),
                                               regimenFiscaId);
    }
    /// <summary>
    /// Consulta para combos de la entidad RegimenFiscal.
    /// </summary>
    public async Task<List<MEElemento>> RegimenFiscalCmb()
    {
        return await CallAsync<List<MEElemento>>(NomFn());
    }
    /// <summary>
    /// Permite insertar la entidad RegimenFiscal.
    /// </summary>
    public async Task<Boolean> RegimenFiscalInserta(ERegimenFiscal regimenFiscal)
    {
        return await CallAsync<Boolean>(NomFn(), regimenFiscal);
    }
    /// <summary>
    /// Permite actualizar la entidad RegimenFiscal.
    /// </summary>
    public async Task<Boolean> RegimenFiscalActualiza(ERegimenFiscal regimenFiscal)
    {
        return await CallAsync<Boolean>(NomFn(), regimenFiscal);
    }
    /// <summary>
    /// Permite eliminar la entidad RegimenFiscal.
    /// </summary>
    public async Task<Boolean> RegimenFiscalElimina(ERegimenFiscal regimenFiscal)
    {
        return await CallAsync<Boolean>(NomFn(), regimenFiscal);
    }
    /// <summary>
    /// Reglas de negocio de la entidad RegimenFiscal.
    /// </summary>
    public async Task<List<MEReglaNeg>> RegimenFiscalReglas()
    {
        return await CallAsync<List<MEReglaNeg>>(NomFn());
    }
    #endregion

    #endregion
}
