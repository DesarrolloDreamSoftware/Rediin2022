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
    public class RSapGruposTesoreria : MRepositorio
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
        public RSapGruposTesoreria(IMConexionEntidad conexion)
            : base(conexion)
        {
            _conexion = conexion;
        }
        #endregion

        #region Funciones

        #region SapGrupoTesoreria (SapGruposTesoreria)
        /// <summary>
        /// Consulta paginada de la entidad SapGrupoTesoreria.
        /// </summary>
        public async Task<ESapGrupoTesoreriaPag> SapGrupoTesoreriaPag(ESapGrupoTesoreriaFiltro sapGrupoTesoreriaFiltro)
        {
            return await _conexion.EntidadPagAsync<ESapGrupoTesoreria,
                                                    ESapGrupoTesoreriaPag,
                                                    ESapGrupoTesoreriaFiltro>(sapGrupoTesoreriaFiltro, "NCSapGruposTesoreriaCP");

            //return base.EntidadPagAsync<ESapGrupoTesoreriaPag>(sapGrupoTesoreriaFiltro,
            //               sapGrupoTesoreriaPag =>
            //    {
            //        _conexion.AddParamFilterTL(sapGrupoTesoreriaFiltro);
            //        _conexion.LoadEntity<ESapGrupoTesoreriaPag>("NCSapGruposTesoreriaCP", sapGrupoTesoreriaPag);
            //    },
            //    sapGrupoTesoreriaPag =>
            //    {
            //        _conexion.AddParamFilterPag(sapGrupoTesoreriaFiltro);
            //        sapGrupoTesoreriaPag.Pagina = _conexion.LoadEntities<ESapGrupoTesoreria>("NCSapGruposTesoreriaCP");
            //    });
        }
        /// <summary>
        /// Consulta por id de la entidad SapGrupoTesoreria.
        /// </summary>
        public async Task<ESapGrupoTesoreria> SapGrupoTesoreriaXId(String sapGrupoTesoreriaId)
        {
            _conexion.AddParamIn(sapGrupoTesoreriaId);
            return await _conexion.LoadEntityAsync<ESapGrupoTesoreria>("NCSapGruposTesoreriaCI");
        }
        /// <summary>
        /// Consulta para combos de la entidad SapGrupoTesoreria.
        /// </summary>
        public async Task<List<MEElemento>> SapGrupoTesoreriaCmb()
        {
            return await _conexion.EntidadCmbAsync("NCSapGruposTesoreriaCCmb");
        }
        /// <summary>
        /// Permite insertar la entidad SapGrupoTesoreria.
        /// </summary>
        protected async Task<Boolean> SapGrupoTesoreriaInserta(ESapGrupoTesoreria sapGrupoTesoreria)
        {
            return await _conexion.EntityUpdateAsync(sapGrupoTesoreria, MAccionesBd.Inserta, "NCSapGruposTesoreriaIAE");
            //return sapGrupoTesoreria.SapGrupoTesoreriaId;

            //_conexion.AddParamEntity(sapGrupoTesoreria, MAccionesBd.Inserta);
            //await _conexion.ExecuteScalarValAsync("NCSapGruposTesoreriaIAE",
            //                           MensajesXId.SapGrupoTesoreriaNombre);
            //return Mensajes.Ok;
        }
        /// <summary>
        /// Permite actualizar la entidad SapGrupoTesoreria.
        /// </summary>
        protected async Task<Boolean> SapGrupoTesoreriaActualiza(ESapGrupoTesoreria sapGrupoTesoreria)
        {
            return await _conexion.EntityUpdateAsync(sapGrupoTesoreria, MAccionesBd.Actualiza, "NCSapGruposTesoreriaIAE");

            //_conexion.AddParamEntity(sapGrupoTesoreria, MAccionesBd.Actualiza);
            //await _conexion.ExecuteScalarValAsync("NCSapGruposTesoreriaIAE",
            //                           MensajesXId.SapGrupoTesoreriaNombre);
            //return Mensajes.Ok;
        }
        /// <summary>
        /// Permite eliminar la entidad SapGrupoTesoreria.
        /// </summary>
        protected async Task<Boolean> SapGrupoTesoreriaElimina(ESapGrupoTesoreria sapGrupoTesoreria)
        {
            return await _conexion.EntityUpdateAsync(sapGrupoTesoreria, MAccionesBd.Elimina, "NCSapGruposTesoreriaIAE");

            //_conexion.AddParamEntity(sapGrupoTesoreria, MAccionesBd.Elimina);
            //return await _conexion.ExecuteNonQueryRetAsync("NCSapGruposTesoreriaIAE");
        }
        /// <summary>
        /// Exporta datos a Excel.
        /// </summary>
        protected async Task<string> SapGrupoTesoreriaExporta(ESapGrupoTesoreriaFiltro sapGrupoTesoreriaFiltro,
                                                              MArchivoExcel archivoExcel)
        {
            _conexion.AddParamFilterExp(sapGrupoTesoreriaFiltro);
            return await archivoExcel.ExportAsync(_conexion.GetCurrentCmd("NCSapGruposTesoreriaCP"),
                                                  "SapGrupoTesoreria.xlsb",
                                                  sapGrupoTesoreriaFiltro.Columnas);
        }
        #endregion

        #endregion
    }
}
