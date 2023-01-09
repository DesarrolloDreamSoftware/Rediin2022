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
    public class APLSapCuentasAsociadas : MAplicacion, INSapCuentasAsociadas
    {
        #region Constructores
        /// <summary>
        /// APL Para conexion con un API.
        /// </summary>
        public APLSapCuentasAsociadas(IMApiCliente api) : base(api)
        {
        }
        #endregion

        #region Funciones

        #region SapCuentaAsociada (SapCuentasAsociadas)
        /// <summary>
        /// Consulta paginada de la entidad SapCuentaAsociada.
        /// </summary>
        public ESapCuentaAsociadaPag SapCuentaAsociadaPag(ESapCuentaAsociadaFiltro sapCuentaAsociadaFiltro)
        {
            return Call<ESapCuentaAsociadaPag>(NomFn(), sapCuentaAsociadaFiltro);
        }
        /// <summary>
        /// Consulta por id de la entidad SapCuentaAsociada.
        /// </summary>
        public ESapCuentaAsociada SapCuentaAsociadaXId(String sapCuentaAsociadaId)
        {
            return Call<ESapCuentaAsociada>(NomFn(),
                                            sapCuentaAsociadaId);
        }
        /// <summary>
        /// Consulta para combos de la entidad SapCuentaAsociada.
        /// </summary>
        public List<MEElemento> SapCuentaAsociadaCmb()
        {
            return Call<List<MEElemento>>(NomFn());
        }
        /// <summary>
        /// Permite insertar la entidad SapCuentaAsociada.
        /// </summary>
        public Boolean SapCuentaAsociadaInserta(ESapCuentaAsociada sapCuentaAsociada)
        {
            return Call<Boolean>(NomFn(), sapCuentaAsociada);
        }
        /// <summary>
        /// Permite actualizar la entidad SapCuentaAsociada.
        /// </summary>
        public Boolean SapCuentaAsociadaActualiza(ESapCuentaAsociada sapCuentaAsociada)
        {
            return Call<Boolean>(NomFn(), sapCuentaAsociada);
        }
        /// <summary>
        /// Permite eliminar la entidad SapCuentaAsociada.
        /// </summary>
        public Boolean SapCuentaAsociadaElimina(ESapCuentaAsociada sapCuentaAsociada)
        {
            return Call<Boolean>(NomFn(), sapCuentaAsociada);
        }
        /// <summary>
        /// Exporta datos a Excel.
        /// </summary>
        public MEDatosArchivo SapCuentaAsociadaExporta(ESapCuentaAsociadaFiltro sapCuentaAsociadaFiltro)
        {
            return Call<MEDatosArchivo>(NomFn(),
                                        sapCuentaAsociadaFiltro);
        }
        /// <summary>
        /// Reglas de negocio de la entidad SapCuentaAsociada.
        /// </summary>
        public List<MEReglaNeg> SapCuentaAsociadaReglas()
        {
            return Call<List<MEReglaNeg>>(NomFn());
        }
        #endregion

        #endregion
    }
}
