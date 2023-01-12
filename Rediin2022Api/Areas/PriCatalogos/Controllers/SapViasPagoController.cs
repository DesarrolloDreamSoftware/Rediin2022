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
        public ESapViaPagoPag SapViaPagoPag(ESapViaPagoFiltro sapViaPagoFiltro)
        {
            return NSapViasPago.SapViaPagoPag(sapViaPagoFiltro);
        }
        /// <summary>
        /// Consulta por id de la entidad SapViaPago.
        /// </summary>
        [HttpGet("{sapViaPagoId}")]
        public ESapViaPago SapViaPagoXId(String sapViaPagoId)
        {
            return NSapViasPago.SapViaPagoXId(sapViaPagoId);
        }
        /// <summary>
        /// Consulta para combos de la entidad SapViaPago.
        /// </summary>
        public List<MEElemento> SapViaPagoCmb()
        {
            return NSapViasPago.SapViaPagoCmb();
        }
        /// <summary>
        /// Permite insertar la entidad SapViaPago.
        /// </summary>
        public Boolean SapViaPagoInserta(ESapViaPago sapViaPago)
        {
            return NSapViasPago.SapViaPagoInserta(sapViaPago);
        }
        /// <summary>
        /// Permite actualizar la entidad SapViaPago.
        /// </summary>
        public Boolean SapViaPagoActualiza(ESapViaPago sapViaPago)
        {
            return NSapViasPago.SapViaPagoActualiza(sapViaPago);
        }
        /// <summary>
        /// Permite eliminar la entidad SapViaPago.
        /// </summary>
        public Boolean SapViaPagoElimina(ESapViaPago sapViaPago)
        {
            return NSapViasPago.SapViaPagoElimina(sapViaPago);
        }
        /// <summary>
        /// Exporta datos a Excel.
        /// </summary>
        public MEDatosArchivo SapViaPagoExporta(ESapViaPagoFiltro sapViaPagoFiltro)
        {
            return NSapViasPago.SapViaPagoExporta(sapViaPagoFiltro);
        }
        /// <summary>
        /// Reglas de negocio de la entidad SapViaPago.
        /// </summary>
        public List<MEReglaNeg> SapViaPagoReglas()
        {
            return NSapViasPago.SapViaPagoReglas();
        }
        #endregion

        #endregion
    }
}
