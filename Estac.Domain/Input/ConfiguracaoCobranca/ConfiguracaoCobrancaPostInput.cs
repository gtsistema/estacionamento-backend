using Estac.Domain.Input.Base;
using Estac.Domain.Models.Enuns;
using Estac.Domain.Validators;
using FluentValidation.Results;

namespace Estac.Domain.Input.ConfiguracaoCobranca
{
    public class ConfiguracaoCobrancaPostInput : BaseIntInput
    {
        public int Id { get; set; }
        public int TransportadoraId { get; set; }
        public int EstacionamentoId { get; set; }
        public StatusConfiguracaoCobranca Status { get; set; } = StatusConfiguracaoCobranca.Ativa;
        public ModalidadeCobranca ModalidadeCobranca { get; set; }
        public byte? DiaFechamento { get; set; }
        public RegraFechamento RegraFechamento { get; set; }
        public int PrazoVencimentoDias { get; set; }
        public string EmailFinanceiro { get; set; }
        public bool EnvioAutomaticoEmail { get; set; }
        public bool GerarFaturaAutomaticamente { get; set; }
        public bool PermitirPagamentoParcial { get; set; }
        public bool AplicarMulta { get; set; }
        public decimal MultaPercentual { get; set; }
        public bool AplicarJuros { get; set; }
        public decimal JurosPercentual { get; set; }
        public bool AplicarDescontoFixo { get; set; }
        public decimal ValorDescontoFixo { get; set; }
        public bool AplicarAcrescimoFixo { get; set; }
        public decimal ValorAcrescimoFixo { get; set; }
        public decimal? ValorEstadia { get; set; }
        public bool AgruparPorPlaca { get; set; }
        public bool AgruparPorPeriodo { get; set; }
        public bool AgruparPorTransportadora { get; set; }
        public List<ConfiguracaoAgendamentoInput> ConfiguracoesAgendamento { get; set; } = new();

        public static ValidationResult Validar(ConfiguracaoCobrancaPostInput input) =>
            new ConfiguracaoCobrancaPostInputValidator().Validate(input);
    }
}
