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
    public class APLSapGruposTesoreria : MAplicacion, INSapGruposTesoreria
    {
        #region Constructores
        /// <summary>
        /// APL Para conexion con un API.
        /// </summary>
        public APLSapGruposTesoreria(IMApiCliente api) : base(api)
        {
        }
        #endregion

        #region Funciones

        #region SapGrupoTesoreria (SapGruposTesoreria)
        /// <summary>
        /// Consulta paginada de la entidad SapGrupoTesoreria.
        /// </summary>
        public ESapGrupoTesoreriaPag SapGrupoTesoreriaPag(ESapGrupoTesoreriaFiltro sapGrupoTesoreriaFiltro)
        {
            return Call<ESapGrupoTesoreriaPag>(NomFn(), sapGrupoTesoreriaFiltro);
        }
        /// <summary>
        /// Consulta por id de la entidad SapGrupoTesoreria.
        /// </summary>
        public ESapGrupoTesoreria SapGrupoTesoreriaXId(String sapGrupoTesoreriaId)
        {
            return Call<ESapGrupoTesoreria>(NomFn(),
                                            sapGrupoTesoreriaId);
        }
        /// <summary>
        /// Consulta para combos de la entidad SapGrupoTesoreria.
        /// </summary>
        public List<MEElemento> SapGrupoTesoreriaCmb()
        {
            return Call<List<MEElemento>>(NomFn());
        }
        /// <summary>
        /// Permite insertar la entidad SapGrupoTesoreria.
        /// </summary>
        public Boolean SapGrupoTesoreriaInserta(ESapGrupoTesoreria sapGrupoTesoreria)
        {
            return Call<Boolean>(NomFn(), sapGrupoTesoreria);
        }
        /// <summary>
        /// Permite actualizar la entidad SapGrupoTesoreria.
        /// </summary>
        public Boolean SapGrupoTesoreriaActualiza(ESapGrupoTesoreria sapGrupoTesoreria)
        {
            return Call<Boolean>(NomFn(), sapGrupoTesoreria);
        }
        /// <summary>
        /// Permite eliminar la entidad SapGrupoTesoreria.
        /// </summary>
        public Boolean SapGrupoTesoreriaElimina(ESapGrupoTesoreria sapGrupoTesoreria)
        {
            return Call<Boolean>(NomFn(), sapGrupoTesoreria);
        }
        /// <summary>
        /// Exporta datos a Excel.
        /// </summary>
        public MEDatosArchivo SapGrupoTesoreriaExporta(ESapGrupoTesoreriaFiltro sapGrupoTesoreriaFiltro)
        {
            return Call<MEDatosArchivo>(NomFn(),
                                        sapGrupoTesoreriaFiltro);
        }
        /// <summary>
        /// Reglas de negocio de la entidad SapGrupoTesoreria.
        /// </summary>
        public List<MEReglaNeg> SapGrupoTesoreriaReglas()
        {
            return Call<List<MEReglaNeg>>(NomFn());
        }
        #endregion

        #endregion
    }
}
