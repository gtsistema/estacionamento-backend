using Estac.Domain.Models.Base;

namespace Estac.Domain.Models
{
    public class Estacionamento : BaseIntDataNull
    {
        public int PessoaId { get; set; }
        public Pessoa Pessoa { get; set; }
    }
}