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
        public ESapCuentaAsociadaPag SapCuentaAsociadaPag(ESapCuentaAsociadaFiltro sapCuentaAsociadaFiltro)
        {
            return NSapCuentasAsociadas.SapCuentaAsociadaPag(sapCuentaAsociadaFiltro);
        }
        /// <summary>
        /// Consulta por id de la entidad SapCuentaAsociada.
        /// </summary>
        [HttpGet("{sapCuentaAsociadaId}")]
        public ESapCuentaAsociada SapCuentaAsociadaXId(String sapCuentaAsociadaId)
        {
            return NSapCuentasAsociadas.SapCuentaAsociadaXId(sapCuentaAsociadaId);
        }
        /// <summary>
        /// Consulta para combos de la entidad SapCuentaAsociada.
        /// </summary>
        public List<MEElemento> SapCuentaAsociadaCmb()
        {
            return NSapCuentasAsociadas.SapCuentaAsociadaCmb();
        }
        /// <summary>
        /// Permite insertar la entidad SapCuentaAsociada.
        /// </summary>
        public Boolean SapCuentaAsociadaInserta(ESapCuentaAsociada sapCuentaAsociada)
        {
            return NSapCuentasAsociadas.SapCuentaAsociadaInserta(sapCuentaAsociada);
        }
        /// <summary>
        /// Permite actualizar la entidad SapCuentaAsociada.
        /// </summary>
        public Boolean SapCuentaAsociadaActualiza(ESapCuentaAsociada sapCuentaAsociada)
        {
            return NSapCuentasAsociadas.SapCuentaAsociadaActualiza(sapCuentaAsociada);
        }
        /// <summary>
        /// Permite eliminar la entidad SapCuentaAsociada.
        /// </summary>
        public Boolean SapCuentaAsociadaElimina(ESapCuentaAsociada sapCuentaAsociada)
        {
            return NSapCuentasAsociadas.SapCuentaAsociadaElimina(sapCuentaAsociada);
        }
        /// <summary>
        /// Exporta datos a Excel.
        /// </summary>
        public MEDatosArchivo SapCuentaAsociadaExporta(ESapCuentaAsociadaFiltro sapCuentaAsociadaFiltro)
        {
            return NSapCuentasAsociadas.SapCuentaAsociadaExporta(sapCuentaAsociadaFiltro);
        }
        /// <summary>
        /// Reglas de negocio de la entidad SapCuentaAsociada.
        /// </summary>
        public List<MEReglaNeg> SapCuentaAsociadaReglas()
        {
            return NSapCuentasAsociadas.SapCuentaAsociadaReglas();
        }
        #endregion

        #endregion
    }
}
