using Estac.Domain.Models.Enuns;

namespace Estac.Domain.Output.Pessoa
{
    public class PessoaContatoOutput
    {
        public int PessoaId { get; set; }
        public bool Principal { get; set; }
        public TipoContato TipoContato { get; set; }
        public string Numero { get; set; }
        public string Observacao { get; set; }
    }
}
