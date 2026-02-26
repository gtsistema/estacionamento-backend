using Estac.Domain.Input.Base;
using Estac.Domain.Models;
using Estac.Domain.Shared;
using FluentValidation;
using FluentValidation.Results;

namespace Estac.Domain.Input.Veiculo
{
    public class VeiculoPostInput : BaseIntInput
    {
        public string Placa { get; set; }
        public int? Ano { get; set; }
        public bool Ativo { get; set; }
        public string Cor { get; set; }
        public int? VeiculoMarcaId { get; set; }
        public int? VeiculoDetalheId { get; set; }
        public VeiculoDetalheInput VeiculoDetalhe { get; set; }
        public VeiculoModeloInput VeiculoModelo { get; set; }

        //public static ValidationResult Validar(VeiculoPostInput input)
        //{
        //    return new DespesaPostInputValidate().Validate(input);
        //}
    }

    public class VeiculoDetalheInput()
    {
        public string Uf { get; set; }
        public string NomeProprietario { get; set; }
        public string CpfCnpjProprietario { get; set; }
        public decimal? KmAtual { get; set; }
        public decimal? KmRastreador { get; set; }
        public decimal? CapacidadeCombustivel { get; set; }
        public decimal? CapacidadeArla { get; set; }
        public decimal? MediaMinima { get; set; }
        public decimal? MediaMaxima { get; set; }
        public string InscricaoEstadual { get; set; }
        public bool VeiculoTerceiro { get; set; } = false;
        public string Observacoes { get; set; }
    }

    public class VeiculoModeloInput : BaseIntDataNullInput
    {
        public int? VeiculoMarcaId { get; set; }
        public VeiculoMarcaInput VeiculoMarca { get; set; }
    }


    public class VeiculoMarcaInput : BaseIntDataNullInput
    {
    }

    //public class DespesaPostInputValidate : AbstractValidator<VeiculoPostInput>
    //{
    //    public DespesaPostInputValidate()
    //    {
    //        RuleFor(x => x.Descricao)
    //        .NotEmpty().WithMessage(string.Format(ResourceDomain.MSG_Campo_Obrigatorio, ResourceDomain.Descricao))
    //        .MinimumLength(Constantes.Tres).WithMessage(string.Format(ResourceDomain.MSG_Min_Lengh_Campo, ResourceDomain.Descricao, Constantes.Tres))
    //        .MaximumLength(Constantes.Mil).WithMessage(string.Format(ResourceDomain.MSG_Max_Lengh_Campo, ResourceDomain.Descricao, Constantes.Mil));

    //        RuleFor(x => x.TipoDespesa).NotEmpty()
    //        .WithMessage(string.Format(ResourceDomain.MSG_Campo_Obrigatorio, ResourceDomain.TipoDespesa));

    //        RuleFor(x => x.ValorTotal).NotEmpty()
    //        .WithMessage(string.Format(ResourceDomain.MSG_Campo_Obrigatorio, ResourceDomain.ValorTotal));

    //        RuleFor(x => x.OrcamentoId).NotEmpty()
    //        .WithMessage(string.Format(ResourceDomain.MSG_Campo_Obrigatorio, ResourceDomain.OrcamentoId));
    //    }
    //}
}
