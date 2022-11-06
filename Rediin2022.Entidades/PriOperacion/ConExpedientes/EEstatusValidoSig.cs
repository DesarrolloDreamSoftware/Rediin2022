using DSEntityNetX.DataAccess;
using DSMetodNetX.Entidades;
using System;
using System.Collections.Generic;

namespace Rediin2022.Entidades.PriOperacion
{
    //Adi
    [Serializable]
    public class EEstatusValidoSig 
    {
        #region Propiedades
        public Int64 ProcesoOperativoEstIdSig { get; set; } = 0L;
        public String EstatusNombre { get; set; } = String.Empty;
        #endregion
    }
}
