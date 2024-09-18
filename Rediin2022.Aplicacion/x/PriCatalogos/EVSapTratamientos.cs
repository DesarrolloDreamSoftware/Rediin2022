using DSMetodNetX.Aplicacion;
using DSMetodNetX.Entidades;
using DSMetodNetX.Negocio;
using Rediin2022.Entidades.PriCatalogos;
using System;
using System.Collections.Generic;

namespace Rediin2022.Aplicacion.PriCatalogos
{
    /// <summary>
    /// Entidad de variables.
    /// </summary>
    [Serializable]
    public class EVSapTratamientos
    {
        #region Propiedades
        /// <summary>
        /// Control de acciones Inserta, Actualiza y Elimina.
        /// </summary>
        public MAccionesGen Accion { get; set; } = MAccionesGen.Ninguna;

        //SapTratamiento (SapTratamientos)
        /// <summary>
        /// Paginacion.
        /// </summary>
        public ESapTratamientoPag SapTratamientoPag { get; set; } = null;
        /// <summary>
        /// Orden.
        /// </summary>
        public String SapTratamientoColOrden { get; set; } = String.Empty;
        /// <summary>
        /// Filtro.
        /// </summary>
        public ESapTratamientoFiltro SapTratamientoFiltro { get; set; } = new ESapTratamientoFiltro();
        /// <summary>
        /// Indice.
        /// </summary>
        public Int32 SapTratamientoIndice { get; set; } = 0;
        /// <summary>
        /// Entidad de seleccion.
        /// </summary>
        public ESapTratamiento SapTratamientoSel { get; set; } = null;
        /// <summary>
        /// Reglas de negocio.
        /// </summary>
        public List<MEReglaNeg> SapTratamientoReglas { get; set; } = null;
        #endregion
    }
}
