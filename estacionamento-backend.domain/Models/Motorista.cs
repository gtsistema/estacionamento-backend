using Estac.Domain.Models.Base;

namespace Estac.Domain.Models
{
    public class Motorista : BaseIntDataNull
    {
        public int PessoaId { get; set; }
        public string CNH { get; set; }
        public DateTime? ValidadeCNH { get; set; }
        public Pessoa Pessoa { get; set; }
        public int? TransportadoraId { get; set; }
        public Transportadora Transportadora { get; set; }
        public ICollection<VeiculoMotorista> VeiculoMotoristas { get; set; } = new List<VeiculoMotorista>();
    }
}