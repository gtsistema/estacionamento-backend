using Estac.Domain.Input.Faturamento;
using Estac.Domain.Interface.Repositories;
using Estac.Domain.Models;
using Estac.Domain.Models.Enuns;
using Estac.Domain.Output.Faturamento;
using Estac.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace Estac.Infra.Repositories
{
    /// <summary>
    /// Consultas de leitura do job de faturamento em duas fases:
    /// agendamentos vencidos e movimentos elegíveis por keyset.
    /// </summary>
    public class FaturamentoRepositories : IFaturamentoRepositories
    {
        public const int TamanhoLotePadrao = 500;
        public const int TamanhoLoteMaximo = 5000;

        private readonly GtsContext _context;
        private readonly DbSet<ConfiguracaoAgendamento> _agendamentos;
        private readonly DbSet<EntradaSaida> _movimentos;

        public FaturamentoRepositories(GtsContext context)
        {
            _context = context;
            _agendamentos = context.Set<ConfiguracaoAgendamento>();
            _movimentos = context.Set<EntradaSaida>();
        }

        public async Task<IList<AgendamentoFaturamentoOutput>> SelecionarAgendamentosPendentes(DateTime referencia)
        {
            return await _agendamentos
                .AsNoTracking()
                .Where(agendamento => agendamento.Ativo
                    && agendamento.TipoJob == TipoJob.GerarFaturamento
                    && (agendamento.ProximaExecucao == null || agendamento.ProximaExecucao <= referencia)
                    && agendamento.ConfiguracaoCobranca.GerarFaturaAutomaticamente
                    && agendamento.ConfiguracaoCobranca.Status == StatusConfiguracaoCobranca.Ativa)
                .OrderBy(agendamento => agendamento.ProximaExecucao)
                .Select(agendamento => new AgendamentoFaturamentoOutput
                {
                    ConfiguracaoAgendamentoId = agendamento.Id,
                    ConfiguracaoCobrancaId = agendamento.ConfiguracaoCobrancaId,
                    TransportadoraId = agendamento.ConfiguracaoCobranca.TransportadoraId,
                    TransportadoraNome = agendamento.ConfiguracaoCobranca.Transportadora.Descricao,
                    EstacionamentoId = agendamento.ConfiguracaoCobranca.EstacionamentoId,
                    EstacionamentoNome = agendamento.ConfiguracaoCobranca.Estacionamento.Descricao,
                    ModalidadeCobranca = agendamento.ModalidadeCobranca,
                    Intervalo = agendamento.Intervalo,
                    DiaSemana = agendamento.DiaSemana,
                    DiaMes = agendamento.DiaMes,
                    HoraExecucao = agendamento.HoraExecucao,
                    UltimaExecucao = agendamento.UltimaExecucao,
                    ProximaExecucao = agendamento.ProximaExecucao,
                    UltimoPeriodoFaturado = _context.Set<Fatura>()
                        .Where(fatura => fatura.ConfiguracaoCobrancaId == agendamento.ConfiguracaoCobrancaId
                            && fatura.Status != StatusFatura.Cancelada)
                        .Max(fatura => (DateTime?)fatura.PeriodoFim),
                    Cobranca = new RegrasCobrancaOutput
                    {
                        RegraFechamento = agendamento.ConfiguracaoCobranca.RegraFechamento,
                        DiaFechamento = agendamento.ConfiguracaoCobranca.DiaFechamento,
                        DataCobranca = agendamento.ConfiguracaoCobranca.DataCobranca,
                        PrazoVencimentoDias = agendamento.ConfiguracaoCobranca.PrazoVencimentoDias,
                        ValorEstacionamento = agendamento.ConfiguracaoCobranca.ValorEstadia,
                        CobrarLavagem = agendamento.ConfiguracaoCobranca.CobrarLavagem,
                        ValorLavagem = agendamento.ConfiguracaoCobranca.ValorLavagem,
                        CobrarPernoite = agendamento.ConfiguracaoCobranca.CobrarPernoite,
                        ValorPernoite = agendamento.ConfiguracaoCobranca.ValorPernoite,
                        CobrarServicosExtras = agendamento.ConfiguracaoCobranca.CobrarServicosExtras,
                        ValorServicosExtras = agendamento.ConfiguracaoCobranca.ValorServicosExtras,
                        ConsiderarBeneficioAbastecimento = agendamento.ConfiguracaoCobranca.ConsiderarBeneficioAbastecimento,
                        ValorBeneficioAbastecimento = agendamento.ConfiguracaoCobranca.ValorBeneficioAbastecimento,
                        AplicarMulta = agendamento.ConfiguracaoCobranca.AplicarMulta,
                        MultaPercentual = agendamento.ConfiguracaoCobranca.MultaPercentual,
                        AplicarJuros = agendamento.ConfiguracaoCobranca.AplicarJuros,
                        JurosPercentual = agendamento.ConfiguracaoCobranca.JurosPercentual,
                        AplicarDescontoFixo = agendamento.ConfiguracaoCobranca.AplicarDescontoFixo,
                        ValorDescontoFixo = agendamento.ConfiguracaoCobranca.ValorDescontoFixo,
                        AplicarAcrescimoFixo = agendamento.ConfiguracaoCobranca.AplicarAcrescimoFixo,
                        ValorAcrescimoFixo = agendamento.ConfiguracaoCobranca.ValorAcrescimoFixo,
                        AgruparPorPlaca = agendamento.ConfiguracaoCobranca.AgruparPorPlaca,
                        AgruparPorPeriodo = agendamento.ConfiguracaoCobranca.AgruparPorPeriodo,
                        AgruparPorTransportadora = agendamento.ConfiguracaoCobranca.AgruparPorTransportadora,
                        EnvioAutomaticoEmail = agendamento.ConfiguracaoCobranca.EnvioAutomaticoEmail,
                        EmailFinanceiro = agendamento.ConfiguracaoCobranca.EmailFinanceiro
                    }
                })
                .ToListAsync();
        }

        public async Task<LoteFaturavelOutput> SelecionarMovimentosFaturaveis(EntradaSaidaFaturavelFilterInput input)
        {
            var tamanho = Math.Clamp(
                input.Tamanho <= 0 ? TamanhoLotePadrao : input.Tamanho,
                1,
                TamanhoLoteMaximo);

            var query = MontarQueryMovimentosFaturaveis(input);

            var itens = await query
                .OrderBy(movimento => movimento.Id)
                .Take(tamanho + 1)
                .Select(movimento => new EntradaSaidaFaturavelOutput
                {
                    Id = movimento.Id,
                    EstacionamentoId = movimento.EstacionamentoId,
                    TransportadoraId = movimento.TransportadoraId,
                    VeiculoId = movimento.VeiculoId,
                    Placa = movimento.Veiculo.Placa,
                    MotoristaId = movimento.MotoristaId,
                    MotoristaNome = movimento.Motorista.Pessoa != null
                        ? movimento.Motorista.Pessoa.NomeRazaoSocial
                        : movimento.Motorista.Descricao,
                    DataHoraEntrada = movimento.DataHoraEntrada,
                    DataHoraSaida = movimento.DataHoraSaida,
                    TempoPermanenciaMinutos = movimento.TempoPermanenciaMinutos,
                    TempoTotalSuspensaoMinutos = movimento.TempoTotalSuspensaoMinutos,
                    Faturado = movimento.Faturado,
                    DataFaturado = movimento.DataFaturado
                })
                .ToListAsync();

            var possuiMais = itens.Count > tamanho;
            if (possuiMais)
                itens.RemoveAt(itens.Count - 1);

            return new LoteFaturavelOutput
            {
                Itens = itens,
                PossuiMais = possuiMais,
                ProximoCursor = possuiMais && itens.Count > 0 ? itens[^1].Id : null
            };
        }

        private IQueryable<EntradaSaida> MontarQueryMovimentosFaturaveis(EntradaSaidaFaturavelFilterInput input)
        {
            var query = _movimentos
                .AsNoTracking()
                .Where(movimento => movimento.EstacionamentoId == input.EstacionamentoId
                    && movimento.TransportadoraId == input.TransportadoraId
                    && movimento.Finalizado
                    && !movimento.Faturado
                    && movimento.Status == EntradaSaidaStatus.Saida
                    && movimento.DataHoraSaida != null
                    && movimento.DataHoraSaida >= input.PeriodoInicio
                    && movimento.DataHoraSaida < input.PeriodoFim
                    && !_context.Set<FaturaItem>().Any(item =>
                        item.EntradaSaidaId == movimento.Id
                        && item.Fatura.Status != StatusFatura.Cancelada));

            if (input.UltimoId.HasValue)
                query = query.Where(movimento => movimento.Id > input.UltimoId.Value);

            return query;
        }
    }
}
