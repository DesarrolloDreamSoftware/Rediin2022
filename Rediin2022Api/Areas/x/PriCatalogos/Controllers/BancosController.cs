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
    public class BancosController : MControllerApiPri, INBancos
    {
        #region Contructores
        /// <summary>
        /// API que expone el negocio.
        /// </summary>
        public BancosController(INBancos nBancos)
        {
            NBancos = nBancos;
        }
        #endregion

        #region Propiedades
        /// <summary>
        /// Negocio.
        /// </summary>
        public INBancos NBancos { get; }
        /// <summary>
        /// Control de mensajes.
        /// </summary>
        public IMMensajes Mensajes
        {
            get { return NBancos.Mensajes; }
        }
        #endregion

        #region Funciones

        #region Banco (Bancos)
        /// <summary>
        /// Consulta paginada de la entidad Banco.
        /// </summary>
        [HttpPost]
        public EBancoPag BancoPag(EBancoFiltro bancoFiltro)
        {
            return NBancos.BancoPag(bancoFiltro);
        }
        /// <summary>
        /// Consulta por id de la entidad Banco.
        /// </summary>
        [HttpGet("{bancoId}")]
        public EBanco BancoXId(Int64 bancoId)
        {
            return NBancos.BancoXId(bancoId);
        }
        /// <summary>
        /// Consulta para combos de la entidad Banco.
        /// </summary>
        [HttpGet]
        public List<MEElemento> BancoCmb()
        {
            return NBancos.BancoCmb();
        }
        /// <summary>
        /// Permite insertar la entidad Banco.
        /// </summary>
        [HttpPost]
        public Int64 BancoInserta(EBanco banco)
        {
            return NBancos.BancoInserta(banco);
        }
        /// <summary>
        /// Permite actualizar la entidad Banco.
        /// </summary>
        [HttpPost]
        public Boolean BancoActualiza(EBanco banco)
        {
            return NBancos.BancoActualiza(banco);
        }
        /// <summary>
        /// Permite eliminar la entidad Banco.
        /// </summary>
        [HttpPost]
        public Boolean BancoElimina(EBanco banco)
        {
            return NBancos.BancoElimina(banco);
        }
        /// <summary>
        /// Exporta datos a Excel.
        /// </summary>
        [HttpPost]
        public MEDatosArchivo BancoExporta(EBancoFiltro bancoFiltro)
        {
            return NBancos.BancoExporta(bancoFiltro);
        }
        /// <summary>
        /// Reglas de negocio de la entidad Banco.
        /// </summary>
        [HttpGet]
        public List<MEReglaNeg> BancoReglas()
        {
            return NBancos.BancoReglas();
        }
        #endregion

        #endregion
    }
}
