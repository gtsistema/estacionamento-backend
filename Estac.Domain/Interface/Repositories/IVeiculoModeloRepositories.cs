using Estac.Domain.Dtos;
using Estac.Domain.Input.VeiculoModelo;
using Estac.Domain.Models;
using Estac.Domain.Models.Enuns;
using Estac.Domain.Output.VeiculoModelo;
using Estac.Domain.Shared;

namespace Estac.Domain.Interface.Repositories
{
    public interface IVeiculoModeloRepositories : IBaseRepositories<VeiculoModelo>
    {
        Task<PagedResult<VeiculoModeloSearchOutput>> Paginar(VeiculoModeloFilterInput input);
    }
}