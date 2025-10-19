using Estac.Domain.Input;
using Estac.Domain.Models;
using Estac.Domain.Shared;

namespace Estac.Domain.Interface.Repositories.Cursos
{
    public interface ILivroRepositories : IBaseRepositories<Livro>
    {
        Task<PagedResult<Livro>> GetPageAsync(FilterInput input);
    }
}
