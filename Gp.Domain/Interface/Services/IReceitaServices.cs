using Gp.Domain.Models;
using Gp.Domain.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gp.Domain.Interface.Services
{
    public interface IReceitaServices
    {
        Task<ServicesResult> GetAsync(int id);
        Task<ServicesResult> GetAllAsync(Receita filter);
        Task<ServicesResult> PostAsync(Receita input);
        Task<ServicesResult> PutAsync(Receita input);
        Task<ServicesResult> DeleteAsync(int id);
    }
}
