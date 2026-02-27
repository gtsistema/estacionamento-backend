using Estac.Domain.Input.Transportadora;
using Estac.Domain.Output.Transportadora;
using Microsoft.AspNetCore.Mvc;

namespace Estac.Domain.Interface.Services
{
    public interface ITransportadoraService
    {
        Task<ActionResult> ObterPorId(int id);
        Task<ActionResult> Buscar(TransportadoraFilterInput filter);
        Task<ActionResult> Gravar(TransportadoraPostInput input);
        Task<ActionResult> Alterar(TransportadoraPutInput input);
        Task<ActionResult> Excluir(int id);
    }
}