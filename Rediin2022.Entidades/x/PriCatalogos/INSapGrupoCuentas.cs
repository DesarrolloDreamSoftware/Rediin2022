using DSMetodNetX.Entidades;

using System;
using System.Collections.Generic;

namespace Rediin2022.Entidades.PriCatalogos
{
    /// <summary>
    /// Interfaz de negocio.
    /// </summary>
    public interface INSapGrupoCuentas : IMCtrMensajes
    {
        #region Funciones

        #region SapGrupoCuenta (SapGrupoCuentas)
        /// <summary>
        /// Consulta paginada de la entidad SapGrupoCuenta.
        /// </summary>
        ESapGrupoCuentaPag SapGrupoCuentaPag(ESapGrupoCuentaFiltro sapGrupoCuentaFiltro);
        /// <summary>
        /// Consulta por id de la entidad SapGrupoCuenta.
        /// </summary>
        ESapGrupoCuenta SapGrupoCuentaXId(String sapGrupoCuentaId);
        /// <summary>
        /// Consulta para combos de la entidad SapGrupoCuenta.
        /// </summary>
        List<MEElemento> SapGrupoCuentaCmb();
        /// <summary>
        /// Permite insertar la entidad SapGrupoCuenta.
        /// </summary>
        Boolean SapGrupoCuentaInserta(ESapGrupoCuenta sapGrupoCuenta);
        /// <summary>
        /// Permite actualizar la entidad SapGrupoCuenta.
        /// </summary>
        Boolean SapGrupoCuentaActualiza(ESapGrupoCuenta sapGrupoCuenta);
        /// <summary>
        /// Permite eliminar la entidad SapGrupoCuenta.
        /// </summary>
        Boolean SapGrupoCuentaElimina(ESapGrupoCuenta sapGrupoCuenta);
        /// <summary>
        /// Exporta datos a Excel.
        /// </summary>
        MEDatosArchivo SapGrupoCuentaExporta(ESapGrupoCuentaFiltro sapGrupoCuentaFiltro);
        /// <summary>
        /// Reglas de negocio de la entidad SapGrupoCuenta.
        /// </summary>
        List<MEReglaNeg> SapGrupoCuentaReglas();
        #endregion

        #endregion
    }
}
