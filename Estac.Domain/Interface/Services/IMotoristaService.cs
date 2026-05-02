using Estac.Domain.Input.Motorista;
using Estac.Domain.Output.Motorista;
using Microsoft.AspNetCore.Mvc;

namespace Estac.Domain.Interface.Services
{
    public interface IMotoristaService
    {
        Task<ActionResult> ObterPorId(int id);
        Task<ActionResult> Buscar(MotoristaFilterInput filter);
        Task<ActionResult> Gravar(MotoristaPostInput input);
        Task<ActionResult> Alterar(MotoristaPutInput input);
        Task<ActionResult> Excluir(int id);
    }
}
