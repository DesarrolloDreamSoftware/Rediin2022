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
    public class NSapTratamientos : RSapTratamientos, INSapTratamientos
    {
        #region Variables
        /// <summary>
        /// Reglas de negocio.
        /// </summary>
        private IMReglasNeg<ESapTratamiento> _sapTratamientoReglas = null;
        /// <summary>
        /// Objeto para exportacion a excel.
        /// </summary>
        private MArchivoExcel _archivoExcel = null;
        #endregion

        #region Constructores
        /// <summary>
        /// Negocio.
        /// </summary>
        public NSapTratamientos(IMConexionEntidad conexion,
                                MArchivoExcel archivoExcel)
            : base(conexion)
        {
            _archivoExcel = archivoExcel;
        }
        #endregion

        #region Funciones

        #region SapTratamiento (SapTratamientos)
        /// <summary>
        /// Esta funcion valida e inserta un registro en la base de datos.
        /// </summary>
        public new Boolean SapTratamientoInserta(ESapTratamiento sapTratamiento)
        {
            //Validacion
            if (!SapTratamientoValida(sapTratamiento))
                return false;

            //Persistencia
            return base.SapTratamientoInserta(sapTratamiento);
        }
        /// <summary>
        /// Valida y actualiza un registro en la base de datos.
        /// </summary>
        public new Boolean SapTratamientoActualiza(ESapTratamiento sapTratamiento)
        {
            //Validacion
            if (!SapTratamientoValida(sapTratamiento))
                return false;

            //Persistencia
            return base.SapTratamientoActualiza(sapTratamiento);
        }
        /// <summary>
        /// Elimina un registro de la base de datos.
        /// </summary>
        /// <returns></returns>
        public new Boolean SapTratamientoElimina(ESapTratamiento sapTratamiento)
        {
            //Validacion
            SapTratamientoReglasNeg().ValidateProperty(sapTratamiento, e => e.SapTratamientoId);
            if (!_mensajes.Ok)
                return false;

            //Persistencia
            return base.SapTratamientoElimina(sapTratamiento);
        }
        /// <summary>
        /// Exporta datos a Excel.
        /// </summary>
        public MEDatosArchivo SapTratamientoExporta(ESapTratamientoFiltro sapTratamientoFiltro)
        {
            return base.SapTratamientoExporta(sapTratamientoFiltro,
                                              _archivoExcel);
        }
        /// <summary>
        /// Reglas de negocio.
        /// </summary>
        public List<MEReglaNeg> SapTratamientoReglas()
        {
            return SapTratamientoReglasNeg().Rules;
        }
        /// <summary>
        /// Validacion para inserta y actualiza.
        /// </summary>
        private Boolean SapTratamientoValida(ESapTratamiento sapTratamiento)
        {
            _mensajes.Initialize();
            if (!SapTratamientoReglasNeg().Validate(sapTratamiento))
                return false;

            //Validaciones adicionales

            return _mensajes.Ok;
        }
        /// <summary>
        /// Crea las reglas de negocio.
        /// </summary>
        private IMReglasNeg<ESapTratamiento> SapTratamientoReglasNeg()
        {
            if (_sapTratamientoReglas != null)
                return _sapTratamientoReglas;

            _sapTratamientoReglas = Validaciones.CreaReglasNeg<ESapTratamiento>(_mensajes);
            _sapTratamientoReglas.AddSL(e => e.SapTratamientoId, 2, 50);
            _sapTratamientoReglas.AddSL(e => e.SapTratamientoNombre, 2, 120);
            _sapTratamientoReglas.AddSL(e => e.Activo);

            return _sapTratamientoReglas;
        }
        #endregion

        #endregion
    }
}
