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
    public class SapGruposToleranciaController : MControllerApiPri, INSapGruposTolerancia
    {
        #region Contructores
        /// <summary>
        /// API que expone el negocio.
        /// </summary>
        public SapGruposToleranciaController(INSapGruposTolerancia nSapGruposTolerancia)
        {
            NSapGruposTolerancia = nSapGruposTolerancia;
        }
        #endregion

        #region Propiedades
        /// <summary>
        /// Negocio.
        /// </summary>
        public INSapGruposTolerancia NSapGruposTolerancia { get; }
        /// <summary>
        /// Control de mensajes.
        /// </summary>
        public IMMensajes Mensajes
        {
            get { return NSapGruposTolerancia.Mensajes; }
        }
        #endregion

        #region Funciones

        #region SapGrupoTolerancia (SapGruposTolerancia)
        /// <summary>
        /// Consulta paginada de la entidad SapGrupoTolerancia.
        /// </summary>
        public ESapGrupoToleranciaPag SapGrupoToleranciaPag(ESapGrupoToleranciaFiltro sapGrupoToleranciaFiltro)
        {
            return NSapGruposTolerancia.SapGrupoToleranciaPag(sapGrupoToleranciaFiltro);
        }
        /// <summary>
        /// Consulta por id de la entidad SapGrupoTolerancia.
        /// </summary>
        [HttpGet("{sapGrupoToleranciaId}")]
        public ESapGrupoTolerancia SapGrupoToleranciaXId(String sapGrupoToleranciaId)
        {
            return NSapGruposTolerancia.SapGrupoToleranciaXId(sapGrupoToleranciaId);
        }
        /// <summary>
        /// Consulta para combos de la entidad SapGrupoTolerancia.
        /// </summary>
        public List<MEElemento> SapGrupoToleranciaCmb()
        {
            return NSapGruposTolerancia.SapGrupoToleranciaCmb();
        }
        /// <summary>
        /// Permite insertar la entidad SapGrupoTolerancia.
        /// </summary>
        public Boolean SapGrupoToleranciaInserta(ESapGrupoTolerancia sapGrupoTolerancia)
        {
            return NSapGruposTolerancia.SapGrupoToleranciaInserta(sapGrupoTolerancia);
        }
        /// <summary>
        /// Permite actualizar la entidad SapGrupoTolerancia.
        /// </summary>
        public Boolean SapGrupoToleranciaActualiza(ESapGrupoTolerancia sapGrupoTolerancia)
        {
            return NSapGruposTolerancia.SapGrupoToleranciaActualiza(sapGrupoTolerancia);
        }
        /// <summary>
        /// Permite eliminar la entidad SapGrupoTolerancia.
        /// </summary>
        public Boolean SapGrupoToleranciaElimina(ESapGrupoTolerancia sapGrupoTolerancia)
        {
            return NSapGruposTolerancia.SapGrupoToleranciaElimina(sapGrupoTolerancia);
        }
        /// <summary>
        /// Exporta datos a Excel.
        /// </summary>
        public MEDatosArchivo SapGrupoToleranciaExporta(ESapGrupoToleranciaFiltro sapGrupoToleranciaFiltro)
        {
            return NSapGruposTolerancia.SapGrupoToleranciaExporta(sapGrupoToleranciaFiltro);
        }
        /// <summary>
        /// Reglas de negocio de la entidad SapGrupoTolerancia.
        /// </summary>
        public List<MEReglaNeg> SapGrupoToleranciaReglas()
        {
            return NSapGruposTolerancia.SapGrupoToleranciaReglas();
        }
        #endregion

        #endregion
    }
}
