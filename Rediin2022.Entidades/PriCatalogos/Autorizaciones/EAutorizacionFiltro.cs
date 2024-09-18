
using DSMetodNetX.Entidades;
using System;
using System.Collections.Generic;

namespace Rediin2022.Entidades.PriCatalogos
{
    [Serializable]
    [MDAParamSesion(MMetaDatos.establecimientoIdSesion)]
    public class EAutorizacionFiltro : MEFiltro
    {
        #region Propiedades
        //Columnas principales
        public String FilAutorizacionNombre { get; set; } = String.Empty;
        public Int64 FilProcesoOperativoId { get; set; } = 0L;
        #endregion
    }
}
