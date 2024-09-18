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
    public class RSapSociedades : MRepositorio
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
        public RSapSociedades(IMConexionEntidad conexion)
            : base(conexion)
        {
            _conexion = conexion;
        }
        #endregion

        #region Funciones

        #region SapSociedad (SapSociedades)
        /// <summary>
        /// Consulta paginada de la entidad SapSociedad.
        /// </summary>
        public async Task<ESapSociedadPag> SapSociedadPag(ESapSociedadFiltro sapSociedadFiltro)
        {
            return await _conexion.EntidadPagAsync<ESapSociedad,
                                                    ESapSociedadPag,
                                                    ESapSociedadFiltro>(sapSociedadFiltro, "NCSapSociedadesCP");

            //return base.EntidadPagAsync<ESapSociedadPag>(sapSociedadFiltro,
            //               sapSociedadPag =>
            //    {
            //        _conexion.AddParamFilterTL(sapSociedadFiltro);
            //        _conexion.LoadEntity<ESapSociedadPag>("NCSapSociedadesCP", sapSociedadPag);
            //    },
            //    sapSociedadPag =>
            //    {
            //        _conexion.AddParamFilterPag(sapSociedadFiltro);
            //        sapSociedadPag.Pagina = _conexion.LoadEntities<ESapSociedad>("NCSapSociedadesCP");
            //    });
        }
        /// <summary>
        /// Consulta por id de la entidad SapSociedad.
        /// </summary>
        public async Task<ESapSociedad> SapSociedadXId(String sapSociedadId)
        {
            _conexion.AddParamIn(sapSociedadId);
            return await _conexion.LoadEntityAsync<ESapSociedad>("NCSapSociedadesCI");
        }
        /// <summary>
        /// Consulta para combos de la entidad SapSociedad.
        /// </summary>
        public async Task<List<MEElemento>> SapSociedadCmb()
        {
            return await _conexion.EntidadCmbAsync("NCSapSociedadesCCmb");
        }
        /// <summary>
        /// Permite insertar la entidad SapSociedad.
        /// </summary>
        protected async Task<Boolean> SapSociedadInserta(ESapSociedad sapSociedad)
        {
            return await _conexion.EntityUpdateAsync(sapSociedad, MAccionesBd.Inserta, "NCSapSociedadesIAE");
            //return sapSociedad.SapSociedadId;

            //_conexion.AddParamEntity(sapSociedad, MAccionesBd.Inserta);
            //await _conexion.ExecuteScalarValAsync("NCSapSociedadesIAE",
            //                           MensajesXId.SapSociedadNombre);
            //return Mensajes.Ok;
        }
        /// <summary>
        /// Permite actualizar la entidad SapSociedad.
        /// </summary>
        protected async Task<Boolean> SapSociedadActualiza(ESapSociedad sapSociedad)
        {
            return await _conexion.EntityUpdateAsync(sapSociedad, MAccionesBd.Actualiza, "NCSapSociedadesIAE");

            //_conexion.AddParamEntity(sapSociedad, MAccionesBd.Actualiza);
            //await _conexion.ExecuteScalarValAsync("NCSapSociedadesIAE",
            //                           MensajesXId.SapSociedadNombre);
            //return Mensajes.Ok;
        }
        /// <summary>
        /// Permite eliminar la entidad SapSociedad.
        /// </summary>
        protected async Task<Boolean> SapSociedadElimina(ESapSociedad sapSociedad)
        {
            return await _conexion.EntityUpdateAsync(sapSociedad, MAccionesBd.Elimina, "NCSapSociedadesIAE");

            //_conexion.AddParamEntity(sapSociedad, MAccionesBd.Elimina);
            //return await _conexion.ExecuteNonQueryRetAsync("NCSapSociedadesIAE");
        }
        /// <summary>
        /// Exporta datos a Excel.
        /// </summary>
        protected async Task<string> SapSociedadExporta(ESapSociedadFiltro sapSociedadFiltro,
                                                        MArchivoExcel archivoExcel)
        {
            _conexion.AddParamFilterExp(sapSociedadFiltro);
            return await archivoExcel.ExportAsync(_conexion.GetCurrentCmd("NCSapSociedadesCP"),
                                                  "SapSociedad.xlsb",
                                                  sapSociedadFiltro.Columnas);
        }
        #endregion

        #endregion
    }
}
