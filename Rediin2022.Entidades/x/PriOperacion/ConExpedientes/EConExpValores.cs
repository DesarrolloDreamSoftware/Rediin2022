using DSMetodNetX.Entidades;
using System;

namespace Rediin2022.Entidades.PriOperacion
{
    [Serializable]
    public class EConExpValores
    {
        [MDAMain] public Int64 ExpedienteId { get; set; } = 0L;
        [MDAMain] public Int64 ColumnaId { get; set; } = 0L;
        [MDAMain(true)] public String ValorTexto { get; set; } = String.Empty;
        [MDAMain(true)] public Decimal ValorNumerico { get; set; } = 0M;
        [MDAMain(true)] public DateTime ValorFecha { get; set; } = DateTime.MinValue;
    }
}
