using Estac.Domain.Models.Base;

namespace Estac.Domain.Models
{
    public class VagaVeiculo : BaseIntDataNull
    {
        public int VagaId { get; set; }
        public int VeiculoId { get; set; }
        public Veiculo Veiculo { get ; set; } 
        public Vaga Vaga { get; set; }
        public DateTime DataEntrada { get; set; }
        public DateTime? DataSaida { get; set; }
    }
}