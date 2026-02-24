using Estac.Domain.Models.Base;

namespace Estac.Domain.Models
{
    public class Motorista : BaseInt
    {
        public int PessoaId { get; set; }
        public string CNH { get; set; }
        public DateTime? ValidadeCNH { get; set; }
        public Pessoa Pessoa { get; set; }
    }
}