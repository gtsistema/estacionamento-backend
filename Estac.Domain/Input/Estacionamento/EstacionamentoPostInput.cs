using Estac.Domain.Input.Base;
using Estac.Domain.Input.Pessoa;
using Estac.Domain.Models.Enuns;

namespace Estac.Domain.Input.Estacionamento
{
    public class EstacionamentoPostInput : BaseIntInput
    {
        public int PessoaId { get; set; }
        public int? CapacidadeVeiculo { get; set; }
        public string TamanhoTerreno { get; set; }
        public string ResposanvelLegal { get; set; }
        public string ResponsavelCpf { get; set; }
        public bool? PossuiSeguranca { get; set; }
        public bool? PossuiBanheiro { get; set; }
        public TipoCobranca TipoCobranca { get; set; }
        public byte? CobrancaPorcentagem { get; set; }
        public decimal? CobrancaValor { get; set; }
        public PessoaInput Pessoa { get; set; }
    }
}