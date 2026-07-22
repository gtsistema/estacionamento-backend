using Estac.Domain.Input.Fatura;
using Estac.Domain.Models;
using Estac.Domain.Output.Fatura;
using Estac.Domain.Shared;

namespace Estac.Domain.Interface.Repositories
{
    public interface IFaturaRepositories : IBaseRepositories<Fatura>
    {
        Task<Fatura> SelecionarPorIdCompleto(int id);
        Task<PagedResult<FaturaSearchOutput>> Paginar(FaturaFilterInput input);
        Task<FaturaVisaoGeralOutput> ObterVisaoGeral(FaturaFilterInput input);
        Task<bool> ExisteNumeroAsync(string numero, int? ignorarId = null);
        Task Remove(int id);
    }
}
