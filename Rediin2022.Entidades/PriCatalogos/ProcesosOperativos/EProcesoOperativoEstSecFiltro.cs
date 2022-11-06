using DSEntityNetX.DataAccess;
using DSMetodNetX.Entidades;
using System;
using System.Collections.Generic;

namespace Rediin2022.Entidades.PriCatalogos
{
    [Serializable]
    public class EProcesoOperativoEstSecFiltro : MEFiltro
    {
        #region Propiedades
        public Int64 ProcesoOperativoEstId { get; set; } = 0L; //[Llave padre]
        #endregion
    }
}
