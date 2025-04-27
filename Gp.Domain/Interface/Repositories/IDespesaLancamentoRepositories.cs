using Gp.Domain.Models;
using Gp.Domain.Models.Enuns;

namespace Gp.Domain.Interface.Repositories
{
    public interface IDespesaLancamentoRepositories : IBaseRepositories<DespesaLancamento>
    {
        Task<Despesa> GetIdByDescricaoAsync(string descricao, MesDoAno mesDoAno);
    }
}
