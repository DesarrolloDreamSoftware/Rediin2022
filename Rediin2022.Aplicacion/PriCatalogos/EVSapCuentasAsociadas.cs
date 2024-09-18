using DSMetodNetX.Aplicacion;
using DSMetodNetX.Entidades;

using Rediin2022.Entidades.PriCatalogos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rediin2022.Aplicacion.PriCatalogos
{
    /// <summary>
    /// Entidad de variables.
    /// </summary>
    [Serializable]
    public class EVSapCuentasAsociadas
    {
        #region Propiedades
        /// <summary>
        /// Control de acciones Inserta, Actualiza y Elimina.
        /// </summary>
        public MAccionesGen Accion { get; set; } = MAccionesGen.Ninguna;

        //SapCuentaAsociada (SapCuentasAsociadas)

        public MEVSF<ESapCuentaAsociada, ESapCuentaAsociadaPag, ESapCuentaAsociadaFiltro> SapCuentaAsociada { get; set; } = new();
        ///// <summary>
        ///// Paginacion.
        ///// </summary>
        //public ESapCuentaAsociadaPag SapCuentaAsociadaPag { get; set; } = null;
        ///// <summary>
        ///// Orden.
        ///// </summary>
        //public String SapCuentaAsociadaColOrden { get; set; } = String.Empty;
        ///// <summary>
        ///// Filtro.
        ///// </summary>
        //public ESapCuentaAsociadaFiltro SapCuentaAsociadaFiltro { get; set; } = new ESapCuentaAsociadaFiltro();
        ///// <summary>
        ///// Indice.
        ///// </summary>
        //public Int32 SapCuentaAsociadaIndice { get; set; } = 0;
        ///// <summary>
        ///// Entidad de seleccion.
        ///// </summary>
        //public ESapCuentaAsociada SapCuentaAsociadaSel { get; set; } = null;
        ///// <summary>
        ///// Reglas de negocio.
        ///// </summary>
        //public List<MEReglaNeg> SapCuentaAsociadaReglas { get; set; } = null;
        #endregion
    }
}
