using DSEntityNetX.Business.Rules;
using DSEntityNetX.Common.Casting;
using DSEntityNetX.Entities.Rules;
using DSMetodNetX.Entidades;
using DSMetodNetX.Entidades.Config;
using DSMetodNetX.Entidades.Correo;
using Rediin2022.Comun.PriOperacion;
using Rediin2022.Entidades.PriCatalogos;
using Rediin2022.Entidades.PriClientes;
using Rediin2022.Entidades.PriOperacion;
using Sisegui2020.Entidades.PriCatalogos;
using Sisegui2020.Entidades.PriSeguridad;
using System;
using System.Threading.Tasks;

namespace Rediin2022.Aplicacion.PriOperacion;
public class SENConExpedienteMedix : ISENConExpedienteProv
{
    #region Constructor
    public SENConExpedienteMedix(IMSrvPrivado servicios,
                                 INConExpedientes nConExpedientes,
                                 INExpedientes nExpedientes,
                                 INExpedientesProveedor nExpedientesProveedor,
                                 INUsuarios nUsuarios,
                                 INPaises nPaises,
                                 INIdentificaciones nIdentificaciones,
                                 INModelos nModelos,
                                 INRegimenesFiscales nRegimenesFiscales,
                                 INBancos nBancos,
                                 INIncoterms nIncoterms,
                                 IConfig config,
                                 //HttpClient httpClient,
                                 NRMedix nMedix,
                                 INErroresNoControlados nErroresNoControlados)
    {
        Servicios = servicios;
        //Config = config;
        NConExpedientes = nConExpedientes;
        NExpedientes = nExpedientes;
        NExpedientesProveedor = nExpedientesProveedor;
        NUsuarios = nUsuarios;
        NPaises = nPaises;
        NIdentificaciones = nIdentificaciones;
        NModelos = nModelos;
        NRegimenesFiscales = nRegimenesFiscales;
        NBancos = nBancos;
        NIncoterms = nIncoterms;
        //HttpClient = httpClient;
        NMedix = nMedix;

        NErroresNoControlados = nErroresNoControlados;
    }
    #endregion

    #region Propiedades
    private IMMensajes Mensajes
    {
        get { return NConExpedientes.Mensajes; }
    }

    public EVConExpedientes EV { get; set; }

    //private IConfig Config { get; set; }
    //private HttpClient HttpClient { get; set; }
    private NRMedix NMedix { get; set; }

    private IMSrvPrivado Servicios { get; set; }
    private INConExpedientes NConExpedientes { get; set; }
    private INExpedientes NExpedientes { get; set; }
    private INExpedientesProveedor NExpedientesProveedor { get; set; }
    private INUsuarios NUsuarios { get; set; }
    private INPaises NPaises { get; set; }
    private INIdentificaciones NIdentificaciones { get; set; }
    private INModelos NModelos { get; set; }
    private INRegimenesFiscales NRegimenesFiscales { get; set; }
    private INBancos NBancos { get; set; }
    private INIncoterms NIncoterms { get; set; }
    public INErroresNoControlados NErroresNoControlados { get; set; }
    #endregion

