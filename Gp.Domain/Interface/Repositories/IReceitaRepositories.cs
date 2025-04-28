using Gp.Domain.Input.Despesa;
using Gp.Domain.Input.Receita;
using Gp.Domain.Models;
using Gp.Domain.Shared;

namespace Gp.Domain.Interface.Repositories
{
    public interface IReceitaRepositories : IBaseRepositories<Receita>
    {
        Task<PagedResult<Receita>> GetPageAsync(ReceitaFilterInput input);
        Task AtualizarSaldoPagoAsync(Receita receita, ReceitaLancamento receitaLancamento);
    }
}
