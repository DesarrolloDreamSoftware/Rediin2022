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
using Rediin2022.Entidades.PriOperacion;
using Rediin2022.Negocio.PriOperacion;
using System;
using System.Collections.Generic;
using System.IO;

namespace Rediin2022Api.Areas.PriClientes.Controllers
{
    [Route("ApiV1/PriClientes/[controller]/[action]")]
    public class ExpedientesController : MControllerApiPri, INExpedientes
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
        public Int64 ExpedienteInserta(EExpediente expediente)
        {
            return NExpedientes.ExpedienteInserta(expediente);
        }
        public Boolean ExpedienteElimina(Int64 expedienteId)
        {
            return NExpedientes.ExpedienteElimina(expedienteId);
        }
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

            //Guardamos en bd
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
        //public Int64 ObjetoInserta(EExpedienteObjeto expedienteObjeto)
        //{
        //    IFormFile archivoFisico = null;
        //    if (Request.Form.Files != null && Request.Form.Files.Count > 0)
        //        archivoFisico = Request.Form.Files[0];

        //    //Validacion
        //    if (!NExpedientes.ObjetoValida(expedienteObjeto))
        //        return 0L;

        //    if (archivoFisico == null)
        //        Mensajes.AddError("No se envio un archivo junto con el expediente.");

        //    if (!Mensajes.Ok)
        //        return 0L;

        //    //Subimos el archivo
        //    String vEntidad = "Expendientes";
        //    String vExtension = Path.GetExtension(archivoFisico.FileName);
        //    if (!expedienteObjeto.ArchivoNombre.EndsWith(vExtension))
        //        expedienteObjeto.ArchivoNombre += vExtension;

        //    String vRutaBase = MValorConfig<String>("DirBD");
        //    expedienteObjeto.Ruta =
        //        Path.Combine(vRutaBase,
        //                     vEntidad,
        //                     expedienteObjeto.ExpedienteId.ToString());

        //    String vRutaYNombre = Path.Combine(expedienteObjeto.Ruta, expedienteObjeto.ArchivoNombre);

        //    if (!System.IO.Directory.Exists(Path.Combine(vRutaBase, vEntidad)))
        //        System.IO.Directory.CreateDirectory(Path.Combine(vRutaBase, vEntidad));

        //    if (!System.IO.Directory.Exists(expedienteObjeto.Ruta))
        //        System.IO.Directory.CreateDirectory(expedienteObjeto.Ruta);

        //    if (System.IO.File.Exists(vRutaYNombre))
        //    {
        //        Mensajes.AddError("EL nombre del arhivo ya existe, no se puede insertar.");
        //        return 0L;
        //    }

        //    MUtilMvc.RecibeArchivoDeCliente(HttpContext.Request,
        //                                    NExpedientes.Mensajes,
        //                                    archivoFisico,
        //                                    vRutaYNombre);

        //    if (!Mensajes.Ok)
        //    {
        //        Mensajes.AddError("No se puedo insertar el archivo.");
        //        return 0L;
        //    }

        //    return NExpedientes.ObjetoInserta(expedienteObjeto);
        //}
        #endregion

        #endregion
    }
}
