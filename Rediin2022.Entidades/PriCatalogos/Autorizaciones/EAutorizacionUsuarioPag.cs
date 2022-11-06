using DSMetodNetX.Entidades;
using System;
using System.Collections.Generic;

namespace Rediin2022.Entidades.PriCatalogos
{
    [Serializable]
    public class EAutorizacionUsuarioPag : MEPagina
    {
        #region Propiedades
        public List<EAutorizacionUsuario> Pagina { get; set; }
        #endregion
    }
}
