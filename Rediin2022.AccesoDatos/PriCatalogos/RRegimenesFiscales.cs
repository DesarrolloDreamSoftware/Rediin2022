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
public class RRegimenesFiscales : MRepositorio
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
    public RRegimenesFiscales(IMConexionEntidad conexion)
        : base(conexion)
    {
        _conexion = conexion;
    }
    #endregion

    #region Funciones

    #region RegimenFiscal (RegimenesFiscales)
    /// <summary>
    /// Consulta paginada de la entidad RegimenFiscal.
    /// </summary>
    public async Task<ERegimenFiscalPag> RegimenFiscalPag(ERegimenFiscalFiltro regimenFiscalFiltro)
    {
        return await _conexion.EntidadPagAsync<ERegimenFiscal,
                                               ERegimenFiscalPag,
                                               ERegimenFiscalFiltro>(regimenFiscalFiltro, "NCRegimenesFiscalesCP");
    }
    /// <summary>
    /// Consulta por id de la entidad RegimenFiscal.
    /// </summary>
    public async Task<ERegimenFiscal> RegimenFiscalXId(Int64 regimenFiscaId)
    {
        _conexion.AddParamIn(regimenFiscaId);
        return await _conexion.LoadEntityAsync<ERegimenFiscal>("NCRegimenesFiscalesCI");
    }
    /// <summary>
    /// Consulta para combos de la entidad RegimenFiscal.
    /// </summary>
    public async Task<List<MEElemento>> RegimenFiscalCmb()
    {
        return await _conexion.EntidadCmbAsync("NCRegimenesFiscalesCCmb");
    }
    /// <summary>
    /// Permite insertar la entidad RegimenFiscal.
    /// </summary>
    protected async Task<Boolean> RegimenFiscalInserta(ERegimenFiscal regimenFiscal)
    {
        return await _conexion.EntityUpdateAsync(regimenFiscal, MAccionesBd.Inserta, "NCRegimenesFiscalesIAE");
    }
    /// <summary>
    /// Permite actualizar la entidad RegimenFiscal.
    /// </summary>
    protected async Task<Boolean> RegimenFiscalActualiza(ERegimenFiscal regimenFiscal)
    {
        return await _conexion.EntityUpdateAsync(regimenFiscal, MAccionesBd.Actualiza, "NCRegimenesFiscalesIAE");
    }
    /// <summary>
    /// Permite eliminar la entidad RegimenFiscal.
    /// </summary>
    protected async Task<Boolean> RegimenFiscalElimina(ERegimenFiscal regimenFiscal)
    {
        return await _conexion.EntityUpdateAsync(regimenFiscal, MAccionesBd.Elimina, "NCRegimenesFiscalesIAE");
    }
    #endregion

    #endregion
}
