
using DSMetodNetX.Entidades;
using System;
using System.Collections.Generic;

namespace Rediin2022.Entidades.PriOperacion
{
    [Serializable]
    [MDAParamSesion(MMetaDatos.establecimientoIdSesion)]
    public class EConExpProcOperativoFiltro : MEFiltro
    {
        #region Propiedades
        //Columnas principales
        public String FilProcesoOperativoNombre { get; set; } = String.Empty;
		#endregion
	}
}
