using Estac.Domain.Input.Fatura;
using FluentValidation;

namespace Estac.Domain.Validators
{
    public class FaturaPutInputValidator : AbstractValidator<FaturaPutInput>
    {
        public FaturaPutInputValidator()
        {
            Include(new FaturaPostInputValidator());

            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Identificador da fatura é obrigatório.");
        }
    }
}
