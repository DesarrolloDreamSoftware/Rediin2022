using DSMetodNetX.Entidades;
using DSMetodNetX.Negocio;
using System;
using System.Collections.Generic;

namespace Rediin2022.Entidades.PriCatalogos
{
    public interface INProcesosOperativos : IMCtrMensajes
    {
        #region Funciones

        #region ProcesoOperativo (ProcesosOperativos)
        /// <summary>
        /// Consulta paginada de la entidad ProcesoOperativo.
        /// </summary>
        EProcesoOperativoPag ProcesoOperativoPag(EProcesoOperativoFiltro procesoOperativoFiltro);
        /// <summary>
        /// Consulta por id de la entidad ProcesoOperativo.
        /// </summary>
        EProcesoOperativo ProcesoOperativoXId(Int64 procesoOperativoId,
                                              EProcesoOperativoFiltro procesoOperativoFiltro);
        /// <summary>
        /// Consulta para combos de la entidad ProcesoOperativo.
        /// </summary>
        List<MEElemento> ProcesoOperativoCmb();
        /// <summary>
        /// Permite insertar la entidad ProcesoOperativo.
        /// </summary>
        Int64 ProcesoOperativoInserta(EProcesoOperativo procesoOperativo);
        /// <summary>
        /// Permite actualizar la entidad ProcesoOperativo.
        /// </summary>
        Boolean ProcesoOperativoActualiza(EProcesoOperativo procesoOperativo);
        /// <summary>
        /// Permite eliminar la entidad ProcesoOperativo.
        /// </summary>
        Boolean ProcesoOperativoElimina(EProcesoOperativo procesoOperativo);
        /// <summary>
        /// Reglas de negocio de la entidad ProcesoOperativo.
        /// </summary>
        List<MEReglaNeg> ProcesoOperativoReglas();
        #endregion

        #region ProcesoOperativoCol (ProcesosOperativosCols)
        /// <summary>
        /// Consulta paginada de la entidad ProcesoOperativoCol.
        /// </summary>
        EProcesoOperativoColPag ProcesoOperativoColPag(EProcesoOperativoColFiltro procesoOperativoColFiltro);
        /// <summary>
        /// Consulta por id de la entidad ProcesoOperativoCol.
        /// </summary>
        EProcesoOperativoCol ProcesoOperativoColXId(Int64 procesoOperativoId,
                                                    Int64 columnaId);
        /// <summary>
        /// Consulta adicional de la entidad ProcesoOperativoCol.
        /// </summary>
        List<EProcesoOperativoCol> ProcesoOperativoColCT(Int64 procesoOperativoId);
        /// <summary>
        /// Consulta para combos de la entidad ProcesoOperativoCol.
        /// </summary>
        List<MEElemento> ProcesoOperativoColCmb(Int64 procesoOperativoId);
        /// <summary>
        /// Permite insertar la entidad ProcesoOperativoCol.
        /// </summary>
        Int64 ProcesoOperativoColInserta(EProcesoOperativoCol procesoOperativoCol);
        /// <summary>
        /// Permite actualizar la entidad ProcesoOperativoCol.
        /// </summary>
        Boolean ProcesoOperativoColActualiza(EProcesoOperativoCol procesoOperativoCol);
        /// <summary>
        /// Permite eliminar la entidad ProcesoOperativoCol.
        /// </summary>
        Boolean ProcesoOperativoColElimina(EProcesoOperativoCol procesoOperativoCol);
        /// <summary>
        /// Reglas de negocio de la entidad ProcesoOperativoCol.
        /// </summary>
        List<MEReglaNeg> ProcesoOperativoColReglas();
        #endregion

