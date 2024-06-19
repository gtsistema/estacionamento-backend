using Gp.Domain.Input;
using Gp.Domain.Models;
using Gp.Domain.Output;

namespace Gp.Domain.Interface.Services
{
    public interface IDespesaServices 
    {
        Task<ServicesResult> GetAsync(int id);
        Task<ServicesResult> GetAllAsync(DespesaFilterInput filter);
        Task<ServicesResult> PostAsync(DespesaPostInput input);
        Task<ServicesResult> PutAsync(Despesa input);
        Task<ServicesResult> DeleteAsync(int id);
    }
}
