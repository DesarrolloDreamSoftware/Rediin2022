using DSEntityNetX.Business;
using DSEntityNetX.Common.Casting;
using DSEntityNetX.DataAccess;
using DSMetodNetX.AccesoDatos;
using DSMetodNetX.Comun;
using DSMetodNetX.Entidades;

using DSMetodNetX.Negocio;
using Rediin2022.AccesoDatos.PriCatalogos;
using Rediin2022.Entidades.Idioma;
using Rediin2022.Entidades.PriCatalogos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public new async Task<Boolean> SapCuentaAsociadaInserta(ESapCuentaAsociada sapCuentaAsociada)
        {
            //Validacion
            if (!SapCuentaAsociadaValida(sapCuentaAsociada))
                return false;

            //Persistencia
            return await base.SapCuentaAsociadaInserta(sapCuentaAsociada);
        }
        /// <summary>
        /// Valida y actualiza un registro en la base de datos.
        /// </summary>
        public new async Task<Boolean> SapCuentaAsociadaActualiza(ESapCuentaAsociada sapCuentaAsociada)
        {
            //Validacion
            if (!SapCuentaAsociadaValida(sapCuentaAsociada))
                return false;

            //Persistencia
            return await base.SapCuentaAsociadaActualiza(sapCuentaAsociada);
        }
        /// <summary>
        /// Elimina un registro de la base de datos.
        /// </summary>
        /// <returns></returns>
        public new async Task<Boolean> SapCuentaAsociadaElimina(ESapCuentaAsociada sapCuentaAsociada)
        {
            //Validacion
            SapCuentaAsociadaReglasNeg().ValidateProperty(sapCuentaAsociada, e => e.SapCuentaAsociadaId);
            if (!Mensajes.Ok)
                return false;

            //Persistencia
            return await base.SapCuentaAsociadaElimina(sapCuentaAsociada);
        }
        /// <summary>
        /// Exporta datos a Excel.
        /// </summary>
        public async Task<string> SapCuentaAsociadaExporta(ESapCuentaAsociadaFiltro sapCuentaAsociadaFiltro)
        {
            return await base.SapCuentaAsociadaExporta(sapCuentaAsociadaFiltro,
                                                       _archivoExcel);
        }
        /// <summary>
        /// Reglas de negocio.
        /// </summary>
        public async Task<List<MEReglaNeg>> SapCuentaAsociadaReglas()
        {
            return await Task.Run(() => SapCuentaAsociadaReglasNeg().Rules);
        }
        /// <summary>
        /// Validacion para inserta y actualiza.
        /// </summary>
        private Boolean SapCuentaAsociadaValida(ESapCuentaAsociada sapCuentaAsociada)
        {
            Mensajes.Initialize();
            if (!SapCuentaAsociadaReglasNeg().Validate(sapCuentaAsociada))
                return false;

            //Validaciones adicionales

            return Mensajes.Ok;
        }
        /// <summary>
        /// Crea las reglas de negocio.
        /// </summary>
        private IMReglasNeg<ESapCuentaAsociada> SapCuentaAsociadaReglasNeg()
        {
            if (_sapCuentaAsociadaReglas != null)
                return _sapCuentaAsociadaReglas;

            _sapCuentaAsociadaReglas = Validaciones.CreaReglasNeg<ESapCuentaAsociada>(Mensajes);
            _sapCuentaAsociadaReglas.AddSL(e => e.SapCuentaAsociadaId, 2, 50);
            _sapCuentaAsociadaReglas.AddSL(e => e.SapCuentaAsociadaNombre, 2, 120);
            _sapCuentaAsociadaReglas.AddSL(e => e.Activo);

            return _sapCuentaAsociadaReglas;
        }
        #endregion

        #endregion
    }
}
