using DSMetodNetX.Entidades;
using System;
using System.Collections.Generic;

namespace Rediin2022.Entidades.PriCatalogos
{
    [Serializable]
    public class EProcesoOperativoEstSecPag : MEPagina
    {
        #region Propiedades
        public List<EProcesoOperativoEstSec> Pagina { get; set; }
        #endregion
    }
}
