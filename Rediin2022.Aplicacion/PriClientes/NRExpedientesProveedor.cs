using DSEntityNetX.Common.Casting;
using DSMetodNetX.Aplicacion;
using DSMetodNetX.Entidades;

using Rediin2022.Entidades.PriCatalogos;
using Rediin2022.Entidades.PriClientes;
using Rediin2022.Entidades.PriClientes.Expedientes;
using Rediin2022.Entidades.PriOperacion;
using Sisegui2020.Entidades.PriUtilerias;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Rediin2022.Aplicacion.PriClientes
{
    public class NRExpedientesProveedor : MNegRemoto, INExpedientesProveedor
    {
        #region Constructores
        public NRExpedientesProveedor(IMApiCteNeg api) : base(api)
        {
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
        public async Task<EDatosProveedor> ProveedorXUsuario(Int64 procesoOperativoIdProveedor,
                                                               Int64 usuarioId)
        {
            return await CallAsync<EDatosProveedor>(NomFn(),
                                                    procesoOperativoIdProveedor,
                                                    usuarioId);
        }
        /// <summary>
        /// Actualiza el proveedor.
        /// </summary>
        /// <param name="proveedor"></param>
        /// <returns></returns>
        public async Task<Boolean> ProveedorActualiza(string proveedor)
        {
            return await CallAsync<Boolean>(NomFn(), proveedor);
        }
        /// <summary>
        /// Pasa el expediente al siguiente estatus.
        /// </summary>
        /// <param name="conExpedienteCambioEstatus"></param>
        /// <returns></returns>
        public async Task<Boolean> ProveedorCambioEstatus(EConExpedienteCambioEstatus conExpedienteCambioEstatus)
        {
            return await CallAsync<Boolean>(NomFn(), conExpedienteCambioEstatus);
        }
        #endregion

        #region Monte Pio
        
        #endregion

        #endregion
    }
}