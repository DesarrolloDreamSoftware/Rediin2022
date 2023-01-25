using DSMetodNetX.Entidades;
using DSMetodNetX.Negocio;
using System;
using System.Collections.Generic;

namespace Rediin2022.Entidades.PriCatalogos
{
    /// <summary>
    /// Interfaz de negocio.
    /// </summary>
    public interface INSapSociedadesGL : IMCtrMensajes
    {
        #region Funciones

        #region SapSociedadGL (SapSociedadesGL)
        /// <summary>
        /// Consulta paginada de la entidad SapSociedadGL.
        /// </summary>
        ESapSociedadGLPag SapSociedadGLPag(ESapSociedadGLFiltro sapSociedadGLFiltro);
        /// <summary>
        /// Consulta por id de la entidad SapSociedadGL.
        /// </summary>
        ESapSociedadGL SapSociedadGLXId(String sapSociedadGLId);
        /// <summary>
        /// Consulta para combos de la entidad SapSociedadGL.
        /// </summary>
        List<MEElemento> SapSociedadGLCmb();
        /// <summary>
        /// Permite insertar la entidad SapSociedadGL.
        /// </summary>
        Boolean SapSociedadGLInserta(ESapSociedadGL sapSociedadGL);
        /// <summary>
        /// Permite actualizar la entidad SapSociedadGL.
        /// </summary>
        Boolean SapSociedadGLActualiza(ESapSociedadGL sapSociedadGL);
        /// <summary>
        /// Permite eliminar la entidad SapSociedadGL.
        /// </summary>
        Boolean SapSociedadGLElimina(ESapSociedadGL sapSociedadGL);
        /// <summary>
        /// Exporta datos a Excel.
        /// </summary>
        MEDatosArchivo SapSociedadGLExporta(ESapSociedadGLFiltro sapSociedadGLFiltro);
        /// <summary>
        /// Reglas de negocio de la entidad SapSociedadGL.
        /// </summary>
        List<MEReglaNeg> SapSociedadGLReglas();
        #endregion

        #endregion
    }
}
