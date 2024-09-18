
using DSMetodNetX.Entidades;
using System;
using System.Collections.Generic;

namespace Rediin2022.Entidades.PriCatalogos
{
    [Serializable]
    public class EProcesoOperativoFiltro : MEFiltro
    {
        #region Propiedades
        //Columnas principales
        public String FilProcesoOperativoNombre { get; set; } = String.Empty;
        public Boolean FiltraFilActivo { get; set; } = false;
        public Boolean FilActivo { get; set; } = false;
        #endregion
    }
}
