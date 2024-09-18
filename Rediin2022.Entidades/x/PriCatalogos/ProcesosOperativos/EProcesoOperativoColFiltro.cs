
using DSMetodNetX.Entidades;
using System;
using System.Collections.Generic;

namespace Rediin2022.Entidades.PriCatalogos
{
    [Serializable]
    public class EProcesoOperativoColFiltro : MEFiltro
    {
        #region Propiedades
        public Int64 ProcesoOperativoId { get; set; } = 0L; //[Llave padre]

        //Columnas principales
        public String FilEtiqueta { get; set; } = String.Empty;
        public TiposColumna FilTipo { get; set; } = TiposColumna.Ninguno;
        public Boolean FiltraFilActivo { get; set; } = false;
        public Boolean FilActivo { get; set; } = false;
        #endregion

    }
}
