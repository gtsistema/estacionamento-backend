using Estac.Domain.Input.ConfiguracaoCobranca;
using FluentValidation;

namespace Estac.Domain.Validators
{
    public class ConfiguracaoCobrancaPutInputValidator : AbstractValidator<ConfiguracaoCobrancaPutInput>
    {
        public ConfiguracaoCobrancaPutInputValidator()
        {
            Include(new ConfiguracaoCobrancaPostInputValidator());

            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Identificador da configuração é obrigatório.");
        }
    }
}
