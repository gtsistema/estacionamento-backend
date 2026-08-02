using Estac.Domain.Input.Base;
using Estac.Domain.Input.Motorista;
using Estac.Domain.Models.Enuns;

namespace Estac.Domain.Input.Veiculo
{
    public class VeiculoPostInput : BaseIntInput
    {
        public int Id { get; set; }
        public string Placa { get; set; }
        public int? Ano { get; set; }
        public bool Ativo { get; set; }
        public string Cor { get; set; }
        public TipoCarga? TipoCarga { get; set; }
        public int? TransportadoraId { get; set; }
        public VeiculoDetalheInput VeiculoDetalhe { get; set; }
        public VeiculoModeloInput Modelo { get; set; }
        public VeiculoMarcaInput Marca { get; set; }
        public List<MotoristaVinculoInput> Motoristas { get; set; } = new();
    }

    public class VeiculoDetalheInput()
    {
        public string Uf { get; set; }
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

    public class VeiculoModeloInput : BaseIntDataNullInput
    {
        /// <summary>Marca esperada do modelo (opcional; usada para conferência com o cadastro de modelo).</summary>
        public VeiculoMarcaInput Marca { get; set; }
    }


    public class VeiculoMarcaInput : BaseIntDataNullInput
    {
    }
}
