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
    public class EVIdentificaciones
    {
        #region Propiedades
        /// <summary>
        /// Control de acciones Inserta, Actualiza y Elimina.
        /// </summary>
        public MAccionesGen Accion { get; set; } = MAccionesGen.Ninguna;

        //Identificacion (Identificaciones)

        public MEVSF<EIdentificacion, EIdentificacionPag, EIdentificacionFiltro> Identificacion { get; set; } = new();
        ///// <summary>
        ///// Paginacion.
        ///// </summary>
        //public EIdentificacionPag IdentificacionPag { get; set; } = null;
        ///// <summary>
        ///// Orden.
        ///// </summary>
        //public String IdentificacionColOrden { get; set; } = String.Empty;
        ///// <summary>
        ///// Filtro.
        ///// </summary>
        //public EIdentificacionFiltro IdentificacionFiltro { get; set; } = new EIdentificacionFiltro();
        ///// <summary>
        ///// Indice.
        ///// </summary>
        //public Int32 IdentificacionIndice { get; set; } = 0;
        ///// <summary>
        ///// Entidad de seleccion.
        ///// </summary>
        //public EIdentificacion IdentificacionSel { get; set; } = null;
        ///// <summary>
        ///// Reglas de negocio.
        ///// </summary>
        //public List<MEReglaNeg> IdentificacionReglas { get; set; } = null;
        #endregion
    }
}
