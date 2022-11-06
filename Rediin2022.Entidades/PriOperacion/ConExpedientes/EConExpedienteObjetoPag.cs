using DSMetodNetX.Entidades;
using System;
using System.Collections.Generic;

namespace Rediin2022.Entidades.PriOperacion
{
    [Serializable]
    public class EConExpedienteObjetoPag : MEPagina
    {
        #region Propiedades
        public List<EConExpedienteObjeto> Pagina { get; set; }
        #endregion
    }
}
