using Estac.Domain.Input.Faturamento;
using Estac.Domain.Output.Faturamento;
using Microsoft.AspNetCore.Mvc;

namespace Estac.Domain.Interface.Services
{
    public interface IFaturamentoService
    {
        Task<ActionResult> ObterElegiveis(DateTime? referencia = null);
        Task<ActionResult> RegistrarExecucao(RegistrarExecucaoAgendamentoInput input);
    }
}
