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
    public class ExpedientesController : MControllerApiPub, INExpedientes
    {
        #region Variables
        private IXFile _file = null;
        #endregion

        #region Contructores
        public ExpedientesController(INExpedientes nExpedientes,
                                     IXFile xFile)
        {
            NExpedientes = nExpedientes;
            _file = xFile;
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
        public async Task<Int64> ExpedienteInserta(EExpediente expediente)
        {
            return await NExpedientes.ExpedienteInserta(expediente);
        }
        [HttpPost]
        public async Task<Boolean> ExpedienteActualiza(EExpediente expediente)
        {
            return await NExpedientes.ExpedienteActualiza(expediente);
        }
        [HttpGet]
        public async Task<Boolean> ExpedienteElimina(Int64 expedienteId)
        {
            return await NExpedientes.ExpedienteElimina(expedienteId);
        }
        [HttpPost]
        public async Task<Int64> ObjetoInserta(EExpedienteObjeto expedienteObjeto)
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
            Int64 vExpedienteObjetoId = await NExpedientes.ObjetoInserta(expedienteObjeto);
            if (!Mensajes.Ok)
                return 0L;

            //JRD VERFICAR
            await _file.InsertOrUpdateAsync(expedienteObjeto.Archivo, vRutaYNombre);

            ////Subimos el archivo
            //using var vMS = new MemoryStream(expedienteObjeto.Archivo);

            //MUtilMvc.RecibeArchivoDeCliente(HttpContext.Request,
            //                                NExpedientes.Mensajes,
            //                                new FormFile(vMS, 0, vMS.Length, String.Empty, expedienteObjeto.ArchivoNombre),
            //                                vRutaYNombre);
            //JRD FIN VERFICAR

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
        public async Task<Boolean> ObjetoActualiza(EExpedienteObjeto expedienteObjeto)
        {
            //Calculamos la ruta
            String vEntidad = "Expendientes";
            String vRutaBase = MValorConfig<String>("DirBD");
            String vRuta = Path.Combine(vRutaBase, vEntidad);
            expedienteObjeto.Ruta = Path.Combine(vRuta, expedienteObjeto.ExpedienteId.ToString());

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

                if (await NExpedientes.ObjetoActualiza(expedienteObjeto) &&
                    expedienteObjeto.Archivo != null)
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
        public async Task<EDocumento> ObjectoDescargaDocto(Int64 expendienteId, String archivoNombre)
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

            return await Task.FromResult(vDoc);
        }
        [HttpPost]
        public async Task<List<MEElemento>> ConExpedienteCmb(EExpendienteDatCmb expendienteDatCmb)
        {
            return await NExpedientes.ConExpedienteCmb(expendienteDatCmb);
        }
        #endregion

        #endregion
    }
}
