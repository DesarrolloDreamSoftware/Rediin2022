using DSMetodNetX.Aplicacion;
using DSMetodNetX.Entidades;
using DSMetodNetX.Negocio;
using Rediin2022.Entidades.PriCatalogos;
using System;
using System.Collections.Generic;

namespace Rediin2022.Aplicacion.PriCatalogos
{
    /// <summary>
    /// APL Para conexion con un API.
    /// </summary>
    public class APLSapCondicionesPago : MAplicacion, INSapCondicionesPago
    {
        #region Constructores
        /// <summary>
        /// APL Para conexion con un API.
        /// </summary>
        public APLSapCondicionesPago(IMApiCliente api) : base(api)
        {
        }
        #endregion

        #region Funciones

        #region SapCondicionPago (SapCondicionesPago)
        /// <summary>
        /// Consulta paginada de la entidad SapCondicionPago.
        /// </summary>
        public ESapCondicionPagoPag SapCondicionPagoPag(ESapCondicionPagoFiltro sapCondicionPagoFiltro)
        {
            return Call<ESapCondicionPagoPag>(NomFn(), sapCondicionPagoFiltro);
        }
        /// <summary>
        /// Consulta por id de la entidad SapCondicionPago.
        /// </summary>
        public ESapCondicionPago SapCondicionPagoXId(String sapCondicionPagoId)
        {
            return Call<ESapCondicionPago>(NomFn(),
                                           sapCondicionPagoId);
        }
        /// <summary>
        /// Consulta para combos de la entidad SapCondicionPago.
        /// </summary>
        public List<MEElemento> SapCondicionPagoCmb()
        {
            return Call<List<MEElemento>>(NomFn());
        }
        /// <summary>
        /// Permite insertar la entidad SapCondicionPago.
        /// </summary>
        public Boolean SapCondicionPagoInserta(ESapCondicionPago sapCondicionPago)
        {
            return Call<Boolean>(NomFn(), sapCondicionPago);
        }
        /// <summary>
        /// Permite actualizar la entidad SapCondicionPago.
        /// </summary>
        public Boolean SapCondicionPagoActualiza(ESapCondicionPago sapCondicionPago)
        {
            return Call<Boolean>(NomFn(), sapCondicionPago);
        }
        /// <summary>
        /// Permite eliminar la entidad SapCondicionPago.
        /// </summary>
        public Boolean SapCondicionPagoElimina(ESapCondicionPago sapCondicionPago)
        {
            return Call<Boolean>(NomFn(), sapCondicionPago);
        }
        /// <summary>
        /// Exporta datos a Excel.
        /// </summary>
        public MEDatosArchivo SapCondicionPagoExporta(ESapCondicionPagoFiltro sapCondicionPagoFiltro)
        {
            return Call<MEDatosArchivo>(NomFn(),
                                        sapCondicionPagoFiltro);
        }
        /// <summary>
        /// Reglas de negocio de la entidad SapCondicionPago.
        /// </summary>
        public List<MEReglaNeg> SapCondicionPagoReglas()
        {
            return Call<List<MEReglaNeg>>(NomFn());
        }
        #endregion

        #endregion
    }
}
