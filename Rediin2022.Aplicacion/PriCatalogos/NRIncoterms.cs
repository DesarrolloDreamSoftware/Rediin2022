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
public class NRIncoterms : MNegRemoto, INIncoterms
{
    #region Constructores
    /// <summary>
    /// Negocio cliente para conexion con un API.
    /// </summary>
    public NRIncoterms(IMApiCteNeg api) : base(api)
    {
    }
    #endregion

    #region Funciones

    #region Incoterm (Incoterms)
    /// <summary>
    /// Consulta paginada de la entidad Incoterm.
    /// </summary>
    public async Task<EIncotermPag> IncotermPag(EIncotermFiltro incotermFiltro)
    {
        return await CallAsync<EIncotermPag>(NomFn(), incotermFiltro);
    }
    /// <summary>
    /// Consulta por id de la entidad Incoterm.
    /// </summary>
    public async Task<EIncoterm> IncotermXId(Int64 incotermId)
    {
        return await CallAsync<EIncoterm>(NomFn(),
                                          incotermId);
    }
    /// <summary>
    /// Consulta para combos de la entidad Incoterm.
    /// </summary>
    public async Task<List<MEElemento>> IncotermCmb()
    {
        return await CallAsync<List<MEElemento>>(NomFn());
    }
    /// <summary>
    /// Permite insertar la entidad Incoterm.
    /// </summary>
    public async Task<Int64> IncotermInserta(EIncoterm incoterm)
    {
        return await CallAsync<Int64>(NomFn(), incoterm);
    }
    /// <summary>
    /// Permite actualizar la entidad Incoterm.
    /// </summary>
    public async Task<Boolean> IncotermActualiza(EIncoterm incoterm)
    {
        return await CallAsync<Boolean>(NomFn(), incoterm);
    }
    /// <summary>
    /// Permite eliminar la entidad Incoterm.
    /// </summary>
    public async Task<Boolean> IncotermElimina(EIncoterm incoterm)
    {
        return await CallAsync<Boolean>(NomFn(), incoterm);
    }
    /// <summary>
    /// Reglas de negocio de la entidad Incoterm.
    /// </summary>
    public async Task<List<MEReglaNeg>> IncotermReglas()
    {
        return await CallAsync<List<MEReglaNeg>>(NomFn());
    }
    #endregion

    #endregion
}
