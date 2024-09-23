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
    public class NSapSociedadesGL : RSapSociedadesGL, INSapSociedadesGL
    {
        #region Variables
        /// <summary>
        /// Reglas de negocio.
        /// </summary>
        private IMReglasNeg<ESapSociedadGL> _sapSociedadGLReglas = null;
        /// <summary>
        /// Objeto para exportacion a excel.
        /// </summary>
        private MArchivoExcel _archivoExcel = null;
        #endregion

        #region Constructores
        /// <summary>
        /// Negocio.
        /// </summary>
        public NSapSociedadesGL(IMConexionEntidad conexion,
                                MArchivoExcel archivoExcel)
            : base(conexion)
        {
            _archivoExcel = archivoExcel;
        }
        #endregion

        #region Funciones

        #region SapSociedadGL (SapSociedadesGL)
        /// <summary>
        /// Esta funcion valida e inserta un registro en la base de datos.
        /// </summary>
        public new async Task<Boolean> SapSociedadGLInserta(ESapSociedadGL sapSociedadGL)
        {
            //Validacion
            if (!SapSociedadGLValida(sapSociedadGL))
                return false;

            //Persistencia
            return await base.SapSociedadGLInserta(sapSociedadGL);
        }
        /// <summary>
        /// Valida y actualiza un registro en la base de datos.
        /// </summary>
        public new async Task<Boolean> SapSociedadGLActualiza(ESapSociedadGL sapSociedadGL)
        {
            //Validacion
            if (!SapSociedadGLValida(sapSociedadGL))
                return false;

            //Persistencia
            return await base.SapSociedadGLActualiza(sapSociedadGL);
        }
        /// <summary>
        /// Elimina un registro de la base de datos.
        /// </summary>
        /// <returns></returns>
        public new async Task<Boolean> SapSociedadGLElimina(ESapSociedadGL sapSociedadGL)
        {
            //Validacion
            SapSociedadGLReglasNeg().ValidateProperty(sapSociedadGL, e => e.SapSociedadGLId);
            if (!Mensajes.Ok)
                return false;

            //Persistencia
            return await base.SapSociedadGLElimina(sapSociedadGL);
        }
        /// <summary>
        /// Exporta datos a Excel.
        /// </summary>
        public async Task<string> SapSociedadGLExporta(ESapSociedadGLFiltro sapSociedadGLFiltro)
        {
            return await base.SapSociedadGLExporta(sapSociedadGLFiltro,
                                                   _archivoExcel);
        }
        /// <summary>
        /// Reglas de negocio.
        /// </summary>
        public async Task<List<MEReglaNeg>> SapSociedadGLReglas()
        {
            return await Task.Run(() => SapSociedadGLReglasNeg().Rules);
        }
        /// <summary>
        /// Validacion para inserta y actualiza.
        /// </summary>
        private Boolean SapSociedadGLValida(ESapSociedadGL sapSociedadGL)
        {
            Mensajes.Initialize();
            if (!SapSociedadGLReglasNeg().Validate(sapSociedadGL))
                return false;

            //Validaciones adicionales

            return Mensajes.Ok;
        }
        /// <summary>
        /// Crea las reglas de negocio.
        /// </summary>
        private IMReglasNeg<ESapSociedadGL> SapSociedadGLReglasNeg()
        {
            if (_sapSociedadGLReglas != null)
                return _sapSociedadGLReglas;

            _sapSociedadGLReglas = Validaciones.CreaReglasNeg<ESapSociedadGL>(Mensajes);
            _sapSociedadGLReglas.AddSL(e => e.SapSociedadGLId, 2, 50);
            _sapSociedadGLReglas.AddSL(e => e.SapSociedadGLNombre, 2, 120);
            _sapSociedadGLReglas.AddSL(e => e.Activo);

            return _sapSociedadGLReglas;
        }
        #endregion

        #endregion
    }
}