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
    public class RSapCondicionesPago : MRepositorio
    {
        #region Constructores
        /// <summary>
        /// Repositorio.
        /// </summary>
        public RSapCondicionesPago(IMConexionEntidad conexion)
            : base(conexion)
        {
        }
        #endregion

        #region Funciones

        #region SapCondicionPago (SapCondicionesPago)
        /// <summary>
        /// Consulta paginada de la entidad SapCondicionPago.
        /// </summary>
        public ESapCondicionPagoPag SapCondicionPagoPag(ESapCondicionPagoFiltro sapCondicionPagoFiltro)
        {
            return base.EntidadPag<ESapCondicionPagoPag>(sapCondicionPagoFiltro,
                sapCondicionPagoPag =>
                {
                    _conexion.AddParamFilterTL(sapCondicionPagoFiltro);
                    _conexion.LoadEntity<ESapCondicionPagoPag>("NCSapCondicionesPagoCP", sapCondicionPagoPag);
                },
                sapCondicionPagoPag =>
                {
                    _conexion.AddParamFilterPag(sapCondicionPagoFiltro);
                    sapCondicionPagoPag.Pagina = _conexion.LoadEntities<ESapCondicionPago>("NCSapCondicionesPagoCP");
                });
        }
        /// <summary>
        /// Consulta por id de la entidad SapCondicionPago.
        /// </summary>
        public ESapCondicionPago SapCondicionPagoXId(String sapCondicionPagoId)
        {
            _conexion.AddParamIn(nameof(sapCondicionPagoId), sapCondicionPagoId);
            return _conexion.LoadEntity<ESapCondicionPago>("NCSapCondicionesPagoCI");
        }
        /// <summary>
        /// Consulta para combos de la entidad SapCondicionPago.
        /// </summary>
        public List<MEElemento> SapCondicionPagoCmb()
        {
            return _conexion.LoadCmb<MEElemento>("NCSapCondicionesPagoCCmb");
        }
        /// <summary>
        /// Permite insertar la entidad SapCondicionPago.
        /// </summary>
        protected Boolean SapCondicionPagoInserta(ESapCondicionPago sapCondicionPago)
        {
            _conexion.AddParamEntity(sapCondicionPago, MAccionesBd.Inserta);
            _conexion.ExecuteScalarVal("NCSapCondicionesPagoIAE",
                                       MensajesXId.SapCondicionPagoNombre);
            return _mensajes.Ok;
        }
        /// <summary>
        /// Permite actualizar la entidad SapCondicionPago.
        /// </summary>
        protected Boolean SapCondicionPagoActualiza(ESapCondicionPago sapCondicionPago)
        {
            _conexion.AddParamEntity(sapCondicionPago, MAccionesBd.Actualiza);
            _conexion.ExecuteScalarVal("NCSapCondicionesPagoIAE",
                                       MensajesXId.SapCondicionPagoNombre);
            return _mensajes.Ok;
        }
        /// <summary>
        /// Permite eliminar la entidad SapCondicionPago.
        /// </summary>
        protected Boolean SapCondicionPagoElimina(ESapCondicionPago sapCondicionPago)
        {
            _conexion.AddParamEntity(sapCondicionPago, MAccionesBd.Elimina);
            return _conexion.ExecuteNonQueryRet("NCSapCondicionesPagoIAE");
        }
        /// <summary>
        /// Exporta datos a Excel.
        /// </summary>
        protected MEDatosArchivo SapCondicionPagoExporta(ESapCondicionPagoFiltro sapCondicionPagoFiltro,
                                                         MArchivoExcel archivoExcel)
        {
            sapCondicionPagoFiltro.DatPag.StartLine = 1;
            sapCondicionPagoFiltro.DatPag.PageSize = Int32.MaxValue;
            _conexion.AddParamFilterPag(sapCondicionPagoFiltro);

            String vArchivo = archivoExcel.Export(_conexion.GetCurrentCmd("NCSapCondicionesPagoCP"),
                                                  "SapCondicionPago.xlsb",
                                                  sapCondicionPagoFiltro.Columnas);
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
