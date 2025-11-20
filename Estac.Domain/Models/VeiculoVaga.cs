namespace Estac.Domain.Models
{
    public class VeiculoVaga
    {
        public int Id { get; set; }
        public int VeiculoId { get; set; }
        public Veiculo Veiculo { get ; set; } 
        public Vaga Vaga { get; set; }
    }
}