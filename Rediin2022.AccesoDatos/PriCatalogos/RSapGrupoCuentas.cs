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
    public class RSapGrupoCuentas : MRepositorio
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
        public RSapGrupoCuentas(IMConexionEntidad conexion)
            : base(conexion)
        {
            _conexion = conexion;
        }
        #endregion

        #region Funciones

        #region SapGrupoCuenta (SapGrupoCuentas)
        /// <summary>
        /// Consulta paginada de la entidad SapGrupoCuenta.
        /// </summary>
        public async Task<ESapGrupoCuentaPag> SapGrupoCuentaPag(ESapGrupoCuentaFiltro sapGrupoCuentaFiltro)
        {
            return await _conexion.EntidadPagAsync<ESapGrupoCuenta,
                                                    ESapGrupoCuentaPag,
                                                    ESapGrupoCuentaFiltro>(sapGrupoCuentaFiltro, "NCSapGrupoCuentasCP");

            //return base.EntidadPagAsync<ESapGrupoCuentaPag>(sapGrupoCuentaFiltro,
            //               sapGrupoCuentaPag =>
            //    {
            //        _conexion.AddParamFilterTL(sapGrupoCuentaFiltro);
            //        _conexion.LoadEntity<ESapGrupoCuentaPag>("NCSapGrupoCuentasCP", sapGrupoCuentaPag);
            //    },
            //    sapGrupoCuentaPag =>
            //    {
            //        _conexion.AddParamFilterPag(sapGrupoCuentaFiltro);
            //        sapGrupoCuentaPag.Pagina = _conexion.LoadEntities<ESapGrupoCuenta>("NCSapGrupoCuentasCP");
            //    });
        }
        /// <summary>
        /// Consulta por id de la entidad SapGrupoCuenta.
        /// </summary>
        public async Task<ESapGrupoCuenta> SapGrupoCuentaXId(String sapGrupoCuentaId)
        {
            _conexion.AddParamIn(sapGrupoCuentaId);
            return await _conexion.LoadEntityAsync<ESapGrupoCuenta>("NCSapGrupoCuentasCI");
        }
        /// <summary>
        /// Consulta para combos de la entidad SapGrupoCuenta.
        /// </summary>
        public async Task<List<MEElemento>> SapGrupoCuentaCmb()
        {
            return await _conexion.EntidadCmbAsync("NCSapGrupoCuentasCCmb");
        }
        /// <summary>
        /// Permite insertar la entidad SapGrupoCuenta.
        /// </summary>
        protected async Task<Boolean> SapGrupoCuentaInserta(ESapGrupoCuenta sapGrupoCuenta)
        {
            return await _conexion.EntityUpdateAsync(sapGrupoCuenta, MAccionesBd.Inserta, "NCSapGrupoCuentasIAE");
            //return sapGrupoCuenta.SapGrupoCuentaId;

            //_conexion.AddParamEntity(sapGrupoCuenta, MAccionesBd.Inserta);
            //await _conexion.ExecuteScalarValAsync("NCSapGrupoCuentasIAE",
            //                           MensajesXId.SapGrupoCuentaNombre);
            //return Mensajes.Ok;
        }
        /// <summary>
        /// Permite actualizar la entidad SapGrupoCuenta.
        /// </summary>
        protected async Task<Boolean> SapGrupoCuentaActualiza(ESapGrupoCuenta sapGrupoCuenta)
        {
            return await _conexion.EntityUpdateAsync(sapGrupoCuenta, MAccionesBd.Actualiza, "NCSapGrupoCuentasIAE");

            //_conexion.AddParamEntity(sapGrupoCuenta, MAccionesBd.Actualiza);
            //await _conexion.ExecuteScalarValAsync("NCSapGrupoCuentasIAE",
            //                           MensajesXId.SapGrupoCuentaNombre);
            //return Mensajes.Ok;
        }
        /// <summary>
        /// Permite eliminar la entidad SapGrupoCuenta.
        /// </summary>
        protected async Task<Boolean> SapGrupoCuentaElimina(ESapGrupoCuenta sapGrupoCuenta)
        {
            return await _conexion.EntityUpdateAsync(sapGrupoCuenta, MAccionesBd.Elimina, "NCSapGrupoCuentasIAE");

            //_conexion.AddParamEntity(sapGrupoCuenta, MAccionesBd.Elimina);
            //return await _conexion.ExecuteNonQueryRetAsync("NCSapGrupoCuentasIAE");
        }
        /// <summary>
        /// Exporta datos a Excel.
        /// </summary>
        protected async Task<string> SapGrupoCuentaExporta(ESapGrupoCuentaFiltro sapGrupoCuentaFiltro,
                                                           MArchivoExcel archivoExcel)
        {
            _conexion.AddParamFilterExp(sapGrupoCuentaFiltro);
            return await archivoExcel.ExportAsync(_conexion.GetCurrentCmd("NCSapGrupoCuentasCP"),
                                                  "SapGrupoCuenta.xlsb",
                                                  sapGrupoCuentaFiltro.Columnas);
        }
        #endregion

        #endregion
    }
}
