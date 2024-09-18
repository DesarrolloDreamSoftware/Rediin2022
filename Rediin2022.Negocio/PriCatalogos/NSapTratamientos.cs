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
        public new async Task<Boolean> SapTratamientoInserta(ESapTratamiento sapTratamiento)
        {
            //Validacion
            if (!SapTratamientoValida(sapTratamiento))
                return false;

            //Persistencia
            return await base.SapTratamientoInserta(sapTratamiento);
        }
        /// <summary>
        /// Valida y actualiza un registro en la base de datos.
        /// </summary>
        public new async Task<Boolean> SapTratamientoActualiza(ESapTratamiento sapTratamiento)
        {
            //Validacion
            if (!SapTratamientoValida(sapTratamiento))
                return false;

            //Persistencia
            return await base.SapTratamientoActualiza(sapTratamiento);
        }
        /// <summary>
        /// Elimina un registro de la base de datos.
        /// </summary>
        /// <returns></returns>
        public new async Task<Boolean> SapTratamientoElimina(ESapTratamiento sapTratamiento)
        {
            //Validacion
            SapTratamientoReglasNeg().ValidateProperty(sapTratamiento, e => e.SapTratamientoId);
            if (!Mensajes.Ok)
                return false;

            //Persistencia
            return await base.SapTratamientoElimina(sapTratamiento);
        }
        /// <summary>
        /// Exporta datos a Excel.
        /// </summary>
        public async Task<string> SapTratamientoExporta(ESapTratamientoFiltro sapTratamientoFiltro)
        {
            return await base.SapTratamientoExporta(sapTratamientoFiltro,
                                                    _archivoExcel);
        }
        /// <summary>
        /// Reglas de negocio.
        /// </summary>
        public async Task<List<MEReglaNeg>> SapTratamientoReglas()
        {
            return await Task.Run(() => SapTratamientoReglasNeg().Rules);
        }
        /// <summary>
        /// Validacion para inserta y actualiza.
        /// </summary>
        private Boolean SapTratamientoValida(ESapTratamiento sapTratamiento)
        {
            Mensajes.Initialize();
            if (!SapTratamientoReglasNeg().Validate(sapTratamiento))
                return false;

            //Validaciones adicionales

            return Mensajes.Ok;
        }
        /// <summary>
        /// Crea las reglas de negocio.
        /// </summary>
        private IMReglasNeg<ESapTratamiento> SapTratamientoReglasNeg()
        {
            if (_sapTratamientoReglas != null)
                return _sapTratamientoReglas;

            _sapTratamientoReglas = Validaciones.CreaReglasNeg<ESapTratamiento>(Mensajes);
            _sapTratamientoReglas.AddSL(e => e.SapTratamientoId, 2, 50);
            _sapTratamientoReglas.AddSL(e => e.SapTratamientoNombre, 2, 120);
            _sapTratamientoReglas.AddSL(e => e.Activo);

            return _sapTratamientoReglas;
        }
        #endregion

        #endregion
    }
}
