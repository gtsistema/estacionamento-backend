using Estac.Domain.Models.Enuns;
using Newtonsoft.Json;

namespace Estac.Domain.Output.EntradaSaida
{
    public class EntradaSaidaSearchOutput
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public int MotoristaId { get; set; }
        public string NomeMotorista { get; set; }
        public int TransportadoraId { get; set; }
        public string NomeTransportadora { get; set; }
        public int VeiculoId { get; set; }
        public string PlacaVeiculo { get; set; }
        public DateTime DataHoraEntrada { get; set; }
        public DateTime? DataHoraSaida { get; set; }
        public EntradaSaidaStatus Status { get; set; }
    }
}
