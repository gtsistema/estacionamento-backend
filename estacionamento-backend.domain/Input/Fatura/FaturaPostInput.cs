using Estac.Domain.Validators;
using FluentValidation.Results;

namespace Estac.Domain.Input.Fatura
{
    /// <summary>
    /// Entrada do POST de geração de fatura.
    /// Se EstacionamentoId for nulo, usa o estacionamento do usuário logado (claim EmpresaId).
    /// </summary>
    public class FaturaPostInput
    {
        public int TransportadoraId { get; set; }
        public int? EstacionamentoId { get; set; }

        public static ValidationResult Validar(FaturaPostInput input) =>
            new FaturaPostInputValidator().Validate(input);
    }
}
