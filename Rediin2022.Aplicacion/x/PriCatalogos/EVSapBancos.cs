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
    public class EVSapBancos
    {
        #region Propiedades
        /// <summary>
        /// Control de acciones Inserta, Actualiza y Elimina.
        /// </summary>
        public MAccionesGen Accion { get; set; } = MAccionesGen.Ninguna;

        //SapBanco (SapBancos)
        /// <summary>
        /// Paginacion.
        /// </summary>
        public ESapBancoPag SapBancoPag { get; set; } = null;
        /// <summary>
        /// Orden.
        /// </summary>
        public String SapBancoColOrden { get; set; } = String.Empty;
        /// <summary>
        /// Filtro.
        /// </summary>
        public ESapBancoFiltro SapBancoFiltro { get; set; } = new ESapBancoFiltro();
        /// <summary>
        /// Indice.
        /// </summary>
        public Int32 SapBancoIndice { get; set; } = 0;
        /// <summary>
        /// Entidad de seleccion.
        /// </summary>
        public ESapBanco SapBancoSel { get; set; } = null;
        /// <summary>
        /// Reglas de negocio.
        /// </summary>
        public List<MEReglaNeg> SapBancoReglas { get; set; } = null;
        #endregion
    }
}
