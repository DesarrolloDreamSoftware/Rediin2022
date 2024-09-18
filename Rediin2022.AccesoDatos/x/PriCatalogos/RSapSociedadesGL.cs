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
    public class RSapSociedadesGL : MRepositorio
    {
        #region Constructores
        /// <summary>
        /// Repositorio.
        /// </summary>
        public RSapSociedadesGL(IMConexionEntidad conexion)
            : base(conexion)
        {
        }
        #endregion

        #region Funciones

        #region SapSociedadGL (SapSociedadesGL)
        /// <summary>
        /// Consulta paginada de la entidad SapSociedadGL.
        /// </summary>
        public ESapSociedadGLPag SapSociedadGLPag(ESapSociedadGLFiltro sapSociedadGLFiltro)
        {
            return base.EntidadPag<ESapSociedadGLPag>(sapSociedadGLFiltro,
                sapSociedadGLPag =>
                {
                    _conexion.AddParamFilterTL(sapSociedadGLFiltro);
                    _conexion.LoadEntity<ESapSociedadGLPag>("NCSapSociedadesGLCP", sapSociedadGLPag);
                },
                sapSociedadGLPag =>
                {
                    _conexion.AddParamFilterPag(sapSociedadGLFiltro);
                    sapSociedadGLPag.Pagina = _conexion.LoadEntities<ESapSociedadGL>("NCSapSociedadesGLCP");
                });
        }
        /// <summary>
        /// Consulta por id de la entidad SapSociedadGL.
        /// </summary>
        public ESapSociedadGL SapSociedadGLXId(String sapSociedadGLId)
        {
            _conexion.AddParamIn(nameof(sapSociedadGLId), sapSociedadGLId);
            return _conexion.LoadEntity<ESapSociedadGL>("NCSapSociedadesGLCI");
        }
        /// <summary>
        /// Consulta para combos de la entidad SapSociedadGL.
        /// </summary>
        public List<MEElemento> SapSociedadGLCmb()
        {
            return _conexion.LoadCmb<MEElemento>("NCSapSociedadesGLCCmb");
        }
        /// <summary>
        /// Permite insertar la entidad SapSociedadGL.
        /// </summary>
        protected Boolean SapSociedadGLInserta(ESapSociedadGL sapSociedadGL)
        {
            _conexion.AddParamEntity(sapSociedadGL, MAccionesBd.Inserta);
            _conexion.ExecuteScalarVal("NCSapSociedadesGLIAE",
                                       MensajesXId.SapSociedadGLNombre);
            return _mensajes.Ok;
        }
        /// <summary>
        /// Permite actualizar la entidad SapSociedadGL.
        /// </summary>
        protected Boolean SapSociedadGLActualiza(ESapSociedadGL sapSociedadGL)
        {
            _conexion.AddParamEntity(sapSociedadGL, MAccionesBd.Actualiza);
            _conexion.ExecuteScalarVal("NCSapSociedadesGLIAE",
                                       MensajesXId.SapSociedadGLNombre);
            return _mensajes.Ok;
        }
        /// <summary>
        /// Permite eliminar la entidad SapSociedadGL.
        /// </summary>
        protected Boolean SapSociedadGLElimina(ESapSociedadGL sapSociedadGL)
        {
            _conexion.AddParamEntity(sapSociedadGL, MAccionesBd.Elimina);
            return _conexion.ExecuteNonQueryRet("NCSapSociedadesGLIAE");
        }
        /// <summary>
        /// Exporta datos a Excel.
        /// </summary>
        protected MEDatosArchivo SapSociedadGLExporta(ESapSociedadGLFiltro sapSociedadGLFiltro,
                                                      MArchivoExcel archivoExcel)
        {
            sapSociedadGLFiltro.DatPag.StartLine = 1;
            sapSociedadGLFiltro.DatPag.PageSize = Int32.MaxValue;
            _conexion.AddParamFilterPag(sapSociedadGLFiltro);

            String vArchivo = archivoExcel.Export(_conexion.GetCurrentCmd("NCSapSociedadesGLCP"),
                                                  "SapSociedadGL.xlsb",
                                                  sapSociedadGLFiltro.Columnas);
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
