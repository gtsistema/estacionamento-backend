using Gp.Domain.Input.Receita;
using Gp.Domain.Interface.Services;
using Microsoft.AspNetCore.Mvc;

namespace Gp.Api.Controllers
{
    [ApiController]
    [Route("api/v1/despesa")]

    public class ReceitaController : ControllerBase
    {
        public readonly IReceitaServices _services;

        public ReceitaController(IReceitaServices _services)
        {
            this._services = _services;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll([FromQuery] ReceitaFilterInput filter)
        {
            return await _services.GetAllAsync(filter);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(long id)
        {
            return await _services.GetAsync(id);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] ReceitaPostInput product)
        {
            return await _services.PostAsync(product);
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] ReceitaPutInput input)
        {
            return await _services.PutAsync(input);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id)
        {
            return await _services.DeleteAsync(id);
        }

        [HttpPost("lancamento")]
        public async Task<ActionResult> Lancamento([FromBody] ReceitaLancamentoPostInput input)
        {
            return await _services.LancamentoAsync(input);
        }
    }
}
