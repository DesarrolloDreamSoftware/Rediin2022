using DSEntityNetX.DataAccess;
using DSMetodNetX.Entidades;
using System;

namespace Rediin2022.Entidades.PriClientes
{
    [Serializable]
    public class EExpedienteObjeto
    {
        #region Propiedades
        //Columnas principales
        public Int64 ExpedienteId { get; set; } = 0L;
        public Int64 ExpedienteObjetoId { get; set; } = 0L;
        public Int64 ProcesoOperativoObjetoId { get; set; } = 0L;
        public String ArchivoNombre { get; set; } = String.Empty;
        public Boolean Activo { get; set; } = false;
        /// <summary>
        /// Este valor no se requiere recibir en el api, se usa internamente
        /// </summary>
        public String Ruta { get; set; } = String.Empty;
        public Byte[] Archivo { get; set; }
        #endregion
    }
}
