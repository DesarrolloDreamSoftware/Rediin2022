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
    public class NSapBancos : RSapBancos, INSapBancos
    {
        #region Variables
        /// <summary>
        /// Reglas de negocio.
        /// </summary>
        private IMReglasNeg<ESapBanco> _sapBancoReglas = null;
        /// <summary>
        /// Objeto para exportacion a excel.
        /// </summary>
        private MArchivoExcel _archivoExcel = null;
        #endregion

        #region Constructores
        /// <summary>
        /// Negocio.
        /// </summary>
        public NSapBancos(IMConexionEntidad conexion,
                          MArchivoExcel archivoExcel)
            : base(conexion)
        {
            _archivoExcel = archivoExcel;
        }
        #endregion

        #region Funciones

        #region SapBanco (SapBancos)
        /// <summary>
        /// Esta funcion valida e inserta un registro en la base de datos.
        /// </summary>
        public new Boolean SapBancoInserta(ESapBanco sapBanco)
        {
            //Validacion
            if (!SapBancoValida(sapBanco))
                return false;

            //Persistencia
            return base.SapBancoInserta(sapBanco);
        }
        /// <summary>
        /// Valida y actualiza un registro en la base de datos.
        /// </summary>
        public new Boolean SapBancoActualiza(ESapBanco sapBanco)
        {
            //Validacion
            if (!SapBancoValida(sapBanco))
                return false;

            //Persistencia
            return base.SapBancoActualiza(sapBanco);
        }
        /// <summary>
        /// Elimina un registro de la base de datos.
        /// </summary>
        /// <returns></returns>
        public new Boolean SapBancoElimina(ESapBanco sapBanco)
        {
            //Validacion
            SapBancoReglasNeg().ValidateProperty(sapBanco, e => e.SapBancoId);
            if (!_mensajes.Ok)
                return false;

            //Persistencia
            return base.SapBancoElimina(sapBanco);
        }
        /// <summary>
        /// Exporta datos a Excel.
        /// </summary>
        public MEDatosArchivo SapBancoExporta(ESapBancoFiltro sapBancoFiltro)
        {
            return base.SapBancoExporta(sapBancoFiltro,
                                        _archivoExcel);
        }
        /// <summary>
        /// Reglas de negocio.
        /// </summary>
        public List<MEReglaNeg> SapBancoReglas()
        {
            return SapBancoReglasNeg().Rules;
        }
        /// <summary>
        /// Validacion para inserta y actualiza.
        /// </summary>
        private Boolean SapBancoValida(ESapBanco sapBanco)
        {
            _mensajes.Initialize();
            if (!SapBancoReglasNeg().Validate(sapBanco))
                return false;

            //Validaciones adicionales

            return _mensajes.Ok;
        }
        /// <summary>
        /// Crea las reglas de negocio.
        /// </summary>
        private IMReglasNeg<ESapBanco> SapBancoReglasNeg()
        {
            if (_sapBancoReglas != null)
                return _sapBancoReglas;

            _sapBancoReglas = Validaciones.CreaReglasNeg<ESapBanco>(_mensajes);
            _sapBancoReglas.AddSL(e => e.SapBancoId, 2, 50);
            _sapBancoReglas.AddSL(e => e.SapBancoNombre, 2, 120);
            _sapBancoReglas.AddSL(e => e.Activo);

            return _sapBancoReglas;
        }
        #endregion

        #endregion
    }
}
