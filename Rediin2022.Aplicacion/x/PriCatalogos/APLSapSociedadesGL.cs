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
    public class APLSapSociedadesGL : MAplicacion, INSapSociedadesGL
    {
        #region Constructores
        /// <summary>
        /// APL Para conexion con un API.
        /// </summary>
        public APLSapSociedadesGL(IMApiCliente api) : base(api)
        {
        }
        #endregion

        #region Funciones

        #region SapSociedadGL (SapSociedadesGL)
        /// <summary>
        /// Consulta paginada de la entidad SapSociedadGL.
        /// </summary>
        public ESapSociedadGLPag SapSociedadGLPag(ESapSociedadGLFiltro sapSociedadGLFiltro)
        {
            return Call<ESapSociedadGLPag>(NomFn(), sapSociedadGLFiltro);
        }
        /// <summary>
        /// Consulta por id de la entidad SapSociedadGL.
        /// </summary>
        public ESapSociedadGL SapSociedadGLXId(String sapSociedadGLId)
        {
            return Call<ESapSociedadGL>(NomFn(),
                                        sapSociedadGLId);
        }
        /// <summary>
        /// Consulta para combos de la entidad SapSociedadGL.
        /// </summary>
        public List<MEElemento> SapSociedadGLCmb()
        {
            return Call<List<MEElemento>>(NomFn());
        }
        /// <summary>
        /// Permite insertar la entidad SapSociedadGL.
        /// </summary>
        public Boolean SapSociedadGLInserta(ESapSociedadGL sapSociedadGL)
        {
            return Call<Boolean>(NomFn(), sapSociedadGL);
        }
        /// <summary>
        /// Permite actualizar la entidad SapSociedadGL.
        /// </summary>
        public Boolean SapSociedadGLActualiza(ESapSociedadGL sapSociedadGL)
        {
            return Call<Boolean>(NomFn(), sapSociedadGL);
        }
        /// <summary>
        /// Permite eliminar la entidad SapSociedadGL.
        /// </summary>
        public Boolean SapSociedadGLElimina(ESapSociedadGL sapSociedadGL)
        {
            return Call<Boolean>(NomFn(), sapSociedadGL);
        }
        /// <summary>
        /// Exporta datos a Excel.
        /// </summary>
        public MEDatosArchivo SapSociedadGLExporta(ESapSociedadGLFiltro sapSociedadGLFiltro)
        {
            return Call<MEDatosArchivo>(NomFn(),
                                        sapSociedadGLFiltro);
        }
        /// <summary>
        /// Reglas de negocio de la entidad SapSociedadGL.
        /// </summary>
        public List<MEReglaNeg> SapSociedadGLReglas()
        {
            return Call<List<MEReglaNeg>>(NomFn());
        }
        #endregion

        #endregion
    }
}
