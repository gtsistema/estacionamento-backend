using Estac.Domain.Input.Fatura;
using Estac.Domain.Models.Enuns;
using FluentValidation;

namespace Estac.Domain.Validators
{
    public class FaturaPutInputValidator : AbstractValidator<FaturaPutInput>
    {
        public FaturaPutInputValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Identificador da fatura é obrigatório.");

            RuleFor(x => x.TransportadoraId)
                .GreaterThan(0).WithMessage("Transportadora é obrigatória.");

            RuleFor(x => x.EstacionamentoId)
                .GreaterThan(0).WithMessage("Estacionamento é obrigatório.");

            RuleFor(x => x.Status)
                .IsInEnum().WithMessage("Status da fatura inválido.");

            RuleFor(x => x.ModalidadeRecebimento)
                .IsInEnum()
                .When(x => x.ModalidadeRecebimento.HasValue)
                .WithMessage("Modalidade de recebimento inválida.");

            RuleFor(x => x.ValorTotal)
                .GreaterThan(0).WithMessage("Valor total deve ser maior que zero.");

            RuleFor(x => x.ValorRecebido)
                .GreaterThanOrEqualTo(0).WithMessage("Valor recebido não pode ser negativo.")
                .LessThanOrEqualTo(x => x.ValorTotal)
                .WithMessage("Valor recebido não pode ser maior que o valor total.");

            RuleFor(x => x.ValorDesconto)
                .GreaterThanOrEqualTo(0).WithMessage("Valor de desconto não pode ser negativo.");

            RuleFor(x => x.ValorAcrescimo)
                .GreaterThanOrEqualTo(0).WithMessage("Valor de acréscimo não pode ser negativo.");

            RuleFor(x => x.ValorJuros)
                .GreaterThanOrEqualTo(0).WithMessage("Valor de juros não pode ser negativo.");

            RuleFor(x => x.ValorMulta)
                .GreaterThanOrEqualTo(0).WithMessage("Valor de multa não pode ser negativo.");

            RuleFor(x => x.DataEmissao)
                .NotEmpty().WithMessage("Data de emissão é obrigatória.");

            RuleFor(x => x.DataVencimento)
                .NotEmpty().WithMessage("Data de vencimento é obrigatória.")
                .GreaterThanOrEqualTo(x => x.DataEmissao.Date)
                .WithMessage("Data de vencimento deve ser igual ou posterior à data de emissão.");

            RuleFor(x => x.PeriodoInicio)
                .NotEmpty().WithMessage("Período inicial é obrigatório.");

            RuleFor(x => x.PeriodoFim)
                .NotEmpty().WithMessage("Período final é obrigatório.")
                .GreaterThanOrEqualTo(x => x.PeriodoInicio.Date)
                .WithMessage("Período final deve ser igual ou posterior ao período inicial.");

            RuleFor(x => x.EmailEnvio)
                .EmailAddress().WithMessage("E-mail de envio inválido.")
                .MaximumLength(200)
                .When(x => !string.IsNullOrWhiteSpace(x.EmailEnvio));

            RuleFor(x => x.Numero)
                .MaximumLength(50);

            RuleFor(x => x.Observacao)
                .MaximumLength(500);

            RuleFor(x => x.DataPagamento)
                .NotNull()
                .When(x => x.Status is StatusFatura.Pago or StatusFatura.Parcial)
                .WithMessage("Data de pagamento é obrigatória para faturas pagas ou parciais.");

            RuleFor(x => x.ModalidadeRecebimento)
                .NotNull()
                .When(x => x.Status is StatusFatura.Pago or StatusFatura.Parcial)
                .WithMessage("Modalidade de recebimento é obrigatória para faturas pagas ou parciais.");
        }
    }
}
