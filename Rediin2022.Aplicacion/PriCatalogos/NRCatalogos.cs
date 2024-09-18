using DSMetodNetX.Aplicacion;
using DSMetodNetX.Entidades;

using Rediin2022.Entidades.PriCatalogos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rediin2022.Aplicacion.PriCatalogos
{
    public class NRCatalogos : MNegRemoto, INCatalogos
    {
        #region Constructores
        public NRCatalogos(IMApiCteNeg api) : base(api)
        {
        }
        #endregion

        #region Funciones
        /// <summary>
        /// Consulta de combo para la tabla NCProcesosOperativosEst.
        /// </summary>
        public async Task<List<MEElemento>> ProcesoOperativoEstCmb(Int64 procesoOperativoId)
        {
            return await CallAsync<List<MEElemento>>(NomFn(),
                                                     procesoOperativoId);
        }
        #endregion
    }
}