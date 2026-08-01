using Estac.Domain.Input.Estacionamento;
using Microsoft.AspNetCore.Mvc;

namespace Estac.Domain.Interface.Services
{
    public interface IEstacionamentoConfiguracaoService
    {
        Task<ActionResult> ListarPadroesBrasil();
        Task<ActionResult> ObterDoUsuario();
        Task<ActionResult> ObterPorEstacionamentoId(int estacionamentoId);
        Task<ActionResult> Gravar(EstacionamentoConfiguracaoPostInput input);
        Task<ActionResult> Alterar(EstacionamentoConfiguracaoPutInput input);
    }
}
