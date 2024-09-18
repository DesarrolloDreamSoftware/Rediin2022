using DSMetodNetX.Entidades;
using System;
using System.Collections.Generic;

namespace Rediin2022.Entidades.PriCatalogos
{
    public interface INCatalogos : IMCtrMensajes
    {
        #region Funciones
        /// <summary>
        /// Consulta de combo para la tabla NCProcesosOperativosEst.
        /// </summary>
        List<MEElemento> ProcesoOperativoEstCmb(Int64 procesoOperativoId);
        #endregion
    }
}