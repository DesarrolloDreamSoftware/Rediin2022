using DSMetodNetX.Aplicacion;
using DSMetodNetX.Entidades;
using DSMetodNetX.Negocio;
using Rediin2022.Entidades.PriCatalogos;
using System;
using System.Collections.Generic;

namespace Rediin2022.Aplicacion.PriCatalogos
{
    public class APLAutorizaciones : MAplicacion, INAutorizaciones
    {
        #region Constructores
        public APLAutorizaciones(IMApiCliente api) : base(api)
        {
        }
        #endregion

        #region Funciones

        #region Autorizacion (Autorizaciones)
        /// <summary>
        /// Consulta paginada de la entidad Autorizacion.
        /// </summary>
        public EAutorizacionPag AutorizacionPag(EAutorizacionFiltro autorizacionFiltro)
        {
            return Call<EAutorizacionPag>(NomFn(), autorizacionFiltro);
        }
        /// <summary>
        /// Consulta por id de la entidad Autorizacion.
        /// </summary>
        public EAutorizacion AutorizacionXId(Int64 autorizacionId)
        {
            return Call<EAutorizacion>(NomFn(),
                                       autorizacionId);
        }
        /// <summary>
        /// Permite insertar la entidad Autorizacion.
        /// </summary>
        public Int64 AutorizacionInserta(EAutorizacion autorizacion)
        {
            return Call<Int64>(NomFn(), autorizacion);
        }
        /// <summary>
        /// Permite actualizar la entidad Autorizacion.
        /// </summary>
        public Boolean AutorizacionActualiza(EAutorizacion autorizacion)
        {
            return Call<Boolean>(NomFn(), autorizacion);
        }
        /// <summary>
        /// Permite eliminar la entidad Autorizacion.
        /// </summary>
        public Boolean AutorizacionElimina(EAutorizacion autorizacion)
        {
            return Call<Boolean>(NomFn(), autorizacion);
        }
        /// <summary>
        /// Reglas de negocio de la entidad Autorizacion.
        /// </summary>
        public List<MEReglaNeg> AutorizacionReglas()
        {
            return Call<List<MEReglaNeg>>(NomFn());
        }
        #endregion

        #region AutorizacionUsuario (AutorizacionesUsuarios)
        /// <summary>
        /// Consulta paginada de la entidad AutorizacionUsuario.
        /// </summary>
        public EAutorizacionUsuarioPag AutorizacionUsuarioPag(EAutorizacionUsuarioFiltro autorizacionUsuarioFiltro)
        {
            return Call<EAutorizacionUsuarioPag>(NomFn(), autorizacionUsuarioFiltro);
        }
        /// <summary>
        /// Consulta por id de la entidad AutorizacionUsuario.
        /// </summary>
        public EAutorizacionUsuario AutorizacionUsuarioXId(Int64 autorizacionUsuarioId)
        {
            return Call<EAutorizacionUsuario>(NomFn(),
                                              autorizacionUsuarioId);
        }
        /// <summary>
        /// Permite insertar la entidad AutorizacionUsuario.
        /// </summary>
        public Int64 AutorizacionUsuarioInserta(EAutorizacionUsuario autorizacionUsuario)
        {
            return Call<Int64>(NomFn(), autorizacionUsuario);
        }
        /// <summary>
        /// Permite actualizar la entidad AutorizacionUsuario.
        /// </summary>
        public Boolean AutorizacionUsuarioActualiza(EAutorizacionUsuario autorizacionUsuario)
        {
            return Call<Boolean>(NomFn(), autorizacionUsuario);
        }
        /// <summary>
        /// Permite eliminar la entidad AutorizacionUsuario.
        /// </summary>
        public Boolean AutorizacionUsuarioElimina(EAutorizacionUsuario autorizacionUsuario)
        {
            return Call<Boolean>(NomFn(), autorizacionUsuario);
        }
        /// <summary>
        /// Reglas de negocio de la entidad AutorizacionUsuario.
        /// </summary>
        public List<MEReglaNeg> AutorizacionUsuarioReglas()
        {
            return Call<List<MEReglaNeg>>(NomFn());
        }
        #endregion

        #endregion
    }
}
