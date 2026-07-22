using Estac.Domain.Validators;
using FluentValidation.Results;

namespace Estac.Domain.Input.ConfiguracaoCobranca
{
    public class ConfiguracaoCobrancaPutInput : ConfiguracaoCobrancaPostInput
    {
        public static ValidationResult Validar(ConfiguracaoCobrancaPutInput input) =>
            new ConfiguracaoCobrancaPutInputValidator().Validate(input);
    }
}
