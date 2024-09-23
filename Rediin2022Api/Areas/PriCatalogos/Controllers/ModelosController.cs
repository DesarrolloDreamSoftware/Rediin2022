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
public class ModelosController : MControllerApiPri, INModelos
{
    #region Contructores
    /// <summary>
    /// API que expone el negocio.
    /// </summary>
    public ModelosController(INModelos nModelos)
    {
        NModelos = nModelos;
    }
    #endregion

    #region Propiedades
    /// <summary>
    /// Negocio.
    /// </summary>
    public INModelos NModelos { get; }
    /// <summary>
    /// Control de mensajes.
    /// </summary>
    public IMMensajes Mensajes
    {
        get { return NModelos.Mensajes; }
    }
    #endregion

    #region Funciones

    #region Modelo (Modelos)
    /// <summary>
    /// Consulta paginada de la entidad Modelo.
    /// </summary>
    [HttpPost]
    public async Task<EModeloPag> ModeloPag(EModeloFiltro modeloFiltro)
    {
        return await NModelos.ModeloPag(modeloFiltro);
    }
    /// <summary>
    /// Consulta por id de la entidad Modelo.
    /// </summary>
    [HttpGet("{modeloId}")]
    public async Task<EModelo> ModeloXId(Int64 modeloId)
    {
        return await NModelos.ModeloXId(modeloId);
    }
    /// <summary>
    /// Consulta para combos de la entidad Modelo.
    /// </summary>
    public async Task<List<MEElemento>> ModeloCmb()
    {
        return await NModelos.ModeloCmb();
    }
    /// <summary>
    /// Permite insertar la entidad Modelo.
    /// </summary>
    [HttpPost]
    public async Task<Boolean> ModeloInserta(EModelo modelo)
    {
        return await NModelos.ModeloInserta(modelo);
    }
    /// <summary>
    /// Permite actualizar la entidad Modelo.
    /// </summary>
    [HttpPost]
    public async Task<Boolean> ModeloActualiza(EModelo modelo)
    {
        return await NModelos.ModeloActualiza(modelo);
    }
    /// <summary>
    /// Permite eliminar la entidad Modelo.
    /// </summary>
    [HttpPost]
    public async Task<Boolean> ModeloElimina(EModelo modelo)
    {
        return await NModelos.ModeloElimina(modelo);
    }
    /// <summary>
    /// Reglas de negocio de la entidad Modelo.
    /// </summary>
    [HttpGet]
    public async Task<List<MEReglaNeg>> ModeloReglas()
    {
        return await NModelos.ModeloReglas();
    }
    #endregion

    #endregion
}
