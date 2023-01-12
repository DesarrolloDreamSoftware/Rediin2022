using DSMetodNetX.Entidades;
using System;
using System.Collections.Generic;

namespace Rediin2022.Entidades.PriCatalogos
{
    /// <summary>
    /// Entidad de pagina.
    /// </summary>
    [Serializable]
    public class ESapSociedadPag : MEPagina
    {
        #region Propiedades
        public List<ESapSociedad> Pagina { get; set; }
        #endregion
    }
}
