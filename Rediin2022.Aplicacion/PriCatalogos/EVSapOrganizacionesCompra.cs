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
    public class EVSapOrganizacionesCompra
    {
        #region Propiedades
        /// <summary>
        /// Control de acciones Inserta, Actualiza y Elimina.
        /// </summary>
        public MAccionesGen Accion { get; set; } = MAccionesGen.Ninguna;

        //SapOrganizacionCompra (SapOrganizacionesCompra)
        /// <summary>
        /// Paginacion.
        /// </summary>
        public ESapOrganizacionCompraPag SapOrganizacionCompraPag { get; set; } = null;
        /// <summary>
        /// Orden.
        /// </summary>
        public String SapOrganizacionCompraColOrden { get; set; } = String.Empty;
        /// <summary>
        /// Filtro.
        /// </summary>
        public ESapOrganizacionCompraFiltro SapOrganizacionCompraFiltro { get; set; } = new ESapOrganizacionCompraFiltro();
        /// <summary>
        /// Indice.
        /// </summary>
        public Int32 SapOrganizacionCompraIndice { get; set; } = 0;
        /// <summary>
        /// Entidad de seleccion.
        /// </summary>
        public ESapOrganizacionCompra SapOrganizacionCompraSel { get; set; } = null;
        /// <summary>
        /// Reglas de negocio.
        /// </summary>
        public List<MEReglaNeg> SapOrganizacionCompraReglas { get; set; } = null;
        #endregion
    }
}
