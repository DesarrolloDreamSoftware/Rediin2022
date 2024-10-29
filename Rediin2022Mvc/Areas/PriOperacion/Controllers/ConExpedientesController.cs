using DSEntityNetX.Common.Casting;
using DSMetodNetX.Entidades;
using DSMetodNetX.Entidades.Correo;
using DSMetodNetX.Mvc;
using DSMetodNetX.Mvc.Seguridad;
//using GroupDocs.Viewer.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Org.BouncyCastle.Tls;
using Rediin2022.Aplicacion.PriOperacion;
using Rediin2022.Comun.PriOperacion;
using Rediin2022.Entidades.PriCatalogos;
using Rediin2022.Entidades.PriClientes;
using Rediin2022.Entidades.PriOperacion;
using Sisegui2020.Entidades.PriCatalogos;
using Sisegui2020.Entidades.PriSeguridad;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Rediin2022Mvc.Areas.PriOperacion.Controllers
{
    [Area("PriOperacion")]
    public class ConExpedientesController : MControllerMvcPri
    {
        #region Constructores
        public ConExpedientesController(INConExpedientes nConExpedientes,
                                        INProcesosOperativos nProcesosOperativos,
                                        ISENConExpedienteProv senConExpedienteProv,
                                        //Para catalogos
                                        INPaises nPaises,
                                        INBancos nBancos,
                                        INIdentificaciones nIdentificaciones,
                                        INSapSociedades nSapSociedades,
                                        INSapSociedadesGL nSapSociedadesGL,
                                        INSapGrupoCuentas nSapGrupoCuentas,
                                        INSapOrganizacionesCompra nSapOrganizacionesCompra,
                                        INSapTratamientos nSapTratamientos,
                                        INSapCuentasAsociadas nSapCuentasAsociadas,
                                        INSapGruposTesoreria nSapGruposTesoreria,
                                        INSapBancos nSapBancos,
                                        INSapCondicionesPago nSapCondicionesPago,
                                        INSapViasPago nSapViasPago,
                                        INSapGruposTolerancia nSapGruposTolerancia,
                                        INIncoterms nIncoterms,
                                        INRegimenesFiscales nRegimenesFiscales,
                                        INModelos nModelos,
                                        INMonedas nMonedas)
        {
            NConExpedientes = nConExpedientes;
            NProcesosOperativos = nProcesosOperativos;
            SENConExpedienteProv = senConExpedienteProv;

            //Para catalogos
            NPaises = nPaises;
            NBancos = nBancos;
            NIdentificaciones = nIdentificaciones;
            NSapSociedades = nSapSociedades;
            NSapSociedadesGL = nSapSociedadesGL;
            NSapGrupoCuentas = nSapGrupoCuentas;
            NSapOrganizacionesCompra = nSapOrganizacionesCompra;
            NSapTratamientos = nSapTratamientos;
            NSapCuentasAsociadas = nSapCuentasAsociadas;
            NSapGruposTesoreria = nSapGruposTesoreria;
            NSapBancos = nSapBancos;
            NSapCondicionesPago = nSapCondicionesPago;
            NSapViasPago = nSapViasPago;
            NSapGruposTolerancia = nSapGruposTolerancia;
            NIncoterms = nIncoterms;
            NRegimenesFiscales = nRegimenesFiscales;
            NModelos = nModelos;
            NMonedas = nMonedas;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (SENConExpedienteProv != null)
                SENConExpedienteProv.EV = EV;
            base.OnActionExecuting(context);
        }
        #endregion

        #region Propiedades
        private INConExpedientes NConExpedientes { get; set; }
        private INProcesosOperativos NProcesosOperativos { get; set; }
        private ISENConExpedienteProv SENConExpedienteProv { get; set; }

        private INPaises NPaises { get; set; } //Catalogos
        private INBancos NBancos { get; set; } //Catalogos
        private INIdentificaciones NIdentificaciones { get; set; } //Catalogos
        private INSapSociedades NSapSociedades { get; set; }
        private INSapSociedadesGL NSapSociedadesGL { get; set; } //Catalogos
        private INSapGrupoCuentas NSapGrupoCuentas { get; set; } //Catalogos
        private INSapOrganizacionesCompra NSapOrganizacionesCompra { get; set; } //Catalogos
        private INSapTratamientos NSapTratamientos { get; set; } //Catalogos
        private INSapCuentasAsociadas NSapCuentasAsociadas { get; set; } //Catalogos
        private INSapGruposTesoreria NSapGruposTesoreria { get; set; } //Catalogos
        private INSapBancos NSapBancos { get; set; } //Catalogos
        private INSapCondicionesPago NSapCondicionesPago { get; set; } //Catalogos
        private INSapViasPago NSapViasPago { get; set; } //Catalogos
        private INSapGruposTolerancia NSapGruposTolerancia { get; set; } //Catalogos
        private INIncoterms NIncoterms { get; set; } //Catalogos
        private INRegimenesFiscales NRegimenesFiscales { get; set; } //Catalogos
        private INModelos NModelos { get; set; } //Catalogos
        private INMonedas NMonedas { get; set; } //Catalogos
        private EVConExpedientes EV
        {
            get { return base.MEVCtrl<EVConExpedientes>(); }
        }
        #endregion

        #region ConExpProcOperativo (Enc)

        #region Acciones
        public async Task<IActionResult> ConExpProcOperativoInicia()
        {
            //Configuracion de inicio
            await Servicios.Gen.InicializaSF(EV.ConExpProcOperativo, nameof(EConExpProcOperativo.Orden));

            return RedirectToAction(nameof(ConExpProcOperativoCon));
        }
        [MValidaSeg(nameof(ConExpProcOperativoInicia))]
        public async Task<IActionResult> ConExpProcOperativoCon()
        {
            await Servicios.Pag.CargaPagOrdYFil(EV.ConExpProcOperativo);
            EV.ConExpProcOperativo.Pag = await NConExpedientes.ConExpProcOperativoPag(EV.ConExpProcOperativo.Filtro);
            await Servicios.Pag.ActTamPag(EV.ConExpProcOperativo);

            ViewBag.Mensajes = NConExpedientes.Mensajes;
            ViewBag.EV = EV;

            return View(nameof(ConExpProcOperativoCon), EV.ConExpProcOperativo.Pag?.Pagina);
        }
        #endregion

        #region Funciones
        #endregion

        #region Acciones de Paginacion Orden y Filtro
        [MValidaSeg(nameof(ConExpProcOperativoInicia))]
        public IActionResult ConExpProcOperativoPaginacion(MEDatosPaginador datPag)
        {
            EV.ConExpProcOperativo.Pag.DatPag = datPag;
            return RedirectToAction(nameof(ConExpProcOperativoCon));
        }
        [MValidaSeg(nameof(ConExpProcOperativoInicia))]
        public IActionResult ConExpProcOperativoOrdena(String orden)
        {
            EV.ConExpProcOperativo.ColOrden = orden;
            return RedirectToAction(nameof(ConExpProcOperativoCon));
        }
        [MValidaSeg(nameof(ConExpProcOperativoInicia))]
        public IActionResult ConExpProcOperativoFiltra(EConExpProcOperativoFiltro filtro)
        {
            EV.ConExpProcOperativo.Filtro = filtro;
            return RedirectToAction(nameof(ConExpProcOperativoCon));
        }
        [MValidaSeg(nameof(ConExpProcOperativoInicia))]
        public IActionResult ConExpProcOperativoLimpiaFiltros()
        {
            EV.ConExpProcOperativo.Filtro = new EConExpProcOperativoFiltro();
            return RedirectToAction(nameof(ConExpProcOperativoCon));
        }
        #endregion

        #endregion

        #region ConExpediente (Exp)

        #region Acciones
        public async Task<IActionResult> ConExpedienteInicia(Int32 indice)
        {
            //Configuracion de inicio
            //await Servicios.Gen.InicializaSF(EV.ConExpediente, nameof(EConExpediente.ExpedienteId),
            //    async () => await NConExpedientes.ConExpedienteReglas());
            await Servicios.Gen.InicializaSF(EV.ConExpediente, "-" + nameof(EConExpediente.ExpedienteId));

            Servicios.Gen.InicializaSFInd(EV.ConExpProcOperativo, indice);

            //Cargamos el listado de combos para seleccionar segun la columna
            Dictionary<Combos, List<MEElemento>> vCombos = new()
            {
                { Combos.Paises, await NPaises.PaisCmb() },
                { Combos.Estados, null },
                { Combos.Municipios, null },
                { Combos.Colonias, null },
                { Combos.Bancos, await NBancos.BancoCmb() },
                { Combos.Identificaciones, await NIdentificaciones.IdentificacionCmb() },
                { Combos.SAPSociedades, await NSapSociedades.SapSociedadCmb() },
                { Combos.SAPSociedadesGL, await NSapSociedadesGL.SapSociedadGLCmb() },
                { Combos.SapGrupoCuentas, await NSapGrupoCuentas.SapGrupoCuentaCmb() },
                { Combos.SapOrganizacionesCompra, await NSapOrganizacionesCompra.SapOrganizacionCompraCmb() },
                { Combos.SapTratamientos, await NSapTratamientos.SapTratamientoCmb() },
                { Combos.SapCuentasAsociadas, await NSapCuentasAsociadas.SapCuentaAsociadaCmb() },
                { Combos.SapGruposTesoreria, await NSapGruposTesoreria.SapGrupoTesoreriaCmb() },
                { Combos.SapBanco, await NSapBancos.SapBancoCmb() },
                { Combos.SapCondicionPago, await NSapCondicionesPago.SapCondicionPagoCmb() },
                { Combos.SapViaPago, await NSapViasPago.SapViaPagoCmb() },
                { Combos.SapGrupoTolerancia, await NSapGruposTolerancia.SapGrupoToleranciaCmb() },
                { Combos.Incoterms, await NIncoterms.IncotermCmb() },
                { Combos.RegimenesFiscales, await NRegimenesFiscales.RegimenFiscalCmb() },
                { Combos.Modelos, await NModelos.ModeloCmb() },
                { Combos.Monedas, await NMonedas.MonedaCmb() }
            };

            //Entidades adicionales
            EV.ProcOperColumnasCon =
                await NProcesosOperativos.ProcesoOperativoColCT(EV.ConExpProcOperativo.Sel.ProcesoOperativoId);

            //Ordenar columnas para la captura
            if (EV.ProcOperColumnasCon != null)
            {
                EV.ProcOperColumnasCap =
                    (from vCol in EV.ProcOperColumnasCon
                     where vCol.CapOrden > 0
                     orderby vCol.CapOrden
                     select vCol).ToList();
            }
            else
                EV.ProcOperColumnasCap = new List<EProcesoOperativoCol>();

            //Cargamos la informacion de los combos
            EV.ColumnaIdPais = null;
            EV.ColumnaIdEstado = null;
            EV.ColumnaIdMunicipio = null;
            EV.ColumnaIdColonias = null;

            if (EV.ProcOperColumnasCap != null && EV.ProcOperColumnasCap.Count > 0)
            {
                foreach (EProcesoOperativoCol vCol in EV.ProcOperColumnasCap)
                {
                    if (vCol.CapCmbProcesoOperativoId > 0) //JRD REVISAR PORQUE PARECE QUE ESTE CAMPO NO SE USA
                        vCol.ElementosCmb = await NConExpedientes.ConExpedienteCmb(vCol);
                    else if (vCol.ComboId > 0 && vCombos.ContainsKey(vCol.ComboId))
                    {
                        vCol.ElementosCmb = vCombos[vCol.ComboId];

                        if (EV.ColumnaIdPais == null && vCol.ComboId == Combos.Paises)
                            EV.ColumnaIdPais = vCol;
                        else if (EV.ColumnaIdEstado == null && vCol.ComboId == Combos.Estados)
                            EV.ColumnaIdEstado = vCol;
                        else if (EV.ColumnaIdMunicipio == null && vCol.ComboId == Combos.Municipios)
                            EV.ColumnaIdMunicipio = vCol;
                        else if (EV.ColumnaIdColonias == null && vCol.ComboId == Combos.Colonias)
                            EV.ColumnaIdColonias = vCol;
                    }
                }
            }

            //Proveedor especifico
            if (SENConExpedienteProv != null)
            {
                EV.ProcesoOperativoIdProveedor = await Servicios.ParamSist.Param<Int64>("RediinProveedorProcesoOperativoId");
                if (EV.ConExpProcOperativo.Sel.ProcesoOperativoId == EV.ProcesoOperativoIdProveedor)
                {
                    if (!(await SENConExpedienteProv.Inicia()))
                        return await ConExpProcOperativoCon();
                }
            }

            return RedirectToAction(nameof(ConExpedienteCon));
        }
        [MValidaSeg(nameof(ConExpedienteInicia))]
        public async Task<IActionResult> ConExpedienteCon()
        {
            EV.ConExpediente.Filtro.ProcesoOperativoId = EV.ConExpProcOperativo.Sel.ProcesoOperativoId;
            EV.ConExpediente.Filtro.ControlEstatus = EV.ConExpProcOperativo.Sel.ControlEstatus;

            await Servicios.Pag.CargaPagOrdYFil(EV.ConExpediente);

            //Adi
            EV.ConExpediente.Filtro.ColumnaId =
                XString.XToInt64(EV.ConExpediente.Filtro.ColumnaOrden);
            if (EV.ConExpediente.Filtro.ColumnaId < 0)
                EV.ConExpediente.Filtro.ColumnaId *= -1;
            if (EV.ConExpediente.Filtro.ColumnaId > 0)
                EV.ConExpediente.Filtro.Ascendente =
                    !EV.ConExpediente.Filtro.ColumnaOrden.StartsWith("-");

            EV.ConExpediente.Pag = await NConExpedientes.ConExpedientePag(EV.ConExpediente.Filtro);
            await Servicios.Pag.ActTamPag(EV.ConExpediente);

            ViewBag.Mensajes = NConExpedientes.Mensajes;
            ViewBag.EV = EV;

            ViewBag.ProcesosOperativosEst =
                 await NProcesosOperativos.ProcesoOperativoEstCmb(EV.ConExpProcOperativo.Sel.ProcesoOperativoId); //Mod

            //Adi
            ViewBag.ProcOperColumnas =
                (from vCol in EV.ProcOperColumnasCon
                 where vCol.ConOrden > 0
                 orderby vCol.ConOrden
                 select vCol).ToList();

            ViewBag.ControlEstatus = EV.ConExpProcOperativo.Sel.ControlEstatus;

            return View(nameof(ConExpedienteCon), EV.ConExpediente.Pag?.Pagina);
        }

        public async Task<IActionResult> ConExpedienteXId(Int32 indice)
        {
            EV.Accion = MAccionesGen.Consulta;
            EV.ConExpediente.Indice = indice;
            return await ConExpedienteCaptura(EV.ConExpediente.Pag.Pagina[indice]);
        }
        [MValidaSeg(nameof(ConExpedienteInserta))]
        public async Task<IActionResult> ConExpedienteInsertaIni()
        {
            EV.Accion = MAccionesGen.Inserta;
            return await ConExpedienteInsertaCap(new EConExpediente());
        }
        [ValidateAntiForgeryToken]
        [MValidaSeg(nameof(ConExpedienteInserta))]
        public async Task<IActionResult> ConExpedienteInsertaCap(EConExpediente conExpediente)
        {
            return await ConExpedienteCaptura(conExpediente);
        }
        [ValidateAntiForgeryToken]
        [MValidaSeg(nameof(ConExpedienteInserta))]
        public async Task<IActionResult> ConExpedienteInsertaCap2(IFormCollection conExp, Int64 PEMColumnaId)
        {
            EConExpediente conExpediente = ObtenExpediente(conExp);
            conExpediente.ProcesoOperativoId = EV.ConExpProcOperativo.Sel.ProcesoOperativoId; //Llave padre
            conExpediente.ControlEstatus = EV.ConExpProcOperativo.Sel.ControlEstatus;
            AjustaComboCascadaPEM(conExpediente, PEMColumnaId);
            //AjustaComboCascadaPEMProv(conExpediente, PEMColumnaId); //JRD QUITAR
            return await ConExpedienteInsertaCap(conExpediente);
        }
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConExpedienteInserta(IFormCollection conExp)
        {
            EConExpediente conExpediente = ObtenExpediente(conExp);
            conExpediente.ProcesoOperativoId = EV.ConExpProcOperativo.Sel.ProcesoOperativoId; //Llave padre
            conExpediente.ControlEstatus = EV.ConExpProcOperativo.Sel.ControlEstatus;
            //conExpediente.ProcesoOperativoEstId = 0L;

            if (SENConExpedienteProv != null &&
                EV.ConExpProcOperativo.Sel.ProcesoOperativoId == EV.ProcesoOperativoIdProveedor)
                await SENConExpedienteProv.Inserta(conExpediente); //Proveedor especifico
            else
                await NConExpedientes.ConExpedienteInserta(conExpediente);

            if (NConExpedientes.Mensajes.Ok)
                return RedirectToAction(nameof(ConExpedienteCon));

            return await ConExpedienteInsertaCap(conExpediente);
        }
        [MValidaSeg(nameof(ConExpedienteActualiza))]
        public async Task<IActionResult> ConExpedienteActualizaIni(Int32 indice)
        {
            EV.Accion = MAccionesGen.Actualiza;
            EV.ConExpediente.Indice = indice;
            EV.ConExpediente.Sel = EV.ConExpediente.Pag.Pagina[indice];
            return await ConExpedienteActualizaCap(EV.ConExpediente.Sel);
        }
        [ValidateAntiForgeryToken]
        [MValidaSeg(nameof(ConExpedienteActualiza))]
        public async Task<IActionResult> ConExpedienteActualizaCap(EConExpediente conExpediente)
        {
            return await ConExpedienteCaptura(conExpediente);
        }
        [ValidateAntiForgeryToken]
        [MValidaSeg(nameof(ConExpedienteActualiza))]
        public async Task<IActionResult> ConExpedienteActualizaCap2(IFormCollection conExp, Int64 PEMColumnaId)
        {
            EConExpediente conExpediente = ObtenExpediente(conExp);
            conExpediente.ProcesoOperativoId = EV.ConExpProcOperativo.Sel.ProcesoOperativoId; //Llave padre
            conExpediente.ProcesoOperativoEstId = EV.ConExpediente.Sel.ProcesoOperativoEstId;
            AjustaComboCascadaPEM(conExpediente, PEMColumnaId);
            //JRD QUITAR AjustaComboCascadaPEMProv(conExpediente, PEMColumnaId);
            return await ConExpedienteActualizaCap(conExpediente);
        }
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConExpedienteActualiza(IFormCollection conExp)
        {
            EConExpediente conExpediente = ObtenExpediente(conExp);
            conExpediente.ProcesoOperativoId = EV.ConExpProcOperativo.Sel.ProcesoOperativoId; //Llave padre
            conExpediente.ProcesoOperativoEstId = EV.ConExpediente.Sel.ProcesoOperativoEstId;
            if (await NConExpedientes.ConExpedienteActualiza(conExpediente))
                return RedirectToAction(nameof(ConExpedienteCon));

            return await ConExpedienteActualizaCap(conExpediente);
        }
        public async Task<IActionResult> ConExpedienteElimina(Int32 indice)
        {
            await NConExpedientes.ConExpedienteElimina(EV.ConExpediente.Pag.Pagina[indice]);
            return RedirectToAction(nameof(ConExpedienteCon));
        }
        /// <summary>
        /// Acción personalizada CambioEstatus.
        /// </summary>
        [MValidaSeg(nameof(ConExpedienteInicia))]
        public async Task<IActionResult> ConExpedienteCambioEstatusIni(Int32 indice, Int64 procesoOperativoEstIdSig)
        {
            EV.ConExpediente.Indice = indice;
            EV.ConExpediente.Sel = EV.ConExpediente.Pag.Pagina[indice];

            EConExpedienteCambioEstatus vConExpedienteCambioEstatus = new EConExpedienteCambioEstatus();
            vConExpedienteCambioEstatus.ProcesoOperativoEstId = procesoOperativoEstIdSig; //Adi
            vConExpedienteCambioEstatus.Comentarios = String.Empty;

            //Adi
            if (SENConExpedienteProv != null &&
                EV.ConExpProcOperativo.Sel.ProcesoOperativoId == EV.ProcesoOperativoIdProveedor &&
                SENConExpedienteProv.ValidaEstatus(procesoOperativoEstIdSig))
                return await ConExpedienteCambioEstatusCap(vConExpedienteCambioEstatus);
            else
                return await ConExpedienteCambioEstatus(vConExpedienteCambioEstatus);
        }
        /// <summary>
        /// Acción personalizada CambioEstatus.
        /// </summary>
        [ValidateAntiForgeryToken]
        [MValidaSeg(nameof(ConExpedienteInicia))]
        public async Task<IActionResult> ConExpedienteCambioEstatusCap(EConExpedienteCambioEstatus conExpedienteCambioEstatus)
        {
            ViewBag.CambioEstatus = true;
            //ViewBag.CAESMensajes = base.MObtenMensajes(NConExpedientes.Mensajes);
            ViewBag.CAESMensajes = NConExpedientes.Mensajes;
            ViewBag.ConExpedienteCambioEstatus = conExpedienteCambioEstatus;

            return await ConExpedienteCon();
        }

        /// <summary>
        /// Acción personalizada CambioEstatus.
        /// </summary>
        [MValidaSeg(nameof(ConExpedienteInicia))]
        public async Task<IActionResult> ConExpedienteCambioEstatus(EConExpedienteCambioEstatus conExpedienteCambioEstatus)
        {
            conExpedienteCambioEstatus.ExpedienteId = EV.ConExpediente.Sel.ExpedienteId;
            //Eli conExpedienteCambioEstatus.ProcesoOperativoEstId = EV.ConExpediente.Sel.ProcesoOperativoEstId;

            if (SENConExpedienteProv != null &&
                EV.ConExpProcOperativo.Sel.ProcesoOperativoId == EV.ProcesoOperativoIdProveedor)
            {
                if (!await SENConExpedienteProv.ValidaEstatusParaCambio(conExpedienteCambioEstatus))
                    return await ConExpedienteCon();
                    //return RedirectToAction(nameof(ConExpedienteCon));
            }

            if (await NConExpedientes.ConExpedienteCambioEstatus(conExpedienteCambioEstatus))
            {
                //Adi
                if (SENConExpedienteProv != null &&
                    EV.ConExpProcOperativo.Sel.ProcesoOperativoId == EV.ProcesoOperativoIdProveedor)
                    SENConExpedienteProv.CambioEstatus(conExpedienteCambioEstatus);

                return RedirectToAction(nameof(ConExpedienteCon));
            }

            //Adi
            if (SENConExpedienteProv != null &&
                EV.ConExpProcOperativo.Sel.ProcesoOperativoId == EV.ProcesoOperativoIdProveedor &&
                SENConExpedienteProv.ValidaEstatus(conExpedienteCambioEstatus.ProcesoOperativoEstId))
                return await ConExpedienteCambioEstatusCap(conExpedienteCambioEstatus);
            else
                return RedirectToAction(nameof(ConExpedienteCon));
        }
        #endregion

        #region Funciones
        private async Task<IActionResult> ConExpedienteCaptura(EConExpediente conExpediente)
        {
            ViewBag.Mensajes = NConExpedientes.Mensajes;
            ViewBag.EV = EV;

            conExpediente.ProcesoOperativoId = EV.ConExpProcOperativo.Sel.ProcesoOperativoId;

            if (EV.ColumnaIdPais != null)
            {
                if (EV.ColumnaIdEstado != null)
                    EV.ColumnaIdEstado.ElementosCmb = null;
                if (EV.ColumnaIdMunicipio != null)
                    EV.ColumnaIdMunicipio.ElementosCmb = null;
                if (EV.ColumnaIdColonias != null)
                    EV.ColumnaIdColonias.ElementosCmb = null;

                Int64 vPaisId = XObject.ToInt64(ObtenValor(conExpediente, EV.ColumnaIdPais.ColumnaId));
                if (vPaisId > 0 && EV.ColumnaIdEstado != null)
                {
                    EV.ColumnaIdEstado.ElementosCmb = await NPaises.EstadoCmb(vPaisId);
                    Int64 vEstadoId = XObject.ToInt64(ObtenValor(conExpediente, EV.ColumnaIdEstado.ColumnaId));
                    if (vEstadoId > 0 && EV.ColumnaIdMunicipio != null)
                    {
                        EV.ColumnaIdMunicipio.ElementosCmb = await NPaises.MunicipioCmb(vEstadoId);
                        Int64 vMunicipioId = XObject.ToInt64(ObtenValor(conExpediente, EV.ColumnaIdMunicipio.ColumnaId));
                        if (vMunicipioId > 0 && EV.ColumnaIdColonias != null)
                            EV.ColumnaIdColonias.ElementosCmb = await NPaises.ColoniaCmb(vMunicipioId);
                    }
                }
            }


            //if (EV.ConExpProcOperativo.Sel.ProcesoOperativoId == EV.ParamProveedorProcesoOperativoId)
            //{
            //    EV.CombosProveedores[EV.ParamProveedorColumnaIdEstado] = null;
            //    EV.CombosProveedores[EV.ParamProveedorColumnaIdMunicipio] = null;
            //    EV.CombosProveedores[EV.ParamProveedorColumnaIdColonia] = null;

            //    Int64 vPaisId = XObject.ToInt64(ObtenValor(conExpediente, EV.ParamProveedorColumnaIdPais));
            //    if (vPaisId > 0)
            //    {
            //        EV.CombosProveedores[EV.ParamProveedorColumnaIdEstado] =
            //            await NPaises.EstadoCmb(vPaisId);
            //        Int64 vEstadoId = XObject.ToInt64(ObtenValor(conExpediente, EV.ParamProveedorColumnaIdEstado));
            //        if (vEstadoId > 0)
            //        {
            //            EV.CombosProveedores[EV.ParamProveedorColumnaIdMunicipio] =
            //                await NPaises.MunicipioCmb(vEstadoId);
            //            Int64 vMunicipio = XObject.ToInt64(ObtenValor(conExpediente, EV.ParamProveedorColumnaIdMunicipio));
            //            if (vMunicipio > 0)
            //            {
            //                EV.CombosProveedores[EV.ParamProveedorColumnaIdColonia] =
            //                    await NPaises.ColoniaCmb(vMunicipio);
            //            }
            //        }
            //    }
            //}

            return ViewCap(nameof(ConExpedienteCaptura), conExpediente);
        }
        private void AjustaComboCascadaPEM(EConExpediente conExpediente, Int64 columnaId)
        {
            var vCol = EV.ProcOperColumnasCon.FirstOrDefault(x => x.ColumnaId == columnaId);
            if (vCol == null)
                return;

            if (vCol.ComboId == Combos.Paises)
            {
                foreach (var vVal in conExpediente.Valores)
                {
                    if (vVal.ComboId == Combos.Estados ||
                        vVal.ComboId == Combos.Municipios ||
                        vVal.ComboId == Combos.Colonias)
                        UtilExpediente.EstableceValor(vVal, TiposColumna.Entero, "0");
                }
            }
            else if (vCol.ComboId == Combos.Estados)
            {
                foreach (var vVal in conExpediente.Valores)
                {
                    if (vVal.ComboId == Combos.Municipios ||
                        vVal.ComboId == Combos.Colonias)
                        UtilExpediente.EstableceValor(vVal, TiposColumna.Entero, "0");
                }
            }
            else if (vCol.ComboId == Combos.Municipios)
            {
                foreach (var vVal in conExpediente.Valores)
                {
                    if (vVal.ComboId == Combos.Colonias)
                        UtilExpediente.EstableceValor(vVal, TiposColumna.Entero, "0");
                }
            }
        }
        //private void AjustaComboCascadaPEMProv(EConExpediente conExpediente, Int64 columnaId)
        //{
        //    if (columnaId == EV.ParamProveedorColumnaIdPais)
        //    {
        //        foreach (var vVal in conExpediente.Valores)
        //        {
        //            if (vVal.ColumnaId == EV.ParamProveedorColumnaIdEstado ||
        //               vVal.ColumnaId == EV.ParamProveedorColumnaIdMunicipio ||
        //               vVal.ColumnaId == EV.ParamProveedorColumnaIdColonia)
        //                EstableceValor(vVal, TiposColumna.Entero, "0");
        //        }
        //    }
        //    else if (columnaId == EV.ParamProveedorColumnaIdEstado)
        //    {
        //        foreach (var vVal in conExpediente.Valores)
        //        {
        //            if (vVal.ColumnaId == EV.ParamProveedorColumnaIdMunicipio ||
        //                vVal.ColumnaId == EV.ParamProveedorColumnaIdColonia)
        //                EstableceValor(vVal, TiposColumna.Entero, "0");
        //        }
        //    }
        //    else if (columnaId == EV.ParamProveedorColumnaIdMunicipio)
        //    {
        //        foreach (var vVal in conExpediente.Valores)
        //        {
        //            if (vVal.ColumnaId == EV.ParamProveedorColumnaIdColonia)
        //                EstableceValor(vVal, TiposColumna.Entero, "0");
        //        }
        //    }
        //}
        private Object ObtenValor(EConExpediente conExpediente, Int64 columnaId)
        {
            return UtilExpediente.ObtenValor(EV.ProcOperColumnasCon,
                                             conExpediente,
                                             columnaId);
        }
        public EConExpediente ObtenExpediente(IFormCollection conExp)
        {
            EConExpediente conExpediente = new EConExpediente();

            EConExpValores vVal = null;
            if (conExp.ContainsKey(nameof(EConExpediente.ExpedienteId)))
                conExpediente.ExpedienteId = XObject.ToInt64(conExp[nameof(EConExpediente.ExpedienteId)].ToString());
            foreach (EProcesoOperativoCol vCol in EV.ProcOperColumnasCon)
            {
                vVal = new EConExpValores();
                vVal.ExpedienteId = conExpediente.ExpedienteId;
                vVal.ColumnaId = vCol.ColumnaId;
                vVal.ComboId = vCol.ComboId;
                if (conExp.ContainsKey(vCol.ColumnaId.ToString()))
                    UtilExpediente.EstableceValor(vVal, vCol.Tipo, conExp[vCol.ColumnaId.ToString()]);
                conExpediente.Valores.Add(vVal);
            }

            return conExpediente;
        }
        #endregion

        #region Acciones de Paginacion Orden y Filtro
        [MValidaSeg(nameof(ConExpedienteInicia))]
        public IActionResult ConExpedientePaginacion(MEDatosPaginador datPag)
        {
            EV.ConExpediente.Pag.DatPag = datPag;
            return RedirectToAction(nameof(ConExpedienteCon));
        }
        [MValidaSeg(nameof(ConExpedienteInicia))]
        public async Task<IActionResult> ConExpedientePaginacionSigPag()
        {
            EV.ConExpediente.Pag.DatPag.CurrentPage += 1;
            return await Task.FromResult(RedirectToAction(nameof(ConExpedienteCon)));
        }
        [MValidaSeg(nameof(ConExpedienteInicia))]
        public async Task<IActionResult> ConExpedientePaginacionAntPag()
        {
            if (EV.ConExpediente.Pag.DatPag.CurrentPage > 1)
                EV.ConExpediente.Pag.DatPag.CurrentPage -= 1;

            return await Task.FromResult(RedirectToAction(nameof(ConExpedienteCon)));
        }

        [MValidaSeg(nameof(ConExpedienteInicia))]
        public IActionResult ConExpedienteOrdena(String orden)
        {
            EV.ConExpediente.ColOrden = orden;
            return RedirectToAction(nameof(ConExpedienteCon));
        }
        [MValidaSeg(nameof(ConExpedienteInicia))]
        public IActionResult ConExpedienteFiltra(EConExpedienteFiltro filtro)
        {
            EV.ConExpediente.Filtro = filtro;
            return RedirectToAction(nameof(ConExpedienteCon));
        }
        [MValidaSeg(nameof(ConExpedienteInicia))]
        public IActionResult ConExpedienteLimpiaFiltros()
        {
            EV.ConExpediente.Filtro = new EConExpedienteFiltro();
            return RedirectToAction(nameof(ConExpedienteCon));
        }
        [MValidaSeg(nameof(ConExpedienteInicia))]
        public IActionResult ConExpedienteSelCol(String selColGrupoId)
        {
            EV.ConExpedienteSelColGrupoId = selColGrupoId;
            return RedirectToAction(nameof(ConExpedienteCon));
        }
        #endregion

        #endregion

        #region ConExpedienteObjeto (Objs)

        #region Acciones
        public async Task<IActionResult> ConExpedienteObjetoInicia(Int32 indice)
        {
            //Configuracion de inicio
            await Servicios.Gen.InicializaSF(EV.ConExpedienteObjeto, nameof(EConExpedienteObjeto.ExpedienteObjetoId),
                async () => await NConExpedientes.ConExpedienteObjetoReglas());

            Servicios.Gen.InicializaSFInd(EV.ConExpediente, indice);

            //Por proveedor
            EV.TipoCapturaIdExpediente = 0;
            if (SENConExpedienteProv != null &&
                EV.ConExpProcOperativo.Sel.ProcesoOperativoId == EV.ProcesoOperativoIdProveedor)
                await SENConExpedienteProv.ValidaTipoCapturaXExpediente();

            return RedirectToAction(nameof(ConExpedienteObjetoCon));
        }
        [MValidaSeg(nameof(ConExpedienteObjetoInicia))]
        public async Task<IActionResult> ConExpedienteObjetoCon()
        {
            EV.ConExpedienteObjeto.Filtro.ExpedienteId = EV.ConExpediente.Sel.ExpedienteId;
            if(EV.TipoCapturaIdExpediente > 0)
                EV.ConExpedienteObjeto.Filtro.FilTipoCapturaId = EV.TipoCapturaIdExpediente;

            await Servicios.Pag.CargaPagOrdYFil(EV.ConExpedienteObjeto);
            EV.ConExpedienteObjeto.Pag = await NConExpedientes.ConExpedienteObjetoPag(EV.ConExpedienteObjeto.Filtro);
            await Servicios.Pag.ActTamPag(EV.ConExpedienteObjeto.Pag?.DatPag);

            ViewBag.Mensajes = NConExpedientes.Mensajes;
            ViewBag.EV = EV;

            return View(nameof(ConExpedienteObjetoCon), EV.ConExpedienteObjeto.Pag?.Pagina);
        }
        public async Task<IActionResult> ConExpedienteObjetoXId(Int32 indice)
        {
            EV.Accion = MAccionesGen.Consulta;
            EV.ConExpedienteObjeto.Indice = indice;
            return await ConExpedienteObjetoCaptura(EV.ConExpedienteObjeto.Pag.Pagina[indice]);
        }
        [MValidaSeg(nameof(ConExpedienteObjetoInserta))]
        public async Task<IActionResult> ConExpedienteObjetoInsertaIni()
        {
            EV.Accion = MAccionesGen.Inserta;
            return await ConExpedienteObjetoInsertaCap(new EConExpedienteObjeto()
            {
                Activo = true
            });
        }
        [ValidateAntiForgeryToken]
        [MValidaSeg(nameof(ConExpedienteObjetoInserta))]
        public async Task<IActionResult> ConExpedienteObjetoInsertaCap(EConExpedienteObjeto conExpedienteObjeto)
        {
            return await ConExpedienteObjetoCaptura(conExpedienteObjeto);
        }
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConExpedienteObjetoInserta(EConExpedienteObjeto conExpedienteObjeto,
                                                                    IFormFile archivoFisico)
        {
            //Adi
            if (archivoFisico == null)
            {
                NConExpedientes.Mensajes.AddError("No ha seleccionado un archivo.");
                return await ConExpedienteObjetoInsertaCap(conExpedienteObjeto);
            }

            //Subimos el archivo
            String vEntidad = "Expendientes";
            String vExtension = Path.GetExtension(archivoFisico.FileName);
            if (String.IsNullOrWhiteSpace(conExpedienteObjeto.ArchivoNombre))
                conExpedienteObjeto.ArchivoNombre = Path.GetFileName(archivoFisico.FileName);
            if (!conExpedienteObjeto.ArchivoNombre.EndsWith(vExtension))
                conExpedienteObjeto.ArchivoNombre += vExtension;

            String vRutaBase = MValorConfig<String>("DirBD");
            conExpedienteObjeto.Ruta =
                Path.Combine(vRutaBase,
                             vEntidad,
                             EV.ConExpediente.Sel.ExpedienteId.ToString());

            String vRutaYNombre = Path.Combine(conExpedienteObjeto.Ruta, conExpedienteObjeto.ArchivoNombre);

            if (!System.IO.Directory.Exists(Path.Combine(vRutaBase, vEntidad)))
                System.IO.Directory.CreateDirectory(Path.Combine(vRutaBase, vEntidad));

            if (!System.IO.Directory.Exists(conExpedienteObjeto.Ruta))
                System.IO.Directory.CreateDirectory(conExpedienteObjeto.Ruta);

            if (System.IO.File.Exists(vRutaYNombre))
            {
                NConExpedientes.Mensajes.AddError("EL nombre del archivo ya existe, no se puede insertar.");
                return await ConExpedienteObjetoInsertaCap(conExpedienteObjeto);
            }

            await Servicios.Archivos.SubeArchivo(new MEArchivo()
            {
                Archivo = MUtilMvc.MBytes(archivoFisico),
                Nombre = vRutaYNombre
            });
            //base.MRecibeArchivoDeCliente(NConExpedientes.Mensajes,
            //                             archivoFisico,
            //                             vRutaYNombre);

            if (!NConExpedientes.Mensajes.Ok)
                return await ConExpedienteObjetoInsertaCap(conExpedienteObjeto);
            //Fin Adi

            conExpedienteObjeto.ExpedienteId = EV.ConExpediente.Sel.ExpedienteId; //Llave padre
            await NConExpedientes.ConExpedienteObjetoInserta(conExpedienteObjeto);
            if (NConExpedientes.Mensajes.Ok)
            {
                return RedirectToAction(nameof(ConExpedienteObjetoCon));
            }

            return await ConExpedienteObjetoInsertaCap(conExpedienteObjeto);
        }
        public async Task<IActionResult> ConExpedienteObjetoElimina(Int32 indice)
        {
            await NConExpedientes.ConExpedienteObjetoElimina(EV.ConExpedienteObjeto.Pag.Pagina[indice]);
            //base.MMensajesTemp = NConExpedientes.Mensajes.ToString();
            return RedirectToAction(nameof(ConExpedienteObjetoCon));
        }
        /// <summary>
        /// Acción personalizada Descarga.
        /// </summary>
        public async Task<IActionResult> ConExpedienteObjetoDescarga(Int32 indice)
        {
            EConExpedienteObjeto vObj = EV.ConExpedienteObjeto.Pag.Pagina[indice];

            String vCont;
            if (vObj.ArchivoNombre.EndsWith("xlsb"))
                vCont = "application/vnd.ms-excel";
            else
                vCont = "application/pdf";

            using MemoryStream vMS = new MemoryStream();
            using FileStream vFS = new FileStream(Path.Combine(vObj.Ruta, vObj.ArchivoNombre), FileMode.Open);
            vFS.CopyTo(vMS);
            return await Task.FromResult(File(vMS.ToArray(), vCont, "Archivo" + Path.GetExtension(vObj.ArchivoNombre)));
        }
        //[MValidaSeg(nameof(ConExpedienteObjetoDescarga))]
        //public async Task<IActionResult> ConExpedienteObjetoDescarga2(Int32 indice)
        //{
        //    Int32 totalPaginas = 0;
        //    EConExpedienteObjeto vObj = EV.ConExpedienteObjeto.Pag.Pagina[indice];
        //    String imageFilesPath = Path.Combine(Path.Combine(vObj.Ruta, "temp"), "page-{0}.png");
        //    using GroupDocs.Viewer.Viewer v = new GroupDocs.Viewer.Viewer(Path.Combine(vObj.Ruta, vObj.ArchivoNombre));
        //    GroupDocs.Viewer.Results.ViewInfo i = v.GetViewInfo(GroupDocs.Viewer.Options.ViewInfoOptions.ForPngView(false));
        //    totalPaginas = i.Pages.Count;
        //    GroupDocs.Viewer.Options.PngViewOptions o = new PngViewOptions(imageFilesPath);
        //    v.View(o);
        //    return await Task.FromResult(new JsonResult(totalPaginas));
        //}
        [MValidaSeg(nameof(ConExpedienteObjetoDescarga))]
        public async Task<IActionResult> ConExpedienteObjetoDescargaImg(Int32 indice, Int32 pagina)
        {
            EConExpedienteObjeto vObj = EV.ConExpedienteObjeto.Pag.Pagina[indice];
            String vRutaImgTemp = Path.Combine(vObj.Ruta, "temp", $"page-{pagina}.png");
            using MemoryStream vMS = new MemoryStream();
            using FileStream vFS = new FileStream(vRutaImgTemp, FileMode.Open);
            vFS.CopyTo(vMS);
            return await Task.FromResult(File(vMS.ToArray(), "image/png"));
        }

        /// <summary>
        /// Acción personalizada SelArchivo.
        /// </summary>
        [MValidaSeg(nameof(ConExpedienteObjetoInicia))]
        public async Task<IActionResult> ConExpedienteObjetoSelArchivoIni(Int32 indice)
        {
            EV.ConExpedienteObjeto.Indice = indice;
            EV.ConExpedienteObjeto.Sel = EV.ConExpedienteObjeto.Pag.Pagina[indice];
            EConExpedienteObjetoSelArchivo vConExpedienteObjetoSelArchivo = new EConExpedienteObjetoSelArchivo();
            vConExpedienteObjetoSelArchivo.ExpedienteObjetoId = EV.ConExpedienteObjeto.Sel.ExpedienteObjetoId;
            vConExpedienteObjetoSelArchivo.ArchivoNombre = String.Empty;
            return await ConExpedienteObjetoSelArchivoCap(vConExpedienteObjetoSelArchivo);
        }
        /// <summary>
        /// Acción personalizada SelArchivo.
        /// </summary>
        [ValidateAntiForgeryToken]
        [MValidaSeg(nameof(ConExpedienteObjetoInicia))]
        public async Task<IActionResult> ConExpedienteObjetoSelArchivoCap(EConExpedienteObjetoSelArchivo conExpedienteObjetoSelArchivo)
        {
            ViewBag.SelArchivo = true;
            //ViewBag.SEARMensajes = base.MObtenMensajes(NConExpedientes.Mensajes);
            ViewBag.SEARMensajes = NConExpedientes.Mensajes;
            ViewBag.ConExpedienteObjetoSelArchivo = conExpedienteObjetoSelArchivo;

            return await ConExpedienteObjetoCon();
        }
        /// <summary>
        /// Acción personalizada SelArchivo.
        /// </summary>
        [ValidateAntiForgeryToken]
        [MValidaSeg(nameof(ConExpedienteObjetoInicia))]
        public async Task<IActionResult> ConExpedienteObjetoSelArchivo(EConExpedienteObjetoSelArchivo conExpedienteObjetoSelArchivo,
                                                                       IFormFile archivoFisico)
        {
            //Adi
            if (archivoFisico == null)
            {
                NConExpedientes.Mensajes.AddError("No ha seleccionado un archivo.");
                return await ConExpedienteObjetoSelArchivoCap(conExpedienteObjetoSelArchivo);
            }

            //Subimos el archivo
            String vEntidad = "Expendientes";
            String vExtension = Path.GetExtension(archivoFisico.FileName);
            if (String.IsNullOrWhiteSpace(conExpedienteObjetoSelArchivo.ArchivoNombre))
                conExpedienteObjetoSelArchivo.ArchivoNombre = Path.GetFileName(archivoFisico.FileName);
            if (!conExpedienteObjetoSelArchivo.ArchivoNombre.EndsWith(vExtension))
                conExpedienteObjetoSelArchivo.ArchivoNombre += vExtension;

            String vRutaBase = MValorConfig<String>("DirBD");
            conExpedienteObjetoSelArchivo.Ruta =
                Path.Combine(vRutaBase,
                             vEntidad,
                             EV.ConExpediente.Sel.ExpedienteId.ToString());

            String vRutaYNombre = Path.Combine(conExpedienteObjetoSelArchivo.Ruta, conExpedienteObjetoSelArchivo.ArchivoNombre);

            if (!System.IO.Directory.Exists(Path.Combine(vRutaBase, vEntidad)))
                System.IO.Directory.CreateDirectory(Path.Combine(vRutaBase, vEntidad));

            if (!System.IO.Directory.Exists(conExpedienteObjetoSelArchivo.Ruta))
                System.IO.Directory.CreateDirectory(conExpedienteObjetoSelArchivo.Ruta);

            if (System.IO.File.Exists(vRutaYNombre))
            {
                NConExpedientes.Mensajes.AddError("EL nombre del arhivo ya existe, no se puede insertar.");
                return await ConExpedienteObjetoSelArchivoCap(conExpedienteObjetoSelArchivo);
            }

            await Servicios.Archivos.SubeArchivo(new MEArchivo()
            {
                Archivo = MUtilMvc.MBytes(archivoFisico),
                Nombre = vRutaYNombre
            });
            //base.MRecibeArchivoDeCliente(NConExpedientes.Mensajes,
            //                             archivoFisico,
            //                             vRutaYNombre);

            if (!NConExpedientes.Mensajes.Ok)
                return await ConExpedienteObjetoSelArchivoCap(conExpedienteObjetoSelArchivo);
            //Fin Adi

            conExpedienteObjetoSelArchivo.ExpedienteId = EV.ConExpedienteObjeto.Sel.ExpedienteId;
            if (await NConExpedientes.ConExpedienteObjetoSelArchivo(conExpedienteObjetoSelArchivo))
            {
                return RedirectToAction(nameof(ConExpedienteObjetoCon));
            }

            return await ConExpedienteObjetoSelArchivoCap(conExpedienteObjetoSelArchivo);
        }
        #endregion

        #region Funciones
        private async Task<IActionResult> ConExpedienteObjetoCaptura(EConExpedienteObjeto conExpedienteObjeto)
        {
            ViewBag.Mensajes = NConExpedientes.Mensajes;
            ViewBag.EV = EV;

            ViewBag.ProcesosOperativosObjetos = await NProcesosOperativos.ProcesoOperativoObjetoCmb(EV.ConExpediente.Sel.ProcesoOperativoId);

            return await Task.FromResult(ViewCap(nameof(ConExpedienteObjetoCaptura), conExpedienteObjeto));
        }
        #endregion

        #region Acciones de Paginacion Orden y Filtro
        [MValidaSeg(nameof(ConExpedienteObjetoInicia))]
        public IActionResult ConExpedienteObjetoPaginacion(MEDatosPaginador datPag)
        {
            EV.ConExpedienteObjeto.Pag.DatPag = datPag;
            return RedirectToAction(nameof(ConExpedienteObjetoCon));
        }
        [MValidaSeg(nameof(ConExpedienteObjetoInicia))]
        public IActionResult ConExpedienteObjetoOrdena(String orden)
        {
            EV.ConExpedienteObjeto.ColOrden = orden;
            return RedirectToAction(nameof(ConExpedienteObjetoCon));
        }
        [MValidaSeg(nameof(ConExpedienteObjetoInicia))]
        public IActionResult ConExpedienteObjetoFiltra(EConExpedienteObjetoFiltro filtro)
        {
            EV.ConExpedienteObjeto.Filtro = filtro;
            return RedirectToAction(nameof(ConExpedienteObjetoCon));
        }
        [MValidaSeg(nameof(ConExpedienteObjetoInicia))]
        public IActionResult ConExpedienteObjetoLimpiaFiltros()
        {
            EV.ConExpedienteObjeto.Filtro = new EConExpedienteObjetoFiltro();
            return RedirectToAction(nameof(ConExpedienteObjetoCon));
        }
        #endregion

        #endregion

        #region ExpedienteEstatu (ExpeEsta)

        #region Acciones
        public async Task<IActionResult> ExpedienteEstatuInicia(Int32 indice)
        {
            //Configuracion de inicio
            await Servicios.Gen.InicializaSF(EV.ExpedienteEstatu, "-" + nameof(EExpedienteEstatu.FechaCreacion));

            Servicios.Gen.InicializaSFInd(EV.ConExpediente, indice);

            return RedirectToAction(nameof(ExpedienteEstatuCon));
        }
        [MValidaSeg(nameof(ExpedienteEstatuInicia))]
        public async Task<IActionResult> ExpedienteEstatuCon()
        {
            EV.ExpedienteEstatu.Filtro.ExpedienteId = EV.ConExpediente.Sel.ExpedienteId;

            await Servicios.Pag.CargaPagOrdYFil(EV.ExpedienteEstatu);
            EV.ExpedienteEstatu.Pag = await NConExpedientes.ExpedienteEstatuPag(EV.ExpedienteEstatu.Filtro);
            await Servicios.Pag.ActTamPag(EV.ExpedienteEstatu.Pag?.DatPag);

            ViewBag.Mensajes = NConExpedientes.Mensajes;
            ViewBag.EV = EV;

            return View(nameof(ExpedienteEstatuCon), EV.ExpedienteEstatu.Pag?.Pagina);
        }
        /// <summary>
        /// Consulta por id.
        /// </summary>
        [MValidaSeg(nameof(ExpedienteEstatuInicia))]
        public async Task<IActionResult> ExpedienteEstatuXId(Int32 indice)
        {
            EV.Accion = MAccionesGen.Consulta;
            EV.ExpedienteEstatu.Indice = indice;
            return await ExpedienteEstatuCaptura(EV.ExpedienteEstatu.Pag.Pagina[indice]);
        }
        #endregion

        #region Funciones
        /// <summary>
        /// Captura.
        /// </summary>
        private async Task<IActionResult> ExpedienteEstatuCaptura(EExpedienteEstatu expedienteEstatu)
        {
            ViewBag.Mensajes = NConExpedientes.Mensajes;
            ViewBag.EV = EV;

            return await Task.FromResult(ViewCap(nameof(ExpedienteEstatuCaptura), expedienteEstatu));
        }
        #endregion

        #region Acciones de Paginacion Orden y Filtro
        [MValidaSeg(nameof(ExpedienteEstatuInicia))]
        public IActionResult ExpedienteEstatuPaginacion(MEDatosPaginador datPag)
        {
            EV.ExpedienteEstatu.Pag.DatPag = datPag;
            return RedirectToAction(nameof(ExpedienteEstatuCon));
        }
        [MValidaSeg(nameof(ExpedienteEstatuInicia))]
        public IActionResult ExpedienteEstatuOrdena(String orden)
        {
            EV.ExpedienteEstatu.ColOrden = orden;
            return RedirectToAction(nameof(ExpedienteEstatuCon));
        }
        #endregion

        #endregion
    }
}