        #region ProcesoOperativoObjeto (ProcesosOperativosObjetos)
        /// <summary>
        /// Consulta paginada de la entidad ProcesoOperativoObjeto.
        /// </summary>
        EProcesoOperativoObjetoPag ProcesoOperativoObjetoPag(EProcesoOperativoObjetoFiltro procesoOperativoObjetoFiltro);
        /// <summary>
        /// Consulta por id de la entidad ProcesoOperativoObjeto.
        /// </summary>
        EProcesoOperativoObjeto ProcesoOperativoObjetoXId(Int64 procesoOperativoObjetoId);
        /// <summary>
        /// Consulta para combos de la entidad ProcesoOperativoObjeto.
        /// </summary>
        List<MEElemento> ProcesoOperativoObjetoCmb(Int64 procesoOperativoId);
        /// <summary>
        /// Permite insertar la entidad ProcesoOperativoObjeto.
        /// </summary>
        Int64 ProcesoOperativoObjetoInserta(EProcesoOperativoObjeto procesoOperativoObjeto);
        /// <summary>
        /// Permite actualizar la entidad ProcesoOperativoObjeto.
        /// </summary>
        Boolean ProcesoOperativoObjetoActualiza(EProcesoOperativoObjeto procesoOperativoObjeto);
        /// <summary>
        /// Permite eliminar la entidad ProcesoOperativoObjeto.
        /// </summary>
        Boolean ProcesoOperativoObjetoElimina(EProcesoOperativoObjeto procesoOperativoObjeto);
        /// <summary>
        /// Reglas de negocio de la entidad ProcesoOperativoObjeto.
        /// </summary>
        List<MEReglaNeg> ProcesoOperativoObjetoReglas();
        #endregion

        #region ProcesoOperativoEst (ProcesosOperativosEst)
        /// <summary>
        /// Consulta paginada de la entidad ProcesoOperativoEst.
        /// </summary>
        EProcesoOperativoEstPag ProcesoOperativoEstPag(EProcesoOperativoEstFiltro procesoOperativoEstFiltro);
        /// <summary>
        /// Consulta por id de la entidad ProcesoOperativoEst.
        /// </summary>
        EProcesoOperativoEst ProcesoOperativoEstXId(Int64 procesoOperativoEstId);
        /// <summary>
        /// Consulta para combos de la entidad ProcesoOperativoEst.
        /// </summary>
        List<MEElemento> ProcesoOperativoEstCmb(Int64 procesoOperativoId);
        /// <summary>
        /// Permite insertar la entidad ProcesoOperativoEst.
        /// </summary>
        Int64 ProcesoOperativoEstInserta(EProcesoOperativoEst procesoOperativoEst);
        /// <summary>
        /// Permite actualizar la entidad ProcesoOperativoEst.
        /// </summary>
        Boolean ProcesoOperativoEstActualiza(EProcesoOperativoEst procesoOperativoEst);
        /// <summary>
        /// Permite eliminar la entidad ProcesoOperativoEst.
        /// </summary>
        Boolean ProcesoOperativoEstElimina(EProcesoOperativoEst procesoOperativoEst);
        /// <summary>
        /// Reglas de negocio de la entidad ProcesoOperativoEst.
        /// </summary>
        List<MEReglaNeg> ProcesoOperativoEstReglas();
        #endregion

        #region ProcesoOperativoEstSec (ProcesosOperativosEstSec)
        /// <summary>
        /// Consulta paginada de la entidad ProcesoOperativoEstSec.
        /// </summary>
        EProcesoOperativoEstSecPag ProcesoOperativoEstSecPag(EProcesoOperativoEstSecFiltro procesoOperativoEstSecFiltro);
        /// <summary>
        /// Consulta por id de la entidad ProcesoOperativoEstSec.
        /// </summary>
        EProcesoOperativoEstSec ProcesoOperativoEstSecXId(Int64 procesoOperativoEstSecId);
        /// <summary>
        /// Consulta adicional de la entidad ProcesoOperativoEstSec.
        /// </summary>
        List<EProcesoOperativoEstSec> ProcesoOperativoEstSecCTXIdPadre(Int64 procesoOperativoEstId);
        /// <summary>
        /// Permite insertar la entidad ProcesoOperativoEstSec.
        /// </summary>
        Int64 ProcesoOperativoEstSecInserta(EProcesoOperativoEstSec procesoOperativoEstSec);
        /// <summary>
        /// Permite actualizar la entidad ProcesoOperativoEstSec.
        /// </summary>
        Boolean ProcesoOperativoEstSecActualiza(EProcesoOperativoEstSec procesoOperativoEstSec);
        /// <summary>
        /// Permite eliminar la entidad ProcesoOperativoEstSec.
        /// </summary>
        Boolean ProcesoOperativoEstSecElimina(EProcesoOperativoEstSec procesoOperativoEstSec);
        /// <summary>
        /// Reglas de negocio de la entidad ProcesoOperativoEstSec.
        /// </summary>
        List<MEReglaNeg> ProcesoOperativoEstSecReglas();
        #endregion

        #endregion
    }
}
