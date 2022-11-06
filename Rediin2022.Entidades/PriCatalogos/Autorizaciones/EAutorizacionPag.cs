using DSMetodNetX.Entidades;
using System;
using System.Collections.Generic;

namespace Rediin2022.Entidades.PriCatalogos
{
    [Serializable]
    public class EAutorizacionPag : MEPagina
    {
        #region Propiedades
        public List<EAutorizacion> Pagina { get; set; }
        #endregion
    }
}
