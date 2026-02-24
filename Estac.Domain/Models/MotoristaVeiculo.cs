using Estac.Domain.Models.Base;

namespace Estac.Domain.Models
{
    public class MotoristaVeiculo : BaseIntDataNull
    {
        public int MotoristaId { get; set; }
        public int VeiculoId { get; set; }
        public Motorista Motorista { get; set; }
        public Veiculo Veiculo { get; set; }
    }
}