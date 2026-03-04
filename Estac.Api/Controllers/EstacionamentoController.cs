using DocumentFormat.OpenXml.Office2010.Excel;
using Estac.Domain.Input.Estacionamento;
using Estac.Domain.Interface.Services;
using Estac.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Estac.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class EstacionamentoController : ControllerBase
    {
        public readonly IEstacionamentoService _services;

        public EstacionamentoController(IEstacionamentoService _services)
        {
            this._services = _services;
        }

        [HttpGet]
        public async Task<ActionResult> Buscar([FromQuery] EstacionamentoFilterInput filter)
        {
            return await _services.Buscar(filter);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> ObterPorId(int id)
        {
            return await _services.ObterPorId(id);
        }

        [HttpPost]
        public async Task<ActionResult> Gravar([FromForm] EstacionamentoPostInput input)
        {
            return await _services.Gravar(input);
        }

        [HttpPut]
        public async Task<ActionResult> Alterar([FromBody] EstacionamentoPutInput input)
        {
            return await _services.Alterar(input);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            return await _services.Excluir(id);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> BuscarFotos(int id)
        {
            return await _services.BuscarFotos(id);
        }

        [HttpPost]
        public async Task<ActionResult> UploadFotos([FromForm] EstacionamentoFotosInput input)
        {
            return await _services.UploadFotos(input);
        }

        [HttpDelete("{fotoId}")]
        public async Task<ActionResult> DeletarFotos(int fotoId)
        {
            return await _services.ExcluirFotos(fotoId);
        }
    }
}
