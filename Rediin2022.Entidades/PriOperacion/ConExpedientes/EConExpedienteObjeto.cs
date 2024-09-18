
using DSMetodNetX.Entidades;
using System;

namespace Rediin2022.Entidades.PriOperacion
{
    [Serializable]
    [MDAErrorDuplicado(-1, nameof(ArchivoNombre))]
    public class EConExpedienteObjeto : MEntidad
    {
        #region Propiedades
        //Columnas principales
        [MDAMain] public Int64 ExpedienteId { get; set; } = 0L;
        [MDAMain] public Int64 ExpedienteObjetoId { get; set; } = 0L;
        [MDAMain] public Int64 ProcesoOperativoObjetoId { get; set; } = 0L;
        [MDAMain] public String ArchivoNombre { get; set; } = String.Empty;
        [MDAMain] public String Ruta { get; set; } = String.Empty;
        [MDAMain] public Boolean Activo { get; set; } = false;

        //Columnas vista
        public String ProcesoOperativoObjetoNombre { get; set; } = String.Empty;
        public Int32 Cantidad { get; set; } = 0;
        public Int16 Orden { get; set; } = 0;
        public Int16 DiasVencimiento { get; set; } = 0;

        //Columnas calculadas
        public String ArchivoVencido { get; set; } = String.Empty;
        #endregion
    }
}
