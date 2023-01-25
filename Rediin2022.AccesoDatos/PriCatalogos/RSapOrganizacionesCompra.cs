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
    public class RSapOrganizacionesCompra : MRepositorio
    {
        #region Constructores
        /// <summary>
        /// Repositorio.
        /// </summary>
        public RSapOrganizacionesCompra(IMConexionEntidad conexion)
            : base(conexion)
        {
        }
        #endregion

        #region Funciones

        #region SapOrganizacionCompra (SapOrganizacionesCompra)
        /// <summary>
        /// Consulta paginada de la entidad SapOrganizacionCompra.
        /// </summary>
        public ESapOrganizacionCompraPag SapOrganizacionCompraPag(ESapOrganizacionCompraFiltro sapOrganizacionCompraFiltro)
        {
            return base.EntidadPag<ESapOrganizacionCompraPag>(sapOrganizacionCompraFiltro,
                sapOrganizacionCompraPag =>
                {
                    _conexion.AddParamFilterTL(sapOrganizacionCompraFiltro);
                    _conexion.LoadEntity<ESapOrganizacionCompraPag>("NCSapOrganizacionesCompraCP", sapOrganizacionCompraPag);
                },
                sapOrganizacionCompraPag =>
                {
                    _conexion.AddParamFilterPag(sapOrganizacionCompraFiltro);
                    sapOrganizacionCompraPag.Pagina = _conexion.LoadEntities<ESapOrganizacionCompra>("NCSapOrganizacionesCompraCP");
                });
        }
        /// <summary>
        /// Consulta por id de la entidad SapOrganizacionCompra.
        /// </summary>
        public ESapOrganizacionCompra SapOrganizacionCompraXId(String sapOrganizacionCompraId)
        {
            _conexion.AddParamIn(nameof(sapOrganizacionCompraId), sapOrganizacionCompraId);
            return _conexion.LoadEntity<ESapOrganizacionCompra>("NCSapOrganizacionesCompraCI");
        }
        /// <summary>
        /// Consulta para combos de la entidad SapOrganizacionCompra.
        /// </summary>
        public List<MEElemento> SapOrganizacionCompraCmb()
        {
            return _conexion.LoadCmb<MEElemento>("NCSapOrganizacionesCompraCCmb");
        }
        /// <summary>
        /// Permite insertar la entidad SapOrganizacionCompra.
        /// </summary>
        protected Boolean SapOrganizacionCompraInserta(ESapOrganizacionCompra sapOrganizacionCompra)
        {
            _conexion.AddParamEntity(sapOrganizacionCompra, MAccionesBd.Inserta);
            _conexion.ExecuteScalarVal("NCSapOrganizacionesCompraIAE",
                                       MensajesXId.SapOrganizacionCompraNombre);
            return _mensajes.Ok;
        }
        /// <summary>
        /// Permite actualizar la entidad SapOrganizacionCompra.
        /// </summary>
        protected Boolean SapOrganizacionCompraActualiza(ESapOrganizacionCompra sapOrganizacionCompra)
        {
            _conexion.AddParamEntity(sapOrganizacionCompra, MAccionesBd.Actualiza);
            _conexion.ExecuteScalarVal("NCSapOrganizacionesCompraIAE",
                                       MensajesXId.SapOrganizacionCompraNombre);
            return _mensajes.Ok;
        }
        /// <summary>
        /// Permite eliminar la entidad SapOrganizacionCompra.
        /// </summary>
        protected Boolean SapOrganizacionCompraElimina(ESapOrganizacionCompra sapOrganizacionCompra)
        {
            _conexion.AddParamEntity(sapOrganizacionCompra, MAccionesBd.Elimina);
            return _conexion.ExecuteNonQueryRet("NCSapOrganizacionesCompraIAE");
        }
        /// <summary>
        /// Exporta datos a Excel.
        /// </summary>
        protected MEDatosArchivo SapOrganizacionCompraExporta(ESapOrganizacionCompraFiltro sapOrganizacionCompraFiltro,
                                                              MArchivoExcel archivoExcel)
        {
            sapOrganizacionCompraFiltro.DatPag.StartLine = 1;
            sapOrganizacionCompraFiltro.DatPag.PageSize = Int32.MaxValue;
            _conexion.AddParamFilterPag(sapOrganizacionCompraFiltro);

            String vArchivo = archivoExcel.Export(_conexion.GetCurrentCmd("NCSapOrganizacionesCompraCP"),
                                                  "SapOrganizacionCompra.xlsb",
                                                  sapOrganizacionCompraFiltro.Columnas);
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
