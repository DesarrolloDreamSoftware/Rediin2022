using DSMetodNetX.Api.Seguridad;
using DSMetodNetX.Entidades;
using DSMetodNetX.Negocio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Rediin2022.Entidades.PriCatalogos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rediin2022Api.Areas.PriCatalogos.Controllers
{
    /// <summary>
    /// API que expone el negocio.
    /// </summary>
    [Route("ApiV1/PriCatalogos/[controller]/[action]")]
    public class SapBancosController : MControllerApiPri, INSapBancos
    {
        #region Contructores
        /// <summary>
        /// API que expone el negocio.
        /// </summary>
        public SapBancosController(INSapBancos nSapBancos)
        {
            NSapBancos = nSapBancos;
        }
        #endregion

        #region Propiedades
        /// <summary>
        /// Negocio.
        /// </summary>
        public INSapBancos NSapBancos { get; }
        /// <summary>
        /// Control de mensajes.
        /// </summary>
        public IMMensajes Mensajes
        {
            get { return NSapBancos.Mensajes; }
        }
        #endregion

        #region Funciones

        #region SapBanco (SapBancos)
        /// <summary>
        /// Consulta paginada de la entidad SapBanco.
        /// </summary>
        public async Task<ESapBancoPag> SapBancoPag(ESapBancoFiltro sapBancoFiltro)
        {
            return await NSapBancos.SapBancoPag(sapBancoFiltro);
        }
        /// <summary>
        /// Consulta por id de la entidad SapBanco.
        /// </summary>
        [HttpGet("{sapBancoId}")]
        public async Task<ESapBanco> SapBancoXId(String sapBancoId)
        {
            return await NSapBancos.SapBancoXId(sapBancoId);
        }
        /// <summary>
        /// Consulta para combos de la entidad SapBanco.
        /// </summary>
        public async Task<List<MEElemento>> SapBancoCmb()
        {
            return await NSapBancos.SapBancoCmb();
        }
        /// <summary>
        /// Permite insertar la entidad SapBanco.
        /// </summary>
        public async Task<Boolean> SapBancoInserta(ESapBanco sapBanco)
        {
            return await NSapBancos.SapBancoInserta(sapBanco);
        }
        /// <summary>
        /// Permite actualizar la entidad SapBanco.
        /// </summary>
        public async Task<Boolean> SapBancoActualiza(ESapBanco sapBanco)
        {
            return await NSapBancos.SapBancoActualiza(sapBanco);
        }
        /// <summary>
        /// Permite eliminar la entidad SapBanco.
        /// </summary>
        public async Task<Boolean> SapBancoElimina(ESapBanco sapBanco)
        {
            return await NSapBancos.SapBancoElimina(sapBanco);
        }
        /// <summary>
        /// Exporta datos a Excel.
        /// </summary>
        public async Task<string> SapBancoExporta(ESapBancoFiltro sapBancoFiltro)
        {
            return await NSapBancos.SapBancoExporta(sapBancoFiltro);
        }
        /// <summary>
        /// Reglas de negocio de la entidad SapBanco.
        /// </summary>
        public async Task<List<MEReglaNeg>> SapBancoReglas()
        {
            return await NSapBancos.SapBancoReglas();
        }
        #endregion

        #endregion
    }
}
