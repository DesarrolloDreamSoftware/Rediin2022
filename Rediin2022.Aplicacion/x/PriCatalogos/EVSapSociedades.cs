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
    public class EVSapSociedades
    {
        #region Propiedades
        /// <summary>
        /// Control de acciones Inserta, Actualiza y Elimina.
        /// </summary>
        public MAccionesGen Accion { get; set; } = MAccionesGen.Ninguna;

        //SapSociedad (SapSociedades)
        /// <summary>
        /// Paginacion.
        /// </summary>
        public ESapSociedadPag SapSociedadPag { get; set; } = null;
        /// <summary>
        /// Orden.
        /// </summary>
        public String SapSociedadColOrden { get; set; } = String.Empty;
        /// <summary>
        /// Filtro.
        /// </summary>
        public ESapSociedadFiltro SapSociedadFiltro { get; set; } = new ESapSociedadFiltro();
        /// <summary>
        /// Indice.
        /// </summary>
        public Int32 SapSociedadIndice { get; set; } = 0;
        /// <summary>
        /// Entidad de seleccion.
        /// </summary>
        public ESapSociedad SapSociedadSel { get; set; } = null;
        /// <summary>
        /// Reglas de negocio.
        /// </summary>
        public List<MEReglaNeg> SapSociedadReglas { get; set; } = null;
        #endregion
    }
}
