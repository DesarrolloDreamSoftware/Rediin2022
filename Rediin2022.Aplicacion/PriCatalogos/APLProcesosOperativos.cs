using DSMetodNetX.Aplicacion;
using DSMetodNetX.Entidades;
using DSMetodNetX.Negocio;
using Rediin2022.Entidades.PriCatalogos;
using System;
using System.Collections.Generic;

namespace Rediin2022.Aplicacion.PriCatalogos
{
    public class APLProcesosOperativos : MAplicacion, INProcesosOperativos
    {
        #region Constructores
        public APLProcesosOperativos(IMApiCliente api) : base(api)
        {
        }
        #endregion

        #region Funciones

        #region ProcesoOperativo (ProcesosOperativos)
        /// <summary>
        /// Consulta paginada de la entidad ProcesoOperativo.
        /// </summary>
        public EProcesoOperativoPag ProcesoOperativoPag(EProcesoOperativoFiltro procesoOperativoFiltro)
        {
            return Call<EProcesoOperativoPag>(NomFn(), procesoOperativoFiltro);
        }
        /// <summary>
        /// Consulta por id de la entidad ProcesoOperativo.
        /// </summary>
        public EProcesoOperativo ProcesoOperativoXId(Int64 procesoOperativoId,
                                                     EProcesoOperativoFiltro procesoOperativoFiltro)
        {
            return Call<EProcesoOperativo>(NomFn(),
                                           procesoOperativoId,
                                           procesoOperativoFiltro);
        }
        /// <summary>
        /// Consulta para combos de la entidad ProcesoOperativo.
        /// </summary>
        public List<MEElemento> ProcesoOperativoCmb()
        {
            return Call<List<MEElemento>>(NomFn());
        }
        /// <summary>
        /// Permite insertar la entidad ProcesoOperativo.
        /// </summary>
        public Int64 ProcesoOperativoInserta(EProcesoOperativo procesoOperativo)
        {
            return Call<Int64>(NomFn(), procesoOperativo);
        }
        /// <summary>
        /// Permite actualizar la entidad ProcesoOperativo.
        /// </summary>
        public Boolean ProcesoOperativoActualiza(EProcesoOperativo procesoOperativo)
        {
            return Call<Boolean>(NomFn(), procesoOperativo);
        }
        /// <summary>
        /// Permite eliminar la entidad ProcesoOperativo.
        /// </summary>
        public Boolean ProcesoOperativoElimina(EProcesoOperativo procesoOperativo)
        {
            return Call<Boolean>(NomFn(), procesoOperativo);
        }
        /// <summary>
        /// Reglas de negocio de la entidad ProcesoOperativo.
        /// </summary>
        public List<MEReglaNeg> ProcesoOperativoReglas()
        {
            return Call<List<MEReglaNeg>>(NomFn());
        }
        #endregion

        #region ProcesoOperativoCol (ProcesosOperativosCols)
        /// <summary>
        /// Consulta paginada de la entidad ProcesoOperativoCol.
        /// </summary>
        public EProcesoOperativoColPag ProcesoOperativoColPag(EProcesoOperativoColFiltro procesoOperativoColFiltro)
        {
            return Call<EProcesoOperativoColPag>(NomFn(), procesoOperativoColFiltro);
        }
        /// <summary>
        /// Consulta por id de la entidad ProcesoOperativoCol.
        /// </summary>
        public EProcesoOperativoCol ProcesoOperativoColXId(Int64 procesoOperativoId,
                                                           Int64 columnaId)
        {
            return Call<EProcesoOperativoCol>(NomFn(),
                                              procesoOperativoId,
                                              columnaId);
        }
        /// <summary>
        /// Consulta adicional de la entidad ProcesoOperativoCol.
        /// </summary>
        public List<EProcesoOperativoCol> ProcesoOperativoColCT(Int64 procesoOperativoId)
        {
            return Call<List<EProcesoOperativoCol>>(NomFn(),
                                                    procesoOperativoId);
        }
        /// <summary>
        /// Consulta para combos de la entidad ProcesoOperativoCol.
        /// </summary>
        public List<MEElemento> ProcesoOperativoColCmb(Int64 procesoOperativoId)
        {
            return Call<List<MEElemento>>(NomFn(), procesoOperativoId);
        }
        /// <summary>
        /// Permite insertar la entidad ProcesoOperativoCol.
        /// </summary>
        public Int64 ProcesoOperativoColInserta(EProcesoOperativoCol procesoOperativoCol)
        {
            return Call<Int64>(NomFn(), procesoOperativoCol);
        }
        /// <summary>
        /// Permite actualizar la entidad ProcesoOperativoCol.
        /// </summary>
        public Boolean ProcesoOperativoColActualiza(EProcesoOperativoCol procesoOperativoCol)
        {
            return Call<Boolean>(NomFn(), procesoOperativoCol);
        }
        /// <summary>
        /// Permite eliminar la entidad ProcesoOperativoCol.
        /// </summary>
        public Boolean ProcesoOperativoColElimina(EProcesoOperativoCol procesoOperativoCol)
        {
            return Call<Boolean>(NomFn(), procesoOperativoCol);
        }
        /// <summary>
        /// Reglas de negocio de la entidad ProcesoOperativoCol.
        /// </summary>
        public List<MEReglaNeg> ProcesoOperativoColReglas()
        {
            return Call<List<MEReglaNeg>>(NomFn());
        }
        #endregion

