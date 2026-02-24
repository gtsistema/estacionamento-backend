
using Estac.Domain.Models.Enuns;

namespace Estac.Domain.Models
{
    public class PessoaPapel
    {
        public int Id { get; set; }

        public int PessoaId { get; set; }

        public TipoPapel TipoPapel { get; set; }

        public Pessoa Pessoa { get; set; }
    }
}
