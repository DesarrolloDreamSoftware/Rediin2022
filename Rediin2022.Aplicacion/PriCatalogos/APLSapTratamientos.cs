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
    public class APLSapTratamientos : MAplicacion, INSapTratamientos
    {
        #region Constructores
        /// <summary>
        /// APL Para conexion con un API.
        /// </summary>
        public APLSapTratamientos(IMApiCliente api) : base(api)
        {
        }
        #endregion

        #region Funciones

        #region SapTratamiento (SapTratamientos)
        /// <summary>
        /// Consulta paginada de la entidad SapTratamiento.
        /// </summary>
        public ESapTratamientoPag SapTratamientoPag(ESapTratamientoFiltro sapTratamientoFiltro)
        {
            return Call<ESapTratamientoPag>(NomFn(), sapTratamientoFiltro);
        }
        /// <summary>
        /// Consulta por id de la entidad SapTratamiento.
        /// </summary>
        public ESapTratamiento SapTratamientoXId(String sapTratamientoId)
        {
            return Call<ESapTratamiento>(NomFn(),
                                         sapTratamientoId);
        }
        /// <summary>
        /// Consulta para combos de la entidad SapTratamiento.
        /// </summary>
        public List<MEElemento> SapTratamientoCmb()
        {
            return Call<List<MEElemento>>(NomFn());
        }
        /// <summary>
        /// Permite insertar la entidad SapTratamiento.
        /// </summary>
        public Boolean SapTratamientoInserta(ESapTratamiento sapTratamiento)
        {
            return Call<Boolean>(NomFn(), sapTratamiento);
        }
        /// <summary>
        /// Permite actualizar la entidad SapTratamiento.
        /// </summary>
        public Boolean SapTratamientoActualiza(ESapTratamiento sapTratamiento)
        {
            return Call<Boolean>(NomFn(), sapTratamiento);
        }
        /// <summary>
        /// Permite eliminar la entidad SapTratamiento.
        /// </summary>
        public Boolean SapTratamientoElimina(ESapTratamiento sapTratamiento)
        {
            return Call<Boolean>(NomFn(), sapTratamiento);
        }
        /// <summary>
        /// Exporta datos a Excel.
        /// </summary>
        public MEDatosArchivo SapTratamientoExporta(ESapTratamientoFiltro sapTratamientoFiltro)
        {
            return Call<MEDatosArchivo>(NomFn(),
                                        sapTratamientoFiltro);
        }
        /// <summary>
        /// Reglas de negocio de la entidad SapTratamiento.
        /// </summary>
        public List<MEReglaNeg> SapTratamientoReglas()
        {
            return Call<List<MEReglaNeg>>(NomFn());
        }
        #endregion

        #endregion
    }
}
