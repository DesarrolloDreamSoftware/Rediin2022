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
    public class APLIdentificaciones : MAplicacion, INIdentificaciones
    {
        #region Constructores
        /// <summary>
        /// APL Para conexion con un API.
        /// </summary>
        public APLIdentificaciones(IMApiCliente api) : base(api)
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
            return Call<EIdentificacionPag>(NomFn(), identificacionFiltro);
        }
        /// <summary>
        /// Consulta por id de la entidad Identificacion.
        /// </summary>
        public EIdentificacion IdentificacionXId(Int64 identificacionId)
        {
            return Call<EIdentificacion>(NomFn(),
                                         identificacionId);
        }
        /// <summary>
        /// Consulta para combos de la entidad Identificacion.
        /// </summary>
        public List<MEElemento> IdentificacionCmb()
        {
            return Call<List<MEElemento>>(NomFn());
        }
        /// <summary>
        /// Permite insertar la entidad Identificacion.
        /// </summary>
        public Int64 IdentificacionInserta(EIdentificacion identificacion)
        {
            return Call<Int64>(NomFn(), identificacion);
        }
        /// <summary>
        /// Permite actualizar la entidad Identificacion.
        /// </summary>
        public Boolean IdentificacionActualiza(EIdentificacion identificacion)
        {
            return Call<Boolean>(NomFn(), identificacion);
        }
        /// <summary>
        /// Permite eliminar la entidad Identificacion.
        /// </summary>
        public Boolean IdentificacionElimina(EIdentificacion identificacion)
        {
            return Call<Boolean>(NomFn(), identificacion);
        }
        /// <summary>
        /// Reglas de negocio de la entidad Identificacion.
        /// </summary>
        public List<MEReglaNeg> IdentificacionReglas()
        {
            return Call<List<MEReglaNeg>>(NomFn());
        }
        #endregion

        #endregion
    }
}
