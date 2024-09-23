using DSMetodNetX.Entidades;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rediin2022.Entidades.PriCatalogos;

public interface INProcesosOperativos : IMCtrMensajes
{
    #region Funciones

    #region ProcesoOperativo (ProcesosOperativos)
    /// <summary>
    /// Consulta paginada de la entidad ProcesoOperativo.
    /// </summary>
    Task<EProcesoOperativoPag> ProcesoOperativoPag(EProcesoOperativoFiltro procesoOperativoFiltro);
    /// <summary>
    /// Consulta por id de la entidad ProcesoOperativo.
    /// </summary>
    Task<EProcesoOperativo> ProcesoOperativoXId(Int64 procesoOperativoId,
                                                    EProcesoOperativoFiltro procesoOperativoFiltro);
    /// <summary>
    /// Consulta para combos de la entidad ProcesoOperativo.
    /// </summary>
    Task<List<MEElemento>> ProcesoOperativoCmb();
    /// <summary>
    /// Permite insertar la entidad ProcesoOperativo.
    /// </summary>
    Task<Int64> ProcesoOperativoInserta(EProcesoOperativo procesoOperativo);
    /// <summary>
    /// Permite actualizar la entidad ProcesoOperativo.
    /// </summary>
    Task<Boolean> ProcesoOperativoActualiza(EProcesoOperativo procesoOperativo);
    /// <summary>
    /// Permite eliminar la entidad ProcesoOperativo.
    /// </summary>
    Task<Boolean> ProcesoOperativoElimina(EProcesoOperativo procesoOperativo);
    /// <summary>
    /// Reglas de negocio de la entidad ProcesoOperativo.
    /// </summary>
    Task<List<MEReglaNeg>> ProcesoOperativoReglas();
    #endregion

    #region ProcesoOperativoCol (ProcesosOperativosCols)
    /// <summary>
    /// Consulta paginada de la entidad ProcesoOperativoCol.
    /// </summary>
    Task<EProcesoOperativoColPag> ProcesoOperativoColPag(EProcesoOperativoColFiltro procesoOperativoColFiltro);
    /// <summary>
    /// Consulta por id de la entidad ProcesoOperativoCol.
    /// </summary>
    Task<EProcesoOperativoCol> ProcesoOperativoColXId(Int64 procesoOperativoId,
                                                      Int64 columnaId);
    /// <summary>
    /// Consulta adicional de la entidad ProcesoOperativoCol.
    /// </summary>
    Task<List<EProcesoOperativoCol>> ProcesoOperativoColCT(Int64 procesoOperativoId);
    /// <summary>
    /// Consulta adicional de la entidad ProcesoOperativoCol.
    /// </summary>
    Task<List<EProcesoOperativoColMin>> ProcesoOperativoColCTMin(Int64 procesoOperativoId);
    /// <summary>
    /// Consulta para combos de la entidad ProcesoOperativoCol.
    /// </summary>
    Task<List<MEElemento>> ProcesoOperativoColCmb(Int64 procesoOperativoId);
    /// <summary>
    /// Permite insertar la entidad ProcesoOperativoCol.
    /// </summary>
    Task<Int64> ProcesoOperativoColInserta(EProcesoOperativoCol procesoOperativoCol);
    /// <summary>
    /// Permite actualizar la entidad ProcesoOperativoCol.
    /// </summary>
    Task<Boolean> ProcesoOperativoColActualiza(EProcesoOperativoCol procesoOperativoCol);
    /// <summary>
    /// Permite eliminar la entidad ProcesoOperativoCol.
    /// </summary>
    Task<Boolean> ProcesoOperativoColElimina(EProcesoOperativoCol procesoOperativoCol);
    /// <summary>
    /// Reglas de negocio de la entidad ProcesoOperativoCol.
    /// </summary>
    Task<List<MEReglaNeg>> ProcesoOperativoColReglas();
    #endregion

