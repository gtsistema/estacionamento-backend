using Gp.Domain.Dtos;
using Gp.Domain.Input;
using Gp.Domain.Models;
using Gp.Domain.Shared;

namespace Gp.Domain.Interface.Repositories
{
    public interface IDespesaRepositories : IBaseRepositories<Despesa>
    {
        Task<PagedQuery<Despesa>> GetPageAsync(DespesaFilterInput input);
    }
}
