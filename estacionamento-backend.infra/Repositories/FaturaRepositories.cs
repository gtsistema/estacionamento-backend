using System.Linq.Expressions;
using Estac.Domain.Extensions;
using Estac.Domain.Input.Fatura;
using Estac.Domain.Input.Faturamento;
using Estac.Domain.Interface.Repositories;
using Estac.Domain.Models;
using Estac.Domain.Models.Enuns;
using Estac.Domain.Output.Fatura;
using Estac.Domain.Output.Faturamento;
using Estac.Domain.Shared;
using Estac.Infra.Context;
using Estac.Infra.Repository;
using Microsoft.EntityFrameworkCore;

namespace Estac.Infra.Repositories
{
    public class FaturaRepositories : BaseRepositories<Fatura>, IFaturaRepositories
    {
        public const int TamanhoLotePadrao = 500;
        public const int TamanhoLoteMaximo = 5000;

        private readonly DbSet<Fatura> _dataset;
        private readonly DbSet<ConfiguracaoAgendamento> _agendamentos;
        private readonly DbSet<EntradaSaida> _movimentos;

        public FaturaRepositories(GtsContext context) : base(context)
        {
            _dataset = context.Set<Fatura>();
            _agendamentos = context.Set<ConfiguracaoAgendamento>();
            _movimentos = context.Set<EntradaSaida>();
        }

