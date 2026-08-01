using Estac.Domain.Models.Base;
using Estac.Domain.Models.Enuns;

namespace Estac.Domain.Models
{
    public class Estacionamento : BaseIntDataNull
    {
        public int PessoaId { get; set; }
        public int? CapacidadeVeiculo { get; set; }
        public string TamanhoTerreno { get; set; }
        public string ResponsavelLegal { get; set; }
        public string ResponsavelCpf { get; set; }
        public string ResponsavelEmail { get; set; }
        public string ResponsavelTelefone { get; set; }
        public bool? PossuiSeguranca { get; set; }
        public bool? PossuiBanheiro { get; set; }
        public TipoCobranca TipoCobranca { get; set; }
        public byte? CobrancaPorcentagem { get; set; }
        public decimal? CobrancaValor { get; set; }
        public byte[] Contrato { get; set; }
        public Pessoa Pessoa { get; set; }
        public ICollection<ContaBancaria> ContasBancarias { get; set; }
        public ICollection<EstacionamentoFoto> Fotos { get; set; }
        public EstacionamentoConfiguracao Configuracao { get; set; }
    }
}