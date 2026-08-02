using Estac.Domain.Input.ConfiguracaoCobranca;
using FluentValidation;

namespace Estac.Domain.Validators
{
    public class ConfiguracaoAgendamentoPutInputValidator : AbstractValidator<ConfiguracaoAgendamentoPutInput>
    {
        public ConfiguracaoAgendamentoPutInputValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Identificador do agendamento é obrigatório.");

            RuleFor(x => x.UltimaExecucao)
                .NotEmpty().WithMessage("Última execução é obrigatória.");

            RuleFor(x => x.ProximaExecucao)
                .GreaterThan(x => x.UltimaExecucao)
                .When(x => x.ProximaExecucao.HasValue)
                .WithMessage("Próxima execução deve ser posterior à última execução.");
        }
    }
}
