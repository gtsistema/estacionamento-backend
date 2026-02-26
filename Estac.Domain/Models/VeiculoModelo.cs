
using Estac.Domain.Models.Base;

namespace Estac.Domain.Models
{
    public class VeiculoModelo : BaseInt
    {
        public int VeiculoMarcaId { get; set; }
        public VeiculoMarca VeiculoMarca { get; set; }
    }
}