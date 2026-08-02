using Estac.Domain.Input.Transportadora;
using FluentValidation;

namespace Estac.Domain.Validators
{
    public class TransportadoraPostInputValidator : AbstractValidator<TransportadoraPostInput>
    {
        public TransportadoraPostInputValidator()
        {
            RuleFor(x => x.PessoaJuridica)
                .NotNull().WithMessage("Dados da pessoa jurídica são obrigatórios.");

            RuleFor(x => x.PessoaJuridica)
                .SetValidator(new PessoaJuridicaInputValidator())
                .When(x => x.PessoaJuridica != null);

            RuleFor(x => x.ContaBancaria)
                .SetValidator(new ContaBancariaInputValidator())
                .When(x => x.ContaBancaria != null);
        }
    }
}
