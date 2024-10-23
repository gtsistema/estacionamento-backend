using Gp.Domain.Input;
using Gp.Domain.Input.Cursos;
using Gp.Domain.Interface.Services.Cursos;
using Gp.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gp.Api.Controllers.Auth
{
    [ApiController]
    [Authorize]
    [Route("api/v1/cursos/livros")]
    public class LivroController : ControllerBase
    {
        public readonly ILivroServices _services;

        public LivroController(ILivroServices services)
        {
            _services = services;
        }

        [HttpGet("todos")]
        public async Task<ActionResult> ObterTodos(FilterInput input)
        {
            return await _services.ObterTodosAsync(input);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Obter(Livro input)
        {
            return await _services.ObterAsync(input);
        }

        [HttpPost]
        public async Task<ActionResult> Novo(LivroInput input)
        {
            return await _services.NovoAsync(input);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Excluir(Livro input)
        {
            return await _services.ExcluirAsync(input);
        }
    }
}
