using FluentValidation;
using FluentValidation.Results;

namespace Gp.Domain.Input
{
    public class DespesaPostInput
    {
        public int Id { get; set; }

        public static ValidationResult Validar(DespesaPostInput input)
        {
            return new DespesaPostInputValidate().Validate(input);
        }
    }

    public class DespesaPostInputValidate : AbstractValidator<DespesaPostInput>
    {
        public DespesaPostInputValidate()
        {
            //RuleFor(x => x.Id).NotEmpty()
            //    .WithMessage("Captacao obrigatória.");
            //RuleFor(x => x.CorretorCaptacaoId).NotEmpty()
            //    .WithMessage("CorretorCaptacaoId obrigatória.");
            //RuleFor(x => x.FinalidadeId).NotEmpty()
            //    .WithMessage("FinalidadeId obrigatória.");
            //RuleFor(x => x.LogradouroId).NotEmpty()
            //    .WithMessage("LogradouroId obrigatória.");
            //RuleFor(x => x.OperacaoImobiliaria).NotEmpty()
            //    .WithMessage("OperacaoImobiliaria obrigatória.");
            //RuleFor(x => x.PadraoSocial).NotEmpty()
            //    .WithMessage("PadraoSocial obrigatória.");
            //RuleFor(x => x.ProcedimentoVisita).NotEmpty()
            //    .WithMessage("ProcedimentoVisita obrigatória.");

            //RuleFor(x => x.Situacao).NotEmpty()
            //    .WithMessage("Situacao obrigatória.");
            //RuleFor(x => x.SituacaoDocumental).NotEmpty()
            //    .WithMessage("SituacaoDocumental obrigatória.");
            //RuleFor(x => x.SituacaoFinanceira).NotEmpty()
            //    .WithMessage("SituacaoFinanceira obrigatória.");
            //RuleFor(x => x.TipoImovelId).NotEmpty()
            //    .WithMessage("TipoImovelId obrigatória.");
            //RuleFor(x => x.TituloAmigavel).NotEmpty()
            //    .WithMessage("TituloAmigavel obrigatória.");
        }
    }
}
