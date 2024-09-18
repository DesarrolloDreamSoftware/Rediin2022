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
    public class RSapSociedades : MRepositorio
    {
        #region Constructores
        /// <summary>
        /// Repositorio.
        /// </summary>
        public RSapSociedades(IMConexionEntidad conexion)
            : base(conexion)
        {
        }
        #endregion

        #region Funciones

        #region SapSociedad (SapSociedades)
        /// <summary>
        /// Consulta paginada de la entidad SapSociedad.
        /// </summary>
        public ESapSociedadPag SapSociedadPag(ESapSociedadFiltro sapSociedadFiltro)
        {
            return base.EntidadPag<ESapSociedadPag>(sapSociedadFiltro,
                sapSociedadPag =>
                {
                    _conexion.AddParamFilterTL(sapSociedadFiltro);
                    _conexion.LoadEntity<ESapSociedadPag>("NCSapSociedadesCP", sapSociedadPag);
                },
                sapSociedadPag =>
                {
                    _conexion.AddParamFilterPag(sapSociedadFiltro);
                    sapSociedadPag.Pagina = _conexion.LoadEntities<ESapSociedad>("NCSapSociedadesCP");
                });
        }
        /// <summary>
        /// Consulta por id de la entidad SapSociedad.
        /// </summary>
        public ESapSociedad SapSociedadXId(String sapSociedadId)
        {
            _conexion.AddParamIn(nameof(sapSociedadId), sapSociedadId);
            return _conexion.LoadEntity<ESapSociedad>("NCSapSociedadesCI");
        }
        /// <summary>
        /// Consulta para combos de la entidad SapSociedad.
        /// </summary>
        public List<MEElemento> SapSociedadCmb()
        {
            return _conexion.LoadCmb<MEElemento>("NCSapSociedadesCCmb");
        }
        /// <summary>
        /// Permite insertar la entidad SapSociedad.
        /// </summary>
        protected Boolean SapSociedadInserta(ESapSociedad sapSociedad)
        {
            _conexion.AddParamEntity(sapSociedad, MAccionesBd.Inserta);
            _conexion.ExecuteScalarVal("NCSapSociedadesIAE",
                                       MensajesXId.SapSociedadNombre);
            return _mensajes.Ok;
        }
        /// <summary>
        /// Permite actualizar la entidad SapSociedad.
        /// </summary>
        protected Boolean SapSociedadActualiza(ESapSociedad sapSociedad)
        {
            _conexion.AddParamEntity(sapSociedad, MAccionesBd.Actualiza);
            _conexion.ExecuteScalarVal("NCSapSociedadesIAE",
                                       MensajesXId.SapSociedadNombre);
            return _mensajes.Ok;
        }
        /// <summary>
        /// Permite eliminar la entidad SapSociedad.
        /// </summary>
        protected Boolean SapSociedadElimina(ESapSociedad sapSociedad)
        {
            _conexion.AddParamEntity(sapSociedad, MAccionesBd.Elimina);
            return _conexion.ExecuteNonQueryRet("NCSapSociedadesIAE");
        }
        /// <summary>
        /// Exporta datos a Excel.
        /// </summary>
        protected MEDatosArchivo SapSociedadExporta(ESapSociedadFiltro sapSociedadFiltro,
                                                    MArchivoExcel archivoExcel)
        {
            sapSociedadFiltro.DatPag.StartLine = 1;
            sapSociedadFiltro.DatPag.PageSize = Int32.MaxValue;
            _conexion.AddParamFilterPag(sapSociedadFiltro);

            String vArchivo = archivoExcel.Export(_conexion.GetCurrentCmd("NCSapSociedadesCP"),
                                                  "SapSociedad.xlsb",
                                                  sapSociedadFiltro.Columnas);
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
