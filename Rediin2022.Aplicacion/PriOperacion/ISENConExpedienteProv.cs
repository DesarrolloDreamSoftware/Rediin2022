using Rediin2022.Entidades.PriOperacion;
using System;
using System.Threading.Tasks;

namespace Rediin2022.Aplicacion.PriOperacion;

public interface ISENConExpedienteProv
{
    #region ConExpediente
    EVConExpedientes EV { get; set; }
    Task<Boolean> Inicia();
    Task<Boolean> Inserta(EConExpediente conExpediente);
    void CambioEstatus(EConExpedienteCambioEstatus conExpedienteCambioEstatus);
    bool ValidaEstatus(long procesoOperativoEstId);
    Task<bool> ValidaEstatusParaCambio(EConExpedienteCambioEstatus conExpedienteCambioEstatus);
    #endregion

    #region ConExpedienteObjeto
    Task ValidaTipoCapturaXExpediente();
    #endregion
}
