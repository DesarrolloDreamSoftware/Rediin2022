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
    public class RBancos : MRepositorio
    {
        #region Constructores
        /// <summary>
        /// Repositorio.
        /// </summary>
        public RBancos(IMConexionEntidad conexion)
            : base(conexion)
        {
        }
        #endregion

        #region Funciones

        #region Banco (Bancos)
        /// <summary>
        /// Consulta paginada de la entidad Banco.
        /// </summary>
        public EBancoPag BancoPag(EBancoFiltro bancoFiltro)
        {
            return base.EntidadPag<EBancoPag>(bancoFiltro,
                bancoPag =>
                {
                    _conexion.AddParamFilterTL(bancoFiltro);
                    _conexion.LoadEntity<EBancoPag>("NCBancosCP", bancoPag);
                },
                bancoPag =>
                {
                    _conexion.AddParamFilterPag(bancoFiltro);
                    bancoPag.Pagina = _conexion.LoadEntities<EBanco>("NCBancosCP");
                });
        }
        /// <summary>
        /// Consulta por id de la entidad Banco.
        /// </summary>
        public EBanco BancoXId(Int64 bancoId)
        {
            _conexion.AddParamIn(nameof(bancoId), bancoId);
            return _conexion.LoadEntity<EBanco>("NCBancosCI");
        }
        /// <summary>
        /// Consulta para combos de la entidad Banco.
        /// </summary>
        public List<MEElemento> BancoCmb()
        {
            return _conexion.LoadCmb<MEElemento>("NCBancosCCmb");
        }
        /// <summary>
        /// Permite insertar la entidad Banco.
        /// </summary>
        protected Int64 BancoInserta(EBanco banco)
        {
            _conexion.AddParamEntity(banco, MAccionesBd.Inserta);
            Int64 vResultado = _conexion.ExecuteScalarVal("NCBancosIAE",
                                                          MensajesXId.BancoNombre);
            return vResultado;
        }
        /// <summary>
        /// Permite actualizar la entidad Banco.
        /// </summary>
        protected Boolean BancoActualiza(EBanco banco)
        {
            _conexion.AddParamEntity(banco, MAccionesBd.Actualiza);
            _conexion.ExecuteScalarVal("NCBancosIAE",
                                       MensajesXId.BancoNombre);
            return _mensajes.Ok;
        }
        /// <summary>
        /// Permite eliminar la entidad Banco.
        /// </summary>
        protected Boolean BancoElimina(EBanco banco)
        {
            _conexion.AddParamEntity(banco, MAccionesBd.Elimina);
            return _conexion.ExecuteNonQueryRet("NCBancosIAE");
        }
        /// <summary>
        /// Exporta datos a Excel.
        /// </summary>
        protected MEDatosArchivo BancoExporta(EBancoFiltro bancoFiltro,
                                              MArchivoExcel archivoExcel)
        {
            bancoFiltro.DatPag.StartLine = 1;
            bancoFiltro.DatPag.PageSize = Int32.MaxValue;
            _conexion.AddParamFilterPag(bancoFiltro);

            String vArchivo = archivoExcel.Export(_conexion.GetCurrentCmd("NCBancosCP"),
                                                  "Banco.xlsb",
                                                  bancoFiltro.Columnas);
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
