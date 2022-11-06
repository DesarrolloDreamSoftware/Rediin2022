using DSEntityNetX.DataAccess;
using DSMetodNetX.Entidades;
using System;
using System.Collections.Generic;

namespace Rediin2022.Entidades.PriOperacion
{
    [Serializable]
    public class EConExpedienteFiltro : MEFiltro
    {
        #region Propiedades
        public Int64 ProcesoOperativoId { get; set; } = 0L; //[Llave padre]

        //Columnas principales
        public Int64 FilExpedienteId { get; set; } = 0L;
        public Int64 FilProcesoOperativoEstId { get; set; } = 0L;

        [XExclude]
        public Boolean ControlEstatus { get; set; } = false; //[Para visible si]
        #endregion
    }
}
