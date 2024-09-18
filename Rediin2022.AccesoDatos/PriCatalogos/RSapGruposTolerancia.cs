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
    public class RSapGruposTolerancia : MRepositorio
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
        public RSapGruposTolerancia(IMConexionEntidad conexion)
            : base(conexion)
        {
            _conexion = conexion;
        }
        #endregion

        #region Funciones

        #region SapGrupoTolerancia (SapGruposTolerancia)
        /// <summary>
        /// Consulta paginada de la entidad SapGrupoTolerancia.
        /// </summary>
        public async Task<ESapGrupoToleranciaPag> SapGrupoToleranciaPag(ESapGrupoToleranciaFiltro sapGrupoToleranciaFiltro)
        {
            return await _conexion.EntidadPagAsync<ESapGrupoTolerancia,
                                                    ESapGrupoToleranciaPag,
                                                    ESapGrupoToleranciaFiltro>(sapGrupoToleranciaFiltro, "NCSapGruposToleranciaCP");

            //return base.EntidadPagAsync<ESapGrupoToleranciaPag>(sapGrupoToleranciaFiltro,
            //               sapGrupoToleranciaPag =>
            //    {
            //        _conexion.AddParamFilterTL(sapGrupoToleranciaFiltro);
            //        _conexion.LoadEntity<ESapGrupoToleranciaPag>("NCSapGruposToleranciaCP", sapGrupoToleranciaPag);
            //    },
            //    sapGrupoToleranciaPag =>
            //    {
            //        _conexion.AddParamFilterPag(sapGrupoToleranciaFiltro);
            //        sapGrupoToleranciaPag.Pagina = _conexion.LoadEntities<ESapGrupoTolerancia>("NCSapGruposToleranciaCP");
            //    });
        }
        /// <summary>
        /// Consulta por id de la entidad SapGrupoTolerancia.
        /// </summary>
        public async Task<ESapGrupoTolerancia> SapGrupoToleranciaXId(String sapGrupoToleranciaId)
        {
            _conexion.AddParamIn(sapGrupoToleranciaId);
            return await _conexion.LoadEntityAsync<ESapGrupoTolerancia>("NCSapGruposToleranciaCI");
        }
        /// <summary>
        /// Consulta para combos de la entidad SapGrupoTolerancia.
        /// </summary>
        public async Task<List<MEElemento>> SapGrupoToleranciaCmb()
        {
            return await _conexion.EntidadCmbAsync("NCSapGruposToleranciaCCmb");
        }
        /// <summary>
        /// Permite insertar la entidad SapGrupoTolerancia.
        /// </summary>
        protected async Task<Boolean> SapGrupoToleranciaInserta(ESapGrupoTolerancia sapGrupoTolerancia)
        {
            return await _conexion.EntityUpdateAsync(sapGrupoTolerancia, MAccionesBd.Inserta, "NCSapGruposToleranciaIAE");
            //return sapGrupoTolerancia.SapGrupoToleranciaId;

            //_conexion.AddParamEntity(sapGrupoTolerancia, MAccionesBd.Inserta);
            //await _conexion.ExecuteScalarValAsync("NCSapGruposToleranciaIAE",
            //                           MensajesXId.SapGrupoToleranciaNombre);
            //return Mensajes.Ok;
        }
        /// <summary>
        /// Permite actualizar la entidad SapGrupoTolerancia.
        /// </summary>
        protected async Task<Boolean> SapGrupoToleranciaActualiza(ESapGrupoTolerancia sapGrupoTolerancia)
        {
            return await _conexion.EntityUpdateAsync(sapGrupoTolerancia, MAccionesBd.Actualiza, "NCSapGruposToleranciaIAE");

            //_conexion.AddParamEntity(sapGrupoTolerancia, MAccionesBd.Actualiza);
            //await _conexion.ExecuteScalarValAsync("NCSapGruposToleranciaIAE",
            //                           MensajesXId.SapGrupoToleranciaNombre);
            //return Mensajes.Ok;
        }
        /// <summary>
        /// Permite eliminar la entidad SapGrupoTolerancia.
        /// </summary>
        protected async Task<Boolean> SapGrupoToleranciaElimina(ESapGrupoTolerancia sapGrupoTolerancia)
        {
            return await _conexion.EntityUpdateAsync(sapGrupoTolerancia, MAccionesBd.Elimina, "NCSapGruposToleranciaIAE");

            //_conexion.AddParamEntity(sapGrupoTolerancia, MAccionesBd.Elimina);
            //return await _conexion.ExecuteNonQueryRetAsync("NCSapGruposToleranciaIAE");
        }
        /// <summary>
        /// Exporta datos a Excel.
        /// </summary>
        protected async Task<string> SapGrupoToleranciaExporta(ESapGrupoToleranciaFiltro sapGrupoToleranciaFiltro,
                                                               MArchivoExcel archivoExcel)
        {
            _conexion.AddParamFilterExp(sapGrupoToleranciaFiltro);
            return await archivoExcel.ExportAsync(_conexion.GetCurrentCmd("NCSapGruposToleranciaCP"),
                                                  "SapGrupoTolerancia.xlsb",
                                                  sapGrupoToleranciaFiltro.Columnas);
        }
        #endregion

        #endregion
    }
}
