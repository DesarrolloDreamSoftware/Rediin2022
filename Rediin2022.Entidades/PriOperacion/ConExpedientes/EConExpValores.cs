using DSEntityNetX.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rediin2022.Entidades.PriOperacion
{
    [Serializable]
    public class EConExpValores
    {
        [XMain] public Int64 ExpedienteId { get; set; } = 0L;
        [XMain] public Int64 ColumnaId { get; set; } = 0L;
        [XMain(true)] public String ValorTexto { get; set; } = String.Empty;
        [XMain(true)] public Decimal ValorNumerico { get; set; } = 0M;
        [XMain(true)] public DateTime ValorFecha { get; set; } = DateTime.MinValue;
    }
}
