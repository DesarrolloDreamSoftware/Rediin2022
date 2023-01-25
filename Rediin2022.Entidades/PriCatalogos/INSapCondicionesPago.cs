using DSMetodNetX.Entidades;
using DSMetodNetX.Negocio;
using System;
using System.Collections.Generic;

namespace Rediin2022.Entidades.PriCatalogos
{
    /// <summary>
    /// Interfaz de negocio.
    /// </summary>
    public interface INSapCondicionesPago : IMCtrMensajes
    {
        #region Funciones

        #region SapCondicionPago (SapCondicionesPago)
        /// <summary>
        /// Consulta paginada de la entidad SapCondicionPago.
        /// </summary>
        ESapCondicionPagoPag SapCondicionPagoPag(ESapCondicionPagoFiltro sapCondicionPagoFiltro);
        /// <summary>
        /// Consulta por id de la entidad SapCondicionPago.
        /// </summary>
        ESapCondicionPago SapCondicionPagoXId(String sapCondicionPagoId);
        /// <summary>
        /// Consulta para combos de la entidad SapCondicionPago.
        /// </summary>
        List<MEElemento> SapCondicionPagoCmb();
        /// <summary>
        /// Permite insertar la entidad SapCondicionPago.
        /// </summary>
        Boolean SapCondicionPagoInserta(ESapCondicionPago sapCondicionPago);
        /// <summary>
        /// Permite actualizar la entidad SapCondicionPago.
        /// </summary>
        Boolean SapCondicionPagoActualiza(ESapCondicionPago sapCondicionPago);
        /// <summary>
        /// Permite eliminar la entidad SapCondicionPago.
        /// </summary>
        Boolean SapCondicionPagoElimina(ESapCondicionPago sapCondicionPago);
        /// <summary>
        /// Exporta datos a Excel.
        /// </summary>
        MEDatosArchivo SapCondicionPagoExporta(ESapCondicionPagoFiltro sapCondicionPagoFiltro);
        /// <summary>
        /// Reglas de negocio de la entidad SapCondicionPago.
        /// </summary>
        List<MEReglaNeg> SapCondicionPagoReglas();
        #endregion

        #endregion
    }
}
