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
public class RegimenesFiscalesController : MControllerMvcPri
{
    #region Constructores
    /// <summary>
    /// Controlador MVC.
    /// </summary>
    public RegimenesFiscalesController(INRegimenesFiscales nRegimenesFiscales)
    {
        NRegimenesFiscales = nRegimenesFiscales;
    }
    #endregion

    #region Propiedades
    /// <summary>
    /// Negocio NRegimenesFiscales.
    /// </summary>
    private INRegimenesFiscales NRegimenesFiscales { get; set; }
    /// <summary>
    /// Entidad de variables.
    /// </summary>
    private EVRegimenesFiscales EV
    {
        get { return base.MEVCtrl<EVRegimenesFiscales>(); }
    }
    #endregion

    #region RegimenFiscal (RegimenesFiscales)

    #region Acciones
    /// <summary>
    /// Inicia sub funcion.
    /// </summary>
    public async Task<IActionResult> RegimenFiscalInicia()
    {
        //Configuracion de inicio
        await Servicios.Gen.InicializaSF(EV.RegimenFiscal, nameof(ERegimenFiscal.RegimenFiscaId),
            async () => await NRegimenesFiscales.RegimenFiscalReglas());

        return RedirectToAction(nameof(RegimenFiscalCon));
    }
    /// <summary>
    /// Consulta.
    /// </summary>
    [MValidaSeg(nameof(RegimenFiscalInicia))]
    public async Task<IActionResult> RegimenFiscalCon()
    {

        await Servicios.Pag.CargaPagOrdYFil(EV.RegimenFiscal);
        EV.RegimenFiscal.Pag = await NRegimenesFiscales.RegimenFiscalPag(EV.RegimenFiscal.Filtro);
        await Servicios.Pag.ActTamPag(EV.RegimenFiscal);

        ViewBag.Mensajes = NRegimenesFiscales.Mensajes;
        ViewBag.EV = EV;

        return View(nameof(RegimenFiscalCon), EV.RegimenFiscal.Pag?.Pagina);
    }
    /// <summary>
    /// Consulta por id.
    /// </summary>
    public async Task<IActionResult> RegimenFiscalXId(Int32 indice)
    {
        EV.Accion = MAccionesGen.Consulta;
        EV.RegimenFiscal.Indice = indice;
        return await RegimenFiscalCaptura(EV.RegimenFiscal.Pag.Pagina[indice]);
    }
    /// <summary>
    /// Inserta.
    /// </summary>
    [MValidaSeg(nameof(RegimenFiscalInserta))]
    public async Task<IActionResult> RegimenFiscalInsertaIni()
    {
        EV.Accion = MAccionesGen.Inserta;
        return await RegimenFiscalInsertaCap(new ERegimenFiscal()
        {
            Activo = true
        });
    }
    /// <summary>
    /// Inserta.
    /// </summary>
    [ValidateAntiForgeryToken]
    [MValidaSeg(nameof(RegimenFiscalInserta))]
    public async Task<IActionResult> RegimenFiscalInsertaCap(ERegimenFiscal regimenFiscal)
    {
        return await RegimenFiscalCaptura(regimenFiscal);
    }
    /// <summary>
    /// Inserta.
    /// </summary>
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> RegimenFiscalInserta(ERegimenFiscal regimenFiscal)
    {
        if (await NRegimenesFiscales.RegimenFiscalInserta(regimenFiscal))
            return RedirectToAction(nameof(RegimenFiscalCon));

        return await RegimenFiscalInsertaCap(regimenFiscal);
    }
    /// <summary>
    /// Actualiza.
    /// </summary>
    [MValidaSeg(nameof(RegimenFiscalActualiza))]
    public async Task<IActionResult> RegimenFiscalActualizaIni(Int32 indice)
    {
        EV.Accion = MAccionesGen.Actualiza;
        EV.RegimenFiscal.Indice = indice;
        EV.RegimenFiscal.Sel = EV.RegimenFiscal.Pag.Pagina[indice];
        return await RegimenFiscalActualizaCap(EV.RegimenFiscal.Sel);
    }
    /// <summary>
    /// Actualiza.
    /// </summary>
    [ValidateAntiForgeryToken]
    [MValidaSeg(nameof(RegimenFiscalActualiza))]
    public async Task<IActionResult> RegimenFiscalActualizaCap(ERegimenFiscal regimenFiscal)
    {
        return await RegimenFiscalCaptura(regimenFiscal);
    }
    /// <summary>
    /// Actualiza.
    /// </summary>
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> RegimenFiscalActualiza(ERegimenFiscal regimenFiscal)
    {
        if (await NRegimenesFiscales.RegimenFiscalActualiza(regimenFiscal))
            return RedirectToAction(nameof(RegimenFiscalCon));

        return await RegimenFiscalActualizaCap(regimenFiscal);
    }
    /// <summary>
    /// Elimina.
    /// </summary>
    public async Task<IActionResult> RegimenFiscalElimina(Int32 indice)
    {
        await NRegimenesFiscales.RegimenFiscalElimina(EV.RegimenFiscal.Pag.Pagina[indice]);

        return RedirectToAction(nameof(RegimenFiscalCon));
    }
    #endregion

    #region Funciones
    /// <summary>
    /// Captura.
    /// </summary>
    private async Task<IActionResult> RegimenFiscalCaptura(ERegimenFiscal regimenFiscal)
    {
        ViewBag.Mensajes = NRegimenesFiscales.Mensajes;
        ViewBag.EV = EV;

        return ViewCap(nameof(RegimenFiscalCaptura), regimenFiscal);
    }
    #endregion

    #region Acciones de Paginacion Orden y Filtro
    /// <summary>
    /// Control de paginacion.
    /// </summary>
    [MValidaSeg(nameof(RegimenFiscalInicia))]
    public IActionResult RegimenFiscalPaginacion(MEDatosPaginador datPag)
    {
        EV.RegimenFiscal.Pag.DatPag = datPag;
        return RedirectToAction(nameof(RegimenFiscalCon));
    }
    /// <summary>
    /// Control de orden.
    /// </summary>
    [MValidaSeg(nameof(RegimenFiscalInicia))]
    public IActionResult RegimenFiscalOrdena(String orden)
    {
        EV.RegimenFiscal.ColOrden = orden;
        return RedirectToAction(nameof(RegimenFiscalCon));
    }
    /// <summary>
    /// Control de filtro.
    /// </summary>
    [MValidaSeg(nameof(RegimenFiscalInicia))]
    public IActionResult RegimenFiscalFiltra(ERegimenFiscalFiltro filtro)
    {
        EV.RegimenFiscal.Filtro = filtro;
        return RedirectToAction(nameof(RegimenFiscalCon));
    }
    /// <summary>
    /// Limpia filtros.
    /// </summary>
    [MValidaSeg(nameof(RegimenFiscalInicia))]
    public IActionResult RegimenFiscalLimpiaFiltros()
    {
        EV.RegimenFiscal.Filtro = new();
        return RedirectToAction(nameof(RegimenFiscalCon));
    }
    #endregion

    #endregion
}
