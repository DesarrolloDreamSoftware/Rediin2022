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
    public class APLSapBancos : MAplicacion, INSapBancos
    {
        #region Constructores
        /// <summary>
        /// APL Para conexion con un API.
        /// </summary>
        public APLSapBancos(IMApiCliente api) : base(api)
        {
        }
        #endregion

        #region Funciones

        #region SapBanco (SapBancos)
        /// <summary>
        /// Consulta paginada de la entidad SapBanco.
        /// </summary>
        public ESapBancoPag SapBancoPag(ESapBancoFiltro sapBancoFiltro)
        {
            return Call<ESapBancoPag>(NomFn(), sapBancoFiltro);
        }
        /// <summary>
        /// Consulta por id de la entidad SapBanco.
        /// </summary>
        public ESapBanco SapBancoXId(String sapBancoId)
        {
            return Call<ESapBanco>(NomFn(),
                                   sapBancoId);
        }
        /// <summary>
        /// Consulta para combos de la entidad SapBanco.
        /// </summary>
        public List<MEElemento> SapBancoCmb()
        {
            return Call<List<MEElemento>>(NomFn());
        }
        /// <summary>
        /// Permite insertar la entidad SapBanco.
        /// </summary>
        public Boolean SapBancoInserta(ESapBanco sapBanco)
        {
            return Call<Boolean>(NomFn(), sapBanco);
        }
        /// <summary>
        /// Permite actualizar la entidad SapBanco.
        /// </summary>
        public Boolean SapBancoActualiza(ESapBanco sapBanco)
        {
            return Call<Boolean>(NomFn(), sapBanco);
        }
        /// <summary>
        /// Permite eliminar la entidad SapBanco.
        /// </summary>
        public Boolean SapBancoElimina(ESapBanco sapBanco)
        {
            return Call<Boolean>(NomFn(), sapBanco);
        }
        /// <summary>
        /// Exporta datos a Excel.
        /// </summary>
        public MEDatosArchivo SapBancoExporta(ESapBancoFiltro sapBancoFiltro)
        {
            return Call<MEDatosArchivo>(NomFn(),
                                        sapBancoFiltro);
        }
        /// <summary>
        /// Reglas de negocio de la entidad SapBanco.
        /// </summary>
        public List<MEReglaNeg> SapBancoReglas()
        {
            return Call<List<MEReglaNeg>>(NomFn());
        }
        #endregion

        #endregion
    }
}
