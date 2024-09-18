
using DSMetodNetX.Entidades;
using System;
using System.Collections.Generic;

namespace Rediin2022.Entidades.PriCatalogos
{
    [Serializable]
    public class EAutorizacionUsuarioFiltro : MEFiltro
    {
        #region Propiedades
        public Int64 AutorizacionId { get; set; } = 0L; //[Llave padre]

        //Columnas principales
        public Int64 FilProcesoOperativoEstId { get; set; } = 0L;

        //Columnas vista
        public String FilNombre { get; set; } = String.Empty;
        #endregion
    }
}
