using DSMetodNetX.Entidades;
using System;
using System.Collections.Generic;

namespace Rediin2022.Entidades.PriOperacion
{
    [Serializable]
    public class EConExpProcOperativoPag : MEPagina
    {
        #region Propiedades
        public List<EConExpProcOperativo> Pagina { get; set; }
        #endregion
    }
}
