using Estac.Domain.Input.VeiculoModelo;
using Estac.Domain.Output.VeiculoModelo;
using Microsoft.AspNetCore.Mvc;

namespace Estac.Domain.Interface.Services
{
    public interface IVeiculoModeloService
    {
        Task<ActionResult> ObterPorId(int id);
        Task<ActionResult> Buscar(VeiculoModeloFilterInput filter);
        Task<ActionResult> Gravar(VeiculoModeloPostInput input);
        Task<ActionResult> Alterar(VeiculoModeloPutInput input);
        Task<ActionResult> Excluir(int id);
    }
}