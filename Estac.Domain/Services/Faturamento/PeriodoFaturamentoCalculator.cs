using Estac.Domain.Models.Enuns;

namespace Estac.Domain.Services.Faturamento
{
    /// <summary>
    /// Calcula janela de competência e próxima execução do agendamento de faturamento.
    /// </summary>
    public static class PeriodoFaturamentoCalculator
    {
        public static (DateTime PeriodoInicio, DateTime PeriodoFim) CalcularPeriodo(
            ModalidadeCobranca modalidade,
            int intervalo,
            DateTime? ultimaExecucao,
            DateTime? proximaExecucao,
            DateTime? ultimoPeriodoFaturado,
            DateTime referencia)
        {
            var intervaloSeguro = Math.Max(1, intervalo);
            var periodoFim = proximaExecucao ?? referencia;

            DateTime periodoInicio;
            if (ultimoPeriodoFaturado.HasValue)
                periodoInicio = ultimoPeriodoFaturado.Value;
            else if (ultimaExecucao.HasValue)
                periodoInicio = ultimaExecucao.Value;
            else
                periodoInicio = SubtrairIntervalo(periodoFim, modalidade, intervaloSeguro);

            if (periodoInicio > periodoFim)
                periodoInicio = SubtrairIntervalo(periodoFim, modalidade, intervaloSeguro);

            return (periodoInicio, periodoFim);
        }

        public static DateTime CalcularProximaExecucao(
            ModalidadeCobranca modalidade,
            int intervalo,
            DayOfWeek? diaSemana,
            int? diaMes,
            TimeSpan horaExecucao,
            DateTime aPartirDe)
        {
            var intervaloSeguro = Math.Max(1, intervalo);
            var baseDate = aPartirDe.Date.Add(horaExecucao);

            return modalidade switch
            {
                ModalidadeCobranca.Diaria => baseDate.AddDays(intervaloSeguro),
                ModalidadeCobranca.Semanal => ProximaPorDiaSemana(baseDate, diaSemana, intervaloSeguro),
                ModalidadeCobranca.Quinzenal => ProximaPorDiaSemana(baseDate, diaSemana, intervaloSeguro),
                ModalidadeCobranca.Mensal => ProximaPorDiaMes(baseDate, diaMes, intervaloSeguro),
                ModalidadeCobranca.Personalizado => ProximaPorDiaMes(baseDate, diaMes, intervaloSeguro),
                _ => baseDate.AddDays(intervaloSeguro)
            };
        }

        public static bool AtendeRegrasAgendamento(
            ModalidadeCobranca modalidade,
            DayOfWeek? diaSemana,
            int? diaMes,
            TimeSpan horaExecucao,
            DateTime? proximaExecucao,
            DateTime referencia)
        {
            if (proximaExecucao.HasValue)
                return proximaExecucao.Value <= referencia;

            if (referencia.TimeOfDay < horaExecucao)
                return false;

            return modalidade switch
            {
                ModalidadeCobranca.Diaria => true,
                ModalidadeCobranca.Semanal or ModalidadeCobranca.Quinzenal =>
                    !diaSemana.HasValue || referencia.DayOfWeek == diaSemana.Value,
                ModalidadeCobranca.Mensal or ModalidadeCobranca.Personalizado =>
                    !diaMes.HasValue || referencia.Day == ResolverDiaMes(referencia, diaMes),
                _ => true
            };
        }

        private static DateTime SubtrairIntervalo(DateTime fim, ModalidadeCobranca modalidade, int intervalo) =>
            modalidade switch
            {
                ModalidadeCobranca.Diaria => fim.AddDays(-intervalo),
                ModalidadeCobranca.Semanal => fim.AddDays(-7 * intervalo),
                ModalidadeCobranca.Quinzenal => fim.AddDays(-7 * intervalo),
                ModalidadeCobranca.Mensal => fim.AddMonths(-intervalo),
                ModalidadeCobranca.Personalizado => fim.AddMonths(-intervalo),
                _ => fim.AddDays(-intervalo)
            };

        private static DateTime ProximaPorDiaSemana(DateTime aPartirDe, DayOfWeek? diaSemana, int intervaloSemanas)
        {
            var alvo = diaSemana ?? aPartirDe.DayOfWeek;
            var candidato = aPartirDe.AddDays(1);
            while (candidato.DayOfWeek != alvo)
                candidato = candidato.AddDays(1);

            if (intervaloSemanas > 1)
                candidato = candidato.AddDays(7 * (intervaloSemanas - 1));

            return candidato.Date.Add(aPartirDe.TimeOfDay);
        }

        private static DateTime ProximaPorDiaMes(DateTime aPartirDe, int? diaMes, int intervaloMeses)
        {
            var mes = aPartirDe.AddMonths(intervaloMeses);
            var dia = ResolverDiaMes(mes, diaMes);
            return new DateTime(mes.Year, mes.Month, dia, aPartirDe.Hour, aPartirDe.Minute, aPartirDe.Second);
        }

        private static int ResolverDiaMes(DateTime referencia, int? diaMes)
        {
            var ultimoDia = DateTime.DaysInMonth(referencia.Year, referencia.Month);
            if (!diaMes.HasValue || diaMes.Value <= 0)
                return ultimoDia;

            return Math.Min(diaMes.Value, ultimoDia);
        }
    }
}