        #region ProcesoOperativoObjeto (ProcesosOperativosObjetos)
        /// <summary>
        /// Consulta paginada de la entidad ProcesoOperativoObjeto.
        /// </summary>
        public EProcesoOperativoObjetoPag ProcesoOperativoObjetoPag(EProcesoOperativoObjetoFiltro procesoOperativoObjetoFiltro)
        {
            return Call<EProcesoOperativoObjetoPag>(NomFn(), procesoOperativoObjetoFiltro);
        }
        /// <summary>
        /// Consulta por id de la entidad ProcesoOperativoObjeto.
        /// </summary>
        public EProcesoOperativoObjeto ProcesoOperativoObjetoXId(Int64 procesoOperativoObjetoId)
        {
            return Call<EProcesoOperativoObjeto>(NomFn(),
                                                 procesoOperativoObjetoId);
        }
        /// <summary>
        /// Consulta para combos de la entidad ProcesoOperativoObjeto.
        /// </summary>
        public List<MEElemento> ProcesoOperativoObjetoCmb(Int64 procesoOperativoId)
        {
            return Call<List<MEElemento>>(NomFn(), procesoOperativoId);
        }
        /// <summary>
        /// Permite insertar la entidad ProcesoOperativoObjeto.
        /// </summary>
        public Int64 ProcesoOperativoObjetoInserta(EProcesoOperativoObjeto procesoOperativoObjeto)
        {
            return Call<Int64>(NomFn(), procesoOperativoObjeto);
        }
        /// <summary>
        /// Permite actualizar la entidad ProcesoOperativoObjeto.
        /// </summary>
        public Boolean ProcesoOperativoObjetoActualiza(EProcesoOperativoObjeto procesoOperativoObjeto)
        {
            return Call<Boolean>(NomFn(), procesoOperativoObjeto);
        }
        /// <summary>
        /// Permite eliminar la entidad ProcesoOperativoObjeto.
        /// </summary>
        public Boolean ProcesoOperativoObjetoElimina(EProcesoOperativoObjeto procesoOperativoObjeto)
        {
            return Call<Boolean>(NomFn(), procesoOperativoObjeto);
        }
        /// <summary>
        /// Reglas de negocio de la entidad ProcesoOperativoObjeto.
        /// </summary>
        public List<MEReglaNeg> ProcesoOperativoObjetoReglas()
        {
            return Call<List<MEReglaNeg>>(NomFn());
        }
        #endregion