        public async Task<Fatura> SelecionarPorIdCompleto(int id)
        {
            return await _dataset
                .AsNoTracking()
                .Include(x => x.Transportadora)
                .Include(x => x.Estacionamento)
                .Include(x => x.ConfiguracaoCobranca)
                .Include(x => x.Itens)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IList<int>> ObterEntradaSaidaJaFaturadas(IEnumerable<int> entradaSaidaIds)
        {
            var ids = entradaSaidaIds?.Distinct().ToList() ?? new List<int>();
            if (ids.Count == 0)
                return new List<int>();

            return await _context.Set<EntradaSaida>()
                .AsNoTracking()
                .Where(movimento => ids.Contains(movimento.Id) && movimento.Faturado)
                .Select(movimento => movimento.Id)
                .Union(
                    _context.Set<FaturaItem>()
                        .AsNoTracking()
                        .Where(item => ids.Contains(item.EntradaSaidaId)
                            && item.Fatura.Status != StatusFatura.Cancelada)
                        .Select(item => item.EntradaSaidaId))
                .Distinct()
                .ToListAsync();
        }

        public async Task<PagedResult<FaturaSearchOutput>> Paginar(FaturaFilterInput input)
        {
            var query = AplicarFiltros(_dataset.AsNoTracking()
                .Include(x => x.Transportadora)
                .Include(x => x.Estacionamento), input);

            return await query
                .OrderByDescending(x => x.DataEmissao)
                .ThenByDescending(x => x.Id)
                .Select(x => new FaturaSearchOutput
                {
                    Id = x.Id,
                    Numero = x.Numero,
                    TransportadoraId = x.TransportadoraId,
                    TransportadoraNome = x.Transportadora.Descricao,
                    EstacionamentoId = x.EstacionamentoId,
                    EstacionamentoNome = x.Estacionamento.Descricao,
                    Status = x.Status,
                    ModalidadeRecebimento = x.ModalidadeRecebimento,
                    ValorTotal = x.ValorTotal,
                    ValorRecebido = x.ValorRecebido,
                    ValorEmAberto = x.ValorTotal - x.ValorRecebido,
                    DataEmissao = x.DataEmissao,
                    DataVencimento = x.DataVencimento,
                    DataPagamento = x.DataPagamento
                })
                .GetPaged(input.NumeroPagina, input.TamanhoPagina);
        }

        public async Task<FaturaVisaoGeralOutput> ObterVisaoGeral(FaturaFilterInput input)
        {
            var hoje = DateTime.Today;
            var query = AplicarFiltros(_dataset.AsNoTracking()
                .Include(x => x.Transportadora)
                .Include(x => x.Estacionamento), input)
                .Where(x => x.Status != StatusFatura.Cancelada);

            var faturas = await query.ToListAsync();

            var emAbertoStatuses = new[]
            {
                StatusFatura.EmAberto,
                StatusFatura.Parcial,
                StatusFatura.AguardandoEnvio,
                StatusFatura.Vencido
            };

            var resultado = new FaturaVisaoGeralOutput
            {
                TotalAReceber = faturas.Sum(x => x.ValorTotal),
                Recebido = faturas.Sum(x => x.ValorRecebido),
                EmAberto = faturas
                    .Where(x => emAbertoStatuses.Contains(x.Status))
                    .Sum(x => Math.Max(0, x.ValorTotal - x.ValorRecebido)),
                Vencido = faturas
                    .Where(x => x.Status == StatusFatura.Vencido
                        || (x.DataVencimento.Date < hoje
                            && x.Status is StatusFatura.EmAberto or StatusFatura.Parcial or StatusFatura.AguardandoEnvio))
                    .Sum(x => Math.Max(0, x.ValorTotal - x.ValorRecebido)),
                AVencer = faturas
                    .Where(x => x.DataVencimento.Date >= hoje
                        && x.Status is StatusFatura.EmAberto or StatusFatura.Parcial or StatusFatura.AguardandoEnvio)
                    .Sum(x => Math.Max(0, x.ValorTotal - x.ValorRecebido)),
                FaturasEmitidas = faturas.Count,
                FaturasVencidas = faturas.Count(x =>
                    x.Status == StatusFatura.Vencido
                    || (x.DataVencimento.Date < hoje
                        && x.Status is StatusFatura.EmAberto or StatusFatura.Parcial or StatusFatura.AguardandoEnvio)),
                FaturasPorStatus = faturas
                    .GroupBy(x => x.Status)
                    .Select(g => new FaturaStatusResumoOutput
                    {
                        Status = g.Key,
                        Quantidade = g.Count(),
                        Valor = g.Sum(x => x.ValorTotal)
                    })
                    .OrderBy(x => x.Status)
                    .ToList(),
                RecebimentosPorModalidade = faturas
                    .Where(x => x.ModalidadeRecebimento.HasValue && x.ValorRecebido > 0)
                    .GroupBy(x => x.ModalidadeRecebimento!.Value)
                    .Select(g => new FaturaModalidadeResumoOutput
                    {
                        Modalidade = g.Key,
                        Quantidade = g.Count(),
                        Valor = g.Sum(x => x.ValorRecebido)
                    })
                    .OrderByDescending(x => x.Valor)
                    .ToList(),
                EvolucaoFaturamento = faturas
                    .GroupBy(x => new { x.DataEmissao.Year, x.DataEmissao.Month })
                    .Select(g => new FaturaEvolucaoMensalOutput
                    {
                        Ano = g.Key.Year,
                        Mes = g.Key.Month,
                        Valor = g.Sum(x => x.ValorTotal)
                    })
                    .OrderBy(x => x.Ano)
                    .ThenBy(x => x.Mes)
                    .ToList()
            };

            return resultado;
        }

        public Task<bool> ExisteNumeroAsync(string numero, int? ignorarId = null)
        {
            if (string.IsNullOrWhiteSpace(numero))
                return Task.FromResult(false);

            return _dataset
                .AsNoTracking()
                .AnyAsync(x =>
                    x.Numero == numero
                    && (!ignorarId.HasValue || x.Id != ignorarId.Value));
        }

        public override async Task<Fatura> Alterar(Fatura item)
        {
            try
            {
                var result = await _dataset.SingleOrDefaultAsync(p => p.Id == item.Id);
                if (result == null)
                    return null;

                item.DataCriacao = result.DataCriacao;
                item.DataAtualizacao = DateTime.Now;
                _context.Entry(result).CurrentValues.SetValues(item);
            }
            catch (DbUpdateException)
            {
                throw;
            }

            return item;
        }

        public async Task Remove(int id)
        {
            try
            {
                var fatura = await _dataset.FirstOrDefaultAsync(x => x.Id == id);
                if (fatura is null)
                    return;

                _context.Remove(fatura);
            }
            catch (DbUpdateException)
            {
                throw;
            }
        }

        public async Task<IList<AgendamentoFaturamentoOutput>> SelecionarAgendamentosPendentes(DateTime referencia)
        {
            return await QueryAgendamentoBase()
                .Where(agendamento => agendamento.ProximaExecucao == null || agendamento.ProximaExecucao <= referencia)
                .OrderBy(agendamento => agendamento.ProximaExecucao)
                .Select(ProjetarAgendamento())
                .ToListAsync();
        }

        public async Task<AgendamentoFaturamentoOutput> SelecionarAgendamentoParaGeracao(
            int transportadoraId,
            int estacionamentoId)
        {
            return await QueryAgendamentoBase()
                .Where(agendamento =>
                    agendamento.ConfiguracaoCobranca.TransportadoraId == transportadoraId
                    && agendamento.ConfiguracaoCobranca.EstacionamentoId == estacionamentoId)
                .Select(ProjetarAgendamento())
                .FirstOrDefaultAsync();
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

        private IQueryable<ConfiguracaoAgendamento> QueryAgendamentoBase() =>
            _agendamentos
                .AsNoTracking()
                .Where(agendamento => agendamento.Ativo
                    && agendamento.TipoJob == TipoJob.GerarFaturamento
                    && agendamento.ConfiguracaoCobranca.GerarFaturaAutomaticamente
                    && agendamento.ConfiguracaoCobranca.Status == StatusConfiguracaoCobranca.Ativa);

        private Expression<Func<ConfiguracaoAgendamento, AgendamentoFaturamentoOutput>> ProjetarAgendamento() =>
            agendamento => new AgendamentoFaturamentoOutput
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
                UltimoPeriodoFaturado = _dataset
                    .Where(fatura => fatura.ConfiguracaoCobrancaId == agendamento.ConfiguracaoCobrancaId
                        && fatura.Status != StatusFatura.Cancelada)
                    .Max(fatura => (DateTime?)fatura.PeriodoFim),
                Cobranca = new RegrasCobrancaOutput
                {
                    RegraFechamento = agendamento.ConfiguracaoCobranca.RegraFechamento,
                    DiaFechamento = agendamento.ConfiguracaoCobranca.DiaFechamento,
                    DataCobranca = agendamento.ConfiguracaoCobranca.DataCobranca,
                    PrazoVencimentoDias = agendamento.ConfiguracaoCobranca.PrazoVencimentoDias,
                    ValorEstacionamento = agendamento.ConfiguracaoCobranca.ValorEstacionamento,
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
            };

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

        private static IQueryable<Fatura> AplicarFiltros(IQueryable<Fatura> query, FaturaFilterInput input)
        {
            if (input.TransportadoraId.HasValue)
                query = query.Where(x => x.TransportadoraId == input.TransportadoraId.Value);

            if (input.EstacionamentoId.HasValue)
                query = query.Where(x => x.EstacionamentoId == input.EstacionamentoId.Value);

            if (input.Status.HasValue)
                query = query.Where(x => x.Status == input.Status.Value);

            if (input.ModalidadeRecebimento.HasValue)
                query = query.Where(x => x.ModalidadeRecebimento == input.ModalidadeRecebimento.Value);

            if (!string.IsNullOrWhiteSpace(input.Numero))
            {
                var numero = input.Numero.Trim();
                query = query.Where(x => x.Numero.Contains(numero));
            }

            if (!string.IsNullOrWhiteSpace(input.Descricao))
            {
                var termo = input.Descricao.Trim().ToLower();
                query = query.Where(x =>
                    (x.Numero != null && x.Numero.ToLower().Contains(termo))
                    || (x.Observacao != null && x.Observacao.ToLower().Contains(termo))
                    || (x.Transportadora != null && x.Transportadora.Descricao != null && x.Transportadora.Descricao.ToLower().Contains(termo))
                    || (x.Estacionamento != null && x.Estacionamento.Descricao != null && x.Estacionamento.Descricao.ToLower().Contains(termo)));
            }

            if (input.DataInicial.HasValue)
                query = query.Where(x => x.DataEmissao.Date >= input.DataInicial.Value.Date);

            if (input.DataFinal.HasValue)
                query = query.Where(x => x.DataEmissao.Date <= input.DataFinal.Value.Date);

            return query;
        }
    }
}
