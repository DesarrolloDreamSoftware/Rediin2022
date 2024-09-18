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
    public class RBancos : MRepositorio
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
        public RBancos(IMConexionEntidad conexion)
            : base(conexion)
        {
            _conexion = conexion;
        }
        #endregion

        #region Funciones

        #region Banco (Bancos)
        /// <summary>
        /// Consulta paginada de la entidad Banco.
        /// </summary>
        public async Task<EBancoPag> BancoPag(EBancoFiltro bancoFiltro)
        {
            return await _conexion.EntidadPagAsync<EBanco,
                                                    EBancoPag,
                                                    EBancoFiltro>(bancoFiltro, "NCBancosCP");

            //return base.EntidadPagAsync<EBancoPag>(bancoFiltro,
            //               bancoPag =>
            //    {
            //        _conexion.AddParamFilterTL(bancoFiltro);
            //        _conexion.LoadEntity<EBancoPag>("NCBancosCP", bancoPag);
            //    },
            //    bancoPag =>
            //    {
            //        _conexion.AddParamFilterPag(bancoFiltro);
            //        bancoPag.Pagina = _conexion.LoadEntities<EBanco>("NCBancosCP");
            //    });
        }
        /// <summary>
        /// Consulta por id de la entidad Banco.
        /// </summary>
        public async Task<EBanco> BancoXId(Int64 bancoId)
        {
            _conexion.AddParamIn(bancoId);
            return await _conexion.LoadEntityAsync<EBanco>("NCBancosCI");
        }
        /// <summary>
        /// Consulta para combos de la entidad Banco.
        /// </summary>
        public async Task<List<MEElemento>> BancoCmb()
        {
            return await _conexion.EntidadCmbAsync("NCBancosCCmb");
        }
        /// <summary>
        /// Permite insertar la entidad Banco.
        /// </summary>
        protected async Task<Int64> BancoInserta(EBanco banco)
        {
            await _conexion.EntityUpdateAsync(banco, MAccionesBd.Inserta, "NCBancosIAE");
            return banco.BancoId;

            //           _conexion.AddParamEntity(banco, MAccionesBd.Inserta);
            //Int64 vResultado = await _conexion.ExecuteScalarValAsync("NCBancosIAE",
            //                                              MensajesXId.BancoNombre);
            //return vResultado;
        }
        /// <summary>
        /// Permite actualizar la entidad Banco.
        /// </summary>
        protected async Task<Boolean> BancoActualiza(EBanco banco)
        {
            return await _conexion.EntityUpdateAsync(banco, MAccionesBd.Actualiza, "NCBancosIAE");

            //           _conexion.AddParamEntity(banco, MAccionesBd.Actualiza);
            //await _conexion.ExecuteScalarValAsync("NCBancosIAE",
            //                           MensajesXId.BancoNombre);
            //return Mensajes.Ok;
        }
        /// <summary>
        /// Permite eliminar la entidad Banco.
        /// </summary>
        protected async Task<Boolean> BancoElimina(EBanco banco)
        {
            return await _conexion.EntityUpdateAsync(banco, MAccionesBd.Elimina, "NCBancosIAE");

            //           _conexion.AddParamEntity(banco, MAccionesBd.Elimina);
            //return await _conexion.ExecuteNonQueryRetAsync("NCBancosIAE");
        }
        /// <summary>
        /// Exporta datos a Excel.
        /// </summary>
        protected async Task<string> BancoExporta(EBancoFiltro bancoFiltro,
                                                  MArchivoExcel archivoExcel)
        {
            _conexion.AddParamFilterExp(bancoFiltro);
            return await archivoExcel.ExportAsync(_conexion.GetCurrentCmd("NCBancosCP"),
                                                  "Banco.xlsb",
                                                  bancoFiltro.Columnas);

            //bancoFiltro.DatPag.StartLine = 1;
            //bancoFiltro.DatPag.PageSize = Int32.MaxValue;
            //_conexion.AddParamFilterPag(bancoFiltro);

            //String vArchivo = await archivoExcel.ExportAsync(await _conexion.GetCurrentCmd("NCBancosCP"),
            //                                      "Banco.xlsb",
            //                                      bancoFiltro.Columnas);
            //return new MEDatosArchivo()
            //{
            //    PathOrg = vArchivo,
            //    PathDes = vArchivo
            //};
        }
        #endregion

        #endregion
    }
}
