using Estac.Domain.Dtos;
using Estac.Domain.Input.Motorista;
using Estac.Domain.Models;
using Estac.Domain.Models.Enuns;
using Estac.Domain.Output.Motorista;
using Estac.Domain.Shared;

namespace Estac.Domain.Interface.Repositories
{
    public interface IMotoristaRepositories : IBaseRepositories<Motorista>
    {
        Task<PagedResult<MotoristaSearchOutput>> Paginar(MotoristaFilterInput input);
    }
}