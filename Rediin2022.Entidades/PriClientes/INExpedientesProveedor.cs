using DSEntityNetX.Entities.Common;
using DSMetodNetX.Entidades;
using Rediin2022.Entidades.PriOperacion;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rediin2022.Entidades.PriClientes;

public interface INExpedientesProveedor : IMCtrMensajes
{
    #region Funciones

    #region Proveedor
    /// <summary>
    /// Regresa los datos del proveedor segun el usuario autentificado 
    /// para el proceso operativo especifico de proveedores.
    /// </summary>
    /// <param name="usuarioId"></param>
    /// <returns></returns>
    Task<EDatosProveedor> ProveedorXUsuario(Int64 procesoOperativoIdProveedor, Int64 usuarioId);
    /// <summary>
    /// Actualiza el proveedor.
    /// </summary>
    /// <param name="proveedor"></param>
    /// <returns></returns>
    Task<Boolean> ProveedorActualiza(EString proveedor);
    /// <summary>
    /// Pasa el expediente al siguiente estatus.
    /// </summary>
    /// <param name="conExpedienteCambioEstatus"></param>
    /// <returns></returns>
    Task<Boolean> ProveedorCambioEstatus(EConExpedienteCambioEstatus conExpedienteCambioEstatus);
    #endregion

    #region MontePio
   
    #endregion

    #endregion
}
