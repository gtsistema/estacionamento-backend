
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

        /// <summary>Busca modelo por descrição exata (case-insensitive). Opcionalmente filtra pela marca.</summary>
        Task<int?> ObterIdPorDescricaoExataAsync(string descricao, int? veiculoMarcaId = null);

        /// <summary>Busca marca por descrição exata (case-insensitive).</summary>
        Task<int?> ObterMarcaIdPorDescricaoExataAsync(string descricao);

        /// <summary>Busca marca por descrição exata; se não existir, cria e retorna o id.</summary>
        Task<int> ObterOuCriarMarcaIdPorDescricaoAsync(string descricao);

        /// <summary>Busca modelo por descrição exata na marca; se não existir, cria e retorna o id.</summary>
        Task<int> ObterOuCriarModeloIdPorDescricaoAsync(string descricao, int veiculoMarcaId);
    }
}