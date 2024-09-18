using DSMetodNetX.Entidades;

using System;
using System.Collections.Generic;

namespace Rediin2022.Entidades.PriCatalogos
{
    /// <summary>
    /// Interfaz de negocio.
    /// </summary>
    public interface INSapViasPago : IMCtrMensajes
    {
        #region Funciones

        #region SapViaPago (SapViasPago)
        /// <summary>
        /// Consulta paginada de la entidad SapViaPago.
        /// </summary>
        ESapViaPagoPag SapViaPagoPag(ESapViaPagoFiltro sapViaPagoFiltro);
        /// <summary>
        /// Consulta por id de la entidad SapViaPago.
        /// </summary>
        ESapViaPago SapViaPagoXId(String sapViaPagoId);
        /// <summary>
        /// Consulta para combos de la entidad SapViaPago.
        /// </summary>
        List<MEElemento> SapViaPagoCmb();
        /// <summary>
        /// Permite insertar la entidad SapViaPago.
        /// </summary>
        Boolean SapViaPagoInserta(ESapViaPago sapViaPago);
        /// <summary>
        /// Permite actualizar la entidad SapViaPago.
        /// </summary>
        Boolean SapViaPagoActualiza(ESapViaPago sapViaPago);
        /// <summary>
        /// Permite eliminar la entidad SapViaPago.
        /// </summary>
        Boolean SapViaPagoElimina(ESapViaPago sapViaPago);
        /// <summary>
        /// Exporta datos a Excel.
        /// </summary>
        MEDatosArchivo SapViaPagoExporta(ESapViaPagoFiltro sapViaPagoFiltro);
        /// <summary>
        /// Reglas de negocio de la entidad SapViaPago.
        /// </summary>
        List<MEReglaNeg> SapViaPagoReglas();
        #endregion

        #endregion
    }
}
