using Estac.Domain.Input.PessoaContato;
using Estac.Domain.Extensions;
using FluentValidation;

namespace Estac.Domain.Validators
{
    public class PessoaContatoInputValidator : AbstractValidator<PessoaContatoInput>
    {
        public PessoaContatoInputValidator()
        {
            RuleFor(x => x.Descricao)
                .MaximumLength(200);

            RuleFor(x => x.Cpf)
                .MaximumLength(14);

            RuleFor(x => x.Telefone)
                .MaximumLength(20);

            RuleFor(x => x.Telefone)
                .Must(x => string.IsNullOrWhiteSpace(x) || x.TelefoneComDddValido())
                .WithMessage("Contato: telefone deve conter DDD válido (10 ou 11 dígitos).");

            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("Contato: e-mail inválido.")
                .MaximumLength(150)
                .When(x => !string.IsNullOrWhiteSpace(x.Email));

            RuleFor(x => x.Observacao)
                .MaximumLength(500);
        }
    }
}
