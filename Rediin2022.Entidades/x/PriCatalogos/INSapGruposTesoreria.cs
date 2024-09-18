using DSMetodNetX.Entidades;

using System;
using System.Collections.Generic;

namespace Rediin2022.Entidades.PriCatalogos
{
    /// <summary>
    /// Interfaz de negocio.
    /// </summary>
    public interface INSapGruposTesoreria : IMCtrMensajes
    {
        #region Funciones

        #region SapGrupoTesoreria (SapGruposTesoreria)
        /// <summary>
        /// Consulta paginada de la entidad SapGrupoTesoreria.
        /// </summary>
        ESapGrupoTesoreriaPag SapGrupoTesoreriaPag(ESapGrupoTesoreriaFiltro sapGrupoTesoreriaFiltro);
        /// <summary>
        /// Consulta por id de la entidad SapGrupoTesoreria.
        /// </summary>
        ESapGrupoTesoreria SapGrupoTesoreriaXId(String sapGrupoTesoreriaId);
        /// <summary>
        /// Consulta para combos de la entidad SapGrupoTesoreria.
        /// </summary>
        List<MEElemento> SapGrupoTesoreriaCmb();
        /// <summary>
        /// Permite insertar la entidad SapGrupoTesoreria.
        /// </summary>
        Boolean SapGrupoTesoreriaInserta(ESapGrupoTesoreria sapGrupoTesoreria);
        /// <summary>
        /// Permite actualizar la entidad SapGrupoTesoreria.
        /// </summary>
        Boolean SapGrupoTesoreriaActualiza(ESapGrupoTesoreria sapGrupoTesoreria);
        /// <summary>
        /// Permite eliminar la entidad SapGrupoTesoreria.
        /// </summary>
        Boolean SapGrupoTesoreriaElimina(ESapGrupoTesoreria sapGrupoTesoreria);
        /// <summary>
        /// Exporta datos a Excel.
        /// </summary>
        MEDatosArchivo SapGrupoTesoreriaExporta(ESapGrupoTesoreriaFiltro sapGrupoTesoreriaFiltro);
        /// <summary>
        /// Reglas de negocio de la entidad SapGrupoTesoreria.
        /// </summary>
        List<MEReglaNeg> SapGrupoTesoreriaReglas();
        #endregion

        #endregion
    }
}
