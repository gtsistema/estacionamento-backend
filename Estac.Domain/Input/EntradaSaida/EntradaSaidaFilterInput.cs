namespace Estac.Domain.Input.EntradaSaida
{
    public class EntradaSaidaFilterInput : FilterInput
    {
        public string Placa { get; set; }
        public int? MotoristaId { get; set; }
        public int? TransportadoraId { get; set; }
        public bool SomenteEmAberto { get; set; }
    }
}
