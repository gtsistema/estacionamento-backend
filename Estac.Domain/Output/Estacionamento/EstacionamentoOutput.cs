using Estac.Domain.Models.Enuns;
using Estac.Domain.Output.Base;
using Estac.Domain.Output.Pessoa;

namespace Estac.Domain.Output.Estacionamento
{
    public class EstacionamentoOutput : BaseOutput
    {
        public int? CapacidadeVeiculo { get; set; }
        public bool Tamanho { get; set; }
        public string ResposanvelLegal { get; set; }
        public string ResponsavelCpf { get; set; }
        public bool? PossuiSeguranca { get; set; }
        public bool? PossuiBanheiro { get; set; }
        public TipoCobranca TipoCobranca { get; set; }
        public byte? CobrancaPorcentagem { get; set; }
        public byte? CobrancaValor { get; set; }
        public int PessoaId { get; set; }
        public PessoaOutput PessoaJuridica { get; set; }
    }
}