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

        //Exp
        public MEVSF<EConExpediente, EConExpedientePag, EConExpedienteFiltro> ConExpediente { get; set; } = new();

        //Exp (Entidades adicionales)
        public List<EProcesoOperativoCol> ProcOperColumnasCon { get; set; } = null;
        public List<EProcesoOperativoCol> ProcOperColumnasCap { get; set; } = null;
        public String ConExpedienteSelColGrupoId { get; set; } = String.Empty;

        //Objs
        public MEVSF<EConExpedienteObjeto, EConExpedienteObjetoPag, EConExpedienteObjetoFiltro> ConExpedienteObjeto { get; set; } = new();

        //ExpedienteEstatu (ExpeEsta)
        public MEVSFCon<EExpedienteEstatu, EExpedienteEstatuPag, EExpedienteEstatuFiltro> ExpedienteEstatu { get; set; } = new();

        //Control de combo cascada
        public EProcesoOperativoCol ColumnaIdPais { get; set; } = null;
        public EProcesoOperativoCol ColumnaIdEstado { get; set; } = null;
        public EProcesoOperativoCol ColumnaIdMunicipio { get; set; } = null;
        public EProcesoOperativoCol ColumnaIdColonias { get; set; } = null;

        //Diferenciacion entre expedintes segun el proveedor
        /// <summary>
        /// Id del proceso operativo, que nos indica cuales son los expedientes con funcionamiento especifico segun el proveedor.
        /// </summary>
        public Int64 ProcesoOperativoIdProveedor { get; set; }
        /// <summary>
        /// Tipo de captura segun el expediente, si es &gt; 0 se filtraran los objetos que cumplan con el tipo de captura.
        /// Esto por ahora solo se usa en Medix.
        /// </summary>
        public TipoCaptura TipoCapturaIdExpediente { get; set; } = 0;
        /// <summary>
        /// Proveedor MontePio
        /// </summary>
        public EVConExpedientesMontePio MontePio { get; set; } = null;
        /// <summary>
        /// Proveedor Medix
        /// </summary>
        public EVConExpedientesMedix Medix { get; set; } = null;
        #endregion
    }
}
