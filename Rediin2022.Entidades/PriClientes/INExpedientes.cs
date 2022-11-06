using DSMetodNetX.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rediin2022.Entidades.PriClientes
{
    public interface INExpedientes : IMCtrMensajes
    {
        Int64 ExpedienteInserta(EExpediente expediente);
        Boolean ExpedienteElimina(Int64 expedienteId);
        Int64 ObjetoInserta(EExpedienteObjeto expedienteObjeto);
    }
}
