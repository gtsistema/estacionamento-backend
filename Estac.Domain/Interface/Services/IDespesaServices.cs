using Estac.Domain.Input.Despesa;
using Estac.Domain.Models;
using Estac.Domain.Models.Enuns;
using Microsoft.AspNetCore.Mvc;

namespace Estac.Domain.Interface.Services
{
    public interface IDespesaServices 
    {
        Task<ActionResult> GetAsync(long id);
        Task<ActionResult> GetAllAsync(DespesaFilterInput filter);
        Task<ActionResult> PostAsync(DespesaPostInput input);
        Task<ActionResult> PutAsync(DespesaPutInput input);
        Task<ActionResult> DeleteAsync(long id);
        Task<ActionResult> ImportarDadosExcelAsync(long id);
        Task<ActionResult> LancamentoAsync(DespesaLancamentoPostInput input);
        Task<ActionResult> PagamentoAsync(DespesaPagamentoPostInput input);
        Task<ActionResult> AtualizarSaldoDevadorDoMesAsync(AtualizaSaldoDoMesPostInput input);
    }
}
