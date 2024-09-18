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
    public class ProcesosOperativosController : MControllerApiPri, INProcesosOperativos
    {
        #region Contructores
        public ProcesosOperativosController(INProcesosOperativos nProcesosOperativos)
        {
            NProcesosOperativos = nProcesosOperativos;
        }
        #endregion

        #region Propiedades
        public INProcesosOperativos NProcesosOperativos { get; }
        public IMMensajes Mensajes
        {
            get { return NProcesosOperativos.Mensajes; }
        }
        #endregion

        #region Funciones

        #region ProcesoOperativo (ProcesosOperativos)
        /// <summary>
        /// Consulta paginada de la entidad ProcesoOperativo.
        /// </summary>
        [HttpPost]
        public async Task<EProcesoOperativoPag> ProcesoOperativoPag(EProcesoOperativoFiltro procesoOperativoFiltro)
        {
            return await NProcesosOperativos.ProcesoOperativoPag(procesoOperativoFiltro);
        }
        /// <summary>
        /// Consulta por id de la entidad ProcesoOperativo.
        /// </summary>
        [HttpGet("{procesoOperativoId}")]
        public async Task<EProcesoOperativo> ProcesoOperativoXId(Int64 procesoOperativoId,
                                                                 EProcesoOperativoFiltro procesoOperativoFiltro)
        {
            return await NProcesosOperativos.ProcesoOperativoXId(procesoOperativoId,
                                                                 procesoOperativoFiltro);
        }
        /// <summary>
        /// Consulta para combos de la entidad ProcesoOperativo.
        /// </summary>
        [HttpGet]
        public async Task<List<MEElemento>> ProcesoOperativoCmb()
        {
            return await NProcesosOperativos.ProcesoOperativoCmb();
        }
        /// <summary>
        /// Permite insertar la entidad ProcesoOperativo.
        /// </summary>
        [HttpPost]
        public async Task<Int64> ProcesoOperativoInserta(EProcesoOperativo procesoOperativo)
        {
            return await NProcesosOperativos.ProcesoOperativoInserta(procesoOperativo);
        }
        /// <summary>
        /// Permite actualizar la entidad ProcesoOperativo.
        /// </summary>
        [HttpPost]
        public async Task<Boolean> ProcesoOperativoActualiza(EProcesoOperativo procesoOperativo)
        {
            return await NProcesosOperativos.ProcesoOperativoActualiza(procesoOperativo);
        }
        /// <summary>
        /// Permite eliminar la entidad ProcesoOperativo.
        /// </summary>
        [HttpPost]
        public async Task<Boolean> ProcesoOperativoElimina(EProcesoOperativo procesoOperativo)
        {
            return await NProcesosOperativos.ProcesoOperativoElimina(procesoOperativo);
        }
        /// <summary>
        /// Reglas de negocio de la entidad ProcesoOperativo.
        /// </summary>
        [HttpGet]
        public async Task<List<MEReglaNeg>> ProcesoOperativoReglas()
        {
            return await NProcesosOperativos.ProcesoOperativoReglas();
        }
        #endregion

        #region ProcesoOperativoCol (ProcesosOperativosCols)
        /// <summary>
        /// Consulta paginada de la entidad ProcesoOperativoCol.
        /// </summary>
        [HttpPost]
        public async Task<EProcesoOperativoColPag> ProcesoOperativoColPag(EProcesoOperativoColFiltro procesoOperativoColFiltro)
        {
            return await NProcesosOperativos.ProcesoOperativoColPag(procesoOperativoColFiltro);
        }
        /// <summary>
        /// Consulta por id de la entidad ProcesoOperativoCol.
        /// </summary>
        [HttpGet("{procesoOperativoId}/{columnaId}")]
        public async Task<EProcesoOperativoCol> ProcesoOperativoColXId(Int64 procesoOperativoId,
                                                                       Int64 columnaId)
        {
            return await NProcesosOperativos.ProcesoOperativoColXId(procesoOperativoId,
                                                                    columnaId);
        }
        /// <summary>
        /// Consulta adicional de la entidad ProcesoOperativoCol.
        /// </summary>
        [HttpGet("{procesoOperativoId}")]
        public async Task<List<EProcesoOperativoCol>> ProcesoOperativoColCT(Int64 procesoOperativoId)
        {
            return await NProcesosOperativos.ProcesoOperativoColCT(procesoOperativoId);
        }
        /// <summary>
        /// Consulta para combos de la entidad ProcesoOperativoCol.
        /// </summary>
        [HttpGet("{procesoOperativoId}")]
        public async Task<List<MEElemento>> ProcesoOperativoColCmb(Int64 procesoOperativoId)
        {
            return await NProcesosOperativos.ProcesoOperativoColCmb(procesoOperativoId);
        }
        /// <summary>
        /// Permite insertar la entidad ProcesoOperativoCol.
        /// </summary>
        [HttpPost]
        public async Task<Int64> ProcesoOperativoColInserta(EProcesoOperativoCol procesoOperativoCol)
        {
            return await NProcesosOperativos.ProcesoOperativoColInserta(procesoOperativoCol);
        }
        /// <summary>
        /// Permite actualizar la entidad ProcesoOperativoCol.
        /// </summary>
        [HttpPost]
        public async Task<Boolean> ProcesoOperativoColActualiza(EProcesoOperativoCol procesoOperativoCol)
        {
            return await NProcesosOperativos.ProcesoOperativoColActualiza(procesoOperativoCol);
        }
        /// <summary>
        /// Permite eliminar la entidad ProcesoOperativoCol.
        /// </summary>
        [HttpPost]
        public async Task<Boolean> ProcesoOperativoColElimina(EProcesoOperativoCol procesoOperativoCol)
        {
            return await NProcesosOperativos.ProcesoOperativoColElimina(procesoOperativoCol);
        }
        /// <summary>
        /// Reglas de negocio de la entidad ProcesoOperativoCol.
        /// </summary>
        [HttpGet]
        public async Task<List<MEReglaNeg>> ProcesoOperativoColReglas()
        {
            return await NProcesosOperativos.ProcesoOperativoColReglas();
        }
        #endregion

        #region ProcesoOperativoObjeto (ProcesosOperativosObjetos)
        /// <summary>
        /// Consulta paginada de la entidad ProcesoOperativoObjeto.
        /// </summary>
        [HttpPost]
        public async Task<EProcesoOperativoObjetoPag> ProcesoOperativoObjetoPag(EProcesoOperativoObjetoFiltro procesoOperativoObjetoFiltro)
        {
            return await NProcesosOperativos.ProcesoOperativoObjetoPag(procesoOperativoObjetoFiltro);
        }
        /// <summary>
        /// Consulta por id de la entidad ProcesoOperativoObjeto.
        /// </summary>
        [HttpGet("{procesoOperativoObjetoId}")]
        public async Task<EProcesoOperativoObjeto> ProcesoOperativoObjetoXId(Int64 procesoOperativoObjetoId)
        {
            return await NProcesosOperativos.ProcesoOperativoObjetoXId(procesoOperativoObjetoId);
        }
        /// <summary>
        /// Consulta para combos de la entidad ProcesoOperativoObjeto.
        /// </summary>
        [HttpGet("{procesoOperativoId}")]
        public async Task<List<MEElemento>> ProcesoOperativoObjetoCmb(Int64 procesoOperativoId)
        {
            return await NProcesosOperativos.ProcesoOperativoObjetoCmb(procesoOperativoId);
        }
        /// <summary>
        /// Permite insertar la entidad ProcesoOperativoObjeto.
        /// </summary>
        [HttpPost]
        public async Task<Int64> ProcesoOperativoObjetoInserta(EProcesoOperativoObjeto procesoOperativoObjeto)
        {
            return await NProcesosOperativos.ProcesoOperativoObjetoInserta(procesoOperativoObjeto);
        }
        /// <summary>
        /// Permite actualizar la entidad ProcesoOperativoObjeto.
        /// </summary>
        [HttpPost]
        public async Task<Boolean> ProcesoOperativoObjetoActualiza(EProcesoOperativoObjeto procesoOperativoObjeto)
        {
            return await NProcesosOperativos.ProcesoOperativoObjetoActualiza(procesoOperativoObjeto);
        }
        /// <summary>
        /// Permite eliminar la entidad ProcesoOperativoObjeto.
        /// </summary>
        [HttpPost]
        public async Task<Boolean> ProcesoOperativoObjetoElimina(EProcesoOperativoObjeto procesoOperativoObjeto)
        {
            return await NProcesosOperativos.ProcesoOperativoObjetoElimina(procesoOperativoObjeto);
        }
        /// <summary>
        /// Reglas de negocio de la entidad ProcesoOperativoObjeto.
        /// </summary>
        [HttpGet]
        public async Task<List<MEReglaNeg>> ProcesoOperativoObjetoReglas()
        {
            return await NProcesosOperativos.ProcesoOperativoObjetoReglas();
        }
        #endregion

        #region ProcesoOperativoEst (ProcesosOperativosEst)
        /// <summary>
        /// Consulta paginada de la entidad ProcesoOperativoEst.
        /// </summary>
        [HttpPost]
        public async Task<EProcesoOperativoEstPag> ProcesoOperativoEstPag(EProcesoOperativoEstFiltro procesoOperativoEstFiltro)
        {
            return await NProcesosOperativos.ProcesoOperativoEstPag(procesoOperativoEstFiltro);
        }
        /// <summary>
        /// Consulta por id de la entidad ProcesoOperativoEst.
        /// </summary>
        [HttpGet("{procesoOperativoEstId}")]
        public async Task<EProcesoOperativoEst> ProcesoOperativoEstXId(Int64 procesoOperativoEstId)
        {
            return await NProcesosOperativos.ProcesoOperativoEstXId(procesoOperativoEstId);
        }
        /// <summary>
        /// Consulta para combos de la entidad ProcesoOperativoEst.
        /// </summary>
        [HttpGet("{procesoOperativoId}")]
        public async Task<List<MEElemento>> ProcesoOperativoEstCmb(Int64 procesoOperativoId)
        {
            return await NProcesosOperativos.ProcesoOperativoEstCmb(procesoOperativoId);
        }
        /// <summary>
        /// Permite insertar la entidad ProcesoOperativoEst.
        /// </summary>
        [HttpPost]
        public async Task<Int64> ProcesoOperativoEstInserta(EProcesoOperativoEst procesoOperativoEst)
        {
            return await NProcesosOperativos.ProcesoOperativoEstInserta(procesoOperativoEst);
        }
        /// <summary>
        /// Permite actualizar la entidad ProcesoOperativoEst.
        /// </summary>
        [HttpPost]
        public async Task<Boolean> ProcesoOperativoEstActualiza(EProcesoOperativoEst procesoOperativoEst)
        {
            return await NProcesosOperativos.ProcesoOperativoEstActualiza(procesoOperativoEst);
        }
        /// <summary>
        /// Permite eliminar la entidad ProcesoOperativoEst.
        /// </summary>
        [HttpPost]
        public async Task<Boolean> ProcesoOperativoEstElimina(EProcesoOperativoEst procesoOperativoEst)
        {
            return await NProcesosOperativos.ProcesoOperativoEstElimina(procesoOperativoEst);
        }
        /// <summary>
        /// Reglas de negocio de la entidad ProcesoOperativoEst.
        /// </summary>
        [HttpGet]
        public async Task<List<MEReglaNeg>> ProcesoOperativoEstReglas()
        {
            return await NProcesosOperativos.ProcesoOperativoEstReglas();
        }
        #endregion

        #region ProcesoOperativoEstSec (ProcesosOperativosEstSec)
        /// <summary>
        /// Consulta paginada de la entidad ProcesoOperativoEstSec.
        /// </summary>
        [HttpPost]
        public async Task<EProcesoOperativoEstSecPag> ProcesoOperativoEstSecPag(EProcesoOperativoEstSecFiltro procesoOperativoEstSecFiltro)
        {
            return await NProcesosOperativos.ProcesoOperativoEstSecPag(procesoOperativoEstSecFiltro);
        }
        /// <summary>
        /// Consulta por id de la entidad ProcesoOperativoEstSec.
        /// </summary>
        [HttpGet("{procesoOperativoEstSecId}")]
        public async Task<EProcesoOperativoEstSec> ProcesoOperativoEstSecXId(Int64 procesoOperativoEstSecId)
        {
            return await NProcesosOperativos.ProcesoOperativoEstSecXId(procesoOperativoEstSecId);
        }
        /// <summary>
        /// Consulta adicional de la entidad ProcesoOperativoEstSec.
        /// </summary>
        [HttpGet("{procesoOperativoEstId}")]
        public async Task<List<EProcesoOperativoEstSec>> ProcesoOperativoEstSecCTXIdPadre(Int64 procesoOperativoEstId)
        {
            return await NProcesosOperativos.ProcesoOperativoEstSecCTXIdPadre(procesoOperativoEstId);
        }
        /// <summary>
        /// Permite insertar la entidad ProcesoOperativoEstSec.
        /// </summary>
        [HttpPost]
        public async Task<Int64> ProcesoOperativoEstSecInserta(EProcesoOperativoEstSec procesoOperativoEstSec)
        {
            return await NProcesosOperativos.ProcesoOperativoEstSecInserta(procesoOperativoEstSec);
        }
        /// <summary>
        /// Permite actualizar la entidad ProcesoOperativoEstSec.
        /// </summary>
        [HttpPost]
        public async Task<Boolean> ProcesoOperativoEstSecActualiza(EProcesoOperativoEstSec procesoOperativoEstSec)
        {
            return await NProcesosOperativos.ProcesoOperativoEstSecActualiza(procesoOperativoEstSec);
        }
        /// <summary>
        /// Permite eliminar la entidad ProcesoOperativoEstSec.
        /// </summary>
        [HttpPost]
        public async Task<Boolean> ProcesoOperativoEstSecElimina(EProcesoOperativoEstSec procesoOperativoEstSec)
        {
            return await NProcesosOperativos.ProcesoOperativoEstSecElimina(procesoOperativoEstSec);
        }
        /// <summary>
        /// Reglas de negocio de la entidad ProcesoOperativoEstSec.
        /// </summary>
        [HttpGet]
        public async Task<List<MEReglaNeg>> ProcesoOperativoEstSecReglas()
        {
            return await NProcesosOperativos.ProcesoOperativoEstSecReglas();
        }
        #endregion

        #endregion
    }
}
