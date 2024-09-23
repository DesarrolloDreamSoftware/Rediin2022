using DSEntityNetX.Entities.File;
using DSMetodNetX.Api.Seguridad;
using DSMetodNetX.Entidades;
using DSMetodNetX.Negocio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Rediin2022.Entidades.Idioma;
using Rediin2022.Entidades.PriCatalogos;
using Rediin2022.Entidades.PriClientes;
using Rediin2022.Entidades.PriClientes.Expedientes;
using Rediin2022.Entidades.PriOperacion;
using Rediin2022.Negocio.PriClientes;
using Rediin2022.Negocio.PriOperacion;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Rediin2022Api.Areas.PriClientes.Controllers
{
    [Route("ApiV1/PriClientes/[controller]/[action]")]
    public class ExpedientesProveedorController : MControllerApiPub, INExpedientesProveedor
    {
        #region Variables
        private IXFile _file = null;
        #endregion

        #region Contructores
        public ExpedientesProveedorController(INExpedientesProveedor nExpedientesProveedor,
                                              IXFile xFile)
        {
            NExpedientesProveedor = nExpedientesProveedor;
            _file = xFile;
        }
        #endregion

        #region Propiedades
        public INExpedientesProveedor NExpedientesProveedor { get; }
        public IMMensajes Mensajes
        {
            get { return NExpedientesProveedor.Mensajes; }
        }
        #endregion

        #region Funciones

        #region Proveedor
        /// <summary>
        /// Regresa los datos del proveedor segun el usuario autentificado 
        /// para el proceso operativo especifico de proveedores.
        /// </summary>
        /// <param name="usuarioId"></param>
        /// <returns></returns>
        [HttpGet("{procesoOperativoIdProveedor}/{usuarioId}")]
        public async Task<EDatosProveedor> ProveedorXUsuario(Int64 procesoOperativoIdProveedor,
                                                             Int64 usuarioId)
        {
            return await NExpedientesProveedor.ProveedorXUsuario(procesoOperativoIdProveedor,
                                                                 usuarioId);
        }
        /// <summary>
        /// Actualiza el proveedor.
        /// </summary>
        /// <param name="proveedor"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Boolean> ProveedorActualiza(string proveedor)
        {
            return await NExpedientesProveedor.ProveedorActualiza(proveedor);
        }
        /// <summary>
        /// Pasa el expediente al siguiente estatus.
        /// </summary>
        /// <param name="conExpedienteCambioEstatus"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Boolean> ProveedorCambioEstatus(EConExpedienteCambioEstatus conExpedienteCambioEstatus)
        {
            return await NExpedientesProveedor.ProveedorCambioEstatus(conExpedienteCambioEstatus);
        }
        #endregion

        #region Monte Pio
       
        #endregion

        #endregion
    }
}
