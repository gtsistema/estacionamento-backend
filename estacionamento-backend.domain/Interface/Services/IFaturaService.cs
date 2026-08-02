using Estac.Domain.Input.Fatura;
using Estac.Domain.Models.Enuns;
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
        Task<ActionResult> GerarRelatorio(int id, FormatoRelatorio formato, CancellationToken cancellationToken = default);
        Task<ActionResult> GerarExcel(int id, CancellationToken cancellationToken = default);
    }
}
