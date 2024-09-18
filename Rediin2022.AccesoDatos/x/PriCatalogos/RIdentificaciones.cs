using DSEntityNetX.Common.Casting;
using DSEntityNetX.DataAccess;
using DSMetodNetX.AccesoDatos;
using DSMetodNetX.Entidades;
using DSMetodNetX.Idioma;
using Rediin2022.Entidades.Idioma;
using Rediin2022.Entidades.PriCatalogos;
using System;
using System.Collections.Generic;

namespace Rediin2022.AccesoDatos.PriCatalogos
{
    /// <summary>
    /// Repositorio.
    /// </summary>
    [Serializable]
    public class RIdentificaciones : MRepositorio
    {
        #region Constructores
        /// <summary>
        /// Repositorio.
        /// </summary>
        public RIdentificaciones(IMConexionEntidad conexion)
            : base(conexion)
        {
        }
        #endregion

        #region Funciones

        #region Identificacion (Identificaciones)
        /// <summary>
        /// Consulta paginada de la entidad Identificacion.
        /// </summary>
        public EIdentificacionPag IdentificacionPag(EIdentificacionFiltro identificacionFiltro)
        {
            return base.EntidadPag<EIdentificacionPag>(identificacionFiltro,
                identificacionPag =>
                {
                    _conexion.AddParamFilterTL(identificacionFiltro);
                    _conexion.LoadEntity<EIdentificacionPag>("NCIdentificacionesCP", identificacionPag);
                },
                identificacionPag =>
                {
                    _conexion.AddParamFilterPag(identificacionFiltro);
                    identificacionPag.Pagina = _conexion.LoadEntities<EIdentificacion>("NCIdentificacionesCP");
                });
        }
        /// <summary>
        /// Consulta por id de la entidad Identificacion.
        /// </summary>
        public EIdentificacion IdentificacionXId(Int64 identificacionId)
        {
            _conexion.AddParamIn(nameof(identificacionId), identificacionId);
            return _conexion.LoadEntity<EIdentificacion>("NCIdentificacionesCI");
        }
        /// <summary>
        /// Consulta para combos de la entidad Identificacion.
        /// </summary>
        public List<MEElemento> IdentificacionCmb()
        {
            return _conexion.LoadCmb<MEElemento>("NCIdentificacionesCCmb");
        }
        /// <summary>
        /// Permite insertar la entidad Identificacion.
        /// </summary>
        protected Int64 IdentificacionInserta(EIdentificacion identificacion)
        {
            _conexion.AddParamEntity(identificacion, MAccionesBd.Inserta);
            Int64 vResultado = _conexion.ExecuteScalarVal("NCIdentificacionesIAE",
                                                          MensajesXId.IdentificacionNombre);
            return vResultado;
        }
        /// <summary>
        /// Permite actualizar la entidad Identificacion.
        /// </summary>
        protected Boolean IdentificacionActualiza(EIdentificacion identificacion)
        {
            _conexion.AddParamEntity(identificacion, MAccionesBd.Actualiza);
            _conexion.ExecuteScalarVal("NCIdentificacionesIAE",
                                       MensajesXId.IdentificacionNombre);
            return _mensajes.Ok;
        }
        /// <summary>
        /// Permite eliminar la entidad Identificacion.
        /// </summary>
        protected Boolean IdentificacionElimina(EIdentificacion identificacion)
        {
            _conexion.AddParamEntity(identificacion, MAccionesBd.Elimina);
            return _conexion.ExecuteNonQueryRet("NCIdentificacionesIAE");
        }
        #endregion

        #endregion
    }
}
