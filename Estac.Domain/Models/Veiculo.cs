using Estac.Domain.Models.Base;

namespace Estac.Domain.Models
{
    public class Veiculo : BaseInt
    {
        public string Placa { get; set; }
        public int? Ano { get; set; }
        public bool Ativo { get; set; }
        public string Cor { get; set; }
        public int? VeiculoMarcaId { get; set; }
        public int? VeiculoModeloId { get; set; }
        public int? VeiculoDetalheId { get; set; }
        public VeiculoDetalhe VeiculoDetalhe { get; set; }
        public VeiculoMarca VeiculoMarca { get; set; }
        public VeiculoModelo VeiculoModelo { get; set; }
    }
}