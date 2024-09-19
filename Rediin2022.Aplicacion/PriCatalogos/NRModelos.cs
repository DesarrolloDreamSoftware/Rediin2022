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
public class NRModelos : MNegRemoto, INModelos
{
    #region Constructores
    /// <summary>
    /// Negocio cliente para conexion con un API.
    /// </summary>
    public NRModelos(IMApiCteNeg api) : base(api)
    {
    }
    #endregion

    #region Funciones

    #region Modelo (Modelos)
    /// <summary>
    /// Consulta paginada de la entidad Modelo.
    /// </summary>
    public async Task<EModeloPag> ModeloPag(EModeloFiltro modeloFiltro)
    {
        return await CallAsync<EModeloPag>(NomFn(), modeloFiltro);
    }
    /// <summary>
    /// Consulta por id de la entidad Modelo.
    /// </summary>
    public async Task<EModelo> ModeloXId(Int64 modeloId)
    {
        return await CallAsync<EModelo>(NomFn(),
                                        modeloId);
    }
    /// <summary>
    /// Consulta para combos de la entidad Modelo.
    /// </summary>
    public async Task<List<MEElemento>> ModeloCmb()
    {
        return await CallAsync<List<MEElemento>>(NomFn());
    }
    /// <summary>
    /// Permite insertar la entidad Modelo.
    /// </summary>
    public async Task<Boolean> ModeloInserta(EModelo modelo)
    {
        return await CallAsync<Boolean>(NomFn(), modelo);
    }
    /// <summary>
    /// Permite actualizar la entidad Modelo.
    /// </summary>
    public async Task<Boolean> ModeloActualiza(EModelo modelo)
    {
        return await CallAsync<Boolean>(NomFn(), modelo);
    }
    /// <summary>
    /// Permite eliminar la entidad Modelo.
    /// </summary>
    public async Task<Boolean> ModeloElimina(EModelo modelo)
    {
        return await CallAsync<Boolean>(NomFn(), modelo);
    }
    /// <summary>
    /// Reglas de negocio de la entidad Modelo.
    /// </summary>
    public async Task<List<MEReglaNeg>> ModeloReglas()
    {
        return await CallAsync<List<MEReglaNeg>>(NomFn());
    }
    #endregion

    #endregion
}
