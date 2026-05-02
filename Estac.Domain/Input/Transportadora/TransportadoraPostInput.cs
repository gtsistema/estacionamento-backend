using Estac.Domain.Input.Pessoa;
using Estac.Domain.Validators;
using FluentValidation.Results;

namespace Estac.Domain.Input.Transportadora
{
    public class TransportadoraPostInput
    {
        public int Id { get; set; }
        public PessoaInput PessoaJuridica { get; set; } = new PessoaInput();

        public static ValidationResult Validar(TransportadoraPostInput input) =>
            new TransportadoraPostInputValidator().Validate(input);
    }
}