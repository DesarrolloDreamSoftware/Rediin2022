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
public class RModelos : MRepositorio
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
    public RModelos(IMConexionEntidad conexion)
        : base(conexion)
    {
        _conexion = conexion;
    }
    #endregion

    #region Funciones

    #region Modelo (Modelos)
    /// <summary>
    /// Consulta paginada de la entidad Modelo.
    /// </summary>
    public async Task<EModeloPag> ModeloPag(EModeloFiltro modeloFiltro)
    {
        return await _conexion.EntidadPagAsync<EModelo,
                                               EModeloPag,
                                               EModeloFiltro>(modeloFiltro, "NCModelosCP");
    }
    /// <summary>
    /// Consulta por id de la entidad Modelo.
    /// </summary>
    public async Task<EModelo> ModeloXId(Int64 modeloId)
    {
        _conexion.AddParamIn(modeloId);
        return await _conexion.LoadEntityAsync<EModelo>("NCModelosCI");
    }
    /// <summary>
    /// Consulta para combos de la entidad Modelo.
    /// </summary>
    public async Task<List<MEElemento>> ModeloCmb()
    {
        return await _conexion.EntidadCmbAsync("NCModelosCCmb");
    }
    /// <summary>
    /// Permite insertar la entidad Modelo.
    /// </summary>
    protected async Task<Boolean> ModeloInserta(EModelo modelo)
    {
        return await _conexion.EntityUpdateAsync(modelo, MAccionesBd.Inserta, "NCModelosIAE");
    }
    /// <summary>
    /// Permite actualizar la entidad Modelo.
    /// </summary>
    protected async Task<Boolean> ModeloActualiza(EModelo modelo)
    {
        return await _conexion.EntityUpdateAsync(modelo, MAccionesBd.Actualiza, "NCModelosIAE");
    }
    /// <summary>
    /// Permite eliminar la entidad Modelo.
    /// </summary>
    protected async Task<Boolean> ModeloElimina(EModelo modelo)
    {
        return await _conexion.EntityUpdateAsync(modelo, MAccionesBd.Elimina, "NCModelosIAE");
    }
    #endregion

    #endregion
}
