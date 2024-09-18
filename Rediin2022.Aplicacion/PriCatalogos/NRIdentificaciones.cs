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
    public class NRIdentificaciones : MNegRemoto, INIdentificaciones
    {
        #region Constructores
        /// <summary>
        /// APL Para conexion con un API.
        /// </summary>
        public NRIdentificaciones(IMApiCteNeg api) : base(api)
        {
        }
        #endregion

        #region Funciones

        #region Identificacion (Identificaciones)
        /// <summary>
        /// Consulta paginada de la entidad Identificacion.
        /// </summary>
        public async Task<EIdentificacionPag> IdentificacionPag(EIdentificacionFiltro identificacionFiltro)
        {
            return await CallAsync<EIdentificacionPag>(NomFn(), identificacionFiltro);
        }
        /// <summary>
        /// Consulta por id de la entidad Identificacion.
        /// </summary>
        public async Task<EIdentificacion> IdentificacionXId(Int64 identificacionId)
        {
            return await CallAsync<EIdentificacion>(NomFn(),
                                                    identificacionId);
        }
        /// <summary>
        /// Consulta para combos de la entidad Identificacion.
        /// </summary>
        public async Task<List<MEElemento>> IdentificacionCmb()
        {
            return await CallAsync<List<MEElemento>>(NomFn());
        }
        /// <summary>
        /// Permite insertar la entidad Identificacion.
        /// </summary>
        public async Task<Int64> IdentificacionInserta(EIdentificacion identificacion)
        {
            return await CallAsync<Int64>(NomFn(), identificacion);
        }
        /// <summary>
        /// Permite actualizar la entidad Identificacion.
        /// </summary>
        public async Task<Boolean> IdentificacionActualiza(EIdentificacion identificacion)
        {
            return await CallAsync<Boolean>(NomFn(), identificacion);
        }
        /// <summary>
        /// Permite eliminar la entidad Identificacion.
        /// </summary>
        public async Task<Boolean> IdentificacionElimina(EIdentificacion identificacion)
        {
            return await CallAsync<Boolean>(NomFn(), identificacion);
        }
        /// <summary>
        /// Reglas de negocio de la entidad Identificacion.
        /// </summary>
        public async Task<List<MEReglaNeg>> IdentificacionReglas()
        {
            return await CallAsync<List<MEReglaNeg>>(NomFn());
        }
        #endregion

        #endregion
    }
}
