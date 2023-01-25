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
    public class SapGruposTesoreriaController : MControllerApiPri, INSapGruposTesoreria
    {
        #region Contructores
        /// <summary>
        /// API que expone el negocio.
        /// </summary>
        public SapGruposTesoreriaController(INSapGruposTesoreria nSapGruposTesoreria)
        {
            NSapGruposTesoreria = nSapGruposTesoreria;
        }
        #endregion

        #region Propiedades
        /// <summary>
        /// Negocio.
        /// </summary>
        public INSapGruposTesoreria NSapGruposTesoreria { get; }
        /// <summary>
        /// Control de mensajes.
        /// </summary>
        public IMMensajes Mensajes
        {
            get { return NSapGruposTesoreria.Mensajes; }
        }
        #endregion

        #region Funciones

        #region SapGrupoTesoreria (SapGruposTesoreria)
        /// <summary>
        /// Consulta paginada de la entidad SapGrupoTesoreria.
        /// </summary>
        public ESapGrupoTesoreriaPag SapGrupoTesoreriaPag(ESapGrupoTesoreriaFiltro sapGrupoTesoreriaFiltro)
        {
            return NSapGruposTesoreria.SapGrupoTesoreriaPag(sapGrupoTesoreriaFiltro);
        }
        /// <summary>
        /// Consulta por id de la entidad SapGrupoTesoreria.
        /// </summary>
        [HttpGet("{sapGrupoTesoreriaId}")]
        public ESapGrupoTesoreria SapGrupoTesoreriaXId(String sapGrupoTesoreriaId)
        {
            return NSapGruposTesoreria.SapGrupoTesoreriaXId(sapGrupoTesoreriaId);
        }
        /// <summary>
        /// Consulta para combos de la entidad SapGrupoTesoreria.
        /// </summary>
        public List<MEElemento> SapGrupoTesoreriaCmb()
        {
            return NSapGruposTesoreria.SapGrupoTesoreriaCmb();
        }
        /// <summary>
        /// Permite insertar la entidad SapGrupoTesoreria.
        /// </summary>
        public Boolean SapGrupoTesoreriaInserta(ESapGrupoTesoreria sapGrupoTesoreria)
        {
            return NSapGruposTesoreria.SapGrupoTesoreriaInserta(sapGrupoTesoreria);
        }
        /// <summary>
        /// Permite actualizar la entidad SapGrupoTesoreria.
        /// </summary>
        public Boolean SapGrupoTesoreriaActualiza(ESapGrupoTesoreria sapGrupoTesoreria)
        {
            return NSapGruposTesoreria.SapGrupoTesoreriaActualiza(sapGrupoTesoreria);
        }
        /// <summary>
        /// Permite eliminar la entidad SapGrupoTesoreria.
        /// </summary>
        public Boolean SapGrupoTesoreriaElimina(ESapGrupoTesoreria sapGrupoTesoreria)
        {
            return NSapGruposTesoreria.SapGrupoTesoreriaElimina(sapGrupoTesoreria);
        }
        /// <summary>
        /// Exporta datos a Excel.
        /// </summary>
        public MEDatosArchivo SapGrupoTesoreriaExporta(ESapGrupoTesoreriaFiltro sapGrupoTesoreriaFiltro)
        {
            return NSapGruposTesoreria.SapGrupoTesoreriaExporta(sapGrupoTesoreriaFiltro);
        }
        /// <summary>
        /// Reglas de negocio de la entidad SapGrupoTesoreria.
        /// </summary>
        public List<MEReglaNeg> SapGrupoTesoreriaReglas()
        {
            return NSapGruposTesoreria.SapGrupoTesoreriaReglas();
        }
        #endregion

        #endregion
    }
}
