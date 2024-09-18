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
    public class RSapTratamientos : MRepositorio
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
        public RSapTratamientos(IMConexionEntidad conexion)
            : base(conexion)
        {
            _conexion = conexion;
        }
        #endregion

        #region Funciones

        #region SapTratamiento (SapTratamientos)
        /// <summary>
        /// Consulta paginada de la entidad SapTratamiento.
        /// </summary>
        public async Task<ESapTratamientoPag> SapTratamientoPag(ESapTratamientoFiltro sapTratamientoFiltro)
        {
            return await _conexion.EntidadPagAsync<ESapTratamiento,
                                                    ESapTratamientoPag,
                                                    ESapTratamientoFiltro>(sapTratamientoFiltro, "NCSapTratamientosCP");

            //return base.EntidadPagAsync<ESapTratamientoPag>(sapTratamientoFiltro,
            //               sapTratamientoPag =>
            //    {
            //        _conexion.AddParamFilterTL(sapTratamientoFiltro);
            //        _conexion.LoadEntity<ESapTratamientoPag>("NCSapTratamientosCP", sapTratamientoPag);
            //    },
            //    sapTratamientoPag =>
            //    {
            //        _conexion.AddParamFilterPag(sapTratamientoFiltro);
            //        sapTratamientoPag.Pagina = _conexion.LoadEntities<ESapTratamiento>("NCSapTratamientosCP");
            //    });
        }
        /// <summary>
        /// Consulta por id de la entidad SapTratamiento.
        /// </summary>
        public async Task<ESapTratamiento> SapTratamientoXId(String sapTratamientoId)
        {
            _conexion.AddParamIn(sapTratamientoId);
            return await _conexion.LoadEntityAsync<ESapTratamiento>("NCSapTratamientosCI");
        }
        /// <summary>
        /// Consulta para combos de la entidad SapTratamiento.
        /// </summary>
        public async Task<List<MEElemento>> SapTratamientoCmb()
        {
            return await _conexion.EntidadCmbAsync("NCSapTratamientosCCmb");
        }
        /// <summary>
        /// Permite insertar la entidad SapTratamiento.
        /// </summary>
        protected async Task<Boolean> SapTratamientoInserta(ESapTratamiento sapTratamiento)
        {
            return await _conexion.EntityUpdateAsync(sapTratamiento, MAccionesBd.Inserta, "NCSapTratamientosIAE");
            //return sapTratamiento.SapTratamientoId;

            //_conexion.AddParamEntity(sapTratamiento, MAccionesBd.Inserta);
            //await _conexion.ExecuteScalarValAsync("NCSapTratamientosIAE",
            //                           MensajesXId.SapTratamientoNombre);
            //return Mensajes.Ok;
        }
        /// <summary>
        /// Permite actualizar la entidad SapTratamiento.
        /// </summary>
        protected async Task<Boolean> SapTratamientoActualiza(ESapTratamiento sapTratamiento)
        {
            return await _conexion.EntityUpdateAsync(sapTratamiento, MAccionesBd.Actualiza, "NCSapTratamientosIAE");

            //_conexion.AddParamEntity(sapTratamiento, MAccionesBd.Actualiza);
            //await _conexion.ExecuteScalarValAsync("NCSapTratamientosIAE",
            //                           MensajesXId.SapTratamientoNombre);
            //return Mensajes.Ok;
        }
        /// <summary>
        /// Permite eliminar la entidad SapTratamiento.
        /// </summary>
        protected async Task<Boolean> SapTratamientoElimina(ESapTratamiento sapTratamiento)
        {
            return await _conexion.EntityUpdateAsync(sapTratamiento, MAccionesBd.Elimina, "NCSapTratamientosIAE");

            //_conexion.AddParamEntity(sapTratamiento, MAccionesBd.Elimina);
            //return await _conexion.ExecuteNonQueryRetAsync("NCSapTratamientosIAE");
        }
        /// <summary>
        /// Exporta datos a Excel.
        /// </summary>
        protected async Task<string> SapTratamientoExporta(ESapTratamientoFiltro sapTratamientoFiltro,
                                                           MArchivoExcel archivoExcel)
        {
            _conexion.AddParamFilterExp(sapTratamientoFiltro);
            return await archivoExcel.ExportAsync(_conexion.GetCurrentCmd("NCSapTratamientosCP"),
                                                  "SapTratamiento.xlsb",
                                                  sapTratamientoFiltro.Columnas);
        }
        #endregion

        #endregion
    }
}
