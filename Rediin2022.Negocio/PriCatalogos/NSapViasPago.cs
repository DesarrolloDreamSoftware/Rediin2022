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
    public class NSapViasPago : RSapViasPago, INSapViasPago
    {
        #region Variables
        /// <summary>
        /// Reglas de negocio.
        /// </summary>
        private IMReglasNeg<ESapViaPago> _sapViaPagoReglas = null;
        /// <summary>
        /// Objeto para exportacion a excel.
        /// </summary>
        private MArchivoExcel _archivoExcel = null;
        #endregion

        #region Constructores
        /// <summary>
        /// Negocio.
        /// </summary>
        public NSapViasPago(IMConexionEntidad conexion,
                            MArchivoExcel archivoExcel)
            : base(conexion)
        {
            _archivoExcel = archivoExcel;
        }
        #endregion

        #region Funciones

        #region SapViaPago (SapViasPago)
        /// <summary>
        /// Esta funcion valida e inserta un registro en la base de datos.
        /// </summary>
        public new async Task<Boolean> SapViaPagoInserta(ESapViaPago sapViaPago)
        {
            //Validacion
            if (!SapViaPagoValida(sapViaPago))
                return false;

            //Persistencia
            return await base.SapViaPagoInserta(sapViaPago);
        }
        /// <summary>
        /// Valida y actualiza un registro en la base de datos.
        /// </summary>
        public new async Task<Boolean> SapViaPagoActualiza(ESapViaPago sapViaPago)
        {
            //Validacion
            if (!SapViaPagoValida(sapViaPago))
                return false;

            //Persistencia
            return await base.SapViaPagoActualiza(sapViaPago);
        }
        /// <summary>
        /// Elimina un registro de la base de datos.
        /// </summary>
        /// <returns></returns>
        public new async Task<Boolean> SapViaPagoElimina(ESapViaPago sapViaPago)
        {
            //Validacion
            SapViaPagoReglasNeg().ValidateProperty(sapViaPago, e => e.SapViaPagoId);
            if (!Mensajes.Ok)
                return false;

            //Persistencia
            return await base.SapViaPagoElimina(sapViaPago);
        }
        /// <summary>
        /// Exporta datos a Excel.
        /// </summary>
        public async Task<string> SapViaPagoExporta(ESapViaPagoFiltro sapViaPagoFiltro)
        {
            return await base.SapViaPagoExporta(sapViaPagoFiltro,
                                                _archivoExcel);
        }
        /// <summary>
        /// Reglas de negocio.
        /// </summary>
        public async Task<List<MEReglaNeg>> SapViaPagoReglas()
        {
            return await Task.Run(() => SapViaPagoReglasNeg().Rules);
        }
        /// <summary>
        /// Validacion para inserta y actualiza.
        /// </summary>
        private Boolean SapViaPagoValida(ESapViaPago sapViaPago)
        {
            Mensajes.Initialize();
            if (!SapViaPagoReglasNeg().Validate(sapViaPago))
                return false;

            //Validaciones adicionales

            return Mensajes.Ok;
        }
        /// <summary>
        /// Crea las reglas de negocio.
        /// </summary>
        private IMReglasNeg<ESapViaPago> SapViaPagoReglasNeg()
        {
            if (_sapViaPagoReglas != null)
                return _sapViaPagoReglas;

            _sapViaPagoReglas = Validaciones.CreaReglasNeg<ESapViaPago>(Mensajes);
            _sapViaPagoReglas.AddSL(e => e.SapViaPagoId, 1, 50);
            _sapViaPagoReglas.AddSL(e => e.SapViaPagoNombre, 2, 120);
            _sapViaPagoReglas.AddSL(e => e.Activo);

            return _sapViaPagoReglas;
        }
        #endregion

        #endregion
    }
}
