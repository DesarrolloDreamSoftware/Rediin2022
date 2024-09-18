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
    public class NSapCondicionesPago : RSapCondicionesPago, INSapCondicionesPago
    {
        #region Variables
        /// <summary>
        /// Reglas de negocio.
        /// </summary>
        private IMReglasNeg<ESapCondicionPago> _sapCondicionPagoReglas = null;
        /// <summary>
        /// Objeto para exportacion a excel.
        /// </summary>
        private MArchivoExcel _archivoExcel = null;
        #endregion

        #region Constructores
        /// <summary>
        /// Negocio.
        /// </summary>
        public NSapCondicionesPago(IMConexionEntidad conexion,
                                   MArchivoExcel archivoExcel)
            : base(conexion)
        {
            _archivoExcel = archivoExcel;
        }
        #endregion

        #region Funciones

        #region SapCondicionPago (SapCondicionesPago)
        /// <summary>
        /// Esta funcion valida e inserta un registro en la base de datos.
        /// </summary>
        public new async Task<Boolean> SapCondicionPagoInserta(ESapCondicionPago sapCondicionPago)
        {
            //Validacion
            if (!SapCondicionPagoValida(sapCondicionPago))
                return false;

            //Persistencia
            return await base.SapCondicionPagoInserta(sapCondicionPago);
        }
        /// <summary>
        /// Valida y actualiza un registro en la base de datos.
        /// </summary>
        public new async Task<Boolean> SapCondicionPagoActualiza(ESapCondicionPago sapCondicionPago)
        {
            //Validacion
            if (!SapCondicionPagoValida(sapCondicionPago))
                return false;

            //Persistencia
            return await base.SapCondicionPagoActualiza(sapCondicionPago);
        }
        /// <summary>
        /// Elimina un registro de la base de datos.
        /// </summary>
        /// <returns></returns>
        public new async Task<Boolean> SapCondicionPagoElimina(ESapCondicionPago sapCondicionPago)
        {
            //Validacion
            SapCondicionPagoReglasNeg().ValidateProperty(sapCondicionPago, e => e.SapCondicionPagoId);
            if (!Mensajes.Ok)
                return false;

            //Persistencia
            return await base.SapCondicionPagoElimina(sapCondicionPago);
        }
        /// <summary>
        /// Exporta datos a Excel.
        /// </summary>
        public async Task<string> SapCondicionPagoExporta(ESapCondicionPagoFiltro sapCondicionPagoFiltro)
        {
            return await base.SapCondicionPagoExporta(sapCondicionPagoFiltro,
                                                      _archivoExcel);
        }
        /// <summary>
        /// Reglas de negocio.
        /// </summary>
        public async Task<List<MEReglaNeg>> SapCondicionPagoReglas()
        {
            return await Task.Run(() => SapCondicionPagoReglasNeg().Rules);
        }
        /// <summary>
        /// Validacion para inserta y actualiza.
        /// </summary>
        private Boolean SapCondicionPagoValida(ESapCondicionPago sapCondicionPago)
        {
            Mensajes.Initialize();
            if (!SapCondicionPagoReglasNeg().Validate(sapCondicionPago))
                return false;

            //Validaciones adicionales

            return Mensajes.Ok;
        }
        /// <summary>
        /// Crea las reglas de negocio.
        /// </summary>
        private IMReglasNeg<ESapCondicionPago> SapCondicionPagoReglasNeg()
        {
            if (_sapCondicionPagoReglas != null)
                return _sapCondicionPagoReglas;

            _sapCondicionPagoReglas = Validaciones.CreaReglasNeg<ESapCondicionPago>(Mensajes);
            _sapCondicionPagoReglas.AddSL(e => e.SapCondicionPagoId, 2, 50);
            _sapCondicionPagoReglas.AddSL(e => e.SapCondicionPagoNombre, 2, 120);
            _sapCondicionPagoReglas.AddSL(e => e.Activo);

            return _sapCondicionPagoReglas;
        }
        #endregion

        #endregion
    }
}
