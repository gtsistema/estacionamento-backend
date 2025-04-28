using Gp.Domain.Models;
using Gp.Domain.Models.Enuns;

namespace Gp.Domain.Interface.Repositories
{
    public interface IReceitaLancamentoRepositories : IBaseRepositories<ReceitaLancamento>
    {
        Task<Receita> GetIdByDescricaoAsync(string descricao, MesDoAno mesDoAno);
    }
}
