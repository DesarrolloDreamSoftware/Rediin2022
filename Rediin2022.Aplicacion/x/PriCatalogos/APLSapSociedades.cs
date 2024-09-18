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
    public class APLSapSociedades : MAplicacion, INSapSociedades
    {
        #region Constructores
        /// <summary>
        /// APL Para conexion con un API.
        /// </summary>
        public APLSapSociedades(IMApiCliente api) : base(api)
        {
        }
        #endregion

        #region Funciones

        #region SapSociedad (SapSociedades)
        /// <summary>
        /// Consulta paginada de la entidad SapSociedad.
        /// </summary>
        public ESapSociedadPag SapSociedadPag(ESapSociedadFiltro sapSociedadFiltro)
        {
            return Call<ESapSociedadPag>(NomFn(), sapSociedadFiltro);
        }
        /// <summary>
        /// Consulta por id de la entidad SapSociedad.
        /// </summary>
        public ESapSociedad SapSociedadXId(String sapSociedadId)
        {
            return Call<ESapSociedad>(NomFn(),
                                      sapSociedadId);
        }
        /// <summary>
        /// Consulta para combos de la entidad SapSociedad.
        /// </summary>
        public List<MEElemento> SapSociedadCmb()
        {
            return Call<List<MEElemento>>(NomFn());
        }
        /// <summary>
        /// Permite insertar la entidad SapSociedad.
        /// </summary>
        public Boolean SapSociedadInserta(ESapSociedad sapSociedad)
        {
            return Call<Boolean>(NomFn(), sapSociedad);
        }
        /// <summary>
        /// Permite actualizar la entidad SapSociedad.
        /// </summary>
        public Boolean SapSociedadActualiza(ESapSociedad sapSociedad)
        {
            return Call<Boolean>(NomFn(), sapSociedad);
        }
        /// <summary>
        /// Permite eliminar la entidad SapSociedad.
        /// </summary>
        public Boolean SapSociedadElimina(ESapSociedad sapSociedad)
        {
            return Call<Boolean>(NomFn(), sapSociedad);
        }
        /// <summary>
        /// Exporta datos a Excel.
        /// </summary>
        public MEDatosArchivo SapSociedadExporta(ESapSociedadFiltro sapSociedadFiltro)
        {
            return Call<MEDatosArchivo>(NomFn(),
                                        sapSociedadFiltro);
        }
        /// <summary>
        /// Reglas de negocio de la entidad SapSociedad.
        /// </summary>
        public List<MEReglaNeg> SapSociedadReglas()
        {
            return Call<List<MEReglaNeg>>(NomFn());
        }
        #endregion

        #endregion
    }
}
