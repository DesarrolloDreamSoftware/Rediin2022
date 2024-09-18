using Rediin2022.Entidades.Idioma;
using Rediin2022.Entidades.PriCatalogos;
using DSEntityNetX.DataAccess;
using DSMetodNetX.AccesoDatos;
using DSMetodNetX.Entidades;
using DSMetodNetX.Idioma;
using System;
using System.Collections.Generic;

namespace Rediin2022.AccesoDatos.PriCatalogos
{
    [Serializable]
    public class RCatalogos : MRepositorio
    {
        #region Constructores
        public RCatalogos(IMConexionEntidad conexion)
            : base(conexion)
        {
        }
        #endregion

        #region Funciones
        /// <summary>
        /// Consulta de combo para la tabla NCProcesosOperativosEst.
        /// </summary>
        public List<MEElemento> ProcesoOperativoEstCmb(Int64 procesoOperativoId)
        {
            _conexion.AddParamInOpt(nameof(procesoOperativoId), procesoOperativoId);
            return _conexion.LoadCmb<MEElemento>("NCProcesosOperativosEstCCmb1");
        }
        #endregion

    }
}