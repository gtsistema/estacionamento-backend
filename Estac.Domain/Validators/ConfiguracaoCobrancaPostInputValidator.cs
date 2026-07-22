using Estac.Domain.Input.ConfiguracaoCobranca;
using FluentValidation;

namespace Estac.Domain.Validators
{
    public class ConfiguracaoCobrancaPostInputValidator : AbstractValidator<ConfiguracaoCobrancaPostInput>
    {
        public ConfiguracaoCobrancaPostInputValidator()
        {
            RuleFor(x => x.TransportadoraId)
                .GreaterThan(0).WithMessage("Transportadora é obrigatória.");

            RuleFor(x => x.EstacionamentoId)
                .GreaterThan(0).WithMessage("Estacionamento é obrigatório.");

            RuleFor(x => x.Status)
                .IsInEnum().WithMessage("Status da configuração inválido.");

            RuleFor(x => x.ModalidadeCobranca)
                .IsInEnum().WithMessage("Modalidade de cobrança inválida.");

            RuleFor(x => x.RegraFechamento)
                .IsInEnum().WithMessage("Regra de fechamento inválida.");

            RuleFor(x => x.PrazoVencimentoDias)
                .GreaterThan(0).WithMessage("Prazo de vencimento deve ser maior que zero.");

            RuleFor(x => x.DiaFechamento)
                .InclusiveBetween((byte)1, (byte)31)
                .When(x => x.DiaFechamento.HasValue)
                .WithMessage("Dia de fechamento deve estar entre 1 e 31.");

            RuleFor(x => x.EmailFinanceiro)
                .NotEmpty().WithMessage("E-mail financeiro é obrigatório.")
                .EmailAddress().WithMessage("E-mail financeiro inválido.")
                .MaximumLength(200);

            RuleFor(x => x.MultaPercentual)
                .GreaterThan(0)
                .When(x => x.AplicarMulta)
                .WithMessage("Informe o percentual de multa quando a aplicação de multa estiver ativa.");

            RuleFor(x => x.MultaPercentual)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Percentual de multa não pode ser negativo.");

            RuleFor(x => x.JurosPercentual)
                .GreaterThan(0)
                .When(x => x.AplicarJuros)
                .WithMessage("Informe o percentual de juros quando a aplicação de juros estiver ativa.");

            RuleFor(x => x.JurosPercentual)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Percentual de juros não pode ser negativo.");

            RuleFor(x => x.ValorDescontoFixo)
                .GreaterThan(0)
                .When(x => x.AplicarDescontoFixo)
                .WithMessage("Informe o valor do desconto fixo quando a aplicação estiver ativa.");

            RuleFor(x => x.ValorDescontoFixo)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Valor do desconto fixo não pode ser negativo.");

            RuleFor(x => x.ValorAcrescimoFixo)
                .GreaterThan(0)
                .When(x => x.AplicarAcrescimoFixo)
                .WithMessage("Informe o valor do acréscimo fixo quando a aplicação estiver ativa.");

            RuleFor(x => x.ValorAcrescimoFixo)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Valor do acréscimo fixo não pode ser negativo.");

            RuleFor(x => x.Regra)
                .NotNull().WithMessage("Regra de cobrança é obrigatória.");

            RuleFor(x => x.Regra)
                .Must(PossuiAoMenosUmaRegraDeCobranca)
                .When(x => x.Regra != null)
                .WithMessage("Selecione ao menos uma regra de cobrança.");
        }

        private static bool PossuiAoMenosUmaRegraDeCobranca(ConfiguracaoCobrancaRegraInput regra)
        {
            return regra.CobrarDiaria
                || regra.CobrarSemanal
                || regra.CobrarQuinzenal
                || regra.CobrarMensal
                || regra.CobrarDataPersonalizada
                || regra.CobrarLavagem
                || regra.CobrarPernoite
                || regra.CobrarServicosExtras
                || regra.ConsiderarBeneficioAbastecimento;
        }
    }
}
