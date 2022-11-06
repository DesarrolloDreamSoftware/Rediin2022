using DSMetodNetX.Entidades;
using System;
using System.Collections.Generic;

namespace Rediin2022.Entidades.PriOperacion
{
    [Serializable]
    public class EConExpedientePag : MEPagina
    {
        #region Propiedades
        public List<EConExpediente> Pagina { get; set; }
        #endregion
    }
}
