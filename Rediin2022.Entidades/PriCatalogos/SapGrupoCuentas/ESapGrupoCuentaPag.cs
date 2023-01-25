using DSMetodNetX.Entidades;
using System;
using System.Collections.Generic;

namespace Rediin2022.Entidades.PriCatalogos
{
    /// <summary>
    /// Entidad de pagina.
    /// </summary>
    [Serializable]
    public class ESapGrupoCuentaPag : MEPagina
    {
        #region Propiedades
        public List<ESapGrupoCuenta> Pagina { get; set; }
        #endregion
    }
}
