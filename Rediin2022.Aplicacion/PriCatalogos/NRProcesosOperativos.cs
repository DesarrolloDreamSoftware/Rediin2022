using DSMetodNetX.Aplicacion;
using DSMetodNetX.Entidades;

using Rediin2022.Entidades.PriCatalogos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rediin2022.Aplicacion.PriCatalogos
{
    public class NRProcesosOperativos : MNegRemoto, INProcesosOperativos
    {
        #region Constructores
        public NRProcesosOperativos(IMApiCteNeg api) : base(api)
        {
        }
        #endregion

        #region Funciones

        #region ProcesoOperativo (ProcesosOperativos)
        /// <summary>
        /// Consulta paginada de la entidad ProcesoOperativo.
        /// </summary>
        public async Task<EProcesoOperativoPag> ProcesoOperativoPag(EProcesoOperativoFiltro procesoOperativoFiltro)
        {
            return await CallAsync<EProcesoOperativoPag>(NomFn(), procesoOperativoFiltro);
        }
        /// <summary>
        /// Consulta por id de la entidad ProcesoOperativo.
        /// </summary>
        public async Task<EProcesoOperativo> ProcesoOperativoXId(Int64 procesoOperativoId,
                                                                 EProcesoOperativoFiltro procesoOperativoFiltro)
        {
            return await CallAsync<EProcesoOperativo>(NomFn(),
                                                      procesoOperativoId,
                                                      procesoOperativoFiltro);
        }
        /// <summary>
        /// Consulta para combos de la entidad ProcesoOperativo.
        /// </summary>
        public async Task<List<MEElemento>> ProcesoOperativoCmb()
        {
            return await CallAsync<List<MEElemento>>(NomFn());
        }
        /// <summary>
        /// Permite insertar la entidad ProcesoOperativo.
        /// </summary>
        public async Task<Int64> ProcesoOperativoInserta(EProcesoOperativo procesoOperativo)
        {
            return await CallAsync<Int64>(NomFn(), procesoOperativo);
        }
        /// <summary>
        /// Permite actualizar la entidad ProcesoOperativo.
        /// </summary>
        public async Task<Boolean> ProcesoOperativoActualiza(EProcesoOperativo procesoOperativo)
        {
            return await CallAsync<Boolean>(NomFn(), procesoOperativo);
        }
        /// <summary>
        /// Permite eliminar la entidad ProcesoOperativo.
        /// </summary>
        public async Task<Boolean> ProcesoOperativoElimina(EProcesoOperativo procesoOperativo)
        {
            return await CallAsync<Boolean>(NomFn(), procesoOperativo);
        }
        /// <summary>
        /// Reglas de negocio de la entidad ProcesoOperativo.
        /// </summary>
        public async Task<List<MEReglaNeg>> ProcesoOperativoReglas()
        {
            return await CallAsync<List<MEReglaNeg>>(NomFn());
        }
        #endregion

        #region ProcesoOperativoCol (ProcesosOperativosCols)
        /// <summary>
        /// Consulta paginada de la entidad ProcesoOperativoCol.
        /// </summary>
        public async Task<EProcesoOperativoColPag> ProcesoOperativoColPag(EProcesoOperativoColFiltro procesoOperativoColFiltro)
        {
            return await CallAsync<EProcesoOperativoColPag>(NomFn(), procesoOperativoColFiltro);
        }
        /// <summary>
        /// Consulta por id de la entidad ProcesoOperativoCol.
        /// </summary>
        public async Task<EProcesoOperativoCol> ProcesoOperativoColXId(Int64 procesoOperativoId,
                                                                       Int64 columnaId)
        {
            return await CallAsync<EProcesoOperativoCol>(NomFn(),
                                                         procesoOperativoId,
                                                         columnaId);
        }
        /// <summary>
        /// Consulta adicional de la entidad ProcesoOperativoCol.
        /// </summary>
        public async Task<List<EProcesoOperativoCol>> ProcesoOperativoColCT(Int64 procesoOperativoId)
        {
            return await CallAsync<List<EProcesoOperativoCol>>(NomFn(),
                                                               procesoOperativoId);
        }
        /// <summary>
        /// Consulta para combos de la entidad ProcesoOperativoCol.
        /// </summary>
        public async Task<List<MEElemento>> ProcesoOperativoColCmb(Int64 procesoOperativoId)
        {
            return await CallAsync<List<MEElemento>>(NomFn(), procesoOperativoId);
        }
        /// <summary>
        /// Permite insertar la entidad ProcesoOperativoCol.
        /// </summary>
        public async Task<Int64> ProcesoOperativoColInserta(EProcesoOperativoCol procesoOperativoCol)
        {
            return await CallAsync<Int64>(NomFn(), procesoOperativoCol);
        }
        /// <summary>
        /// Permite actualizar la entidad ProcesoOperativoCol.
        /// </summary>
        public async Task<Boolean> ProcesoOperativoColActualiza(EProcesoOperativoCol procesoOperativoCol)
        {
            return await CallAsync<Boolean>(NomFn(), procesoOperativoCol);
        }
        /// <summary>
        /// Permite eliminar la entidad ProcesoOperativoCol.
        /// </summary>
        public async Task<Boolean> ProcesoOperativoColElimina(EProcesoOperativoCol procesoOperativoCol)
        {
            return await CallAsync<Boolean>(NomFn(), procesoOperativoCol);
        }
        /// <summary>
        /// Reglas de negocio de la entidad ProcesoOperativoCol.
        /// </summary>
        public async Task<List<MEReglaNeg>> ProcesoOperativoColReglas()
        {
            return await CallAsync<List<MEReglaNeg>>(NomFn());
        }
        #endregion

        #region ProcesoOperativoObjeto (ProcesosOperativosObjetos)
        /// <summary>
        /// Consulta paginada de la entidad ProcesoOperativoObjeto.
        /// </summary>
        public async Task<EProcesoOperativoObjetoPag> ProcesoOperativoObjetoPag(EProcesoOperativoObjetoFiltro procesoOperativoObjetoFiltro)
        {
            return await CallAsync<EProcesoOperativoObjetoPag>(NomFn(), procesoOperativoObjetoFiltro);
        }
        /// <summary>
        /// Consulta por id de la entidad ProcesoOperativoObjeto.
        /// </summary>
        public async Task<EProcesoOperativoObjeto> ProcesoOperativoObjetoXId(Int64 procesoOperativoObjetoId)
        {
            return await CallAsync<EProcesoOperativoObjeto>(NomFn(),
                                                            procesoOperativoObjetoId);
        }
        /// <summary>
        /// Consulta para combos de la entidad ProcesoOperativoObjeto.
        /// </summary>
        public async Task<List<MEElemento>> ProcesoOperativoObjetoCmb(Int64 procesoOperativoId)
        {
            return await CallAsync<List<MEElemento>>(NomFn(), procesoOperativoId);
        }
        /// <summary>
        /// Permite insertar la entidad ProcesoOperativoObjeto.
        /// </summary>
        public async Task<Int64> ProcesoOperativoObjetoInserta(EProcesoOperativoObjeto procesoOperativoObjeto)
        {
            return await CallAsync<Int64>(NomFn(), procesoOperativoObjeto);
        }
        /// <summary>
        /// Permite actualizar la entidad ProcesoOperativoObjeto.
        /// </summary>
        public async Task<Boolean> ProcesoOperativoObjetoActualiza(EProcesoOperativoObjeto procesoOperativoObjeto)
        {
            return await CallAsync<Boolean>(NomFn(), procesoOperativoObjeto);
        }
        /// <summary>
        /// Permite eliminar la entidad ProcesoOperativoObjeto.
        /// </summary>
        public async Task<Boolean> ProcesoOperativoObjetoElimina(EProcesoOperativoObjeto procesoOperativoObjeto)
        {
            return await CallAsync<Boolean>(NomFn(), procesoOperativoObjeto);
        }
        /// <summary>
        /// Reglas de negocio de la entidad ProcesoOperativoObjeto.
        /// </summary>
        public async Task<List<MEReglaNeg>> ProcesoOperativoObjetoReglas()
        {
            return await CallAsync<List<MEReglaNeg>>(NomFn());
        }
        #endregion

        #region ProcesoOperativoEst (ProcesosOperativosEst)
        /// <summary>
        /// Consulta paginada de la entidad ProcesoOperativoEst.
        /// </summary>
        public async Task<EProcesoOperativoEstPag> ProcesoOperativoEstPag(EProcesoOperativoEstFiltro procesoOperativoEstFiltro)
        {
            return await CallAsync<EProcesoOperativoEstPag>(NomFn(), procesoOperativoEstFiltro);
        }
        /// <summary>
        /// Consulta por id de la entidad ProcesoOperativoEst.
        /// </summary>
        public async Task<EProcesoOperativoEst> ProcesoOperativoEstXId(Int64 procesoOperativoEstId)
        {
            return await CallAsync<EProcesoOperativoEst>(NomFn(),
                                                         procesoOperativoEstId);
        }
        /// <summary>
        /// Consulta para combos de la entidad ProcesoOperativoEst.
        /// </summary>
        public async Task<List<MEElemento>> ProcesoOperativoEstCmb(Int64 procesoOperativoId)
        {
            return await CallAsync<List<MEElemento>>(NomFn(), procesoOperativoId);
        }
        /// <summary>
        /// Permite insertar la entidad ProcesoOperativoEst.
        /// </summary>
        public async Task<Int64> ProcesoOperativoEstInserta(EProcesoOperativoEst procesoOperativoEst)
        {
            return await CallAsync<Int64>(NomFn(), procesoOperativoEst);
        }
        /// <summary>
        /// Permite actualizar la entidad ProcesoOperativoEst.
        /// </summary>
        public async Task<Boolean> ProcesoOperativoEstActualiza(EProcesoOperativoEst procesoOperativoEst)
        {
            return await CallAsync<Boolean>(NomFn(), procesoOperativoEst);
        }
        /// <summary>
        /// Permite eliminar la entidad ProcesoOperativoEst.
        /// </summary>
        public async Task<Boolean> ProcesoOperativoEstElimina(EProcesoOperativoEst procesoOperativoEst)
        {
            return await CallAsync<Boolean>(NomFn(), procesoOperativoEst);
        }
        /// <summary>
        /// Reglas de negocio de la entidad ProcesoOperativoEst.
        /// </summary>
        public async Task<List<MEReglaNeg>> ProcesoOperativoEstReglas()
        {
            return await CallAsync<List<MEReglaNeg>>(NomFn());
        }
        #endregion

        #region ProcesoOperativoEstSec (ProcesosOperativosEstSec)
        /// <summary>
        /// Consulta paginada de la entidad ProcesoOperativoEstSec.
        /// </summary>
        public async Task<EProcesoOperativoEstSecPag> ProcesoOperativoEstSecPag(EProcesoOperativoEstSecFiltro procesoOperativoEstSecFiltro)
        {
            return await CallAsync<EProcesoOperativoEstSecPag>(NomFn(), procesoOperativoEstSecFiltro);
        }
        /// <summary>
        /// Consulta por id de la entidad ProcesoOperativoEstSec.
        /// </summary>
        public async Task<EProcesoOperativoEstSec> ProcesoOperativoEstSecXId(Int64 procesoOperativoEstSecId)
        {
            return await CallAsync<EProcesoOperativoEstSec>(NomFn(),
                                                            procesoOperativoEstSecId);
        }
        /// <summary>
        /// Consulta adicional de la entidad ProcesoOperativoEstSec.
        /// </summary>
        public async Task<List<EProcesoOperativoEstSec>> ProcesoOperativoEstSecCTXIdPadre(Int64 procesoOperativoEstId)
        {
            return await CallAsync<List<EProcesoOperativoEstSec>>(NomFn(),
                                                                  procesoOperativoEstId);
        }
        /// <summary>
        /// Permite insertar la entidad ProcesoOperativoEstSec.
        /// </summary>
        public async Task<Int64> ProcesoOperativoEstSecInserta(EProcesoOperativoEstSec procesoOperativoEstSec)
        {
            return await CallAsync<Int64>(NomFn(), procesoOperativoEstSec);
        }
        /// <summary>
        /// Permite actualizar la entidad ProcesoOperativoEstSec.
        /// </summary>
        public async Task<Boolean> ProcesoOperativoEstSecActualiza(EProcesoOperativoEstSec procesoOperativoEstSec)
        {
            return await CallAsync<Boolean>(NomFn(), procesoOperativoEstSec);
        }
        /// <summary>
        /// Permite eliminar la entidad ProcesoOperativoEstSec.
        /// </summary>
        public async Task<Boolean> ProcesoOperativoEstSecElimina(EProcesoOperativoEstSec procesoOperativoEstSec)
        {
            return await CallAsync<Boolean>(NomFn(), procesoOperativoEstSec);
        }
        /// <summary>
        /// Reglas de negocio de la entidad ProcesoOperativoEstSec.
        /// </summary>
        public async Task<List<MEReglaNeg>> ProcesoOperativoEstSecReglas()
        {
            return await CallAsync<List<MEReglaNeg>>(NomFn());
        }
        #endregion

        #endregion
    }
}
