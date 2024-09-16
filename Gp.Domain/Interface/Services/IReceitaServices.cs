using Gp.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Gp.Domain.Interface.Services
{
    public interface IReceitaServices
    {
        Task<ActionResult> GetAsync(int id);
        Task<ActionResult> GetAllAsync(Receita filter);
        Task<ActionResult> PostAsync(Receita input);
        Task<ActionResult> PutAsync(Receita input);
        Task<ActionResult> DeleteAsync(int id);
    }
}
