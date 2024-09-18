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
    public class NSapSociedades : RSapSociedades, INSapSociedades
    {
        #region Variables
        /// <summary>
        /// Reglas de negocio.
        /// </summary>
        private IMReglasNeg<ESapSociedad> _sapSociedadReglas = null;
        /// <summary>
        /// Objeto para exportacion a excel.
        /// </summary>
        private MArchivoExcel _archivoExcel = null;
        #endregion

        #region Constructores
        /// <summary>
        /// Negocio.
        /// </summary>
        public NSapSociedades(IMConexionEntidad conexion,
                              MArchivoExcel archivoExcel)
            : base(conexion)
        {
            _archivoExcel = archivoExcel;
        }
        #endregion

        #region Funciones

        #region SapSociedad (SapSociedades)
        /// <summary>
        /// Esta funcion valida e inserta un registro en la base de datos.
        /// </summary>
        public new async Task<Boolean> SapSociedadInserta(ESapSociedad sapSociedad)
        {
            //Validacion
            if (!SapSociedadValida(sapSociedad))
                return false;

            //Persistencia
            return await base.SapSociedadInserta(sapSociedad);
        }
        /// <summary>
        /// Valida y actualiza un registro en la base de datos.
        /// </summary>
        public new async Task<Boolean> SapSociedadActualiza(ESapSociedad sapSociedad)
        {
            //Validacion
            if (!SapSociedadValida(sapSociedad))
                return false;

            //Persistencia
            return await base.SapSociedadActualiza(sapSociedad);
        }
        /// <summary>
        /// Elimina un registro de la base de datos.
        /// </summary>
        /// <returns></returns>
        public new async Task<Boolean> SapSociedadElimina(ESapSociedad sapSociedad)
        {
            //Validacion
            SapSociedadReglasNeg().ValidateProperty(sapSociedad, e => e.SapSociedadId);
            if (!Mensajes.Ok)
                return false;

            //Persistencia
            return await base.SapSociedadElimina(sapSociedad);
        }
        /// <summary>
        /// Exporta datos a Excel.
        /// </summary>
        public async Task<string> SapSociedadExporta(ESapSociedadFiltro sapSociedadFiltro)
        {
            return await base.SapSociedadExporta(sapSociedadFiltro,
                                                 _archivoExcel);
        }
        /// <summary>
        /// Reglas de negocio.
        /// </summary>
        public async Task<List<MEReglaNeg>> SapSociedadReglas()
        {
            return await Task.Run(() => SapSociedadReglasNeg().Rules);
        }
        /// <summary>
        /// Validacion para inserta y actualiza.
        /// </summary>
        private Boolean SapSociedadValida(ESapSociedad sapSociedad)
        {
            Mensajes.Initialize();
            if (!SapSociedadReglasNeg().Validate(sapSociedad))
                return false;

            //Validaciones adicionales

            return Mensajes.Ok;
        }
        /// <summary>
        /// Crea las reglas de negocio.
        /// </summary>
        private IMReglasNeg<ESapSociedad> SapSociedadReglasNeg()
        {
            if (_sapSociedadReglas != null)
                return _sapSociedadReglas;

            _sapSociedadReglas = Validaciones.CreaReglasNeg<ESapSociedad>(Mensajes);
            _sapSociedadReglas.AddSL(e => e.SapSociedadId, 2, 50);
            _sapSociedadReglas.AddSL(e => e.SapSociedadNombre, 2, 120);
            _sapSociedadReglas.AddSL(e => e.Activo);

            return _sapSociedadReglas;
        }
        #endregion

        #endregion
    }
}
