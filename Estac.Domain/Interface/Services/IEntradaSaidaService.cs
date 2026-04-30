using Estac.Domain.Input.EntradaSaida;
using Microsoft.AspNetCore.Mvc;

namespace Estac.Domain.Interface.Services
{
    public interface IEntradaSaidaService
    {
        Task<ActionResult> ObterPorId(int id);
        Task<ActionResult> ObterPorPlaca(string placa);
        Task<ActionResult> Buscar(EntradaSaidaFilterInput filter);
        Task<ActionResult> Gravar(EntradaSaidaPostInput input);
        Task<ActionResult> Alterar(EntradaSaidaPutInput input);
        Task<ActionResult> SuspenderPermanencia(int id, EntradaSaidaPermanenciaInput input);
        Task<ActionResult> FinalizarPermanencia(int id, DateTime? dataHoraSaida);
        Task<ActionResult> Excluir(int id);
    }
}
