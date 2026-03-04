
using Estac.Domain.Input.Estacionamento;
using Estac.Domain.Models;
using Estac.Domain.Models.Enuns;
using Estac.Domain.Output.Estacionamento;
using Estac.Domain.Shared;

namespace Estac.Domain.Interface.Repositories
{
    public interface IEstacionamentoRepositories : IBaseRepositoriesNone<Estacionamento>
    {
        Task<Estacionamento> SelecionarPorIdCompleto(int id);
        Task<PagedResult<EstacionamentoSearchOutput>> Paginar(EstacionamentoFilterInput input);
        Task<IEnumerable<EstacionamentoFoto>> ListarFotosPorEstacionamento(int id);
        Task<IEnumerable<EstacionamentoFotoOutput>> ListarFotosPorEstacionamentoAsNoTracking(int id);
        Task UploadFotos(List<EstacionamentoFoto> fotos);
        Task ExcluirFotos(int id);
    }
}