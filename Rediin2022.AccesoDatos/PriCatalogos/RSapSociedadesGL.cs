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
    public class RSapSociedadesGL : MRepositorio
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
        public RSapSociedadesGL(IMConexionEntidad conexion)
            : base(conexion)
        {
            _conexion = conexion;
        }
        #endregion

        #region Funciones

        #region SapSociedadGL (SapSociedadesGL)
        /// <summary>
        /// Consulta paginada de la entidad SapSociedadGL.
        /// </summary>
        public async Task<ESapSociedadGLPag> SapSociedadGLPag(ESapSociedadGLFiltro sapSociedadGLFiltro)
        {
            return await _conexion.EntidadPagAsync<ESapSociedadGL,
                                                    ESapSociedadGLPag,
                                                    ESapSociedadGLFiltro>(sapSociedadGLFiltro, "NCSapSociedadesGLCP");

            //return base.EntidadPagAsync<ESapSociedadGLPag>(sapSociedadGLFiltro,
            //               sapSociedadGLPag =>
            //    {
            //        _conexion.AddParamFilterTL(sapSociedadGLFiltro);
            //        _conexion.LoadEntity<ESapSociedadGLPag>("NCSapSociedadesGLCP", sapSociedadGLPag);
            //    },
            //    sapSociedadGLPag =>
            //    {
            //        _conexion.AddParamFilterPag(sapSociedadGLFiltro);
            //        sapSociedadGLPag.Pagina = _conexion.LoadEntities<ESapSociedadGL>("NCSapSociedadesGLCP");
            //    });
        }
        /// <summary>
        /// Consulta por id de la entidad SapSociedadGL.
        /// </summary>
        public async Task<ESapSociedadGL> SapSociedadGLXId(String sapSociedadGLId)
        {
            _conexion.AddParamIn(sapSociedadGLId);
            return await _conexion.LoadEntityAsync<ESapSociedadGL>("NCSapSociedadesGLCI");
        }
        /// <summary>
        /// Consulta para combos de la entidad SapSociedadGL.
        /// </summary>
        public async Task<List<MEElemento>> SapSociedadGLCmb()
        {
            return await _conexion.EntidadCmbAsync("NCSapSociedadesGLCCmb");
        }
        /// <summary>
        /// Permite insertar la entidad SapSociedadGL.
        /// </summary>
        protected async Task<Boolean> SapSociedadGLInserta(ESapSociedadGL sapSociedadGL)
        {
            return await _conexion.EntityUpdateAsync(sapSociedadGL, MAccionesBd.Inserta, "NCSapSociedadesGLIAE");
            //return sapSociedadGL.SapSociedadGLId;

            //_conexion.AddParamEntity(sapSociedadGL, MAccionesBd.Inserta);
            //await _conexion.ExecuteScalarValAsync("NCSapSociedadesGLIAE",
            //                           MensajesXId.SapSociedadGLNombre);
            //return Mensajes.Ok;
        }
        /// <summary>
        /// Permite actualizar la entidad SapSociedadGL.
        /// </summary>
        protected async Task<Boolean> SapSociedadGLActualiza(ESapSociedadGL sapSociedadGL)
        {
            return await _conexion.EntityUpdateAsync(sapSociedadGL, MAccionesBd.Actualiza, "NCSapSociedadesGLIAE");

            //_conexion.AddParamEntity(sapSociedadGL, MAccionesBd.Actualiza);
            //await _conexion.ExecuteScalarValAsync("NCSapSociedadesGLIAE",
            //                           MensajesXId.SapSociedadGLNombre);
            //return Mensajes.Ok;
        }
        /// <summary>
        /// Permite eliminar la entidad SapSociedadGL.
        /// </summary>
        protected async Task<Boolean> SapSociedadGLElimina(ESapSociedadGL sapSociedadGL)
        {
            return await _conexion.EntityUpdateAsync(sapSociedadGL, MAccionesBd.Elimina, "NCSapSociedadesGLIAE");

            //_conexion.AddParamEntity(sapSociedadGL, MAccionesBd.Elimina);
            //return await _conexion.ExecuteNonQueryRetAsync("NCSapSociedadesGLIAE");
        }
        /// <summary>
        /// Exporta datos a Excel.
        /// </summary>
        protected async Task<string> SapSociedadGLExporta(ESapSociedadGLFiltro sapSociedadGLFiltro,
                                                          MArchivoExcel archivoExcel)
        {
            _conexion.AddParamFilterExp(sapSociedadGLFiltro);
            return await archivoExcel.ExportAsync(_conexion.GetCurrentCmd("NCSapSociedadesGLCP"),
                                                  "SapSociedadGL.xlsb",
                                                  sapSociedadGLFiltro.Columnas);
        }
        #endregion

        #endregion
    }
}
