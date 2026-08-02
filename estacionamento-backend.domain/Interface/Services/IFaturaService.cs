using Estac.Domain.Input.Fatura;
using Microsoft.AspNetCore.Mvc;

namespace Estac.Domain.Interface.Services
{
    public interface IFaturaService
    {
        Task<ActionResult> ObterPorId(int id);
        Task<ActionResult> Buscar(FaturaFilterInput filter);
        Task<ActionResult> ObterVisaoGeral(FaturaFilterInput filter);
        Task<ActionResult> Gravar(FaturaPostInput input);
        Task<ActionResult> Alterar(FaturaPutInput input);
        Task<ActionResult> Excluir(int id);
    }
}
