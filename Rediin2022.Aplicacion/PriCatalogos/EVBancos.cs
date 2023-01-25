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
    public class EVBancos
    {
        #region Propiedades
        /// <summary>
        /// Control de acciones Inserta, Actualiza y Elimina.
        /// </summary>
        public MAccionesGen Accion { get; set; } = MAccionesGen.Ninguna;

        //Banco (Bancos)
        /// <summary>
        /// Paginacion.
        /// </summary>
        public EBancoPag BancoPag { get; set; } = null;
        /// <summary>
        /// Orden.
        /// </summary>
        public String BancoColOrden { get; set; } = String.Empty;
        /// <summary>
        /// Filtro.
        /// </summary>
        public EBancoFiltro BancoFiltro { get; set; } = new EBancoFiltro();
        /// <summary>
        /// Indice.
        /// </summary>
        public Int32 BancoIndice { get; set; } = 0;
        /// <summary>
        /// Entidad de seleccion.
        /// </summary>
        public EBanco BancoSel { get; set; } = null;
        /// <summary>
        /// Reglas de negocio.
        /// </summary>
        public List<MEReglaNeg> BancoReglas { get; set; } = null;
        #endregion
    }
}
