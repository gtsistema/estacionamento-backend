
using Estac.Domain.Input.Transportadora;
using Estac.Domain.Models;
using Estac.Domain.Models.Enuns;
using Estac.Domain.Output.Transportadora;
using Estac.Domain.Shared;

namespace Estac.Domain.Interface.Repositories
{
    public interface ITransportadoraRepositories : IBaseRepositoriesNone<Transportadora>
    {
        Task<Transportadora> GravarCompleto(Transportadora item);
        Task<Transportadora> SelecionarPorIdCompleto(int id);
        Task<PagedResult<TransportadoraSearchOutput>> Paginar(TransportadoraFilterInput input);
        Task<Transportadora> SelecionarIdSimplificado(int id);
        Task<TransportadoraPorCnpjOutput> SelecionarPorCnpj(string cnpj);
        Task<bool> ExistePorCnpjAsync(string cnpj);
        Task<bool> PossuiVeiculoVinculadoAsync(int transportadoraId);
        Task<bool> PossuiEntradaSaidaVinculadaAsync(int transportadoraId);
        Task Remove(int id);
    }
}