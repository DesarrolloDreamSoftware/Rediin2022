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
    public class EVSapGruposTesoreria
    {
        #region Propiedades
        /// <summary>
        /// Control de acciones Inserta, Actualiza y Elimina.
        /// </summary>
        public MAccionesGen Accion { get; set; } = MAccionesGen.Ninguna;

        //SapGrupoTesoreria (SapGruposTesoreria)
        /// <summary>
        /// Paginacion.
        /// </summary>
        public ESapGrupoTesoreriaPag SapGrupoTesoreriaPag { get; set; } = null;
        /// <summary>
        /// Orden.
        /// </summary>
        public String SapGrupoTesoreriaColOrden { get; set; } = String.Empty;
        /// <summary>
        /// Filtro.
        /// </summary>
        public ESapGrupoTesoreriaFiltro SapGrupoTesoreriaFiltro { get; set; } = new ESapGrupoTesoreriaFiltro();
        /// <summary>
        /// Indice.
        /// </summary>
        public Int32 SapGrupoTesoreriaIndice { get; set; } = 0;
        /// <summary>
        /// Entidad de seleccion.
        /// </summary>
        public ESapGrupoTesoreria SapGrupoTesoreriaSel { get; set; } = null;
        /// <summary>
        /// Reglas de negocio.
        /// </summary>
        public List<MEReglaNeg> SapGrupoTesoreriaReglas { get; set; } = null;
        #endregion
    }
}
