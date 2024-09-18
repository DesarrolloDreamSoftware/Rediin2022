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
    public class EVSapGruposTolerancia
    {
        #region Propiedades
        /// <summary>
        /// Control de acciones Inserta, Actualiza y Elimina.
        /// </summary>
        public MAccionesGen Accion { get; set; } = MAccionesGen.Ninguna;

        //SapGrupoTolerancia (SapGruposTolerancia)
        public MEVSF<ESapGrupoTolerancia, ESapGrupoToleranciaPag, ESapGrupoToleranciaFiltro> SapGrupoTolerancia { get; set; } = new();
        ///// <summary>
        ///// Paginacion.
        ///// </summary>
        //public ESapGrupoToleranciaPag SapGrupoToleranciaPag { get; set; } = null;
        ///// <summary>
        ///// Orden.
        ///// </summary>
        //public String SapGrupoToleranciaColOrden { get; set; } = String.Empty;
        ///// <summary>
        ///// Filtro.
        ///// </summary>
        //public ESapGrupoToleranciaFiltro SapGrupoToleranciaFiltro { get; set; } = new ESapGrupoToleranciaFiltro();
        ///// <summary>
        ///// Indice.
        ///// </summary>
        //public Int32 SapGrupoToleranciaIndice { get; set; } = 0;
        ///// <summary>
        ///// Entidad de seleccion.
        ///// </summary>
        //public ESapGrupoTolerancia SapGrupoToleranciaSel { get; set; } = null;
        ///// <summary>
        ///// Reglas de negocio.
        ///// </summary>
        //public List<MEReglaNeg> SapGrupoToleranciaReglas { get; set; } = null;
        #endregion
    }
}
