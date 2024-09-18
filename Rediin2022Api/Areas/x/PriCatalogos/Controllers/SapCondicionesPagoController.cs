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
        public ESapCondicionPagoPag SapCondicionPagoPag(ESapCondicionPagoFiltro sapCondicionPagoFiltro)
        {
            return NSapCondicionesPago.SapCondicionPagoPag(sapCondicionPagoFiltro);
        }
        /// <summary>
        /// Consulta por id de la entidad SapCondicionPago.
        /// </summary>
        [HttpGet("{sapCondicionPagoId}")]
        public ESapCondicionPago SapCondicionPagoXId(String sapCondicionPagoId)
        {
            return NSapCondicionesPago.SapCondicionPagoXId(sapCondicionPagoId);
        }
        /// <summary>
        /// Consulta para combos de la entidad SapCondicionPago.
        /// </summary>
        public List<MEElemento> SapCondicionPagoCmb()
        {
            return NSapCondicionesPago.SapCondicionPagoCmb();
        }
        /// <summary>
        /// Permite insertar la entidad SapCondicionPago.
        /// </summary>
        public Boolean SapCondicionPagoInserta(ESapCondicionPago sapCondicionPago)
        {
            return NSapCondicionesPago.SapCondicionPagoInserta(sapCondicionPago);
        }
        /// <summary>
        /// Permite actualizar la entidad SapCondicionPago.
        /// </summary>
        public Boolean SapCondicionPagoActualiza(ESapCondicionPago sapCondicionPago)
        {
            return NSapCondicionesPago.SapCondicionPagoActualiza(sapCondicionPago);
        }
        /// <summary>
        /// Permite eliminar la entidad SapCondicionPago.
        /// </summary>
        public Boolean SapCondicionPagoElimina(ESapCondicionPago sapCondicionPago)
        {
            return NSapCondicionesPago.SapCondicionPagoElimina(sapCondicionPago);
        }
        /// <summary>
        /// Exporta datos a Excel.
        /// </summary>
        public MEDatosArchivo SapCondicionPagoExporta(ESapCondicionPagoFiltro sapCondicionPagoFiltro)
        {
            return NSapCondicionesPago.SapCondicionPagoExporta(sapCondicionPagoFiltro);
        }
        /// <summary>
        /// Reglas de negocio de la entidad SapCondicionPago.
        /// </summary>
        public List<MEReglaNeg> SapCondicionPagoReglas()
        {
            return NSapCondicionesPago.SapCondicionPagoReglas();
        }
        #endregion

        #endregion
    }
}
