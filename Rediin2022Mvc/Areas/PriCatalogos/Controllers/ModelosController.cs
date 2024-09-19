using DSEntityNetX.Common.Casting;
using DSMetodNetX.Entidades;
using DSMetodNetX.Mvc;
using DSMetodNetX.Mvc;
using DSMetodNetX.Mvc.Seguridad;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rediin2022.Aplicacion.PriCatalogos;
using Rediin2022.Entidades.Idioma;
using Rediin2022.Entidades.PriCatalogos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Rediin2022Mvc.Areas.PriCatalogos.Controllers;

/// <summary>
/// Controlador MVC.
/// </summary>
[Area("PriCatalogos")]
public class ModelosController : MControllerMvcPri
{
    #region Constructores
    /// <summary>
    /// Controlador MVC.
    /// </summary>
    public ModelosController(INModelos nModelos)
    {
        NModelos = nModelos;
    }
    #endregion

    #region Propiedades
    /// <summary>
    /// Negocio NModelos.
    /// </summary>
    private INModelos NModelos { get; set; }
    /// <summary>
    /// Entidad de variables.
    /// </summary>
    private EVModelos EV
    {
        get { return base.MEVCtrl<EVModelos>(); }
    }
    #endregion

    #region Modelo (Modelos)

    #region Acciones
    /// <summary>
    /// Inicia sub funcion.
    /// </summary>
    public async Task<IActionResult> ModeloInicia()
    {
        //Configuracion de inicio
        await Servicios.Gen.InicializaSF(EV.Modelo, nameof(EModelo.ModeloId),
            async () => await NModelos.ModeloReglas());

        return RedirectToAction(nameof(ModeloCon));
    }
    /// <summary>
    /// Consulta.
    /// </summary>
    [MValidaSeg(nameof(ModeloInicia))]
    public async Task<IActionResult> ModeloCon()
    {

        await Servicios.Pag.CargaPagOrdYFil(EV.Modelo);
        EV.Modelo.Pag = await NModelos.ModeloPag(EV.Modelo.Filtro);
        await Servicios.Pag.ActTamPag(EV.Modelo);

        ViewBag.Mensajes = NModelos.Mensajes;
        ViewBag.EV = EV;

        return View(nameof(ModeloCon), EV.Modelo.Pag?.Pagina);
    }
    /// <summary>
    /// Consulta por id.
    /// </summary>
    public async Task<IActionResult> ModeloXId(Int32 indice)
    {
        EV.Accion = MAccionesGen.Consulta;
        EV.Modelo.Indice = indice;
        return await ModeloCaptura(EV.Modelo.Pag.Pagina[indice]);
    }
    /// <summary>
    /// Inserta.
    /// </summary>
    [MValidaSeg(nameof(ModeloInserta))]
    public async Task<IActionResult> ModeloInsertaIni()
    {
        EV.Accion = MAccionesGen.Inserta;
        return await ModeloInsertaCap(new EModelo()
        {
            Activo = true
        });
    }
    /// <summary>
    /// Inserta.
    /// </summary>
    [ValidateAntiForgeryToken]
    [MValidaSeg(nameof(ModeloInserta))]
    public async Task<IActionResult> ModeloInsertaCap(EModelo modelo)
    {
        return await ModeloCaptura(modelo);
    }
    /// <summary>
    /// Inserta.
    /// </summary>
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ModeloInserta(EModelo modelo)
    {
        if (await NModelos.ModeloInserta(modelo))
            return RedirectToAction(nameof(ModeloCon));

        return await ModeloInsertaCap(modelo);
    }
    /// <summary>
    /// Actualiza.
    /// </summary>
    [MValidaSeg(nameof(ModeloActualiza))]
    public async Task<IActionResult> ModeloActualizaIni(Int32 indice)
    {
        EV.Accion = MAccionesGen.Actualiza;
        EV.Modelo.Indice = indice;
        EV.Modelo.Sel = EV.Modelo.Pag.Pagina[indice];
        return await ModeloActualizaCap(EV.Modelo.Sel);
    }
    /// <summary>
    /// Actualiza.
    /// </summary>
    [ValidateAntiForgeryToken]
    [MValidaSeg(nameof(ModeloActualiza))]
    public async Task<IActionResult> ModeloActualizaCap(EModelo modelo)
    {
        return await ModeloCaptura(modelo);
    }
    /// <summary>
    /// Actualiza.
    /// </summary>
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ModeloActualiza(EModelo modelo)
    {
        if (await NModelos.ModeloActualiza(modelo))
            return RedirectToAction(nameof(ModeloCon));

        return await ModeloActualizaCap(modelo);
    }
    /// <summary>
    /// Elimina.
    /// </summary>
    public async Task<IActionResult> ModeloElimina(Int32 indice)
    {
        await NModelos.ModeloElimina(EV.Modelo.Pag.Pagina[indice]);

        return RedirectToAction(nameof(ModeloCon));
    }
    #endregion

    #region Funciones
    /// <summary>
    /// Captura.
    /// </summary>
    private async Task<IActionResult> ModeloCaptura(EModelo modelo)
    {
        ViewBag.Mensajes = NModelos.Mensajes;
        ViewBag.EV = EV;

        return ViewCap(nameof(ModeloCaptura), modelo);
    }
    #endregion

    #region Acciones de Paginacion Orden y Filtro
    /// <summary>
    /// Control de paginacion.
    /// </summary>
    [MValidaSeg(nameof(ModeloInicia))]
    public IActionResult ModeloPaginacion(MEDatosPaginador datPag)
    {
        EV.Modelo.Pag.DatPag = datPag;
        return RedirectToAction(nameof(ModeloCon));
    }
    /// <summary>
    /// Control de orden.
    /// </summary>
    [MValidaSeg(nameof(ModeloInicia))]
    public IActionResult ModeloOrdena(String orden)
    {
        EV.Modelo.ColOrden = orden;
        return RedirectToAction(nameof(ModeloCon));
    }
    /// <summary>
    /// Control de filtro.
    /// </summary>
    [MValidaSeg(nameof(ModeloInicia))]
    public IActionResult ModeloFiltra(EModeloFiltro filtro)
    {
        EV.Modelo.Filtro = filtro;
        return RedirectToAction(nameof(ModeloCon));
    }
    /// <summary>
    /// Limpia filtros.
    /// </summary>
    [MValidaSeg(nameof(ModeloInicia))]
    public IActionResult ModeloLimpiaFiltros()
    {
        EV.Modelo.Filtro = new();
        return RedirectToAction(nameof(ModeloCon));
    }
    #endregion

    #endregion
}
