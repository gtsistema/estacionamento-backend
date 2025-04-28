using Gp.Domain.Input.Receita;
using Gp.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Gp.Domain.Interface.Services
{
    public interface IReceitaServices
    {
        Task<ActionResult> GetAsync(long id);
        Task<ActionResult> GetAllAsync(ReceitaFilterInput filter);
        Task<ActionResult> PostAsync(ReceitaPostInput input);
        Task<ActionResult> PutAsync(ReceitaPutInput input);
        Task<ActionResult> DeleteAsync(long id);
        Task<ActionResult> ImportarDadosExcelAsync(long id);
        Task<ActionResult> LancamentoAsync(ReceitaLancamentoPostInput input);
    }
}
