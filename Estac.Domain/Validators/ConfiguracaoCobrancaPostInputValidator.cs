using System.Linq.Expressions;
using Estac.Domain.Input.ConfiguracaoCobranca;
using Estac.Domain.Models.Enuns;
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

            RuleFor(x => x.DiaFechamento)
                .NotNull()
                .When(x => x.RegraFechamento == RegraFechamento.DiaFixo)
                .WithMessage("Dia de fechamento é obrigatório quando a regra de fechamento for dia fixo.");

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

            RuleFor(x => x.ValorEstadia)
                .GreaterThanOrEqualTo(0)
                .When(x => x.ValorEstadia.HasValue)
                .WithMessage("Valor da estadia não pode ser negativo.");

            RuleFor(x => x.DataCobranca)
                .NotNull()
                .When(x => x.ModalidadeCobranca == ModalidadeCobranca.Personalizado)
                .WithMessage("Data da cobrança é obrigatória para cobrança em data personalizada.");

            RegraServicoAdicional(x => x.ValorLavagem, x => x.CobrarLavagem, "valor da lavagem");
            RegraServicoAdicional(x => x.ValorPernoite, x => x.CobrarPernoite, "valor da pernoite");
            RegraServicoAdicional(x => x.ValorServicosExtras, x => x.CobrarServicosExtras, "valor dos serviços extras");
            RegraServicoAdicional(
                x => x.ValorBeneficioAbastecimento,
                x => x.ConsiderarBeneficioAbastecimento,
                "valor do benefício por abastecimento");
        }

        /// <summary>
        /// Serviço adicional habilitado exige valor maior que zero; quando desabilitado o valor é
        /// descartado pelo serviço, então aqui só é validado o sinal.
        /// </summary>
        private void RegraServicoAdicional(
            Expression<Func<ConfiguracaoCobrancaPostInput, decimal?>> valor,
            Func<ConfiguracaoCobrancaPostInput, bool> habilitado,
            string descricao)
        {
            var lerValor = valor.Compile();

            RuleFor(valor)
                .NotNull().WithMessage($"Informe o {descricao} quando o serviço estiver habilitado.")
                .GreaterThan(0m).WithMessage($"Informe o {descricao} maior que zero.")
                .When(habilitado);

            RuleFor(valor)
                .GreaterThanOrEqualTo(0m)
                .When(input => lerValor(input).HasValue)
                .WithMessage($"O {descricao} não pode ser negativo.");
        }
    }
}
