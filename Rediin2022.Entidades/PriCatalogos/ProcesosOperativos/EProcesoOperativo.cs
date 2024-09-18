
using DSMetodNetX.Entidades;
using System;

namespace Rediin2022.Entidades.PriCatalogos
{
    [Serializable]
    [MDAErrorDuplicado(-1, nameof(ProcesoOperativoNombre))]
    [MDAErrorDuplicado(-2, nameof(Orden))]
    public class EProcesoOperativo: MEntidad
    {
        #region Propiedades
        //Columnas principales
        [MDAMain] public Int64 EstablecimientoId { get; set; } = 0L;
        [MDAMain] public Int64 ProcesoOperativoId { get; set; } = 0L;
        [MDAMain] public String ProcesoOperativoNombre { get; set; } = String.Empty;
        [MDAMain] public Int16 Orden { get; set; } = 0;
        [MDAMain] public Boolean ControlEstatus { get; set; } = false;
        [MDAMain] public EsquemaObjetos EsquemaObjetos { get; set; } = EsquemaObjetos.Ninguno;
		[MDAMain] public Boolean Activo { get; set; } = false;

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
