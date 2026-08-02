using Estac.Domain.Models;

namespace Estac.Domain.Interface.Repositories
{
    public interface IEstacionamentoConfiguracaoRepositories
    {
        Task<EstacionamentoConfiguracao> ObterPorEstacionamentoIdAsync(int estacionamentoId);
        Task<EstacionamentoConfiguracao> ObterPorIdAsync(int id);
        Task<EstacionamentoConfiguracao> GravarAsync(EstacionamentoConfiguracao entity);
        Task<EstacionamentoConfiguracao> AlterarAsync(EstacionamentoConfiguracao entity);
        Task<bool> ExistePorEstacionamentoIdAsync(int estacionamentoId);
    }
}
