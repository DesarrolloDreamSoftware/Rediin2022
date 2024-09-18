//using DSMetodMvcNetX.Mvc;
using DSMetodNetX.Entidades;
using DSMetodNetX.Negocio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Rediin2022.Entidades.PriCatalogos;
using System;
using System.Collections.Generic;
using DSMetodNetX.Api.Seguridad;
using System.Threading.Tasks;

namespace Rediin2022Api.Areas.PriCatalogos.Controllers
{
    [Route("apiV1/PriCatalogos/[controller]/[action]")]
    public class CatalogosController : MControllerApiPri, INCatalogos
    {
        #region Contructores
        public CatalogosController(INCatalogos nCatalogos)
        {
            NCatalogos = nCatalogos;
        }
        #endregion

        #region Propiedades
        public INCatalogos NCatalogos { get; }
        public IMMensajes Mensajes
        {
            get { return NCatalogos.Mensajes; }
        }
        #endregion

        #region Funciones
        /// <summary>
        /// Consulta de combo para la tabla NCProcesosOperativosEst.
        /// </summary>
        [HttpGet("{procesoOperativoId}")]
        public async Task<List<MEElemento>> ProcesoOperativoEstCmb(Int64 procesoOperativoId)
        {
            return await NCatalogos.ProcesoOperativoEstCmb(procesoOperativoId);
        }
        #endregion
    }
}