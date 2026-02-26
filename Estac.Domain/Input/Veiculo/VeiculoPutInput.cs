using FluentValidation;
using FluentValidation.Results;
using Estac.Domain.Models.Enuns;
using Estac.Domain.Shared;

namespace Estac.Domain.Input.Veiculo
{
    public class VeiculoPutInput : VeiculoPostInput
    {

        //public static ValidationResult Validar(VeiculoPutInput input)
        //{
        //    return new DespesaPutInputValidate().Validate(input);
        //}
    }

    public class DespesaPutInputValidate : AbstractValidator<VeiculoPutInput>
    {
        //public DespesaPutInputValidate()
        //{
        //    RuleFor(x => x.Id)
        //    .NotEmpty().WithMessage(string.Format(ResourceDomain.MSG_Campo_Obrigatorio, ResourceDomain.Id));

        //    RuleFor(x => x.Descricao)
        //    .NotEmpty().WithMessage(string.Format(ResourceDomain.MSG_Campo_Obrigatorio, ResourceDomain.Descricao))
        //    .MinimumLength(Constantes.Tres).WithMessage(string.Format(ResourceDomain.MSG_Min_Lengh_Campo, ResourceDomain.Descricao, Constantes.Tres))
        //    .MaximumLength(Constantes.Mil).WithMessage(string.Format(ResourceDomain.MSG_Max_Lengh_Campo, ResourceDomain.Descricao, Constantes.Mil));

        //    RuleFor(x => x.TipoDespesa).NotEmpty()
        //    .WithMessage(string.Format(ResourceDomain.MSG_Campo_Obrigatorio, ResourceDomain.TipoDespesa));

        //    RuleFor(x => x.ValorTotal).NotEmpty()
        //    .WithMessage(string.Format(ResourceDomain.MSG_Campo_Obrigatorio, ResourceDomain.ValorTotal));

        //    RuleFor(x => x.OrcamentoId).NotEmpty()
        //    .WithMessage(string.Format(ResourceDomain.MSG_Campo_Obrigatorio, ResourceDomain.OrcamentoId));
        //}
    }
}
