

using Estac.Domain.Input.Veiculo;
using Estac.Domain.Output.Veiculo;
using Microsoft.AspNetCore.Mvc;

namespace Estac.Domain.Interface.Services
{
    public interface IVeiculoService
    {
        Task<ActionResult> ObterPorId(int id);
        Task<ActionResult> Buscar(VeiculoFilterInput filter);
        Task<ActionResult> Gravar(VeiculoPostInput input);
        Task<ActionResult> Alterar(VeiculoPutInput input);
        Task<ActionResult> Excluir(int id);
    }
}
