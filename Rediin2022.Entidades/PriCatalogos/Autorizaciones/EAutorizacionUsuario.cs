
using DSMetodNetX.Entidades;
using System;

namespace Rediin2022.Entidades.PriCatalogos
{
    [Serializable]
    public class EAutorizacionUsuario: MEntidad
    {
        #region Propiedades
        //Columnas principales
        [MDAMain] public Int64 AutorizacionId { get; set; } = 0L;
        [MDAMain] public Int64 AutorizacionUsuarioId { get; set; } = 0L;
        [MDAMain] public Int64 ProcesoOperativoEstId { get; set; } = 0L;
        [MDAMain] public Int64 UsuarioId { get; set; } = 0L;

        //Columnas vista
        public String Nombre { get; set; } = String.Empty;
        public String ApellidoPaterno { get; set; } = String.Empty;
        public String ApellidoMaterno { get; set; } = String.Empty;
        public String EstatusNombre { get; set; } = String.Empty;
        #endregion
    }
}
