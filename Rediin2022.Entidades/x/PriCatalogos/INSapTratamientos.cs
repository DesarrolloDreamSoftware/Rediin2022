using DSMetodNetX.Entidades;

using System;
using System.Collections.Generic;

namespace Rediin2022.Entidades.PriCatalogos
{
    /// <summary>
    /// Interfaz de negocio.
    /// </summary>
    public interface INSapTratamientos : IMCtrMensajes
    {
        #region Funciones

        #region SapTratamiento (SapTratamientos)
        /// <summary>
        /// Consulta paginada de la entidad SapTratamiento.
        /// </summary>
        ESapTratamientoPag SapTratamientoPag(ESapTratamientoFiltro sapTratamientoFiltro);
        /// <summary>
        /// Consulta por id de la entidad SapTratamiento.
        /// </summary>
        ESapTratamiento SapTratamientoXId(String sapTratamientoId);
        /// <summary>
        /// Consulta para combos de la entidad SapTratamiento.
        /// </summary>
        List<MEElemento> SapTratamientoCmb();
        /// <summary>
        /// Permite insertar la entidad SapTratamiento.
        /// </summary>
        Boolean SapTratamientoInserta(ESapTratamiento sapTratamiento);
        /// <summary>
        /// Permite actualizar la entidad SapTratamiento.
        /// </summary>
        Boolean SapTratamientoActualiza(ESapTratamiento sapTratamiento);
        /// <summary>
        /// Permite eliminar la entidad SapTratamiento.
        /// </summary>
        Boolean SapTratamientoElimina(ESapTratamiento sapTratamiento);
        /// <summary>
        /// Exporta datos a Excel.
        /// </summary>
        MEDatosArchivo SapTratamientoExporta(ESapTratamientoFiltro sapTratamientoFiltro);
        /// <summary>
        /// Reglas de negocio de la entidad SapTratamiento.
        /// </summary>
        List<MEReglaNeg> SapTratamientoReglas();
        #endregion

        #endregion
    }
}
