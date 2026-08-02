using Estac.Domain.Input.Faturamento;
using Estac.Domain.Output.Faturamento;

namespace Estac.Domain.Interface.Repositories
{
    public interface IFaturamentoRepositories
    {
        /// <summary>Agendamentos vencidos até a referência, com as regras de cobrança embutidas.</summary>
        Task<IList<AgendamentoFaturamentoOutput>> SelecionarAgendamentosPendentes(DateTime referencia);

        /// <summary>Movimentos encerrados no período e ainda não vinculados a uma fatura ativa.</summary>
        Task<LoteFaturavelOutput> SelecionarMovimentosFaturaveis(EntradaSaidaFaturavelFilterInput input);
    }
}
