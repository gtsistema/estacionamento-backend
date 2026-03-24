using Estac.Api.Controllers.Base.Claim;
using Estac.Api.Controllers.Base.Permission;
using Estac.Domain.Input.Estacionamento;
using Estac.Domain.Interface.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Estac.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class EstacionamentoController : ControllerBase
    {
        public readonly IEstacionamentoService _services;

        public EstacionamentoController(IEstacionamentoService _services)
        {
            this._services = _services;
        }

        [PermissionAuthorize(PermissionAcess.Estacionamento.Visualizar)]
        [HttpGet]
        public async Task<ActionResult> Buscar([FromQuery] EstacionamentoFilterInput filter)
        {
            return await _services.Buscar(filter);
        }

        [PermissionAuthorize(PermissionAcess.Estacionamento.Visualizar)]
        [HttpGet("{id}")]
        public async Task<ActionResult> ObterPorId(int id)
        {
            return await _services.ObterPorId(id);
        }

        [PermissionAuthorize(PermissionAcess.Estacionamento.Gravar)]
        [HttpPost]
        public async Task<ActionResult> Gravar([FromBody] EstacionamentoPostInput input)
        {
            return await _services.Gravar(input);
        }

        [PermissionAuthorize(PermissionAcess.Estacionamento.Alterar)]
        [HttpPut]
        public async Task<ActionResult> Alterar([FromBody] EstacionamentoPutInput input)
        {
            return await _services.Alterar(input);
        }

        [PermissionAuthorize(PermissionAcess.Estacionamento.Excluir)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            return await _services.Excluir(id);
        }

        [PermissionAuthorize(PermissionAcess.Estacionamento.Visualizar)]
        [HttpGet("buscar-fotos/{id}")]
        public async Task<ActionResult> BuscarFotos(int id)
        {
            return await _services.BuscarFotos(id);
        }

        [PermissionAuthorize(PermissionAcess.Estacionamento.Gravar)]
        [HttpPost("upload-fotos")]
        public async Task<ActionResult> UploadFotos([FromForm] EstacionamentoFotosInput input)
        {
            return await _services.UploadFotos(input);
        }

        [PermissionAuthorize(PermissionAcess.Estacionamento.Excluir)]
        [HttpDelete("deletar-fotos/{fotoId}")]
        public async Task<ActionResult> DeletarFotos(int fotoId)
        {
            return await _services.ExcluirFotos(fotoId);
        }
    }
}
