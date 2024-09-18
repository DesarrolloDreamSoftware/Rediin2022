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
    public class EVSapViasPago
    {
        #region Propiedades
        /// <summary>
        /// Control de acciones Inserta, Actualiza y Elimina.
        /// </summary>
        public MAccionesGen Accion { get; set; } = MAccionesGen.Ninguna;

        //SapViaPago (SapViasPago)
        public MEVSF<ESapViaPago, ESapViaPagoPag, ESapViaPagoFiltro> SapViaPago { get; set; } = new();
        ///// <summary>
        ///// Paginacion.
        ///// </summary>
        //public ESapViaPagoPag SapViaPagoPag { get; set; } = null;
        ///// <summary>
        ///// Orden.
        ///// </summary>
        //public String SapViaPagoColOrden { get; set; } = String.Empty;
        ///// <summary>
        ///// Filtro.
        ///// </summary>
        //public ESapViaPagoFiltro SapViaPagoFiltro { get; set; } = new ESapViaPagoFiltro();
        ///// <summary>
        ///// Indice.
        ///// </summary>
        //public Int32 SapViaPagoIndice { get; set; } = 0;
        ///// <summary>
        ///// Entidad de seleccion.
        ///// </summary>
        //public ESapViaPago SapViaPagoSel { get; set; } = null;
        ///// <summary>
        ///// Reglas de negocio.
        ///// </summary>
        //public List<MEReglaNeg> SapViaPagoReglas { get; set; } = null;
        #endregion
    }
}
