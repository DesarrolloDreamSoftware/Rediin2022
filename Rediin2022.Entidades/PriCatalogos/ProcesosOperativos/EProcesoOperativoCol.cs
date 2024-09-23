
using DSEntityNetX.Entities.DropDownList;
using DSMetodNetX.Entidades;
using System;
using System.Collections.Generic;

namespace Rediin2022.Entidades.PriCatalogos
{
    [Serializable]
	[MDAErrorDuplicado(-1, nameof(Propiedad))]
	[MDAErrorDuplicado(-2, nameof(Etiqueta))]
    public class EProcesoOperativoCol: MEntidad
    {
        #region Propiedades
        //Columnas principales
        [MDAMain] public Int64 ProcesoOperativoId { get; set; } = 0L;
        [MDAMain] public Int64 ColumnaId { get; set; } = 0L;
		[MDAMain] public String Propiedad { get; set; } = String.Empty;
		[MDAMain] public String Etiqueta { get; set; } = String.Empty;
        [MDAMain] public TiposColumna Tipo { get; set; } = TiposColumna.Ninguno;
        [MDAMain] public Int16 Decimales { get; set; } = 0;
        [MDAMain] public Int32 ConOrden { get; set; } = 0;
        [MDAMain] public Boolean ConOrdenar { get; set; } = false;
        [MDAMain] public Int32 ConLongitud { get; set; } = 0;
        [MDAMain] public String CapTab { get; set; } = String.Empty;
        [MDAMain] public Int32 CapOrden { get; set; } = 0;
        [MDAMain] public Int32 CapColumnas { get; set; } = 0;
        [MDAMain] public Int32 CapColumnasVacias { get; set; } = 0;
        [MDAMain] public Boolean CapObligatorio { get; set; } = false;
        [MDAMain] public String CapRangoIni { get; set; } = String.Empty;
        [MDAMain] public String CapRangoFin { get; set; } = String.Empty;
		[MDAMain(true)] public Int64 CapCmbProcesoOperativoId { get; set; } = 0L;
		[MDAMain(true)] public Int64 CapCmbIdColumnaId { get; set; } = 0L;
		[MDAMain(true)] public Int64 CapCmbTextoColumnaId { get; set; } = 0L;
        [MDAMain(true)] public Combos ComboId { get; set; } = Combos.Ninguno;
        [MDAMain] public Boolean Activo { get; set; } = false;

        /// <summary>
        /// Para presentacion
        /// </summary>
        public List<MEElemento> ElementosCmb { get; set; } = null;
        #endregion
    }
}
