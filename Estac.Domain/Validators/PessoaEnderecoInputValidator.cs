using Estac.Domain.Input.Endereco;
using FluentValidation;

namespace Estac.Domain.Validators
{
    public class PessoaEnderecoInputValidator : AbstractValidator<PessoaEnderecoInput>
    {
        public PessoaEnderecoInputValidator()
        {
            RuleFor(x => x.Cep)
                .NotEmpty().WithMessage("Endereço: CEP é obrigatório.")
                .MaximumLength(10);

            RuleFor(x => x.Logradouro)
                .NotEmpty().WithMessage("Endereço: logradouro é obrigatório.")
                .MaximumLength(200);

            RuleFor(x => x.Numero)
                .NotEmpty().WithMessage("Endereço: número é obrigatório.")
                .MaximumLength(20);

            RuleFor(x => x.Bairro)
                .NotEmpty().WithMessage("Endereço: bairro é obrigatório.")
                .MaximumLength(100);

            RuleFor(x => x.Cidade)
                .NotEmpty().WithMessage("Endereço: cidade é obrigatória.")
                .MaximumLength(100);

            RuleFor(x => x.Estado)
                .NotEmpty().WithMessage("Endereço: estado (UF) é obrigatório.")
                .Length(2).WithMessage("Endereço: UF deve ter 2 caracteres.");
        }
    }
}
