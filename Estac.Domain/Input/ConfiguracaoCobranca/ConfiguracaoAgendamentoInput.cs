using Estac.Domain.Models.Enuns;
using Estac.Domain.Validators;
using FluentValidation.Results;

namespace Estac.Domain.Input.ConfiguracaoCobranca
{
    public class ConfiguracaoAgendamentoInput
    {
        public Guid Id { get; set; }
        public TipoJob TipoJob { get; set; }
        public Periodicidade Periodicidade { get; set; }
        public int Intervalo { get; set; } = 1;
        public DayOfWeek? DiaSemana { get; set; }
        public int? DiaMes { get; set; }
        public TimeSpan HoraExecucao { get; set; }
        public bool Ativo { get; set; } = true;

        public static ValidationResult Validar(ConfiguracaoAgendamentoInput input) =>
            new ConfiguracaoAgendamentoInputValidator().Validate(input);
    }
}
