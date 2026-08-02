using Estac.Domain.Input.ContaBancaria;
using Estac.Domain.Input.Pessoa;
using Estac.Domain.Models.Enuns;
using Estac.Domain.Output.Base;
using Estac.Domain.Output.Pessoa;

namespace Estac.Domain.Output.Estacionamento
{
    public class EstacionamentoOutput : BaseOutput
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
        public PessoaEstacionamentoOutput PessoaJuridica { get; set; }
        public List<ContaBancariaOutput> ContaBancaria { get; set; } = new();

    }
}