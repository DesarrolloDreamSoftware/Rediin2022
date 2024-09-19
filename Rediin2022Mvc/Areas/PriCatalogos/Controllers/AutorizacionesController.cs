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
using Microsoft.VisualBasic;
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
    public class AutorizacionesController : MControllerMvcPri
    {
        #region Constructores
        public AutorizacionesController(INAutorizaciones nAutorizaciones,
                                        INProcesosOperativos nProcesosOperativos,
                                        INUsuarios nUsuarios)
        {
            NAutorizaciones = nAutorizaciones;
            NProcesosOperativos = nProcesosOperativos;
            NUsuarios = nUsuarios;
        }
        #endregion

        #region Propiedades
        private INAutorizaciones NAutorizaciones { get; set; }
        private INProcesosOperativos NProcesosOperativos { get; set; }
        private INUsuarios NUsuarios { get; set; }
        private EVAutorizaciones EV
        {
            get { return base.MEVCtrl<EVAutorizaciones>(); }
        }
        #endregion

        #region Autorizacion (Autorizaciones)

        #region Acciones
        public async Task<IActionResult> AutorizacionInicia()
        {
            //Configuracion de inicio
            await Servicios.Gen.InicializaSF(EV.Autorizacion, nameof(EAutorizacion.AutorizacionId),
                async () => await NAutorizaciones.AutorizacionReglas());

            return RedirectToAction(nameof(AutorizacionCon));
        }
        [MValidaSeg(nameof(AutorizacionInicia))]
        public async Task<IActionResult> AutorizacionCon()
        {
            await Servicios.Pag.CargaPagOrdYFil(EV.Autorizacion);
            EV.Autorizacion.Pag = await NAutorizaciones.AutorizacionPag(EV.Autorizacion.Filtro);
            await Servicios.Pag.ActTamPag(EV.Autorizacion);

            ViewBag.Mensajes = NAutorizaciones.Mensajes;
            ViewBag.EV = EV;

            ViewBag.ProcesosOperativos = await NProcesosOperativos.ProcesoOperativoCmb();

            return View(nameof(AutorizacionCon), EV.Autorizacion.Pag?.Pagina);
        }
        public async Task<IActionResult> AutorizacionXId(Int32 indice)
        {
            EV.Accion = MAccionesGen.Consulta;
            EV.Autorizacion.Indice = indice;
            return await AutorizacionCaptura(EV.Autorizacion.Pag.Pagina[indice]);
        }
        [MValidaSeg(nameof(AutorizacionInserta))]
        public async Task<IActionResult> AutorizacionInsertaIni()
        {
            EV.Accion = MAccionesGen.Inserta;
            return await AutorizacionInsertaCap(new EAutorizacion());
        }
        [ValidateAntiForgeryToken]
        [MValidaSeg(nameof(AutorizacionInserta))]
        public async Task<IActionResult> AutorizacionInsertaCap(EAutorizacion autorizacion)
        {
            return await AutorizacionCaptura(autorizacion);
        }
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AutorizacionInserta(EAutorizacion autorizacion)
        {
            await NAutorizaciones.AutorizacionInserta(autorizacion);
            if (NAutorizaciones.Mensajes.Ok)
                return RedirectToAction(nameof(AutorizacionCon));

            return await AutorizacionInsertaCap(autorizacion);
        }
        [MValidaSeg(nameof(AutorizacionActualiza))]
        public async Task<IActionResult> AutorizacionActualizaIni(Int32 indice)
        {
            EV.Accion = MAccionesGen.Actualiza;
            EV.Autorizacion.Indice = indice;
            EV.Autorizacion.Sel = EV.Autorizacion.Pag.Pagina[indice];
            return await AutorizacionActualizaCap(EV.Autorizacion.Sel);
        }
        [ValidateAntiForgeryToken]
        [MValidaSeg(nameof(AutorizacionActualiza))]
        public async Task<IActionResult> AutorizacionActualizaCap(EAutorizacion autorizacion)
        {
            return await AutorizacionCaptura(autorizacion);
        }
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AutorizacionActualiza(EAutorizacion autorizacion)
        {
            if (await NAutorizaciones.AutorizacionActualiza(autorizacion))
                return RedirectToAction(nameof(AutorizacionCon));

            return await AutorizacionActualizaCap(autorizacion);
        }
        public async Task<IActionResult> AutorizacionElimina(Int32 indice)
        {
            await NAutorizaciones.AutorizacionElimina(EV.Autorizacion.Pag.Pagina[indice]);
            return RedirectToAction(nameof(AutorizacionCon));
        }
        #endregion

        #region Funciones
        private async Task<IActionResult> AutorizacionCaptura(EAutorizacion autorizacion)
        {
            ViewBag.Mensajes = NAutorizaciones.Mensajes;
            ViewBag.EV = EV;

            ViewBag.ProcesosOperativos = await NProcesosOperativos.ProcesoOperativoCmb();

            return await Task.FromResult(ViewCap(nameof(AutorizacionCaptura), autorizacion));
        }
        #endregion

        #region Acciones de Paginacion Orden y Filtro
        [MValidaSeg(nameof(AutorizacionInicia))]
        public IActionResult AutorizacionPaginacion(MEDatosPaginador datPag)
        {
            EV.Autorizacion.Pag.DatPag = datPag;
            return RedirectToAction(nameof(AutorizacionCon));
        }
        [MValidaSeg(nameof(AutorizacionInicia))]
        public IActionResult AutorizacionOrdena(String orden)
        {
            EV.Autorizacion.ColOrden = orden;
            return RedirectToAction(nameof(AutorizacionCon));
        }
        [MValidaSeg(nameof(AutorizacionInicia))]
        public IActionResult AutorizacionFiltra(EAutorizacionFiltro filtro)
        {
            EV.Autorizacion.Filtro = filtro;
            return RedirectToAction(nameof(AutorizacionCon));
        }
        [MValidaSeg(nameof(AutorizacionInicia))]
        public IActionResult AutorizacionLimpiaFiltros()
        {
            EV.Autorizacion.Filtro = new EAutorizacionFiltro();
            return RedirectToAction(nameof(AutorizacionCon));
        }
        #endregion

        #endregion

        #region AutorizacionUsuario (AutorizacionesUsuarios)

        #region Acciones
        public async Task<IActionResult> AutorizacionUsuarioInicia(Int32 indice)
        {
            //Configuracion de inicio
            await Servicios.Gen.InicializaSF(EV.AutorizacionUsuario, nameof(EAutorizacionUsuario.AutorizacionUsuarioId),
                async () => await NAutorizaciones.AutorizacionUsuarioReglas());

            Servicios.Gen.InicializaSFInd(EV.Autorizacion, indice);

            return RedirectToAction(nameof(AutorizacionUsuarioCon));
        }
        [MValidaSeg(nameof(AutorizacionUsuarioInicia))]
        public async Task<IActionResult> AutorizacionUsuarioCon()
        {
            EV.AutorizacionUsuario.Filtro.AutorizacionId = EV.Autorizacion.Sel.AutorizacionId;
            
            await Servicios.Pag.CargaPagOrdYFil(EV.AutorizacionUsuario);
            EV.AutorizacionUsuario.Pag = await NAutorizaciones.AutorizacionUsuarioPag(EV.AutorizacionUsuario.Filtro);
            await Servicios.Pag.ActTamPag(EV.AutorizacionUsuario);

            ViewBag.Mensajes = NAutorizaciones.Mensajes;
            ViewBag.EV = EV;

            return View(nameof(AutorizacionUsuarioCon), EV.AutorizacionUsuario.Pag?.Pagina);
        }
        public async Task<IActionResult> AutorizacionUsuarioXId(Int32 indice)
        {
            EV.Accion = MAccionesGen.Consulta;
            EV.AutorizacionUsuario.Indice = indice;
            return await AutorizacionUsuarioCaptura(EV.AutorizacionUsuario.Pag.Pagina[indice]);
        }
        [MValidaSeg(nameof(AutorizacionUsuarioInserta))]
        public async Task<IActionResult> AutorizacionUsuarioInsertaIni()
        {
            EV.Accion = MAccionesGen.Inserta;
            return await AutorizacionUsuarioInsertaCap(new EAutorizacionUsuario());
        }
        [ValidateAntiForgeryToken]
        [MValidaSeg(nameof(AutorizacionUsuarioInserta))]
        public async Task<IActionResult> AutorizacionUsuarioInsertaCap(EAutorizacionUsuario autorizacionUsuario)
        {
            return await AutorizacionUsuarioCaptura(autorizacionUsuario);
        }
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AutorizacionUsuarioInserta(EAutorizacionUsuario autorizacionUsuario)
        {
            autorizacionUsuario.AutorizacionId = EV.Autorizacion.Sel.AutorizacionId; //Llave padre
            await NAutorizaciones.AutorizacionUsuarioInserta(autorizacionUsuario);
            if (NAutorizaciones.Mensajes.Ok)
                return RedirectToAction(nameof(AutorizacionUsuarioCon));

            return await AutorizacionUsuarioInsertaCap(autorizacionUsuario);
        }
        [MValidaSeg(nameof(AutorizacionUsuarioActualiza))]
        public async Task<IActionResult> AutorizacionUsuarioActualizaIni(Int32 indice)
        {
            EV.Accion = MAccionesGen.Actualiza;
            EV.AutorizacionUsuario.Indice = indice;
            EV.AutorizacionUsuario.Sel = EV.AutorizacionUsuario.Pag.Pagina[indice];
            return await AutorizacionUsuarioActualizaCap(EV.AutorizacionUsuario.Sel);
        }
        [ValidateAntiForgeryToken]
        [MValidaSeg(nameof(AutorizacionUsuarioActualiza))]
        public async Task<IActionResult> AutorizacionUsuarioActualizaCap(EAutorizacionUsuario autorizacionUsuario)
        {
            return await AutorizacionUsuarioCaptura(autorizacionUsuario);
        }
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AutorizacionUsuarioActualiza(EAutorizacionUsuario autorizacionUsuario)
        {
            autorizacionUsuario.AutorizacionId = EV.Autorizacion.Sel.AutorizacionId; //Llave padre
            if (await NAutorizaciones.AutorizacionUsuarioActualiza(autorizacionUsuario))
                return RedirectToAction(nameof(AutorizacionUsuarioCon));

            return await AutorizacionUsuarioActualizaCap(autorizacionUsuario);
        }
        public async Task<IActionResult> AutorizacionUsuarioElimina(Int32 indice)
        {
            await NAutorizaciones.AutorizacionUsuarioElimina(EV.AutorizacionUsuario.Pag.Pagina[indice]);
            return RedirectToAction(nameof(AutorizacionUsuarioCon));
        }
        #endregion

        #region Funciones
        private async Task<IActionResult> AutorizacionUsuarioCaptura(EAutorizacionUsuario autorizacionUsuario)
        {
            ViewBag.Mensajes = NAutorizaciones.Mensajes;
            ViewBag.EV = EV;

            ViewBag.ProcesosOperativosEst = await NProcesosOperativos.ProcesoOperativoEstCmb(EV.Autorizacion.Sel.ProcesoOperativoId);

            return await Task.FromResult(ViewCap(nameof(AutorizacionUsuarioCaptura), autorizacionUsuario));
        }
        #endregion

        #region Acciones de Paginacion Orden y Filtro
        [MValidaSeg(nameof(AutorizacionUsuarioInicia))]
        public IActionResult AutorizacionUsuarioPaginacion(MEDatosPaginador datPag)
        {
            EV.AutorizacionUsuario.Pag.DatPag = datPag;
            return RedirectToAction(nameof(AutorizacionUsuarioCon));
        }
        [MValidaSeg(nameof(AutorizacionUsuarioInicia))]
        public IActionResult AutorizacionUsuarioOrdena(String orden)
        {
            EV.AutorizacionUsuario.ColOrden = orden;
            return RedirectToAction(nameof(AutorizacionUsuarioCon));
        }
        [MValidaSeg(nameof(AutorizacionUsuarioInicia))]
        public IActionResult AutorizacionUsuarioFiltra(EAutorizacionUsuarioFiltro filtro)
        {
            EV.AutorizacionUsuario.Filtro = filtro;
            return RedirectToAction(nameof(AutorizacionUsuarioCon));
        }
        [MValidaSeg(nameof(AutorizacionUsuarioInicia))]
        public IActionResult AutorizacionUsuarioLimpiaFiltros()
        {
            EV.AutorizacionUsuario.Filtro = new EAutorizacionUsuarioFiltro();
            return RedirectToAction(nameof(AutorizacionUsuarioCon));
        }
        #endregion

        #endregion

        #region Combos Filtro
        [MValidaSeg(nameof(AutorizacionUsuarioInicia))]
        public async Task<IActionResult> OpcUsuarioId(String usuarioId)
        {
            return Ok(MUtilMvc.ElementosAHtml(await NUsuarios.UsuarioCmb(EV.Autorizacion.Sel.EstablecimientoId,
                                                                         usuarioId)));
        }
        #endregion
    }
}
