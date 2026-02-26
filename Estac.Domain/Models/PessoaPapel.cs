
using Estac.Domain.Models.Base;
using Estac.Domain.Models.Enuns;

namespace Estac.Domain.Models
{
    public class PessoaPapel : BaseIntDataNull
    {
        public int PessoaId { get; set; }

        public TipoPapel TipoPapel { get; set; }

        public Pessoa Pessoa { get; set; }
    }
}
