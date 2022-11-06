using DSMetodNetX.Entidades;
using System;
using System.Collections.Generic;

namespace Rediin2022.Entidades.PriCatalogos
{
    /// <summary>
    /// Entidad de pagina.
    /// </summary>
    [Serializable]
    public class EIdentificacionPag : MEPagina
    {
        #region Propiedades
        public List<EIdentificacion> Pagina { get; set; }
        #endregion
    }
}
