using Rediin2022.Entidades.PriOperacion;
using Sisegui2020.Entidades.PriSeguridad;
using System;
using System.Threading.Tasks;

namespace Rediin2022.Aplicacion.PriOperacion;

public interface ISENConExpedienteProv
{
    EVConExpedientes EV { get; set; }
    Task<Boolean> Inicia();
    Task<Boolean> Inserta(EConExpediente conExpediente);
    void CambioEstatus(EConExpedienteCambioEstatus conExpedienteCambioEstatus);
    bool ValidaEstatus(long procesoOperativoEstId);
    Task<bool> ValidaEstatusParaCambio(EConExpedienteCambioEstatus conExpedienteCambioEstatus);
}
