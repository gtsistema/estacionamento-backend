using Estac.Domain.Models.Base;

namespace Estac.Domain.Models
{
    public class VeiculoDetalhe : BaseInt
    {
        public int VeiculoId { get; set; }
        public string NomeProprietario { get; set; }
        public string CpfCnpjProprietario { get; set; }
        public decimal? KmAtual { get; set; }
        public decimal? KmRastreador { get; set; }
        public decimal? CapacidadeCombustivel { get; set; }
        public decimal? CapacidadeArla { get; set; }
        public decimal? MediaMinima { get; set; }
        public decimal? MediaMaxima { get; set; }
        public string InscricaoEstadual { get; set; }
        public bool VeiculoTerceiro { get; set; } = false;
        public string Observacoes { get; set; }
        public Veiculo Veiculo { get; set; }
    }
}
