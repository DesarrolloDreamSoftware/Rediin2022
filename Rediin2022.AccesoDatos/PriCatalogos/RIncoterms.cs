using DSEntityNetX.Common.Casting;
using DSMetodNetX.AccesoDatos;
using DSMetodNetX.Comun;
using DSMetodNetX.Entidades;
using DSMetodNetX.Entidades.Idioma;
using Rediin2022.Entidades.Idioma;
using Rediin2022.Entidades.PriCatalogos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rediin2022.AccesoDatos.PriCatalogos;

/// <summary>
/// Repositorio.
/// </summary>
[Serializable]
public class RIncoterms : MRepositorio
{
    #region Variables
    /// <summary>
    /// Conexión.
    /// </summary>
    private IMConexionEntidad _conexion;
    #endregion

    #region Constructores
    /// <summary>
    /// Repositorio.
    /// </summary>
    public RIncoterms(IMConexionEntidad conexion)
        : base(conexion)
    {
        _conexion = conexion;
    }
    #endregion

    #region Funciones

    #region Incoterm (Incoterms)
    /// <summary>
    /// Consulta paginada de la entidad Incoterm.
    /// </summary>
    public async Task<EIncotermPag> IncotermPag(EIncotermFiltro incotermFiltro)
    {
        return await _conexion.EntidadPagAsync<EIncoterm,
                                               EIncotermPag,
                                               EIncotermFiltro>(incotermFiltro, "NCIncotermsCP");
    }
    /// <summary>
    /// Consulta por id de la entidad Incoterm.
    /// </summary>
    public async Task<EIncoterm> IncotermXId(Int64 incotermId)
    {
        _conexion.AddParamIn(incotermId);
        return await _conexion.LoadEntityAsync<EIncoterm>("NCIncotermsCI");
    }
    /// <summary>
    /// Consulta para combos de la entidad Incoterm.
    /// </summary>
    public async Task<List<MEElemento>> IncotermCmb()
    {
        return await _conexion.EntidadCmbAsync("NCIncotermsCCmb");
    }
    /// <summary>
    /// Permite insertar la entidad Incoterm.
    /// </summary>
    protected async Task<Int64> IncotermInserta(EIncoterm incoterm)
    {
        await _conexion.EntityUpdateAsync(incoterm, MAccionesBd.Inserta, "NCIncotermsIAE");
        return incoterm.IncotermId;
    }
    /// <summary>
    /// Permite actualizar la entidad Incoterm.
    /// </summary>
    protected async Task<Boolean> IncotermActualiza(EIncoterm incoterm)
    {
        return await _conexion.EntityUpdateAsync(incoterm, MAccionesBd.Actualiza, "NCIncotermsIAE");
    }
    /// <summary>
    /// Permite eliminar la entidad Incoterm.
    /// </summary>
    protected async Task<Boolean> IncotermElimina(EIncoterm incoterm)
    {
        return await _conexion.EntityUpdateAsync(incoterm, MAccionesBd.Elimina, "NCIncotermsIAE");
    }
    #endregion

    #endregion
}
