using DSEntityNetX.DataAccess;
using DSMetodNetX.Entidades;
using System;

namespace Rediin2022.Entidades.PriOperacion
{
    [Serializable]
    public class EConExpedienteObjeto: MEntidad
    {
        #region Propiedades
        //Columnas principales
        [XMain] public Int64 ExpedienteId { get; set; } = 0L;
        [XMain] public Int64 ExpedienteObjetoId { get; set; } = 0L;
        [XMain] public Int64 ProcesoOperativoObjetoId { get; set; } = 0L;
        [XMain] public String ArchivoNombre { get; set; } = String.Empty;
        [XMain] public String Ruta { get; set; } = String.Empty;
        [XMain] public Boolean Activo { get; set; } = false;

        //Columnas vista
        public String ProcesoOperativoObjetoNombre { get; set; } = String.Empty;
        public Int32 Cantidad { get; set; } = 0;
        public Int16 Orden { get; set; } = 0;

        //Columnas calculadas
        public String ArchivoVencido { get; set; } = String.Empty;
        public String Archivo { get; set; } = String.Empty;
        public String NombreArchivo { get; set; } = String.Empty;
        #endregion
    }
}
