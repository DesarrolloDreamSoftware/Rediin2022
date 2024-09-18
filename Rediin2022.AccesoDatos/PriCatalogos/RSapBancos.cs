using DSEntityNetX.Common.Casting;
using DSEntityNetX.DataAccess;
using DSMetodNetX.AccesoDatos;
using DSMetodNetX.Comun;
using DSMetodNetX.Entidades;

using Rediin2022.Entidades.Idioma;
using Rediin2022.Entidades.PriCatalogos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rediin2022.AccesoDatos.PriCatalogos
{
    /// <summary>
    /// Repositorio.
    /// </summary>
    [Serializable]
    public class RSapBancos : MRepositorio
    {
        #region Variables
        /// <summary>
        /// Conexión.
        /// </summary>
        private IMConexionEntidad _conexion;
        #endregion

        #region Constructores
        /// <summary>
        /// Repositorio.
        /// </summary>
        public RSapBancos(IMConexionEntidad conexion)
            : base(conexion)
        {
            _conexion = conexion;
        }
        #endregion

        #region Funciones

        #region SapBanco (SapBancos)
        /// <summary>
        /// Consulta paginada de la entidad SapBanco.
        /// </summary>
        public async Task<ESapBancoPag> SapBancoPag(ESapBancoFiltro sapBancoFiltro)
        {
            return await _conexion.EntidadPagAsync<ESapBanco,
                                                    ESapBancoPag,
                                                    ESapBancoFiltro>(sapBancoFiltro, "NCSapBancosCP");

            //return base.EntidadPagAsync<ESapBancoPag>(sapBancoFiltro,
            //               sapBancoPag =>
            //    {
            //        _conexion.AddParamFilterTL(sapBancoFiltro);
            //        _conexion.LoadEntity<ESapBancoPag>("NCSapBancosCP", sapBancoPag);
            //    },
            //    sapBancoPag =>
            //    {
            //        _conexion.AddParamFilterPag(sapBancoFiltro);
            //        sapBancoPag.Pagina = _conexion.LoadEntities<ESapBanco>("NCSapBancosCP");
            //    });
        }
        /// <summary>
        /// Consulta por id de la entidad SapBanco.
        /// </summary>
        public async Task<ESapBanco> SapBancoXId(String sapBancoId)
        {
            _conexion.AddParamIn(sapBancoId);
            return await _conexion.LoadEntityAsync<ESapBanco>("NCSapBancosCI");
        }
        /// <summary>
        /// Consulta para combos de la entidad SapBanco.
        /// </summary>
        public async Task<List<MEElemento>> SapBancoCmb()
        {
            return await _conexion.EntidadCmbAsync("NCSapBancosCCmb");
        }
        /// <summary>
        /// Permite insertar la entidad SapBanco.
        /// </summary>
        protected async Task<Boolean> SapBancoInserta(ESapBanco sapBanco)
        {
            return await _conexion.EntityUpdateAsync(sapBanco, MAccionesBd.Inserta, "NCSapBancosIAE");
            //return sapBanco.SapBancoId;

            //_conexion.AddParamEntity(sapBanco, MAccionesBd.Inserta);
            //await _conexion.ExecuteScalarValAsync("NCSapBancosIAE",
            //                           MensajesXId.SapBancoNombre);
            //return Mensajes.Ok;
        }
        /// <summary>
        /// Permite actualizar la entidad SapBanco.
        /// </summary>
        protected async Task<Boolean> SapBancoActualiza(ESapBanco sapBanco)
        {
            return await _conexion.EntityUpdateAsync(sapBanco, MAccionesBd.Actualiza, "NCSapBancosIAE");

            //_conexion.AddParamEntity(sapBanco, MAccionesBd.Actualiza);
            //await _conexion.ExecuteScalarValAsync("NCSapBancosIAE",
            //                           MensajesXId.SapBancoNombre);
            //return Mensajes.Ok;
        }
        /// <summary>
        /// Permite eliminar la entidad SapBanco.
        /// </summary>
        protected async Task<Boolean> SapBancoElimina(ESapBanco sapBanco)
        {
            return await _conexion.EntityUpdateAsync(sapBanco, MAccionesBd.Elimina, "NCSapBancosIAE");

            //_conexion.AddParamEntity(sapBanco, MAccionesBd.Elimina);
            //return await _conexion.ExecuteNonQueryRetAsync("NCSapBancosIAE");
        }
        /// <summary>
        /// Exporta datos a Excel.
        /// </summary>
        protected async Task<string> SapBancoExporta(ESapBancoFiltro sapBancoFiltro,
                                                     MArchivoExcel archivoExcel)
        {
            _conexion.AddParamFilterExp(sapBancoFiltro);
            return await archivoExcel.ExportAsync(_conexion.GetCurrentCmd("NCSapBancosCP"),
                                                  "SapBanco.xlsb",
                                                  sapBancoFiltro.Columnas);
        }
        #endregion

        #endregion
    }
}
