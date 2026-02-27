using Estac.Domain.Models.Enuns;

namespace Estac.Domain.Input.PessoaContato
{
    public class PessoaContatoInput
    {
        public int PessoaId { get; set; }
        public bool Principal { get; set; }
        public TipoContato TipoContato { get; set; }
        public string Numero { get; set; }
        public string Observacao { get; set; }
    }
}
