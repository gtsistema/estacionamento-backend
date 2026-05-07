using Estac.Domain.Models.Enuns;

namespace Estac.Domain.Input.Movimento.Entrada
{
    public class EntradaPostInput
    {
        public EntradaSaidaStatus Status { get; set; }
        public DateTime? DataHoraEntrada { get; set; }
        public string Observacao { get; set; }
        public EntradaMotoristaInput Motorista { get; set; }
        public EntradaTransportadoraInput Transportadora { get; set; }
        public EntradaVeiculoInput Veiculo { get; set; }
    }
}