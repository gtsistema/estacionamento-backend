using Gp.Domain.Input.Despesa;
using Gp.Domain.Interface.Services;
using Microsoft.AspNetCore.Mvc;

namespace Gp.Api.Controllers
{
    [ApiController]
    [Route("api/v1/despesa")]

    public class DespesaController : ControllerBase
    {
        public readonly IDespesaServices _services;

        public DespesaController(IDespesaServices _services)
        {
            this._services = _services;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll([FromQuery] DespesaFilterInput filter)
        {
            return await _services.GetAllAsync(filter);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(long id)
        {
            return await _services.GetAsync(id);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] DespesaPostInput product)
        {
            return await _services.PostAsync(product);
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] DespesaPutInput input)
        {
            return await _services.PutAsync(input);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id)
        {
            return await _services.DeleteAsync(id);
        }

        [HttpPost("lancamento")]
        public async Task<ActionResult> Lancamento([FromBody] DespesaLancamentoPostInput input)
        {
            return await _services.LancamentoAsync(input);
        }
    }
}
