using Gp.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Gp.Domain.Interface.Services
{
    public interface IOrcamentoServices
    {
        Task<ActionResult> GetAsync(int id);
        Task<ActionResult> GetAllAsync(Orcamento filter);
        Task<ActionResult> PostAsync(Orcamento input);
        Task<ActionResult> PutAsync(Orcamento input);
        Task<ActionResult> DeleteAsync(int id);
    }
}
