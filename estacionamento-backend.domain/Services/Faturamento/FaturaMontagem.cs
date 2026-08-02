using Estac.Domain.Models;
using Estac.Domain.Models.Enuns;
using Estac.Domain.Output.Faturamento;

namespace Estac.Domain.Services.Faturamento
{
    /// <summary>Monta entidade Fatura + itens a partir do agendamento e dos movimentos elegíveis.</summary>
    public static class FaturaMontagem
    {
        public static Fatura Montar(
            AgendamentoFaturamentoOutput agendamento,
            IEnumerable<EntradaSaidaFaturavelOutput> movimentos,
            DateTime agora,
            DateTime periodoInicio,
            DateTime periodoFim)
        {
            ArgumentNullException.ThrowIfNull(agendamento);
            ArgumentNullException.ThrowIfNull(agendamento.Cobranca);
            ArgumentNullException.ThrowIfNull(movimentos);

            var cobranca = agendamento.Cobranca;
            var itens = movimentos
                .Where(m => m.DataHoraSaida.HasValue)
                .GroupBy(m => m.Id)
                .Select(g => MontarItem(g.First(), cobranca))
                .ToList();

            var somaItens = itens.Sum(i => i.ValorTotal);
            var desconto = cobranca.AplicarDescontoFixo ? cobranca.ValorDescontoFixo : 0m;
            var acrescimo = cobranca.AplicarAcrescimoFixo ? cobranca.ValorAcrescimoFixo : 0m;
            var valorTotal = Math.Max(0, somaItens + acrescimo - desconto);

            return new Fatura
            {
                TransportadoraId = agendamento.TransportadoraId,
                EstacionamentoId = agendamento.EstacionamentoId,
                ConfiguracaoCobrancaId = agendamento.ConfiguracaoCobrancaId,
                Status = StatusFatura.AguardandoEnvio,
                ValorTotal = valorTotal,
                ValorRecebido = 0,
                ValorDesconto = desconto,
                ValorAcrescimo = acrescimo,
                ValorJuros = 0,
                ValorMulta = 0,
                DataEmissao = agora,
                DataVencimento = agora.Date.AddDays(Math.Max(0, cobranca.PrazoVencimentoDias)),
                PeriodoInicio = periodoInicio,
                PeriodoFim = periodoFim,
                EmailEnvio = cobranca.EmailFinanceiro,
                Observacao = $"Geração automática — {agendamento.ModalidadeCobranca} — {itens.Count} movimento(s).",
                Descricao = $"Fatura {agendamento.TransportadoraNome}/{agendamento.EstacionamentoNome}",
                Numero = GerarNumeroProvisorio(agora),
                DataCriacao = agora,
                Itens = itens
            };
        }

        private static FaturaItem MontarItem(EntradaSaidaFaturavelOutput movimento, RegrasCobrancaOutput cobranca)
        {
            var valorEstacionamento = cobranca.ValorEstacionamento ?? 0m;
            var valorLavagem = cobranca.CobrarLavagem ? cobranca.ValorLavagem ?? 0m : 0m;
            var valorPernoite = cobranca.CobrarPernoite ? cobranca.ValorPernoite ?? 0m : 0m;
            var valorExtras = cobranca.CobrarServicosExtras ? cobranca.ValorServicosExtras ?? 0m : 0m;
            var beneficio = cobranca.ConsiderarBeneficioAbastecimento
                ? cobranca.ValorBeneficioAbastecimento ?? 0m
                : 0m;

            var total = Math.Max(0, valorEstacionamento + valorLavagem + valorPernoite + valorExtras - beneficio);

            return new FaturaItem
            {
                EntradaSaidaId = movimento.Id,
                Placa = movimento.Placa,
                DataHoraEntrada = movimento.DataHoraEntrada,
                DataHoraSaida = movimento.DataHoraSaida!.Value,
                TempoPermanenciaMinutos = movimento.TempoPermanenciaMinutos,
                ValorEstacionamento = valorEstacionamento,
                ValorLavagem = valorLavagem,
                ValorPernoite = valorPernoite,
                ValorServicosExtras = valorExtras,
                ValorBeneficioAbastecimento = beneficio,
                ValorTotal = total,
                Descricao = $"Movimento {movimento.Id} — {movimento.Placa}",
                DataCriacao = DateTime.Now
            };
        }

        private static string GerarNumeroProvisorio(DateTime dataEmissao) =>
            $"FAT-{dataEmissao:yyyyMM}-{Guid.NewGuid().ToString("N")[..8].ToUpperInvariant()}";
    }
}
