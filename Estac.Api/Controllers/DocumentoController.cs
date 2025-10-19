using Estac.Domain.Input.Despesa;
using Estac.Domain.Interface.Services;
using Microsoft.AspNetCore.Mvc;

namespace Estac.Api.Controllers
{
    [ApiController]
    [Route("api/v1/documento")]

    public class DocumentoController : ControllerBase
    {
        public readonly IDespesaServices _services;

        public DocumentoController(IDespesaServices _services)
        {
            this._services = _services;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] DespesaFilterInput filter)
        {
            return await _services.GetAllAsync(filter);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return await _services.GetAsync(id);
        }

        //[HttpPost]
        //public ActionResult<DespesaGetInput> Create(DespesaGetInput product)
        //{
        //    product.Id = _products.Count + 1;
        //    _products.Add(product);
        //    return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        //}

        //[HttpPut("{id}")]
        //public IActionResult Update(int id, DespesaGetInput product)
        //{
        //    var existingProduct = _products.Find(p => p.Id == id);

        //    return result.Error.Any() ? BadRequest(result) : Ok(result);
        //}

        //[HttpDelete("{id}")]
        //public IActionResult Delete(int id)
        //{
        //    return result.Error.Any() ? BadRequest(result) : Ok(result);

        //}
    }
}
