namespace Estac.Domain.Models
{
    public class MotoristaVeiculo
    {
        public MotoristaVeiculo()
        {
            
        }

        public int Id { get; set; }
        public int MotoristaId { get; set; }
        public Motorista Motorista { get; set; }
        public int VeiculoId { get; set; }
        public Veiculo Veiculo { get; set; }
    }
}
