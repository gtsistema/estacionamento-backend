using Estac.Domain.Models.Enuns;

namespace Estac.Domain.Output.Veiculo
{
    public class VeiculoSearchOutput
    {
        public int Id { get; set; }
        public string Placa { get; set; }
        public int? Ano { get; set; }
        public string ModeloMarca { get; set; }
        public bool Ativo { get; set; }
        public string Cor { get; set; }
        public TipoCarga? TipoCarga { get; set; }
        public IReadOnlyList<int> MotoristaIds { get; set; }
        public IReadOnlyList<string> Motoristas { get; set; }
    }
}
