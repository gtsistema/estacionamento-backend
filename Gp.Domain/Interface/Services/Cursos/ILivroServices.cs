
using Gp.Domain.Input;
using Gp.Domain.Input.Cursos;
using Gp.Domain.Models;
using Gp.Domain.Output;

namespace Gp.Domain.Interface.Services.Cursos
{
    public interface ILivroServices
    {
        Task<ServicesResult> ObterTodosAsync(FilterInput input);
        Task<ServicesResult> ObterAsync(Livro dto);
        Task<ServicesResult> NovoAsync(LivroInput dto);
        Task<ServicesResult> ExcluirAsync(Livro dto);

        Task<IEnumerable<Livro>> GetallTeste();
    }
}
