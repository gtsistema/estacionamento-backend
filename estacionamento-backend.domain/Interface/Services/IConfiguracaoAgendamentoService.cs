using Estac.Domain.Input.ConfiguracaoCobranca;
using Microsoft.AspNetCore.Mvc;

namespace Estac.Domain.Interface.Services
{
    public interface IConfiguracaoAgendamentoService
    {
        Task<ActionResult> ObterPorId(Guid id);
        Task<ActionResult> Buscar(ConfiguracaoAgendamentoFilterInput filter);
        Task<ActionResult> Alterar(ConfiguracaoAgendamentoPutInput input);
    }
}
