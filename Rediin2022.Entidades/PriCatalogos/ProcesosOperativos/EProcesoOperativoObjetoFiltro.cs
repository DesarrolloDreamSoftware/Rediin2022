
using DSMetodNetX.Entidades;
using System;
using System.Collections.Generic;

namespace Rediin2022.Entidades.PriCatalogos
{
    [Serializable]
    public class EProcesoOperativoObjetoFiltro : MEFiltro
    {
        #region Propiedades
        public Int64 ProcesoOperativoId { get; set; } = 0L; //[Llave padre]

        //Columnas principales
        public String FilProcesoOperativoObjetoNombre { get; set; } = String.Empty;
        public TipoCaptura FilTipoCapturaId { get; set; } = TipoCaptura.Ninguno;
        public Boolean FiltraFilActivo { get; set; } = false;
        public Boolean FilActivo { get; set; } = false;
        #endregion
    }
}