    #region ConExpediente (Exp)
    public async Task<Boolean> Inicia()
    {
        EV.Medix = new();
        EV.Medix.ParamEstIdCaptura = await Servicios.ParamSist.Param<Int64>("RediinProveedorProcesoOperativoEstIdCaptura");
        //EV.Medix.ParamEstIdAutorizado = await Servicios.ParamSist.Param<Int64>("RediinProveedorProcesoOperativoEstIdAutorizado");
        EV.Medix.ParamEstIdEnTesoreria = await Servicios.ParamSist.Param<Int64>("RediinProveedorProcesoOperativoEstIdEnTesoreria");
        EV.Medix.ParamEstIdRevisado = await Servicios.ParamSist.Param<Int64>("RediinProveedorProcesoOperativoEstIdRevisado");
        EV.Medix.ParamUrlRediinProveedores = await Servicios.ParamSist.Param<String>("RediinProveedorUrl");

        EV.Medix.ColumnaIdUsuario = UtilExpediente.ObtenRelacion(EV.ProcOperColumnasCap, nameof(EProveedorMedix.UsuarioId)).ColumnaId;
        if (EV.Medix.ColumnaIdUsuario <= 0)
        {
            NConExpedientes.Mensajes.AddError($"No se configuro correctamente el usuarioId para un nuevo usuario.");
            return false;
        }

        EV.Medix.ColumnaIdProveedor = UtilExpediente.ObtenRelacion(EV.ProcOperColumnasCap, nameof(EProveedorMedix.ProveedorId)).ColumnaId;
        if (EV.Medix.ColumnaIdProveedor <= 0)
        {
            NConExpedientes.Mensajes.AddError($"No se configuro correctamente el proveedorId para actualizar desde el API.");
            return false;
        }

        EV.Medix.ColumnaIdModelo = UtilExpediente.ObtenRelacion(EV.ProcOperColumnasCap, nameof(EProveedorMedix.ModeloId)).ColumnaId;
        if (EV.Medix.ColumnaIdModelo <= 0)
        {
            NConExpedientes.Mensajes.AddError($"No se configuro correctamente el modeloId para obtener el tipo de captura con el que se filtran los objetos del expediente.");
            return false;
        }

        EV.Medix.ParamPerfilIdNvoUsr = await Servicios.ParamSist.Param<Int64>("RediinProveedorPerfilIdNvoUsr");
        if (EV.Medix.ParamPerfilIdNvoUsr <= 0)
        {
            NConExpedientes.Mensajes.AddError($"No se configuro correctamente el perfil para un nuevo usuario.");
            return false;
        }

        EV.Medix.ParamColumnaIdNombre = UtilExpediente.ObtenRelacion(EV.ProcOperColumnasCap, nameof(EProveedorMedix.NombreORazonSocial)).ColumnaId;
        if (!EV.ProcOperColumnasCon.Exists(e => e.ColumnaId == EV.Medix.ParamColumnaIdNombre))
        {
            NConExpedientes.Mensajes.AddError($"No se configuro correctamente la columna de nombre para este proceso operativo de proveedores [{EV.Medix.ParamColumnaIdNombre}].");
            return false;
        }
        EV.Medix.ParamColumnaIdCorreo = UtilExpediente.ObtenRelacion(EV.ProcOperColumnasCap, nameof(EProveedorMedix.CorreoElectronico1)).ColumnaId;
        //EV.Medix.ParamColumnaIdCorreo = UtilExpediente.ObtenRelacion(EV.ProcOperColumnasCap, nameof(EProveedorMedix.ContactoCorreoElectronico)).ColumnaId;
        if (!EV.ProcOperColumnasCon.Exists(e => e.ColumnaId == EV.Medix.ParamColumnaIdCorreo))
        {
            NConExpedientes.Mensajes.AddError($"No se configuro correctamente la columna de correo para este proceso operativo de proveedores [{EV.Medix.ParamColumnaIdCorreo}].");
            return false;
        }

        //EV.Medix.ApiSapUsuario = Config.Valor<String>("MedixApiSapUsuario");
        //EV.Medix.ApiSapPwd = Config.Valor<String>("MedixApiSapPwd");
        //EV.Medix.ApiSapUrl = Config.Valor<String>("MedixApiSapUrl");

        return Mensajes.Ok;
    }
    public async Task<Boolean> Inserta(EConExpediente conExpediente)
    {
        //Se valida aqui porque usa List<EProcesoOperativoCol> para ObtenValor
        String vNombre = ObtenValor(conExpediente, EV.Medix.ParamColumnaIdNombre).ToString();
        String vCorreo = ObtenValor(conExpediente, EV.Medix.ParamColumnaIdCorreo).ToString();
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
            //foreach (var vValor in conExpediente.Valores)
            //{
            //    if (vValor.ColumnaId == EV.Medix.ColumnaIdUsuario)
            //    {
            //        UtilExpediente.EstableceValor(vValor, TiposColumna.Entero, vCve.UsuarioId.ToString());
            //        break;
            //    }
            //}
            UtilExpediente.EstableceValor(conExpediente.Valores, EV.Medix.ColumnaIdUsuario, TiposColumna.Entero, vCve.UsuarioId.ToString());
            await NConExpedientes.ConExpedienteActualiza(conExpediente);

            await EnviaCorreo(vUsuario.CorreoElectronico,
                        "Su usuario de Rediin Proveedores ha sido creado.",
                        String.Format("Bienvenido a Rediin Proveedores.<br/><br/>Su usuario es {0}<br/>Su contraseña es {1}<br/><br/>La URL donde puede acceder a sus sistema es:<br/>{2}",
                                vUsuario.Usuario, vCve.ClaveVerif, EV.Medix.ParamUrlRediinProveedores));
        }

