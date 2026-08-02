using Estac.Domain.Input.Fatura;
using Estac.Domain.Input.Faturamento;
using Estac.Domain.Models;
using Estac.Domain.Output.Fatura;
using Estac.Domain.Output.Faturamento;
using Estac.Domain.Shared;

namespace Estac.Domain.Interface.Repositories
{
    public interface IFaturaRepositories : IBaseRepositories<Fatura>
    {
        Task<Fatura> SelecionarPorIdCompleto(int id);
        Task<PagedResult<FaturaSearchOutput>> Paginar(FaturaFilterInput input);
        Task<FaturaVisaoGeralOutput> ObterVisaoGeral(FaturaFilterInput input);
        Task<bool> ExisteNumeroAsync(string numero, int? ignorarId = null);
        Task<IList<int>> ObterEntradaSaidaJaFaturadas(IEnumerable<int> entradaSaidaIds);
        Task Remove(int id);

        /// <summary>Agendamentos vencidos até a referência, com as regras de cobrança embutidas.</summary>
        Task<IList<AgendamentoFaturamentoOutput>> SelecionarAgendamentosPendentes(DateTime referencia);

        /// <summary>Agendamento ativo de GerarFaturamento para o par transportadora/estacionamento.</summary>
        Task<AgendamentoFaturamentoOutput> SelecionarAgendamentoParaGeracao(
            int transportadoraId,
            int estacionamentoId);

        /// <summary>Movimentos encerrados no período e ainda não vinculados a uma fatura ativa.</summary>
        Task<LoteFaturavelOutput> SelecionarMovimentosFaturaveis(EntradaSaidaFaturavelFilterInput input);
    }
}
