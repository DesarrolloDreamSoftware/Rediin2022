using DSMetodNetX.Entidades;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rediin2022.Entidades.PriCatalogos
{
    /// <summary>
    /// Interfaz de negocio.
    /// </summary>
    public interface INSapCuentasAsociadas : IMCtrMensajes
    {
        #region Funciones

        #region SapCuentaAsociada (SapCuentasAsociadas)
        /// <summary>
        /// Consulta paginada de la entidad SapCuentaAsociada.
        /// </summary>
        ESapCuentaAsociadaPag SapCuentaAsociadaPag(ESapCuentaAsociadaFiltro sapCuentaAsociadaFiltro);
        /// <summary>
        /// Consulta por id de la entidad SapCuentaAsociada.
        /// </summary>
        ESapCuentaAsociada SapCuentaAsociadaXId(String sapCuentaAsociadaId);
        /// <summary>
        /// Consulta para combos de la entidad SapCuentaAsociada.
        /// </summary>
        List<MEElemento> SapCuentaAsociadaCmb();
        /// <summary>
        /// Permite insertar la entidad SapCuentaAsociada.
        /// </summary>
        Boolean SapCuentaAsociadaInserta(ESapCuentaAsociada sapCuentaAsociada);
        /// <summary>
        /// Permite actualizar la entidad SapCuentaAsociada.
        /// </summary>
        Boolean SapCuentaAsociadaActualiza(ESapCuentaAsociada sapCuentaAsociada);
        /// <summary>
        /// Permite eliminar la entidad SapCuentaAsociada.
        /// </summary>
        Boolean SapCuentaAsociadaElimina(ESapCuentaAsociada sapCuentaAsociada);
        /// <summary>
        /// Exporta datos a Excel.
        /// </summary>
        Task<String> SapCuentaAsociadaExporta(ESapCuentaAsociadaFiltro sapCuentaAsociadaFiltro);
        /// <summary>
        /// Reglas de negocio de la entidad SapCuentaAsociada.
        /// </summary>
        List<MEReglaNeg> SapCuentaAsociadaReglas();
        #endregion

        #endregion
    }
}