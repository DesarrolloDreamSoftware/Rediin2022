using DSMetodNetX.Aplicacion;
using DSMetodNetX.Entidades;

using Rediin2022.Entidades.PriCatalogos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rediin2022.Aplicacion.PriCatalogos
{
    /// <summary>
    /// APL Para conexion con un API.
    /// </summary>
    public class NRSapGruposTolerancia : MNegRemoto, INSapGruposTolerancia
    {
        #region Constructores
        /// <summary>
        /// APL Para conexion con un API.
        /// </summary>
        public NRSapGruposTolerancia(IMApiCteNeg api) : base(api)
        {
        }
        #endregion

        #region Funciones

        #region SapGrupoTolerancia (SapGruposTolerancia)
        /// <summary>
        /// Consulta paginada de la entidad SapGrupoTolerancia.
        /// </summary>
        public async Task<ESapGrupoToleranciaPag> SapGrupoToleranciaPag(ESapGrupoToleranciaFiltro sapGrupoToleranciaFiltro)
        {
            return await CallAsync<ESapGrupoToleranciaPag>(NomFn(), sapGrupoToleranciaFiltro);
        }
        /// <summary>
        /// Consulta por id de la entidad SapGrupoTolerancia.
        /// </summary>
        public async Task<ESapGrupoTolerancia> SapGrupoToleranciaXId(String sapGrupoToleranciaId)
        {
            return await CallAsync<ESapGrupoTolerancia>(NomFn(),
                                                        sapGrupoToleranciaId);
        }
        /// <summary>
        /// Consulta para combos de la entidad SapGrupoTolerancia.
        /// </summary>
        public async Task<List<MEElemento>> SapGrupoToleranciaCmb()
        {
            return await CallAsync<List<MEElemento>>(NomFn());
        }
        /// <summary>
        /// Permite insertar la entidad SapGrupoTolerancia.
        /// </summary>
        public async Task<Boolean> SapGrupoToleranciaInserta(ESapGrupoTolerancia sapGrupoTolerancia)
        {
            return await CallAsync<Boolean>(NomFn(), sapGrupoTolerancia);
        }
        /// <summary>
        /// Permite actualizar la entidad SapGrupoTolerancia.
        /// </summary>
        public async Task<Boolean> SapGrupoToleranciaActualiza(ESapGrupoTolerancia sapGrupoTolerancia)
        {
            return await CallAsync<Boolean>(NomFn(), sapGrupoTolerancia);
        }
        /// <summary>
        /// Permite eliminar la entidad SapGrupoTolerancia.
        /// </summary>
        public async Task<Boolean> SapGrupoToleranciaElimina(ESapGrupoTolerancia sapGrupoTolerancia)
        {
            return await CallAsync<Boolean>(NomFn(), sapGrupoTolerancia);
        }
        /// <summary>
        /// Exporta datos a Excel.
        /// </summary>
        public async Task<String> SapGrupoToleranciaExporta(ESapGrupoToleranciaFiltro sapGrupoToleranciaFiltro)
        {
            return await CallAsync<String>(NomFn(),
                                           sapGrupoToleranciaFiltro);
        }
        /// <summary>
        /// Reglas de negocio de la entidad SapGrupoTolerancia.
        /// </summary>
        public async Task<List<MEReglaNeg>> SapGrupoToleranciaReglas()
        {
            return await CallAsync<List<MEReglaNeg>>(NomFn());
        }
        #endregion

        #endregion
    }
}
