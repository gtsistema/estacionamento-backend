using Estac.Domain.Input.Faturamento;
using Estac.Domain.Interface.Services;
using Estac.Domain.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Estac.Api.Controllers.Financeiro
{
    [Authorize]
    [ApiController]
    [Route("api/financeiro/[controller]")]
    public class FaturamentoController : ControllerBase
    {
        private readonly IFaturamentoService _services;

        public FaturamentoController(IFaturamentoService services)
        {
            _services = services;
        }

        /// <summary>
        /// Retorna configurações de cobrança elegíveis com as movimentações aptas a faturamento.
        /// </summary>
        [PermissionAuthorize(PermissionAcess.Fatura.Visualizar)]
        [HttpGet("elegiveis")]
        public async Task<ActionResult> ObterElegiveis([FromQuery] DateTime? referencia = null)
        {
            return await _services.ObterElegiveis(referencia);
        }

        /// <summary>
        /// Atualiza UltimaExecucao/ProximaExecucao após o job gerar a fatura com sucesso.
        /// </summary>
        [PermissionAuthorize(PermissionAcess.Fatura.Alterar)]
        [HttpPost("registrar-execucao")]
        public async Task<ActionResult> RegistrarExecucao([FromBody] RegistrarExecucaoAgendamentoInput input)
        {
            return await _services.RegistrarExecucao(input);
        }
    }
}
