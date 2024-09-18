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
    public class SapCuentasAsociadasController : MControllerApiPri, INSapCuentasAsociadas
    {
        #region Contructores
        /// <summary>
        /// API que expone el negocio.
        /// </summary>
        public SapCuentasAsociadasController(INSapCuentasAsociadas nSapCuentasAsociadas)
        {
            NSapCuentasAsociadas = nSapCuentasAsociadas;
        }
        #endregion

        #region Propiedades
        /// <summary>
        /// Negocio.
        /// </summary>
        public INSapCuentasAsociadas NSapCuentasAsociadas { get; }
        /// <summary>
        /// Control de mensajes.
        /// </summary>
        public IMMensajes Mensajes
        {
            get { return NSapCuentasAsociadas.Mensajes; }
        }
        #endregion

        #region Funciones

        #region SapCuentaAsociada (SapCuentasAsociadas)
        /// <summary>
        /// Consulta paginada de la entidad SapCuentaAsociada.
        /// </summary>
        public async Task<ESapCuentaAsociadaPag> SapCuentaAsociadaPag(ESapCuentaAsociadaFiltro sapCuentaAsociadaFiltro)
        {
            return await NSapCuentasAsociadas.SapCuentaAsociadaPag(sapCuentaAsociadaFiltro);
        }
        /// <summary>
        /// Consulta por id de la entidad SapCuentaAsociada.
        /// </summary>
        [HttpGet("{sapCuentaAsociadaId}")]
        public async Task<ESapCuentaAsociada> SapCuentaAsociadaXId(String sapCuentaAsociadaId)
        {
            return await NSapCuentasAsociadas.SapCuentaAsociadaXId(sapCuentaAsociadaId);
        }
        /// <summary>
        /// Consulta para combos de la entidad SapCuentaAsociada.
        /// </summary>
        public async Task<List<MEElemento>> SapCuentaAsociadaCmb()
        {
            return await NSapCuentasAsociadas.SapCuentaAsociadaCmb();
        }
        /// <summary>
        /// Permite insertar la entidad SapCuentaAsociada.
        /// </summary>
        public async Task<Boolean> SapCuentaAsociadaInserta(ESapCuentaAsociada sapCuentaAsociada)
        {
            return await NSapCuentasAsociadas.SapCuentaAsociadaInserta(sapCuentaAsociada);
        }
        /// <summary>
        /// Permite actualizar la entidad SapCuentaAsociada.
        /// </summary>
        public async Task<Boolean> SapCuentaAsociadaActualiza(ESapCuentaAsociada sapCuentaAsociada)
        {
            return await NSapCuentasAsociadas.SapCuentaAsociadaActualiza(sapCuentaAsociada);
        }
        /// <summary>
        /// Permite eliminar la entidad SapCuentaAsociada.
        /// </summary>
        public async Task<Boolean> SapCuentaAsociadaElimina(ESapCuentaAsociada sapCuentaAsociada)
        {
            return await NSapCuentasAsociadas.SapCuentaAsociadaElimina(sapCuentaAsociada);
        }
        /// <summary>
        /// Exporta datos a Excel.
        /// </summary>
        public async Task<string> SapCuentaAsociadaExporta(ESapCuentaAsociadaFiltro sapCuentaAsociadaFiltro)
        {
            return await NSapCuentasAsociadas.SapCuentaAsociadaExporta(sapCuentaAsociadaFiltro);
        }
        /// <summary>
        /// Reglas de negocio de la entidad SapCuentaAsociada.
        /// </summary>
        public async Task<List<MEReglaNeg>> SapCuentaAsociadaReglas()
        {
            return await NSapCuentasAsociadas.SapCuentaAsociadaReglas();
        }
        #endregion

        #endregion
    }
}
