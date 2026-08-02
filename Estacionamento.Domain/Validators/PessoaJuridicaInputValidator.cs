using Estac.Domain.Input.Pessoa;
using Estac.Domain.Models.Enuns;
using FluentValidation;
using System.Linq;

namespace Estac.Domain.Validators
{
    /// <summary>
    /// Regras comuns para cadastro de pessoa jurídica (ex.: transportadora, empresas).
    /// </summary>
    public class PessoaJuridicaInputValidator : AbstractValidator<PessoaInput>
    {
        public PessoaJuridicaInputValidator()
        {
            RuleFor(x => x.NomeRazaoSocial)
                .NotEmpty().WithMessage("Razão social é obrigatória.")
                .MaximumLength(200);

            RuleFor(x => x.Descricao)
                .NotEmpty().WithMessage("Nome fantasia é obrigatório.")
                .MaximumLength(200);

            RuleFor(x => x.Cnpj)
                .NotEmpty().WithMessage("CNPJ é obrigatório.")
                .Must(DocumentoBrasil.CnpjValido).WithMessage("CNPJ inválido.");

            RuleForEach(x => x.Enderecos)
                .SetValidator(new PessoaEnderecoInputValidator())
                .When(x => x.Enderecos != null && x.Enderecos.Any());

            RuleForEach(x => x.Contatos)
                .SetValidator(new PessoaContatoInputValidator())
                .When(x => x.Contatos != null && x.Contatos.Any());
        }
    }
}
