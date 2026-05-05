using Estac.Domain.Input.Base;
using Estac.Domain.Input.Endereco;
using Estac.Domain.Input.PessoaContato;
using Estac.Domain.Models.Enuns;

namespace Estac.Domain.Input.Transportadora
{
    public class TransportadoraJuridica : BaseIntInput
    {
        public TipoPessoa TipoPessoa { get; set; } = TipoPessoa.Juridica;
        public string NomeRazaoSocial { get; set; }
        public string Descricao { get; set; }
        public string Documento { get; set; }
        public string Email { get; set; }
        public bool Ativo { get; set; }
        public string InscricaoEstadual { get; set; }
        public PessoaEnderecoInput Endereco { get; set; } = new PessoaEnderecoInput();
        public List<PessoaContatoInput> Contatos { get; set; } = new List<PessoaContatoInput>();
    }
}
