using Estac.Domain.Input.Pessoa;
using Estac.Domain.Models.Enuns;
using FluentValidation;

namespace Estac.Domain.Validators
{
    /// <summary>
    /// Regras comuns para cadastro de pessoa jurídica (ex.: transportadora, empresas).
    /// </summary>
    public class PessoaJuridicaInputValidator : AbstractValidator<PessoaInput>
    {
        public PessoaJuridicaInputValidator()
        {
            RuleFor(x => x.TipoPessoa)
                .Equal(TipoPessoa.Juridica)
                .When(x => x.TipoPessoa != default)
                .WithMessage("Tipo de pessoa deve ser jurídica.");

            RuleFor(x => x.NomeRazaoSocial)
                .NotEmpty().WithMessage("Razão social é obrigatória.")
                .MaximumLength(200);

            RuleFor(x => x.NomeFantasia)
                .NotEmpty().WithMessage("Nome fantasia é obrigatório.")
                .MaximumLength(200);

            RuleFor(x => x.Documento)
                .NotEmpty().WithMessage("CNPJ é obrigatório.")
                .Must(DocumentoBrasil.CnpjValido).WithMessage("CNPJ inválido.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("E-mail é obrigatório.")
                .EmailAddress().WithMessage("E-mail inválido.")
                .MaximumLength(200);

            RuleForEach(x => x.Enderecos)
                .SetValidator(new PessoaEnderecoInputValidator())
                .When(x => x.Enderecos != null && x.Enderecos.Any());

            RuleForEach(x => x.Contatos)
                .SetValidator(new PessoaContatoInputValidator())
                .When(x => x.Contatos != null && x.Contatos.Any());
        }
    }
}
