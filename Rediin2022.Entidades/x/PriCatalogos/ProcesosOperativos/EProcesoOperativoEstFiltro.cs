
using DSMetodNetX.Entidades;
using System;
using System.Collections.Generic;

namespace Rediin2022.Entidades.PriCatalogos
{
    [Serializable]
    public class EProcesoOperativoEstFiltro : MEFiltro
    {
        #region Propiedades
        public Int64 ProcesoOperativoId { get; set; } = 0L; //[Llave padre]
        #endregion
    }
}
