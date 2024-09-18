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
    public class APLSapViasPago : MAplicacion, INSapViasPago
    {
        #region Constructores
        /// <summary>
        /// APL Para conexion con un API.
        /// </summary>
        public APLSapViasPago(IMApiCliente api) : base(api)
        {
        }
        #endregion

        #region Funciones

        #region SapViaPago (SapViasPago)
        /// <summary>
        /// Consulta paginada de la entidad SapViaPago.
        /// </summary>
        public ESapViaPagoPag SapViaPagoPag(ESapViaPagoFiltro sapViaPagoFiltro)
        {
            return Call<ESapViaPagoPag>(NomFn(), sapViaPagoFiltro);
        }
        /// <summary>
        /// Consulta por id de la entidad SapViaPago.
        /// </summary>
        public ESapViaPago SapViaPagoXId(String sapViaPagoId)
        {
            return Call<ESapViaPago>(NomFn(),
                                     sapViaPagoId);
        }
        /// <summary>
        /// Consulta para combos de la entidad SapViaPago.
        /// </summary>
        public List<MEElemento> SapViaPagoCmb()
        {
            return Call<List<MEElemento>>(NomFn());
        }
        /// <summary>
        /// Permite insertar la entidad SapViaPago.
        /// </summary>
        public Boolean SapViaPagoInserta(ESapViaPago sapViaPago)
        {
            return Call<Boolean>(NomFn(), sapViaPago);
        }
        /// <summary>
        /// Permite actualizar la entidad SapViaPago.
        /// </summary>
        public Boolean SapViaPagoActualiza(ESapViaPago sapViaPago)
        {
            return Call<Boolean>(NomFn(), sapViaPago);
        }
        /// <summary>
        /// Permite eliminar la entidad SapViaPago.
        /// </summary>
        public Boolean SapViaPagoElimina(ESapViaPago sapViaPago)
        {
            return Call<Boolean>(NomFn(), sapViaPago);
        }
        /// <summary>
        /// Exporta datos a Excel.
        /// </summary>
        public MEDatosArchivo SapViaPagoExporta(ESapViaPagoFiltro sapViaPagoFiltro)
        {
            return Call<MEDatosArchivo>(NomFn(),
                                        sapViaPagoFiltro);
        }
        /// <summary>
        /// Reglas de negocio de la entidad SapViaPago.
        /// </summary>
        public List<MEReglaNeg> SapViaPagoReglas()
        {
            return Call<List<MEReglaNeg>>(NomFn());
        }
        #endregion

        #endregion
    }
}
