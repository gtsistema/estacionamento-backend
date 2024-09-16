using Gp.Domain.Input;
using Gp.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Gp.Domain.Interface.Services
{
    public interface IDespesaServices 
    {
        Task<ActionResult> GetAsync(int id);
        Task<ActionResult> GetAllAsync(DespesaFilterInput filter);
        Task<ActionResult> PostAsync(DespesaPostInput input);
        Task<ActionResult> PutAsync(Despesa item);
        Task<ActionResult> DeleteAsync(int id);
    }
}
