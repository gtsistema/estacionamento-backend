using Estac.Domain.Input.Fatura;
using FluentValidation;

namespace Estac.Domain.Validators
{
    public class FaturaPostInputValidator : AbstractValidator<FaturaPostInput>
    {
        public FaturaPostInputValidator()
        {
            RuleFor(x => x.TransportadoraId)
                .GreaterThan(0).WithMessage("Transportadora é obrigatória.");
        }
    }
}
