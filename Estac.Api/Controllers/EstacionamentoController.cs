using Estac.Api.Controllers.Base;
using Estac.Api.Controllers.Base.Claim;
using Estac.Domain.Input.Estacionamento;
using Estac.Domain.Interface.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Estac.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/")]
    public class EstacionamentoController : ControllerBase
    {
        public readonly IEstacionamentoService _services;

        public EstacionamentoController(IEstacionamentoService _services)
        {
            this._services = _services;
        }

        [ClaimAuthorize(Claim.Estacionamento.Visualizar)]
        [HttpGet]
        public async Task<ActionResult> Buscar([FromQuery] EstacionamentoFilterInput filter)
        {
            return await _services.Buscar(filter);
        }

        [ClaimAuthorize(Claim.Estacionamento.Visualizar)]
        [HttpGet("{id}")]
        public async Task<ActionResult> ObterPorId(int id)
        {
            return await _services.ObterPorId(id);
        }

        [ClaimAuthorize(Claim.Estacionamento.Gravar)]
        [HttpPost]
        public async Task<ActionResult> Gravar([FromForm] EstacionamentoPostInput input)
        {
            return await _services.Gravar(input);
        }

        [ClaimAuthorize(Claim.Estacionamento.Alterar)]
        [HttpPut]
        public async Task<ActionResult> Alterar([FromBody] EstacionamentoPutInput input)
        {
            return await _services.Alterar(input);
        }

        [ClaimAuthorize(Claim.Estacionamento.Excluir)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            return await _services.Excluir(id);
        }

        [ClaimAuthorize(Claim.Estacionamento.Visualizar)]
        [HttpGet("buscar-fotos/{id}")]
        public async Task<ActionResult> BuscarFotos(int id)
        {
            return await _services.BuscarFotos(id);
        }

        [ClaimAuthorize(Claim.Estacionamento.Gravar)]
        [HttpPost("upload-fotos")]
        public async Task<ActionResult> UploadFotos([FromForm] EstacionamentoFotosInput input)
        {
            return await _services.UploadFotos(input);
        }

        [ClaimAuthorize(Claim.Estacionamento.Excluir)]
        [HttpDelete("deletar-fotos/{fotoId}")]
        public async Task<ActionResult> DeletarFotos(int fotoId)
        {
            return await _services.ExcluirFotos(fotoId);
        }
    }
}
