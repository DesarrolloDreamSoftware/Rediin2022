using DSMetodNetX.Entidades;
using System;
using System.Collections.Generic;

namespace Rediin2022.Entidades.PriCatalogos
{
    /// <summary>
    /// Entidad de pagina.
    /// </summary>
    [Serializable]
    public class ESapTratamientoPag : MEPagina
    {
        #region Propiedades
        public List<ESapTratamiento> Pagina { get; set; }
        #endregion
    }
}
