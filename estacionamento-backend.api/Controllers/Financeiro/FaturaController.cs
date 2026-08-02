using Estac.Domain.Input.Fatura;
using Estac.Domain.Interface.Services;
using Estac.Domain.Models.Enuns;
using Estac.Domain.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Estac.Api.Controllers.Financeiro
{
    [Authorize]
    [ApiController]
    [Route("api/financeiro/[controller]")]
    public class FaturaController : ControllerBase
    {
        private readonly IFaturaService _services;

        public FaturaController(IFaturaService services)
        {
            _services = services;
        }

        [PermissionAuthorize(PermissionAcess.Fatura.Visualizar)]
        [HttpGet]
        public async Task<ActionResult> Buscar([FromQuery] FaturaFilterInput filter)
        {
            return await _services.Buscar(filter);
        }

        [PermissionAuthorize(PermissionAcess.Fatura.Visualizar)]
        [HttpGet("visao-geral")]
        public async Task<ActionResult> ObterVisaoGeral([FromQuery] FaturaFilterInput filter)
        {
            return await _services.ObterVisaoGeral(filter);
        }

        [PermissionAuthorize(PermissionAcess.Fatura.Visualizar)]
        [HttpGet("{id}")]
        public async Task<ActionResult> ObterPorId(int id)
        {
            return await _services.ObterPorId(id);
        }

        /// <summary>
        /// Gera fatura automaticamente para transportadora + estacionamento.
        /// Body: { transportadoraId, estacionamentoId? }.
        /// Se estacionamentoId for omitido, usa o estacionamento do usuário logado.
        /// </summary>
        [PermissionAuthorize(PermissionAcess.Fatura.Gravar)]
        [HttpPost]
        public async Task<ActionResult> Gravar([FromBody] FaturaPostInput input)
        {
            return await _services.Gravar(input);
        }

        [PermissionAuthorize(PermissionAcess.Fatura.Alterar)]
        [HttpPut]
        public async Task<ActionResult> Alterar([FromBody] FaturaPutInput input)
        {
            return await _services.Alterar(input);
        }

        [PermissionAuthorize(PermissionAcess.Fatura.Excluir)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Excluir(int id)
        {
            return await _services.Excluir(id);
        }

        /// <summary>
        /// Gera o PDF da fatura via estacionamento-report (Relatorios/Gerar).
        /// </summary>
        [PermissionAuthorize(PermissionAcess.Fatura.Visualizar)]
        [HttpGet("{id:int}/report")]
        public async Task<ActionResult> Report(int id, CancellationToken cancellationToken = default)
        {
            return await _services.GerarRelatorio(id, FormatoRelatorio.Pdf, cancellationToken);
        }

        /// <summary>
        /// Gera o Excel da fatura via estacionamento-report (Relatorios/Gerar).
        /// </summary>
        [PermissionAuthorize(PermissionAcess.Fatura.Visualizar)]
        [HttpGet("{id:int}/excel")]
        public async Task<ActionResult> Excel(int id, CancellationToken cancellationToken = default)
        {
            return await _services.GerarExcel(id, cancellationToken);
        }
    }
}
