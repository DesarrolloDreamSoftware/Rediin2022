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
    public class NRExpedientes : MNegRemoto, INExpedientes
    {
        #region Constructores
        public NRExpedientes(IMApiCteNeg api) : base(api)
        {
        }
        #endregion

        #region Funciones

        #region Funciones para el cliente
        public async Task<Int64> ExpedienteInserta(EExpediente expediente)
        {
            return await CallAsync<Int64>(NomFn(), expediente);
        }
        public async Task<Boolean> ExpedienteActualiza(EExpediente expediente)
        {
            return await CallAsync<bool>(NomFn(), expediente);
        }
        public async Task<Boolean> ExpedienteElimina(Int64 expedienteId)
        {
            return await CallAsync<Boolean>(NomFn(), expedienteId);
        }
        public async Task<Int64> ObjetoInserta(EExpedienteObjeto expedienteObjeto)
        {
            return await CallAsync<Int64>(NomFn(), expedienteObjeto);
        }
        /// <summary>
        /// Sube el documento y modifica su nombre.
        /// Es necesario cargar los campos ExpedienteId, ExpedienteObjetoId, ArchivoNombre y Archivo
        /// </summary>
        /// <param name="documento"></param>
        /// <returns></returns>
        public async Task<Boolean> ObjetoActualiza(EExpedienteObjeto expedienteObjeto)
        {
            return await CallAsync<Boolean>(NomFn(), expedienteObjeto);
        }
        /// <summary>
        /// Descargar solo el documento
        /// </summary>
        /// <param name="expendienteId"></param>
        /// <param name="archivoNombre"></param>
        /// <returns></returns>
        public async Task<EDocumento> ObjectoDescargaDocto(Int64 expendienteId, String archivoNombre)
        {
            return await CallAsync<EDocumento>(NomFn(), expendienteId, archivoNombre);
        }
        /// <summary>
        /// Listados para cargar los combos con expedientes de procesos operativos que son catalogos
        /// </summary>
        /// <param name="expendienteDatCmb"></param>
        /// <returns></returns>
        public async Task<List<MEElemento>> ConExpedienteCmb(EExpendienteDatCmb expendienteDatCmb)
        {
            return await CallAsync<List<MEElemento>>(NomFn(), expendienteDatCmb);
        }
        #endregion

        //ProcesoOperativoIdMontePio
        #region Funciones especificas para Monte Pio
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
        public async Task<Boolean> ProveedorActualiza(EProveedorMontePio proveedor)
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
        ///// <summary>
        ///// Relaciones de las ColumnaId con una Propiedad para los procesos operativos que se fijan.
        ///// </summary>
        ///// <param name="procesoOperativoId"></param>
        ///// <returns></returns>
        //public async Task<List<ERelacionProcOper>> RelacionProcesoOperativo(Int64 procesoOperativoId)
        //{
        //    return await CallAsync<List<ERelacionProcOper>>(NomFn(), procesoOperativoId);
        //}
        #endregion
        //ProcesoOperativoIdMontePio

        #endregion
    }
}