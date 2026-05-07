using Estac.Domain.Input.Movimento.EntradaSaida;
using Estac.Domain.Models;
using Estac.Domain.Output.Movimento.EntradaSaida;
using Estac.Domain.Shared;

namespace Estac.Domain.Interface.Repositories
{
    public interface IEntradaSaidaRepositories : IBaseRepositories<EntradaSaida>
    {
        Task<EntradaSaida> SelecionarPorIdCompleto(int id);
        Task<EntradaSaida> SelecionarParaControlePermanencia(int id);
        Task<EntradaSaida> SelecionarPorPlaca(string placa);
        Task<EntradaSaida> SelecionarEmAbertoPorPlaca(string placa);
        Task<PagedResult<EntradaSaidaSearchOutput>> Paginar(EntradaSaidaFilterInput input);
    }
}
