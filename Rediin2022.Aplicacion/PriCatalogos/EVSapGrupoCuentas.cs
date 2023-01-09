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
    public class EVSapGrupoCuentas
    {
        #region Propiedades
        /// <summary>
        /// Control de acciones Inserta, Actualiza y Elimina.
        /// </summary>
        public MAccionesGen Accion { get; set; } = MAccionesGen.Ninguna;

        //SapGrupoCuenta (SapGrupoCuentas)
        /// <summary>
        /// Paginacion.
        /// </summary>
        public ESapGrupoCuentaPag SapGrupoCuentaPag { get; set; } = null;
        /// <summary>
        /// Orden.
        /// </summary>
        public String SapGrupoCuentaColOrden { get; set; } = String.Empty;
        /// <summary>
        /// Filtro.
        /// </summary>
        public ESapGrupoCuentaFiltro SapGrupoCuentaFiltro { get; set; } = new ESapGrupoCuentaFiltro();
        /// <summary>
        /// Indice.
        /// </summary>
        public Int32 SapGrupoCuentaIndice { get; set; } = 0;
        /// <summary>
        /// Entidad de seleccion.
        /// </summary>
        public ESapGrupoCuenta SapGrupoCuentaSel { get; set; } = null;
        /// <summary>
        /// Reglas de negocio.
        /// </summary>
        public List<MEReglaNeg> SapGrupoCuentaReglas { get; set; } = null;
        #endregion
    }
}
