using Estac.Domain.Models;

namespace Estac.Domain.Interface.Repositories
{
    public interface IVeiculoMotoristaRepositories : IBaseRepositoriesNone<VeiculoMotorista>
    {
        Task VincularAsync(int veiculoId, int motoristaId);
    }
}
