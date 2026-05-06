using Estac.Domain.Input.Base;
using Estac.Domain.Input.ContaBancaria;
using Estac.Domain.Input.Pessoa;
using Estac.Domain.Models.Enuns;

namespace Estac.Domain.Input.Estacionamento
{
    public class EstacionamentoPostInput : BaseIntInput
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
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
        public bool Ativo { get; set; }
        public PessoaEstacionamentoInput PessoaJuridica { get; set; }
        public ContaBancariaInput ContaBancaria { get; set; }
    }
}