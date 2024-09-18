using DSEntityNetX.Common.Casting;
using DSEntityNetX.DataAccess;
using DSMetodNetX.AccesoDatos;
using DSMetodNetX.Comun;
using DSMetodNetX.Entidades;

using Rediin2022.Entidades.Idioma;
using Rediin2022.Entidades.PriCatalogos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rediin2022.AccesoDatos.PriCatalogos
{
    [Serializable]
    public class RAutorizaciones : MRepositorio
    {
        #region Variables
        /// <summary>
        /// Conexión.
        /// </summary>
        private IMConexionEntidad _conexion;
        #endregion

        #region Constructores
        public RAutorizaciones(IMConexionEntidad conexion)
            : base(conexion)
        {
            _conexion = conexion;
        }
        #endregion

        #region Funciones

        #region Autorizacion (Autorizaciones)
        /// <summary>
        /// Consulta paginada de la entidad Autorizacion.
        /// </summary>
        public async Task<EAutorizacionPag> AutorizacionPag(EAutorizacionFiltro autorizacionFiltro)
        {
            return await _conexion.EntidadPagAsync<EAutorizacion,
                                                    EAutorizacionPag,
                                                    EAutorizacionFiltro>(autorizacionFiltro, "NCAutorizacionesCP");

            //           EAutorizacionPag vAutorizacionPag = new EAutorizacionPag();

            //           _conexion.AddParamIn(MMetaDatos.establecimientoId, _conexion.UsuarioSesion.EstablecimientoId);
            //           _conexion.AddParamFilterTL(autorizacionFiltro);
            //await _conexion.LoadEntityAsync<EAutorizacionPag>("NCAutorizacionesCP", vAutorizacionPag);
            //if (!Mensajes.Ok)
            //    return vAutorizacionPag;

            //base.MProcesaDatPag(autorizacionFiltro, vAutorizacionPag);

            //_conexion.AddParamIn(MMetaDatos.establecimientoId, _conexion.UsuarioSesion.EstablecimientoId);
            //_conexion.AddParamFilterPag(autorizacionFiltro);
            //vAutorizacionPag.Pagina = await _conexion.LoadEntitiesAsync<EAutorizacion>("NCAutorizacionesCP");

            //return vAutorizacionPag;
        }
        /// <summary>
        /// Consulta por id de la entidad Autorizacion.
        /// </summary>
        public async Task<EAutorizacion> AutorizacionXId(Int64 autorizacionId)
        {
            _conexion.AddParamIn(autorizacionId);
            return await _conexion.LoadEntityAsync<EAutorizacion>("NCAutorizacionesCI");
        }
        /// <summary>
        /// Permite insertar la entidad Autorizacion.
        /// </summary>
        protected async Task<Int64> AutorizacionInserta(EAutorizacion autorizacion)
        {
            await _conexion.EntityUpdateAsync(autorizacion, MAccionesBd.Inserta, "NCAutorizacionesIAE");
            return autorizacion.AutorizacionId;

            //           _conexion.AddParamEntity(autorizacion, MAccionesBd.Inserta);
            //Int64 vResultado = await _conexion.ExecuteScalarValAsync("NCAutorizacionesIAE",
            //                                              MensajesXId.AutorizacionNombre);
            //return vResultado;
        }
        /// <summary>
        /// Permite actualizar la entidad Autorizacion.
        /// </summary>
        protected async Task<Boolean> AutorizacionActualiza(EAutorizacion autorizacion)
        {
            return await _conexion.EntityUpdateAsync(autorizacion, MAccionesBd.Actualiza, "NCAutorizacionesIAE");

            //           _conexion.AddParamEntity(autorizacion, MAccionesBd.Actualiza);
            //await _conexion.ExecuteScalarValAsync("NCAutorizacionesIAE",
            //                           MensajesXId.AutorizacionNombre);
            //return Mensajes.Ok;
        }
        /// <summary>
        /// Permite eliminar la entidad Autorizacion.
        /// </summary>
        protected async Task<Boolean> AutorizacionElimina(EAutorizacion autorizacion)
        {
            return await _conexion.EntityUpdateAsync(autorizacion, MAccionesBd.Elimina, "NCAutorizacionesIAE");

            //           _conexion.AddParamEntity(autorizacion, MAccionesBd.Elimina);
            //return await _conexion.ExecuteNonQueryRetAsync("NCAutorizacionesIAE");
        }
        #endregion

        #region AutorizacionUsuario (AutorizacionesUsuarios)
        /// <summary>
        /// Consulta paginada de la entidad AutorizacionUsuario.
        /// </summary>
        public async Task<EAutorizacionUsuarioPag> AutorizacionUsuarioPag(EAutorizacionUsuarioFiltro autorizacionUsuarioFiltro)
        {
            return await _conexion.EntidadPagAsync<EAutorizacionUsuario,
                                                    EAutorizacionUsuarioPag,
                                                    EAutorizacionUsuarioFiltro>(autorizacionUsuarioFiltro, "NCAutorizacionesUsuariosCP");

            //           EAutorizacionUsuarioPag vAutorizacionUsuarioPag = new EAutorizacionUsuarioPag();

            //           _conexion.AddParamFilterTL(autorizacionUsuarioFiltro);
            //await _conexion.LoadEntityAsync<EAutorizacionUsuarioPag>("NCAutorizacionesUsuariosCP", vAutorizacionUsuarioPag);
            //if (!Mensajes.Ok)
            //    return vAutorizacionUsuarioPag;

            //base.MProcesaDatPag(autorizacionUsuarioFiltro, vAutorizacionUsuarioPag);

            //_conexion.AddParamFilterPag(autorizacionUsuarioFiltro);
            //vAutorizacionUsuarioPag.Pagina = await _conexion.LoadEntitiesAsync<EAutorizacionUsuario>("NCAutorizacionesUsuariosCP");

            //return vAutorizacionUsuarioPag;
        }
        /// <summary>
        /// Consulta por id de la entidad AutorizacionUsuario.
        /// </summary>
        public async Task<EAutorizacionUsuario> AutorizacionUsuarioXId(Int64 autorizacionUsuarioId)
        {
            _conexion.AddParamIn(autorizacionUsuarioId);
            return await _conexion.LoadEntityAsync<EAutorizacionUsuario>("NCAutorizacionesUsuariosCI");
        }
        /// <summary>
        /// Permite insertar la entidad AutorizacionUsuario.
        /// </summary>
        protected async Task<Int64> AutorizacionUsuarioInserta(EAutorizacionUsuario autorizacionUsuario)
        {
            await _conexion.EntityUpdateAsync(autorizacionUsuario, MAccionesBd.Inserta, "NCAutorizacionesUsuariosIAE");
            return autorizacionUsuario.AutorizacionUsuarioId;

            //           _conexion.AddParamEntity(autorizacionUsuario, MAccionesBd.Inserta);
            //Int64 vResultado = await _conexion.ExecuteScalarAsync<Int64>("NCAutorizacionesUsuariosIAE");
            //return vResultado;
        }
        /// <summary>
        /// Permite actualizar la entidad AutorizacionUsuario.
        /// </summary>
        protected async Task<Boolean> AutorizacionUsuarioActualiza(EAutorizacionUsuario autorizacionUsuario)
        {
            return await _conexion.EntityUpdateAsync(autorizacionUsuario, MAccionesBd.Actualiza, "NCAutorizacionesUsuariosIAE");

            //           _conexion.AddParamEntity(autorizacionUsuario, MAccionesBd.Actualiza);
            //await _conexion.ExecuteScalarAsync("NCAutorizacionesUsuariosIAE");
            //return Mensajes.Ok;
        }
        /// <summary>
        /// Permite eliminar la entidad AutorizacionUsuario.
        /// </summary>
        protected async Task<Boolean> AutorizacionUsuarioElimina(EAutorizacionUsuario autorizacionUsuario)
        {
            return await _conexion.EntityUpdateAsync(autorizacionUsuario, MAccionesBd.Elimina, "NCAutorizacionesUsuariosIAE");

            //           _conexion.AddParamEntity(autorizacionUsuario, MAccionesBd.Elimina);
            //return await _conexion.ExecuteNonQueryRetAsync("NCAutorizacionesUsuariosIAE");
        }
        #endregion

        #endregion
    }
}
