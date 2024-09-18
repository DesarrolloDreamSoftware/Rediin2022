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
    public class SapCondicionesPagoController : MControllerApiPri, INSapCondicionesPago
    {
        #region Contructores
        /// <summary>
        /// API que expone el negocio.
        /// </summary>
        public SapCondicionesPagoController(INSapCondicionesPago nSapCondicionesPago)
        {
            NSapCondicionesPago = nSapCondicionesPago;
        }
        #endregion

        #region Propiedades
        /// <summary>
        /// Negocio.
        /// </summary>
        public INSapCondicionesPago NSapCondicionesPago { get; }
        /// <summary>
        /// Control de mensajes.
        /// </summary>
        public IMMensajes Mensajes
        {
            get { return NSapCondicionesPago.Mensajes; }
        }
        #endregion

        #region Funciones

        #region SapCondicionPago (SapCondicionesPago)
        /// <summary>
        /// Consulta paginada de la entidad SapCondicionPago.
        /// </summary>
        public async Task<ESapCondicionPagoPag> SapCondicionPagoPag(ESapCondicionPagoFiltro sapCondicionPagoFiltro)
        {
            return await NSapCondicionesPago.SapCondicionPagoPag(sapCondicionPagoFiltro);
        }
        /// <summary>
        /// Consulta por id de la entidad SapCondicionPago.
        /// </summary>
        [HttpGet("{sapCondicionPagoId}")]
        public async Task<ESapCondicionPago> SapCondicionPagoXId(String sapCondicionPagoId)
        {
            return await NSapCondicionesPago.SapCondicionPagoXId(sapCondicionPagoId);
        }
        /// <summary>
        /// Consulta para combos de la entidad SapCondicionPago.
        /// </summary>
        public async Task<List<MEElemento>> SapCondicionPagoCmb()
        {
            return await NSapCondicionesPago.SapCondicionPagoCmb();
        }
        /// <summary>
        /// Permite insertar la entidad SapCondicionPago.
        /// </summary>
        public async Task<Boolean> SapCondicionPagoInserta(ESapCondicionPago sapCondicionPago)
        {
            return await NSapCondicionesPago.SapCondicionPagoInserta(sapCondicionPago);
        }
        /// <summary>
        /// Permite actualizar la entidad SapCondicionPago.
        /// </summary>
        public async Task<Boolean> SapCondicionPagoActualiza(ESapCondicionPago sapCondicionPago)
        {
            return await NSapCondicionesPago.SapCondicionPagoActualiza(sapCondicionPago);
        }
        /// <summary>
        /// Permite eliminar la entidad SapCondicionPago.
        /// </summary>
        public async Task<Boolean> SapCondicionPagoElimina(ESapCondicionPago sapCondicionPago)
        {
            return await NSapCondicionesPago.SapCondicionPagoElimina(sapCondicionPago);
        }
        /// <summary>
        /// Exporta datos a Excel.
        /// </summary>
        public async Task<String> SapCondicionPagoExporta(ESapCondicionPagoFiltro sapCondicionPagoFiltro)
        {
            return await NSapCondicionesPago.SapCondicionPagoExporta(sapCondicionPagoFiltro);
        }
        /// <summary>
        /// Reglas de negocio de la entidad SapCondicionPago.
        /// </summary>
        public async Task<List<MEReglaNeg>> SapCondicionPagoReglas()
        {
            return await NSapCondicionesPago.SapCondicionPagoReglas();
        }
        #endregion

        #endregion
    }
}
