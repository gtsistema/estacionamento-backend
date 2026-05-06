using Estac.Domain.Models.Enuns;
using Estac.Domain.Output.Base;
using Estac.Domain.Output.Transportadora;

namespace Estac.Domain.Output.Motorista
{
    public class MotoristaVinculosPorPlacaOutput
    {
        public MotoristaOutput Motorista { get; set; }
        public VeiculoVinculoResumoOutput Veiculo { get; set; }
        public TransportadoraOutput Transportadora { get; set; }
        public IReadOnlyList<TransportadoraVeiculoVinculoOutput> VinculosTransportadoraVeiculo { get; set; }
    }

    public class VeiculoVinculoResumoOutput : BaseOutput
    {
        public string Placa { get; set; }
        public int? Ano { get; set; }
        public string Cor { get; set; }
        public TipoCarga? TipoCarga { get; set; }
        public int? TransportadoraId { get; set; }
        public int? MotoristaId { get; set; }
    }

    public class TransportadoraVeiculoVinculoOutput
    {
        public int TransportadoraId { get; set; }
        public int VeiculoId { get; set; }
        public string Placa { get; set; }
    }
}
