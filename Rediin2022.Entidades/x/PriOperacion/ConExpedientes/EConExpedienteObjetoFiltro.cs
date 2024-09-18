
using DSMetodNetX.Entidades;
using System;
using System.Collections.Generic;

namespace Rediin2022.Entidades.PriOperacion
{
    [Serializable]
    public class EConExpedienteObjetoFiltro : MEFiltro
    {
        #region Propiedades
        public Int64 ExpedienteId { get; set; } = 0L; //[Llave padre]

        //Columnas principales
        public String FilArchivoNombre { get; set; } = String.Empty;

        //Columnas vista
        public String FilProcesoOperativoObjetoNombre { get; set; } = String.Empty;
        #endregion
    }
}
