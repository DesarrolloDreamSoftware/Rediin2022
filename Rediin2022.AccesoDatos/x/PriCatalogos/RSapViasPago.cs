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
    public class RSapViasPago : MRepositorio
    {
        #region Constructores
        /// <summary>
        /// Repositorio.
        /// </summary>
        public RSapViasPago(IMConexionEntidad conexion)
            : base(conexion)
        {
        }
        #endregion

        #region Funciones

        #region SapViaPago (SapViasPago)
        /// <summary>
        /// Consulta paginada de la entidad SapViaPago.
        /// </summary>
        public ESapViaPagoPag SapViaPagoPag(ESapViaPagoFiltro sapViaPagoFiltro)
        {
            return base.EntidadPag<ESapViaPagoPag>(sapViaPagoFiltro,
                sapViaPagoPag =>
                {
                    _conexion.AddParamFilterTL(sapViaPagoFiltro);
                    _conexion.LoadEntity<ESapViaPagoPag>("NCSapViasPagoCP", sapViaPagoPag);
                },
                sapViaPagoPag =>
                {
                    _conexion.AddParamFilterPag(sapViaPagoFiltro);
                    sapViaPagoPag.Pagina = _conexion.LoadEntities<ESapViaPago>("NCSapViasPagoCP");
                });
        }
        /// <summary>
        /// Consulta por id de la entidad SapViaPago.
        /// </summary>
        public ESapViaPago SapViaPagoXId(String sapViaPagoId)
        {
            _conexion.AddParamIn(nameof(sapViaPagoId), sapViaPagoId);
            return _conexion.LoadEntity<ESapViaPago>("NCSapViasPagoCI");
        }
        /// <summary>
        /// Consulta para combos de la entidad SapViaPago.
        /// </summary>
        public List<MEElemento> SapViaPagoCmb()
        {
            return _conexion.LoadCmb<MEElemento>("NCSapViasPagoCCmb");
        }
        /// <summary>
        /// Permite insertar la entidad SapViaPago.
        /// </summary>
        protected Boolean SapViaPagoInserta(ESapViaPago sapViaPago)
        {
            _conexion.AddParamEntity(sapViaPago, MAccionesBd.Inserta);
            _conexion.ExecuteScalarVal("NCSapViasPagoIAE",
                                       MensajesXId.SapViaPagoNombre);
            return _mensajes.Ok;
        }
        /// <summary>
        /// Permite actualizar la entidad SapViaPago.
        /// </summary>
        protected Boolean SapViaPagoActualiza(ESapViaPago sapViaPago)
        {
            _conexion.AddParamEntity(sapViaPago, MAccionesBd.Actualiza);
            _conexion.ExecuteScalarVal("NCSapViasPagoIAE",
                                       MensajesXId.SapViaPagoNombre);
            return _mensajes.Ok;
        }
        /// <summary>
        /// Permite eliminar la entidad SapViaPago.
        /// </summary>
        protected Boolean SapViaPagoElimina(ESapViaPago sapViaPago)
        {
            _conexion.AddParamEntity(sapViaPago, MAccionesBd.Elimina);
            return _conexion.ExecuteNonQueryRet("NCSapViasPagoIAE");
        }
        /// <summary>
        /// Exporta datos a Excel.
        /// </summary>
        protected MEDatosArchivo SapViaPagoExporta(ESapViaPagoFiltro sapViaPagoFiltro,
                                                   MArchivoExcel archivoExcel)
        {
            sapViaPagoFiltro.DatPag.StartLine = 1;
            sapViaPagoFiltro.DatPag.PageSize = Int32.MaxValue;
            _conexion.AddParamFilterPag(sapViaPagoFiltro);

            String vArchivo = archivoExcel.Export(_conexion.GetCurrentCmd("NCSapViasPagoCP"),
                                                  "SapViaPago.xlsb",
                                                  sapViaPagoFiltro.Columnas);
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
