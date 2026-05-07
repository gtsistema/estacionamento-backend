using Estac.Domain.Output.Base;
using Estac.Domain.Output.Motorista;
using Estac.Domain.Output.Pessoa;
using Estac.Domain.Models.Enuns;

namespace Estac.Domain.Output.Veiculo
{
    public class VeiculoOutput : BaseOutput
    {
        public string Placa { get; set; }
        public int? Ano { get; set; }
        public bool Ativo { get; set; }
        public string Cor { get; set; }
        public TipoCarga? TipoCarga { get; set; }
        public VeiculoDetalheOutput Detalhe { get; set; }
        public VeiculoModeloOutput Modelo { get; set; }
        public IReadOnlyList<MotoristaOutput> Motoristas { get; set; }
    }

    public class VeiculoDetalheOutput()
    {
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
    }

    public class VeiculoModeloOutput : BaseIntDataNullOutput
    {
        public int? VeiculoMarcaId { get; set; }
        public VeiculoMarcaOutput VeiculoMarca { get; set; }
    }

    public class VeiculoMarcaOutput : BaseIntDataNullOutput
    {
    }
}
