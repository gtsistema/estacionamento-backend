using Estac.Domain.Input.Despesa;
using Estac.Domain.Interface.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Estac.Api.Controllers
{
    [Authorize]
    public class EstacionamentoController
    {
        public readonly IDespesaServices _services;
        public EstacionamentoController(IDespesaServices _services)
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
    }
}
