
using Estac.Domain.Input.Veiculo;
using Estac.Domain.Models;
using Estac.Domain.Output.Motorista;
using Estac.Domain.Output.Veiculo;
using Estac.Domain.Shared;

namespace Estac.Domain.Interface.Repositories
{
    public interface IVeiculoRepositories : IBaseRepositories<Veiculo>
    {
        Task<PagedResult<VeiculoSearchOutput>> Paginar(VeiculoFilterInput input);
        Task<Veiculo> SelecionarPorIdCompleto(int id);
        Task<Veiculo> GravarCompleto(Veiculo veiculo);
        Task<Veiculo?> AlterarCompleto(Veiculo dados);
        Task<bool> ExcluirCompleto(int id);
        Task<MotoristaVinculosPorPlacaOutput> ObterVinculosPorPlaca(string placa);
    }
}