using DSEntityNetX.DataAccess;
using DSMetodNetX.Entidades;
using System;

namespace Rediin2022.Entidades.PriCatalogos
{
    [Serializable]
    public class EProcesoOperativo: MEntidad
    {
        #region Propiedades
        //Columnas principales
        [XMain] public Int64 EstablecimientoId { get; set; } = 0L;
        [XMain] public Int64 ProcesoOperativoId { get; set; } = 0L;
        [XMain] public String ProcesoOperativoNombre { get; set; } = String.Empty;
        [XMain] public Int16 Orden { get; set; } = 0;
        [XMain] public Boolean ControlEstatus { get; set; } = false;
        [XMain] public EsquemaObjetos EsquemaObjetos { get; set; } = EsquemaObjetos.Ninguno;
        [XMain] public Boolean Activo { get; set; } = false;

        //Columnas vista
        public String EstablecimientoNombre { get; set; } = String.Empty;

        //Columnas calculadas
        public Boolean TieneExpedientes { get; set; } = false;

        //Columnas permite (acciones por registro)
        public Boolean PermiteElimina { get; set; } = false;
        public Boolean PermiteIrAProcesoOperativoEst { get; set; } = false;
        #endregion
    }
}
