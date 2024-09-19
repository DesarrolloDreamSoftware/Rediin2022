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
public class IncotermsController : MControllerMvcPri
{
    #region Constructores
    /// <summary>
    /// Controlador MVC.
    /// </summary>
    public IncotermsController(INIncoterms nIncoterms)
    {
        NIncoterms = nIncoterms;
    }
    #endregion

    #region Propiedades
    /// <summary>
    /// Negocio NIncoterms.
    /// </summary>
    private INIncoterms NIncoterms { get; set; }
    /// <summary>
    /// Entidad de variables.
    /// </summary>
    private EVIncoterms EV
    {
        get { return base.MEVCtrl<EVIncoterms>(); }
    }
    #endregion

    #region Incoterm (Incoterms)

    #region Acciones
    /// <summary>
    /// Inicia sub funcion.
    /// </summary>
    public async Task<IActionResult> IncotermInicia()
    {
        //Configuracion de inicio
        await Servicios.Gen.InicializaSF(EV.Incoterm, nameof(EIncoterm.IncotermId),
            async () => await NIncoterms.IncotermReglas());

        return RedirectToAction(nameof(IncotermCon));
    }
    /// <summary>
    /// Consulta.
    /// </summary>
    [MValidaSeg(nameof(IncotermInicia))]
    public async Task<IActionResult> IncotermCon()
    {

        await Servicios.Pag.CargaPagOrdYFil(EV.Incoterm);
        EV.Incoterm.Pag = await NIncoterms.IncotermPag(EV.Incoterm.Filtro);
        await Servicios.Pag.ActTamPag(EV.Incoterm);

        ViewBag.Mensajes = NIncoterms.Mensajes;
        ViewBag.EV = EV;

        return View(nameof(IncotermCon), EV.Incoterm.Pag?.Pagina);
    }
    /// <summary>
    /// Consulta por id.
    /// </summary>
    public async Task<IActionResult> IncotermXId(Int32 indice)
    {
        EV.Accion = MAccionesGen.Consulta;
        EV.Incoterm.Indice = indice;
        return await IncotermCaptura(EV.Incoterm.Pag.Pagina[indice]);
    }
    /// <summary>
    /// Inserta.
    /// </summary>
    [MValidaSeg(nameof(IncotermInserta))]
    public async Task<IActionResult> IncotermInsertaIni()
    {
        EV.Accion = MAccionesGen.Inserta;
        return await IncotermInsertaCap(new EIncoterm()
        {
            Activo = true
        });
    }
    /// <summary>
    /// Inserta.
    /// </summary>
    [ValidateAntiForgeryToken]
    [MValidaSeg(nameof(IncotermInserta))]
    public async Task<IActionResult> IncotermInsertaCap(EIncoterm incoterm)
    {
        return await IncotermCaptura(incoterm);
    }
    /// <summary>
    /// Inserta.
    /// </summary>
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> IncotermInserta(EIncoterm incoterm)
    {
        await NIncoterms.IncotermInserta(incoterm);
        if (NIncoterms.Mensajes.Ok)
            return RedirectToAction(nameof(IncotermCon));

        return await IncotermInsertaCap(incoterm);
    }
    /// <summary>
    /// Actualiza.
    /// </summary>
    [MValidaSeg(nameof(IncotermActualiza))]
    public async Task<IActionResult> IncotermActualizaIni(Int32 indice)
    {
        EV.Accion = MAccionesGen.Actualiza;
        EV.Incoterm.Indice = indice;
        EV.Incoterm.Sel = EV.Incoterm.Pag.Pagina[indice];
        return await IncotermActualizaCap(EV.Incoterm.Sel);
    }
    /// <summary>
    /// Actualiza.
    /// </summary>
    [ValidateAntiForgeryToken]
    [MValidaSeg(nameof(IncotermActualiza))]
    public async Task<IActionResult> IncotermActualizaCap(EIncoterm incoterm)
    {
        return await IncotermCaptura(incoterm);
    }
    /// <summary>
    /// Actualiza.
    /// </summary>
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> IncotermActualiza(EIncoterm incoterm)
    {
        if (await NIncoterms.IncotermActualiza(incoterm))
            return RedirectToAction(nameof(IncotermCon));

        return await IncotermActualizaCap(incoterm);
    }
    /// <summary>
    /// Elimina.
    /// </summary>
    public async Task<IActionResult> IncotermElimina(Int32 indice)
    {
        await NIncoterms.IncotermElimina(EV.Incoterm.Pag.Pagina[indice]);

        return RedirectToAction(nameof(IncotermCon));
    }
    #endregion

    #region Funciones
    /// <summary>
    /// Captura.
    /// </summary>
    private async Task<IActionResult> IncotermCaptura(EIncoterm incoterm)
    {
        ViewBag.Mensajes = NIncoterms.Mensajes;
        ViewBag.EV = EV;

        return ViewCap(nameof(IncotermCaptura), incoterm);
    }
    #endregion

    #region Acciones de Paginacion Orden y Filtro
    /// <summary>
    /// Control de paginacion.
    /// </summary>
    [MValidaSeg(nameof(IncotermInicia))]
    public IActionResult IncotermPaginacion(MEDatosPaginador datPag)
    {
        EV.Incoterm.Pag.DatPag = datPag;
        return RedirectToAction(nameof(IncotermCon));
    }
    /// <summary>
    /// Control de orden.
    /// </summary>
    [MValidaSeg(nameof(IncotermInicia))]
    public IActionResult IncotermOrdena(String orden)
    {
        EV.Incoterm.ColOrden = orden;
        return RedirectToAction(nameof(IncotermCon));
    }
    /// <summary>
    /// Control de filtro.
    /// </summary>
    [MValidaSeg(nameof(IncotermInicia))]
    public IActionResult IncotermFiltra(EIncotermFiltro filtro)
    {
        EV.Incoterm.Filtro = filtro;
        return RedirectToAction(nameof(IncotermCon));
    }
    /// <summary>
    /// Limpia filtros.
    /// </summary>
    [MValidaSeg(nameof(IncotermInicia))]
    public IActionResult IncotermLimpiaFiltros()
    {
        EV.Incoterm.Filtro = new();
        return RedirectToAction(nameof(IncotermCon));
    }
    #endregion

    #endregion
}
