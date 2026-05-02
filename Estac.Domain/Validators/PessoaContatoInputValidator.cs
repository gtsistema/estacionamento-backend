using Estac.Domain.Input.PessoaContato;
using FluentValidation;

namespace Estac.Domain.Validators
{
    public class PessoaContatoInputValidator : AbstractValidator<PessoaContatoInput>
    {
        public PessoaContatoInputValidator()
        {
            RuleFor(x => x.Numero)
                .NotEmpty().WithMessage("Contato: número é obrigatório.")
                .MaximumLength(30);
        }
    }
}
