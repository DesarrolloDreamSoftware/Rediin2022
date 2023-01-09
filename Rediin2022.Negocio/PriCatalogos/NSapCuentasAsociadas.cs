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
    public class NSapCuentasAsociadas : RSapCuentasAsociadas, INSapCuentasAsociadas
    {
        #region Variables
        /// <summary>
        /// Reglas de negocio.
        /// </summary>
        private IMReglasNeg<ESapCuentaAsociada> _sapCuentaAsociadaReglas = null;
        /// <summary>
        /// Objeto para exportacion a excel.
        /// </summary>
        private MArchivoExcel _archivoExcel = null;
        #endregion

        #region Constructores
        /// <summary>
        /// Negocio.
        /// </summary>
        public NSapCuentasAsociadas(IMConexionEntidad conexion,
                                    MArchivoExcel archivoExcel)
            : base(conexion)
        {
            _archivoExcel = archivoExcel;
        }
        #endregion

        #region Funciones

        #region SapCuentaAsociada (SapCuentasAsociadas)
        /// <summary>
        /// Esta funcion valida e inserta un registro en la base de datos.
        /// </summary>
        public new Boolean SapCuentaAsociadaInserta(ESapCuentaAsociada sapCuentaAsociada)
        {
            //Validacion
            if (!SapCuentaAsociadaValida(sapCuentaAsociada))
                return false;

            //Persistencia
            return base.SapCuentaAsociadaInserta(sapCuentaAsociada);
        }
        /// <summary>
        /// Valida y actualiza un registro en la base de datos.
        /// </summary>
        public new Boolean SapCuentaAsociadaActualiza(ESapCuentaAsociada sapCuentaAsociada)
        {
            //Validacion
            if (!SapCuentaAsociadaValida(sapCuentaAsociada))
                return false;

            //Persistencia
            return base.SapCuentaAsociadaActualiza(sapCuentaAsociada);
        }
        /// <summary>
        /// Elimina un registro de la base de datos.
        /// </summary>
        /// <returns></returns>
        public new Boolean SapCuentaAsociadaElimina(ESapCuentaAsociada sapCuentaAsociada)
        {
            //Validacion
            SapCuentaAsociadaReglasNeg().ValidateProperty(sapCuentaAsociada, e => e.SapCuentaAsociadaId);
            if (!_mensajes.Ok)
                return false;

            //Persistencia
            return base.SapCuentaAsociadaElimina(sapCuentaAsociada);
        }
        /// <summary>
        /// Exporta datos a Excel.
        /// </summary>
        public MEDatosArchivo SapCuentaAsociadaExporta(ESapCuentaAsociadaFiltro sapCuentaAsociadaFiltro)
        {
            return base.SapCuentaAsociadaExporta(sapCuentaAsociadaFiltro,
                                                 _archivoExcel);
        }
        /// <summary>
        /// Reglas de negocio.
        /// </summary>
        public List<MEReglaNeg> SapCuentaAsociadaReglas()
        {
            return SapCuentaAsociadaReglasNeg().Rules;
        }
        /// <summary>
        /// Validacion para inserta y actualiza.
        /// </summary>
        private Boolean SapCuentaAsociadaValida(ESapCuentaAsociada sapCuentaAsociada)
        {
            _mensajes.Initialize();
            if (!SapCuentaAsociadaReglasNeg().Validate(sapCuentaAsociada))
                return false;

            //Validaciones adicionales

            return _mensajes.Ok;
        }
        /// <summary>
        /// Crea las reglas de negocio.
        /// </summary>
        private IMReglasNeg<ESapCuentaAsociada> SapCuentaAsociadaReglasNeg()
        {
            if (_sapCuentaAsociadaReglas != null)
                return _sapCuentaAsociadaReglas;

            _sapCuentaAsociadaReglas = Validaciones.CreaReglasNeg<ESapCuentaAsociada>(_mensajes);
            _sapCuentaAsociadaReglas.AddSL(e => e.SapCuentaAsociadaId, 2, 50);
            _sapCuentaAsociadaReglas.AddSL(e => e.SapCuentaAsociadaNombre, 2, 120);
            _sapCuentaAsociadaReglas.AddSL(e => e.Activo);

            return _sapCuentaAsociadaReglas;
        }
        #endregion

        #endregion
    }
}