        #region ProcesoOperativoEst (ProcesosOperativosEst)
        /// <summary>
        /// Consulta paginada de la entidad ProcesoOperativoEst.
        /// </summary>
        public EProcesoOperativoEstPag ProcesoOperativoEstPag(EProcesoOperativoEstFiltro procesoOperativoEstFiltro)
        {
            return Call<EProcesoOperativoEstPag>(NomFn(), procesoOperativoEstFiltro);
        }
        /// <summary>
        /// Consulta por id de la entidad ProcesoOperativoEst.
        /// </summary>
        public EProcesoOperativoEst ProcesoOperativoEstXId(Int64 procesoOperativoEstId)
        {
            return Call<EProcesoOperativoEst>(NomFn(),
                                              procesoOperativoEstId);
        }
        /// <summary>
        /// Consulta para combos de la entidad ProcesoOperativoEst.
        /// </summary>
        public List<MEElemento> ProcesoOperativoEstCmb(Int64 procesoOperativoId)
        {
            return Call<List<MEElemento>>(NomFn(), procesoOperativoId);
        }
        /// <summary>
        /// Permite insertar la entidad ProcesoOperativoEst.
        /// </summary>
        public Int64 ProcesoOperativoEstInserta(EProcesoOperativoEst procesoOperativoEst)
        {
            return Call<Int64>(NomFn(), procesoOperativoEst);
        }
        /// <summary>
        /// Permite actualizar la entidad ProcesoOperativoEst.
        /// </summary>
        public Boolean ProcesoOperativoEstActualiza(EProcesoOperativoEst procesoOperativoEst)
        {
            return Call<Boolean>(NomFn(), procesoOperativoEst);
        }
        /// <summary>
        /// Permite eliminar la entidad ProcesoOperativoEst.
        /// </summary>
        public Boolean ProcesoOperativoEstElimina(EProcesoOperativoEst procesoOperativoEst)
        {
            return Call<Boolean>(NomFn(), procesoOperativoEst);
        }
        /// <summary>
        /// Reglas de negocio de la entidad ProcesoOperativoEst.
        /// </summary>
        public List<MEReglaNeg> ProcesoOperativoEstReglas()
        {
            return Call<List<MEReglaNeg>>(NomFn());
        }
        #endregion

        #region ProcesoOperativoEstSec (ProcesosOperativosEstSec)
        /// <summary>
        /// Consulta paginada de la entidad ProcesoOperativoEstSec.
        /// </summary>
        public EProcesoOperativoEstSecPag ProcesoOperativoEstSecPag(EProcesoOperativoEstSecFiltro procesoOperativoEstSecFiltro)
        {
            return Call<EProcesoOperativoEstSecPag>(NomFn(), procesoOperativoEstSecFiltro);
        }
        /// <summary>
        /// Consulta por id de la entidad ProcesoOperativoEstSec.
        /// </summary>
        public EProcesoOperativoEstSec ProcesoOperativoEstSecXId(Int64 procesoOperativoEstSecId)
        {
            return Call<EProcesoOperativoEstSec>(NomFn(),
                                                 procesoOperativoEstSecId);
        }
        /// <summary>
        /// Consulta adicional de la entidad ProcesoOperativoEstSec.
        /// </summary>
        public List<EProcesoOperativoEstSec> ProcesoOperativoEstSecCTXIdPadre(Int64 procesoOperativoEstId)
        {
            return Call<List<EProcesoOperativoEstSec>>(NomFn(),
                                                       procesoOperativoEstId);
        }
        /// <summary>
        /// Permite insertar la entidad ProcesoOperativoEstSec.
        /// </summary>
        public Int64 ProcesoOperativoEstSecInserta(EProcesoOperativoEstSec procesoOperativoEstSec)
        {
            return Call<Int64>(NomFn(), procesoOperativoEstSec);
        }
        /// <summary>
        /// Permite actualizar la entidad ProcesoOperativoEstSec.
        /// </summary>
        public Boolean ProcesoOperativoEstSecActualiza(EProcesoOperativoEstSec procesoOperativoEstSec)
        {
            return Call<Boolean>(NomFn(), procesoOperativoEstSec);
        }
        /// <summary>
        /// Permite eliminar la entidad ProcesoOperativoEstSec.
        /// </summary>
        public Boolean ProcesoOperativoEstSecElimina(EProcesoOperativoEstSec procesoOperativoEstSec)
        {
            return Call<Boolean>(NomFn(), procesoOperativoEstSec);
        }
        /// <summary>
        /// Reglas de negocio de la entidad ProcesoOperativoEstSec.
        /// </summary>
        public List<MEReglaNeg> ProcesoOperativoEstSecReglas()
        {
            return Call<List<MEReglaNeg>>(NomFn());
        }
        #endregion

        #endregion
    }
}
