using Estac.Domain.Input;
using Estac.Domain.Input.Cursos;
using Estac.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Estac.Domain.Interface.Services.Cursos
{
    public interface ILivroServices
    {
        Task<ActionResult> ObterTodosAsync(FilterInput input);
        Task<ActionResult> ObterAsync(Livro dto);
        Task<ActionResult> NovoAsync(LivroInput dto);
        Task<ActionResult> ExcluirAsync(Livro dto);
    }
}
