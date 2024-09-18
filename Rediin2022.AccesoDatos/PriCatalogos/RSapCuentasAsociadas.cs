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
    public class RSapCuentasAsociadas : MRepositorio
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
        public RSapCuentasAsociadas(IMConexionEntidad conexion)
            : base(conexion)
        {
            _conexion = conexion;
    }
        #endregion

        #region Funciones

        #region SapCuentaAsociada (SapCuentasAsociadas)
        /// <summary>
        /// Consulta paginada de la entidad SapCuentaAsociada.
        /// </summary>
        public async Task<ESapCuentaAsociadaPag> SapCuentaAsociadaPag(ESapCuentaAsociadaFiltro sapCuentaAsociadaFiltro)
        {
        return await _conexion.EntidadPagAsync<ESapCuentaAsociada,
                                                ESapCuentaAsociadaPag,
                                                ESapCuentaAsociadaFiltro>(sapCuentaAsociadaFiltro, "NCSapCuentasAsociadasCP");

            //return base.EntidadPagAsync<ESapCuentaAsociadaPag>(sapCuentaAsociadaFiltro,
            //               sapCuentaAsociadaPag =>
            //    {
            //        _conexion.AddParamFilterTL(sapCuentaAsociadaFiltro);
            //        _conexion.LoadEntity<ESapCuentaAsociadaPag>("NCSapCuentasAsociadasCP", sapCuentaAsociadaPag);
            //    },
            //    sapCuentaAsociadaPag =>
            //    {
            //        _conexion.AddParamFilterPag(sapCuentaAsociadaFiltro);
            //        sapCuentaAsociadaPag.Pagina = _conexion.LoadEntities<ESapCuentaAsociada>("NCSapCuentasAsociadasCP");
            //    });
        }
        /// <summary>
        /// Consulta por id de la entidad SapCuentaAsociada.
        /// </summary>
        public async Task<ESapCuentaAsociada> SapCuentaAsociadaXId(String sapCuentaAsociadaId)
        {
            _conexion.AddParamIn(sapCuentaAsociadaId);
            return await _conexion.LoadEntityAsync<ESapCuentaAsociada>("NCSapCuentasAsociadasCI");
        }
        /// <summary>
        /// Consulta para combos de la entidad SapCuentaAsociada.
        /// </summary>
        public async Task<List<MEElemento>> SapCuentaAsociadaCmb()
        {
            return await _conexion.EntidadCmbAsync("NCSapCuentasAsociadasCCmb");
        }
        /// <summary>
        /// Permite insertar la entidad SapCuentaAsociada.
        /// </summary>
        protected async Task<Boolean> SapCuentaAsociadaInserta(ESapCuentaAsociada sapCuentaAsociada)
        {
            return await _conexion.EntityUpdateAsync(sapCuentaAsociada, MAccionesBd.Inserta, "NCSapCuentasAsociadasIAE");
                   //return sapCuentaAsociada.SapCuentaAsociadaId;

            //           _conexion.AddParamEntity(sapCuentaAsociada, MAccionesBd.Inserta);
            //await _conexion.ExecuteScalarValAsync("NCSapCuentasAsociadasIAE",
            //                           MensajesXId.SapCuentaAsociadaNombre);
            //return Mensajes.Ok;
        }
        /// <summary>
        /// Permite actualizar la entidad SapCuentaAsociada.
        /// </summary>
        protected async Task<Boolean> SapCuentaAsociadaActualiza(ESapCuentaAsociada sapCuentaAsociada)
        {
        return await _conexion.EntityUpdateAsync(sapCuentaAsociada, MAccionesBd.Actualiza, "NCSapCuentasAsociadasIAE");

            //           _conexion.AddParamEntity(sapCuentaAsociada, MAccionesBd.Actualiza);
            //await _conexion.ExecuteScalarValAsync("NCSapCuentasAsociadasIAE",
            //                           MensajesXId.SapCuentaAsociadaNombre);
            //return Mensajes.Ok;
        }
        /// <summary>
        /// Permite eliminar la entidad SapCuentaAsociada.
        /// </summary>
        protected async Task<Boolean> SapCuentaAsociadaElimina(ESapCuentaAsociada sapCuentaAsociada)
        {
        return await _conexion.EntityUpdateAsync(sapCuentaAsociada, MAccionesBd.Elimina, "NCSapCuentasAsociadasIAE");

            //           _conexion.AddParamEntity(sapCuentaAsociada, MAccionesBd.Elimina);
            //return await _conexion.ExecuteNonQueryRetAsync("NCSapCuentasAsociadasIAE");
        }
        /// <summary>
        /// Exporta datos a Excel.
        /// </summary>
        protected async Task<string> SapCuentaAsociadaExporta(ESapCuentaAsociadaFiltro sapCuentaAsociadaFiltro,
                                                              MArchivoExcel archivoExcel)
        {
            _conexion.AddParamFilterExp(sapCuentaAsociadaFiltro);
            return await archivoExcel.ExportAsync(_conexion.GetCurrentCmd("NCSapCuentasAsociadasCP"),
                                                  "SapCuentaAsociada.xlsb",
                                                  sapCuentaAsociadaFiltro.Columnas);
        }
        #endregion

        #endregion
    }
}
