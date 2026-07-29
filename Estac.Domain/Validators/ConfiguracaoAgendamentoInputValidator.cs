using Estac.Domain.Input.ConfiguracaoCobranca;
using Estac.Domain.Models.Enuns;
using FluentValidation;

namespace Estac.Domain.Validators
{
    public class ConfiguracaoAgendamentoInputValidator : AbstractValidator<ConfiguracaoAgendamentoInput>
    {
        public ConfiguracaoAgendamentoInputValidator()
        {
            RuleFor(x => x.TipoJob)
                .IsInEnum()
                .WithMessage("Tipo de job inválido.");

            RuleFor(x => x.Periodicidade)
                .IsInEnum()
                .WithMessage("Periodicidade inválida.");

            RuleFor(x => x.Intervalo)
                .GreaterThan(0)
                .WithMessage("Intervalo deve ser maior que zero.");

            RuleFor(x => x.DiaSemana)
                .NotNull()
                .When(x => x.Periodicidade == Periodicidade.Semanal)
                .WithMessage("Dia da semana é obrigatório para periodicidade semanal.");

            RuleFor(x => x.DiaSemana)
                .IsInEnum()
                .When(x => x.DiaSemana.HasValue)
                .WithMessage("Dia da semana inválido.");

            RuleFor(x => x.DiaMes)
                .NotNull()
                .When(x => x.Periodicidade == Periodicidade.Mensal)
                .WithMessage("Dia do mês é obrigatório para periodicidade mensal.");

            RuleFor(x => x.DiaMes)
                .InclusiveBetween(1, 31)
                .When(x => x.DiaMes.HasValue)
                .WithMessage("Dia do mês deve estar entre 1 e 31.");
        }
    }
}
