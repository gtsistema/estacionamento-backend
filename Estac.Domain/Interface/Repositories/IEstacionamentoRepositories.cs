
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
        Task<PagedResult<Estacionamento>> Paginar(EstacionamentoFilterInput input);
    }
}