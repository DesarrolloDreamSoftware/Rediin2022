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
        public async Task<ESapGrupoToleranciaPag> SapGrupoToleranciaPag(ESapGrupoToleranciaFiltro sapGrupoToleranciaFiltro)
        {
            return await NSapGruposTolerancia.SapGrupoToleranciaPag(sapGrupoToleranciaFiltro);
        }
        /// <summary>
        /// Consulta por id de la entidad SapGrupoTolerancia.
        /// </summary>
        [HttpGet("{sapGrupoToleranciaId}")]
        public async Task<ESapGrupoTolerancia> SapGrupoToleranciaXId(String sapGrupoToleranciaId)
        {
            return await NSapGruposTolerancia.SapGrupoToleranciaXId(sapGrupoToleranciaId);
        }
        /// <summary>
        /// Consulta para combos de la entidad SapGrupoTolerancia.
        /// </summary>
        public async Task<List<MEElemento>> SapGrupoToleranciaCmb()
        {
            return await NSapGruposTolerancia.SapGrupoToleranciaCmb();
        }
        /// <summary>
        /// Permite insertar la entidad SapGrupoTolerancia.
        /// </summary>
        public async Task<Boolean> SapGrupoToleranciaInserta(ESapGrupoTolerancia sapGrupoTolerancia)
        {
            return await NSapGruposTolerancia.SapGrupoToleranciaInserta(sapGrupoTolerancia);
        }
        /// <summary>
        /// Permite actualizar la entidad SapGrupoTolerancia.
        /// </summary>
        public async Task<Boolean> SapGrupoToleranciaActualiza(ESapGrupoTolerancia sapGrupoTolerancia)
        {
            return await NSapGruposTolerancia.SapGrupoToleranciaActualiza(sapGrupoTolerancia);
        }
        /// <summary>
        /// Permite eliminar la entidad SapGrupoTolerancia.
        /// </summary>
        public async Task<Boolean> SapGrupoToleranciaElimina(ESapGrupoTolerancia sapGrupoTolerancia)
        {
            return await NSapGruposTolerancia.SapGrupoToleranciaElimina(sapGrupoTolerancia);
        }
        /// <summary>
        /// Exporta datos a Excel.
        /// </summary>
        public async Task<string> SapGrupoToleranciaExporta(ESapGrupoToleranciaFiltro sapGrupoToleranciaFiltro)
        {
            return await NSapGruposTolerancia.SapGrupoToleranciaExporta(sapGrupoToleranciaFiltro);
        }
        /// <summary>
        /// Reglas de negocio de la entidad SapGrupoTolerancia.
        /// </summary>
        public async Task<List<MEReglaNeg>> SapGrupoToleranciaReglas()
        {
            return await NSapGruposTolerancia.SapGrupoToleranciaReglas();
        }
        #endregion

        #endregion
    }
}
