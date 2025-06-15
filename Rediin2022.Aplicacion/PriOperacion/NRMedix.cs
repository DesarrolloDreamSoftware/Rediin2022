using DSMetodNetX.Entidades;
using DSMetodNetX.Entidades.Config;
using Rediin2022.Comun.PriOperacion;
using Rediin2022.Entidades.PriCatalogos;
using Rediin2022.Entidades.PriClientes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Rediin2022.Aplicacion.PriOperacion
{
    public class NRMedix : IMCtrMensajes
    {
        protected HttpClient _httpClient;
        protected IConfig _config;
        protected string _apiSapUsuario = string.Empty;
        protected string _apiSapPwd = string.Empty;
        protected string _apiSapUrl = string.Empty;
        public NRMedix(IMMensajes mensajes, HttpClient httpClient, IConfig config)
        {
            Mensajes = mensajes;
            _httpClient = httpClient;
            _config = config;
        }
        public IMMensajes Mensajes { get; }

        protected void CargaConfig()
        {
            if (string.IsNullOrWhiteSpace(_apiSapUsuario))
                _apiSapUsuario = _config.Valor<String>("MedixApiSapUsuario");
            if (string.IsNullOrWhiteSpace(_apiSapPwd))
                _apiSapPwd = _config.Valor<String>("MedixApiSapPwd");
            if (string.IsNullOrWhiteSpace(_apiSapUrl))
                _apiSapUrl = _config.Valor<String>("MedixApiSapUrl");
        }

        public async Task<EMedixApiRecibir> AltaProveedorRediin(EMedixApi datos)
        {
            CargaConfig();

            string vApiBaseJson = JsonSerializer.Serialize(datos);
            byte[] vApiBaseBytes = Encoding.UTF8.GetBytes(vApiBaseJson);
            string vApiBaseBase64 = Convert.ToBase64String(vApiBaseBytes);

            //Preparamos la entidad de envio
            EMedixApiEnviar vEnvio = new();
            vEnvio.solicitud.idApp = _apiSapUsuario;
            vEnvio.solicitud.pwdApp = _apiSapPwd;
            vEnvio.solicitud.enc_request = vApiBaseBase64;

            try
            {
                HttpResponseMessage vRes = await _httpClient.PostAsJsonAsync(_apiSapUrl, vEnvio);
                if (!vRes.IsSuccessStatusCode)
                {
                    Mensajes.AddError("Error al consultar el API.");
                    return null;
                }

                //JRD ELIMINAR DESPUES
                //string vApiRespuesta = await vRes.Content.ReadAsStringAsync();

                EMedixApiRecibir vApiRecibir = await vRes.Content.ReadFromJsonAsync<EMedixApiRecibir>();

                if (vApiRecibir.respuesta.estatus != "success")
                {
                    Mensajes.AddError(vApiRecibir.respuesta.mensaje);
                    return vApiRecibir;
                }

                if (vApiRecibir.respuesta.data == null ||
                    vApiRecibir.respuesta.data.proveedor == null ||
                    vApiRecibir.respuesta.data.proveedor.Count == 0)
                {
                    Mensajes.AddError("No se recibio el proveedor en el resultado.");
                    return vApiRecibir;
                }

                if (vApiRecibir.respuesta.data.proveedor[0].respuestaSAPEstatus != "success")
                {
                    Mensajes.AddError(vApiRecibir.respuesta.data.proveedor[0].respuestaSAPMensaje);
                    return vApiRecibir;
                }

                if (string.IsNullOrWhiteSpace(vApiRecibir.respuesta.data.proveedor[0].numeroProveedor))
                {
                    Mensajes.AddError("El numero de proveedor esta vacio.");
                    return vApiRecibir;
                }

                return vApiRecibir;
            }
            catch (Exception ex)
            {
                Mensajes.AddError("Error al llamar al API.");
                Mensajes.AddError(ex.Message);
            }

            return null;
        }
    }
}
