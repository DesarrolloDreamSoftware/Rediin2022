using DSMetodNetX.Aplicacion;
using DSMetodNetX.Entidades;
using DSMetodNetX.Negocio;
using Rediin2022.Entidades.PriCatalogos;
using System;
using System.Collections.Generic;

namespace Rediin2022.Aplicacion.PriCatalogos
{
    /// <summary>
    /// APL Para conexion con un API.
    /// </summary>
    public class APLBancos : MAplicacion, INBancos
    {
        #region Constructores
        /// <summary>
        /// APL Para conexion con un API.
        /// </summary>
        public APLBancos(IMApiCliente api) : base(api)
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
            return Call<EBancoPag>(NomFn(), bancoFiltro);
        }
        /// <summary>
        /// Consulta por id de la entidad Banco.
        /// </summary>
        public EBanco BancoXId(Int64 bancoId)
        {
            return Call<EBanco>(NomFn(),
                                bancoId);
        }
        /// <summary>
        /// Consulta para combos de la entidad Banco.
        /// </summary>
        public List<MEElemento> BancoCmb()
        {
            return Call<List<MEElemento>>(NomFn());
        }
        /// <summary>
        /// Permite insertar la entidad Banco.
        /// </summary>
        public Int64 BancoInserta(EBanco banco)
        {
            return Call<Int64>(NomFn(), banco);
        }
        /// <summary>
        /// Permite actualizar la entidad Banco.
        /// </summary>
        public Boolean BancoActualiza(EBanco banco)
        {
            return Call<Boolean>(NomFn(), banco);
        }
        /// <summary>
        /// Permite eliminar la entidad Banco.
        /// </summary>
        public Boolean BancoElimina(EBanco banco)
        {
            return Call<Boolean>(NomFn(), banco);
        }
        /// <summary>
        /// Exporta datos a Excel.
        /// </summary>
        public MEDatosArchivo BancoExporta(EBancoFiltro bancoFiltro)
        {
            return Call<MEDatosArchivo>(NomFn(),
                                        bancoFiltro);
        }
        /// <summary>
        /// Reglas de negocio de la entidad Banco.
        /// </summary>
        public List<MEReglaNeg> BancoReglas()
        {
            return Call<List<MEReglaNeg>>(NomFn());
        }
        #endregion

        #endregion
    }
}
