using Gp.Domain.Input;
using Gp.Domain.Input.Cursos;
using Gp.Domain.Interface.Services.Cursos;
using Gp.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

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
            var resultado = await _services.ObterTodosAsync(input);

            return resultado.Error.Any() ? BadRequest(resultado.Error) : Ok(resultado.Data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Obter(Livro input)
        {
            var resultado = await _services.ObterAsync(input);

            return resultado.Error.Any() ? BadRequest(resultado.Error) : Ok(resultado.Data);
        }

        [HttpPost]
        public async Task<ActionResult> Novo(LivroInput input)
        {
            var resultado = await _services.NovoAsync(input);

            return resultado.Error.Any() ? BadRequest(resultado.Error) : Ok(resultado.Data);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Excluir(Livro input)
        {
            var resultado = await _services.ExcluirAsync(input);

            return resultado.Error.Any() ? BadRequest(resultado.Error) : Ok(resultado.Data);
        }
    }
}
