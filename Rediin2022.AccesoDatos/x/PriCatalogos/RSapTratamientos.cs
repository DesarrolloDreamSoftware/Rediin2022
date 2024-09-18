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
    public class RSapTratamientos : MRepositorio
    {
        #region Constructores
        /// <summary>
        /// Repositorio.
        /// </summary>
        public RSapTratamientos(IMConexionEntidad conexion)
            : base(conexion)
        {
        }
        #endregion

        #region Funciones

        #region SapTratamiento (SapTratamientos)
        /// <summary>
        /// Consulta paginada de la entidad SapTratamiento.
        /// </summary>
        public ESapTratamientoPag SapTratamientoPag(ESapTratamientoFiltro sapTratamientoFiltro)
        {
            return base.EntidadPag<ESapTratamientoPag>(sapTratamientoFiltro,
                sapTratamientoPag =>
                {
                    _conexion.AddParamFilterTL(sapTratamientoFiltro);
                    _conexion.LoadEntity<ESapTratamientoPag>("NCSapTratamientosCP", sapTratamientoPag);
                },
                sapTratamientoPag =>
                {
                    _conexion.AddParamFilterPag(sapTratamientoFiltro);
                    sapTratamientoPag.Pagina = _conexion.LoadEntities<ESapTratamiento>("NCSapTratamientosCP");
                });
        }
        /// <summary>
        /// Consulta por id de la entidad SapTratamiento.
        /// </summary>
        public ESapTratamiento SapTratamientoXId(String sapTratamientoId)
        {
            _conexion.AddParamIn(nameof(sapTratamientoId), sapTratamientoId);
            return _conexion.LoadEntity<ESapTratamiento>("NCSapTratamientosCI");
        }
        /// <summary>
        /// Consulta para combos de la entidad SapTratamiento.
        /// </summary>
        public List<MEElemento> SapTratamientoCmb()
        {
            return _conexion.LoadCmb<MEElemento>("NCSapTratamientosCCmb");
        }
        /// <summary>
        /// Permite insertar la entidad SapTratamiento.
        /// </summary>
        protected Boolean SapTratamientoInserta(ESapTratamiento sapTratamiento)
        {
            _conexion.AddParamEntity(sapTratamiento, MAccionesBd.Inserta);
            _conexion.ExecuteScalarVal("NCSapTratamientosIAE",
                                       MensajesXId.SapTratamientoNombre);
            return _mensajes.Ok;
        }
        /// <summary>
        /// Permite actualizar la entidad SapTratamiento.
        /// </summary>
        protected Boolean SapTratamientoActualiza(ESapTratamiento sapTratamiento)
        {
            _conexion.AddParamEntity(sapTratamiento, MAccionesBd.Actualiza);
            _conexion.ExecuteScalarVal("NCSapTratamientosIAE",
                                       MensajesXId.SapTratamientoNombre);
            return _mensajes.Ok;
        }
        /// <summary>
        /// Permite eliminar la entidad SapTratamiento.
        /// </summary>
        protected Boolean SapTratamientoElimina(ESapTratamiento sapTratamiento)
        {
            _conexion.AddParamEntity(sapTratamiento, MAccionesBd.Elimina);
            return _conexion.ExecuteNonQueryRet("NCSapTratamientosIAE");
        }
        /// <summary>
        /// Exporta datos a Excel.
        /// </summary>
        protected MEDatosArchivo SapTratamientoExporta(ESapTratamientoFiltro sapTratamientoFiltro,
                                                       MArchivoExcel archivoExcel)
        {
            sapTratamientoFiltro.DatPag.StartLine = 1;
            sapTratamientoFiltro.DatPag.PageSize = Int32.MaxValue;
            _conexion.AddParamFilterPag(sapTratamientoFiltro);

            String vArchivo = archivoExcel.Export(_conexion.GetCurrentCmd("NCSapTratamientosCP"),
                                                  "SapTratamiento.xlsb",
                                                  sapTratamientoFiltro.Columnas);
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
