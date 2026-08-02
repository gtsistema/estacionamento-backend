using Estac.Domain.Validators;
using FluentValidation.Results;

namespace Estac.Domain.Input.Transportadora
{
    public class TransportadoraPutInput : TransportadoraPostInput
    {
        public static ValidationResult Validar(TransportadoraPutInput input) =>
            new TransportadoraPutInputValidator().Validate(input);
    }
}