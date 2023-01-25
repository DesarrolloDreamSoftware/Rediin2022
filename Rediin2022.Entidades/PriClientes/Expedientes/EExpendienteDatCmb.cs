using DSEntityNetX.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rediin2022.Entidades.PriClientes.Expedientes
{
	[Serializable]
	public class EExpendienteDatCmb
	{
		public Int64 CapCmbProcesoOperativoId { get; set; } = 0L;
		public Int64 CapCmbIdColumnaId { get; set; } = 0L;
		public Int64 CapCmbTextoColumnaId { get; set; } = 0L;
	}
}
