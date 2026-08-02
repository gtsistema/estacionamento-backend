namespace Estac.Domain.Output.Pessoa
{
    public class PessoaEstacionamentoOutput
    {
        public string NomeRazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string Cnpj { get; set; }
        public bool Ativo { get; set; }
        public List<PessoaEnderecoOutput> Enderecos { get; set; } = new List<PessoaEnderecoOutput>();
        public List<PessoaContatoOutput> Contatos { get; set; } = new List<PessoaContatoOutput>();
    }
}
