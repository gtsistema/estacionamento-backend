using Gp.Domain.Input;
using Gp.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Gp.Domain.Interface.Services
{
    public interface IDespesaServices 
    {
        Task<ActionResult> GetAsync(long id);
        Task<ActionResult> GetAllAsync(DespesaFilterInput filter);
        Task<ActionResult> PostAsync(DespesaPostInput input);
        Task<ActionResult> PutAsync(DespesaPutInput input);
        Task<ActionResult> DeleteAsync(long id);
    }
}
