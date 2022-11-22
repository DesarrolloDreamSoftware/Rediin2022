using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rediin2022.Entidades.PriClientes
{
    [Serializable]
    public class EExpediente
    {
        public long ProcesoOperativoId { get; set; } = 0L; //[Llave padre]
        public long ExpendienteId { get; set; } = 0L; //[Llave solo para actualizacion y eliminacion]
        public List<EExpendienteValor> Valores { get; set; } = new List<EExpendienteValor>();
    }
}
