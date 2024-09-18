
using DSMetodNetX.Entidades;
using System;

namespace Rediin2022.Entidades.PriClientes
{
    [Serializable]
    public class EExpedienteObjeto
    {
        #region Propiedades
        //Columnas principales
        public long ExpedienteId { get; set; } = 0L;
        public long ExpedienteObjetoId { get; set; } = 0L;
        public long ProcesoOperativoObjetoId { get; set; } = 0L;
        public string ArchivoNombre { get; set; } = string.Empty;
        public bool Activo { get; set; } = false;
        /// <summary>
        /// Este valor no se requiere recibir en el api, se usa internamente
        /// </summary>
        public string Ruta { get; set; } = string.Empty;
        public byte[] Archivo { get; set; }
        public Boolean Eliminar { get; set; } = false;
        #endregion
    }
}
