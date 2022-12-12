using DSMetodNetX.Api.Seguridad;
using DSMetodNetX.Entidades;
using DSMetodNetX.Mvc;
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
using System.Threading;

namespace Rediin2022Api.Areas.PriClientes.Controllers
{
    [Route("ApiV1/PriClientes/[controller]/[action]")]
    public class ExpedientesController : MControllerApiPub, INExpedientes
    {
        #region Contructores
        public ExpedientesController(INExpedientes nExpedientes)
        {
            NExpedientes = nExpedientes;
        }
        #endregion

        #region Propiedades
        public INExpedientes NExpedientes { get; }
        public IMMensajes Mensajes
        {
            get { return NExpedientes.Mensajes; }
        }
        #endregion

        #region Funciones

        #region Funciones para el cliente
        [HttpPost]
        public Int64 ExpedienteInserta(EExpediente expediente)
        {
            return NExpedientes.ExpedienteInserta(expediente);
        }
        [HttpPost]
        public Boolean ExpedienteActualiza(EExpediente expediente)
        {
            return NExpedientes.ExpedienteActualiza(expediente);
        }
        [HttpGet]
        public Boolean ExpedienteElimina(Int64 expedienteId)
        {
            return NExpedientes.ExpedienteElimina(expedienteId);
        }
        [HttpPost]
        public Int64 ObjetoInserta(EExpedienteObjeto expedienteObjeto)
        {
            //Calculamos la ruta
            String vEntidad = "Expendientes";
            String vRutaBase = MValorConfig<String>("DirBD");
            expedienteObjeto.Ruta =
                Path.Combine(vRutaBase,
                             vEntidad,
                             expedienteObjeto.ExpedienteId.ToString());

            String vRutaYNombre = Path.Combine(expedienteObjeto.Ruta, expedienteObjeto.ArchivoNombre);

            if (!System.IO.Directory.Exists(Path.Combine(vRutaBase, vEntidad)))
                System.IO.Directory.CreateDirectory(Path.Combine(vRutaBase, vEntidad));

            if (!System.IO.Directory.Exists(expedienteObjeto.Ruta))
                System.IO.Directory.CreateDirectory(expedienteObjeto.Ruta);

            if (System.IO.File.Exists(vRutaYNombre))
            {
                Mensajes.AddError("EL nombre del arhivo ya existe, no se puede insertar.");
                return 0L;
            }

            //Insertamos en bd
            Int64 vExpedienteObjetoId = NExpedientes.ObjetoInserta(expedienteObjeto);
            if (!Mensajes.Ok)
                return 0L;

            //Subimos el archivo
            using var vMS = new MemoryStream(expedienteObjeto.Archivo);
            MUtilMvc.RecibeArchivoDeCliente(HttpContext.Request,
                                            NExpedientes.Mensajes,
                                            new FormFile(vMS, 0, vMS.Length, String.Empty, expedienteObjeto.ArchivoNombre),
                                            vRutaYNombre);

            if (!Mensajes.Ok)
            {
                Mensajes.AddError("No se puedo insertar el archivo.");
                return 0L;
            }

            return vExpedienteObjetoId;
        }
        [HttpPost]
        /// <summary>
        /// Sube el documento y modifica su nombre.
		/// Es necesario cargar los campos ExpedienteId, ExpedienteObjetoId, ArchivoNombre y Archivo
		/// </summary>
		/// <param name="documento"></param>
		/// <returns></returns>
        public Boolean ObjetoActualiza(EExpedienteObjeto expedienteObjeto)
        {
            //Calculamos la ruta
            String vEntidad = "Expendientes";
            String vRutaBase = MValorConfig<String>("DirBD");
            String vRuta = Path.Combine(vRutaBase, vEntidad);

            try
            {
                if (!System.IO.Directory.Exists(vRuta))
                    System.IO.Directory.CreateDirectory(vRuta);

                vRuta = Path.Combine(vRuta, expedienteObjeto.ExpedienteId.ToString());
                if (!System.IO.Directory.Exists(vRuta))
                    System.IO.Directory.CreateDirectory(vRuta);

                vRuta = Path.Combine(vRuta, expedienteObjeto.ArchivoNombre);
                if (System.IO.File.Exists(vRuta))
                    System.IO.File.Delete(vRuta);

                if (NExpedientes.ObjetoActualiza(expedienteObjeto))
                    System.IO.File.WriteAllBytes(vRuta, expedienteObjeto.Archivo);
            }
            catch (Exception e)
            {
                Mensajes.AddError(e.Message);
            }

            return Mensajes.Ok;
        }
        /// <summary>
        /// Descargar solo el documento
        /// </summary>
        /// <param name="expendienteId"></param>
        /// <param name="archivoNombre"></param>
        /// <returns></returns>
        [HttpGet("{expendienteId}/{archivoNombre}")]
        public EDocumento ObjectoDescargaDocto(Int64 expendienteId, String archivoNombre)
        {
            EDocumento vDoc = new EDocumento();
            vDoc.ExpedienteId = expendienteId;
            vDoc.ArchivoNombre = archivoNombre;

            String vEntidad = "Expendientes";
            String vRutaBase = MValorConfig<String>("DirBD");
            String vRuta = Path.Combine(vRutaBase, vEntidad, expendienteId.ToString(), archivoNombre);
            if (System.IO.File.Exists(vRuta))
                vDoc.Documento = System.IO.File.ReadAllBytes(vRuta);
            else
                Mensajes.AddError($"El documento no existe [{vRuta}].");

            return vDoc;
        }
        [HttpPost]
        public List<MEElemento> ConExpedienteCmb(EExpendienteDatCmb expendienteDatCmb)
        {
            return NExpedientes.ConExpedienteCmb(expendienteDatCmb);
        }
        #endregion

