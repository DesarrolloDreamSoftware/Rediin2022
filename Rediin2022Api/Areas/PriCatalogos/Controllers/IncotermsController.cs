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
public class IncotermsController : MControllerApiPri, INIncoterms
{
    #region Contructores
    /// <summary>
    /// API que expone el negocio.
    /// </summary>
    public IncotermsController(INIncoterms nIncoterms)
    {
        NIncoterms = nIncoterms;
    }
    #endregion

    #region Propiedades
    /// <summary>
    /// Negocio.
    /// </summary>
    public INIncoterms NIncoterms { get; }
    /// <summary>
    /// Control de mensajes.
    /// </summary>
    public IMMensajes Mensajes
    {
        get { return NIncoterms.Mensajes; }
    }
    #endregion

    #region Funciones

    #region Incoterm (Incoterms)
    /// <summary>
    /// Consulta paginada de la entidad Incoterm.
    /// </summary>
    [HttpPost]
    public async Task<EIncotermPag> IncotermPag(EIncotermFiltro incotermFiltro)
    {
        return await NIncoterms.IncotermPag(incotermFiltro);
    }
    /// <summary>
    /// Consulta por id de la entidad Incoterm.
    /// </summary>
    [HttpGet("{incotermId}")]
    public async Task<EIncoterm> IncotermXId(Int64 incotermId)
    {
        return await NIncoterms.IncotermXId(incotermId);
    }
    /// <summary>
    /// Consulta para combos de la entidad Incoterm.
    /// </summary>
    public async Task<List<MEElemento>> IncotermCmb()
    {
        return await NIncoterms.IncotermCmb();
    }
    /// <summary>
    /// Permite insertar la entidad Incoterm.
    /// </summary>
    [HttpPost]
    public async Task<Int64> IncotermInserta(EIncoterm incoterm)
    {
        return await NIncoterms.IncotermInserta(incoterm);
    }
    /// <summary>
    /// Permite actualizar la entidad Incoterm.
    /// </summary>
    [HttpPost]
    public async Task<Boolean> IncotermActualiza(EIncoterm incoterm)
    {
        return await NIncoterms.IncotermActualiza(incoterm);
    }
    /// <summary>
    /// Permite eliminar la entidad Incoterm.
    /// </summary>
    [HttpPost]
    public async Task<Boolean> IncotermElimina(EIncoterm incoterm)
    {
        return await NIncoterms.IncotermElimina(incoterm);
    }
    /// <summary>
    /// Reglas de negocio de la entidad Incoterm.
    /// </summary>
    [HttpGet]
    public async Task<List<MEReglaNeg>> IncotermReglas()
    {
        return await NIncoterms.IncotermReglas();
    }
    #endregion

    #endregion
}
