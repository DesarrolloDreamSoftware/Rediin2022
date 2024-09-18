using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rediin2022.Entidades.PriClientes
{
    public class EDocumento
    {
        //public Int64 ProcesoOperativoId { get; set; }
        public Int64 ExpedienteId { get; set; }
        public Int64 ExpedienteObjetoId { get; set; } = 0L;
        public String ArchivoNombre { get; set; }
        public Byte[] Documento { get; set; }
    }
}
