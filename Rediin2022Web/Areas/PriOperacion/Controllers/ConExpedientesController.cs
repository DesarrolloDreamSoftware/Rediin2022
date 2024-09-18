using DSEntityNetX.Common.Casting;
using DSEntityNetX.Common.File;
using DSEntityNetX.Common.Pagination;
using DSEntityNetX.Common.Security;
using DSEntityNetX.Mvc;
using DSMetodNetX.Aplicacion;
using DSMetodNetX.Entidades;
using DSMetodNetX.Entidades.Correo;
using DSMetodNetX.Mvc.Seguridad;
using GroupDocs.Viewer.Options;
using MailKit.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MimeKit;
using Rediin2022.Aplicacion.PriCatalogos;
using Rediin2022.Aplicacion.PriOperacion;
using Rediin2022.Entidades.Idioma;
using Rediin2022.Entidades.PriCatalogos;
using Rediin2022.Entidades.PriClientes;
using Rediin2022.Entidades.PriOperacion;
using Sisegui2020.Aplicacion.PriSeguridad;
using Sisegui2020.Entidades.Idioma;
using Sisegui2020.Entidades.PriCatalogos;
using Sisegui2020.Entidades.PriSeguridad;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Rediin2022Web.Areas.PriOperacion.Controllers
{
    [Area("PriOperacion")]
    public class ConExpedientesController : MControllerMvcPri
    {
        #region Constructores
        public ConExpedientesController(INConExpedientes nConExpedientes,
                                        INProcesosOperativos nProcesosOperativos,
                                        INUsuarios nUsuarios,
                                        INExpedientes nExpedientes,
                                        //Para proveedores
                                        INPaises nPaises,
                                        INBancos nBancos,
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
                                        INSapGruposTolerancia nSapGruposTolerancia)
        {
            NConExpedientes = nConExpedientes;
            NProcesosOperativos = nProcesosOperativos;
            NUsuarios = nUsuarios;
            NExpedientes = nExpedientes;

            //Para proveedores
            NPaises = nPaises;
            NBancos = nBancos;
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
        }
        #endregion

        #region Propiedades
        private INConExpedientes NConExpedientes { get; set; }
        private INProcesosOperativos NProcesosOperativos { get; set; }
        private INUsuarios NUsuarios { get; set; }
        private INExpedientes NExpedientes { get; set; }
        private INPaises NPaises { get; set; } //Proveedores
        private INBancos NBancos { get; set; } //Proveedores

        private INSapSociedades NSapSociedades { get; set; } //Proveedores
        private INSapSociedadesGL NSapSociedadesGL { get; set; } //Proveedores
        private INSapGrupoCuentas NSapGrupoCuentas { get; set; } //Proveedores
        private INSapOrganizacionesCompra NSapOrganizacionesCompra { get; set; } //Proveedores
        private INSapTratamientos NSapTratamientos { get; set; } //Proveedores
        private INSapCuentasAsociadas NSapCuentasAsociadas { get; set; } //Proveedores
        private INSapGruposTesoreria NSapGruposTesoreria { get; set; } //Proveedores
        private INSapBancos NSapBancos { get; set; } //Proveedores
        private INSapCondicionesPago NSapCondicionesPago { get; set; } //Proveedores
        private INSapViasPago NSapViasPago { get; set; } //Proveedores
        private INSapGruposTolerancia NSapGruposTolerancia { get; set; } //Proveedores
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
            if (EV.ProcOperColumnasCap != null && EV.ProcOperColumnasCap.Count > 0)
            {
                foreach (EProcesoOperativoCol vCol in EV.ProcOperColumnasCap)
                {
                    if (vCol.CapCmbProcesoOperativoId > 0)
                        vCol.ElementosCmb = await NConExpedientes.ConExpedienteCmb(vCol);
                }
            }

            //No config Proveedor
            EV.ParamProveedorProcesoOperativoId = await Servicios.ParamSist.Param<Int64>("RediinProveedorProcesoOperativoId");
            if (EV.ConExpProcOperativo.Sel.ProcesoOperativoId == EV.ParamProveedorProcesoOperativoId)
            {
                var vRelaciones = await NExpedientes.RelacionProcesoOperativo(EV.ParamProveedorProcesoOperativoId);
                EV.ParamEstIdCaptura = await Servicios.ParamSist.Param<Int64>("RediinProveedorProcesoOperativoEstIdCaptura");
                EV.ParamEstIdAutorizado = await Servicios.ParamSist.Param<Int64>("RediinProveedorProcesoOperativoEstIdAutorizado");
                EV.ParamUrlRediinProveedores = await Servicios.ParamSist.Param<String>("RediinProveedorUrl");


                EV.ProveedorColumnaIdUsuario = UtilExpediente.ObtenRelacion(vRelaciones, nameof(EProveedor.UsuarioId)).ColumnaId;
                if (EV.ProveedorColumnaIdUsuario <= 0)
                {
                    NConExpedientes.Mensajes.AddError($"No se configuro correctamente el usuarioId para un nuevo usuario.");
                    return await ConExpProcOperativoCon();
                }

                EV.ParamPerfilIdNvoUsr = await Servicios.ParamSist.Param<Int64>("RediinProveedorPerfilIdNvoUsr");
                if (EV.ParamPerfilIdNvoUsr <= 0)
                {
                    NConExpedientes.Mensajes.AddError($"No se configuro correctamente el perfil para un nuevo usuario.");
                    return await ConExpProcOperativoCon();
                }
                EV.ParamProveedorColumnaIdNombre = UtilExpediente.ObtenRelacion(vRelaciones, nameof(EProveedor.NombreORazonSocial)).ColumnaId;
                EV.ParamProveedorColumnaIdCorreo = UtilExpediente.ObtenRelacion(vRelaciones, nameof(EProveedor.ContactoCorreoElectronico)).ColumnaId;
                if (!EV.ProcOperColumnasCon.Exists(e => e.ColumnaId == EV.ParamProveedorColumnaIdNombre))
                {
                    NConExpedientes.Mensajes.AddError($"No se configuro correctamente la columna de nombre para este proceso operativo de proveedores [{EV.ParamProveedorColumnaIdNombre}].");
                    return await ConExpProcOperativoCon();
                }
                if (!EV.ProcOperColumnasCon.Exists(e => e.ColumnaId == EV.ParamProveedorColumnaIdCorreo))
                {
                    NConExpedientes.Mensajes.AddError($"No se configuro correctamente la columna de correo para este proceso operativo de proveedores [{EV.ParamProveedorColumnaIdCorreo}].");
                    return await ConExpProcOperativoCon();
                }

                //Para catalogos
                EV.ParamProveedorColumnaIdPais = UtilExpediente.ObtenRelacion(vRelaciones, nameof(EProveedor.PaisId)).ColumnaId;
                EV.ParamProveedorColumnaIdEstado = UtilExpediente.ObtenRelacion(vRelaciones, nameof(EProveedor.EstadoId)).ColumnaId;
                EV.ParamProveedorColumnaIdMunicipio = UtilExpediente.ObtenRelacion(vRelaciones, nameof(EProveedor.MunicipioId)).ColumnaId;
                EV.ParamProveedorColumnaIdColonia = UtilExpediente.ObtenRelacion(vRelaciones, nameof(EProveedor.ColoniaId)).ColumnaId;
                List<MEElemento> vBancos = await NBancos.BancoCmb();
                EV.CombosProveedores = new Dictionary<Int64, List<MEElemento>>()
                {
                    { EV.ParamProveedorColumnaIdPais, await NPaises.PaisCmb() },
                    { EV.ParamProveedorColumnaIdEstado, null},
                    { EV.ParamProveedorColumnaIdMunicipio, null},
                    { EV.ParamProveedorColumnaIdColonia, null},
                    { UtilExpediente.ObtenRelacion(vRelaciones, nameof(EProveedor.BancoId)).ColumnaId, vBancos },
                    { UtilExpediente.ObtenRelacion(vRelaciones, nameof(EProveedor.BancoId2)).ColumnaId, vBancos },
                    { UtilExpediente.ObtenRelacion(vRelaciones, nameof(EProveedor.BancoId3)).ColumnaId, vBancos },
                    { UtilExpediente.ObtenRelacion(vRelaciones, nameof(EProveedor.SapSociedadId)).ColumnaId, await NSapSociedades.SapSociedadCmb() },
                    { UtilExpediente.ObtenRelacion(vRelaciones, nameof(EProveedor.SapSociedadGLId)).ColumnaId, await NSapSociedadesGL.SapSociedadGLCmb() },
                    { UtilExpediente.ObtenRelacion(vRelaciones, nameof(EProveedor.SapGrupoCuentaId)).ColumnaId, await NSapGrupoCuentas.SapGrupoCuentaCmb() },
                    { UtilExpediente.ObtenRelacion(vRelaciones, nameof(EProveedor.SapOrganizacionCompraId)).ColumnaId, await NSapOrganizacionesCompra.SapOrganizacionCompraCmb() },
                    { UtilExpediente.ObtenRelacion(vRelaciones, nameof(EProveedor.SapTratamientoId)).ColumnaId, await NSapTratamientos.SapTratamientoCmb() },
                    { UtilExpediente.ObtenRelacion(vRelaciones, nameof(EProveedor.SapCuentaAsociadaId)).ColumnaId, await NSapCuentasAsociadas.SapCuentaAsociadaCmb() },
                    { UtilExpediente.ObtenRelacion(vRelaciones, nameof(EProveedor.SapGrupoTesoreriaId)).ColumnaId, await NSapGruposTesoreria.SapGrupoTesoreriaCmb() },
                    { UtilExpediente.ObtenRelacion(vRelaciones, nameof(EProveedor.SapBancoId)).ColumnaId, await NSapBancos.SapBancoCmb() },
                    { UtilExpediente.ObtenRelacion(vRelaciones, nameof(EProveedor.SapCondicionPagoId)).ColumnaId, await NSapCondicionesPago.SapCondicionPagoCmb() },
                    { UtilExpediente.ObtenRelacion(vRelaciones, nameof(EProveedor.SapViaPagoId)).ColumnaId, await NSapViasPago.SapViaPagoCmb() },
                    { UtilExpediente.ObtenRelacion(vRelaciones, nameof(EProveedor.SapGrupoToleranciaId)).ColumnaId, await NSapGruposTolerancia.SapGrupoToleranciaCmb() },
                };
            }
            //No config Proveedor

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
            AjustaComboCascadaPEMProv(conExpediente, PEMColumnaId);
            return await ConExpedienteInsertaCap(conExpediente);
        }
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConExpedienteInserta(IFormCollection conExp)
        {
            EConExpediente conExpediente = ObtenExpediente(conExp);
            conExpediente.ProcesoOperativoId = EV.ConExpProcOperativo.Sel.ProcesoOperativoId; //Llave padre
            conExpediente.ControlEstatus = EV.ConExpProcOperativo.Sel.ControlEstatus;
            //conExpediente.ProcesoOperativoEstId = 0L;

            if (EV.ConExpProcOperativo.Sel.ProcesoOperativoId == EV.ParamProveedorProcesoOperativoId)
            {
                String vNombre = ObtenValor(conExpediente, EV.ParamProveedorColumnaIdNombre).ToString();
                String vCorreo = ObtenValor(conExpediente, EV.ParamProveedorColumnaIdCorreo).ToString();
                if (String.IsNullOrWhiteSpace(vNombre))
                    NExpedientes.Mensajes.AddError("El campo [Nombre o razón social] es obligatorio.");
                if (String.IsNullOrWhiteSpace(vNombre))
                    NExpedientes.Mensajes.AddError("El campo [Correo] es obligatorio.");
                if (!NExpedientes.Mensajes.Ok)
                    return await ConExpedienteInsertaCap(conExpediente);

                conExpediente.ExpedienteId = await NConExpedientes.ConExpedienteInserta(conExpediente);
                if (!NConExpedientes.Mensajes.Ok)
                    return await ConExpedienteInsertaCap(conExpediente);

                //JRD VERIFICAR
                var vResultado = await CreaUsuario(conExpediente);
                EClave vCve = vResultado.Item1;
                EUsuario vUsuario = vResultado.Item2;

                if (NExpedientes.Mensajes.Ok)
                {
                    foreach (var vValor in conExpediente.Valores)
                    {
                        if (vValor.ColumnaId == EV.ProveedorColumnaIdUsuario)
                            EstableceValor(vValor, TiposColumna.Entero, vCve.UsuarioId.ToString());
                    }
                    await NConExpedientes.ConExpedienteActualiza(conExpediente);

                    EnviaCorreo(vUsuario.CorreoElectronico,
                                "Su usuario de Rediin Proveedores ha sido creado.",
                                String.Format("Bienvenido a Rediin Proveedores.<br/><br/>Su usuario es {0}<br/>Su contraseña es {1}<br/><br/>La URL donde puede acceder a sus sistema es:<br/>{2}",
                                        vUsuario.Usuario, vCve.ClaveVerif, EV.ParamUrlRediinProveedores));
                }

                return RedirectToAction(nameof(ConExpedienteCon));
            }
            else
            {
                await NConExpedientes.ConExpedienteInserta(conExpediente);
                if (NConExpedientes.Mensajes.Ok)
                    return RedirectToAction(nameof(ConExpedienteCon));

                return await ConExpedienteInsertaCap(conExpediente);
            }
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
            AjustaComboCascadaPEMProv(conExpediente, PEMColumnaId);
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
            if (EV.ConExpProcOperativo.Sel.ProcesoOperativoId == EV.ParamProveedorProcesoOperativoId &&
               procesoOperativoEstIdSig == EV.ParamEstIdCaptura)
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
            if (await NConExpedientes.ConExpedienteCambioEstatus(conExpedienteCambioEstatus))
            {
                //Adi
                if (EV.ConExpProcOperativo.Sel.ProcesoOperativoId == EV.ParamProveedorProcesoOperativoId)
                {
                    String vCorreo = ObtenValor(EV.ConExpediente.Sel, EV.ParamProveedorColumnaIdCorreo).ToString();
                    String vProveedor = ObtenValor(EV.ConExpediente.Sel, EV.ParamProveedorColumnaIdNombre).ToString();
                    if (conExpedienteCambioEstatus.ProcesoOperativoEstId == EV.ParamEstIdCaptura)
                    {
                        EnviaCorreo(vCorreo,
                                    "Seguimiento en Portal de Rediin Proveedores",
                                    $"Estimado {vProveedor}:<br/><br/>Su alta como proveedor tiene las siguientes observaciones:<br/>{conExpedienteCambioEstatus.Comentarios}");
                    }
                    else if (conExpedienteCambioEstatus.ProcesoOperativoEstId == EV.ParamEstIdAutorizado)
                    {
                        EnviaCorreo(vCorreo,
                                    "Seguimiento en Portal de Rediin Proveedores",
                                    $"Estimado {vProveedor}:<br/><br/>Su alta como proveedor ha sido satisfactoria.");
                    }
                }

                return RedirectToAction(nameof(ConExpedienteCon));
            }

            //Adi
            if (EV.ConExpProcOperativo.Sel.ProcesoOperativoId == EV.ParamProveedorProcesoOperativoId &&
               conExpedienteCambioEstatus.ProcesoOperativoEstId == EV.ParamEstIdCaptura)
                return await ConExpedienteCambioEstatusCap(conExpedienteCambioEstatus);
            else
            {
                return RedirectToAction(nameof(ConExpedienteCon));
            }
        }
        #endregion

        #region Funciones
        private async Task<IActionResult> ConExpedienteCaptura(EConExpediente conExpediente)
        {
            ViewBag.Mensajes = NConExpedientes.Mensajes;
            ViewBag.EV = EV;

            //Adi
            ViewBag.ProcOperColumnas = EV.ProcOperColumnasCap;
            ViewBag.ParamProveedorProcesoOperativoId = EV.ParamProveedorProcesoOperativoId;

            conExpediente.ProcesoOperativoId = EV.ConExpProcOperativo.Sel.ProcesoOperativoId;
            if (EV.ConExpProcOperativo.Sel.ProcesoOperativoId == EV.ParamProveedorProcesoOperativoId)
            {
                ViewBag.EVConExpedientes = EV;
                EV.CombosProveedores[EV.ParamProveedorColumnaIdEstado] = null;
                EV.CombosProveedores[EV.ParamProveedorColumnaIdMunicipio] = null;
                EV.CombosProveedores[EV.ParamProveedorColumnaIdColonia] = null;

                Int64 vPaisId = XObject.ToInt64(ObtenValor(conExpediente, EV.ParamProveedorColumnaIdPais));
                if (vPaisId > 0)
                {
                    EV.CombosProveedores[EV.ParamProveedorColumnaIdEstado] =
                        await NPaises.EstadoCmb(vPaisId);
                    Int64 vEstadoId = XObject.ToInt64(ObtenValor(conExpediente, EV.ParamProveedorColumnaIdEstado));
                    if (vEstadoId > 0)
                    {
                        EV.CombosProveedores[EV.ParamProveedorColumnaIdMunicipio] =
                            await NPaises.MunicipioCmb(vEstadoId);
                        Int64 vMunicipio = XObject.ToInt64(ObtenValor(conExpediente, EV.ParamProveedorColumnaIdMunicipio));
                        if (vMunicipio > 0)
                        {
                            EV.CombosProveedores[EV.ParamProveedorColumnaIdColonia] =
                                await NPaises.ColoniaCmb(vMunicipio);
                        }
                    }
                }
            }

            return ViewCap(nameof(ConExpedienteCaptura), conExpediente);
        }
        private void AjustaComboCascadaPEMProv(EConExpediente conExpediente, Int64 columnaId)
        {
            if (columnaId == EV.ParamProveedorColumnaIdPais)
            {
                foreach (var vVal in conExpediente.Valores)
                {
                    if (vVal.ColumnaId == EV.ParamProveedorColumnaIdEstado ||
                       vVal.ColumnaId == EV.ParamProveedorColumnaIdMunicipio ||
                       vVal.ColumnaId == EV.ParamProveedorColumnaIdColonia)
                        EstableceValor(vVal, TiposColumna.Entero, "0");
                }
            }
            else if (columnaId == EV.ParamProveedorColumnaIdEstado)
            {
                foreach (var vVal in conExpediente.Valores)
                {
                    if (vVal.ColumnaId == EV.ParamProveedorColumnaIdMunicipio ||
                        vVal.ColumnaId == EV.ParamProveedorColumnaIdColonia)
                        EstableceValor(vVal, TiposColumna.Entero, "0");
                }
            }
            else if (columnaId == EV.ParamProveedorColumnaIdMunicipio)
            {
                foreach (var vVal in conExpediente.Valores)
                {
                    if (vVal.ColumnaId == EV.ParamProveedorColumnaIdColonia)
                        EstableceValor(vVal, TiposColumna.Entero, "0");
                }
            }
        }
        private void EstableceValor(EConExpValores valor, TiposColumna tipo, String cadena)
        {
            if (tipo == TiposColumna.Entero || tipo == TiposColumna.Importe)
                valor.ValorNumerico = XObject.ToDecimal(cadena);
            else if (tipo == TiposColumna.Fecha || tipo == TiposColumna.FechaYHora || tipo == TiposColumna.Hora)
                valor.ValorFecha = XObject.ToDateTime(cadena);
            else if (tipo == TiposColumna.Boleano)
                valor.ValorTexto = (cadena == "true" ? "1" : String.Empty);
            else
                valor.ValorTexto = cadena;
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
                if (conExp.ContainsKey(vCol.ColumnaId.ToString()))
                    EstableceValor(vVal, vCol.Tipo, conExp[vCol.ColumnaId.ToString()]);
                conExpediente.Valores.Add(vVal);
            }

            return conExpediente;
        }
        //No config Proveedor
        private async Task<(EClave, EUsuario)> CreaUsuario(EConExpediente conExpediente)
        {
            EUsuario usuario = new();
            String vProveedor = ObtenValor(conExpediente, EV.ParamProveedorColumnaIdNombre).ToString();
            String[] vNombres = vProveedor.Split(" ");

            usuario.CorreoElectronico = ObtenValor(conExpediente, EV.ParamProveedorColumnaIdCorreo).ToString();
            usuario.EstablecimientoId = Servicios.EVDatosPortal.UsuarioSesion.EstablecimientoId;
            usuario.PerfilId = EV.ParamPerfilIdNvoUsr;

            if (vNombres.Length >= 3)
            {
                usuario.ApellidoMaterno = vNombres[vNombres.Length - 1];
                usuario.ApellidoPaterno = vNombres[vNombres.Length - 2];
                usuario.Nombre = String.Empty;
                for (int i = 0; i < vNombres.Length - 2; i++)
                    usuario.Nombre += (i > 0 ? " " : String.Empty) + vNombres[i];

                usuario.Usuario = $"{usuario.Nombre[0]}{usuario.ApellidoPaterno}".ToLower();
            }
            else if (vNombres.Length >= 2)
            {
                usuario.ApellidoMaterno = "S/N.";
                usuario.ApellidoPaterno = vNombres[vNombres.Length - 2];
                usuario.Nombre = String.Empty;
                for (int i = 0; i < vNombres.Length - 2; i++)
                    usuario.Nombre += (i > 0 ? " " : String.Empty) + vNombres[i];

                usuario.Usuario = $"{usuario.Nombre[0]}{usuario.ApellidoPaterno}".ToLower();
            }
            else
            {
                usuario.ApellidoPaterno = "S/N.";
                usuario.ApellidoMaterno = "S/N.";
                usuario.Usuario = $"{usuario.Nombre.Trim().Replace(" ", "")}".ToLower();
            }

            usuario.Usuario += (DateTime.Now.Year - 2000).ToString();
            usuario.Usuario += DateTime.Now.DayOfYear.ToString();

            try
            {
                return (await NUsuarios.UsuarioInsertaAuto(usuario), usuario);
            }
            catch (Exception e)
            {
                NUsuarios.Mensajes.AddError(e.Message);
                return (null, null);
            }
        }
        private async void EnviaCorreo(String correoDestino, String subject, String body)
        {
            //JRD REVISAR QUE ESTE BIEN
            IMCorreo vCorreo = await Servicios.ServCorreo.ServCorreo("RediinProveedoresMail");
            vCorreo.To.Add(vCorreo.CreateUser("Cliente", correoDestino));
            vCorreo.Send(subject, body);

            //var vCorreo = base.ServidorCorreo("RediinProveedoresMail");
            //vCorreo.To.Add(vCorreo.NewUser("Cliente", correoDestino));
            //vCorreo.Send(subject, body);
        }
        private Object ObtenValor(EConExpediente conExpediente, Int64 columnaId)
        {
            return UtilExpediente.ObtenValor(EV.ProcOperColumnasCon,
                                             conExpediente,
                                             columnaId);
        }
        //No config Proveedor
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

            return RedirectToAction(nameof(ConExpedienteObjetoCon));
        }
        [MValidaSeg(nameof(ConExpedienteObjetoInicia))]
        public async Task<IActionResult> ConExpedienteObjetoCon()
        {
            EV.ConExpedienteObjeto.Filtro.ExpedienteId = EV.ConExpediente.Sel.ExpedienteId;

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
        [MValidaSeg(nameof(ConExpedienteObjetoDescarga))]
        public async Task<IActionResult> ConExpedienteObjetoDescarga2(Int32 indice)
        {
            Int32 totalPaginas = 0;
            EConExpedienteObjeto vObj = EV.ConExpedienteObjeto.Pag.Pagina[indice];
            String imageFilesPath = Path.Combine(Path.Combine(vObj.Ruta, "temp"), "page-{0}.png");
            using GroupDocs.Viewer.Viewer v = new GroupDocs.Viewer.Viewer(Path.Combine(vObj.Ruta, vObj.ArchivoNombre));
            GroupDocs.Viewer.Results.ViewInfo i = v.GetViewInfo(GroupDocs.Viewer.Options.ViewInfoOptions.ForPngView(false));
            totalPaginas = i.Pages.Count;
            GroupDocs.Viewer.Options.PngViewOptions o = new PngViewOptions(imageFilesPath);
            v.View(o);
            return await Task.FromResult(new JsonResult(totalPaginas));
        }
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
