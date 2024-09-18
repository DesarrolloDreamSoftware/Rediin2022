using DSMetodNetX.Aplicacion;
using DSMetodNetX.Entidades;
using DSMetodNetX.Negocio;
using Rediin2022.Entidades.PriCatalogos;
using System;
using System.Collections.Generic;

namespace Rediin2022.Aplicacion.PriCatalogos
{
	[Serializable]
	public class EVProcesosOperativos
	{
		#region Propiedades
		public MAccionesGen Accion { get; set; } = MAccionesGen.Ninguna;

		//ProcesosOperativos
		public EProcesoOperativoPag ProcesoOperativoPag { get; set; } = null;
		public String ProcesoOperativoColOrden { get; set; } = String.Empty;
		public EProcesoOperativoFiltro ProcesoOperativoFiltro { get; set; } = new EProcesoOperativoFiltro();
		public Int32 ProcesoOperativoIndice { get; set; } = 0;
		public EProcesoOperativo ProcesoOperativoSel { get; set; } = null;
		public List<MEReglaNeg> ProcesoOperativoReglas { get; set; } = null;

		//ProcesosOperativosCols
		public EProcesoOperativoColPag ProcesoOperativoColPag { get; set; } = null;
		public String ProcesoOperativoColColOrden { get; set; } = String.Empty;
		public EProcesoOperativoColFiltro ProcesoOperativoColFiltro { get; set; } = new EProcesoOperativoColFiltro();
		public Int32 ProcesoOperativoColIndice { get; set; } = 0;
		public EProcesoOperativoCol ProcesoOperativoColSel { get; set; } = null;
		public List<MEReglaNeg> ProcesoOperativoColReglas { get; set; } = null;

		//ProcesosOperativosObjetos
		public EProcesoOperativoObjetoPag ProcesoOperativoObjetoPag { get; set; } = null;
		public String ProcesoOperativoObjetoColOrden { get; set; } = String.Empty;
		public EProcesoOperativoObjetoFiltro ProcesoOperativoObjetoFiltro { get; set; } = new EProcesoOperativoObjetoFiltro();
		public Int32 ProcesoOperativoObjetoIndice { get; set; } = 0;
		public EProcesoOperativoObjeto ProcesoOperativoObjetoSel { get; set; } = null;
		public List<MEReglaNeg> ProcesoOperativoObjetoReglas { get; set; } = null;

		//ProcesoOperativoEst (ProcesosOperativosEst)
		public EProcesoOperativoEstPag ProcesoOperativoEstPag { get; set; } = null;
		public String ProcesoOperativoEstColOrden { get; set; } = String.Empty;
		public EProcesoOperativoEstFiltro ProcesoOperativoEstFiltro { get; set; } = new EProcesoOperativoEstFiltro();
		public Int32 ProcesoOperativoEstIndice { get; set; } = 0;
		public EProcesoOperativoEst ProcesoOperativoEstSel { get; set; } = null;
		public List<MEReglaNeg> ProcesoOperativoEstReglas { get; set; } = null;

		//ProcesoOperativoEstSec (ProcesosOperativosEstSec)
		public EProcesoOperativoEstSecPag ProcesoOperativoEstSecPag { get; set; } = null;
		public String ProcesoOperativoEstSecColOrden { get; set; } = String.Empty;
		public EProcesoOperativoEstSecFiltro ProcesoOperativoEstSecFiltro { get; set; } = new EProcesoOperativoEstSecFiltro();
		public Int32 ProcesoOperativoEstSecIndice { get; set; } = 0;
		public EProcesoOperativoEstSec ProcesoOperativoEstSecSel { get; set; } = null;
		public List<MEReglaNeg> ProcesoOperativoEstSecReglas { get; set; } = null;		
		#endregion
	}
}
