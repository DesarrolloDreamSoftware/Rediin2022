using DSMetodNetX.Entidades;
using System;
using System.Collections.Generic;

namespace Rediin2022.Entidades.PriCatalogos
{
    [Serializable]
    public class EProcesoOperativoColPag : MEPagina
    {
        #region Propiedades
        public List<EProcesoOperativoCol> Pagina { get; set; }
        #endregion
    }
}
