using DSMetodNetX.Entidades;
using System;
using System.Collections.Generic;

namespace Rediin2022.Entidades.PriCatalogos
{
    [Serializable]
    public class EProcesoOperativoEstPag : MEPagina
    {
        #region Propiedades
        public List<EProcesoOperativoEst> Pagina { get; set; }
        #endregion
    }
}
