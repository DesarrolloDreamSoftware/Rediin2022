using DSMetodNetX.Entidades;
using System;
using System.Collections.Generic;

namespace Rediin2022.Entidades.PriCatalogos
{
    [Serializable]
    public class EProcesoOperativoObjetoPag : MEPagina
    {
        #region Propiedades
        public List<EProcesoOperativoObjeto> Pagina { get; set; }
        #endregion
    }
}
