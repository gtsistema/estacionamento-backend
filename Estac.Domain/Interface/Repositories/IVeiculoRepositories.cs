
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
        Task<bool> PossuiMotoristaVinculadoAsync(int veiculoId);
        /// <summary>Indica se existe vínculo em <c>VeiculoMotorista</c> para algum veículo desta transportadora.</summary>
        Task<bool> PossuiVeiculoMotoristaNaTransportadoraAsync(int transportadoraId);
        /// <summary>Indica se o motorista possui algum registro em <c>VeiculoMotorista</c>.</summary>
        Task<bool> PossuiVeiculoMotoristaParaMotoristaAsync(int motoristaId);
        Task<bool> ExcluirCompleto(int id);
        Task<EntradaSaidaVinculoOutput> ObterVinculosPorPlaca(string placa);
    }
}