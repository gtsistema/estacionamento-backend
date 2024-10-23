using Gp.Domain.Input;
using Gp.Domain.Models;
using Gp.Domain.Output;

namespace Gp.Domain.Interface.Services
{
    public interface IDespesaServices 
    {
        Task<ActionResult> GetAsync(int id);
        Task<ActionResult> GetAllAsync(DespesaFilterInput filter);
        Task<ActionResult> PostAsync(DespesaPostInput input);
        Task<ActionResult> PutAsync(Despesa input);
        Task<ActionResult> DeleteAsync(int id);
    }
}
