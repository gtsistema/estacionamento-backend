
using Estac.Domain.Models.Enuns;

namespace Estac.Domain.Models
{
    public class PessoaTelefone
    {
        public int Id { get; set; }
        public int PessoaId { get; set; }
        public bool Principal { get; set; }
        public TipoTelefone TipoTelefone { get; set; }
        public string Numero { get; set; }
        public string Observacao { get; set; }
        public Pessoa Pessoa { get; set; }
    }
}