    #region ProcesoOperativoObjeto (ProcesosOperativosObjetos)
    /// <summary>
    /// Consulta paginada de la entidad ProcesoOperativoObjeto.
    /// </summary>
    Task<EProcesoOperativoObjetoPag> ProcesoOperativoObjetoPag(EProcesoOperativoObjetoFiltro procesoOperativoObjetoFiltro);
    /// <summary>
    /// Consulta por id de la entidad ProcesoOperativoObjeto.
    /// </summary>
    Task<EProcesoOperativoObjeto> ProcesoOperativoObjetoXId(Int64 procesoOperativoObjetoId);
    /// <summary>
    /// Consulta para combos de la entidad ProcesoOperativoObjeto.
    /// </summary>
    Task<List<MEElemento>> ProcesoOperativoObjetoCmb(Int64 procesoOperativoId);
    /// <summary>
    /// Permite insertar la entidad ProcesoOperativoObjeto.
    /// </summary>
    Task<Int64> ProcesoOperativoObjetoInserta(EProcesoOperativoObjeto procesoOperativoObjeto);
    /// <summary>
    /// Permite actualizar la entidad ProcesoOperativoObjeto.
    /// </summary>
    Task<Boolean> ProcesoOperativoObjetoActualiza(EProcesoOperativoObjeto procesoOperativoObjeto);
    /// <summary>
    /// Permite eliminar la entidad ProcesoOperativoObjeto.
    /// </summary>
    Task<Boolean> ProcesoOperativoObjetoElimina(EProcesoOperativoObjeto procesoOperativoObjeto);
    /// <summary>
    /// Reglas de negocio de la entidad ProcesoOperativoObjeto.
    /// </summary>
    Task<List<MEReglaNeg>> ProcesoOperativoObjetoReglas();
    #endregion

    #region ProcesoOperativoEst (ProcesosOperativosEst)
    /// <summary>
    /// Consulta paginada de la entidad ProcesoOperativoEst.
    /// </summary>
    Task<EProcesoOperativoEstPag> ProcesoOperativoEstPag(EProcesoOperativoEstFiltro procesoOperativoEstFiltro);
    /// <summary>
    /// Consulta por id de la entidad ProcesoOperativoEst.
    /// </summary>
    Task<EProcesoOperativoEst> ProcesoOperativoEstXId(Int64 procesoOperativoEstId);
    /// <summary>
    /// Consulta para combos de la entidad ProcesoOperativoEst.
    /// </summary>
    Task<List<MEElemento>> ProcesoOperativoEstCmb(Int64 procesoOperativoId);
    /// <summary>
    /// Permite insertar la entidad ProcesoOperativoEst.
    /// </summary>
    Task<Int64> ProcesoOperativoEstInserta(EProcesoOperativoEst procesoOperativoEst);
    /// <summary>
    /// Permite actualizar la entidad ProcesoOperativoEst.
    /// </summary>
    Task<Boolean> ProcesoOperativoEstActualiza(EProcesoOperativoEst procesoOperativoEst);
    /// <summary>
    /// Permite eliminar la entidad ProcesoOperativoEst.
    /// </summary>
    Task<Boolean> ProcesoOperativoEstElimina(EProcesoOperativoEst procesoOperativoEst);
    /// <summary>
    /// Reglas de negocio de la entidad ProcesoOperativoEst.
    /// </summary>
    Task<List<MEReglaNeg>> ProcesoOperativoEstReglas();
    #endregion

    #region ProcesoOperativoEstSec (ProcesosOperativosEstSec)
    /// <summary>
    /// Consulta paginada de la entidad ProcesoOperativoEstSec.
    /// </summary>
    Task<EProcesoOperativoEstSecPag> ProcesoOperativoEstSecPag(EProcesoOperativoEstSecFiltro procesoOperativoEstSecFiltro);
    /// <summary>
    /// Consulta por id de la entidad ProcesoOperativoEstSec.
    /// </summary>
    Task<EProcesoOperativoEstSec> ProcesoOperativoEstSecXId(Int64 procesoOperativoEstSecId);
    /// <summary>
    /// Consulta adicional de la entidad ProcesoOperativoEstSec.
    /// </summary>
    Task<List<EProcesoOperativoEstSec>> ProcesoOperativoEstSecCTXIdPadre(Int64 procesoOperativoEstId);
    /// <summary>
    /// Permite insertar la entidad ProcesoOperativoEstSec.
    /// </summary>
    Task<Int64> ProcesoOperativoEstSecInserta(EProcesoOperativoEstSec procesoOperativoEstSec);
    /// <summary>
    /// Permite actualizar la entidad ProcesoOperativoEstSec.
    /// </summary>
    Task<Boolean> ProcesoOperativoEstSecActualiza(EProcesoOperativoEstSec procesoOperativoEstSec);
    /// <summary>
    /// Permite eliminar la entidad ProcesoOperativoEstSec.
    /// </summary>
    Task<Boolean> ProcesoOperativoEstSecElimina(EProcesoOperativoEstSec procesoOperativoEstSec);
    /// <summary>
    /// Reglas de negocio de la entidad ProcesoOperativoEstSec.
    /// </summary>
    Task<List<MEReglaNeg>> ProcesoOperativoEstSecReglas();
    #endregion

    #endregion
}
