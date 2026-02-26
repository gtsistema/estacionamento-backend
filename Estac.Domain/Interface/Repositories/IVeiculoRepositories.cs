using Estac.Domain.Dtos;
using Estac.Domain.Input.Veiculo;
using Estac.Domain.Models;
using Estac.Domain.Models.Enuns;
using Estac.Domain.Output.Veiculo;
using Estac.Domain.Shared;

namespace Estac.Domain.Interface.Repositories
{
    public interface IVeiculoRepositories : IBaseRepositories<Veiculo>
    {
        Task<PagedResult<VeiculoSearchOutput>> Paginar(VeiculoFilterInput input);
    }
}