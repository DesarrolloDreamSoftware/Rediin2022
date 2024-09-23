using DSMetodNetX.Aplicacion;
using DSMetodNetX.Entidades;
using Rediin2022.Entidades.PriCatalogos;
using Rediin2022.Entidades.PriClientes;
using Rediin2022.Entidades.PriOperacion;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rediin2022.Aplicacion.PriOperacion
{
    [Serializable]
    public class EVConExpedientes
    {
        #region Propiedades
        public MAccionesGen Accion { get; set; } = MAccionesGen.Ninguna;

        //Enc
        public MEVSFCon<EConExpProcOperativo, EConExpProcOperativoPag, EConExpProcOperativoFiltro> ConExpProcOperativo { get; set; } = new();
        //public EConExpProcOperativoPag ConExpProcOperativoPag { get; set; } = null;
        //public String ConExpProcOperativoColOrden { get; set; } = String.Empty;
        //public EConExpProcOperativoFiltro ConExpProcOperativoFiltro { get; set; } = new EConExpProcOperativoFiltro();
        //public Int32 ConExpProcOperativoIndice { get; set; } = 0;
        //public EConExpProcOperativo ConExpProcOperativoSel { get; set; } = null;

        //Exp
        public MEVSF<EConExpediente, EConExpedientePag, EConExpedienteFiltro> ConExpediente { get; set; } = new();
        //public EConExpedientePag ConExpedientePag { get; set; } = null;
        //public String ConExpedienteColOrden { get; set; } = String.Empty;
        //public EConExpedienteFiltro ConExpedienteFiltro { get; set; } = new EConExpedienteFiltro();
        //public Int32 ConExpedienteIndice { get; set; } = 0;
        //public EConExpediente ConExpedienteSel { get; set; } = null;
        //public List<MEReglaNeg> ConExpedienteReglas { get; set; } = null;

        //Exp (Entidades adicionales)
        public List<EProcesoOperativoCol> ProcOperColumnasCon { get; set; } = null;
        public List<EProcesoOperativoCol> ProcOperColumnasCap { get; set; } = null;
        public String ConExpedienteSelColGrupoId { get; set; } = String.Empty;

        //Objs
        public MEVSF<EConExpedienteObjeto, EConExpedienteObjetoPag, EConExpedienteObjetoFiltro> ConExpedienteObjeto { get; set; } = new();
        //public EConExpedienteObjetoPag ConExpedienteObjetoPag { get; set; } = null;
        //public String ConExpedienteObjetoColOrden { get; set; } = String.Empty;
        //public EConExpedienteObjetoFiltro ConExpedienteObjetoFiltro { get; set; } = new EConExpedienteObjetoFiltro();
        //public Int32 ConExpedienteObjetoIndice { get; set; } = 0;
        //public EConExpedienteObjeto ConExpedienteObjetoSel { get; set; } = null;
        //public List<MEReglaNeg> ConExpedienteObjetoReglas { get; set; } = null;

        //ExpedienteEstatu (ExpeEsta)
        public MEVSFCon<EExpedienteEstatu, EExpedienteEstatuPag, EExpedienteEstatuFiltro> ExpedienteEstatu { get; set; } = new();
        //public EExpedienteEstatuPag ExpedienteEstatuPag { get; set; } = null;
        //public String ExpedienteEstatuColOrden { get; set; } = String.Empty;
        //public EExpedienteEstatuFiltro ExpedienteEstatuFiltro { get; set; } = new EExpedienteEstatuFiltro();
        //public Int32 ExpedienteEstatuIndice { get; set; } = 0;
        //public EExpedienteEstatu ExpedienteEstatuSel { get; set; } = null;

        public EProcesoOperativoCol ColumnaIdPais { get; set; } = null;
        public EProcesoOperativoCol ColumnaIdEstado { get; set; } = null;
        public EProcesoOperativoCol ColumnaIdMunicipio { get; set; } = null;
        public EProcesoOperativoCol ColumnaIdColonias { get; set; } = null;

        public EVConExpedientesMontePio MontePio { get; set; } = null;
        public EVConExpedientesMedix Medix { get; set; } = null;
        #endregion
    }
}
