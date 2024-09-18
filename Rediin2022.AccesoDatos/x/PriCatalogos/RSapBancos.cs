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
    public class RSapBancos : MRepositorio
    {
        #region Constructores
        /// <summary>
        /// Repositorio.
        /// </summary>
        public RSapBancos(IMConexionEntidad conexion)
            : base(conexion)
        {
        }
        #endregion

        #region Funciones

        #region SapBanco (SapBancos)
        /// <summary>
        /// Consulta paginada de la entidad SapBanco.
        /// </summary>
        public ESapBancoPag SapBancoPag(ESapBancoFiltro sapBancoFiltro)
        {
            return base.EntidadPag<ESapBancoPag>(sapBancoFiltro,
                sapBancoPag =>
                {
                    _conexion.AddParamFilterTL(sapBancoFiltro);
                    _conexion.LoadEntity<ESapBancoPag>("NCSapBancosCP", sapBancoPag);
                },
                sapBancoPag =>
                {
                    _conexion.AddParamFilterPag(sapBancoFiltro);
                    sapBancoPag.Pagina = _conexion.LoadEntities<ESapBanco>("NCSapBancosCP");
                });
        }
        /// <summary>
        /// Consulta por id de la entidad SapBanco.
        /// </summary>
        public ESapBanco SapBancoXId(String sapBancoId)
        {
            _conexion.AddParamIn(nameof(sapBancoId), sapBancoId);
            return _conexion.LoadEntity<ESapBanco>("NCSapBancosCI");
        }
        /// <summary>
        /// Consulta para combos de la entidad SapBanco.
        /// </summary>
        public List<MEElemento> SapBancoCmb()
        {
            return _conexion.LoadCmb<MEElemento>("NCSapBancosCCmb");
        }
        /// <summary>
        /// Permite insertar la entidad SapBanco.
        /// </summary>
        protected Boolean SapBancoInserta(ESapBanco sapBanco)
        {
            _conexion.AddParamEntity(sapBanco, MAccionesBd.Inserta);
            _conexion.ExecuteScalarVal("NCSapBancosIAE",
                                       MensajesXId.SapBancoNombre);
            return _mensajes.Ok;
        }
        /// <summary>
        /// Permite actualizar la entidad SapBanco.
        /// </summary>
        protected Boolean SapBancoActualiza(ESapBanco sapBanco)
        {
            _conexion.AddParamEntity(sapBanco, MAccionesBd.Actualiza);
            _conexion.ExecuteScalarVal("NCSapBancosIAE",
                                       MensajesXId.SapBancoNombre);
            return _mensajes.Ok;
        }
        /// <summary>
        /// Permite eliminar la entidad SapBanco.
        /// </summary>
        protected Boolean SapBancoElimina(ESapBanco sapBanco)
        {
            _conexion.AddParamEntity(sapBanco, MAccionesBd.Elimina);
            return _conexion.ExecuteNonQueryRet("NCSapBancosIAE");
        }
        /// <summary>
        /// Exporta datos a Excel.
        /// </summary>
        protected MEDatosArchivo SapBancoExporta(ESapBancoFiltro sapBancoFiltro,
                                                 MArchivoExcel archivoExcel)
        {
            sapBancoFiltro.DatPag.StartLine = 1;
            sapBancoFiltro.DatPag.PageSize = Int32.MaxValue;
            _conexion.AddParamFilterPag(sapBancoFiltro);

            String vArchivo = archivoExcel.Export(_conexion.GetCurrentCmd("NCSapBancosCP"),
                                                  "SapBanco.xlsb",
                                                  sapBancoFiltro.Columnas);
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
