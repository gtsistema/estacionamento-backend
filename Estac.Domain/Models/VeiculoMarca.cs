using Estac.Domain.Models.Base;

namespace Estac.Domain.Models
{
    public class VeiculoMarca : BaseInt
    {
        public int VeiculoModeloId { get; set; }
        public VeiculoModelo VeiculoModelo { get; set; }
    }
}