using DSMetodNetX.Aplicacion;
using DSMetodNetX.Entidades;

using Rediin2022.Entidades.PriCatalogos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rediin2022.Aplicacion.PriCatalogos
{
    public class NRAutorizaciones : MNegRemoto, INAutorizaciones
    {
        #region Constructores
        public NRAutorizaciones(IMApiCteNeg api) : base(api)
        {
        }
        #endregion

        #region Funciones

        #region Autorizacion (Autorizaciones)
        /// <summary>
        /// Consulta paginada de la entidad Autorizacion.
        /// </summary>
        public async Task<EAutorizacionPag> AutorizacionPag(EAutorizacionFiltro autorizacionFiltro)
        {
            return await CallAsync<EAutorizacionPag>(NomFn(), autorizacionFiltro);
        }
        /// <summary>
        /// Consulta por id de la entidad Autorizacion.
        /// </summary>
        public async Task<EAutorizacion> AutorizacionXId(Int64 autorizacionId)
        {
            return await CallAsync<EAutorizacion>(NomFn(),
                                                  autorizacionId);
        }
        /// <summary>
        /// Permite insertar la entidad Autorizacion.
        /// </summary>
        public async Task<Int64> AutorizacionInserta(EAutorizacion autorizacion)
        {
            return await CallAsync<Int64>(NomFn(), autorizacion);
        }
        /// <summary>
        /// Permite actualizar la entidad Autorizacion.
        /// </summary>
        public async Task<Boolean> AutorizacionActualiza(EAutorizacion autorizacion)
        {
            return await CallAsync<Boolean>(NomFn(), autorizacion);
        }
        /// <summary>
        /// Permite eliminar la entidad Autorizacion.
        /// </summary>
        public async Task<Boolean> AutorizacionElimina(EAutorizacion autorizacion)
        {
            return await CallAsync<Boolean>(NomFn(), autorizacion);
        }
        /// <summary>
        /// Reglas de negocio de la entidad Autorizacion.
        /// </summary>
        public async Task<List<MEReglaNeg>> AutorizacionReglas()
        {
            return await CallAsync<List<MEReglaNeg>>(NomFn());
        }
        #endregion

        #region AutorizacionUsuario (AutorizacionesUsuarios)
        /// <summary>
        /// Consulta paginada de la entidad AutorizacionUsuario.
        /// </summary>
        public async Task<EAutorizacionUsuarioPag> AutorizacionUsuarioPag(EAutorizacionUsuarioFiltro autorizacionUsuarioFiltro)
        {
            return await CallAsync<EAutorizacionUsuarioPag>(NomFn(), autorizacionUsuarioFiltro);
        }
        /// <summary>
        /// Consulta por id de la entidad AutorizacionUsuario.
        /// </summary>
        public async Task<EAutorizacionUsuario> AutorizacionUsuarioXId(Int64 autorizacionUsuarioId)
        {
            return await CallAsync<EAutorizacionUsuario>(NomFn(),
                                                         autorizacionUsuarioId);
        }
        /// <summary>
        /// Permite insertar la entidad AutorizacionUsuario.
        /// </summary>
        public async Task<Int64> AutorizacionUsuarioInserta(EAutorizacionUsuario autorizacionUsuario)
        {
            return await CallAsync<Int64>(NomFn(), autorizacionUsuario);
        }
        /// <summary>
        /// Permite actualizar la entidad AutorizacionUsuario.
        /// </summary>
        public async Task<Boolean> AutorizacionUsuarioActualiza(EAutorizacionUsuario autorizacionUsuario)
        {
            return await CallAsync<Boolean>(NomFn(), autorizacionUsuario);
        }
        /// <summary>
        /// Permite eliminar la entidad AutorizacionUsuario.
        /// </summary>
        public async Task<Boolean> AutorizacionUsuarioElimina(EAutorizacionUsuario autorizacionUsuario)
        {
            return await CallAsync<Boolean>(NomFn(), autorizacionUsuario);
        }
        /// <summary>
        /// Reglas de negocio de la entidad AutorizacionUsuario.
        /// </summary>
        public async Task<List<MEReglaNeg>> AutorizacionUsuarioReglas()
        {
            return await CallAsync<List<MEReglaNeg>>(NomFn());
        }
        #endregion

        #endregion
    }
}
