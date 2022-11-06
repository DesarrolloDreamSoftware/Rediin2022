using DSMetodNetX.Entidades;
using DSMetodNetX.Negocio;
using System;
using System.Collections.Generic;

namespace Rediin2022.Entidades.PriCatalogos
{
    /// <summary>
    /// Interfaz de negocio.
    /// </summary>
    public interface INBancos : IMCtrMensajes
    {
        #region Funciones

        #region Banco (Bancos)
        /// <summary>
        /// Consulta paginada de la entidad Banco.
        /// </summary>
        EBancoPag BancoPag(EBancoFiltro bancoFiltro);
        /// <summary>
        /// Consulta por id de la entidad Banco.
        /// </summary>
        EBanco BancoXId(Int64 bancoId);
        /// <summary>
        /// Consulta para combos de la entidad Banco.
        /// </summary>
        List<MEElemento> BancoCmb();
        /// <summary>
        /// Permite insertar la entidad Banco.
        /// </summary>
        Int64 BancoInserta(EBanco banco);
        /// <summary>
        /// Permite actualizar la entidad Banco.
        /// </summary>
        Boolean BancoActualiza(EBanco banco);
        /// <summary>
        /// Permite eliminar la entidad Banco.
        /// </summary>
        Boolean BancoElimina(EBanco banco);
        /// <summary>
        /// Exporta datos a Excel.
        /// </summary>
        MEDatosArchivo BancoExporta(EBancoFiltro bancoFiltro);
        /// <summary>
        /// Reglas de negocio de la entidad Banco.
        /// </summary>
        List<MEReglaNeg> BancoReglas();
        #endregion

        #endregion
    }
}
