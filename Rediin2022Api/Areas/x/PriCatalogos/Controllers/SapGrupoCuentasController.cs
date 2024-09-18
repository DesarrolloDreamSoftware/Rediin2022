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
        public ESapGrupoCuentaPag SapGrupoCuentaPag(ESapGrupoCuentaFiltro sapGrupoCuentaFiltro)
        {
            return NSapGrupoCuentas.SapGrupoCuentaPag(sapGrupoCuentaFiltro);
        }
        /// <summary>
        /// Consulta por id de la entidad SapGrupoCuenta.
        /// </summary>
        [HttpGet("{sapGrupoCuentaId}")]
        public ESapGrupoCuenta SapGrupoCuentaXId(String sapGrupoCuentaId)
        {
            return NSapGrupoCuentas.SapGrupoCuentaXId(sapGrupoCuentaId);
        }
        /// <summary>
        /// Consulta para combos de la entidad SapGrupoCuenta.
        /// </summary>
        public List<MEElemento> SapGrupoCuentaCmb()
        {
            return NSapGrupoCuentas.SapGrupoCuentaCmb();
        }
        /// <summary>
        /// Permite insertar la entidad SapGrupoCuenta.
        /// </summary>
        public Boolean SapGrupoCuentaInserta(ESapGrupoCuenta sapGrupoCuenta)
        {
            return NSapGrupoCuentas.SapGrupoCuentaInserta(sapGrupoCuenta);
        }
        /// <summary>
        /// Permite actualizar la entidad SapGrupoCuenta.
        /// </summary>
        public Boolean SapGrupoCuentaActualiza(ESapGrupoCuenta sapGrupoCuenta)
        {
            return NSapGrupoCuentas.SapGrupoCuentaActualiza(sapGrupoCuenta);
        }
        /// <summary>
        /// Permite eliminar la entidad SapGrupoCuenta.
        /// </summary>
        public Boolean SapGrupoCuentaElimina(ESapGrupoCuenta sapGrupoCuenta)
        {
            return NSapGrupoCuentas.SapGrupoCuentaElimina(sapGrupoCuenta);
        }
        /// <summary>
        /// Exporta datos a Excel.
        /// </summary>
        public MEDatosArchivo SapGrupoCuentaExporta(ESapGrupoCuentaFiltro sapGrupoCuentaFiltro)
        {
            return NSapGrupoCuentas.SapGrupoCuentaExporta(sapGrupoCuentaFiltro);
        }
        /// <summary>
        /// Reglas de negocio de la entidad SapGrupoCuenta.
        /// </summary>
        public List<MEReglaNeg> SapGrupoCuentaReglas()
        {
            return NSapGrupoCuentas.SapGrupoCuentaReglas();
        }
        #endregion

        #endregion
    }
}
