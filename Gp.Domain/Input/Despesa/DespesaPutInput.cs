using FluentValidation;
using FluentValidation.Results;
using Gp.Domain.Models.Enuns;
using Gp.Domain.Shared;

namespace Gp.Domain.Input.Despesa
{
    public class DespesaPutInput
    {
        public long Id { get; set; }
        public string Descricao { get; set; }
        public TipoDespesa TipoDespesa { get; set; }
        public decimal ValorTotal { get; set; }
        public int OrcamentoId { get; set; }

        public static ValidationResult Validar(DespesaPutInput input)
        {
            return new DespesaPutInputValidate().Validate(input);
        }
    }

    public class DespesaPutInputValidate : AbstractValidator<DespesaPutInput>
    {
        public DespesaPutInputValidate()
        {
            RuleFor(x => x.Id)
            .NotEmpty().WithMessage(string.Format(ResourceDomain.MSG_Campo_Obrigatorio, ResourceDomain.Id));

            RuleFor(x => x.Descricao)
            .NotEmpty().WithMessage(string.Format(ResourceDomain.MSG_Campo_Obrigatorio, ResourceDomain.Descricao))
            .MinimumLength(Constantes.Tres).WithMessage(string.Format(ResourceDomain.MSG_Min_Lengh_Campo, ResourceDomain.Descricao, Constantes.Tres))
            .MaximumLength(Constantes.Mil).WithMessage(string.Format(ResourceDomain.MSG_Max_Lengh_Campo, ResourceDomain.Descricao, Constantes.Mil));

            RuleFor(x => x.TipoDespesa).NotEmpty()
            .WithMessage(string.Format(ResourceDomain.MSG_Campo_Obrigatorio, ResourceDomain.TipoDespesa));

            RuleFor(x => x.ValorTotal).NotEmpty()
            .WithMessage(string.Format(ResourceDomain.MSG_Campo_Obrigatorio, ResourceDomain.ValorTotal));

            RuleFor(x => x.OrcamentoId).NotEmpty()
            .WithMessage(string.Format(ResourceDomain.MSG_Campo_Obrigatorio, ResourceDomain.OrcamentoId));
        }
    }
}
