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
    public class RSapCondicionesPago : MRepositorio
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
        public RSapCondicionesPago(IMConexionEntidad conexion)
            : base(conexion)
        {
            _conexion = conexion;
        }
        #endregion

        #region Funciones

        #region SapCondicionPago (SapCondicionesPago)
        /// <summary>
        /// Consulta paginada de la entidad SapCondicionPago.
        /// </summary>
        public async Task<ESapCondicionPagoPag> SapCondicionPagoPag(ESapCondicionPagoFiltro sapCondicionPagoFiltro)
        {
            return await _conexion.EntidadPagAsync<ESapCondicionPago,
                                                    ESapCondicionPagoPag,
                                                    ESapCondicionPagoFiltro>(sapCondicionPagoFiltro, "NCSapCondicionesPagoCP");

            //return base.EntidadPagAsync<ESapCondicionPagoPag>(sapCondicionPagoFiltro,
            //               sapCondicionPagoPag =>
            //    {
            //        _conexion.AddParamFilterTL(sapCondicionPagoFiltro);
            //        _conexion.LoadEntity<ESapCondicionPagoPag>("NCSapCondicionesPagoCP", sapCondicionPagoPag);
            //    },
            //    sapCondicionPagoPag =>
            //    {
            //        _conexion.AddParamFilterPag(sapCondicionPagoFiltro);
            //        sapCondicionPagoPag.Pagina = _conexion.LoadEntities<ESapCondicionPago>("NCSapCondicionesPagoCP");
            //    });
        }
        /// <summary>
        /// Consulta por id de la entidad SapCondicionPago.
        /// </summary>
        public async Task<ESapCondicionPago> SapCondicionPagoXId(String sapCondicionPagoId)
        {
            _conexion.AddParamIn(sapCondicionPagoId);
            return await _conexion.LoadEntityAsync<ESapCondicionPago>("NCSapCondicionesPagoCI");
        }
        /// <summary>
        /// Consulta para combos de la entidad SapCondicionPago.
        /// </summary>
        public async Task<List<MEElemento>> SapCondicionPagoCmb()
        {
            return await _conexion.EntidadCmbAsync("NCSapCondicionesPagoCCmb");
        }
        /// <summary>
        /// Permite insertar la entidad SapCondicionPago.
        /// </summary>
        protected async Task<Boolean> SapCondicionPagoInserta(ESapCondicionPago sapCondicionPago)
        {
            return await _conexion.EntityUpdateAsync(sapCondicionPago, MAccionesBd.Inserta, "NCSapCondicionesPagoIAE");
            //return sapCondicionPago.SapCondicionPagoId;

            //_conexion.AddParamEntity(sapCondicionPago, MAccionesBd.Inserta);
            //await _conexion.ExecuteScalarValAsync("NCSapCondicionesPagoIAE",
            //                           MensajesXId.SapCondicionPagoNombre);
            //return Mensajes.Ok;
        }
        /// <summary>
        /// Permite actualizar la entidad SapCondicionPago.
        /// </summary>
        protected async Task<Boolean> SapCondicionPagoActualiza(ESapCondicionPago sapCondicionPago)
        {
            return await _conexion.EntityUpdateAsync(sapCondicionPago, MAccionesBd.Actualiza, "NCSapCondicionesPagoIAE");

            //_conexion.AddParamEntity(sapCondicionPago, MAccionesBd.Actualiza);
            //await _conexion.ExecuteScalarValAsync("NCSapCondicionesPagoIAE",
            //                           MensajesXId.SapCondicionPagoNombre);
            //return Mensajes.Ok;
        }
        /// <summary>
        /// Permite eliminar la entidad SapCondicionPago.
        /// </summary>
        protected async Task<Boolean> SapCondicionPagoElimina(ESapCondicionPago sapCondicionPago)
        {
            return await _conexion.EntityUpdateAsync(sapCondicionPago, MAccionesBd.Elimina, "NCSapCondicionesPagoIAE");

            //_conexion.AddParamEntity(sapCondicionPago, MAccionesBd.Elimina);
            //return await _conexion.ExecuteNonQueryRetAsync("NCSapCondicionesPagoIAE");
        }
        /// <summary>
        /// Exporta datos a Excel.
        /// </summary>
        protected async Task<string> SapCondicionPagoExporta(ESapCondicionPagoFiltro sapCondicionPagoFiltro,
                                                             MArchivoExcel archivoExcel)
        {
            _conexion.AddParamFilterExp(sapCondicionPagoFiltro);
            return await archivoExcel.ExportAsync(_conexion.GetCurrentCmd("NCSapCondicionesPagoCP"),
                                                  "SapCondicionPago.xlsb",
                                                  sapCondicionPagoFiltro.Columnas);
        }
        #endregion

        #endregion
    }
}
