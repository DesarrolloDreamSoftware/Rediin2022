
using DSMetodNetX.Entidades;
using System;
using System.Collections.Generic;

namespace Rediin2022.Entidades.PriCatalogos
{
    /// <summary>
    /// Entidad de filtro.
    /// </summary>
    [Serializable]
    public class ESapGrupoCuentaFiltro : MEFiltro
    {
        #region Propiedades
        //Columnas principales
        public String FilSapGrupoCuentaNombre { get; set; } = String.Empty;
        public Boolean FiltraFilActivo { get; set; } = false;
        public Boolean FilActivo { get; set; } = false;

        [MDAExclude]
        public Dictionary<String, String> Columnas { get; set; } = null; //[Para exportacion a excel]
        #endregion
    }
}
