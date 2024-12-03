using DSEntityNetX.Entities.Common;
using DSEntityNetX.Entities.Language;
using DSMetodNetX.Comun;
using DSMetodNetX.Entidades;
using Rediin2022.AccesoDatos.PriClientes;
using Rediin2022.Comun.PriOperacion;
using Rediin2022.Entidades.PriCatalogos;
using Rediin2022.Entidades.PriClientes;
using Rediin2022.Entidades.PriOperacion;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Rediin2022.Negocio.PriClientes
{
    public class NExpedientesMedix : RExpedientesProveedor, INExpedientesProveedor
    {
        #region Constructores
        public NExpedientesMedix(IMConexionEntidad conexionEntidad,
                                 INConExpedientes nConExpedientes,
                                 INProcesosOperativos nProcesosOperativos,
                                 INExpedientes nExpedientes,
                                 INModelos nModelos)
            : base(conexionEntidad)
        {
            NConExpedientes = nConExpedientes;
            NProcesosOperativos = nProcesosOperativos;
            NExpedientes = nExpedientes;
            NModelos = nModelos;
        }
        #endregion

        #region Propiedades
        public INConExpedientes NConExpedientes { get; }
        public INProcesosOperativos NProcesosOperativos { get; }
        public INExpedientes NExpedientes { get; }
        public INModelos NModelos { get; }
        #endregion

        #region Funciones
        /// <summary>
        /// Regresa los datos del proveedor segun el usuario autentificado 
        /// para el proceso operativo especifico de proveedores.
        /// </summary>
        /// <param name="usuarioId"></param>
        /// <returns></returns>
        public async Task<EDatosProveedor> ProveedorXUsuario(Int64 procesoOperativoIdProveedor,
                                                             Int64 usuarioId)
        {
            EDatosProveedor vDP = new EDatosProveedor();

            //Obtenemos el id de expediente del usuario.
            Int64 vExpendienteId = await base.ProveedorExpedienteId(usuarioId,
                                                                    procesoOperativoIdProveedor);
            if (vExpendienteId <= 0)
                return vDP;

            //Obtenemos los datos del expediente.
            EConExpediente vExpediente = await NConExpedientes.ConExpedienteXId(vExpendienteId);
            if (vExpediente == null)
                return vDP;

            //Obtenemos los metadatos de las columnas
            List<EProcesoOperativoCol> vColMD =
                await NProcesosOperativos.ProcesoOperativoColCT(procesoOperativoIdProveedor);

            //Cargamos las propiedades
            EProveedorMedix vProveedor = new();
            UtilProveedorEspecif.CargaEntidadProveedor(vColMD, vExpediente, vProveedor);

            //Cargamos el ultimo comentario
            EExpedienteEstatu vEstatusUlt = await NConExpedientes.ExpedienteEstatusUltimo(vExpendienteId);
            vProveedor.Comentarios = vEstatusUlt.Comentarios;

            //Cargamos los datos comunes de expediente
            UtilProveedorEspecif.CargaProveedor(vProveedor,
                                                vExpediente,
                                                procesoOperativoIdProveedor,
                                                usuarioId,
                                                vEstatusUlt.Comentarios);

            //Cargamos al proveedor el dato TipoCaptura
            EModelo vModelo = await NModelos.ModeloXId(vProveedor.ModeloId);
            if (vModelo != null)
                vProveedor.TipoCaptura = vModelo.TipoCapturaId;

            //Reglas de negocio
            vDP.ReglasNegocio = CrearReglasNeg(vProveedor, vColMD);

            //Salida
            vDP.Proveedor = JsonSerializer.Serialize(vProveedor);
            return vDP;
        }
        /// <summary>
        /// Actualiza el proveedor.
        /// </summary>
        /// <param name="proveedor"></param>
        /// <returns></returns>
        public async Task<Boolean> ProveedorActualiza(EString proveedor)
        {
            EProveedorMedix vProveedor = JsonSerializer.Deserialize<EProveedorMedix>(proveedor.StringValue);

            EExpediente vExp = new EExpediente();
            vExp.ProcesoOperativoId = vProveedor.ProcesoOperativoId;
            vExp.ExpendienteId = vProveedor.ExpedienteId;

            List<EProcesoOperativoColMin> vColMin =
                await NProcesosOperativos.ProcesoOperativoColCTMin(vProveedor.ProcesoOperativoId);

            UtilProveedorEspecif.CargaExpedienteValores(vProveedor, vColMin, vExp.Valores);

            return await NExpedientes.ExpedienteActualiza(vExp);
        }
        /// <summary>
        /// Pasa el expediente al siguiente estatus.
        /// </summary>
        /// <param name="expedienteId"></param>
        /// <returns></returns>
        public async Task<Boolean> ProveedorCambioEstatus(EConExpedienteCambioEstatus conExpedienteCambioEstatus)
        {
            return await NConExpedientes.ConExpedienteCambioEstatus(conExpedienteCambioEstatus);
        }
        /// <summary>
        /// Reglas de validacion para SAP.
        /// En Medix se usa para validar antes de pasar a estatus EnTesoreria
        /// </summary>
        /// <returns></returns>
        public async Task<List<MEReglaNeg>> ReglasValidacionSAP(Int64 procesoOperativoIdProveedor)
        {
            List<EProcesoOperativoCol> vColMD =
                await NProcesosOperativos.ProcesoOperativoColCT(procesoOperativoIdProveedor);

            IMReglasNeg<EProveedorMedix> vReglas = Validaciones.CreaReglasNeg<EProveedorMedix>(Mensajes);
            vReglas.Rules = new List<MEReglaNeg>();
            UtilProveedorEspecif.CargaReglasNegocioProveedor(vColMD, vReglas.Rules);

            foreach (MEReglaNeg vRN in vReglas.Rules)
            {
                if (vRN.Property == nameof(EProveedorMedix.ProveedorId) ||
                    vRN.Property == nameof(EProveedorMedix.SapSociedadId) ||
                    vRN.Property == nameof(EProveedorMedix.SapOrganizacionCompraId) ||
                    vRN.Property == nameof(EProveedorMedix.MonedaId) ||
                    vRN.Property == nameof(EProveedorMedix.SapCondicionPagoId) ||
                    vRN.Property == nameof(EProveedorMedix.IncotermId) ||
                    vRN.Property == nameof(EProveedorMedix.Destino) ||
                    vRN.Property == nameof(EProveedorMedix.Busqueda1) ||
                    vRN.Property == nameof(EProveedorMedix.Busqueda2) ||
                    vRN.Property == nameof(EProveedorMedix.UsuarioId))
                    vRN.Required = true; //Todos obligatorios
            }

            return vReglas.Rules;
        }
        protected List<MEReglaNeg> CrearReglasNeg(EProveedorMedix proveedor,
                                                  List<EProcesoOperativoCol> colMD)
        {
            //Creamos las reglas de negocio
            IMReglasNeg<EProveedorMedix> vReglas = Validaciones.CreaReglasNeg<EProveedorMedix>(Mensajes);
            vReglas.Rules = new List<MEReglaNeg>();
            UtilProveedorEspecif.CargaReglasNegocioProveedor(colMD, vReglas.Rules);

            foreach (MEReglaNeg vRN in vReglas.Rules)
            {
                //Los que se capturan en rediinProveedores
                if (vRN.Property == nameof(EProveedorMedix.NombreORazonSocial) ||
                    vRN.Property == nameof(EProveedorMedix.PaisId) ||
                    vRN.Property == nameof(EProveedorMedix.EstadoId) ||
                    vRN.Property == nameof(EProveedorMedix.Municipio) || //Depende TipoCaptura
                    vRN.Property == nameof(EProveedorMedix.Colonia) || //Depende TipoCaptura
                    vRN.Property == nameof(EProveedorMedix.Calle) ||
                    vRN.Property == nameof(EProveedorMedix.Numero) || //Depende TipoCaptura
                    vRN.Property == nameof(EProveedorMedix.CodigoPostal) ||
                    vRN.Property == nameof(EProveedorMedix.Curp) || //Depende visibilidad
                    vRN.Property == nameof(EProveedorMedix.Rfc) || //Depende visibilidad
                    vRN.Property == nameof(EProveedorMedix.RegimenFiscalId) || //Depende visibilidad
                    vRN.Property == nameof(EProveedorMedix.VendedorNombre) ||
                    vRN.Property == nameof(EProveedorMedix.Telefono) ||
                    vRN.Property == nameof(EProveedorMedix.CorreoElectronico1) ||
                    vRN.Property == nameof(EProveedorMedix.PaisIdBanco1) ||
                    vRN.Property == nameof(EProveedorMedix.BancoId1) ||
                    vRN.Property == nameof(EProveedorMedix.Cuenta1) ||
                    vRN.Property == nameof(EProveedorMedix.CuentaClabe1))
                    vRN.Required = true; //Todos obligatorios
                //else if (proveedor.TipoCaptura == TipoCaptura.PersonaMoral && (
                         //vRN.Property == nameof(EProveedorMedix.NotarioNombre) ||
                         //vRN.Property == nameof(EProveedorMedix.NumeroEscritura) ||
                         //vRN.Property == nameof(EProveedorMedix.FechaEscritura) ||
                         //vRN.Property == nameof(EProveedorMedix.RepresentanteLegal) ||
                         //vRN.Property == nameof(EProveedorMedix.IdentificacionId) ||
                         //vRN.Property == nameof(EProveedorMedix.NumIdentificacion) ||
                         //vRN.Property == nameof(EProveedorMedix.PoderNotarialNotarioNombre) ||
                         //vRN.Property == nameof(EProveedorMedix.PoderNotarialNumEscritura) ||
                         //vRN.Property == nameof(EProveedorMedix.PoderNotarialFechaEscritura) ||
                         //vRN.Property == nameof(EProveedorMedix.PoderNotarialRepresentanteLegal) ||
                         //vRN.Property == nameof(EProveedorMedix.PoderNotarialIdentificacionId) ||
                         //vRN.Property == nameof(EProveedorMedix.PoderNotarialNumIdentificacion)))
                    //vRN.Required = true; //Todos obligatorios
                else
                    vRN.Required = false;
            }

            vReglas.AddVisible(e => e.Rfc, e => e.TipoCaptura != TipoCaptura.PersonaExtranjera);
            vReglas.AddVisible(e => e.RegimenFiscalId, e => e.TipoCaptura != TipoCaptura.PersonaExtranjera);
            vReglas.AddVisible(e => e.Curp, e => e.TipoCaptura == TipoCaptura.PersonaFisica);

            vReglas.Rule(nameof(EProveedorMedix.Municipio)).Required = proveedor.TipoCaptura != TipoCaptura.PersonaExtranjera;
            vReglas.Rule(nameof(EProveedorMedix.Colonia)).Required = proveedor.TipoCaptura != TipoCaptura.PersonaExtranjera;
            vReglas.Rule(nameof(EProveedorMedix.Numero)).Required = proveedor.TipoCaptura != TipoCaptura.PersonaExtranjera;
            return vReglas.Rules;
        }
        #endregion
    }
}
