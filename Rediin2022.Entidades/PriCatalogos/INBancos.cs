using DSMetodNetX.Entidades;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rediin2022.Entidades.PriCatalogos;

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
    Task<EBancoPag> BancoPag(EBancoFiltro bancoFiltro);
    /// <summary>
    /// Consulta por id de la entidad Banco.
    /// </summary>
    Task<EBanco> BancoXId(Int64 bancoId);
    /// <summary>
    /// Consulta para combos de la entidad Banco.
    /// </summary>
    Task<List<MEElemento>> BancoCmb();
    /// <summary>
    /// Permite insertar la entidad Banco.
    /// </summary>
    Task<Int64> BancoInserta(EBanco banco);
    /// <summary>
    /// Permite actualizar la entidad Banco.
    /// </summary>
    Task<Boolean> BancoActualiza(EBanco banco);
    /// <summary>
    /// Permite eliminar la entidad Banco.
    /// </summary>
    Task<Boolean> BancoElimina(EBanco banco);
    /// <summary>
    /// Exporta datos a Excel.
    /// </summary>
    Task<string> BancoExporta(EBancoFiltro bancoFiltro);
    /// <summary>
    /// Reglas de negocio de la entidad Banco.
    /// </summary>
    Task<List<MEReglaNeg>> BancoReglas();
    #endregion

    #endregion
}
