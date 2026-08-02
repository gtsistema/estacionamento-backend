using Estac.Domain.Input.Base;
using Estac.Domain.Models.Enuns;
using Estac.Domain.Validators;
using FluentValidation.Results;

namespace Estac.Domain.Input.Fatura
{
    public class FaturaPostInput : BaseIntInput
    {
        public int Id { get; set; }
        public int TransportadoraId { get; set; }
        public int EstacionamentoId { get; set; }
        public int? ConfiguracaoCobrancaId { get; set; }
        public string Numero { get; set; }
        public StatusFatura Status { get; set; } = StatusFatura.AguardandoEnvio;
        public ModalidadeRecebimento? ModalidadeRecebimento { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal ValorRecebido { get; set; }
        public decimal ValorDesconto { get; set; }
        public decimal ValorAcrescimo { get; set; }
        public decimal ValorJuros { get; set; }
        public decimal ValorMulta { get; set; }
        public DateTime DataEmissao { get; set; }
        public DateTime DataVencimento { get; set; }
        public DateTime? DataPagamento { get; set; }
        public DateTime PeriodoInicio { get; set; }
        public DateTime PeriodoFim { get; set; }
        public string EmailEnvio { get; set; }
        public string Observacao { get; set; }
        public List<FaturaItemPostInput> Itens { get; set; } = new();

        public static ValidationResult Validar(FaturaPostInput input) =>
            new FaturaPostInputValidator().Validate(input);
    }
}
