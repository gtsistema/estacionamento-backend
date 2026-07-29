using Estac.Domain.Input.ConfiguracaoCobranca;
using Estac.Domain.Models;
using Estac.Domain.Output.ConfiguracaoCobranca;
using Estac.Domain.Shared;

namespace Estac.Domain.Interface.Repositories
{
    public interface IConfiguracaoAgendamentoRepositories
    {
        Task<ConfiguracaoAgendamento> SelecionarPorIdCompleto(Guid id);
        Task<PagedResult<ConfiguracaoAgendamentoOutput>> Paginar(ConfiguracaoAgendamentoFilterInput input);
    }
}
