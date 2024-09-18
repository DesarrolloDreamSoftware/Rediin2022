using DSMetodNetX.Entidades;

using System;
using System.Collections.Generic;

namespace Rediin2022.Entidades.PriCatalogos
{
    /// <summary>
    /// Interfaz de negocio.
    /// </summary>
    public interface INSapSociedades : IMCtrMensajes
    {
        #region Funciones

        #region SapSociedad (SapSociedades)
        /// <summary>
        /// Consulta paginada de la entidad SapSociedad.
        /// </summary>
        ESapSociedadPag SapSociedadPag(ESapSociedadFiltro sapSociedadFiltro);
        /// <summary>
        /// Consulta por id de la entidad SapSociedad.
        /// </summary>
        ESapSociedad SapSociedadXId(String sapSociedadId);
        /// <summary>
        /// Consulta para combos de la entidad SapSociedad.
        /// </summary>
        List<MEElemento> SapSociedadCmb();
        /// <summary>
        /// Permite insertar la entidad SapSociedad.
        /// </summary>
        Boolean SapSociedadInserta(ESapSociedad sapSociedad);
        /// <summary>
        /// Permite actualizar la entidad SapSociedad.
        /// </summary>
        Boolean SapSociedadActualiza(ESapSociedad sapSociedad);
        /// <summary>
        /// Permite eliminar la entidad SapSociedad.
        /// </summary>
        Boolean SapSociedadElimina(ESapSociedad sapSociedad);
        /// <summary>
        /// Exporta datos a Excel.
        /// </summary>
        MEDatosArchivo SapSociedadExporta(ESapSociedadFiltro sapSociedadFiltro);
        /// <summary>
        /// Reglas de negocio de la entidad SapSociedad.
        /// </summary>
        List<MEReglaNeg> SapSociedadReglas();
        #endregion

        #endregion
    }
}
