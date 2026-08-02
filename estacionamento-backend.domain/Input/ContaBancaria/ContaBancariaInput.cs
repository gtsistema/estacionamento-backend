using Estac.Domain.Input.Base;
using Estac.Domain.Models.Enuns;
using Estac.Domain.Validators;
using FluentValidation.Results;

namespace Estac.Domain.Input.ContaBancaria
{
    public class ContaBancariaInput : BaseIntInput
    {
        public int Id { get; set; }
        public int EstacionamentoId { get; set; }
        public int? TransportadoraId { get; set; }
        public string Titular { get; set; }
        public string CpfCnpj { get; set; }
        public string Banco { get; set; }
        public string Agencia { get; set; }
        public string AgenciaDigito { get; set; }
        public string Conta { get; set; }
        public string ContaDigito { get; set; }
        public string TipoConta { get; set; }
        public bool Ativa { get; set; }
        public string ChavePix { get; set; }
        public TipoChave? TipoChave { get; set; }

        public static ValidationResult Validar(ContaBancariaInput input) =>
            new ContaBancariaInputValidator().Validate(input);
    }
}
