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
    public class RSapGruposTesoreria : MRepositorio
    {
        #region Constructores
        /// <summary>
        /// Repositorio.
        /// </summary>
        public RSapGruposTesoreria(IMConexionEntidad conexion)
            : base(conexion)
        {
        }
        #endregion

        #region Funciones

        #region SapGrupoTesoreria (SapGruposTesoreria)
        /// <summary>
        /// Consulta paginada de la entidad SapGrupoTesoreria.
        /// </summary>
        public ESapGrupoTesoreriaPag SapGrupoTesoreriaPag(ESapGrupoTesoreriaFiltro sapGrupoTesoreriaFiltro)
        {
            return base.EntidadPag<ESapGrupoTesoreriaPag>(sapGrupoTesoreriaFiltro,
                sapGrupoTesoreriaPag =>
                {
                    _conexion.AddParamFilterTL(sapGrupoTesoreriaFiltro);
                    _conexion.LoadEntity<ESapGrupoTesoreriaPag>("NCSapGruposTesoreriaCP", sapGrupoTesoreriaPag);
                },
                sapGrupoTesoreriaPag =>
                {
                    _conexion.AddParamFilterPag(sapGrupoTesoreriaFiltro);
                    sapGrupoTesoreriaPag.Pagina = _conexion.LoadEntities<ESapGrupoTesoreria>("NCSapGruposTesoreriaCP");
                });
        }
        /// <summary>
        /// Consulta por id de la entidad SapGrupoTesoreria.
        /// </summary>
        public ESapGrupoTesoreria SapGrupoTesoreriaXId(String sapGrupoTesoreriaId)
        {
            _conexion.AddParamIn(nameof(sapGrupoTesoreriaId), sapGrupoTesoreriaId);
            return _conexion.LoadEntity<ESapGrupoTesoreria>("NCSapGruposTesoreriaCI");
        }
        /// <summary>
        /// Consulta para combos de la entidad SapGrupoTesoreria.
        /// </summary>
        public List<MEElemento> SapGrupoTesoreriaCmb()
        {
            return _conexion.LoadCmb<MEElemento>("NCSapGruposTesoreriaCCmb");
        }
        /// <summary>
        /// Permite insertar la entidad SapGrupoTesoreria.
        /// </summary>
        protected Boolean SapGrupoTesoreriaInserta(ESapGrupoTesoreria sapGrupoTesoreria)
        {
            _conexion.AddParamEntity(sapGrupoTesoreria, MAccionesBd.Inserta);
            _conexion.ExecuteScalarVal("NCSapGruposTesoreriaIAE",
                                       MensajesXId.SapGrupoTesoreriaNombre);
            return _mensajes.Ok;
        }
        /// <summary>
        /// Permite actualizar la entidad SapGrupoTesoreria.
        /// </summary>
        protected Boolean SapGrupoTesoreriaActualiza(ESapGrupoTesoreria sapGrupoTesoreria)
        {
            _conexion.AddParamEntity(sapGrupoTesoreria, MAccionesBd.Actualiza);
            _conexion.ExecuteScalarVal("NCSapGruposTesoreriaIAE",
                                       MensajesXId.SapGrupoTesoreriaNombre);
            return _mensajes.Ok;
        }
        /// <summary>
        /// Permite eliminar la entidad SapGrupoTesoreria.
        /// </summary>
        protected Boolean SapGrupoTesoreriaElimina(ESapGrupoTesoreria sapGrupoTesoreria)
        {
            _conexion.AddParamEntity(sapGrupoTesoreria, MAccionesBd.Elimina);
            return _conexion.ExecuteNonQueryRet("NCSapGruposTesoreriaIAE");
        }
        /// <summary>
        /// Exporta datos a Excel.
        /// </summary>
        protected MEDatosArchivo SapGrupoTesoreriaExporta(ESapGrupoTesoreriaFiltro sapGrupoTesoreriaFiltro,
                                                          MArchivoExcel archivoExcel)
        {
            sapGrupoTesoreriaFiltro.DatPag.StartLine = 1;
            sapGrupoTesoreriaFiltro.DatPag.PageSize = Int32.MaxValue;
            _conexion.AddParamFilterPag(sapGrupoTesoreriaFiltro);

            String vArchivo = archivoExcel.Export(_conexion.GetCurrentCmd("NCSapGruposTesoreriaCP"),
                                                  "SapGrupoTesoreria.xlsb",
                                                  sapGrupoTesoreriaFiltro.Columnas);
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
