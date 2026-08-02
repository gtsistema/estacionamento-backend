
namespace Estac.Domain.Input.Motorista
{
    public class MotoristaFilterInput : FilterInput
    {
        public int? TransportadoraId { get; set; }
        public string Cpf { get; set; }
    }
}
