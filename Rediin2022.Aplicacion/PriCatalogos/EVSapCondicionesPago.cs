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
    public class EVSapCondicionesPago
    {
        #region Propiedades
        /// <summary>
        /// Control de acciones Inserta, Actualiza y Elimina.
        /// </summary>
        public MAccionesGen Accion { get; set; } = MAccionesGen.Ninguna;

        //SapCondicionPago (SapCondicionesPago)
        /// <summary>
        /// Paginacion.
        /// </summary>
        public ESapCondicionPagoPag SapCondicionPagoPag { get; set; } = null;
        /// <summary>
        /// Orden.
        /// </summary>
        public String SapCondicionPagoColOrden { get; set; } = String.Empty;
        /// <summary>
        /// Filtro.
        /// </summary>
        public ESapCondicionPagoFiltro SapCondicionPagoFiltro { get; set; } = new ESapCondicionPagoFiltro();
        /// <summary>
        /// Indice.
        /// </summary>
        public Int32 SapCondicionPagoIndice { get; set; } = 0;
        /// <summary>
        /// Entidad de seleccion.
        /// </summary>
        public ESapCondicionPago SapCondicionPagoSel { get; set; } = null;
        /// <summary>
        /// Reglas de negocio.
        /// </summary>
        public List<MEReglaNeg> SapCondicionPagoReglas { get; set; } = null;
        #endregion
    }
}
