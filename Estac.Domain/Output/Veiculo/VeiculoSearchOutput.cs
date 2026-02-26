
namespace Estac.Domain.Output.Veiculo
{
    public class VeiculoSearchOutput
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public string Placa { get; set; }
        public int? Ano { get; set; }
        public bool Ativo { get; set; }
        public string Cor { get; set; }
    }
}
