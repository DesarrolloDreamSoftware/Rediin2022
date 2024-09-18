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
    public class SapGrupoCuentasController : MControllerApiPri, INSapGrupoCuentas
    {
        #region Contructores
        /// <summary>
        /// API que expone el negocio.
        /// </summary>
        public SapGrupoCuentasController(INSapGrupoCuentas nSapGrupoCuentas)
        {
            NSapGrupoCuentas = nSapGrupoCuentas;
        }
        #endregion

        #region Propiedades
        /// <summary>
        /// Negocio.
        /// </summary>
        public INSapGrupoCuentas NSapGrupoCuentas { get; }
        /// <summary>
        /// Control de mensajes.
        /// </summary>
        public IMMensajes Mensajes
        {
            get { return NSapGrupoCuentas.Mensajes; }
        }
        #endregion

        #region Funciones

        #region SapGrupoCuenta (SapGrupoCuentas)
        /// <summary>
        /// Consulta paginada de la entidad SapGrupoCuenta.
        /// </summary>
        public async Task<ESapGrupoCuentaPag> SapGrupoCuentaPag(ESapGrupoCuentaFiltro sapGrupoCuentaFiltro)
        {
            return await NSapGrupoCuentas.SapGrupoCuentaPag(sapGrupoCuentaFiltro);
        }
        /// <summary>
        /// Consulta por id de la entidad SapGrupoCuenta.
        /// </summary>
        [HttpGet("{sapGrupoCuentaId}")]
        public async Task<ESapGrupoCuenta> SapGrupoCuentaXId(String sapGrupoCuentaId)
        {
            return await NSapGrupoCuentas.SapGrupoCuentaXId(sapGrupoCuentaId);
        }
        /// <summary>
        /// Consulta para combos de la entidad SapGrupoCuenta.
        /// </summary>
        public async Task<List<MEElemento>> SapGrupoCuentaCmb()
        {
            return await NSapGrupoCuentas.SapGrupoCuentaCmb();
        }
        /// <summary>
        /// Permite insertar la entidad SapGrupoCuenta.
        /// </summary>
        public async Task<Boolean> SapGrupoCuentaInserta(ESapGrupoCuenta sapGrupoCuenta)
        {
            return await NSapGrupoCuentas.SapGrupoCuentaInserta(sapGrupoCuenta);
        }
        /// <summary>
        /// Permite actualizar la entidad SapGrupoCuenta.
        /// </summary>
        public async Task<Boolean> SapGrupoCuentaActualiza(ESapGrupoCuenta sapGrupoCuenta)
        {
            return await NSapGrupoCuentas.SapGrupoCuentaActualiza(sapGrupoCuenta);
        }
        /// <summary>
        /// Permite eliminar la entidad SapGrupoCuenta.
        /// </summary>
        public async Task<Boolean> SapGrupoCuentaElimina(ESapGrupoCuenta sapGrupoCuenta)
        {
            return await NSapGrupoCuentas.SapGrupoCuentaElimina(sapGrupoCuenta);
        }
        /// <summary>
        /// Exporta datos a Excel.
        /// </summary>
        public async Task<string> SapGrupoCuentaExporta(ESapGrupoCuentaFiltro sapGrupoCuentaFiltro)
        {
            return await NSapGrupoCuentas.SapGrupoCuentaExporta(sapGrupoCuentaFiltro);
        }
        /// <summary>
        /// Reglas de negocio de la entidad SapGrupoCuenta.
        /// </summary>
        public async Task<List<MEReglaNeg>> SapGrupoCuentaReglas()
        {
            return await NSapGrupoCuentas.SapGrupoCuentaReglas();
        }
        #endregion

        #endregion
    }
}
