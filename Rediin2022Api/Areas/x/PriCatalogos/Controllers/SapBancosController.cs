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
        public ESapBancoPag SapBancoPag(ESapBancoFiltro sapBancoFiltro)
        {
            return NSapBancos.SapBancoPag(sapBancoFiltro);
        }
        /// <summary>
        /// Consulta por id de la entidad SapBanco.
        /// </summary>
        [HttpGet("{sapBancoId}")]
        public ESapBanco SapBancoXId(String sapBancoId)
        {
            return NSapBancos.SapBancoXId(sapBancoId);
        }
        /// <summary>
        /// Consulta para combos de la entidad SapBanco.
        /// </summary>
        public List<MEElemento> SapBancoCmb()
        {
            return NSapBancos.SapBancoCmb();
        }
        /// <summary>
        /// Permite insertar la entidad SapBanco.
        /// </summary>
        public Boolean SapBancoInserta(ESapBanco sapBanco)
        {
            return NSapBancos.SapBancoInserta(sapBanco);
        }
        /// <summary>
        /// Permite actualizar la entidad SapBanco.
        /// </summary>
        public Boolean SapBancoActualiza(ESapBanco sapBanco)
        {
            return NSapBancos.SapBancoActualiza(sapBanco);
        }
        /// <summary>
        /// Permite eliminar la entidad SapBanco.
        /// </summary>
        public Boolean SapBancoElimina(ESapBanco sapBanco)
        {
            return NSapBancos.SapBancoElimina(sapBanco);
        }
        /// <summary>
        /// Exporta datos a Excel.
        /// </summary>
        public MEDatosArchivo SapBancoExporta(ESapBancoFiltro sapBancoFiltro)
        {
            return NSapBancos.SapBancoExporta(sapBancoFiltro);
        }
        /// <summary>
        /// Reglas de negocio de la entidad SapBanco.
        /// </summary>
        public List<MEReglaNeg> SapBancoReglas()
        {
            return NSapBancos.SapBancoReglas();
        }
        #endregion

        #endregion
    }
}
