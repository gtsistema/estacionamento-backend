using Estac.Domain.Input.Base;
using Estac.Domain.Input.Pessoa;
using Estac.Domain.Models;

namespace Estac.Domain.Input.VeiculoModelo
{
    public class VeiculoModeloPostInput : BaseIntInput
    {
        public int VeiculoMarcaId { get; set; }
        public VeiculoMarca VeiculoMarca { get; set; }
    }
}
