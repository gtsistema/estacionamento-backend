using Estac.Domain.Input.Transportadora;
using FluentValidation;

namespace Estac.Domain.Validators
{
    public class TransportadoraPutInputValidator : AbstractValidator<TransportadoraPutInput>
    {
        public TransportadoraPutInputValidator()
        {
            Include(new TransportadoraPostInputValidator());

            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Identificador da transportadora é obrigatório.");
        }
    }
}
