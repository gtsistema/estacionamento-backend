using Estac.Domain.Validators;
using FluentValidation.Results;

namespace Estac.Domain.Input.Fatura
{
    public class FaturaPutInput : FaturaPostInput
    {
        public static ValidationResult Validar(FaturaPutInput input) =>
            new FaturaPutInputValidator().Validate(input);
    }
}
