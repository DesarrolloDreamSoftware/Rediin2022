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
    public class EVSapSociedadesGL
    {
        #region Propiedades
        /// <summary>
        /// Control de acciones Inserta, Actualiza y Elimina.
        /// </summary>
        public MAccionesGen Accion { get; set; } = MAccionesGen.Ninguna;

        //SapSociedadGL (SapSociedadesGL)
        /// <summary>
        /// Paginacion.
        /// </summary>
        public ESapSociedadGLPag SapSociedadGLPag { get; set; } = null;
        /// <summary>
        /// Orden.
        /// </summary>
        public String SapSociedadGLColOrden { get; set; } = String.Empty;
        /// <summary>
        /// Filtro.
        /// </summary>
        public ESapSociedadGLFiltro SapSociedadGLFiltro { get; set; } = new ESapSociedadGLFiltro();
        /// <summary>
        /// Indice.
        /// </summary>
        public Int32 SapSociedadGLIndice { get; set; } = 0;
        /// <summary>
        /// Entidad de seleccion.
        /// </summary>
        public ESapSociedadGL SapSociedadGLSel { get; set; } = null;
        /// <summary>
        /// Reglas de negocio.
        /// </summary>
        public List<MEReglaNeg> SapSociedadGLReglas { get; set; } = null;
        #endregion
    }
}
