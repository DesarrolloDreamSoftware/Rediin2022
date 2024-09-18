using Rediin2022.Entidades.Idioma;
using Rediin2022.Entidades.PriCatalogos;
using DSEntityNetX.DataAccess;
using DSMetodNetX.AccesoDatos;
using DSMetodNetX.Entidades;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DSMetodNetX.Comun;

namespace Rediin2022.AccesoDatos.PriCatalogos
{
    [Serializable]
    public class RCatalogos : MRepositorio
    {
        #region Variables
        /// <summary>
        /// Conexión.
        /// </summary>
        private IMConexionEntidad _conexion;
        #endregion

        #region Constructores
        public RCatalogos(IMConexionEntidad conexion)
            : base(conexion)
        {
            _conexion = conexion;
        }
        #endregion

        #region Funciones
        /// <summary>
        /// Consulta de combo para la tabla NCProcesosOperativosEst.
        /// </summary>
        public async Task<List<MEElemento>> ProcesoOperativoEstCmb(Int64 procesoOperativoId)
        {
            _conexion.AddParamInOpt(procesoOperativoId);
            return await _conexion.EntidadCmbAsync("NCProcesosOperativosEstCCmb1");
        }
        #endregion

    }
}