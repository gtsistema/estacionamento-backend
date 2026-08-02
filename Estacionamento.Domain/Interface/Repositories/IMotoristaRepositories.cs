using Estac.Domain.Input.Motorista;
using Estac.Domain.Models;
using Estac.Domain.Output.Motorista;
using Estac.Domain.Shared;

namespace Estac.Domain.Interface.Repositories
{
    public interface IMotoristaRepositories : IBaseRepositoriesNone<Motorista>
    {
        Task<PagedResult<MotoristaSearchOutput>> Paginar(MotoristaFilterInput input);
        Task<Motorista> SelecionarPorIdCompleto(int id);
        Task<MotoristaPorCpfOutput> SelecionarPorCpf(string cpf);
        Task<bool> PossuiEntradaSaidaVinculadaAsync(int motoristaId);

        Task Remove(int id);
    }
}