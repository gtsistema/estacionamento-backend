using Estac.Domain.Input.ConfiguracaoCobranca;
using Estac.Domain.Models;
using Estac.Domain.Output.ConfiguracaoCobranca;
using Estac.Domain.Shared;

namespace Estac.Domain.Interface.Repositories
{
    public interface IConfiguracaoCobrancaRepositories : IBaseRepositories<ConfiguracaoCobranca>
    {
        Task<ConfiguracaoCobranca> SelecionarPorIdCompleto(int id);
        Task<PagedResult<ConfiguracaoCobrancaSearchOutput>> Paginar(ConfiguracaoCobrancaFilterInput input);
        Task<bool> ExistePorTransportadoraEstacionamentoAsync(int transportadoraId, int estacionamentoId, int? ignorarId = null);
        Task Remove(int id);
    }
}
