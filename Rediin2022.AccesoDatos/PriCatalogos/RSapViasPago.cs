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
    public class RSapViasPago : MRepositorio
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
        public RSapViasPago(IMConexionEntidad conexion)
            : base(conexion)
        {
            _conexion = conexion;
        }
        #endregion

        #region Funciones

        #region SapViaPago (SapViasPago)
        /// <summary>
        /// Consulta paginada de la entidad SapViaPago.
        /// </summary>
        public async Task<ESapViaPagoPag> SapViaPagoPag(ESapViaPagoFiltro sapViaPagoFiltro)
        {
            return await _conexion.EntidadPagAsync<ESapViaPago,
                                                    ESapViaPagoPag,
                                                    ESapViaPagoFiltro>(sapViaPagoFiltro, "NCSapViasPagoCP");

            //return base.EntidadPagAsync<ESapViaPagoPag>(sapViaPagoFiltro,
            //               sapViaPagoPag =>
            //    {
            //        _conexion.AddParamFilterTL(sapViaPagoFiltro);
            //        _conexion.LoadEntity<ESapViaPagoPag>("NCSapViasPagoCP", sapViaPagoPag);
            //    },
            //    sapViaPagoPag =>
            //    {
            //        _conexion.AddParamFilterPag(sapViaPagoFiltro);
            //        sapViaPagoPag.Pagina = _conexion.LoadEntities<ESapViaPago>("NCSapViasPagoCP");
            //    });
        }
        /// <summary>
        /// Consulta por id de la entidad SapViaPago.
        /// </summary>
        public async Task<ESapViaPago> SapViaPagoXId(String sapViaPagoId)
        {
            _conexion.AddParamIn(sapViaPagoId);
            return await _conexion.LoadEntityAsync<ESapViaPago>("NCSapViasPagoCI");
        }
        /// <summary>
        /// Consulta para combos de la entidad SapViaPago.
        /// </summary>
        public async Task<List<MEElemento>> SapViaPagoCmb()
        {
            return await _conexion.EntidadCmbAsync("NCSapViasPagoCCmb");
        }
        /// <summary>
        /// Permite insertar la entidad SapViaPago.
        /// </summary>
        protected async Task<Boolean> SapViaPagoInserta(ESapViaPago sapViaPago)
        {
            return await _conexion.EntityUpdateAsync(sapViaPago, MAccionesBd.Inserta, "NCSapViasPagoIAE");
            //return sapViaPago.SapViaPagoId;

            //           _conexion.AddParamEntity(sapViaPago, MAccionesBd.Inserta);
            //await _conexion.ExecuteScalarValAsync("NCSapViasPagoIAE",
            //                           MensajesXId.SapViaPagoNombre);
            //return Mensajes.Ok;
        }
        /// <summary>
        /// Permite actualizar la entidad SapViaPago.
        /// </summary>
        protected async Task<Boolean> SapViaPagoActualiza(ESapViaPago sapViaPago)
        {
            return await _conexion.EntityUpdateAsync(sapViaPago, MAccionesBd.Actualiza, "NCSapViasPagoIAE");

            //           _conexion.AddParamEntity(sapViaPago, MAccionesBd.Actualiza);
            //await _conexion.ExecuteScalarValAsync("NCSapViasPagoIAE",
            //                           MensajesXId.SapViaPagoNombre);
            //return Mensajes.Ok;
        }
        /// <summary>
        /// Permite eliminar la entidad SapViaPago.
        /// </summary>
        protected async Task<Boolean> SapViaPagoElimina(ESapViaPago sapViaPago)
        {
            return await _conexion.EntityUpdateAsync(sapViaPago, MAccionesBd.Elimina, "NCSapViasPagoIAE");

            //           _conexion.AddParamEntity(sapViaPago, MAccionesBd.Elimina);
            //return await _conexion.ExecuteNonQueryRetAsync("NCSapViasPagoIAE");
        }
        /// <summary>
        /// Exporta datos a Excel.
        /// </summary>
        protected async Task<string> SapViaPagoExporta(ESapViaPagoFiltro sapViaPagoFiltro,
                                                       MArchivoExcel archivoExcel)
        {
            _conexion.AddParamFilterExp(sapViaPagoFiltro);

            return await archivoExcel.ExportAsync(_conexion.GetCurrentCmd("NCSapViasPagoCP"),
                                                  "SapViaPago.xlsb",
                                                  sapViaPagoFiltro.Columnas);
        }
        #endregion

        #endregion
    }
}
