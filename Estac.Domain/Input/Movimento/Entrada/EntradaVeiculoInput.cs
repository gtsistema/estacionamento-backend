using Estac.Domain.Models.Enuns;

namespace Estac.Domain.Input.Movimento.Entrada
{
    public class EntradaVeiculoInput
    {
        public int? Id { get; set; }
        public string Placa { get; set; }
        public TipoCarga? TipoCarga { get; set; }
    }
}