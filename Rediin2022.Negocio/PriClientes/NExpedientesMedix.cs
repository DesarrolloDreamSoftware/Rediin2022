using DSEntityNetX.Entities.Common;
using DSMetodNetX.Comun;
using DSMetodNetX.Entidades;
using Rediin2022.AccesoDatos.PriClientes;
using Rediin2022.Comun.PriOperacion;
using Rediin2022.Entidades.PriCatalogos;
using Rediin2022.Entidades.PriClientes;
using Rediin2022.Entidades.PriOperacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

            //Creamos las reglas de negocio
            vDP.ReglasNegocio = new List<MEReglaNeg>();
            UtilProveedorEspecif.CargaReglasNegocioProveedor(vColMD, vDP.ReglasNegocio);

            IMReglasNeg<EProveedorMedix> vReglasAdic = Validaciones.CreaReglasNeg<EProveedorMedix>(Mensajes);
            vReglasAdic.Rules = vDP.ReglasNegocio;
            vReglasAdic.AddVisible(e => e.Rfc, e => e.TipoCaptura != TipoCaptura.PersonaExtranjera);
            vReglasAdic.AddVisible(e => e.RegimenFiscalId, e => e.TipoCaptura != TipoCaptura.PersonaExtranjera);

            vReglasAdic.AddVisible(e => e.Curp, e => e.TipoCaptura == TipoCaptura.PersonaFisica);

            EModelo vModelo = await NModelos.ModeloXId(vProveedor.ModeloId);
            if (vModelo != null)
                vProveedor.TipoCaptura = vModelo.TipoCapturaId;

            vReglasAdic.Rule(nameof(EProveedorMedix.Municipio)).Required = vProveedor.TipoCaptura != TipoCaptura.PersonaExtranjera;
            vReglasAdic.Rule(nameof(EProveedorMedix.Colonia)).Required = vProveedor.TipoCaptura != TipoCaptura.PersonaExtranjera;
            vReglasAdic.Rule(nameof(EProveedorMedix.Numero)).Required = vProveedor.TipoCaptura != TipoCaptura.PersonaExtranjera;

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
        #endregion
    }
}
