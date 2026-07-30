using Estac.Domain.Models;
using Estac.Domain.Models.Enuns;

namespace Estac.Domain.Factories
{
    /// <summary>
    /// Monta o agendamento de geração de fatura a partir dos dados de cobrança.
    /// O agendamento não possui entrada própria na API: ele é sempre derivado de
    /// ModalidadeCobranca, RegraFechamento e DiaFechamento.
    /// </summary>
    public static class ConfiguracaoAgendamentoFactory
    {
        /// <summary>Horário de execução do job, por não existir campo equivalente no cadastro.</summary>
        public static readonly TimeSpan HoraExecucaoPadrao = new(1, 0, 0);

        /// <summary>Retorna null quando a configuração não deve gerar fatura automaticamente.</summary>
        public static ConfiguracaoAgendamento Criar(ConfiguracaoCobranca cobranca)
        {
            if (cobranca is null || !cobranca.GerarFaturaAutomaticamente)
                return null;

            var agendamento = new ConfiguracaoAgendamento
            {
                Id = Guid.NewGuid(),
                ConfiguracaoCobrancaId = cobranca.Id,
                TipoJob = TipoJob.GerarFaturamento,
                ModalidadeCobranca = cobranca.ModalidadeCobranca,
                Intervalo = 1,
                HoraExecucao = HoraExecucaoPadrao,
                Ativo = cobranca.Status == StatusConfiguracaoCobranca.Ativa,
                DataCadastro = DateTime.Now
            };

            AplicarModalidade(cobranca, agendamento);

            return agendamento;
        }

        private static void AplicarModalidade(ConfiguracaoCobranca cobranca, ConfiguracaoAgendamento agendamento)
        {
            switch (cobranca.ModalidadeCobranca)
            {
                case ModalidadeCobranca.Semanal:
                    agendamento.DiaSemana = DiaSemanaFechamento(cobranca);
                    break;

                case ModalidadeCobranca.Quinzenal:
                    agendamento.Intervalo = 2;
                    agendamento.DiaSemana = DiaSemanaFechamento(cobranca);
                    break;

                case ModalidadeCobranca.Mensal:
                    agendamento.DiaMes = DiaMesFechamento(cobranca);
                    break;

                case ModalidadeCobranca.Personalizado:
                    AplicarCobrancaPersonalizada(cobranca, agendamento);
                    break;
            }
        }

        /// <summary>
        /// Cobrança por data personalizada: a data de execução vem da regra de fechamento
        /// escolhida no cadastro, sem periodicidade fixa de dia da semana.
        /// </summary>
        private static void AplicarCobrancaPersonalizada(ConfiguracaoCobranca cobranca, ConfiguracaoAgendamento agendamento)
        {
            agendamento.DiaSemana = null;
            agendamento.DiaMes = DiaMesFechamento(cobranca);
        }

        /// <summary>Null indica último dia do mês, resolvido pelo job em tempo de execução.</summary>
        private static int? DiaMesFechamento(ConfiguracaoCobranca cobranca)
        {
            return cobranca.RegraFechamento == RegraFechamento.DiaFixo
                ? cobranca.DiaFechamento
                : null;
        }

        /// <summary>DiaFechamento de 1 a 7 representa domingo a sábado nas modalidades semanais.</summary>
        private static DayOfWeek? DiaSemanaFechamento(ConfiguracaoCobranca cobranca)
        {
            if (cobranca.DiaFechamento is null or < 1 or > 7)
                return null;

            return (DayOfWeek)(cobranca.DiaFechamento.Value - 1);
        }
    }
}
