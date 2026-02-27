using Estac.Domain.Models.Enuns;

namespace Estac.Domain.Output.Endereco
{
    public class PessoaEnderecoPostOutput
    {
        public int PessoaId { get; set; }
        public bool Principal { get; set; }
        public TipoEndereco TipoEndereco { get; set; }
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
    }
}