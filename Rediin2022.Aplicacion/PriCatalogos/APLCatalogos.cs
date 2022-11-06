using DSMetodNetX.Aplicacion;
using DSMetodNetX.Entidades;
using DSMetodNetX.Negocio;
using Rediin2022.Entidades.PriCatalogos;
using System;
using System.Collections.Generic;

namespace Rediin2022.Aplicacion.PriCatalogos
{
    public class APLCatalogos : MAplicacion, INCatalogos
    {
        #region Constructores
        public APLCatalogos(IMApiCliente api) : base(api)
        {
        }
        #endregion

        #region Funciones
        /// <summary>
        /// Consulta de combo para la tabla NCProcesosOperativosEst.
        /// </summary>
        public List<MEElemento> ProcesoOperativoEstCmb(Int64 procesoOperativoId)
        {
            return Call<List<MEElemento>>(NomFn(),
                                          procesoOperativoId);
        }
        #endregion
    }
}