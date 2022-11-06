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
using Rediin2022.Aplicacion.PriOperacion;
using Rediin2022.Entidades.Idioma;
using Rediin2022.Entidades.PriCatalogos;
using Rediin2022.Entidades.PriOperacion;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Rediin2022Web.Areas.PriOperacion.Controllers
{
    [Area("PriOperacion")]
    public class ConExpedientesController : MControllerMvcPri
    {
        #region Constructores
        public ConExpedientesController(INConExpedientes nConExpedientes,
                                        INProcesosOperativos nProcesosOperativos)
        {
            NConExpedientes = nConExpedientes;
            NProcesosOperativos = nProcesosOperativos;
        }
        #endregion

        #region Propiedades
        private INConExpedientes NConExpedientes { get; set; }
        private INProcesosOperativos NProcesosOperativos { get; set; }
        private EVConExpedientes EVConExpedientes
        {
            get
            {
                if (base.MSesion<EVConExpedientes>() == null)
                    base.MSesion(new EVConExpedientes());

                return base.MSesionAuto<EVConExpedientes>();
            }
        }
        #endregion

        #region ConExpProcOperativo (Enc)

        #region Acciones
        public IActionResult ConExpProcOperativoInicia()
        {
            //Configuracion de inicio
            if (String.IsNullOrWhiteSpace(EVConExpedientes.ConExpProcOperativoColOrden))
                EVConExpedientes.ConExpProcOperativoColOrden = nameof(EConExpProcOperativo.Orden);

            return RedirectToAction(nameof(ConExpProcOperativoCon));
        }
        [MValidaSeg(nameof(ConExpProcOperativoInicia))]
        public IActionResult ConExpProcOperativoCon()
        {
            base.MCargaFiltroPagYOrd(EVConExpedientes.ConExpProcOperativoFiltro,
                                     EVConExpedientes.ConExpProcOperativoPag,
                                     EVConExpedientes.ConExpProcOperativoColOrden,
                                     nameof(EConExpProcOperativo));

            EVConExpedientes.ConExpProcOperativoPag = NConExpedientes.ConExpProcOperativoPag(EVConExpedientes.ConExpProcOperativoFiltro);
            base.MActualizaTamPag(EVConExpedientes.ConExpProcOperativoPag?.DatPag);

            ViewBag.Mensajes = base.MObtenMensajes(NConExpedientes.Mensajes);
            ViewBag.DatPag = EVConExpedientes.ConExpProcOperativoPag?.DatPag;
            ViewBag.Orden = EVConExpedientes.ConExpProcOperativoColOrden;
            ViewBag.Filtro = EVConExpedientes.ConExpProcOperativoFiltro;
            ViewBag.Indice = EVConExpedientes.ConExpProcOperativoIndice;

            return View(nameof(ConExpProcOperativoCon), EVConExpedientes.ConExpProcOperativoPag?.Pagina);
        }
        #endregion

        #region Funciones
        #endregion

        #region Acciones de Paginacion Orden y Filtro
        [MValidaSeg(nameof(ConExpProcOperativoInicia))]
        public IActionResult ConExpProcOperativoPaginacion(MEDatosPaginador datPag)
        {
            EVConExpedientes.ConExpProcOperativoPag.DatPag = datPag;
            return RedirectToAction(nameof(ConExpProcOperativoCon));
        }
        [MValidaSeg(nameof(ConExpProcOperativoInicia))]
        public IActionResult ConExpProcOperativoOrdena(String orden)
        {
            EVConExpedientes.ConExpProcOperativoColOrden = orden;
            return RedirectToAction(nameof(ConExpProcOperativoCon));
        }
        [MValidaSeg(nameof(ConExpProcOperativoInicia))]
        public IActionResult ConExpProcOperativoFiltra(EConExpProcOperativoFiltro filtro)
        {
            EVConExpedientes.ConExpProcOperativoFiltro = filtro;
            return RedirectToAction(nameof(ConExpProcOperativoCon));
        }
        [MValidaSeg(nameof(ConExpProcOperativoInicia))]
        public IActionResult ConExpProcOperativoLimpiaFiltros()
        {
            EVConExpedientes.ConExpProcOperativoFiltro = new EConExpProcOperativoFiltro();
            return RedirectToAction(nameof(ConExpProcOperativoCon));
        }
        #endregion

        #endregion

        #region ConExpediente (Exp)

        #region Acciones
        public IActionResult ConExpedienteInicia(Int32 indice)
        {
            //Configuracion de inicio
            if (String.IsNullOrWhiteSpace(EVConExpedientes.ConExpedienteColOrden))
                EVConExpedientes.ConExpedienteColOrden = nameof(EConExpediente.ExpedienteId);

            if (EVConExpedientes.ConExpedienteReglas == null)
            {
                EVConExpedientes.ConExpedienteReglas = NConExpedientes.ConExpedienteReglas();
                base.MMensajesTemp = NConExpedientes.Mensajes.ToString();
            }

            if (indice >= 0)
            {
                EVConExpedientes.ConExpProcOperativoIndice = indice;
                EVConExpedientes.ConExpProcOperativoSel = EVConExpedientes.ConExpProcOperativoPag.Pagina[indice];
            }

            //Entidades adicionales
            EVConExpedientes.ProcOperColumnas = NProcesosOperativos.ProcesoOperativoColCT(procesoOperativoCol.ProcesoOperativoId);

            return RedirectToAction(nameof(ConExpedienteCon));
        }
        [MValidaSeg(nameof(ConExpedienteInicia))]
        public IActionResult ConExpedienteCon()
        {
            EVConExpedientes.ConExpedienteFiltro.ProcesoOperativoId = EVConExpedientes.ConExpProcOperativoSel.ProcesoOperativoId;
            EVConExpedientes.ConExpedienteFiltro.ControlEstatus = EVConExpedientes.ConExpProcOperativoSel.ControlEstatus;
            base.MCargaFiltroPagYOrd(EVConExpedientes.ConExpedienteFiltro,
                                     EVConExpedientes.ConExpedientePag,
                                     EVConExpedientes.ConExpedienteColOrden,
                                     nameof(EConExpediente));

            EVConExpedientes.ConExpedientePag = NConExpedientes.ConExpedientePag(EVConExpedientes.ConExpedienteFiltro);
            base.MActualizaTamPag(EVConExpedientes.ConExpedientePag?.DatPag);

            ViewBag.Mensajes = base.MObtenMensajes(NConExpedientes.Mensajes);
            ViewBag.Reglas = EVConExpedientes.ConExpedienteReglas;
            ViewBag.DatPag = EVConExpedientes.ConExpedientePag?.DatPag;
            ViewBag.Orden = EVConExpedientes.ConExpedienteColOrden;
            ViewBag.Filtro = EVConExpedientes.ConExpedienteFiltro;
            ViewBag.Indice = EVConExpedientes.ConExpedienteIndice;

            ViewBag.ProcesosOperativosEst = NProcesosOperativos.ProcesoOperativoEstCmb(EVConExpedientes.ProcOperColumnas.ProcesoOperativoId);

            return View(nameof(ConExpedienteCon), EVConExpedientes.ConExpedientePag?.Pagina);
        }
        public IActionResult ConExpedienteXId(Int32 indice)
        {
            EVConExpedientes.Accion = MAccionesGen.Consulta;
            EVConExpedientes.ConExpedienteIndice = indice;
            return ConExpedienteCaptura(EVConExpedientes.ConExpedientePag.Pagina[indice]);
        }
        [MValidaSeg(nameof(ConExpedienteInserta))]
        public IActionResult ConExpedienteInsertaIni()
        {
            EVConExpedientes.Accion = MAccionesGen.Inserta;
            return ConExpedienteInsertaCap(new EConExpediente());
        }
        [ValidateAntiForgeryToken]
        [MValidaSeg(nameof(ConExpedienteInserta))]
        public IActionResult ConExpedienteInsertaCap(EConExpediente conExpediente)
        {
            return ConExpedienteCaptura(conExpediente);
        }
        [ValidateAntiForgeryToken]
        public IActionResult ConExpedienteInserta(EConExpediente conExpediente)
        {
            conExpediente.ProcesoOperativoId = EVConExpedientes.ConExpProcOperativoSel.ProcesoOperativoId; //Llave padre
            conExpediente.ProcesoOperativoEstId = 0L; //PENDIENTE: Este campo se actualiza pero no se captura ni es llave padre
            NConExpedientes.ConExpedienteInserta(conExpediente);
            if (NConExpedientes.Mensajes.Ok)
                return RedirectToAction(nameof(ConExpedienteCon));

            return ConExpedienteInsertaCap(conExpediente);
        }
        [MValidaSeg(nameof(ConExpedienteActualiza))]
        public IActionResult ConExpedienteActualizaIni(Int32 indice)
        {
            EVConExpedientes.Accion = MAccionesGen.Actualiza;
            EVConExpedientes.ConExpedienteIndice = indice;
            EVConExpedientes.ConExpedienteSel = EVConExpedientes.ConExpedientePag.Pagina[indice];
            return ConExpedienteActualizaCap(EVConExpedientes.ConExpedienteSel);
        }
        [ValidateAntiForgeryToken]
        [MValidaSeg(nameof(ConExpedienteActualiza))]
        public IActionResult ConExpedienteActualizaCap(EConExpediente conExpediente)
        {
            return ConExpedienteCaptura(conExpediente);
        }
        [ValidateAntiForgeryToken]
        public IActionResult ConExpedienteActualiza(EConExpediente conExpediente)
        {
            conExpediente.ProcesoOperativoId = EVConExpedientes.ConExpProcOperativoSel.ProcesoOperativoId; //Llave padre
            conExpediente.ProcesoOperativoEstId = EVConExpedientes.ConExpedienteSel.ProcesoOperativoEstId;
            if (NConExpedientes.ConExpedienteActualiza(conExpediente))
                return RedirectToAction(nameof(ConExpedienteCon));

            return ConExpedienteActualizaCap(conExpediente);
        }
        public IActionResult ConExpedienteElimina(Int32 indice)
        {
            NConExpedientes.ConExpedienteElimina(EVConExpedientes.ConExpedientePag.Pagina[indice]);
            base.MMensajesTemp = NConExpedientes.Mensajes.ToString();
            return RedirectToAction(nameof(ConExpedienteCon));
        }
        /// <summary>
        /// Acción personalizada CambioEstatusUno.
        /// </summary>
        public IActionResult ConExpedienteCambioEstatusUno(Int32 indice)
        {
            EConExpedienteCambioEstatusUno vConExpedienteCambioEstatusUno = new EConExpedienteCambioEstatusUno();
            vConExpedienteCambioEstatusUno.ExpedienteId = EVConExpedientes.ConExpedientePag.Pagina[indice].ExpedienteId;
            vConExpedienteCambioEstatusUno.ProcesoOperativoEstId = EVConExpedientes.ConExpedientePag.Pagina[indice].ProcesoOperativoEstId;
            NConExpedientes.ConExpedienteCambioEstatusUno(vConExpedienteCambioEstatusUno);
            base.MMensajesTemp = NConExpedientes.Mensajes.ToString();
            return RedirectToAction(nameof(ConExpedienteCon));
        }
        /// <summary>
        /// Acción personalizada CambioEstatusDos.
        /// </summary>
        public IActionResult ConExpedienteCambioEstatusDos(Int32 indice)
        {
            EConExpedienteCambioEstatusDos vConExpedienteCambioEstatusDos = new EConExpedienteCambioEstatusDos();
            vConExpedienteCambioEstatusDos.ExpedienteId = EVConExpedientes.ConExpedientePag.Pagina[indice].ExpedienteId;
            vConExpedienteCambioEstatusDos.ProcesoOperativoEstId = EVConExpedientes.ConExpedientePag.Pagina[indice].ProcesoOperativoEstId;
            NConExpedientes.ConExpedienteCambioEstatusDos(vConExpedienteCambioEstatusDos);
            base.MMensajesTemp = NConExpedientes.Mensajes.ToString();
            return RedirectToAction(nameof(ConExpedienteCon));
        }
        #endregion

        #region Funciones
        private IActionResult ConExpedienteCaptura(EConExpediente conExpediente)
        {
            ViewBag.Mensajes = base.MObtenMensajes(NConExpedientes.Mensajes);
            ViewBag.Accion = EVConExpedientes.Accion;
            ViewBag.Reglas = EVConExpedientes.ConExpedienteReglas;

            return ViewCap(nameof(ConExpedienteCaptura), conExpediente);
        }
        #endregion

        #region Acciones de Paginacion Orden y Filtro
        [MValidaSeg(nameof(ConExpedienteInicia))]
        public IActionResult ConExpedientePaginacion(MEDatosPaginador datPag)
        {
            EVConExpedientes.ConExpedientePag.DatPag = datPag;
            return RedirectToAction(nameof(ConExpedienteCon));
        }
        [MValidaSeg(nameof(ConExpedienteInicia))]
        public IActionResult ConExpedienteOrdena(String orden)
        {
            EVConExpedientes.ConExpedienteColOrden = orden;
            return RedirectToAction(nameof(ConExpedienteCon));
        }
        [MValidaSeg(nameof(ConExpedienteInicia))]
        public IActionResult ConExpedienteFiltra(EConExpedienteFiltro filtro)
        {
            EVConExpedientes.ConExpedienteFiltro = filtro;
            return RedirectToAction(nameof(ConExpedienteCon));
        }
        [MValidaSeg(nameof(ConExpedienteInicia))]
        public IActionResult ConExpedienteLimpiaFiltros()
        {
            EVConExpedientes.ConExpedienteFiltro = new EConExpedienteFiltro();
            return RedirectToAction(nameof(ConExpedienteCon));
        }
        #endregion

        #endregion

        #region ConExpedienteObjeto (Objs)

        #region Acciones
        public IActionResult ConExpedienteObjetoInicia(Int32 indice)
        {
            //Configuracion de inicio
            if (String.IsNullOrWhiteSpace(EVConExpedientes.ConExpedienteObjetoColOrden))
                EVConExpedientes.ConExpedienteObjetoColOrden = nameof(EConExpedienteObjeto.Orden);

            if (EVConExpedientes.ConExpedienteObjetoReglas == null)
            {
                EVConExpedientes.ConExpedienteObjetoReglas = NConExpedientes.ConExpedienteObjetoReglas();
                base.MMensajesTemp = NConExpedientes.Mensajes.ToString();
            }

            if (indice >= 0)
            {
                EVConExpedientes.ConExpedienteIndice = indice;
                EVConExpedientes.ConExpedienteSel = EVConExpedientes.ConExpedientePag.Pagina[indice];
            }

            return RedirectToAction(nameof(ConExpedienteObjetoCon));
        }
        [MValidaSeg(nameof(ConExpedienteObjetoInicia))]
        public IActionResult ConExpedienteObjetoCon()
        {
            EVConExpedientes.ConExpedienteObjetoFiltro.ExpedienteId = EVConExpedientes.ConExpedienteSel.ExpedienteId;
            base.MCargaFiltroPagYOrd(EVConExpedientes.ConExpedienteObjetoFiltro,
                                     EVConExpedientes.ConExpedienteObjetoPag,
                                     EVConExpedientes.ConExpedienteObjetoColOrden,
                                     nameof(EConExpedienteObjeto));

            EVConExpedientes.ConExpedienteObjetoPag = NConExpedientes.ConExpedienteObjetoPag(EVConExpedientes.ConExpedienteObjetoFiltro);
            base.MActualizaTamPag(EVConExpedientes.ConExpedienteObjetoPag?.DatPag);

            ViewBag.Mensajes = base.MObtenMensajes(NConExpedientes.Mensajes);
            ViewBag.Reglas = EVConExpedientes.ConExpedienteObjetoReglas;
            ViewBag.DatPag = EVConExpedientes.ConExpedienteObjetoPag?.DatPag;
            ViewBag.Orden = EVConExpedientes.ConExpedienteObjetoColOrden;
            ViewBag.Indice = EVConExpedientes.ConExpedienteObjetoIndice;

            return View(nameof(ConExpedienteObjetoCon), EVConExpedientes.ConExpedienteObjetoPag?.Pagina);
        }
        public IActionResult ConExpedienteObjetoXId(Int32 indice)
        {
            EVConExpedientes.Accion = MAccionesGen.Consulta;
            EVConExpedientes.ConExpedienteObjetoIndice = indice;
            return ConExpedienteObjetoCaptura(EVConExpedientes.ConExpedienteObjetoPag.Pagina[indice]);
        }
        [MValidaSeg(nameof(ConExpedienteObjetoInserta))]
        public IActionResult ConExpedienteObjetoInsertaIni()
        {
            EVConExpedientes.Accion = MAccionesGen.Inserta;
            return ConExpedienteObjetoInsertaCap(new EConExpedienteObjeto());
        }
        [ValidateAntiForgeryToken]
        [MValidaSeg(nameof(ConExpedienteObjetoInserta))]
        public IActionResult ConExpedienteObjetoInsertaCap(EConExpedienteObjeto conExpedienteObjeto)
        {
            return ConExpedienteObjetoCaptura(conExpedienteObjeto);
        }
        [ValidateAntiForgeryToken]
        public IActionResult ConExpedienteObjetoInserta(EConExpedienteObjeto conExpedienteObjeto)
        {
            conExpedienteObjeto.ExpedienteId = EVConExpedientes.ConExpedienteSel.ExpedienteId; //Llave padre
            conExpedienteObjeto.Ruta = String.Empty; //PENDIENTE: Este campo se actualiza pero no se captura ni es llave padre
            conExpedienteObjeto.Activo = EVConExpedientes.ConExpProcOperativoSel.Activo;
            NConExpedientes.ConExpedienteObjetoInserta(conExpedienteObjeto);
            if (NConExpedientes.Mensajes.Ok)
                return RedirectToAction(nameof(ConExpedienteObjetoCon));

            return ConExpedienteObjetoInsertaCap(conExpedienteObjeto);
        }
        public IActionResult ConExpedienteObjetoElimina(Int32 indice)
        {
            NConExpedientes.ConExpedienteObjetoElimina(EVConExpedientes.ConExpedienteObjetoPag.Pagina[indice]);
            base.MMensajesTemp = NConExpedientes.Mensajes.ToString();
            return RedirectToAction(nameof(ConExpedienteObjetoCon));
        }
        /// <summary>
        /// Acción personalizada AgregarArchivo.
        /// </summary>
        public IActionResult ConExpedienteObjetoAgregarArchivo(Int32 indice)
        {
            NConExpedientes.ConExpedienteObjetoAgregarArchivo();
            base.MMensajesTemp = NConExpedientes.Mensajes.ToString();
            return RedirectToAction(nameof(ConExpedienteObjetoCon));
        }
        #endregion

        #region Funciones
        private IActionResult ConExpedienteObjetoCaptura(EConExpedienteObjeto conExpedienteObjeto)
        {
            ViewBag.Mensajes = base.MObtenMensajes(NConExpedientes.Mensajes);
            ViewBag.Accion = EVConExpedientes.Accion;
            ViewBag.Reglas = EVConExpedientes.ConExpedienteObjetoReglas;

            ViewBag.ProcesosOperativosObjetos = NProcesosOperativos.ProcesoOperativoObjetoCmb(EVConExpedientes.ConExpedienteSel.ProcesoOperativoId);

            return ViewCap(nameof(ConExpedienteObjetoCaptura), conExpedienteObjeto);
        }
        #endregion

        #region Acciones de Paginacion Orden y Filtro
        [MValidaSeg(nameof(ConExpedienteObjetoInicia))]
        public IActionResult ConExpedienteObjetoPaginacion(MEDatosPaginador datPag)
        {
            EVConExpedientes.ConExpedienteObjetoPag.DatPag = datPag;
            return RedirectToAction(nameof(ConExpedienteObjetoCon));
        }
        [MValidaSeg(nameof(ConExpedienteObjetoInicia))]
        public IActionResult ConExpedienteObjetoOrdena(String orden)
        {
            EVConExpedientes.ConExpedienteObjetoColOrden = orden;
            return RedirectToAction(nameof(ConExpedienteObjetoCon));
        }
        #endregion

        #endregion

        #region ExpedienteEstatu (ExpeEsta)

        #region Acciones
        public IActionResult ExpedienteEstatuInicia(Int32 indice)
        {
            //Configuracion de inicio
            if (String.IsNullOrWhiteSpace(EVConExpedientes.ExpedienteEstatuColOrden))
                EVConExpedientes.ExpedienteEstatuColOrden = "-" + nameof(EExpedienteEstatu.FechaCreacion);

            if (indice >= 0)
            {
                EVConExpedientes.ConExpedienteIndice = indice;
                EVConExpedientes.ConExpedienteSel = EVConExpedientes.ConExpedientePag.Pagina[indice];
            }

            return RedirectToAction(nameof(ExpedienteEstatuCon));
        }
        [MValidaSeg(nameof(ExpedienteEstatuInicia))]
        public IActionResult ExpedienteEstatuCon()
        {
            EVConExpedientes.ExpedienteEstatuFiltro.ExpedienteId = EVConExpedientes.ConExpedienteSel.ExpedienteId;
            base.MCargaFiltroPagYOrd(EVConExpedientes.ExpedienteEstatuFiltro,
                                     EVConExpedientes.ExpedienteEstatuPag,
                                     EVConExpedientes.ExpedienteEstatuColOrden,
                                     nameof(EExpedienteEstatu));

            EVConExpedientes.ExpedienteEstatuPag = NConExpedientes.ExpedienteEstatuPag(EVConExpedientes.ExpedienteEstatuFiltro);
            base.MActualizaTamPag(EVConExpedientes.ExpedienteEstatuPag?.DatPag);

            ViewBag.Mensajes = base.MObtenMensajes(NConExpedientes.Mensajes);
            ViewBag.DatPag = EVConExpedientes.ExpedienteEstatuPag?.DatPag;
            ViewBag.Orden = EVConExpedientes.ExpedienteEstatuColOrden;
            ViewBag.Indice = EVConExpedientes.ExpedienteEstatuIndice;

            return View(nameof(ExpedienteEstatuCon), EVConExpedientes.ExpedienteEstatuPag?.Pagina);
        }
        #endregion

        #region Funciones
        private IActionResult ExpedienteEstatuCaptura(EExpedienteEstatu expedienteEstatu)
        {
            ViewBag.Mensajes = base.MObtenMensajes(NConExpedientes.Mensajes);
            ViewBag.Accion = EVConExpedientes.Accion;

            ViewBag.ProcesosOperativosEst = NProcesosOperativos.ProcesoOperativoEstCmb(EVConExpedientes.ConExpedienteSel.ProcesoOperativoId);

            return ViewCap(nameof(ExpedienteEstatuCaptura), expedienteEstatu);
        }
        #endregion

        #region Acciones de Paginacion Orden y Filtro
        [MValidaSeg(nameof(ExpedienteEstatuInicia))]
        public IActionResult ExpedienteEstatuPaginacion(MEDatosPaginador datPag)
        {
            EVConExpedientes.ExpedienteEstatuPag.DatPag = datPag;
            return RedirectToAction(nameof(ExpedienteEstatuCon));
        }
        [MValidaSeg(nameof(ExpedienteEstatuInicia))]
        public IActionResult ExpedienteEstatuOrdena(String orden)
        {
            EVConExpedientes.ExpedienteEstatuColOrden = orden;
            return RedirectToAction(nameof(ExpedienteEstatuCon));
        }
        #endregion

        #endregion
    }
}
