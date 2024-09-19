using DSMetodNetX.Api.Seguridad;
using DSMetodNetX.Entidades;
using Microsoft.AspNetCore.Mvc;
using Rediin2022.Entidades.PriCatalogos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rediin2022Api.Areas.PriCatalogos.Controllers;

/// <summary>
/// API que expone el negocio.
/// </summary>
[Route("ApiV1/PriCatalogos/[controller]/[action]")]
public class RegimenesFiscalesController : MControllerApiPri, INRegimenesFiscales
{
    #region Contructores
    /// <summary>
    /// API que expone el negocio.
    /// </summary>
    public RegimenesFiscalesController(INRegimenesFiscales nRegimenesFiscales)
    {
        NRegimenesFiscales = nRegimenesFiscales;
    }
    #endregion

    #region Propiedades
    /// <summary>
    /// Negocio.
    /// </summary>
    public INRegimenesFiscales NRegimenesFiscales { get; }
    /// <summary>
    /// Control de mensajes.
    /// </summary>
    public IMMensajes Mensajes
    {
        get { return NRegimenesFiscales.Mensajes; }
    }
    #endregion

    #region Funciones

    #region RegimenFiscal (RegimenesFiscales)
    /// <summary>
    /// Consulta paginada de la entidad RegimenFiscal.
    /// </summary>
    [HttpPost]
    public async Task<ERegimenFiscalPag> RegimenFiscalPag(ERegimenFiscalFiltro regimenFiscalFiltro)
    {
        return await NRegimenesFiscales.RegimenFiscalPag(regimenFiscalFiltro);
    }
    /// <summary>
    /// Consulta por id de la entidad RegimenFiscal.
    /// </summary>
    [HttpGet("{regimenFiscaId}")]
    public async Task<ERegimenFiscal> RegimenFiscalXId(Int64 regimenFiscaId)
    {
        return await NRegimenesFiscales.RegimenFiscalXId(regimenFiscaId);
    }
    /// <summary>
    /// Consulta para combos de la entidad RegimenFiscal.
    /// </summary>
    public async Task<List<MEElemento>> RegimenFiscalCmb()
    {
        return await NRegimenesFiscales.RegimenFiscalCmb();
    }
    /// <summary>
    /// Permite insertar la entidad RegimenFiscal.
    /// </summary>
    [HttpPost]
    public async Task<Boolean> RegimenFiscalInserta(ERegimenFiscal regimenFiscal)
    {
        return await NRegimenesFiscales.RegimenFiscalInserta(regimenFiscal);
    }
    /// <summary>
    /// Permite actualizar la entidad RegimenFiscal.
    /// </summary>
    [HttpPost]
    public async Task<Boolean> RegimenFiscalActualiza(ERegimenFiscal regimenFiscal)
    {
        return await NRegimenesFiscales.RegimenFiscalActualiza(regimenFiscal);
    }
    /// <summary>
    /// Permite eliminar la entidad RegimenFiscal.
    /// </summary>
    [HttpPost]
    public async Task<Boolean> RegimenFiscalElimina(ERegimenFiscal regimenFiscal)
    {
        return await NRegimenesFiscales.RegimenFiscalElimina(regimenFiscal);
    }
    /// <summary>
    /// Reglas de negocio de la entidad RegimenFiscal.
    /// </summary>
    [HttpGet]
    public async Task<List<MEReglaNeg>> RegimenFiscalReglas()
    {
        return await NRegimenesFiscales.RegimenFiscalReglas();
    }
    #endregion

    #endregion
}
