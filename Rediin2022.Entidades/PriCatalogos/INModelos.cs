using DSMetodNetX.Entidades;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rediin2022.Entidades.PriCatalogos;

/// <summary>
/// Interfaz de negocio.
/// </summary>
public interface INModelos : IMCtrMensajes
{
    #region Funciones

    #region Modelo (Modelos)
    /// <summary>
    /// Consulta paginada de la entidad Modelo.
    /// </summary>
    Task<EModeloPag> ModeloPag(EModeloFiltro modeloFiltro);
    /// <summary>
    /// Consulta por id de la entidad Modelo.
    /// </summary>
    Task<EModelo> ModeloXId(Int64 modeloId);
    /// <summary>
    /// Consulta para combos de la entidad Modelo.
    /// </summary>
    Task<List<MEElemento>> ModeloCmb();
    /// <summary>
    /// Permite insertar la entidad Modelo.
    /// </summary>
    Task<Boolean> ModeloInserta(EModelo modelo);
    /// <summary>
    /// Permite actualizar la entidad Modelo.
    /// </summary>
    Task<Boolean> ModeloActualiza(EModelo modelo);
    /// <summary>
    /// Permite eliminar la entidad Modelo.
    /// </summary>
    Task<Boolean> ModeloElimina(EModelo modelo);
    /// <summary>
    /// Reglas de negocio de la entidad Modelo.
    /// </summary>
    Task<List<MEReglaNeg>> ModeloReglas();
    #endregion

    #endregion
}
