using Gp.Domain.Models;
using Gp.Domain.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gp.Domain.Interface.Services
{
    public interface IOrcamentoServices
    {
        Task<ServicesResult> GetAsync(int id);
        Task<ServicesResult> GetAllAsync(Orcamento filter);
        Task<ServicesResult> PostAsync(Orcamento input);
        Task<ServicesResult> PutAsync(Orcamento input);
        Task<ServicesResult> DeleteAsync(int id);
    }
}
