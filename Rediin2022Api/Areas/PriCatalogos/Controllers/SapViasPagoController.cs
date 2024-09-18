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
    public class SapViasPagoController : MControllerApiPri, INSapViasPago
    {
        #region Contructores
        /// <summary>
        /// API que expone el negocio.
        /// </summary>
        public SapViasPagoController(INSapViasPago nSapViasPago)
        {
            NSapViasPago = nSapViasPago;
        }
        #endregion

        #region Propiedades
        /// <summary>
        /// Negocio.
        /// </summary>
        public INSapViasPago NSapViasPago { get; }
        /// <summary>
        /// Control de mensajes.
        /// </summary>
        public IMMensajes Mensajes
        {
            get { return NSapViasPago.Mensajes; }
        }
        #endregion

        #region Funciones

        #region SapViaPago (SapViasPago)
        /// <summary>
        /// Consulta paginada de la entidad SapViaPago.
        /// </summary>
        public async Task<ESapViaPagoPag> SapViaPagoPag(ESapViaPagoFiltro sapViaPagoFiltro)
        {
            return await NSapViasPago.SapViaPagoPag(sapViaPagoFiltro);
        }
        /// <summary>
        /// Consulta por id de la entidad SapViaPago.
        /// </summary>
        [HttpGet("{sapViaPagoId}")]
        public async Task<ESapViaPago> SapViaPagoXId(String sapViaPagoId)
        {
            return await NSapViasPago.SapViaPagoXId(sapViaPagoId);
        }
        /// <summary>
        /// Consulta para combos de la entidad SapViaPago.
        /// </summary>
        public async Task<List<MEElemento>> SapViaPagoCmb()
        {
            return await NSapViasPago.SapViaPagoCmb();
        }
        /// <summary>
        /// Permite insertar la entidad SapViaPago.
        /// </summary>
        public async Task<Boolean> SapViaPagoInserta(ESapViaPago sapViaPago)
        {
            return await NSapViasPago.SapViaPagoInserta(sapViaPago);
        }
        /// <summary>
        /// Permite actualizar la entidad SapViaPago.
        /// </summary>
        public async Task<Boolean> SapViaPagoActualiza(ESapViaPago sapViaPago)
        {
            return await NSapViasPago.SapViaPagoActualiza(sapViaPago);
        }
        /// <summary>
        /// Permite eliminar la entidad SapViaPago.
        /// </summary>
        public async Task<Boolean> SapViaPagoElimina(ESapViaPago sapViaPago)
        {
            return await NSapViasPago.SapViaPagoElimina(sapViaPago);
        }
        /// <summary>
        /// Exporta datos a Excel.
        /// </summary>
        public async Task<string> SapViaPagoExporta(ESapViaPagoFiltro sapViaPagoFiltro)
        {
            return await NSapViasPago.SapViaPagoExporta(sapViaPagoFiltro);
        }
        /// <summary>
        /// Reglas de negocio de la entidad SapViaPago.
        /// </summary>
        public async Task<List<MEReglaNeg>> SapViaPagoReglas()
        {
            return await NSapViasPago.SapViaPagoReglas();
        }
        #endregion

        #endregion
    }
}
