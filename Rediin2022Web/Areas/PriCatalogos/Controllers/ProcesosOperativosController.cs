using DSEntityNetX.Common.Casting;
using DSEntityNetX.Common.File;
using DSEntityNetX.Common.Pagination;
using DSMetodNetX.Aplicacion;
using DSMetodNetX.Entidades;
using DSMetodNetX.Mvc.Seguridad;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Rediin2022.Aplicacion.PriCatalogos;
using Rediin2022.Entidades.Idioma;
using Rediin2022.Entidades.PriCatalogos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Rediin2022Web.Areas.PriCatalogos.Controllers
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
        private EVProcesosOperativos EVProcesosOperativos
        {
            get
            {
                if (base.MSesion<EVProcesosOperativos>() == null)
                    base.MSesion(new EVProcesosOperativos());

                return base.MSesionAuto<EVProcesosOperativos>();
            }
        }
        #endregion

        #region ProcesoOperativo (ProcesosOperativos)

        #region Acciones
        public IActionResult ProcesoOperativoInicia()
        {
            //Configuracion de inicio
            if (String.IsNullOrWhiteSpace(EVProcesosOperativos.ProcesoOperativoColOrden))
                EVProcesosOperativos.ProcesoOperativoColOrden = nameof(EProcesoOperativo.Orden);

            if (EVProcesosOperativos.ProcesoOperativoReglas == null)
            {
                EVProcesosOperativos.ProcesoOperativoReglas = NProcesosOperativos.ProcesoOperativoReglas();
                base.MMensajesTemp = NProcesosOperativos.Mensajes.ToString();
            }

            return RedirectToAction(nameof(ProcesoOperativoCon));
        }
        [MValidaSeg(nameof(ProcesoOperativoInicia))]
        public IActionResult ProcesoOperativoCon()
        {
            base.MCargaFiltroPagYOrd(EVProcesosOperativos.ProcesoOperativoFiltro,
                                     EVProcesosOperativos.ProcesoOperativoPag,
                                     EVProcesosOperativos.ProcesoOperativoColOrden,
                                     nameof(EProcesoOperativo));

            EVProcesosOperativos.ProcesoOperativoPag = NProcesosOperativos.ProcesoOperativoPag(EVProcesosOperativos.ProcesoOperativoFiltro);
            base.MActualizaTamPag(EVProcesosOperativos.ProcesoOperativoPag?.DatPag);

            ViewBag.Mensajes = base.MObtenMensajes(NProcesosOperativos.Mensajes);
            ViewBag.Reglas = EVProcesosOperativos.ProcesoOperativoReglas;
            ViewBag.DatPag = EVProcesosOperativos.ProcesoOperativoPag?.DatPag;
            ViewBag.Orden = EVProcesosOperativos.ProcesoOperativoColOrden;
            ViewBag.Filtro = EVProcesosOperativos.ProcesoOperativoFiltro;
            ViewBag.Indice = EVProcesosOperativos.ProcesoOperativoIndice;

            return View(nameof(ProcesoOperativoCon), EVProcesosOperativos.ProcesoOperativoPag?.Pagina);
        }
        public IActionResult ProcesoOperativoXId(Int32 indice)
        {
            EVProcesosOperativos.Accion = MAccionesGen.Consulta;
            EVProcesosOperativos.ProcesoOperativoIndice = indice;
            return ProcesoOperativoCaptura(EVProcesosOperativos.ProcesoOperativoPag.Pagina[indice]);
        }
        [MValidaSeg(nameof(ProcesoOperativoInserta))]
        public IActionResult ProcesoOperativoInsertaIni()
        {
            EVProcesosOperativos.Accion = MAccionesGen.Inserta;
            return ProcesoOperativoInsertaCap(new EProcesoOperativo()
            {
                Activo = true
            });
        }
        [ValidateAntiForgeryToken]
        [MValidaSeg(nameof(ProcesoOperativoInserta))]
        public IActionResult ProcesoOperativoInsertaCap(EProcesoOperativo procesoOperativo)
        {
            return ProcesoOperativoCaptura(procesoOperativo);
        }
        [ValidateAntiForgeryToken]
        public IActionResult ProcesoOperativoInserta(EProcesoOperativo procesoOperativo)
        {
            NProcesosOperativos.ProcesoOperativoInserta(procesoOperativo);
            if (NProcesosOperativos.Mensajes.Ok)
                return RedirectToAction(nameof(ProcesoOperativoCon));

            return ProcesoOperativoInsertaCap(procesoOperativo);
        }
        [MValidaSeg(nameof(ProcesoOperativoActualiza))]
        public IActionResult ProcesoOperativoActualizaIni(Int32 indice)
        {
            EVProcesosOperativos.Accion = MAccionesGen.Actualiza;
            EVProcesosOperativos.ProcesoOperativoIndice = indice;
            EVProcesosOperativos.ProcesoOperativoSel = EVProcesosOperativos.ProcesoOperativoPag.Pagina[indice];
            return ProcesoOperativoActualizaCap(EVProcesosOperativos.ProcesoOperativoSel);
        }
        [ValidateAntiForgeryToken]
        [MValidaSeg(nameof(ProcesoOperativoActualiza))]
        public IActionResult ProcesoOperativoActualizaCap(EProcesoOperativo procesoOperativo)
        {
            return ProcesoOperativoCaptura(procesoOperativo);
        }
        [ValidateAntiForgeryToken]
        public IActionResult ProcesoOperativoActualiza(EProcesoOperativo procesoOperativo)
        {
            if (NProcesosOperativos.ProcesoOperativoActualiza(procesoOperativo))
                return RedirectToAction(nameof(ProcesoOperativoCon));

            return ProcesoOperativoActualizaCap(procesoOperativo);
        }
        public IActionResult ProcesoOperativoElimina(Int32 indice)
        {
            NProcesosOperativos.ProcesoOperativoElimina(EVProcesosOperativos.ProcesoOperativoPag.Pagina[indice]);
            base.MMensajesTemp = NProcesosOperativos.Mensajes.ToString();
            return RedirectToAction(nameof(ProcesoOperativoCon));
        }
        #endregion

        #region Funciones
        private IActionResult ProcesoOperativoCaptura(EProcesoOperativo procesoOperativo)
        {
            ViewBag.Mensajes = base.MObtenMensajes(NProcesosOperativos.Mensajes);
            ViewBag.Accion = EVProcesosOperativos.Accion;
            ViewBag.Reglas = EVProcesosOperativos.ProcesoOperativoReglas;

            return ViewCap(nameof(ProcesoOperativoCaptura), procesoOperativo);
        }
        #endregion

        #region Acciones de Paginacion Orden y Filtro
        [MValidaSeg(nameof(ProcesoOperativoInicia))]
        public IActionResult ProcesoOperativoPaginacion(MEDatosPaginador datPag)
        {
            EVProcesosOperativos.ProcesoOperativoPag.DatPag = datPag;
            return RedirectToAction(nameof(ProcesoOperativoCon));
        }
        [MValidaSeg(nameof(ProcesoOperativoInicia))]
        public IActionResult ProcesoOperativoOrdena(String orden)
        {
            EVProcesosOperativos.ProcesoOperativoColOrden = orden;
            return RedirectToAction(nameof(ProcesoOperativoCon));
        }
        [MValidaSeg(nameof(ProcesoOperativoInicia))]
        public IActionResult ProcesoOperativoFiltra(EProcesoOperativoFiltro filtro)
        {
            EVProcesosOperativos.ProcesoOperativoFiltro = filtro;
            return RedirectToAction(nameof(ProcesoOperativoCon));
        }
        [MValidaSeg(nameof(ProcesoOperativoInicia))]
        public IActionResult ProcesoOperativoLimpiaFiltros()
        {
            EVProcesosOperativos.ProcesoOperativoFiltro = new EProcesoOperativoFiltro();
            return RedirectToAction(nameof(ProcesoOperativoCon));
        }
        #endregion

        #endregion

        #region ProcesoOperativoCol (ProcesosOperativosCols)

        #region Acciones
        public IActionResult ProcesoOperativoColInicia(Int32 indice)
        {
            //Configuracion de inicio
            if (String.IsNullOrWhiteSpace(EVProcesosOperativos.ProcesoOperativoColColOrden))
                EVProcesosOperativos.ProcesoOperativoColColOrden = nameof(EProcesoOperativoCol.ConOrden);

            if (EVProcesosOperativos.ProcesoOperativoColReglas == null)
            {
                EVProcesosOperativos.ProcesoOperativoColReglas = NProcesosOperativos.ProcesoOperativoColReglas();
                base.MMensajesTemp = NProcesosOperativos.Mensajes.ToString();
            }

            if (indice >= 0)
            {
                EVProcesosOperativos.ProcesoOperativoIndice = indice;
                EVProcesosOperativos.ProcesoOperativoSel = EVProcesosOperativos.ProcesoOperativoPag.Pagina[indice];
            }

            return RedirectToAction(nameof(ProcesoOperativoColCon));
        }
        [MValidaSeg(nameof(ProcesoOperativoColInicia))]
        public IActionResult ProcesoOperativoColCon()
        {
            EVProcesosOperativos.ProcesoOperativoColFiltro.ProcesoOperativoId = EVProcesosOperativos.ProcesoOperativoSel.ProcesoOperativoId;
            base.MCargaFiltroPagYOrd(EVProcesosOperativos.ProcesoOperativoColFiltro,
                                     EVProcesosOperativos.ProcesoOperativoColPag,
                                     EVProcesosOperativos.ProcesoOperativoColColOrden,
                                     nameof(EProcesoOperativoCol));

            EVProcesosOperativos.ProcesoOperativoColPag = NProcesosOperativos.ProcesoOperativoColPag(EVProcesosOperativos.ProcesoOperativoColFiltro);
            base.MActualizaTamPag(EVProcesosOperativos.ProcesoOperativoColPag?.DatPag);

            ViewBag.Mensajes = base.MObtenMensajes(NProcesosOperativos.Mensajes);
            ViewBag.Reglas = EVProcesosOperativos.ProcesoOperativoColReglas;
            ViewBag.DatPag = EVProcesosOperativos.ProcesoOperativoColPag?.DatPag;
            ViewBag.Orden = EVProcesosOperativos.ProcesoOperativoColColOrden;
            ViewBag.Filtro = EVProcesosOperativos.ProcesoOperativoColFiltro;
            ViewBag.Indice = EVProcesosOperativos.ProcesoOperativoColIndice;
            //Adi
            ViewBag.PermiteInserta = !EVProcesosOperativos.ProcesoOperativoSel.TieneExpedientes;

            return View(nameof(ProcesoOperativoColCon), EVProcesosOperativos.ProcesoOperativoColPag?.Pagina);
        }
        public IActionResult ProcesoOperativoColXId(Int32 indice)
        {
            EVProcesosOperativos.Accion = MAccionesGen.Consulta;
            EVProcesosOperativos.ProcesoOperativoColIndice = indice;
            return ProcesoOperativoColCaptura(EVProcesosOperativos.ProcesoOperativoColPag.Pagina[indice]);
        }
        [MValidaSeg(nameof(ProcesoOperativoColInserta))]
        public IActionResult ProcesoOperativoColInsertaIni()
        {
            EVProcesosOperativos.Accion = MAccionesGen.Inserta;
            return ProcesoOperativoColInsertaCap(new EProcesoOperativoCol()
            {
                ConOrdenar = true,
                CapColumnas = 1,
                CapObligatorio = true,
                Activo = true
            });
        }
        [ValidateAntiForgeryToken]
        [MValidaSeg(nameof(ProcesoOperativoColInserta))]
        public IActionResult ProcesoOperativoColInsertaCap(EProcesoOperativoCol procesoOperativoCol)
        {
            return ProcesoOperativoColCaptura(procesoOperativoCol);
        }
        [ValidateAntiForgeryToken]
        public IActionResult ProcesoOperativoColInserta(EProcesoOperativoCol procesoOperativoCol)
        {
            procesoOperativoCol.ProcesoOperativoId = EVProcesosOperativos.ProcesoOperativoSel.ProcesoOperativoId; //Llave padre
            NProcesosOperativos.ProcesoOperativoColInserta(procesoOperativoCol);
            if (NProcesosOperativos.Mensajes.Ok)
                return RedirectToAction(nameof(ProcesoOperativoColCon));

            return ProcesoOperativoColInsertaCap(procesoOperativoCol);
        }
        [MValidaSeg(nameof(ProcesoOperativoColActualiza))]
        public IActionResult ProcesoOperativoColActualizaIni(Int32 indice)
        {
            EVProcesosOperativos.Accion = MAccionesGen.Actualiza;
            EVProcesosOperativos.ProcesoOperativoColIndice = indice;
            EVProcesosOperativos.ProcesoOperativoColSel = EVProcesosOperativos.ProcesoOperativoColPag.Pagina[indice];
            return ProcesoOperativoColActualizaCap(EVProcesosOperativos.ProcesoOperativoColSel);
        }
        [ValidateAntiForgeryToken]
        [MValidaSeg(nameof(ProcesoOperativoColActualiza))]
        public IActionResult ProcesoOperativoColActualizaCap(EProcesoOperativoCol procesoOperativoCol)
        {
            return ProcesoOperativoColCaptura(procesoOperativoCol);
        }
        [ValidateAntiForgeryToken]
        public IActionResult ProcesoOperativoColActualiza(EProcesoOperativoCol procesoOperativoCol)
        {
            procesoOperativoCol.ProcesoOperativoId = EVProcesosOperativos.ProcesoOperativoSel.ProcesoOperativoId; //Llave padre
            if (NProcesosOperativos.ProcesoOperativoColActualiza(procesoOperativoCol))
                return RedirectToAction(nameof(ProcesoOperativoColCon));

            return ProcesoOperativoColActualizaCap(procesoOperativoCol);
        }
        public IActionResult ProcesoOperativoColElimina(Int32 indice)
        {
            NProcesosOperativos.ProcesoOperativoColElimina(EVProcesosOperativos.ProcesoOperativoColPag.Pagina[indice]);
            base.MMensajesTemp = NProcesosOperativos.Mensajes.ToString();
            return RedirectToAction(nameof(ProcesoOperativoColCon));
        }
        #endregion

        #region Funciones
        private IActionResult ProcesoOperativoColCaptura(EProcesoOperativoCol procesoOperativoCol)
        {
            ViewBag.Mensajes = base.MObtenMensajes(NProcesosOperativos.Mensajes);
            ViewBag.Accion = EVProcesosOperativos.Accion;
            ViewBag.Reglas = EVProcesosOperativos.ProcesoOperativoColReglas;
            ViewBag.TieneExpedientes = EVProcesosOperativos.ProcesoOperativoSel.TieneExpedientes;

            return ViewCap(nameof(ProcesoOperativoColCaptura), procesoOperativoCol);
        }
        #endregion

        #region Acciones de Paginacion Orden y Filtro
        [MValidaSeg(nameof(ProcesoOperativoColInicia))]
        public IActionResult ProcesoOperativoColPaginacion(MEDatosPaginador datPag)
        {
            EVProcesosOperativos.ProcesoOperativoColPag.DatPag = datPag;
            return RedirectToAction(nameof(ProcesoOperativoColCon));
        }
        [MValidaSeg(nameof(ProcesoOperativoColInicia))]
        public IActionResult ProcesoOperativoColOrdena(String orden)
        {
            EVProcesosOperativos.ProcesoOperativoColColOrden = orden;
            return RedirectToAction(nameof(ProcesoOperativoColCon));
        }
        [MValidaSeg(nameof(ProcesoOperativoColInicia))]
        public IActionResult ProcesoOperativoColFiltra(EProcesoOperativoColFiltro filtro)
        {
            EVProcesosOperativos.ProcesoOperativoColFiltro = filtro;
            return RedirectToAction(nameof(ProcesoOperativoColCon));
        }
        [MValidaSeg(nameof(ProcesoOperativoColInicia))]
        public IActionResult ProcesoOperativoColLimpiaFiltros()
        {
            EVProcesosOperativos.ProcesoOperativoColFiltro = new EProcesoOperativoColFiltro();
            return RedirectToAction(nameof(ProcesoOperativoColCon));
        }
        #endregion

        #endregion

        #region ProcesoOperativoObjeto (ProcesosOperativosObjetos)

        #region Acciones
        public IActionResult ProcesoOperativoObjetoInicia(Int32 indice)
        {
            //Configuracion de inicio
            if (String.IsNullOrWhiteSpace(EVProcesosOperativos.ProcesoOperativoObjetoColOrden))
                EVProcesosOperativos.ProcesoOperativoObjetoColOrden = nameof(EProcesoOperativoObjeto.ProcesoOperativoObjetoId);

            if (EVProcesosOperativos.ProcesoOperativoObjetoReglas == null)
            {
                EVProcesosOperativos.ProcesoOperativoObjetoReglas = NProcesosOperativos.ProcesoOperativoObjetoReglas();
                base.MMensajesTemp = NProcesosOperativos.Mensajes.ToString();
            }

            if (indice >= 0)
            {
                EVProcesosOperativos.ProcesoOperativoIndice = indice;
                EVProcesosOperativos.ProcesoOperativoSel = EVProcesosOperativos.ProcesoOperativoPag.Pagina[indice];
            }

            return RedirectToAction(nameof(ProcesoOperativoObjetoCon));
        }
        [MValidaSeg(nameof(ProcesoOperativoObjetoInicia))]
        public IActionResult ProcesoOperativoObjetoCon()
        {
            EVProcesosOperativos.ProcesoOperativoObjetoFiltro.ProcesoOperativoId = EVProcesosOperativos.ProcesoOperativoSel.ProcesoOperativoId;
            base.MCargaFiltroPagYOrd(EVProcesosOperativos.ProcesoOperativoObjetoFiltro,
                                     EVProcesosOperativos.ProcesoOperativoObjetoPag,
                                     EVProcesosOperativos.ProcesoOperativoObjetoColOrden,
                                     nameof(EProcesoOperativoObjeto));

            EVProcesosOperativos.ProcesoOperativoObjetoPag = NProcesosOperativos.ProcesoOperativoObjetoPag(EVProcesosOperativos.ProcesoOperativoObjetoFiltro);
            base.MActualizaTamPag(EVProcesosOperativos.ProcesoOperativoObjetoPag?.DatPag);

            ViewBag.Mensajes = base.MObtenMensajes(NProcesosOperativos.Mensajes);
            ViewBag.Reglas = EVProcesosOperativos.ProcesoOperativoObjetoReglas;
            ViewBag.DatPag = EVProcesosOperativos.ProcesoOperativoObjetoPag?.DatPag;
            ViewBag.Orden = EVProcesosOperativos.ProcesoOperativoObjetoColOrden;
            ViewBag.Filtro = EVProcesosOperativos.ProcesoOperativoObjetoFiltro;
            ViewBag.Indice = EVProcesosOperativos.ProcesoOperativoObjetoIndice;

            return View(nameof(ProcesoOperativoObjetoCon), EVProcesosOperativos.ProcesoOperativoObjetoPag?.Pagina);
        }
        public IActionResult ProcesoOperativoObjetoXId(Int32 indice)
        {
            EVProcesosOperativos.Accion = MAccionesGen.Consulta;
            EVProcesosOperativos.ProcesoOperativoObjetoIndice = indice;
            return ProcesoOperativoObjetoCaptura(EVProcesosOperativos.ProcesoOperativoObjetoPag.Pagina[indice]);
        }
        [MValidaSeg(nameof(ProcesoOperativoObjetoInserta))]
        public IActionResult ProcesoOperativoObjetoInsertaIni()
        {
            EVProcesosOperativos.Accion = MAccionesGen.Inserta;
            return ProcesoOperativoObjetoInsertaCap(new EProcesoOperativoObjeto()
            {
                Activo = true
            });
        }
        [ValidateAntiForgeryToken]
        [MValidaSeg(nameof(ProcesoOperativoObjetoInserta))]
        public IActionResult ProcesoOperativoObjetoInsertaCap(EProcesoOperativoObjeto procesoOperativoObjeto)
        {
            return ProcesoOperativoObjetoCaptura(procesoOperativoObjeto);
        }
        [ValidateAntiForgeryToken]
        public IActionResult ProcesoOperativoObjetoInserta(EProcesoOperativoObjeto procesoOperativoObjeto)
        {
            procesoOperativoObjeto.ProcesoOperativoId = EVProcesosOperativos.ProcesoOperativoSel.ProcesoOperativoId; //Llave padre
            NProcesosOperativos.ProcesoOperativoObjetoInserta(procesoOperativoObjeto);
            if (NProcesosOperativos.Mensajes.Ok)
                return RedirectToAction(nameof(ProcesoOperativoObjetoCon));

            return ProcesoOperativoObjetoInsertaCap(procesoOperativoObjeto);
        }
        [MValidaSeg(nameof(ProcesoOperativoObjetoActualiza))]
        public IActionResult ProcesoOperativoObjetoActualizaIni(Int32 indice)
        {
            EVProcesosOperativos.Accion = MAccionesGen.Actualiza;
            EVProcesosOperativos.ProcesoOperativoObjetoIndice = indice;
            EVProcesosOperativos.ProcesoOperativoObjetoSel = EVProcesosOperativos.ProcesoOperativoObjetoPag.Pagina[indice];
            return ProcesoOperativoObjetoActualizaCap(EVProcesosOperativos.ProcesoOperativoObjetoSel);
        }
        [ValidateAntiForgeryToken]
        [MValidaSeg(nameof(ProcesoOperativoObjetoActualiza))]
        public IActionResult ProcesoOperativoObjetoActualizaCap(EProcesoOperativoObjeto procesoOperativoObjeto)
        {
            return ProcesoOperativoObjetoCaptura(procesoOperativoObjeto);
        }
        [ValidateAntiForgeryToken]
        public IActionResult ProcesoOperativoObjetoActualiza(EProcesoOperativoObjeto procesoOperativoObjeto)
        {
            procesoOperativoObjeto.ProcesoOperativoId = EVProcesosOperativos.ProcesoOperativoSel.ProcesoOperativoId; //Llave padre
            if (NProcesosOperativos.ProcesoOperativoObjetoActualiza(procesoOperativoObjeto))
                return RedirectToAction(nameof(ProcesoOperativoObjetoCon));

            return ProcesoOperativoObjetoActualizaCap(procesoOperativoObjeto);
        }
        public IActionResult ProcesoOperativoObjetoElimina(Int32 indice)
        {
            NProcesosOperativos.ProcesoOperativoObjetoElimina(EVProcesosOperativos.ProcesoOperativoObjetoPag.Pagina[indice]);
            base.MMensajesTemp = NProcesosOperativos.Mensajes.ToString();
            return RedirectToAction(nameof(ProcesoOperativoObjetoCon));
        }
        #endregion

        #region Funciones
        private IActionResult ProcesoOperativoObjetoCaptura(EProcesoOperativoObjeto procesoOperativoObjeto)
        {
            ViewBag.Mensajes = base.MObtenMensajes(NProcesosOperativos.Mensajes);
            ViewBag.Accion = EVProcesosOperativos.Accion;
            ViewBag.Reglas = EVProcesosOperativos.ProcesoOperativoObjetoReglas;

            return ViewCap(nameof(ProcesoOperativoObjetoCaptura), procesoOperativoObjeto);
        }
        #endregion

        #region Acciones de Paginacion Orden y Filtro
        [MValidaSeg(nameof(ProcesoOperativoObjetoInicia))]
        public IActionResult ProcesoOperativoObjetoPaginacion(MEDatosPaginador datPag)
        {
            EVProcesosOperativos.ProcesoOperativoObjetoPag.DatPag = datPag;
            return RedirectToAction(nameof(ProcesoOperativoObjetoCon));
        }
        [MValidaSeg(nameof(ProcesoOperativoObjetoInicia))]
        public IActionResult ProcesoOperativoObjetoOrdena(String orden)
        {
            EVProcesosOperativos.ProcesoOperativoObjetoColOrden = orden;
            return RedirectToAction(nameof(ProcesoOperativoObjetoCon));
        }
        [MValidaSeg(nameof(ProcesoOperativoObjetoInicia))]
        public IActionResult ProcesoOperativoObjetoFiltra(EProcesoOperativoObjetoFiltro filtro)
        {
            EVProcesosOperativos.ProcesoOperativoObjetoFiltro = filtro;
            return RedirectToAction(nameof(ProcesoOperativoObjetoCon));
        }
        [MValidaSeg(nameof(ProcesoOperativoObjetoInicia))]
        public IActionResult ProcesoOperativoObjetoLimpiaFiltros()
        {
            EVProcesosOperativos.ProcesoOperativoObjetoFiltro = new EProcesoOperativoObjetoFiltro();
            return RedirectToAction(nameof(ProcesoOperativoObjetoCon));
        }
        #endregion

        #endregion

        #region ProcesoOperativoEst (ProcesosOperativosEst)

        #region Acciones
        public IActionResult ProcesoOperativoEstInicia(Int32 indice)
        {
            //Configuracion de inicio
            if (String.IsNullOrWhiteSpace(EVProcesosOperativos.ProcesoOperativoEstColOrden))
                EVProcesosOperativos.ProcesoOperativoEstColOrden = nameof(EProcesoOperativoEst.Orden);

            if (EVProcesosOperativos.ProcesoOperativoEstReglas == null)
            {
                EVProcesosOperativos.ProcesoOperativoEstReglas = NProcesosOperativos.ProcesoOperativoEstReglas();
                base.MMensajesTemp = NProcesosOperativos.Mensajes.ToString();
            }

            if (indice >= 0)
            {
                EVProcesosOperativos.ProcesoOperativoIndice = indice;
                EVProcesosOperativos.ProcesoOperativoSel = EVProcesosOperativos.ProcesoOperativoPag.Pagina[indice];
            }

            return RedirectToAction(nameof(ProcesoOperativoEstCon));
        }
        [MValidaSeg(nameof(ProcesoOperativoEstInicia))]
        public IActionResult ProcesoOperativoEstCon()
        {
            EVProcesosOperativos.ProcesoOperativoEstFiltro.ProcesoOperativoId = EVProcesosOperativos.ProcesoOperativoSel.ProcesoOperativoId;
            base.MCargaFiltroPagYOrd(EVProcesosOperativos.ProcesoOperativoEstFiltro,
                                     EVProcesosOperativos.ProcesoOperativoEstPag,
                                     EVProcesosOperativos.ProcesoOperativoEstColOrden,
                                     nameof(EProcesoOperativoEst));

            EVProcesosOperativos.ProcesoOperativoEstPag = NProcesosOperativos.ProcesoOperativoEstPag(EVProcesosOperativos.ProcesoOperativoEstFiltro);
            base.MActualizaTamPag(EVProcesosOperativos.ProcesoOperativoEstPag?.DatPag);

            ViewBag.Mensajes = base.MObtenMensajes(NProcesosOperativos.Mensajes);
            ViewBag.Reglas = EVProcesosOperativos.ProcesoOperativoEstReglas;
            ViewBag.DatPag = EVProcesosOperativos.ProcesoOperativoEstPag?.DatPag;
            ViewBag.Orden = EVProcesosOperativos.ProcesoOperativoEstColOrden;
            ViewBag.Indice = EVProcesosOperativos.ProcesoOperativoEstIndice;

            return View(nameof(ProcesoOperativoEstCon), EVProcesosOperativos.ProcesoOperativoEstPag?.Pagina);
        }
        public IActionResult ProcesoOperativoEstXId(Int32 indice)
        {
            EVProcesosOperativos.Accion = MAccionesGen.Consulta;
            EVProcesosOperativos.ProcesoOperativoEstIndice = indice;
            return ProcesoOperativoEstCaptura(EVProcesosOperativos.ProcesoOperativoEstPag.Pagina[indice]);
        }
        [MValidaSeg(nameof(ProcesoOperativoEstInserta))]
        public IActionResult ProcesoOperativoEstInsertaIni()
        {
            EVProcesosOperativos.Accion = MAccionesGen.Inserta;
            return ProcesoOperativoEstInsertaCap(new EProcesoOperativoEst()
            {
                Activo = true
            });
        }
        [ValidateAntiForgeryToken]
        [MValidaSeg(nameof(ProcesoOperativoEstInserta))]
        public IActionResult ProcesoOperativoEstInsertaCap(EProcesoOperativoEst procesoOperativoEst)
        {
            return ProcesoOperativoEstCaptura(procesoOperativoEst);
        }
        [ValidateAntiForgeryToken]
        public IActionResult ProcesoOperativoEstInserta(EProcesoOperativoEst procesoOperativoEst)
        {
            procesoOperativoEst.ProcesoOperativoId = EVProcesosOperativos.ProcesoOperativoSel.ProcesoOperativoId; //Llave padre
            NProcesosOperativos.ProcesoOperativoEstInserta(procesoOperativoEst);
            if (NProcesosOperativos.Mensajes.Ok)
                return RedirectToAction(nameof(ProcesoOperativoEstCon));

            return ProcesoOperativoEstInsertaCap(procesoOperativoEst);
        }
        [MValidaSeg(nameof(ProcesoOperativoEstActualiza))]
        public IActionResult ProcesoOperativoEstActualizaIni(Int32 indice)
        {
            EVProcesosOperativos.Accion = MAccionesGen.Actualiza;
            EVProcesosOperativos.ProcesoOperativoEstIndice = indice;
            EVProcesosOperativos.ProcesoOperativoEstSel = EVProcesosOperativos.ProcesoOperativoEstPag.Pagina[indice];
            return ProcesoOperativoEstActualizaCap(EVProcesosOperativos.ProcesoOperativoEstSel);
        }
        [ValidateAntiForgeryToken]
        [MValidaSeg(nameof(ProcesoOperativoEstActualiza))]
        public IActionResult ProcesoOperativoEstActualizaCap(EProcesoOperativoEst procesoOperativoEst)
        {
            return ProcesoOperativoEstCaptura(procesoOperativoEst);
        }
        [ValidateAntiForgeryToken]
        public IActionResult ProcesoOperativoEstActualiza(EProcesoOperativoEst procesoOperativoEst)
        {
            procesoOperativoEst.ProcesoOperativoId = EVProcesosOperativos.ProcesoOperativoSel.ProcesoOperativoId; //Llave padre
            if (NProcesosOperativos.ProcesoOperativoEstActualiza(procesoOperativoEst))
                return RedirectToAction(nameof(ProcesoOperativoEstCon));

            return ProcesoOperativoEstActualizaCap(procesoOperativoEst);
        }
        public IActionResult ProcesoOperativoEstElimina(Int32 indice)
        {
            NProcesosOperativos.ProcesoOperativoEstElimina(EVProcesosOperativos.ProcesoOperativoEstPag.Pagina[indice]);
            base.MMensajesTemp = NProcesosOperativos.Mensajes.ToString();
            return RedirectToAction(nameof(ProcesoOperativoEstCon));
        }
        #endregion

        #region Funciones
        private IActionResult ProcesoOperativoEstCaptura(EProcesoOperativoEst procesoOperativoEst)
        {
            ViewBag.Mensajes = base.MObtenMensajes(NProcesosOperativos.Mensajes);
            ViewBag.Accion = EVProcesosOperativos.Accion;
            ViewBag.Reglas = EVProcesosOperativos.ProcesoOperativoEstReglas;

            return ViewCap(nameof(ProcesoOperativoEstCaptura), procesoOperativoEst);
        }
        #endregion

        #region Acciones de Paginacion Orden y Filtro
        [MValidaSeg(nameof(ProcesoOperativoEstInicia))]
        public IActionResult ProcesoOperativoEstPaginacion(MEDatosPaginador datPag)
        {
            EVProcesosOperativos.ProcesoOperativoEstPag.DatPag = datPag;
            return RedirectToAction(nameof(ProcesoOperativoEstCon));
        }
        [MValidaSeg(nameof(ProcesoOperativoEstInicia))]
        public IActionResult ProcesoOperativoEstOrdena(String orden)
        {
            EVProcesosOperativos.ProcesoOperativoEstColOrden = orden;
            return RedirectToAction(nameof(ProcesoOperativoEstCon));
        }
        #endregion

        #endregion

        #region ProcesoOperativoEstSec (ProcesosOperativosEstSec)

        #region Acciones
        public IActionResult ProcesoOperativoEstSecInicia(Int32 indice)
        {
            //Configuracion de inicio
            if (String.IsNullOrWhiteSpace(EVProcesosOperativos.ProcesoOperativoEstSecColOrden))
                EVProcesosOperativos.ProcesoOperativoEstSecColOrden = nameof(EProcesoOperativoEstSec.ProcesoOperativoEstSecId);

            if (EVProcesosOperativos.ProcesoOperativoEstSecReglas == null)
            {
                EVProcesosOperativos.ProcesoOperativoEstSecReglas = NProcesosOperativos.ProcesoOperativoEstSecReglas();
                base.MMensajesTemp = NProcesosOperativos.Mensajes.ToString();
            }

            if (indice >= 0)
            {
                EVProcesosOperativos.ProcesoOperativoEstIndice = indice;
                EVProcesosOperativos.ProcesoOperativoEstSel = EVProcesosOperativos.ProcesoOperativoEstPag.Pagina[indice];
            }

            return RedirectToAction(nameof(ProcesoOperativoEstSecCon));
        }
        [MValidaSeg(nameof(ProcesoOperativoEstSecInicia))]
        public IActionResult ProcesoOperativoEstSecCon()
        {
            EVProcesosOperativos.ProcesoOperativoEstSecFiltro.ProcesoOperativoEstId = EVProcesosOperativos.ProcesoOperativoEstSel.ProcesoOperativoEstId;
            base.MCargaFiltroPagYOrd(EVProcesosOperativos.ProcesoOperativoEstSecFiltro,
                                     EVProcesosOperativos.ProcesoOperativoEstSecPag,
                                     EVProcesosOperativos.ProcesoOperativoEstSecColOrden,
                                     nameof(EProcesoOperativoEstSec));

            EVProcesosOperativos.ProcesoOperativoEstSecPag = NProcesosOperativos.ProcesoOperativoEstSecPag(EVProcesosOperativos.ProcesoOperativoEstSecFiltro);
            base.MActualizaTamPag(EVProcesosOperativos.ProcesoOperativoEstSecPag?.DatPag);

            ViewBag.Mensajes = base.MObtenMensajes(NProcesosOperativos.Mensajes);
            ViewBag.Reglas = EVProcesosOperativos.ProcesoOperativoEstSecReglas;
            ViewBag.DatPag = EVProcesosOperativos.ProcesoOperativoEstSecPag?.DatPag;
            ViewBag.Orden = EVProcesosOperativos.ProcesoOperativoEstSecColOrden;
            ViewBag.Indice = EVProcesosOperativos.ProcesoOperativoEstSecIndice;

            return View(nameof(ProcesoOperativoEstSecCon), EVProcesosOperativos.ProcesoOperativoEstSecPag?.Pagina);
        }
        public IActionResult ProcesoOperativoEstSecXId(Int32 indice)
        {
            EVProcesosOperativos.Accion = MAccionesGen.Consulta;
            EVProcesosOperativos.ProcesoOperativoEstSecIndice = indice;
            return ProcesoOperativoEstSecCaptura(EVProcesosOperativos.ProcesoOperativoEstSecPag.Pagina[indice]);
        }
        [MValidaSeg(nameof(ProcesoOperativoEstSecInserta))]
        public IActionResult ProcesoOperativoEstSecInsertaIni()
        {
            EVProcesosOperativos.Accion = MAccionesGen.Inserta;
            return ProcesoOperativoEstSecInsertaCap(new EProcesoOperativoEstSec());
        }
        [ValidateAntiForgeryToken]
        [MValidaSeg(nameof(ProcesoOperativoEstSecInserta))]
        public IActionResult ProcesoOperativoEstSecInsertaCap(EProcesoOperativoEstSec procesoOperativoEstSec)
        {
            return ProcesoOperativoEstSecCaptura(procesoOperativoEstSec);
        }
        [ValidateAntiForgeryToken]
        public IActionResult ProcesoOperativoEstSecInserta(EProcesoOperativoEstSec procesoOperativoEstSec)
        {
            procesoOperativoEstSec.ProcesoOperativoEstId = EVProcesosOperativos.ProcesoOperativoEstSel.ProcesoOperativoEstId; //Llave padre
            NProcesosOperativos.ProcesoOperativoEstSecInserta(procesoOperativoEstSec);
            if (NProcesosOperativos.Mensajes.Ok)
                return RedirectToAction(nameof(ProcesoOperativoEstSecCon));

            return ProcesoOperativoEstSecInsertaCap(procesoOperativoEstSec);
        }
        [MValidaSeg(nameof(ProcesoOperativoEstSecActualiza))]
        public IActionResult ProcesoOperativoEstSecActualizaIni(Int32 indice)
        {
            EVProcesosOperativos.Accion = MAccionesGen.Actualiza;
            EVProcesosOperativos.ProcesoOperativoEstSecIndice = indice;
            EVProcesosOperativos.ProcesoOperativoEstSecSel = EVProcesosOperativos.ProcesoOperativoEstSecPag.Pagina[indice];
            return ProcesoOperativoEstSecActualizaCap(EVProcesosOperativos.ProcesoOperativoEstSecSel);
        }
        [ValidateAntiForgeryToken]
        [MValidaSeg(nameof(ProcesoOperativoEstSecActualiza))]
        public IActionResult ProcesoOperativoEstSecActualizaCap(EProcesoOperativoEstSec procesoOperativoEstSec)
        {
            return ProcesoOperativoEstSecCaptura(procesoOperativoEstSec);
        }
        [ValidateAntiForgeryToken]
        public IActionResult ProcesoOperativoEstSecActualiza(EProcesoOperativoEstSec procesoOperativoEstSec)
        {
            procesoOperativoEstSec.ProcesoOperativoEstId = EVProcesosOperativos.ProcesoOperativoEstSel.ProcesoOperativoEstId; //Llave padre
            if (NProcesosOperativos.ProcesoOperativoEstSecActualiza(procesoOperativoEstSec))
                return RedirectToAction(nameof(ProcesoOperativoEstSecCon));

            return ProcesoOperativoEstSecActualizaCap(procesoOperativoEstSec);
        }
        public IActionResult ProcesoOperativoEstSecElimina(Int32 indice)
        {
            NProcesosOperativos.ProcesoOperativoEstSecElimina(EVProcesosOperativos.ProcesoOperativoEstSecPag.Pagina[indice]);
            base.MMensajesTemp = NProcesosOperativos.Mensajes.ToString();
            return RedirectToAction(nameof(ProcesoOperativoEstSecCon));
        }
        #endregion

        #region Funciones
        private IActionResult ProcesoOperativoEstSecCaptura(EProcesoOperativoEstSec procesoOperativoEstSec)
        {
            ViewBag.Mensajes = base.MObtenMensajes(NProcesosOperativos.Mensajes);
            ViewBag.Accion = EVProcesosOperativos.Accion;
            ViewBag.Reglas = EVProcesosOperativos.ProcesoOperativoEstSecReglas;

            ViewBag.ProcesosOperativosEst = NCatalogos.ProcesoOperativoEstCmb(EVProcesosOperativos.ProcesoOperativoEstSel.ProcesoOperativoId);

            return ViewCap(nameof(ProcesoOperativoEstSecCaptura), procesoOperativoEstSec);
        }
        #endregion

        #region Acciones de Paginacion Orden y Filtro
        [MValidaSeg(nameof(ProcesoOperativoEstSecInicia))]
        public IActionResult ProcesoOperativoEstSecPaginacion(MEDatosPaginador datPag)
        {
            EVProcesosOperativos.ProcesoOperativoEstSecPag.DatPag = datPag;
            return RedirectToAction(nameof(ProcesoOperativoEstSecCon));
        }
        [MValidaSeg(nameof(ProcesoOperativoEstSecInicia))]
        public IActionResult ProcesoOperativoEstSecOrdena(String orden)
        {
            EVProcesosOperativos.ProcesoOperativoEstSecColOrden = orden;
            return RedirectToAction(nameof(ProcesoOperativoEstSecCon));
        }
        #endregion

        #endregion
    }
}
