using Gp.Domain.Input;
using Gp.Domain.Input.Cursos;
using Gp.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Gp.Domain.Interface.Services.Cursos
{
    public interface ILivroServices
    {
        Task<ActionResult> ObterTodosAsync(FilterInput input);
        Task<ActionResult> ObterAsync(Livro dto);
        Task<ActionResult> NovoAsync(LivroInput dto);
        Task<ActionResult> ExcluirAsync(Livro dto);
    }
}
