using DSMetodNetX.Entidades;
using System;
using System.Collections.Generic;

namespace Rediin2022.Entidades.PriOperacion
{
    [Serializable]
    public class EExpedienteEstatuPag : MEPagina
    {
        #region Propiedades
        public List<EExpedienteEstatu> Pagina { get; set; }
        #endregion
    }
}
