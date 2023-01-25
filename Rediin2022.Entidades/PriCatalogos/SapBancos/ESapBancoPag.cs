using DSMetodNetX.Entidades;
using System;
using System.Collections.Generic;

namespace Rediin2022.Entidades.PriCatalogos
{
    /// <summary>
    /// Entidad de pagina.
    /// </summary>
    [Serializable]
    public class ESapBancoPag : MEPagina
    {
        #region Propiedades
        public List<ESapBanco> Pagina { get; set; }
        #endregion
    }
}
