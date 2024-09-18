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
    [Route("ApiV1/PriCatalogos/[controller]/[action]")]
    public class AutorizacionesController : MControllerApiPri, INAutorizaciones
    {
        #region Contructores
        public AutorizacionesController(INAutorizaciones nAutorizaciones)
        {
            NAutorizaciones = nAutorizaciones;
        }
        #endregion

        #region Propiedades
        public INAutorizaciones NAutorizaciones { get; }
        public IMMensajes Mensajes
        {
            get { return NAutorizaciones.Mensajes; }
        }
        #endregion

        #region Funciones

        #region Autorizacion (Autorizaciones)
        /// <summary>
        /// Consulta paginada de la entidad Autorizacion.
        /// </summary>
        [HttpPost]
        public async Task<EAutorizacionPag> AutorizacionPag(EAutorizacionFiltro autorizacionFiltro)
        {
            return await NAutorizaciones.AutorizacionPag(autorizacionFiltro);
        }
        /// <summary>
        /// Consulta por id de la entidad Autorizacion.
        /// </summary>
        [HttpGet("{autorizacionId}")]
        public async Task<EAutorizacion> AutorizacionXId(Int64 autorizacionId)
        {
            return await NAutorizaciones.AutorizacionXId(autorizacionId);
        }
        /// <summary>
        /// Permite insertar la entidad Autorizacion.
        /// </summary>
        [HttpPost]
        public async Task<Int64> AutorizacionInserta(EAutorizacion autorizacion)
        {
            return await NAutorizaciones.AutorizacionInserta(autorizacion);
        }
        /// <summary>
        /// Permite actualizar la entidad Autorizacion.
        /// </summary>
        [HttpPost]
        public async Task<Boolean> AutorizacionActualiza(EAutorizacion autorizacion)
        {
            return await NAutorizaciones.AutorizacionActualiza(autorizacion);
        }
        /// <summary>
        /// Permite eliminar la entidad Autorizacion.
        /// </summary>
        [HttpPost]
        public async Task<Boolean> AutorizacionElimina(EAutorizacion autorizacion)
        {
            return await NAutorizaciones.AutorizacionElimina(autorizacion);
        }
        /// <summary>
        /// Reglas de negocio de la entidad Autorizacion.
        /// </summary>
        [HttpGet]
        public async Task<List<MEReglaNeg>> AutorizacionReglas()
        {
            return await NAutorizaciones.AutorizacionReglas();
        }
        #endregion

        #region AutorizacionUsuario (AutorizacionesUsuarios)
        /// <summary>
        /// Consulta paginada de la entidad AutorizacionUsuario.
        /// </summary>
        [HttpPost]
        public async Task<EAutorizacionUsuarioPag> AutorizacionUsuarioPag(EAutorizacionUsuarioFiltro autorizacionUsuarioFiltro)
        {
            return await NAutorizaciones.AutorizacionUsuarioPag(autorizacionUsuarioFiltro);
        }
        /// <summary>
        /// Consulta por id de la entidad AutorizacionUsuario.
        /// </summary>
        [HttpGet("{autorizacionUsuarioId}")]
        public async Task<EAutorizacionUsuario> AutorizacionUsuarioXId(Int64 autorizacionUsuarioId)
        {
            return await NAutorizaciones.AutorizacionUsuarioXId(autorizacionUsuarioId);
        }
        /// <summary>
        /// Permite insertar la entidad AutorizacionUsuario.
        /// </summary>
        [HttpPost]
        public async Task<Int64> AutorizacionUsuarioInserta(EAutorizacionUsuario autorizacionUsuario)
        {
            return await NAutorizaciones.AutorizacionUsuarioInserta(autorizacionUsuario);
        }
        /// <summary>
        /// Permite actualizar la entidad AutorizacionUsuario.
        /// </summary>
        [HttpPost]
        public async Task<Boolean> AutorizacionUsuarioActualiza(EAutorizacionUsuario autorizacionUsuario)
        {
            return await NAutorizaciones.AutorizacionUsuarioActualiza(autorizacionUsuario);
        }
        /// <summary>
        /// Permite eliminar la entidad AutorizacionUsuario.
        /// </summary>
        [HttpPost]
        public async Task<Boolean> AutorizacionUsuarioElimina(EAutorizacionUsuario autorizacionUsuario)
        {
            return await NAutorizaciones.AutorizacionUsuarioElimina(autorizacionUsuario);
        }
        /// <summary>
        /// Reglas de negocio de la entidad AutorizacionUsuario.
        /// </summary>
        [HttpGet]
        public async Task<List<MEReglaNeg>> AutorizacionUsuarioReglas()
        {
            return await NAutorizaciones.AutorizacionUsuarioReglas();
        }
        #endregion

        #endregion
    }
}
