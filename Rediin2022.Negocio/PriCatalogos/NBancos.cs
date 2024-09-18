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
        public new async Task<Int64> BancoInserta(EBanco banco)
        {
            //Validacion
            if (!BancoValida(banco))
                return 0L;

            //Persistencia
            return await base.BancoInserta(banco);
        }
        /// <summary>
        /// Valida y actualiza un registro en la base de datos.
        /// </summary>
        public new async Task<Boolean> BancoActualiza(EBanco banco)
        {
            //Validacion
            if (!BancoValida(banco))
                return false;

            //Persistencia
            return await base.BancoActualiza(banco);
        }
        /// <summary>
        /// Elimina un registro de la base de datos.
        /// </summary>
        /// <returns></returns>
        public new async Task<Boolean> BancoElimina(EBanco banco)
        {
            //Validacion
            BancoReglasNeg().ValidateProperty(banco, e => e.BancoId);
            if (!Mensajes.Ok)
                return false;

            //Persistencia
            return await base.BancoElimina(banco);
        }
        /// <summary>
        /// Exporta datos a Excel.
        /// </summary>
        public async Task<string> BancoExporta(EBancoFiltro bancoFiltro)
        {
            return await base.BancoExporta(bancoFiltro,
                                           _archivoExcel);
        }
        /// <summary>
        /// Reglas de negocio.
        /// </summary>
        public async Task<List<MEReglaNeg>> BancoReglas()
        {
            return await Task.Run(() => BancoReglasNeg().Rules);
        }
        /// <summary>
        /// Validacion para inserta y actualiza.
        /// </summary>
        private Boolean BancoValida(EBanco banco)
        {
            Mensajes.Initialize();
            if (!BancoReglasNeg().Validate(banco))
                return false;

            //Validaciones adicionales

            return Mensajes.Ok;
        }
        /// <summary>
        /// Crea las reglas de negocio.
        /// </summary>
        private IMReglasNeg<EBanco> BancoReglasNeg()
        {
            if (_bancoReglas != null)
                return _bancoReglas;

            _bancoReglas = Validaciones.CreaReglasNeg<EBanco>(Mensajes);
            _bancoReglas.AddSL(e => e.BancoId, 0L, Validaciones._int64Max, false); // Consecutivo
            _bancoReglas.AddSL(e => e.BancoNombre, 2, 120);
            _bancoReglas.AddSL(e => e.Activo);

            return _bancoReglas;
        }
        #endregion

        #endregion
    }
}
