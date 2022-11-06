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
        public EAutorizacionPag AutorizacionPag(EAutorizacionFiltro autorizacionFiltro)
        {
            return NAutorizaciones.AutorizacionPag(autorizacionFiltro);
        }
        /// <summary>
        /// Consulta por id de la entidad Autorizacion.
        /// </summary>
        [HttpGet("{autorizacionId}")]
        public EAutorizacion AutorizacionXId(Int64 autorizacionId)
        {
            return NAutorizaciones.AutorizacionXId(autorizacionId);
        }
        /// <summary>
        /// Permite insertar la entidad Autorizacion.
        /// </summary>
        public Int64 AutorizacionInserta(EAutorizacion autorizacion)
        {
            return NAutorizaciones.AutorizacionInserta(autorizacion);
        }
        /// <summary>
        /// Permite actualizar la entidad Autorizacion.
        /// </summary>
        public Boolean AutorizacionActualiza(EAutorizacion autorizacion)
        {
            return NAutorizaciones.AutorizacionActualiza(autorizacion);
        }
        /// <summary>
        /// Permite eliminar la entidad Autorizacion.
        /// </summary>
        public Boolean AutorizacionElimina(EAutorizacion autorizacion)
        {
            return NAutorizaciones.AutorizacionElimina(autorizacion);
        }
        /// <summary>
        /// Reglas de negocio de la entidad Autorizacion.
        /// </summary>
        public List<MEReglaNeg> AutorizacionReglas()
        {
            return NAutorizaciones.AutorizacionReglas();
        }
        #endregion

        #region AutorizacionUsuario (AutorizacionesUsuarios)
        /// <summary>
        /// Consulta paginada de la entidad AutorizacionUsuario.
        /// </summary>
        public EAutorizacionUsuarioPag AutorizacionUsuarioPag(EAutorizacionUsuarioFiltro autorizacionUsuarioFiltro)
        {
            return NAutorizaciones.AutorizacionUsuarioPag(autorizacionUsuarioFiltro);
        }
        /// <summary>
        /// Consulta por id de la entidad AutorizacionUsuario.
        /// </summary>
        [HttpGet("{autorizacionUsuarioId}")]
        public EAutorizacionUsuario AutorizacionUsuarioXId(Int64 autorizacionUsuarioId)
        {
            return NAutorizaciones.AutorizacionUsuarioXId(autorizacionUsuarioId);
        }
        /// <summary>
        /// Permite insertar la entidad AutorizacionUsuario.
        /// </summary>
        public Int64 AutorizacionUsuarioInserta(EAutorizacionUsuario autorizacionUsuario)
        {
            return NAutorizaciones.AutorizacionUsuarioInserta(autorizacionUsuario);
        }
        /// <summary>
        /// Permite actualizar la entidad AutorizacionUsuario.
        /// </summary>
        public Boolean AutorizacionUsuarioActualiza(EAutorizacionUsuario autorizacionUsuario)
        {
            return NAutorizaciones.AutorizacionUsuarioActualiza(autorizacionUsuario);
        }
        /// <summary>
        /// Permite eliminar la entidad AutorizacionUsuario.
        /// </summary>
        public Boolean AutorizacionUsuarioElimina(EAutorizacionUsuario autorizacionUsuario)
        {
            return NAutorizaciones.AutorizacionUsuarioElimina(autorizacionUsuario);
        }
        /// <summary>
        /// Reglas de negocio de la entidad AutorizacionUsuario.
        /// </summary>
        public List<MEReglaNeg> AutorizacionUsuarioReglas()
        {
            return NAutorizaciones.AutorizacionUsuarioReglas();
        }
        #endregion

        #endregion
    }
}
