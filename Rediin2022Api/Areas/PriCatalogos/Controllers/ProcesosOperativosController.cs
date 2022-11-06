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
        public EProcesoOperativoPag ProcesoOperativoPag(EProcesoOperativoFiltro procesoOperativoFiltro)
        {
            return NProcesosOperativos.ProcesoOperativoPag(procesoOperativoFiltro);
        }
        /// <summary>
        /// Consulta por id de la entidad ProcesoOperativo.
        /// </summary>
        [HttpGet("{procesoOperativoId}")]
        public EProcesoOperativo ProcesoOperativoXId(Int64 procesoOperativoId,
                                                     EProcesoOperativoFiltro procesoOperativoFiltro)
        {
            return NProcesosOperativos.ProcesoOperativoXId(procesoOperativoId,
                                                           procesoOperativoFiltro);
        }
        /// <summary>
        /// Consulta para combos de la entidad ProcesoOperativo.
        /// </summary>
        public List<MEElemento> ProcesoOperativoCmb()
        {
            return NProcesosOperativos.ProcesoOperativoCmb();
        }
        /// <summary>
        /// Permite insertar la entidad ProcesoOperativo.
        /// </summary>
        public Int64 ProcesoOperativoInserta(EProcesoOperativo procesoOperativo)
        {
            return NProcesosOperativos.ProcesoOperativoInserta(procesoOperativo);
        }
        /// <summary>
        /// Permite actualizar la entidad ProcesoOperativo.
        /// </summary>
        public Boolean ProcesoOperativoActualiza(EProcesoOperativo procesoOperativo)
        {
            return NProcesosOperativos.ProcesoOperativoActualiza(procesoOperativo);
        }
        /// <summary>
        /// Permite eliminar la entidad ProcesoOperativo.
        /// </summary>
        public Boolean ProcesoOperativoElimina(EProcesoOperativo procesoOperativo)
        {
            return NProcesosOperativos.ProcesoOperativoElimina(procesoOperativo);
        }
        /// <summary>
        /// Reglas de negocio de la entidad ProcesoOperativo.
        /// </summary>
        public List<MEReglaNeg> ProcesoOperativoReglas()
        {
            return NProcesosOperativos.ProcesoOperativoReglas();
        }
        #endregion

        #region ProcesoOperativoCol (ProcesosOperativosCols)
        /// <summary>
        /// Consulta paginada de la entidad ProcesoOperativoCol.
        /// </summary>
        public EProcesoOperativoColPag ProcesoOperativoColPag(EProcesoOperativoColFiltro procesoOperativoColFiltro)
        {
            return NProcesosOperativos.ProcesoOperativoColPag(procesoOperativoColFiltro);
        }
        /// <summary>
        /// Consulta por id de la entidad ProcesoOperativoCol.
        /// </summary>
        [HttpGet("{procesoOperativoId}/{columnaId}")]
        public EProcesoOperativoCol ProcesoOperativoColXId(Int64 procesoOperativoId,
                                                           Int64 columnaId)
        {
            return NProcesosOperativos.ProcesoOperativoColXId(procesoOperativoId,
                                                              columnaId);
        }
        /// <summary>
        /// Consulta adicional de la entidad ProcesoOperativoCol.
        /// </summary>
        [HttpGet("{procesoOperativoId}")]
        public List<EProcesoOperativoCol> ProcesoOperativoColCT(Int64 procesoOperativoId)
        {
            return NProcesosOperativos.ProcesoOperativoColCT(procesoOperativoId);
        }
        /// <summary>
        /// Consulta para combos de la entidad ProcesoOperativoCol.
        /// </summary>
        [HttpGet("{procesoOperativoId}")]
        public List<MEElemento> ProcesoOperativoColCmb(Int64 procesoOperativoId)
        {
            return NProcesosOperativos.ProcesoOperativoColCmb(procesoOperativoId);
        }
        /// <summary>
        /// Permite insertar la entidad ProcesoOperativoCol.
        /// </summary>
        public Int64 ProcesoOperativoColInserta(EProcesoOperativoCol procesoOperativoCol)
        {
            return NProcesosOperativos.ProcesoOperativoColInserta(procesoOperativoCol);
        }
        /// <summary>
        /// Permite actualizar la entidad ProcesoOperativoCol.
        /// </summary>
        public Boolean ProcesoOperativoColActualiza(EProcesoOperativoCol procesoOperativoCol)
        {
            return NProcesosOperativos.ProcesoOperativoColActualiza(procesoOperativoCol);
        }
        /// <summary>
        /// Permite eliminar la entidad ProcesoOperativoCol.
        /// </summary>
        public Boolean ProcesoOperativoColElimina(EProcesoOperativoCol procesoOperativoCol)
        {
            return NProcesosOperativos.ProcesoOperativoColElimina(procesoOperativoCol);
        }
        /// <summary>
        /// Reglas de negocio de la entidad ProcesoOperativoCol.
        /// </summary>
        public List<MEReglaNeg> ProcesoOperativoColReglas()
        {
            return NProcesosOperativos.ProcesoOperativoColReglas();
        }
        #endregion

        #region ProcesoOperativoObjeto (ProcesosOperativosObjetos)
        /// <summary>
        /// Consulta paginada de la entidad ProcesoOperativoObjeto.
        /// </summary>
        public EProcesoOperativoObjetoPag ProcesoOperativoObjetoPag(EProcesoOperativoObjetoFiltro procesoOperativoObjetoFiltro)
        {
            return NProcesosOperativos.ProcesoOperativoObjetoPag(procesoOperativoObjetoFiltro);
        }
        /// <summary>
        /// Consulta por id de la entidad ProcesoOperativoObjeto.
        /// </summary>
        [HttpGet("{procesoOperativoObjetoId}")]
        public EProcesoOperativoObjeto ProcesoOperativoObjetoXId(Int64 procesoOperativoObjetoId)
        {
            return NProcesosOperativos.ProcesoOperativoObjetoXId(procesoOperativoObjetoId);
        }
        /// <summary>
        /// Consulta para combos de la entidad ProcesoOperativoObjeto.
        /// </summary>
        [HttpGet("{procesoOperativoId}")]
        public List<MEElemento> ProcesoOperativoObjetoCmb(Int64 procesoOperativoId)
        {
            return NProcesosOperativos.ProcesoOperativoObjetoCmb(procesoOperativoId);
        }
        /// <summary>
        /// Permite insertar la entidad ProcesoOperativoObjeto.
        /// </summary>
        public Int64 ProcesoOperativoObjetoInserta(EProcesoOperativoObjeto procesoOperativoObjeto)
        {
            return NProcesosOperativos.ProcesoOperativoObjetoInserta(procesoOperativoObjeto);
        }
        /// <summary>
        /// Permite actualizar la entidad ProcesoOperativoObjeto.
        /// </summary>
        public Boolean ProcesoOperativoObjetoActualiza(EProcesoOperativoObjeto procesoOperativoObjeto)
        {
            return NProcesosOperativos.ProcesoOperativoObjetoActualiza(procesoOperativoObjeto);
        }
        /// <summary>
        /// Permite eliminar la entidad ProcesoOperativoObjeto.
        /// </summary>
        public Boolean ProcesoOperativoObjetoElimina(EProcesoOperativoObjeto procesoOperativoObjeto)
        {
            return NProcesosOperativos.ProcesoOperativoObjetoElimina(procesoOperativoObjeto);
        }
        /// <summary>
        /// Reglas de negocio de la entidad ProcesoOperativoObjeto.
        /// </summary>
        public List<MEReglaNeg> ProcesoOperativoObjetoReglas()
        {
            return NProcesosOperativos.ProcesoOperativoObjetoReglas();
        }
        #endregion

        #region ProcesoOperativoEst (ProcesosOperativosEst)
        /// <summary>
        /// Consulta paginada de la entidad ProcesoOperativoEst.
        /// </summary>
        public EProcesoOperativoEstPag ProcesoOperativoEstPag(EProcesoOperativoEstFiltro procesoOperativoEstFiltro)
        {
            return NProcesosOperativos.ProcesoOperativoEstPag(procesoOperativoEstFiltro);
        }
        /// <summary>
        /// Consulta por id de la entidad ProcesoOperativoEst.
        /// </summary>
        [HttpGet("{procesoOperativoEstId}")]
        public EProcesoOperativoEst ProcesoOperativoEstXId(Int64 procesoOperativoEstId)
        {
            return NProcesosOperativos.ProcesoOperativoEstXId(procesoOperativoEstId);
        }
        /// <summary>
        /// Consulta para combos de la entidad ProcesoOperativoEst.
        /// </summary>
        [HttpGet("{procesoOperativoId}")]
        public List<MEElemento> ProcesoOperativoEstCmb(Int64 procesoOperativoId)
        {
            return NProcesosOperativos.ProcesoOperativoEstCmb(procesoOperativoId);
        }
        /// <summary>
        /// Permite insertar la entidad ProcesoOperativoEst.
        /// </summary>
        public Int64 ProcesoOperativoEstInserta(EProcesoOperativoEst procesoOperativoEst)
        {
            return NProcesosOperativos.ProcesoOperativoEstInserta(procesoOperativoEst);
        }
        /// <summary>
        /// Permite actualizar la entidad ProcesoOperativoEst.
        /// </summary>
        public Boolean ProcesoOperativoEstActualiza(EProcesoOperativoEst procesoOperativoEst)
        {
            return NProcesosOperativos.ProcesoOperativoEstActualiza(procesoOperativoEst);
        }
        /// <summary>
        /// Permite eliminar la entidad ProcesoOperativoEst.
        /// </summary>
        public Boolean ProcesoOperativoEstElimina(EProcesoOperativoEst procesoOperativoEst)
        {
            return NProcesosOperativos.ProcesoOperativoEstElimina(procesoOperativoEst);
        }
        /// <summary>
        /// Reglas de negocio de la entidad ProcesoOperativoEst.
        /// </summary>
        public List<MEReglaNeg> ProcesoOperativoEstReglas()
        {
            return NProcesosOperativos.ProcesoOperativoEstReglas();
        }
        #endregion

        #region ProcesoOperativoEstSec (ProcesosOperativosEstSec)
        /// <summary>
        /// Consulta paginada de la entidad ProcesoOperativoEstSec.
        /// </summary>
        public EProcesoOperativoEstSecPag ProcesoOperativoEstSecPag(EProcesoOperativoEstSecFiltro procesoOperativoEstSecFiltro)
        {
            return NProcesosOperativos.ProcesoOperativoEstSecPag(procesoOperativoEstSecFiltro);
        }
        /// <summary>
        /// Consulta por id de la entidad ProcesoOperativoEstSec.
        /// </summary>
        [HttpGet("{procesoOperativoEstSecId}")]
        public EProcesoOperativoEstSec ProcesoOperativoEstSecXId(Int64 procesoOperativoEstSecId)
        {
            return NProcesosOperativos.ProcesoOperativoEstSecXId(procesoOperativoEstSecId);
        }
        /// <summary>
        /// Consulta adicional de la entidad ProcesoOperativoEstSec.
        /// </summary>
        [HttpGet("{procesoOperativoEstId}")]
        public List<EProcesoOperativoEstSec> ProcesoOperativoEstSecCTXIdPadre(Int64 procesoOperativoEstId)
        {
            return NProcesosOperativos.ProcesoOperativoEstSecCTXIdPadre(procesoOperativoEstId);
        }
        /// <summary>
        /// Permite insertar la entidad ProcesoOperativoEstSec.
        /// </summary>
        public Int64 ProcesoOperativoEstSecInserta(EProcesoOperativoEstSec procesoOperativoEstSec)
        {
            return NProcesosOperativos.ProcesoOperativoEstSecInserta(procesoOperativoEstSec);
        }
        /// <summary>
        /// Permite actualizar la entidad ProcesoOperativoEstSec.
        /// </summary>
        public Boolean ProcesoOperativoEstSecActualiza(EProcesoOperativoEstSec procesoOperativoEstSec)
        {
            return NProcesosOperativos.ProcesoOperativoEstSecActualiza(procesoOperativoEstSec);
        }
        /// <summary>
        /// Permite eliminar la entidad ProcesoOperativoEstSec.
        /// </summary>
        public Boolean ProcesoOperativoEstSecElimina(EProcesoOperativoEstSec procesoOperativoEstSec)
        {
            return NProcesosOperativos.ProcesoOperativoEstSecElimina(procesoOperativoEstSec);
        }
        /// <summary>
        /// Reglas de negocio de la entidad ProcesoOperativoEstSec.
        /// </summary>
        public List<MEReglaNeg> ProcesoOperativoEstSecReglas()
        {
            return NProcesosOperativos.ProcesoOperativoEstSecReglas();
        }
        #endregion

        #endregion
    }
}
