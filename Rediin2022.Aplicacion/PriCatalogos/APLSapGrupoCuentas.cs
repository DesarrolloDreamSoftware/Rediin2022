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
    public class APLSapGrupoCuentas : MAplicacion, INSapGrupoCuentas
    {
        #region Constructores
        /// <summary>
        /// APL Para conexion con un API.
        /// </summary>
        public APLSapGrupoCuentas(IMApiCliente api) : base(api)
        {
        }
        #endregion

        #region Funciones

        #region SapGrupoCuenta (SapGrupoCuentas)
        /// <summary>
        /// Consulta paginada de la entidad SapGrupoCuenta.
        /// </summary>
        public ESapGrupoCuentaPag SapGrupoCuentaPag(ESapGrupoCuentaFiltro sapGrupoCuentaFiltro)
        {
            return Call<ESapGrupoCuentaPag>(NomFn(), sapGrupoCuentaFiltro);
        }
        /// <summary>
        /// Consulta por id de la entidad SapGrupoCuenta.
        /// </summary>
        public ESapGrupoCuenta SapGrupoCuentaXId(String sapGrupoCuentaId)
        {
            return Call<ESapGrupoCuenta>(NomFn(),
                                         sapGrupoCuentaId);
        }
        /// <summary>
        /// Consulta para combos de la entidad SapGrupoCuenta.
        /// </summary>
        public List<MEElemento> SapGrupoCuentaCmb()
        {
            return Call<List<MEElemento>>(NomFn());
        }
        /// <summary>
        /// Permite insertar la entidad SapGrupoCuenta.
        /// </summary>
        public Boolean SapGrupoCuentaInserta(ESapGrupoCuenta sapGrupoCuenta)
        {
            return Call<Boolean>(NomFn(), sapGrupoCuenta);
        }
        /// <summary>
        /// Permite actualizar la entidad SapGrupoCuenta.
        /// </summary>
        public Boolean SapGrupoCuentaActualiza(ESapGrupoCuenta sapGrupoCuenta)
        {
            return Call<Boolean>(NomFn(), sapGrupoCuenta);
        }
        /// <summary>
        /// Permite eliminar la entidad SapGrupoCuenta.
        /// </summary>
        public Boolean SapGrupoCuentaElimina(ESapGrupoCuenta sapGrupoCuenta)
        {
            return Call<Boolean>(NomFn(), sapGrupoCuenta);
        }
        /// <summary>
        /// Exporta datos a Excel.
        /// </summary>
        public MEDatosArchivo SapGrupoCuentaExporta(ESapGrupoCuentaFiltro sapGrupoCuentaFiltro)
        {
            return Call<MEDatosArchivo>(NomFn(),
                                        sapGrupoCuentaFiltro);
        }
        /// <summary>
        /// Reglas de negocio de la entidad SapGrupoCuenta.
        /// </summary>
        public List<MEReglaNeg> SapGrupoCuentaReglas()
        {
            return Call<List<MEReglaNeg>>(NomFn());
        }
        #endregion

        #endregion
    }
}
