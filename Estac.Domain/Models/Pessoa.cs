using Estac.Domain.Models.Base;
using Estac.Domain.Models.Enuns;

namespace Estac.Domain.Models
{
    public class Pessoa : BaseInt
    {
        public TipoPessoa TipoPessoa { get; set; }
        public string NomeRazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string Documento { get; set; } // CPF ou CNPJ
        public string Email { get; set; }
        public string InscricaoEstadual { get; set; }
        public bool Ativo { get; set; }
        public ICollection<PessoaPapel> Papeis { get; set; }
        public ICollection<PessoaTelefone> Telefones { get; set; }
        public ICollection<PessoaEndereco> Enderecos { get; set; }

        public void AdicionarPapel(TipoPapel tipo)
        {
            Papeis ??= new List<PessoaPapel>();

            if (!Papeis.Any(x => x.TipoPapel == tipo))
            {
                Papeis.Add(new PessoaPapel
                {
                    TipoPapel = tipo,
                });
            }
        }
    }
}