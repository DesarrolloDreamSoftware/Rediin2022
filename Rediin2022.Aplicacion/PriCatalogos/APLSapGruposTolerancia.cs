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
    public class APLSapGruposTolerancia : MAplicacion, INSapGruposTolerancia
    {
        #region Constructores
        /// <summary>
        /// APL Para conexion con un API.
        /// </summary>
        public APLSapGruposTolerancia(IMApiCliente api) : base(api)
        {
        }
        #endregion

        #region Funciones

        #region SapGrupoTolerancia (SapGruposTolerancia)
        /// <summary>
        /// Consulta paginada de la entidad SapGrupoTolerancia.
        /// </summary>
        public ESapGrupoToleranciaPag SapGrupoToleranciaPag(ESapGrupoToleranciaFiltro sapGrupoToleranciaFiltro)
        {
            return Call<ESapGrupoToleranciaPag>(NomFn(), sapGrupoToleranciaFiltro);
        }
        /// <summary>
        /// Consulta por id de la entidad SapGrupoTolerancia.
        /// </summary>
        public ESapGrupoTolerancia SapGrupoToleranciaXId(String sapGrupoToleranciaId)
        {
            return Call<ESapGrupoTolerancia>(NomFn(),
                                             sapGrupoToleranciaId);
        }
        /// <summary>
        /// Consulta para combos de la entidad SapGrupoTolerancia.
        /// </summary>
        public List<MEElemento> SapGrupoToleranciaCmb()
        {
            return Call<List<MEElemento>>(NomFn());
        }
        /// <summary>
        /// Permite insertar la entidad SapGrupoTolerancia.
        /// </summary>
        public Boolean SapGrupoToleranciaInserta(ESapGrupoTolerancia sapGrupoTolerancia)
        {
            return Call<Boolean>(NomFn(), sapGrupoTolerancia);
        }
        /// <summary>
        /// Permite actualizar la entidad SapGrupoTolerancia.
        /// </summary>
        public Boolean SapGrupoToleranciaActualiza(ESapGrupoTolerancia sapGrupoTolerancia)
        {
            return Call<Boolean>(NomFn(), sapGrupoTolerancia);
        }
        /// <summary>
        /// Permite eliminar la entidad SapGrupoTolerancia.
        /// </summary>
        public Boolean SapGrupoToleranciaElimina(ESapGrupoTolerancia sapGrupoTolerancia)
        {
            return Call<Boolean>(NomFn(), sapGrupoTolerancia);
        }
        /// <summary>
        /// Exporta datos a Excel.
        /// </summary>
        public MEDatosArchivo SapGrupoToleranciaExporta(ESapGrupoToleranciaFiltro sapGrupoToleranciaFiltro)
        {
            return Call<MEDatosArchivo>(NomFn(),
                                        sapGrupoToleranciaFiltro);
        }
        /// <summary>
        /// Reglas de negocio de la entidad SapGrupoTolerancia.
        /// </summary>
        public List<MEReglaNeg> SapGrupoToleranciaReglas()
        {
            return Call<List<MEReglaNeg>>(NomFn());
        }
        #endregion

        #endregion
    }
}
