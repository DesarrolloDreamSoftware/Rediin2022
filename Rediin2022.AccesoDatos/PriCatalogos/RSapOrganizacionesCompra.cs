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
    public class RSapOrganizacionesCompra : MRepositorio
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
        public RSapOrganizacionesCompra(IMConexionEntidad conexion)
            : base(conexion)
        {
            _conexion = conexion;
        }
        #endregion

        #region Funciones

        #region SapOrganizacionCompra (SapOrganizacionesCompra)
        /// <summary>
        /// Consulta paginada de la entidad SapOrganizacionCompra.
        /// </summary>
        public async Task<ESapOrganizacionCompraPag> SapOrganizacionCompraPag(ESapOrganizacionCompraFiltro sapOrganizacionCompraFiltro)
        {
            return await _conexion.EntidadPagAsync<ESapOrganizacionCompra,
                                                    ESapOrganizacionCompraPag,
                                                    ESapOrganizacionCompraFiltro>(sapOrganizacionCompraFiltro, "NCSapOrganizacionesCompraCP");

            //return base.EntidadPagAsync<ESapOrganizacionCompraPag>(sapOrganizacionCompraFiltro,
            //               sapOrganizacionCompraPag =>
            //    {
            //        _conexion.AddParamFilterTL(sapOrganizacionCompraFiltro);
            //        _conexion.LoadEntity<ESapOrganizacionCompraPag>("NCSapOrganizacionesCompraCP", sapOrganizacionCompraPag);
            //    },
            //    sapOrganizacionCompraPag =>
            //    {
            //        _conexion.AddParamFilterPag(sapOrganizacionCompraFiltro);
            //        sapOrganizacionCompraPag.Pagina = _conexion.LoadEntities<ESapOrganizacionCompra>("NCSapOrganizacionesCompraCP");
            //    });
        }
        /// <summary>
        /// Consulta por id de la entidad SapOrganizacionCompra.
        /// </summary>
        public async Task<ESapOrganizacionCompra> SapOrganizacionCompraXId(String sapOrganizacionCompraId)
        {
            _conexion.AddParamIn(sapOrganizacionCompraId);
            return await _conexion.LoadEntityAsync<ESapOrganizacionCompra>("NCSapOrganizacionesCompraCI");
        }
        /// <summary>
        /// Consulta para combos de la entidad SapOrganizacionCompra.
        /// </summary>
        public async Task<List<MEElemento>> SapOrganizacionCompraCmb()
        {
            return await _conexion.EntidadCmbAsync("NCSapOrganizacionesCompraCCmb");
        }
        /// <summary>
        /// Permite insertar la entidad SapOrganizacionCompra.
        /// </summary>
        protected async Task<Boolean> SapOrganizacionCompraInserta(ESapOrganizacionCompra sapOrganizacionCompra)
        {
            return await _conexion.EntityUpdateAsync(sapOrganizacionCompra, MAccionesBd.Inserta, "NCSapOrganizacionesCompraIAE");
            //return sapOrganizacionCompra.SapOrganizacionCompraId;

            //           _conexion.AddParamEntity(sapOrganizacionCompra, MAccionesBd.Inserta);
            //await _conexion.ExecuteScalarValAsync("NCSapOrganizacionesCompraIAE",
            //                           MensajesXId.SapOrganizacionCompraNombre);
            //return Mensajes.Ok;
        }
        /// <summary>
        /// Permite actualizar la entidad SapOrganizacionCompra.
        /// </summary>
        protected async Task<Boolean> SapOrganizacionCompraActualiza(ESapOrganizacionCompra sapOrganizacionCompra)
        {
            return await _conexion.EntityUpdateAsync(sapOrganizacionCompra, MAccionesBd.Actualiza, "NCSapOrganizacionesCompraIAE");

            //_conexion.AddParamEntity(sapOrganizacionCompra, MAccionesBd.Actualiza);
            //await _conexion.ExecuteScalarValAsync("NCSapOrganizacionesCompraIAE",
            //                           MensajesXId.SapOrganizacionCompraNombre);
            //return Mensajes.Ok;
        }
        /// <summary>
        /// Permite eliminar la entidad SapOrganizacionCompra.
        /// </summary>
        protected async Task<Boolean> SapOrganizacionCompraElimina(ESapOrganizacionCompra sapOrganizacionCompra)
        {
            return await _conexion.EntityUpdateAsync(sapOrganizacionCompra, MAccionesBd.Elimina, "NCSapOrganizacionesCompraIAE");

            //_conexion.AddParamEntity(sapOrganizacionCompra, MAccionesBd.Elimina);
            //return await _conexion.ExecuteNonQueryRetAsync("NCSapOrganizacionesCompraIAE");
        }
        /// <summary>
        /// Exporta datos a Excel.
        /// </summary>
        protected async Task<string> SapOrganizacionCompraExporta(ESapOrganizacionCompraFiltro sapOrganizacionCompraFiltro,
                                                                  MArchivoExcel archivoExcel)
        {
            _conexion.AddParamFilterExp(sapOrganizacionCompraFiltro);
            return await archivoExcel.ExportAsync(_conexion.GetCurrentCmd("NCSapOrganizacionesCompraCP"),
                                                  "SapOrganizacionCompra.xlsb",
                                                  sapOrganizacionCompraFiltro.Columnas);
        }
        #endregion

        #endregion
    }
}
