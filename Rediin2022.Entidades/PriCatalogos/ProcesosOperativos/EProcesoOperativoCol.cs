using DSEntityNetX.DataAccess;
using DSMetodNetX.Entidades;
using System;
using System.Collections.Generic;

namespace Rediin2022.Entidades.PriCatalogos
{
    [Serializable]
    public class EProcesoOperativoCol: MEntidad
    {
        #region Propiedades
        //Columnas principales
        [XMain] public Int64 ProcesoOperativoId { get; set; } = 0L;
        [XMain] public Int64 ColumnaId { get; set; } = 0L;
        [XMain] public String Etiqueta { get; set; } = String.Empty;
        [XMain] public TiposColumna Tipo { get; set; } = TiposColumna.Ninguno;
        [XMain] public Int16 Decimales { get; set; } = 0;
        [XMain] public Int32 ConOrden { get; set; } = 0;
        [XMain] public Boolean ConOrdenar { get; set; } = false;
        [XMain] public Int32 ConLongitud { get; set; } = 0;
        [XMain] public String CapTab { get; set; } = String.Empty;
        [XMain] public Int32 CapOrden { get; set; } = 0;
        [XMain] public Int32 CapColumnas { get; set; } = 0;
        [XMain] public Int32 CapColumnasVacias { get; set; } = 0;
        [XMain] public Boolean CapObligatorio { get; set; } = false;
        [XMain] public String CapRangoIni { get; set; } = String.Empty;
        [XMain] public String CapRangoFin { get; set; } = String.Empty;
		[XMain(true)] public Int64 CapCmbProcesoOperativoId { get; set; } = 0L;
		[XMain(true)] public Int64 CapCmbIdColumnaId { get; set; } = 0L;
		[XMain(true)] public Int64 CapCmbTextoColumnaId { get; set; } = 0L;
		[XMain] public Boolean Activo { get; set; } = false;

        public List<MEElemento> ElementosCmb { get; set; } = null;
        #endregion
    }
}