        //No config Proveedor
        #region Funciones especificas para un proc operativo
        /// <summary>
        /// Regresa los datos del proveedor segun el usuario autentificado 
        /// para el proceso operativo especifico de proveedores.
        /// </summary>
        /// <param name="usuarioId"></param>
        /// <returns></returns>
        [HttpGet("{procesoOperativoIdProveedor}/{usuarioId}")]
        public EDatosProveedor ProveedorXUsuario(Int64 procesoOperativoIdProveedor,
                                                    Int64 usuarioId)
        {
            return NExpedientes.ProveedorXUsuario(procesoOperativoIdProveedor,
                                                  usuarioId);
        }
        /// <summary>
        /// Actualiza el proveedor.
        /// </summary>
        /// <param name="proveedor"></param>
        /// <returns></returns>
        [HttpPost]
        public Boolean ProveedorActualiza(EProveedor proveedor)
        {
            return NExpedientes.ProveedorActualiza(proveedor);
        }
        /// <summary>
        /// Pasa el expediente al siguiente estatus.
        /// </summary>
        /// <param name="conExpedienteCambioEstatus"></param>
        /// <returns></returns>
        [HttpPost]
        public Boolean ProveedorCambioEstatus(EConExpedienteCambioEstatus conExpedienteCambioEstatus)
        {
            return NExpedientes.ProveedorCambioEstatus(conExpedienteCambioEstatus);
        }
        /// <summary>
        /// Relaciones de las ColumnaId con una Propiedad para los procesos operativos que se fijan.
        /// </summary>
        /// <param name="procesoOperativoId"></param>
        /// <returns></returns>
        [HttpGet("{procesoOperativoId}")]
        public List<ERelacionProcOper> RelacionProcesoOperativo(Int64 procesoOperativoId)
        {
            return NExpedientes.RelacionProcesoOperativo(procesoOperativoId);
        }
        #endregion
        //No config Proveedor

        #endregion
    }
}
