using Estac.Domain.Models.Base;

namespace Estac.Domain.Models
{
    public class VeiculoPlaca : BaseInt
    {
        public int VeiculoId { get; set; }
        public Veiculo Veiculo { get; set; }
    }
}