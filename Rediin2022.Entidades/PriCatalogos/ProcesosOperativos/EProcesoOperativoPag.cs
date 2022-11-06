using DSMetodNetX.Entidades;
using System;
using System.Collections.Generic;

namespace Rediin2022.Entidades.PriCatalogos
{
    [Serializable]
    public class EProcesoOperativoPag : MEPagina
    {
        #region Propiedades
        public List<EProcesoOperativo> Pagina { get; set; }
        #endregion
    }
}
