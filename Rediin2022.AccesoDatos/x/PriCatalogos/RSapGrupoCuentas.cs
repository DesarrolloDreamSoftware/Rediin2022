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
    public class RSapGrupoCuentas : MRepositorio
    {
        #region Constructores
        /// <summary>
        /// Repositorio.
        /// </summary>
        public RSapGrupoCuentas(IMConexionEntidad conexion)
            : base(conexion)
        {
        }
        #endregion

        #region Funciones

        #region SapGrupoCuenta (SapGrupoCuentas)
        /// <summary>
        /// Consulta paginada de la entidad SapGrupoCuenta.
        /// </summary>
        public ESapGrupoCuentaPag SapGrupoCuentaPag(ESapGrupoCuentaFiltro sapGrupoCuentaFiltro)
        {
            return base.EntidadPag<ESapGrupoCuentaPag>(sapGrupoCuentaFiltro,
                sapGrupoCuentaPag =>
                {
                    _conexion.AddParamFilterTL(sapGrupoCuentaFiltro);
                    _conexion.LoadEntity<ESapGrupoCuentaPag>("NCSapGrupoCuentasCP", sapGrupoCuentaPag);
                },
                sapGrupoCuentaPag =>
                {
                    _conexion.AddParamFilterPag(sapGrupoCuentaFiltro);
                    sapGrupoCuentaPag.Pagina = _conexion.LoadEntities<ESapGrupoCuenta>("NCSapGrupoCuentasCP");
                });
        }
        /// <summary>
        /// Consulta por id de la entidad SapGrupoCuenta.
        /// </summary>
        public ESapGrupoCuenta SapGrupoCuentaXId(String sapGrupoCuentaId)
        {
            _conexion.AddParamIn(nameof(sapGrupoCuentaId), sapGrupoCuentaId);
            return _conexion.LoadEntity<ESapGrupoCuenta>("NCSapGrupoCuentasCI");
        }
        /// <summary>
        /// Consulta para combos de la entidad SapGrupoCuenta.
        /// </summary>
        public List<MEElemento> SapGrupoCuentaCmb()
        {
            return _conexion.LoadCmb<MEElemento>("NCSapGrupoCuentasCCmb");
        }
        /// <summary>
        /// Permite insertar la entidad SapGrupoCuenta.
        /// </summary>
        protected Boolean SapGrupoCuentaInserta(ESapGrupoCuenta sapGrupoCuenta)
        {
            _conexion.AddParamEntity(sapGrupoCuenta, MAccionesBd.Inserta);
            _conexion.ExecuteScalarVal("NCSapGrupoCuentasIAE",
                                       MensajesXId.SapGrupoCuentaNombre);
            return _mensajes.Ok;
        }
        /// <summary>
        /// Permite actualizar la entidad SapGrupoCuenta.
        /// </summary>
        protected Boolean SapGrupoCuentaActualiza(ESapGrupoCuenta sapGrupoCuenta)
        {
            _conexion.AddParamEntity(sapGrupoCuenta, MAccionesBd.Actualiza);
            _conexion.ExecuteScalarVal("NCSapGrupoCuentasIAE",
                                       MensajesXId.SapGrupoCuentaNombre);
            return _mensajes.Ok;
        }
        /// <summary>
        /// Permite eliminar la entidad SapGrupoCuenta.
        /// </summary>
        protected Boolean SapGrupoCuentaElimina(ESapGrupoCuenta sapGrupoCuenta)
        {
            _conexion.AddParamEntity(sapGrupoCuenta, MAccionesBd.Elimina);
            return _conexion.ExecuteNonQueryRet("NCSapGrupoCuentasIAE");
        }
        /// <summary>
        /// Exporta datos a Excel.
        /// </summary>
        protected MEDatosArchivo SapGrupoCuentaExporta(ESapGrupoCuentaFiltro sapGrupoCuentaFiltro,
                                                       MArchivoExcel archivoExcel)
        {
            sapGrupoCuentaFiltro.DatPag.StartLine = 1;
            sapGrupoCuentaFiltro.DatPag.PageSize = Int32.MaxValue;
            _conexion.AddParamFilterPag(sapGrupoCuentaFiltro);

            String vArchivo = archivoExcel.Export(_conexion.GetCurrentCmd("NCSapGrupoCuentasCP"),
                                                  "SapGrupoCuenta.xlsb",
                                                  sapGrupoCuentaFiltro.Columnas);
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
