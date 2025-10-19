using Estac.Domain.Models;
using Estac.Domain.Models.Enuns;

namespace Estac.Domain.Interface.Repositories
{
    public interface IDespesaPagamentoRepositories : IBaseRepositories<DespesaPagamento>
    {
        Task<Despesa> GetIdByDescricaoAsync(string descricao, MesDoAno mesDoAno);
    }
}
