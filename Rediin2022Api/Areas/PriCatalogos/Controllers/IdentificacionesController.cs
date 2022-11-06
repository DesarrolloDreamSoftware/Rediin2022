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
    public class IdentificacionesController : MControllerApiPri, INIdentificaciones
    {
        #region Contructores
        /// <summary>
        /// API que expone el negocio.
        /// </summary>
        public IdentificacionesController(INIdentificaciones nIdentificaciones)
        {
            NIdentificaciones = nIdentificaciones;
        }
        #endregion

        #region Propiedades
        /// <summary>
        /// Negocio.
        /// </summary>
        public INIdentificaciones NIdentificaciones { get; }
        /// <summary>
        /// Control de mensajes.
        /// </summary>
        public IMMensajes Mensajes
        {
            get { return NIdentificaciones.Mensajes; }
        }
        #endregion

        #region Funciones

        #region Identificacion (Identificaciones)
        /// <summary>
        /// Consulta paginada de la entidad Identificacion.
        /// </summary>
        public EIdentificacionPag IdentificacionPag(EIdentificacionFiltro identificacionFiltro)
        {
            return NIdentificaciones.IdentificacionPag(identificacionFiltro);
        }
        /// <summary>
        /// Consulta por id de la entidad Identificacion.
        /// </summary>
        [HttpGet("{identificacionId}")]
        public EIdentificacion IdentificacionXId(Int64 identificacionId)
        {
            return NIdentificaciones.IdentificacionXId(identificacionId);
        }
        /// <summary>
        /// Consulta para combos de la entidad Identificacion.
        /// </summary>
        public List<MEElemento> IdentificacionCmb()
        {
            return NIdentificaciones.IdentificacionCmb();
        }
        /// <summary>
        /// Permite insertar la entidad Identificacion.
        /// </summary>
        public Int64 IdentificacionInserta(EIdentificacion identificacion)
        {
            return NIdentificaciones.IdentificacionInserta(identificacion);
        }
        /// <summary>
        /// Permite actualizar la entidad Identificacion.
        /// </summary>
        public Boolean IdentificacionActualiza(EIdentificacion identificacion)
        {
            return NIdentificaciones.IdentificacionActualiza(identificacion);
        }
        /// <summary>
        /// Permite eliminar la entidad Identificacion.
        /// </summary>
        public Boolean IdentificacionElimina(EIdentificacion identificacion)
        {
            return NIdentificaciones.IdentificacionElimina(identificacion);
        }
        /// <summary>
        /// Reglas de negocio de la entidad Identificacion.
        /// </summary>
        public List<MEReglaNeg> IdentificacionReglas()
        {
            return NIdentificaciones.IdentificacionReglas();
        }
        #endregion

        #endregion
    }
}
