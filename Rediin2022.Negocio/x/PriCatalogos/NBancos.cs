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
    public class NBancos : RBancos, INBancos
    {
        #region Variables
        /// <summary>
        /// Reglas de negocio.
        /// </summary>
        private IMReglasNeg<EBanco> _bancoReglas = null;
        /// <summary>
        /// Objeto para exportacion a excel.
        /// </summary>
        private MArchivoExcel _archivoExcel = null;
        #endregion

        #region Constructores
        /// <summary>
        /// Negocio.
        /// </summary>
        public NBancos(IMConexionEntidad conexion,
                       MArchivoExcel archivoExcel)
            : base(conexion)
        {
            _archivoExcel = archivoExcel;
        }
        #endregion

        #region Funciones

        #region Banco (Bancos)
        /// <summary>
        /// Esta funcion valida e inserta un registro en la base de datos.
        /// </summary>
        public new Int64 BancoInserta(EBanco banco)
        {
            //Validacion
            if (!BancoValida(banco))
                return 0L;

            //Persistencia
            return base.BancoInserta(banco);
        }
        /// <summary>
        /// Valida y actualiza un registro en la base de datos.
        /// </summary>
        public new Boolean BancoActualiza(EBanco banco)
        {
            //Validacion
            if (!BancoValida(banco))
                return false;

            //Persistencia
            return base.BancoActualiza(banco);
        }
        /// <summary>
        /// Elimina un registro de la base de datos.
        /// </summary>
        /// <returns></returns>
        public new Boolean BancoElimina(EBanco banco)
        {
            //Validacion
            BancoReglasNeg().ValidateProperty(banco, e => e.BancoId);
            if (!_mensajes.Ok)
                return false;

            //Persistencia
            return base.BancoElimina(banco);
        }
        /// <summary>
        /// Exporta datos a Excel.
        /// </summary>
        public MEDatosArchivo BancoExporta(EBancoFiltro bancoFiltro)
        {
            return base.BancoExporta(bancoFiltro,
                                     _archivoExcel);
        }
        /// <summary>
        /// Reglas de negocio.
        /// </summary>
        public List<MEReglaNeg> BancoReglas()
        {
            return BancoReglasNeg().Rules;
        }
        /// <summary>
        /// Validacion para inserta y actualiza.
        /// </summary>
        private Boolean BancoValida(EBanco banco)
        {
            _mensajes.Initialize();
            if (!BancoReglasNeg().Validate(banco))
                return false;

            //Validaciones adicionales

            return _mensajes.Ok;
        }
        /// <summary>
        /// Crea las reglas de negocio.
        /// </summary>
        private IMReglasNeg<EBanco> BancoReglasNeg()
        {
            if (_bancoReglas != null)
                return _bancoReglas;

            _bancoReglas = Validaciones.CreaReglasNeg<EBanco>(_mensajes);
            _bancoReglas.AddSL(e => e.BancoId, 0L, Validaciones._int64Max, false); // Consecutivo
            _bancoReglas.AddSL(e => e.BancoNombre, 2, 120);
            _bancoReglas.AddSL(e => e.Activo);

            return _bancoReglas;
        }
        #endregion

        #endregion
    }
}
