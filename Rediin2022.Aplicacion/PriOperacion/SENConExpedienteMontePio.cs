using DSMetodNetX.Entidades;
using DSMetodNetX.Entidades.Correo;
using Rediin2022.Comun.PriOperacion;
using Rediin2022.Entidades.PriCatalogos;
using Rediin2022.Entidades.PriClientes;
using Rediin2022.Entidades.PriOperacion;
using Sisegui2020.Entidades.PriSeguridad;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Rediin2022.Aplicacion.PriOperacion
{
    public class SENConExpedienteMontePio : ISENConExpedienteProv
    {
        public SENConExpedienteMontePio(IMSrvPrivado servicios,
                                        INConExpedientes nConExpedientes, 
                                        INExpedientes nExpedientes,
                                        INExpedientesProveedor nExpedientesProveedor,
                                        INUsuarios nUsuarios)
        { 
            Servicios = servicios;
            NConExpedientes = nConExpedientes;
            NExpedientes = nExpedientes;
            NExpedientesProveedor = nExpedientesProveedor;
            NUsuarios = nUsuarios;
        }
        public IMMensajes Mensajes
        {
            get { return NConExpedientes.Mensajes; }
        }
        public EVConExpedientes EV { get; set; }
        private IMSrvPrivado Servicios { get; set; }
        private INConExpedientes NConExpedientes { get; set; }
        private INExpedientes NExpedientes { get; set; }
        private INExpedientesProveedor NExpedientesProveedor { get; set; }
        private INUsuarios NUsuarios { get; set; }

        public async Task<Boolean> Inicia()
        {
            EV.MontePio = new();
            EV.MontePio.ProcesoOperativoIdProveedor = await Servicios.ParamSist.Param<Int64>("RediinProveedorProcesoOperativoId");
            EV.MontePio.ParamEstIdCaptura = await Servicios.ParamSist.Param<Int64>("RediinProveedorProcesoOperativoEstIdCaptura");
            EV.MontePio.ParamEstIdAutorizado = await Servicios.ParamSist.Param<Int64>("RediinProveedorProcesoOperativoEstIdAutorizado");
            EV.MontePio.ParamUrlRediinProveedores = await Servicios.ParamSist.Param<String>("RediinProveedorUrl");

            EV.MontePio.ColumnaIdUsuario = UtilExpediente.ObtenRelacion(EV.ProcOperColumnasCap, nameof(EProveedorMontePio.UsuarioId)).ColumnaId;
            if (EV.MontePio.ColumnaIdUsuario <= 0)
            {
                NConExpedientes.Mensajes.AddError($"No se configuro correctamente el usuarioId para un nuevo usuario.");
                return false;
            }

            EV.MontePio.ParamPerfilIdNvoUsr = await Servicios.ParamSist.Param<Int64>("RediinProveedorPerfilIdNvoUsr");
            if (EV.MontePio.ParamPerfilIdNvoUsr <= 0)
            {
                NConExpedientes.Mensajes.AddError($"No se configuro correctamente el perfil para un nuevo usuario.");
                return false;
            }

            EV.MontePio.ParamColumnaIdNombre = UtilExpediente.ObtenRelacion(EV.ProcOperColumnasCap, nameof(EProveedorMontePio.NombreORazonSocial)).ColumnaId;
            if (!EV.ProcOperColumnasCon.Exists(e => e.ColumnaId == EV.MontePio.ParamColumnaIdNombre))
            {
                NConExpedientes.Mensajes.AddError($"No se configuro correctamente la columna de nombre para este proceso operativo de proveedores [{EV.MontePio.ParamColumnaIdNombre}].");
                return false;
            }
            EV.MontePio.ParamColumnaIdCorreo = UtilExpediente.ObtenRelacion(EV.ProcOperColumnasCap, nameof(EProveedorMontePio.ContactoCorreoElectronico)).ColumnaId;
            if (!EV.ProcOperColumnasCon.Exists(e => e.ColumnaId == EV.MontePio.ParamColumnaIdCorreo))
            {
                NConExpedientes.Mensajes.AddError($"No se configuro correctamente la columna de correo para este proceso operativo de proveedores [{EV.MontePio.ParamColumnaIdCorreo}].");
                return false;
            }

            //Para catalogos
            //JRD QUITART
            //EV.ParamProveedorColumnaIdPais = UtilExpediente.ObtenRelacion(vRelaciones, nameof(EProveedor.PaisId)).ColumnaId;
            //EV.ParamProveedorColumnaIdEstado = UtilExpediente.ObtenRelacion(vRelaciones, nameof(EProveedor.EstadoId)).ColumnaId;
            //EV.ParamProveedorColumnaIdMunicipio = UtilExpediente.ObtenRelacion(vRelaciones, nameof(EProveedor.MunicipioId)).ColumnaId;
            //EV.ParamProveedorColumnaIdColonia = UtilExpediente.ObtenRelacion(vRelaciones, nameof(EProveedor.ColoniaId)).ColumnaId;

            //JRD QUITART
            //List<MEElemento> vBancos = await NBancos.BancoCmb();
            //EV.CombosProveedores = new Dictionary<Int64, List<MEElemento>>()
            //{
            //    { EV.ParamProveedorColumnaIdPais, await NPaises.PaisCmb() },
            //    { EV.ParamProveedorColumnaIdEstado, null},
            //    { EV.ParamProveedorColumnaIdMunicipio, null},
            //    { EV.ParamProveedorColumnaIdColonia, null},
            //    { UtilExpediente.ObtenRelacion(vRelaciones, nameof(EProveedor.BancoId)).ColumnaId, vBancos },
            //    { UtilExpediente.ObtenRelacion(vRelaciones, nameof(EProveedor.BancoId2)).ColumnaId, vBancos },
            //    { UtilExpediente.ObtenRelacion(vRelaciones, nameof(EProveedor.BancoId3)).ColumnaId, vBancos },
            //    { UtilExpediente.ObtenRelacion(vRelaciones, nameof(EProveedor.SapSociedadId)).ColumnaId, await NSapSociedades.SapSociedadCmb() },
            //    { UtilExpediente.ObtenRelacion(vRelaciones, nameof(EProveedor.SapSociedadGLId)).ColumnaId, await NSapSociedadesGL.SapSociedadGLCmb() },
            //    { UtilExpediente.ObtenRelacion(vRelaciones, nameof(EProveedor.SapGrupoCuentaId)).ColumnaId, await NSapGrupoCuentas.SapGrupoCuentaCmb() },
            //    { UtilExpediente.ObtenRelacion(vRelaciones, nameof(EProveedor.SapOrganizacionCompraId)).ColumnaId, await NSapOrganizacionesCompra.SapOrganizacionCompraCmb() },
            //    { UtilExpediente.ObtenRelacion(vRelaciones, nameof(EProveedor.SapTratamientoId)).ColumnaId, await NSapTratamientos.SapTratamientoCmb() },
            //    { UtilExpediente.ObtenRelacion(vRelaciones, nameof(EProveedor.SapCuentaAsociadaId)).ColumnaId, await NSapCuentasAsociadas.SapCuentaAsociadaCmb() },
            //    { UtilExpediente.ObtenRelacion(vRelaciones, nameof(EProveedor.SapGrupoTesoreriaId)).ColumnaId, await NSapGruposTesoreria.SapGrupoTesoreriaCmb() },
            //    { UtilExpediente.ObtenRelacion(vRelaciones, nameof(EProveedor.SapBancoId)).ColumnaId, await NSapBancos.SapBancoCmb() },
            //    { UtilExpediente.ObtenRelacion(vRelaciones, nameof(EProveedor.SapCondicionPagoId)).ColumnaId, await NSapCondicionesPago.SapCondicionPagoCmb() },
            //    { UtilExpediente.ObtenRelacion(vRelaciones, nameof(EProveedor.SapViaPagoId)).ColumnaId, await NSapViasPago.SapViaPagoCmb() },
            //    { UtilExpediente.ObtenRelacion(vRelaciones, nameof(EProveedor.SapGrupoToleranciaId)).ColumnaId, await NSapGruposTolerancia.SapGrupoToleranciaCmb() },
            //};

            return true;
        }
        public async Task<Boolean> Inserta(EConExpediente conExpediente)
        {
            String vNombre = ObtenValor(conExpediente, EV.MontePio.ParamColumnaIdNombre).ToString();
            String vCorreo = ObtenValor(conExpediente, EV.MontePio.ParamColumnaIdCorreo).ToString();
            if (String.IsNullOrWhiteSpace(vNombre))
                NExpedientes.Mensajes.AddError("El campo [Nombre o razón social] es obligatorio.");
            if (String.IsNullOrWhiteSpace(vNombre))
                NExpedientes.Mensajes.AddError("El campo [Correo] es obligatorio.");
            if (!NExpedientes.Mensajes.Ok)
                return false;

            conExpediente.ExpedienteId = await NConExpedientes.ConExpedienteInserta(conExpediente);
            if (!NConExpedientes.Mensajes.Ok)
                return false;

            //JRD VERIFICAR
            var vResultado = await CreaUsuario(conExpediente);
            EClave vCve = vResultado.Item1;
            EUsuario vUsuario = vResultado.Item2;

            if (NExpedientes.Mensajes.Ok)
            {
                foreach (var vValor in conExpediente.Valores)
                {
                    if (vValor.ColumnaId == EV.MontePio.ColumnaIdUsuario)
                        UtilExpediente.EstableceValor(vValor, TiposColumna.Entero, vCve.UsuarioId.ToString());
                }
                await NConExpedientes.ConExpedienteActualiza(conExpediente);

                EnviaCorreo(vUsuario.CorreoElectronico,
                            "Su usuario de Rediin Proveedores ha sido creado.",
                            String.Format("Bienvenido a Rediin Proveedores.<br/><br/>Su usuario es {0}<br/>Su contraseña es {1}<br/><br/>La URL donde puede acceder a sus sistema es:<br/>{2}",
                                    vUsuario.Usuario, vCve.ClaveVerif, EV.MontePio.ParamUrlRediinProveedores));
            }

            return true;
        }
        public void CambioEstatus(EConExpedienteCambioEstatus conExpedienteCambioEstatus)
        {
            String vCorreo = ObtenValor(EV.ConExpediente.Sel, EV.MontePio.ParamColumnaIdCorreo).ToString();
            String vProveedor = ObtenValor(EV.ConExpediente.Sel, EV.MontePio.ParamColumnaIdNombre).ToString();
            if (conExpedienteCambioEstatus.ProcesoOperativoEstId == EV.MontePio.ParamEstIdCaptura)
            {
                EnviaCorreo(vCorreo,
                            "Seguimiento en Portal de Rediin Proveedores",
                            $"Estimado {vProveedor}:<br/><br/>Su alta como proveedor tiene las siguientes observaciones:<br/>{conExpedienteCambioEstatus.Comentarios}");
            }
            else if (conExpedienteCambioEstatus.ProcesoOperativoEstId == EV.MontePio.ParamEstIdAutorizado)
            {
                EnviaCorreo(vCorreo,
                            "Seguimiento en Portal de Rediin Proveedores",
                            $"Estimado {vProveedor}:<br/><br/>Su alta como proveedor ha sido satisfactoria.");
            }
        }
        public bool ValidaEstatus(long procesoOperativoEstId)
        {
            return EV.ConExpProcOperativo.Sel.ProcesoOperativoId == EV.MontePio.ProcesoOperativoIdProveedor &&
                    procesoOperativoEstId == EV.MontePio.ParamEstIdCaptura;
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
        private async Task<(EClave, EUsuario)> CreaUsuario(EConExpediente conExpediente)
        {
            EUsuario usuario = new();
            String vProveedor = ObtenValor(conExpediente, EV.MontePio.ParamColumnaIdNombre).ToString();

            usuario.CorreoElectronico = ObtenValor(conExpediente, EV.MontePio.ParamColumnaIdCorreo).ToString();
            usuario.EstablecimientoId = Servicios.EVDatosPortal.UsuarioSesion.EstablecimientoId;
            usuario.PerfilId = EV.MontePio.ParamPerfilIdNvoUsr;
            UtilProveedorEspecif.SeparaNombreUsuario(vProveedor, usuario);
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
        private Object ObtenValor(EConExpediente conExpediente, Int64 columnaId)
        {
            return UtilExpediente.ObtenValor(EV.ProcOperColumnasCon,
                                             conExpediente,
                                             columnaId);
        }
    }
}
