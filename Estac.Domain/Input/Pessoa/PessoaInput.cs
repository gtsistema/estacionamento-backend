using Estac.Domain.Input.Base;
using Estac.Domain.Input.Endereco;
using Estac.Domain.Input.PessoaContato;
using Estac.Domain.Models.Enuns;
using Estac.Domain.Validators;
using FluentValidation.Results;

namespace Estac.Domain.Input.Pessoa
{
    public class PessoaInput : BaseIntInput
    {
        public TipoPessoa TipoPessoa { get; set; }
        public string NomeRazaoSocial { get; set; }
        public string Descricao { get; set; }
        public string Documento { get; set; }
        public bool Ativo { get; set; }
        public IEnumerable<PessoaEnderecoInput> Enderecos { get; set; } = new List<PessoaEnderecoInput>();
        public IEnumerable<PessoaContatoInput> Contatos { get; set; } = new List<PessoaContatoInput>();

        public static ValidationResult ValidarComoPessoaJuridica(PessoaInput input) =>
            new PessoaJuridicaInputValidator().Validate(input);
    }
}
