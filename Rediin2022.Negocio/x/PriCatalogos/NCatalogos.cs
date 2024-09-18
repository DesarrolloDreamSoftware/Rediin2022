using Rediin2022.AccesoDatos.PriCatalogos;
using Rediin2022.Entidades.Idioma;
using Rediin2022.Entidades.PriCatalogos;
using DSEntityNetX.Business;
using DSEntityNetX.DataAccess;
using DSMetodNetX.AccesoDatos;
using DSMetodNetX.Entidades;
using DSMetodNetX.Idioma;
using DSMetodNetX.Negocio;
using System;
using System.Collections.Generic;

namespace Rediin2022.Negocio.PriCatalogos
{
    [Serializable]
    public class NCatalogos : RCatalogos, INCatalogos
    {
        #region Constructores
        public NCatalogos(IMConexionEntidad conexion)
            : base(conexion)
        {
        }
        #endregion

        #region Funciones
        #endregion
    }
}