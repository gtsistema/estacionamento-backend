using Estac.Domain.Output.Base;

namespace Estac.Domain.Output.VeiculoModelo
{
    public class VeiculoMarcaOutput : BaseIntDataNullOutput    
    {
        public int VeiculoMarcaId { get; set; }
        public VeiculoMarcaOutput VeiculoMarca { get; set; }
    }
}
