using Estac.Domain.Input.Estacionamento;
using Estac.Domain.Output.Estacionamento;
using Microsoft.AspNetCore.Mvc;

namespace Estac.Domain.Interface.Services
{
    public interface IEstacionamentoService
    {
        Task<ActionResult> ObterPorId(int id);
        Task<ActionResult> Buscar(EstacionamentoFilterInput filter);
        Task<ActionResult> Gravar(EstacionamentoPostInput input);
        Task<ActionResult> Alterar(EstacionamentoPutInput input);
        Task<ActionResult> Excluir(int id);
    }
}
