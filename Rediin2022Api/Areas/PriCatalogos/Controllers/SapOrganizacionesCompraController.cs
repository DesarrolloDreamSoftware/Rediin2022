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
    public class SapOrganizacionesCompraController : MControllerApiPri, INSapOrganizacionesCompra
    {
        #region Contructores
        /// <summary>
        /// API que expone el negocio.
        /// </summary>
        public SapOrganizacionesCompraController(INSapOrganizacionesCompra nSapOrganizacionesCompra)
        {
            NSapOrganizacionesCompra = nSapOrganizacionesCompra;
        }
        #endregion

        #region Propiedades
        /// <summary>
        /// Negocio.
        /// </summary>
        public INSapOrganizacionesCompra NSapOrganizacionesCompra { get; }
        /// <summary>
        /// Control de mensajes.
        /// </summary>
        public IMMensajes Mensajes
        {
            get { return NSapOrganizacionesCompra.Mensajes; }
        }
        #endregion

        #region Funciones

        #region SapOrganizacionCompra (SapOrganizacionesCompra)
        /// <summary>
        /// Consulta paginada de la entidad SapOrganizacionCompra.
        /// </summary>
        public async Task<ESapOrganizacionCompraPag> SapOrganizacionCompraPag(ESapOrganizacionCompraFiltro sapOrganizacionCompraFiltro)
        {
            return await NSapOrganizacionesCompra.SapOrganizacionCompraPag(sapOrganizacionCompraFiltro);
        }
        /// <summary>
        /// Consulta por id de la entidad SapOrganizacionCompra.
        /// </summary>
        [HttpGet("{sapOrganizacionCompraId}")]
        public async Task<ESapOrganizacionCompra> SapOrganizacionCompraXId(String sapOrganizacionCompraId)
        {
            return await NSapOrganizacionesCompra.SapOrganizacionCompraXId(sapOrganizacionCompraId);
        }
        /// <summary>
        /// Consulta para combos de la entidad SapOrganizacionCompra.
        /// </summary>
        public async Task<List<MEElemento>> SapOrganizacionCompraCmb()
        {
            return await NSapOrganizacionesCompra.SapOrganizacionCompraCmb();
        }
        /// <summary>
        /// Permite insertar la entidad SapOrganizacionCompra.
        /// </summary>
        public async Task<Boolean> SapOrganizacionCompraInserta(ESapOrganizacionCompra sapOrganizacionCompra)
        {
            return await NSapOrganizacionesCompra.SapOrganizacionCompraInserta(sapOrganizacionCompra);
        }
        /// <summary>
        /// Permite actualizar la entidad SapOrganizacionCompra.
        /// </summary>
        public async Task<Boolean> SapOrganizacionCompraActualiza(ESapOrganizacionCompra sapOrganizacionCompra)
        {
            return await NSapOrganizacionesCompra.SapOrganizacionCompraActualiza(sapOrganizacionCompra);
        }
        /// <summary>
        /// Permite eliminar la entidad SapOrganizacionCompra.
        /// </summary>
        public async Task<Boolean> SapOrganizacionCompraElimina(ESapOrganizacionCompra sapOrganizacionCompra)
        {
            return await NSapOrganizacionesCompra.SapOrganizacionCompraElimina(sapOrganizacionCompra);
        }
        /// <summary>
        /// Exporta datos a Excel.
        /// </summary>
        public async Task<string> SapOrganizacionCompraExporta(ESapOrganizacionCompraFiltro sapOrganizacionCompraFiltro)
        {
            return await NSapOrganizacionesCompra.SapOrganizacionCompraExporta(sapOrganizacionCompraFiltro);
        }
        /// <summary>
        /// Reglas de negocio de la entidad SapOrganizacionCompra.
        /// </summary>
        public async Task<List<MEReglaNeg>> SapOrganizacionCompraReglas()
        {
            return await NSapOrganizacionesCompra.SapOrganizacionCompraReglas();
        }
        #endregion

        #endregion
    }
}
