using Estac.Domain.Input.Base;
using Estac.Domain.Input.Motorista;
using Estac.Domain.Input.Transportadora;
using Estac.Domain.Input.Veiculo;

namespace Estac.Domain.Input.EntradaSaida
{
    public class EntradaSaidaPostInput : BaseIntInput
    {
        public int MotoristaId { get; set; }
        public int TransportadoraId { get; set; }
        public int VeiculoId { get; set; }
        public DateTime DataHoraEntrada { get; set; }
        public DateTime? DataHoraSaida { get; set; }
        public string Observao { get; set; }
        public MotoristaPostInput Motorista { get; set; }
        public TransportadoraPostInput Transportadora { get; set; }
        public VeiculoPostInput Veiculo { get; set; }
    }
}
