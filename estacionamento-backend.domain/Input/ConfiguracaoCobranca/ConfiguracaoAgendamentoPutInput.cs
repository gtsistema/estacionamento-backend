using Estac.Domain.Validators;
using FluentValidation.Results;

namespace Estac.Domain.Input.ConfiguracaoCobranca
{
    /// <summary>
    /// Atualiza a execução do agendamento após geração de fatura.
    /// Se ProximaExecucao for nula, o backend calcula com base na modalidade do agendamento.
    /// </summary>
    public class ConfiguracaoAgendamentoPutInput
    {
        public Guid Id { get; set; }
        public DateTime UltimaExecucao { get; set; }
        public DateTime? ProximaExecucao { get; set; }

        public static ValidationResult Validar(ConfiguracaoAgendamentoPutInput input) =>
            new ConfiguracaoAgendamentoPutInputValidator().Validate(input);
    }
}
