using Estac.Domain.Input.Movimento.Entrada;
using Estac.Domain.Input.Movimento.EntradaSaida;
using Microsoft.AspNetCore.Mvc;

namespace Estac.Domain.Interface.Services
{
    public interface IEntradaSaidaService
    {
        Task<ActionResult> ObterPorId(int id);
        Task<ActionResult> ObterPorPlaca(string placa);
        Task<ActionResult> Buscar(EntradaSaidaFilterInput filter);
        Task<ActionResult> Gravar(EntradaPostInput input);
        Task<ActionResult> Saida(EntradaSaidaPlacaInput input);
        Task<ActionResult> SuspenderPermanencia(int id, EntradaSaidaPermanenciaInput input);
        Task<ActionResult> Excluir(int id);
        Task<ActionResult> FinalizarPermanencia(int id, DateTime? dataHoraEvento);
    }
}
