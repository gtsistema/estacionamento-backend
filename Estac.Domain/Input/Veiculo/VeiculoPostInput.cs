using Estac.Domain.Input.Base;

namespace Estac.Domain.Input.Veiculo
{
    /// <summary>
    /// Payload de criação: modelo e motorista por Id (dropdown); transportadora só Id; detalhes opcionais em <see cref="VeiculoDetalheInput"/>.
    /// </summary>
    public class VeiculoPostInput : BaseIntInput
    {
        public string Placa { get; set; }
        public int? Ano { get; set; }
        public bool Ativo { get; set; }
        public string Cor { get; set; }
        public int? TransportadoraId { get; set; }
        public int? MotoristaId { get; set; }
        public int? VeiculoModeloId { get; set; }
        public VeiculoDetalheInput VeiculoDetalhe { get; set; }
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
        public int? VeiculoMarcaId { get; set; }
        public VeiculoMarcaInput VeiculoMarca { get; set; }
    }


    public class VeiculoMarcaInput : BaseIntDataNullInput
    {
    }
}
