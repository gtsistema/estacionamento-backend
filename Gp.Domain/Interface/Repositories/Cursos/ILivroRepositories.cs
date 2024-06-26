using Gp.Domain.Input;
using Gp.Domain.Models;
using Gp.Domain.Shared;

namespace Gp.Domain.Interface.Repositories.Cursos
{
    public interface ILivroRepositories : IBaseRepositories<Livro>
    {
        Task<PagedQuery<Livro>> GetPageAsync(FilterInput input);
    }
}
