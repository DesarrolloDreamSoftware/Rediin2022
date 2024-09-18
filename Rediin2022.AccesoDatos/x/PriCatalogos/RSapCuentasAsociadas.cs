using DSEntityNetX.Common.Casting;
using DSEntityNetX.DataAccess;
using DSMetodNetX.AccesoDatos;
using DSMetodNetX.Entidades;
using DSMetodNetX.Idioma;
using Rediin2022.Entidades.Idioma;
using Rediin2022.Entidades.PriCatalogos;
using System;
using System.Collections.Generic;

namespace Rediin2022.AccesoDatos.PriCatalogos
{
    /// <summary>
    /// Repositorio.
    /// </summary>
    [Serializable]
    public class RSapCuentasAsociadas : MRepositorio
    {
        #region Constructores
        /// <summary>
        /// Repositorio.
        /// </summary>
        public RSapCuentasAsociadas(IMConexionEntidad conexion)
            : base(conexion)
        {
        }
        #endregion

        #region Funciones

        #region SapCuentaAsociada (SapCuentasAsociadas)
        /// <summary>
        /// Consulta paginada de la entidad SapCuentaAsociada.
        /// </summary>
        public ESapCuentaAsociadaPag SapCuentaAsociadaPag(ESapCuentaAsociadaFiltro sapCuentaAsociadaFiltro)
        {
            return base.EntidadPag<ESapCuentaAsociadaPag>(sapCuentaAsociadaFiltro,
                sapCuentaAsociadaPag =>
                {
                    _conexion.AddParamFilterTL(sapCuentaAsociadaFiltro);
                    _conexion.LoadEntity<ESapCuentaAsociadaPag>("NCSapCuentasAsociadasCP", sapCuentaAsociadaPag);
                },
                sapCuentaAsociadaPag =>
                {
                    _conexion.AddParamFilterPag(sapCuentaAsociadaFiltro);
                    sapCuentaAsociadaPag.Pagina = _conexion.LoadEntities<ESapCuentaAsociada>("NCSapCuentasAsociadasCP");
                });
        }
        /// <summary>
        /// Consulta por id de la entidad SapCuentaAsociada.
        /// </summary>
        public ESapCuentaAsociada SapCuentaAsociadaXId(String sapCuentaAsociadaId)
        {
            _conexion.AddParamIn(nameof(sapCuentaAsociadaId), sapCuentaAsociadaId);
            return _conexion.LoadEntity<ESapCuentaAsociada>("NCSapCuentasAsociadasCI");
        }
        /// <summary>
        /// Consulta para combos de la entidad SapCuentaAsociada.
        /// </summary>
        public List<MEElemento> SapCuentaAsociadaCmb()
        {
            return _conexion.LoadCmb<MEElemento>("NCSapCuentasAsociadasCCmb");
        }
        /// <summary>
        /// Permite insertar la entidad SapCuentaAsociada.
        /// </summary>
        protected Boolean SapCuentaAsociadaInserta(ESapCuentaAsociada sapCuentaAsociada)
        {
            _conexion.AddParamEntity(sapCuentaAsociada, MAccionesBd.Inserta);
            _conexion.ExecuteScalarVal("NCSapCuentasAsociadasIAE",
                                       MensajesXId.SapCuentaAsociadaNombre);
            return _mensajes.Ok;
        }
        /// <summary>
        /// Permite actualizar la entidad SapCuentaAsociada.
        /// </summary>
        protected Boolean SapCuentaAsociadaActualiza(ESapCuentaAsociada sapCuentaAsociada)
        {
            _conexion.AddParamEntity(sapCuentaAsociada, MAccionesBd.Actualiza);
            _conexion.ExecuteScalarVal("NCSapCuentasAsociadasIAE",
                                       MensajesXId.SapCuentaAsociadaNombre);
            return _mensajes.Ok;
        }
        /// <summary>
        /// Permite eliminar la entidad SapCuentaAsociada.
        /// </summary>
        protected Boolean SapCuentaAsociadaElimina(ESapCuentaAsociada sapCuentaAsociada)
        {
            _conexion.AddParamEntity(sapCuentaAsociada, MAccionesBd.Elimina);
            return _conexion.ExecuteNonQueryRet("NCSapCuentasAsociadasIAE");
        }
        /// <summary>
        /// Exporta datos a Excel.
        /// </summary>
        protected MEDatosArchivo SapCuentaAsociadaExporta(ESapCuentaAsociadaFiltro sapCuentaAsociadaFiltro,
                                                          MArchivoExcel archivoExcel)
        {
            sapCuentaAsociadaFiltro.DatPag.StartLine = 1;
            sapCuentaAsociadaFiltro.DatPag.PageSize = Int32.MaxValue;
            _conexion.AddParamFilterPag(sapCuentaAsociadaFiltro);

            String vArchivo = archivoExcel.Export(_conexion.GetCurrentCmd("NCSapCuentasAsociadasCP"),
                                                  "SapCuentaAsociada.xlsb",
                                                  sapCuentaAsociadaFiltro.Columnas);
            return new MEDatosArchivo()
            {
                PathOrg = vArchivo,
                PathDes = vArchivo
            };
        }
        #endregion

        #endregion
    }
}
