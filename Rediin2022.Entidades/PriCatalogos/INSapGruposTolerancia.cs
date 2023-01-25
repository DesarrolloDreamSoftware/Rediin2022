using DSMetodNetX.Entidades;
using DSMetodNetX.Negocio;
using System;
using System.Collections.Generic;

namespace Rediin2022.Entidades.PriCatalogos
{
    /// <summary>
    /// Interfaz de negocio.
    /// </summary>
    public interface INSapGruposTolerancia : IMCtrMensajes
    {
        #region Funciones

        #region SapGrupoTolerancia (SapGruposTolerancia)
        /// <summary>
        /// Consulta paginada de la entidad SapGrupoTolerancia.
        /// </summary>
        ESapGrupoToleranciaPag SapGrupoToleranciaPag(ESapGrupoToleranciaFiltro sapGrupoToleranciaFiltro);
        /// <summary>
        /// Consulta por id de la entidad SapGrupoTolerancia.
        /// </summary>
        ESapGrupoTolerancia SapGrupoToleranciaXId(String sapGrupoToleranciaId);
        /// <summary>
        /// Consulta para combos de la entidad SapGrupoTolerancia.
        /// </summary>
        List<MEElemento> SapGrupoToleranciaCmb();
        /// <summary>
        /// Permite insertar la entidad SapGrupoTolerancia.
        /// </summary>
        Boolean SapGrupoToleranciaInserta(ESapGrupoTolerancia sapGrupoTolerancia);
        /// <summary>
        /// Permite actualizar la entidad SapGrupoTolerancia.
        /// </summary>
        Boolean SapGrupoToleranciaActualiza(ESapGrupoTolerancia sapGrupoTolerancia);
        /// <summary>
        /// Permite eliminar la entidad SapGrupoTolerancia.
        /// </summary>
        Boolean SapGrupoToleranciaElimina(ESapGrupoTolerancia sapGrupoTolerancia);
        /// <summary>
        /// Exporta datos a Excel.
        /// </summary>
        MEDatosArchivo SapGrupoToleranciaExporta(ESapGrupoToleranciaFiltro sapGrupoToleranciaFiltro);
        /// <summary>
        /// Reglas de negocio de la entidad SapGrupoTolerancia.
        /// </summary>
        List<MEReglaNeg> SapGrupoToleranciaReglas();
        #endregion

        #endregion
    }
}
