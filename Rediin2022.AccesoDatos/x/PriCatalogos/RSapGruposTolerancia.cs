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
    public class RSapGruposTolerancia : MRepositorio
    {
        #region Constructores
        /// <summary>
        /// Repositorio.
        /// </summary>
        public RSapGruposTolerancia(IMConexionEntidad conexion)
            : base(conexion)
        {
        }
        #endregion

        #region Funciones

        #region SapGrupoTolerancia (SapGruposTolerancia)
        /// <summary>
        /// Consulta paginada de la entidad SapGrupoTolerancia.
        /// </summary>
        public ESapGrupoToleranciaPag SapGrupoToleranciaPag(ESapGrupoToleranciaFiltro sapGrupoToleranciaFiltro)
        {
            return base.EntidadPag<ESapGrupoToleranciaPag>(sapGrupoToleranciaFiltro,
                sapGrupoToleranciaPag =>
                {
                    _conexion.AddParamFilterTL(sapGrupoToleranciaFiltro);
                    _conexion.LoadEntity<ESapGrupoToleranciaPag>("NCSapGruposToleranciaCP", sapGrupoToleranciaPag);
                },
                sapGrupoToleranciaPag =>
                {
                    _conexion.AddParamFilterPag(sapGrupoToleranciaFiltro);
                    sapGrupoToleranciaPag.Pagina = _conexion.LoadEntities<ESapGrupoTolerancia>("NCSapGruposToleranciaCP");
                });
        }
        /// <summary>
        /// Consulta por id de la entidad SapGrupoTolerancia.
        /// </summary>
        public ESapGrupoTolerancia SapGrupoToleranciaXId(String sapGrupoToleranciaId)
        {
            _conexion.AddParamIn(nameof(sapGrupoToleranciaId), sapGrupoToleranciaId);
            return _conexion.LoadEntity<ESapGrupoTolerancia>("NCSapGruposToleranciaCI");
        }
        /// <summary>
        /// Consulta para combos de la entidad SapGrupoTolerancia.
        /// </summary>
        public List<MEElemento> SapGrupoToleranciaCmb()
        {
            return _conexion.LoadCmb<MEElemento>("NCSapGruposToleranciaCCmb");
        }
        /// <summary>
        /// Permite insertar la entidad SapGrupoTolerancia.
        /// </summary>
        protected Boolean SapGrupoToleranciaInserta(ESapGrupoTolerancia sapGrupoTolerancia)
        {
            _conexion.AddParamEntity(sapGrupoTolerancia, MAccionesBd.Inserta);
            _conexion.ExecuteScalarVal("NCSapGruposToleranciaIAE",
                                       MensajesXId.SapGrupoToleranciaNombre);
            return _mensajes.Ok;
        }
        /// <summary>
        /// Permite actualizar la entidad SapGrupoTolerancia.
        /// </summary>
        protected Boolean SapGrupoToleranciaActualiza(ESapGrupoTolerancia sapGrupoTolerancia)
        {
            _conexion.AddParamEntity(sapGrupoTolerancia, MAccionesBd.Actualiza);
            _conexion.ExecuteScalarVal("NCSapGruposToleranciaIAE",
                                       MensajesXId.SapGrupoToleranciaNombre);
            return _mensajes.Ok;
        }
        /// <summary>
        /// Permite eliminar la entidad SapGrupoTolerancia.
        /// </summary>
        protected Boolean SapGrupoToleranciaElimina(ESapGrupoTolerancia sapGrupoTolerancia)
        {
            _conexion.AddParamEntity(sapGrupoTolerancia, MAccionesBd.Elimina);
            return _conexion.ExecuteNonQueryRet("NCSapGruposToleranciaIAE");
        }
        /// <summary>
        /// Exporta datos a Excel.
        /// </summary>
        protected MEDatosArchivo SapGrupoToleranciaExporta(ESapGrupoToleranciaFiltro sapGrupoToleranciaFiltro,
                                                           MArchivoExcel archivoExcel)
        {
            sapGrupoToleranciaFiltro.DatPag.StartLine = 1;
            sapGrupoToleranciaFiltro.DatPag.PageSize = Int32.MaxValue;
            _conexion.AddParamFilterPag(sapGrupoToleranciaFiltro);

            String vArchivo = archivoExcel.Export(_conexion.GetCurrentCmd("NCSapGruposToleranciaCP"),
                                                  "SapGrupoTolerancia.xlsb",
                                                  sapGrupoToleranciaFiltro.Columnas);
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