        return true;
    }
    public async void CambioEstatus(EConExpedienteCambioEstatus conExpedienteCambioEstatus)
    {
        String vCorreo = ObtenValor(EV.ConExpediente.Sel, EV.Medix.ParamColumnaIdCorreo).ToString();
        String vProveedor = ObtenValor(EV.ConExpediente.Sel, EV.Medix.ParamColumnaIdNombre).ToString();
        if (conExpedienteCambioEstatus.ProcesoOperativoEstId == EV.Medix.ParamEstIdCaptura)
        {
            await EnviaCorreo(vCorreo,
                          "Seguimiento en Portal de Rediin Proveedores",
                          $"Estimado {vProveedor}:<br/><br/>Su alta como proveedor tiene las siguientes observaciones:<br/>{conExpedienteCambioEstatus.Comentarios}");
        }
        //else if (conExpedienteCambioEstatus.ProcesoOperativoEstId == EV.Medix. )
        //{
        //    await EnviaCorreo(vCorreo,
        //                "Seguimiento en Portal de Rediin Proveedores",
        //                $"Estimado {vProveedor}:<br/><br/>Su alta como proveedor ha sido satisfactoria.");
        //}
    }

    public bool ValidaEstatus(long procesoOperativoEstId)
    {
        return procesoOperativoEstId == EV.Medix.ParamEstIdCaptura;
    }

    public async Task<bool> ValidaEstatusParaCambio(EConExpedienteCambioEstatus conExpedienteCambioEstatus)
    {
        if (conExpedienteCambioEstatus.ProcesoOperativoEstId == EV.Medix.ParamEstIdRevisado)
            return await ActualizaAPI();
        else if (conExpedienteCambioEstatus.ProcesoOperativoEstId == EV.Medix.ParamEstIdEnTesoreria)
            return await ValidaDatosSAP();
        else
            return true;
    }

    //private async Task<Boolean> ActualizaAPI(Int64 expedienteId)
    private async Task<Boolean> ActualizaAPI()
    {
        EMedixApi vApiBase = new();
        //Lenamos el proveedor
        await LlenaCamposAPI(vApiBase.proveedor);

        ////Convertimos el proveedor a base64
        //string vApiBaseJson = JsonSerializer.Serialize(vApiBase);
        //byte[] vApiBaseBytes = Encoding.UTF8.GetBytes(vApiBaseJson);
        //string vApiBaseBase64 = Convert.ToBase64String(vApiBaseBytes);

        ////Preparamos la entidad de envio
        //EMedixApiEnviar vEnvio = new();
        //vEnvio.solicitud.idApp = EV.Medix.ApiSapUsuario;
        //vEnvio.solicitud.pwdApp = EV.Medix.ApiSapPwd;
        //vEnvio.solicitud.enc_request = vApiBaseBase64;

        //Llamamos al API
        //try
        //{
        EMedixApiRecibir vApiRecibir = await NMedix.AltaProveedorRediin(vApiBase);
        if (!Mensajes.Ok)
            return false;

        //HttpResponseMessage vRes = await HttpClient.PostAsJsonAsync(EV.Medix.ApiSapUrl, vEnvio);
        //if (!vRes.IsSuccessStatusCode)
        //{
        //    Mensajes.AddError("Error al consultar el API.");
        //    return false;
        //}

        ////JRD ELIMINAR DESPUES
        ////string vApiRespuesta = await vRes.Content.ReadAsStringAsync();

        //EMedixApiRecibir vApiRecibir = await vRes.Content.ReadFromJsonAsync<EMedixApiRecibir>();

        //if (vApiRecibir.respuesta.estatus != "success")
        //{
        //    Mensajes.AddError(vApiRecibir.respuesta.mensaje);
        //    return false;
        //}

        //if (vApiRecibir.respuesta.data == null ||
        //    vApiRecibir.respuesta.data.proveedor == null ||
        //    vApiRecibir.respuesta.data.proveedor.Count == 0)
        //{
        //    Mensajes.AddError("No se recibio el proveedor en el resultado.");
        //    return false;
        //}

        //if (vApiRecibir.respuesta.data.proveedor[0].respuestaSAPEstatus != "success")
        //{
        //    Mensajes.AddError(vApiRecibir.respuesta.data.proveedor[0].respuestaSAPMensaje);
        //    return false;
        //}

        //if (string.IsNullOrWhiteSpace(vApiRecibir.respuesta.data.proveedor[0].numeroProveedor))
        //{
        //    Mensajes.AddError("El numero de proveedor esta vacio.");
        //    return false;
        //}

        //Actualizamos en el expediente el proveedorId
        //foreach (var vValor in EV.ConExpediente.Sel.Valores)
        //{
        //    if (vValor.ColumnaId == EV.Medix.ColumnaIdProveedor)
        //    {
        //        UtilExpediente.EstableceValor(vValor, TiposColumna.Entero, vApiRecibir.respuesta.data.proveedor[0].numeroProveedor);
        //        break;
        //    }
        //}
        UtilExpediente.EstableceValor(EV.ConExpediente.Sel.Valores, EV.Medix.ColumnaIdProveedor,
            TiposColumna.Entero, vApiRecibir.respuesta.data.proveedor[0].numeroProveedor);
        if (await NConExpedientes.ConExpedienteActualiza(EV.ConExpediente.Sel))
        {
            //Envia correo
            String vCorreo = ObtenValor(EV.ConExpediente.Sel, EV.Medix.ParamColumnaIdCorreo).ToString();
            String vProveedor = ObtenValor(EV.ConExpediente.Sel, EV.Medix.ParamColumnaIdNombre).ToString();
            await EnviaCorreo(vCorreo,
               "Seguimiento en Portal de Rediin Proveedores",
               $"Estimado {vProveedor}:<br/><br/>Su alta como proveedor ha sido satisfactoria.");
        }
        //}
        //catch (Exception ex)
        //{
        //    Mensajes.AddError("Error al llamar al API.");
        //    Mensajes.AddError(ex.Message);
        //}

        return Mensajes.Ok;
    }
    private async Task LlenaCamposAPI(EMedixApiProveedor apiProveedor)
    {
        EProveedorMedix vProveedor = new();
        UtilProveedorEspecif.CargaEntidadProveedor(EV.ProcOperColumnasCon, EV.ConExpediente.Sel, vProveedor);

        EPais vPais = await NPaises.PaisXId(vProveedor.PaisId);
        EEstado vEstado = await NPaises.EstadoXId(vProveedor.EstadoId);
        EIdentificacion vIdentificacion = await NIdentificaciones.IdentificacionXId(vProveedor.IdentificacionId);
        EModelo vModelo = await NModelos.ModeloXId(vProveedor.ModeloId);
        ERegimenFiscal vRegimenFiscal = await NRegimenesFiscales.RegimenFiscalXId(vProveedor.RegimenFiscalId);
        EBanco vBanco = null;
        EIncoterm vIncoterm = await NIncoterms.IncotermXId(vProveedor.IncotermId);

        apiProveedor.idCliente = 1;
        apiProveedor.numeroIdenFiscal = vProveedor.Rfc;

        string vNombreORazonSocial = vProveedor.NombreORazonSocial ?? String.Empty;
        apiProveedor.nombre1 = vNombreORazonSocial.XFirst(35);
        apiProveedor.nombre2 = String.Empty;
        apiProveedor.nombre3 = String.Empty;
        if (vNombreORazonSocial.Length > 35)
        {
            vNombreORazonSocial.Remove(0, 35);
            apiProveedor.nombre2 = vNombreORazonSocial.XFirst(35);
        }
        if (vNombreORazonSocial.Length > 35)
        {
            vNombreORazonSocial.Remove(0, 35);
            apiProveedor.nombre3 = vNombreORazonSocial.XFirst(35);
        }

        if (vPais != null)
            apiProveedor.clavePais = vPais.SatPais;
        apiProveedor.poblacion = vProveedor.Municipio;
        apiProveedor.distrito = vProveedor.Colonia;
        apiProveedor.cp = vProveedor.CodigoPostal;
        if (vEstado != null)
            apiProveedor.region = vEstado.SatEstado;
        apiProveedor.calle = vProveedor.Calle;
        apiProveedor.numeroInterior = vProveedor.Numero; //Duda
        apiProveedor.telefono1 = vProveedor.Telefono;
        apiProveedor.telefono2 = vProveedor.Celular;
        apiProveedor.correo = vProveedor.CorreoElectronico1;
        apiProveedor.nombreNotario = vProveedor.NotarioNombre;
        apiProveedor.numeroEscritura = vProveedor.NumeroEscritura;
        apiProveedor.fechaEscritura = vProveedor.FechaEscritura.ToLongDateString(); //Duda por el tipo
        apiProveedor.nombreRepresentante = vProveedor.RepresentanteLegal;
        if (vIdentificacion != null)
            apiProveedor.idenRepresentante = vIdentificacion.IdentificacionNombre;
        apiProveedor.numeroIdenRepresentante = vProveedor.NumIdentificacion;
        if (vModelo != null)
        {
            apiProveedor.grupoCuentasAcreedor = vModelo.ModeloNombre.XFirst(4);
            if (vModelo.ModeloNombre.Length > 0)
                apiProveedor.tipoFiscal = vModelo.ModeloNombre.Substring(5);
        }
        apiProveedor.sociedad = vProveedor.SapSociedadId;
        apiProveedor.organizacionCompra = vProveedor.SapOrganizacionCompraId;
        apiProveedor.conceptoBusqueda1 = vProveedor.Busqueda1;
        apiProveedor.conceptoBusqueda2 = vProveedor.Busqueda2;
        apiProveedor.claveCondicionPago = vProveedor.SapCondicionPagoId;
        apiProveedor.numeroRegistroAnterior = vProveedor.ProveedorIdAnt.ToString(); //Duda
        apiProveedor.claveMoneda = vProveedor.MonedaId;
        apiProveedor.vendedorResponsable = vProveedor.VendedorNombre;
        if (vIncoterm != null)
            apiProveedor.incoTerms1 = vIncoterm.IncotermClave;
        apiProveedor.incoTerms2 = vProveedor.Destino;
        apiProveedor.curp = vProveedor.Curp;
        if (vRegimenFiscal != null)
            apiProveedor.regimenFiscal = vRegimenFiscal.RegimenFiscalClave;

        if (vProveedor.PaisIdBanco1 > 0 && vProveedor.BancoId1 > 0 && !string.IsNullOrWhiteSpace(vProveedor.Cuenta1))
        {
            vPais = await NPaises.PaisXId(vProveedor.PaisIdBanco1);
            vBanco = await NBancos.BancoXId(vProveedor.BancoId1);
            if (vBanco != null && vPais != null)
            {
                apiProveedor.cuentas.Add(new EMedixApiCuenta()
                {
                    clavePaisBanco = vPais.SatPais,
                    codigoBanco = vBanco.BancoNombre.XFirst(3),
                    cuentaBancaria = vProveedor.Cuenta1
                });
            }
        }
        if (vProveedor.PaisIdBanco2 > 0 && vProveedor.BancoId2 > 0 && !string.IsNullOrWhiteSpace(vProveedor.Cuenta2))
        {
            vPais = await NPaises.PaisXId(vProveedor.PaisIdBanco2);
            vBanco = await NBancos.BancoXId(vProveedor.BancoId2);
            if (vBanco != null && vPais != null)
            {
                apiProveedor.cuentas.Add(new EMedixApiCuenta()
                {
                    clavePaisBanco = vPais.SatPais,
                    codigoBanco = vBanco.BancoNombre.XFirst(3),
                    cuentaBancaria = vProveedor.Cuenta2
                });
            }
        }
        if (vProveedor.PaisIdBanco3 > 0 && vProveedor.BancoId3 > 0 && !string.IsNullOrWhiteSpace(vProveedor.Cuenta3))
        {
            vPais = await NPaises.PaisXId(vProveedor.PaisIdBanco3);
            vBanco = await NBancos.BancoXId(vProveedor.BancoId3);
            if (vBanco != null && vPais != null)
            {
                apiProveedor.cuentas.Add(new EMedixApiCuenta()
                {
                    clavePaisBanco = vPais.SatPais,
                    codigoBanco = vBanco.BancoNombre.XFirst(3),
                    cuentaBancaria = vProveedor.Cuenta3
                });
            }
        }
    }
    private async Task<Boolean> ValidaDatosSAP()
    {
        //Cargamos las reglas de negocio si no estan cargadas
        if (EV.Medix.ReglasSAP == null)
            EV.Medix.ReglasSAP = await NExpedientesProveedor.ReglasValidacionSAP(EV.ProcesoOperativoIdProveedor);

        //Creamos el objeto de validacion
        NExpedientes.Mensajes.Initialize();
        IXBusinessRules<EProveedorMedix, MEReglaNeg> vReglas =
            new XBusinessRules<EProveedorMedix, MEReglaNeg>(NExpedientes.Mensajes, EV.Medix.ReglasSAP);

        //Obtenemos el expediente
        EProveedorMedix vProveedor = new();
        UtilProveedorEspecif.CargaEntidadProveedor(EV.ProcOperColumnasCon, EV.ConExpediente.Sel, vProveedor);
        if (vProveedor.SapSociedadId == "0")
            vProveedor.SapSociedadId = string.Empty;
        if (vProveedor.SapOrganizacionCompraId == "0")
            vProveedor.SapOrganizacionCompraId = string.Empty;
        if (vProveedor.MonedaId == "0")
            vProveedor.MonedaId = string.Empty;
        if (vProveedor.SapCondicionPagoId == "0")
            vProveedor.SapCondicionPagoId = string.Empty;

        //Validamos
        return vReglas.Validate(vProveedor);
    }
    private async Task EnviaCorreo(String correoDestino, String subject, String body)
    {
        IMCorreo vCorreo = await Servicios.ServCorreo.ServCorreo("RediinProveedoresMail");
        vCorreo.To.Add(vCorreo.CreateUser("Cliente", correoDestino));
        vCorreo.Send(subject, body);
    }
    private async Task<(EClave, EUsuario)> CreaUsuario(EConExpediente conExpediente)
    {
        EUsuario usuario = new();
        String vProveedor = ObtenValor(conExpediente, EV.Medix.ParamColumnaIdNombre).ToString();

        usuario.CorreoElectronico = ObtenValor(conExpediente, EV.Medix.ParamColumnaIdCorreo).ToString();
        usuario.EstablecimientoId = Servicios.EVDatosPortal.UsuarioSesion.EstablecimientoId;
        usuario.PerfilId = EV.Medix.ParamPerfilIdNvoUsr;
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
    #endregion

    #region ConExpedienteObjeto (Objs)
    public async Task ValidaTipoCapturaXExpediente()
    {
        Int64 vModeloId = (Int64)UtilExpediente.ObtenValor(EV.ConExpediente.Sel.Valores, TiposColumna.Entero, EV.Medix.ColumnaIdModelo, typeof(Int64));
        EModelo vModelo = await NModelos.ModeloXId(vModeloId);
        if (vModelo != null)
            EV.TipoCapturaIdExpediente = vModelo.TipoCapturaId;
    }
    #endregion
}
