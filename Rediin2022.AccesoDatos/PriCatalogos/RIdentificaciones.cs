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
    /// <summary>
    /// Repositorio.
    /// </summary>
    [Serializable]
    public class RIdentificaciones : MRepositorio
    {
    #region Variables
    /// <summary>
    /// Conexión.
    /// </summary>
    private IMConexionEntidad _conexion;
    #endregion

        #region Constructores
        /// <summary>
        /// Repositorio.
        /// </summary>
        public RIdentificaciones(IMConexionEntidad conexion)
            : base(conexion)
        {
            _conexion = conexion;
    }
        #endregion

        #region Funciones

        #region Identificacion (Identificaciones)
        /// <summary>
        /// Consulta paginada de la entidad Identificacion.
        /// </summary>
        public async Task<EIdentificacionPag> IdentificacionPag(EIdentificacionFiltro identificacionFiltro)
        {
        return await _conexion.EntidadPagAsync<EIdentificacion,
                                                EIdentificacionPag,
                                                EIdentificacionFiltro>(identificacionFiltro, "NCIdentificacionesCP");

            //return base.EntidadPagAsync<EIdentificacionPag>(identificacionFiltro,
            //               identificacionPag =>
            //    {
            //        _conexion.AddParamFilterTL(identificacionFiltro);
            //        _conexion.LoadEntity<EIdentificacionPag>("NCIdentificacionesCP", identificacionPag);
            //    },
            //    identificacionPag =>
            //    {
            //        _conexion.AddParamFilterPag(identificacionFiltro);
            //        identificacionPag.Pagina = _conexion.LoadEntities<EIdentificacion>("NCIdentificacionesCP");
            //    });
        }
        /// <summary>
        /// Consulta por id de la entidad Identificacion.
        /// </summary>
        public async Task<EIdentificacion> IdentificacionXId(Int64 identificacionId)
        {
            _conexion.AddParamIn(identificacionId);
            return await _conexion.LoadEntityAsync<EIdentificacion>("NCIdentificacionesCI");
        }
        /// <summary>
        /// Consulta para combos de la entidad Identificacion.
        /// </summary>
        public async Task<List<MEElemento>> IdentificacionCmb()
        {
            return await _conexion.EntidadCmbAsync("NCIdentificacionesCCmb");
        }
        /// <summary>
        /// Permite insertar la entidad Identificacion.
        /// </summary>
        protected async Task<Int64> IdentificacionInserta(EIdentificacion identificacion)
        {
        await _conexion.EntityUpdateAsync(identificacion, MAccionesBd.Inserta, "NCIdentificacionesIAE");
                   return identificacion.IdentificacionId;

            //           _conexion.AddParamEntity(identificacion, MAccionesBd.Inserta);
            //Int64 vResultado = await _conexion.ExecuteScalarValAsync("NCIdentificacionesIAE",
            //                                              MensajesXId.IdentificacionNombre);
            //return vResultado;
        }
        /// <summary>
        /// Permite actualizar la entidad Identificacion.
        /// </summary>
        protected async Task<Boolean> IdentificacionActualiza(EIdentificacion identificacion)
        {
        return await _conexion.EntityUpdateAsync(identificacion, MAccionesBd.Actualiza, "NCIdentificacionesIAE");

            //           _conexion.AddParamEntity(identificacion, MAccionesBd.Actualiza);
            //await _conexion.ExecuteScalarValAsync("NCIdentificacionesIAE",
            //                           MensajesXId.IdentificacionNombre);
            //return Mensajes.Ok;
        }
        /// <summary>
        /// Permite eliminar la entidad Identificacion.
        /// </summary>
        protected async Task<Boolean> IdentificacionElimina(EIdentificacion identificacion)
        {
        return await _conexion.EntityUpdateAsync(identificacion, MAccionesBd.Elimina, "NCIdentificacionesIAE");

            //           _conexion.AddParamEntity(identificacion, MAccionesBd.Elimina);
            //return await _conexion.ExecuteNonQueryRetAsync("NCIdentificacionesIAE");
        }
        #endregion

        #endregion
    }
}
