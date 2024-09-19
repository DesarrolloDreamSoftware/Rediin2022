using DSEntityNetX.Common.Casting;
using DSEntityNetX.Common.File;
using DSEntityNetX.Common.Pagination;
using DSMetodNetX.Aplicacion;
using DSMetodNetX.Entidades;
using DSMetodNetX.Mvc;
using DSMetodNetX.Mvc.Seguridad;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Rediin2022.Aplicacion.PriCatalogos;
using Rediin2022.Entidades.Idioma;
using Rediin2022.Entidades.PriCatalogos;
using Sisegui2020.Entidades.PriSeguridad;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Rediin2022Mvc.Areas.PriCatalogos.Controllers
{
    [Area("PriCatalogos")]
    public class ProcesosOperativosController : MControllerMvcPri
    {
        #region Constructores
        public ProcesosOperativosController(INProcesosOperativos nProcesosOperativos,
                                            INCatalogos nCatalogos)
        {
            NProcesosOperativos = nProcesosOperativos;
            NCatalogos = nCatalogos;
        }
        #endregion

        #region Propiedades
        private INProcesosOperativos NProcesosOperativos { get; set; }
        private INCatalogos NCatalogos { get; set; }
        private EVProcesosOperativos EV
        {
            get { return base.MEVCtrl<EVProcesosOperativos>(); }
        }
        #endregion

        #region ProcesoOperativo (ProcesosOperativos)

        #region Acciones
        public async Task<IActionResult> ProcesoOperativoInicia()
        {
            //Configuracion de inicio
            await Servicios.Gen.InicializaSF(EV.ProcesoOperativo, nameof(EProcesoOperativo.Orden),
                async () => await NProcesosOperativos.ProcesoOperativoReglas());

            return RedirectToAction(nameof(ProcesoOperativoCon));
        }
        [MValidaSeg(nameof(ProcesoOperativoInicia))]
        public async Task<IActionResult> ProcesoOperativoCon()
        {
            await Servicios.Pag.CargaPagOrdYFil(EV.ProcesoOperativo);
            EV.ProcesoOperativo.Pag = await NProcesosOperativos.ProcesoOperativoPag(EV.ProcesoOperativo.Filtro);
            await Servicios.Pag.ActTamPag(EV.ProcesoOperativo);

            ViewBag.Mensajes = NProcesosOperativos.Mensajes;
            ViewBag.EV = EV;

            return View(nameof(ProcesoOperativoCon), EV.ProcesoOperativo.Pag?.Pagina);
        }
        public async Task<IActionResult> ProcesoOperativoXId(Int32 indice)
        {
            EV.Accion = MAccionesGen.Consulta;
            EV.ProcesoOperativo.Indice = indice;
            return await ProcesoOperativoCaptura(EV.ProcesoOperativo.Pag.Pagina[indice]);
        }
        [MValidaSeg(nameof(ProcesoOperativoInserta))]
        public async Task<IActionResult> ProcesoOperativoInsertaIni()
        {
            EV.Accion = MAccionesGen.Inserta;
            return await ProcesoOperativoInsertaCap(new EProcesoOperativo()
            {
                Activo = true
            });
        }
        [ValidateAntiForgeryToken]
        [MValidaSeg(nameof(ProcesoOperativoInserta))]
        public async Task<IActionResult> ProcesoOperativoInsertaCap(EProcesoOperativo procesoOperativo)
        {
            return await ProcesoOperativoCaptura(procesoOperativo);
        }
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProcesoOperativoInserta(EProcesoOperativo procesoOperativo)
        {
            await NProcesosOperativos.ProcesoOperativoInserta(procesoOperativo);
            if (NProcesosOperativos.Mensajes.Ok)
                return RedirectToAction(nameof(ProcesoOperativoCon));

            return await ProcesoOperativoInsertaCap(procesoOperativo);
        }
        [MValidaSeg(nameof(ProcesoOperativoActualiza))]
        public async Task<IActionResult> ProcesoOperativoActualizaIni(Int32 indice)
        {
            EV.Accion = MAccionesGen.Actualiza;
            EV.ProcesoOperativo.Indice = indice;
            EV.ProcesoOperativo.Sel = EV.ProcesoOperativo.Pag.Pagina[indice];
            return await ProcesoOperativoActualizaCap(EV.ProcesoOperativo.Sel);
        }
        [ValidateAntiForgeryToken]
        [MValidaSeg(nameof(ProcesoOperativoActualiza))]
        public async Task<IActionResult> ProcesoOperativoActualizaCap(EProcesoOperativo procesoOperativo)
        {
            return await ProcesoOperativoCaptura(procesoOperativo);
        }
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProcesoOperativoActualiza(EProcesoOperativo procesoOperativo)
        {
            if (await NProcesosOperativos.ProcesoOperativoActualiza(procesoOperativo))
                return RedirectToAction(nameof(ProcesoOperativoCon));

            return await ProcesoOperativoActualizaCap(procesoOperativo);
        }
        public async Task<IActionResult> ProcesoOperativoElimina(Int32 indice)
        {
            await NProcesosOperativos.ProcesoOperativoElimina(EV.ProcesoOperativo.Pag.Pagina[indice]);
            return RedirectToAction(nameof(ProcesoOperativoCon));
        }
        #endregion

        #region Funciones
        private async Task<IActionResult> ProcesoOperativoCaptura(EProcesoOperativo procesoOperativo)
        {
            ViewBag.Mensajes = NProcesosOperativos.Mensajes;
            ViewBag.EV = EV;

            return await Task.FromResult(ViewCap(nameof(ProcesoOperativoCaptura), procesoOperativo));
        }
        #endregion

        #region Acciones de Paginacion Orden y Filtro
        [MValidaSeg(nameof(ProcesoOperativoInicia))]
        public IActionResult ProcesoOperativoPaginacion(MEDatosPaginador datPag)
        {
            EV.ProcesoOperativo.Pag.DatPag = datPag;
            return RedirectToAction(nameof(ProcesoOperativoCon));
        }
        [MValidaSeg(nameof(ProcesoOperativoInicia))]
        public IActionResult ProcesoOperativoOrdena(String orden)
        {
            EV.ProcesoOperativo.ColOrden = orden;
            return RedirectToAction(nameof(ProcesoOperativoCon));
        }
        [MValidaSeg(nameof(ProcesoOperativoInicia))]
        public IActionResult ProcesoOperativoFiltra(EProcesoOperativoFiltro filtro)
        {
            EV.ProcesoOperativo.Filtro = filtro;
            return RedirectToAction(nameof(ProcesoOperativoCon));
        }
        [MValidaSeg(nameof(ProcesoOperativoInicia))]
        public IActionResult ProcesoOperativoLimpiaFiltros()
        {
            EV.ProcesoOperativo.Filtro = new EProcesoOperativoFiltro();
            return RedirectToAction(nameof(ProcesoOperativoCon));
        }
        #endregion

        #endregion

        #region ProcesoOperativoCol (ProcesosOperativosCols)

        #region Acciones
        public async Task<IActionResult> ProcesoOperativoColInicia(Int32 indice)
        {
            //Configuracion de inicio
            await Servicios.Gen.InicializaSF(EV.ProcesoOperativoCol, nameof(EProcesoOperativoCol.ConOrden),
                async () => await NProcesosOperativos.ProcesoOperativoColReglas());

            Servicios.Gen.InicializaSFInd(EV.ProcesoOperativo, indice);

            return RedirectToAction(nameof(ProcesoOperativoColCon));
        }
        [MValidaSeg(nameof(ProcesoOperativoColInicia))]
        public async Task<IActionResult> ProcesoOperativoColCon()
        {
            EV.ProcesoOperativoCol.Filtro.ProcesoOperativoId = EV.ProcesoOperativo.Sel.ProcesoOperativoId;

            await Servicios.Pag.CargaPagOrdYFil(EV.ProcesoOperativoCol);
            EV.ProcesoOperativoCol.Pag = await NProcesosOperativos.ProcesoOperativoColPag(EV.ProcesoOperativoCol.Filtro);
            await Servicios.Pag.ActTamPag(EV.ProcesoOperativoCol);

            ViewBag.Mensajes = NProcesosOperativos.Mensajes;
            ViewBag.EV = EV;

            //Adi
            ViewBag.PermiteInserta = !EV.ProcesoOperativo.Sel.TieneExpedientes;

            return View(nameof(ProcesoOperativoColCon), EV.ProcesoOperativoCol.Pag?.Pagina);
        }
        public async Task<IActionResult> ProcesoOperativoColXId(Int32 indice)
        {
            EV.Accion = MAccionesGen.Consulta;
            EV.ProcesoOperativoCol.Indice = indice;
            return await ProcesoOperativoColCaptura(EV.ProcesoOperativoCol.Pag.Pagina[indice]);
        }
        [MValidaSeg(nameof(ProcesoOperativoColInserta))]
        public async Task<IActionResult> ProcesoOperativoColInsertaIni()
        {
            EV.Accion = MAccionesGen.Inserta;
            return await ProcesoOperativoColInsertaCap(new EProcesoOperativoCol()
            {
                ConOrdenar = true,
                CapColumnas = 1,
                CapObligatorio = true,
                Activo = true
            });
        }
        [ValidateAntiForgeryToken]
        [MValidaSeg(nameof(ProcesoOperativoColInserta))]
        public async Task<IActionResult> ProcesoOperativoColInsertaCap(EProcesoOperativoCol procesoOperativoCol)
        {
            return await ProcesoOperativoColCaptura(procesoOperativoCol);
        }
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProcesoOperativoColInserta(EProcesoOperativoCol procesoOperativoCol)
        {
            procesoOperativoCol.ProcesoOperativoId = EV.ProcesoOperativo.Sel.ProcesoOperativoId; //Llave padre
            await NProcesosOperativos.ProcesoOperativoColInserta(procesoOperativoCol);
            if (NProcesosOperativos.Mensajes.Ok)
                return RedirectToAction(nameof(ProcesoOperativoColCon));

            return await ProcesoOperativoColInsertaCap(procesoOperativoCol);
        }
        [MValidaSeg(nameof(ProcesoOperativoColActualiza))]
        public async Task<IActionResult> ProcesoOperativoColActualizaIni(Int32 indice)
        {
            EV.Accion = MAccionesGen.Actualiza;
            EV.ProcesoOperativoCol.Indice = indice;
            EV.ProcesoOperativoCol.Sel = EV.ProcesoOperativoCol.Pag.Pagina[indice];
            return await ProcesoOperativoColActualizaCap(EV.ProcesoOperativoCol.Sel);
        }
        [ValidateAntiForgeryToken]
        [MValidaSeg(nameof(ProcesoOperativoColActualiza))]
        public async Task<IActionResult> ProcesoOperativoColActualizaCap(EProcesoOperativoCol procesoOperativoCol)
        {
            return await ProcesoOperativoColCaptura(procesoOperativoCol);
        }
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProcesoOperativoColActualiza(EProcesoOperativoCol procesoOperativoCol)
        {
            procesoOperativoCol.ProcesoOperativoId = EV.ProcesoOperativo.Sel.ProcesoOperativoId; //Llave padre
            if (await NProcesosOperativos.ProcesoOperativoColActualiza(procesoOperativoCol))
                return RedirectToAction(nameof(ProcesoOperativoColCon));

            return await ProcesoOperativoColActualizaCap(procesoOperativoCol);
        }
        public async Task<IActionResult> ProcesoOperativoColElimina(Int32 indice)
        {
            await NProcesosOperativos.ProcesoOperativoColElimina(EV.ProcesoOperativoCol.Pag.Pagina[indice]);
            return RedirectToAction(nameof(ProcesoOperativoColCon));
        }
        #endregion

        #region Funciones
        private async Task<IActionResult> ProcesoOperativoColCaptura(EProcesoOperativoCol procesoOperativoCol)
        {
            ViewBag.Mensajes = NProcesosOperativos.Mensajes;
            ViewBag.EV = EV;

            //Mod
            ViewBag.TieneExpedientes = EV.ProcesoOperativo.Sel.TieneExpedientes;

            await MUtilMvc.CargaCmbCascada(ViewData, NProcesosOperativos.Mensajes, "Id",
                ("ProcesosOperativos", async () => await NProcesosOperativos.ProcesoOperativoCmb()),
                (procesoOperativoCol.ProcesoOperativoId > 0, "ProcesosOperativosCols", async () => await NProcesosOperativos.ProcesoOperativoColCmb(procesoOperativoCol.ProcesoOperativoId)));

            await MUtilMvc.CargaCmbCascada(ViewData, NProcesosOperativos.Mensajes, "Texto",
                ("ProcesosOperativos", async () => await NProcesosOperativos.ProcesoOperativoCmb()),
                (procesoOperativoCol.ProcesoOperativoId > 0, "ProcesosOperativosCols", async () => await NProcesosOperativos.ProcesoOperativoColCmb(procesoOperativoCol.ProcesoOperativoId)));

            //CargaCombosPO("Id",
            //              procesoOperativoCol.CapCmbProcesoOperativoId);

            //CargaCombosPO("Texto",
            //              procesoOperativoCol.CapCmbProcesoOperativoId);

            return ViewCap(nameof(ProcesoOperativoColCaptura), procesoOperativoCol);
        }
        #endregion

        #region Acciones de Paginacion Orden y Filtro
        [MValidaSeg(nameof(ProcesoOperativoColInicia))]
        public IActionResult ProcesoOperativoColPaginacion(MEDatosPaginador datPag)
        {
            EV.ProcesoOperativoCol.Pag.DatPag = datPag;
            return RedirectToAction(nameof(ProcesoOperativoColCon));
        }
        [MValidaSeg(nameof(ProcesoOperativoColInicia))]
        public IActionResult ProcesoOperativoColOrdena(String orden)
        {
            EV.ProcesoOperativoCol.ColOrden = orden;
            return RedirectToAction(nameof(ProcesoOperativoColCon));
        }
        [MValidaSeg(nameof(ProcesoOperativoColInicia))]
        public IActionResult ProcesoOperativoColFiltra(EProcesoOperativoColFiltro filtro)
        {
            EV.ProcesoOperativoCol.Filtro = filtro;
            return RedirectToAction(nameof(ProcesoOperativoColCon));
        }
        [MValidaSeg(nameof(ProcesoOperativoColInicia))]
        public IActionResult ProcesoOperativoColLimpiaFiltros()
        {
            EV.ProcesoOperativoCol.Filtro = new EProcesoOperativoColFiltro();
            return RedirectToAction(nameof(ProcesoOperativoColCon));
        }
        #endregion

        #endregion

        #region ProcesoOperativoObjeto (ProcesosOperativosObjetos)

        #region Acciones
        public async Task<IActionResult> ProcesoOperativoObjetoInicia(Int32 indice)
        {
            //Configuracion de inicio
            await Servicios.Gen.InicializaSF(EV.ProcesoOperativoObjeto, nameof(EProcesoOperativoObjeto.ProcesoOperativoObjetoId),
                async () => await NProcesosOperativos.ProcesoOperativoObjetoReglas());

            Servicios.Gen.InicializaSFInd(EV.ProcesoOperativo, indice);

            return RedirectToAction(nameof(ProcesoOperativoObjetoCon));
        }
        [MValidaSeg(nameof(ProcesoOperativoObjetoInicia))]
        public async Task<IActionResult> ProcesoOperativoObjetoCon()
        {
            EV.ProcesoOperativoObjeto.Filtro.ProcesoOperativoId = EV.ProcesoOperativo.Sel.ProcesoOperativoId;

            await Servicios.Pag.CargaPagOrdYFil(EV.ProcesoOperativoObjeto);
            EV.ProcesoOperativoObjeto.Pag = await NProcesosOperativos.ProcesoOperativoObjetoPag(EV.ProcesoOperativoObjeto.Filtro);
            await Servicios.Pag.ActTamPag(EV.ProcesoOperativoObjeto);

            ViewBag.Mensajes = NProcesosOperativos.Mensajes;
            ViewBag.EV = EV;

            return View(nameof(ProcesoOperativoObjetoCon), EV.ProcesoOperativoObjeto.Pag?.Pagina);
        }
        public async Task<IActionResult> ProcesoOperativoObjetoXId(Int32 indice)
        {
            EV.Accion = MAccionesGen.Consulta;
            EV.ProcesoOperativoObjeto.Indice = indice;
            return await ProcesoOperativoObjetoCaptura(EV.ProcesoOperativoObjeto.Pag.Pagina[indice]);
        }
        [MValidaSeg(nameof(ProcesoOperativoObjetoInserta))]
        public async Task<IActionResult> ProcesoOperativoObjetoInsertaIni()
        {
            EV.Accion = MAccionesGen.Inserta;
            return await ProcesoOperativoObjetoInsertaCap(new EProcesoOperativoObjeto()
            {
                Activo = true
            });
        }
        [ValidateAntiForgeryToken]
        [MValidaSeg(nameof(ProcesoOperativoObjetoInserta))]
        public async Task<IActionResult> ProcesoOperativoObjetoInsertaCap(EProcesoOperativoObjeto procesoOperativoObjeto)
        {
            return await ProcesoOperativoObjetoCaptura(procesoOperativoObjeto);
        }
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProcesoOperativoObjetoInserta(EProcesoOperativoObjeto procesoOperativoObjeto)
        {
            procesoOperativoObjeto.ProcesoOperativoId = EV.ProcesoOperativo.Sel.ProcesoOperativoId; //Llave padre
            await NProcesosOperativos.ProcesoOperativoObjetoInserta(procesoOperativoObjeto);
            if (NProcesosOperativos.Mensajes.Ok)
                return RedirectToAction(nameof(ProcesoOperativoObjetoCon));

            return await ProcesoOperativoObjetoInsertaCap(procesoOperativoObjeto);
        }
        [MValidaSeg(nameof(ProcesoOperativoObjetoActualiza))]
        public async Task<IActionResult> ProcesoOperativoObjetoActualizaIni(Int32 indice)
        {
            EV.Accion = MAccionesGen.Actualiza;
            EV.ProcesoOperativoObjeto.Indice = indice;
            EV.ProcesoOperativoObjeto.Sel = EV.ProcesoOperativoObjeto.Pag.Pagina[indice];
            return await ProcesoOperativoObjetoActualizaCap(EV.ProcesoOperativoObjeto.Sel);
        }
        [ValidateAntiForgeryToken]
        [MValidaSeg(nameof(ProcesoOperativoObjetoActualiza))]
        public async Task<IActionResult> ProcesoOperativoObjetoActualizaCap(EProcesoOperativoObjeto procesoOperativoObjeto)
        {
            return await ProcesoOperativoObjetoCaptura(procesoOperativoObjeto);
        }
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProcesoOperativoObjetoActualiza(EProcesoOperativoObjeto procesoOperativoObjeto)
        {
            procesoOperativoObjeto.ProcesoOperativoId = EV.ProcesoOperativo.Sel.ProcesoOperativoId; //Llave padre
            if (await NProcesosOperativos.ProcesoOperativoObjetoActualiza(procesoOperativoObjeto))
                return RedirectToAction(nameof(ProcesoOperativoObjetoCon));

            return await ProcesoOperativoObjetoActualizaCap(procesoOperativoObjeto);
        }
        public async Task<IActionResult> ProcesoOperativoObjetoElimina(Int32 indice)
        {
            await NProcesosOperativos.ProcesoOperativoObjetoElimina(EV.ProcesoOperativoObjeto.Pag.Pagina[indice]);
            return RedirectToAction(nameof(ProcesoOperativoObjetoCon));
        }
        #endregion

        #region Funciones
        private async Task<IActionResult> ProcesoOperativoObjetoCaptura(EProcesoOperativoObjeto procesoOperativoObjeto)
        {
            ViewBag.Mensajes = NProcesosOperativos.Mensajes;
            ViewBag.EV = EV;

            return await Task.FromResult(ViewCap(nameof(ProcesoOperativoObjetoCaptura), procesoOperativoObjeto));
        }
        #endregion

        #region Acciones de Paginacion Orden y Filtro
        [MValidaSeg(nameof(ProcesoOperativoObjetoInicia))]
        public IActionResult ProcesoOperativoObjetoPaginacion(MEDatosPaginador datPag)
        {
            EV.ProcesoOperativoObjeto.Pag.DatPag = datPag;
            return RedirectToAction(nameof(ProcesoOperativoObjetoCon));
        }
        [MValidaSeg(nameof(ProcesoOperativoObjetoInicia))]
        public IActionResult ProcesoOperativoObjetoOrdena(String orden)
        {
            EV.ProcesoOperativoObjeto.ColOrden = orden;
            return RedirectToAction(nameof(ProcesoOperativoObjetoCon));
        }
        [MValidaSeg(nameof(ProcesoOperativoObjetoInicia))]
        public IActionResult ProcesoOperativoObjetoFiltra(EProcesoOperativoObjetoFiltro filtro)
        {
            EV.ProcesoOperativoObjeto.Filtro = filtro;
            return RedirectToAction(nameof(ProcesoOperativoObjetoCon));
        }
        [MValidaSeg(nameof(ProcesoOperativoObjetoInicia))]
        public IActionResult ProcesoOperativoObjetoLimpiaFiltros()
        {
            EV.ProcesoOperativoObjeto.Filtro = new EProcesoOperativoObjetoFiltro();
            return RedirectToAction(nameof(ProcesoOperativoObjetoCon));
        }
        #endregion

        #endregion

        #region ProcesoOperativoEst (ProcesosOperativosEst)

        #region Acciones
        public async Task<IActionResult> ProcesoOperativoEstInicia(Int32 indice)
        {
            //Configuracion de inicio
            await Servicios.Gen.InicializaSF(EV.ProcesoOperativoEst, nameof(EProcesoOperativoEst.Orden),
                async () => await NProcesosOperativos.ProcesoOperativoEstReglas());

            Servicios.Gen.InicializaSFInd(EV.ProcesoOperativo, indice);

            return RedirectToAction(nameof(ProcesoOperativoEstCon));
        }
        [MValidaSeg(nameof(ProcesoOperativoEstInicia))]
        public async Task<IActionResult> ProcesoOperativoEstCon()
        {
            EV.ProcesoOperativoEst.Filtro.ProcesoOperativoId = EV.ProcesoOperativo.Sel.ProcesoOperativoId;

            await Servicios.Pag.CargaPagOrdYFil(EV.ProcesoOperativoEst);
            EV.ProcesoOperativoEst.Pag = await NProcesosOperativos.ProcesoOperativoEstPag(EV.ProcesoOperativoEst.Filtro);
            await Servicios.Pag.ActTamPag(EV.ProcesoOperativoEst);

            ViewBag.Mensajes = NProcesosOperativos.Mensajes;
            ViewBag.EV = EV;

            return View(nameof(ProcesoOperativoEstCon), EV.ProcesoOperativoEst.Pag?.Pagina);
        }
        public async Task<IActionResult> ProcesoOperativoEstXId(Int32 indice)
        {
            EV.Accion = MAccionesGen.Consulta;
            EV.ProcesoOperativoEst.Indice = indice;
            return await ProcesoOperativoEstCaptura(EV.ProcesoOperativoEst.Pag.Pagina[indice]);
        }
        [MValidaSeg(nameof(ProcesoOperativoEstInserta))]
        public async Task<IActionResult> ProcesoOperativoEstInsertaIni()
        {
            EV.Accion = MAccionesGen.Inserta;
            return await ProcesoOperativoEstInsertaCap(new EProcesoOperativoEst()
            {
                Activo = true
            });
        }
        [ValidateAntiForgeryToken]
        [MValidaSeg(nameof(ProcesoOperativoEstInserta))]
        public async Task<IActionResult> ProcesoOperativoEstInsertaCap(EProcesoOperativoEst procesoOperativoEst)
        {
            return await ProcesoOperativoEstCaptura(procesoOperativoEst);
        }
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProcesoOperativoEstInserta(EProcesoOperativoEst procesoOperativoEst)
        {
            procesoOperativoEst.ProcesoOperativoId = EV.ProcesoOperativo.Sel.ProcesoOperativoId; //Llave padre
            await NProcesosOperativos.ProcesoOperativoEstInserta(procesoOperativoEst);
            if (NProcesosOperativos.Mensajes.Ok)
                return RedirectToAction(nameof(ProcesoOperativoEstCon));

            return await ProcesoOperativoEstInsertaCap(procesoOperativoEst);
        }
        [MValidaSeg(nameof(ProcesoOperativoEstActualiza))]
        public async Task<IActionResult> ProcesoOperativoEstActualizaIni(Int32 indice)
        {
            EV.Accion = MAccionesGen.Actualiza;
            EV.ProcesoOperativoEst.Indice = indice;
            EV.ProcesoOperativoEst.Sel = EV.ProcesoOperativoEst.Pag.Pagina[indice];
            return await ProcesoOperativoEstActualizaCap(EV.ProcesoOperativoEst.Sel);
        }
        [ValidateAntiForgeryToken]
        [MValidaSeg(nameof(ProcesoOperativoEstActualiza))]
        public async Task<IActionResult> ProcesoOperativoEstActualizaCap(EProcesoOperativoEst procesoOperativoEst)
        {
            return await ProcesoOperativoEstCaptura(procesoOperativoEst);
        }
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProcesoOperativoEstActualiza(EProcesoOperativoEst procesoOperativoEst)
        {
            procesoOperativoEst.ProcesoOperativoId = EV.ProcesoOperativo.Sel.ProcesoOperativoId; //Llave padre
            if (await NProcesosOperativos.ProcesoOperativoEstActualiza(procesoOperativoEst))
                return RedirectToAction(nameof(ProcesoOperativoEstCon));

            return await ProcesoOperativoEstActualizaCap(procesoOperativoEst);
        }
        public async Task<IActionResult> ProcesoOperativoEstElimina(Int32 indice)
        {
            await NProcesosOperativos.ProcesoOperativoEstElimina(EV.ProcesoOperativoEst.Pag.Pagina[indice]);
            return RedirectToAction(nameof(ProcesoOperativoEstCon));
        }
        #endregion

        #region Funciones
        private async Task<IActionResult> ProcesoOperativoEstCaptura(EProcesoOperativoEst procesoOperativoEst)
        {
            ViewBag.Mensajes = NProcesosOperativos.Mensajes;
            ViewBag.EV = EV;

            return await Task.FromResult(ViewCap(nameof(ProcesoOperativoEstCaptura), procesoOperativoEst));
        }
        #endregion

        #region Acciones de Paginacion Orden y Filtro
        [MValidaSeg(nameof(ProcesoOperativoEstInicia))]
        public IActionResult ProcesoOperativoEstPaginacion(MEDatosPaginador datPag)
        {
            EV.ProcesoOperativoEst.Pag.DatPag = datPag;
            return RedirectToAction(nameof(ProcesoOperativoEstCon));
        }
        [MValidaSeg(nameof(ProcesoOperativoEstInicia))]
        public IActionResult ProcesoOperativoEstOrdena(String orden)
        {
            EV.ProcesoOperativoEst.ColOrden = orden;
            return RedirectToAction(nameof(ProcesoOperativoEstCon));
        }
        #endregion

        #endregion

        #region ProcesoOperativoEstSec (ProcesosOperativosEstSec)

        #region Acciones
        public async Task<IActionResult> ProcesoOperativoEstSecInicia(Int32 indice)
        {
            //Configuracion de inicio
            await Servicios.Gen.InicializaSF(EV.ProcesoOperativoEstSec, nameof(EProcesoOperativoEstSec.ProcesoOperativoEstSecId),
                async () => await NProcesosOperativos.ProcesoOperativoEstSecReglas());

            Servicios.Gen.InicializaSFInd(EV.ProcesoOperativoEst, indice);

            return RedirectToAction(nameof(ProcesoOperativoEstSecCon));
        }
        [MValidaSeg(nameof(ProcesoOperativoEstSecInicia))]
        public async Task<IActionResult> ProcesoOperativoEstSecCon()
        {
            EV.ProcesoOperativoEstSec.Filtro.ProcesoOperativoEstId = EV.ProcesoOperativoEst.Sel.ProcesoOperativoEstId;

            await Servicios.Pag.CargaPagOrdYFil(EV.ProcesoOperativoEstSec);
            EV.ProcesoOperativoEstSec.Pag = await NProcesosOperativos.ProcesoOperativoEstSecPag(EV.ProcesoOperativoEstSec.Filtro);
            await Servicios.Pag.ActTamPag(EV.ProcesoOperativoEstSec);

            ViewBag.Mensajes = NProcesosOperativos.Mensajes;
            ViewBag.EV = EV;

            return View(nameof(ProcesoOperativoEstSecCon), EV.ProcesoOperativoEstSec.Pag?.Pagina);
        }
        public async Task<IActionResult> ProcesoOperativoEstSecXId(Int32 indice)
        {
            EV.Accion = MAccionesGen.Consulta;
            EV.ProcesoOperativoEstSec.Indice = indice;
            return await ProcesoOperativoEstSecCaptura(EV.ProcesoOperativoEstSec.Pag.Pagina[indice]);
        }
        [MValidaSeg(nameof(ProcesoOperativoEstSecInserta))]
        public async Task<IActionResult> ProcesoOperativoEstSecInsertaIni()
        {
            EV.Accion = MAccionesGen.Inserta;
            return await ProcesoOperativoEstSecInsertaCap(new EProcesoOperativoEstSec());
        }
        [ValidateAntiForgeryToken]
        [MValidaSeg(nameof(ProcesoOperativoEstSecInserta))]
        public async Task<IActionResult> ProcesoOperativoEstSecInsertaCap(EProcesoOperativoEstSec procesoOperativoEstSec)
        {
            return await ProcesoOperativoEstSecCaptura(procesoOperativoEstSec);
        }
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProcesoOperativoEstSecInserta(EProcesoOperativoEstSec procesoOperativoEstSec)
        {
            procesoOperativoEstSec.ProcesoOperativoEstId = EV.ProcesoOperativoEst.Sel.ProcesoOperativoEstId; //Llave padre
            await NProcesosOperativos.ProcesoOperativoEstSecInserta(procesoOperativoEstSec);
            if (NProcesosOperativos.Mensajes.Ok)
                return RedirectToAction(nameof(ProcesoOperativoEstSecCon));

            return await ProcesoOperativoEstSecInsertaCap(procesoOperativoEstSec);
        }
        [MValidaSeg(nameof(ProcesoOperativoEstSecActualiza))]
        public async Task<IActionResult> ProcesoOperativoEstSecActualizaIni(Int32 indice)
        {
            EV.Accion = MAccionesGen.Actualiza;
            EV.ProcesoOperativoEstSec.Indice = indice;
            EV.ProcesoOperativoEstSec.Sel = EV.ProcesoOperativoEstSec.Pag.Pagina[indice];
            return await ProcesoOperativoEstSecActualizaCap(EV.ProcesoOperativoEstSec.Sel);
        }
        [ValidateAntiForgeryToken]
        [MValidaSeg(nameof(ProcesoOperativoEstSecActualiza))]
        public async Task<IActionResult> ProcesoOperativoEstSecActualizaCap(EProcesoOperativoEstSec procesoOperativoEstSec)
        {
            return await ProcesoOperativoEstSecCaptura(procesoOperativoEstSec);
        }
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProcesoOperativoEstSecActualiza(EProcesoOperativoEstSec procesoOperativoEstSec)
        {
            procesoOperativoEstSec.ProcesoOperativoEstId = EV.ProcesoOperativoEst.Sel.ProcesoOperativoEstId; //Llave padre
            if (await NProcesosOperativos.ProcesoOperativoEstSecActualiza(procesoOperativoEstSec))
                return RedirectToAction(nameof(ProcesoOperativoEstSecCon));

            return await ProcesoOperativoEstSecActualizaCap(procesoOperativoEstSec);
        }
        public async Task<IActionResult> ProcesoOperativoEstSecElimina(Int32 indice)
        {
            await NProcesosOperativos.ProcesoOperativoEstSecElimina(EV.ProcesoOperativoEstSec.Pag.Pagina[indice]);
            return RedirectToAction(nameof(ProcesoOperativoEstSecCon));
        }
        #endregion

        #region Funciones
        private async Task<IActionResult> ProcesoOperativoEstSecCaptura(EProcesoOperativoEstSec procesoOperativoEstSec)
        {
            ViewBag.Mensajes = NProcesosOperativos.Mensajes;
            ViewBag.EV = EV;

            ViewBag.ProcesosOperativosEst = await NCatalogos.ProcesoOperativoEstCmb(EV.ProcesoOperativoEst.Sel.ProcesoOperativoId);

            return await Task.FromResult(ViewCap(nameof(ProcesoOperativoEstSecCaptura), procesoOperativoEstSec));
        }
        #endregion

        #region Acciones de Paginacion Orden y Filtro
        [MValidaSeg(nameof(ProcesoOperativoEstSecInicia))]
        public IActionResult ProcesoOperativoEstSecPaginacion(MEDatosPaginador datPag)
        {
            EV.ProcesoOperativoEstSec.Pag.DatPag = datPag;
            return RedirectToAction(nameof(ProcesoOperativoEstSecCon));
        }
        [MValidaSeg(nameof(ProcesoOperativoEstSecInicia))]
        public IActionResult ProcesoOperativoEstSecOrdena(String orden)
        {
            EV.ProcesoOperativoEstSec.ColOrden = orden;
            return RedirectToAction(nameof(ProcesoOperativoEstSecCon));
        }
        #endregion

        #endregion

        //#region Combos Grupo
        ////Mod
        //private async Task<void> CargaCombosPO(String prefAccion,
        //                                       Int64 capCmbProcesoOperativoId)
        //{
        //    ViewData["ProcesosOperativosCols"] = null;

        //    ViewData["ProcesosOperativos"] = await NProcesosOperativos.ProcesoOperativoCmb();
        //    if (!NProcesosOperativos.Mensajes.Ok)
        //        ViewData["Mensajes"] = NProcesosOperativos.Mensajes.ToString();

        //    if (capCmbProcesoOperativoId > 0)
        //    {
        //        ViewData[prefAccion + "ProcesosOperativosCols"] = NProcesosOperativos.ProcesoOperativoColCmb(capCmbProcesoOperativoId);
        //        if (!NProcesosOperativos.Mensajes.Ok)
        //            ViewData[prefAccion + "Mensajes"] = NProcesosOperativos.Mensajes.ToString();
        //    }
        //}
        //#endregion
    }
}
