using DSEntityNetX.Business;
using DSEntityNetX.Common.Casting;
using DSEntityNetX.DataAccess;
using DSMetodNetX.AccesoDatos;
using DSMetodNetX.Entidades;
using DSMetodNetX.Idioma;
using DSMetodNetX.Negocio;
using Rediin2022.AccesoDatos.PriCatalogos;
using Rediin2022.Entidades.Idioma;
using Rediin2022.Entidades.PriCatalogos;
using System;
using System.Collections.Generic;

namespace Rediin2022.Negocio.PriCatalogos
{
    /// <summary>
    /// Negocio.
    /// </summary>
    public class NSapGrupoCuentas : RSapGrupoCuentas, INSapGrupoCuentas
    {
        #region Variables
        /// <summary>
        /// Reglas de negocio.
        /// </summary>
        private IMReglasNeg<ESapGrupoCuenta> _sapGrupoCuentaReglas = null;
        /// <summary>
        /// Objeto para exportacion a excel.
        /// </summary>
        private MArchivoExcel _archivoExcel = null;
        #endregion

        #region Constructores
        /// <summary>
        /// Negocio.
        /// </summary>
        public NSapGrupoCuentas(IMConexionEntidad conexion,
                                MArchivoExcel archivoExcel)
            : base(conexion)
        {
            _archivoExcel = archivoExcel;
        }
        #endregion

        #region Funciones

        #region SapGrupoCuenta (SapGrupoCuentas)
        /// <summary>
        /// Esta funcion valida e inserta un registro en la base de datos.
        /// </summary>
        public new Boolean SapGrupoCuentaInserta(ESapGrupoCuenta sapGrupoCuenta)
        {
            //Validacion
            if (!SapGrupoCuentaValida(sapGrupoCuenta))
                return false;

            //Persistencia
            return base.SapGrupoCuentaInserta(sapGrupoCuenta);
        }
        /// <summary>
        /// Valida y actualiza un registro en la base de datos.
        /// </summary>
        public new Boolean SapGrupoCuentaActualiza(ESapGrupoCuenta sapGrupoCuenta)
        {
            //Validacion
            if (!SapGrupoCuentaValida(sapGrupoCuenta))
                return false;

            //Persistencia
            return base.SapGrupoCuentaActualiza(sapGrupoCuenta);
        }
        /// <summary>
        /// Elimina un registro de la base de datos.
        /// </summary>
        /// <returns></returns>
        public new Boolean SapGrupoCuentaElimina(ESapGrupoCuenta sapGrupoCuenta)
        {
            //Validacion
            SapGrupoCuentaReglasNeg().ValidateProperty(sapGrupoCuenta, e => e.SapGrupoCuentaId);
            if (!_mensajes.Ok)
                return false;

            //Persistencia
            return base.SapGrupoCuentaElimina(sapGrupoCuenta);
        }
        /// <summary>
        /// Exporta datos a Excel.
        /// </summary>
        public MEDatosArchivo SapGrupoCuentaExporta(ESapGrupoCuentaFiltro sapGrupoCuentaFiltro)
        {
            return base.SapGrupoCuentaExporta(sapGrupoCuentaFiltro,
                                              _archivoExcel);
        }
        /// <summary>
        /// Reglas de negocio.
        /// </summary>
        public List<MEReglaNeg> SapGrupoCuentaReglas()
        {
            return SapGrupoCuentaReglasNeg().Rules;
        }
        /// <summary>
        /// Validacion para inserta y actualiza.
        /// </summary>
        private Boolean SapGrupoCuentaValida(ESapGrupoCuenta sapGrupoCuenta)
        {
            _mensajes.Initialize();
            if (!SapGrupoCuentaReglasNeg().Validate(sapGrupoCuenta))
                return false;

            //Validaciones adicionales

            return _mensajes.Ok;
        }
        /// <summary>
        /// Crea las reglas de negocio.
        /// </summary>
        private IMReglasNeg<ESapGrupoCuenta> SapGrupoCuentaReglasNeg()
        {
            if (_sapGrupoCuentaReglas != null)
                return _sapGrupoCuentaReglas;

            _sapGrupoCuentaReglas = Validaciones.CreaReglasNeg<ESapGrupoCuenta>(_mensajes);
            _sapGrupoCuentaReglas.AddSL(e => e.SapGrupoCuentaId, 2, 50);
            _sapGrupoCuentaReglas.AddSL(e => e.SapGrupoCuentaNombre, 2, 120);
            _sapGrupoCuentaReglas.AddSL(e => e.Activo);

            return _sapGrupoCuentaReglas;
        }
        #endregion

        #endregion
    }
}
