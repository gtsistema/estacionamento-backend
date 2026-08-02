using Estac.Domain.Models.Base;
using Estac.Domain.Models.Enuns;

namespace Estac.Domain.Models
{
    public class Veiculo : BaseInt
    {
        public string Placa { get; set; }
        public int? Ano { get; set; }
        public bool Ativo { get; set; }
        public string Cor { get; set; }
        public TipoCarga? TipoCarga { get; set; }
        public int? VeiculoModeloId { get; set; }
        public int? VeiculoDetalheId { get; set; }
        public int? TransportadoraId { get; set; }
        public VeiculoDetalhe VeiculoDetalhe { get; set; }
        public VeiculoModelo VeiculoModelo { get; set; }
        public Transportadora Transportadora { get; set; }
        public ICollection<VeiculoMotorista> VeiculoMotoristas { get; set; } = new List<VeiculoMotorista>();
    }
}