using System.ComponentModel.DataAnnotations;
using Estac.Domain.Extensions;
using Estac.Domain.Input.ContaBancaria;
using Estac.Domain.Models.Enuns;
using FluentValidation;

namespace Estac.Domain.Validators
{
    public class ContaBancariaInputValidator : AbstractValidator<ContaBancariaInput>
    {
        private static readonly EmailAddressAttribute EmailAttribute = new();

        public ContaBancariaInputValidator()
        {
            RuleFor(x => x.TipoChave)
                .IsInEnum()
                .WithMessage("Tipo da chave PIX inválido.")
                .When(x => x.TipoChave.HasValue);

            RuleFor(x => x.TipoChave)
                .NotNull()
                .WithMessage("Tipo da chave PIX é obrigatório quando a chave PIX é informada.")
                .When(x => !string.IsNullOrWhiteSpace(x.ChavePix));

            RuleFor(x => x.ChavePix)
                .NotEmpty()
                .WithMessage("Chave PIX é obrigatória quando o tipo da chave é informado.")
                .When(x => x.TipoChave.HasValue);

            RuleFor(x => x.ChavePix)
                .MaximumLength(150)
                .When(x => !string.IsNullOrWhiteSpace(x.ChavePix));

            RuleFor(x => x)
                .Must(ChavePixCompativelComTipo)
                .WithMessage(x => MensagemFormatoInvalido(x.TipoChave))
                .When(x => x.TipoChave.HasValue && !string.IsNullOrWhiteSpace(x.ChavePix));
        }

        private static bool ChavePixCompativelComTipo(ContaBancariaInput input)
        {
            return input.TipoChave switch
            {
                TipoChave.Cpf => DocumentoBrasil.CpfValido(input.ChavePix),
                TipoChave.Cnpj => DocumentoBrasil.CnpjValido(input.ChavePix),
                TipoChave.Email => EmailAttribute.IsValid(input.ChavePix.Trim()),
                TipoChave.Telefone => TelefonePixValido(input.ChavePix),
                TipoChave.Aleatoria => Guid.TryParse(input.ChavePix.Trim(), out _),
                _ => false
            };
        }

        private static bool TelefonePixValido(string telefone)
        {
            if (string.IsNullOrWhiteSpace(telefone))
                return false;

            var digitos = telefone.SomenteDigitos();
            if (digitos.StartsWith("55") && digitos.Length is 12 or 13)
                digitos = digitos[2..];

            return digitos.TelefoneComDddValido();
        }

        private static string MensagemFormatoInvalido(TipoChave? tipoChave) =>
            tipoChave switch
            {
                TipoChave.Cpf => "Chave PIX inválida para o tipo CPF.",
                TipoChave.Cnpj => "Chave PIX inválida para o tipo CNPJ.",
                TipoChave.Email => "Chave PIX inválida para o tipo e-mail.",
                TipoChave.Telefone => "Chave PIX inválida para o tipo telefone (use DDD brasileiro ou formato +55).",
                TipoChave.Aleatoria => "Chave PIX inválida para o tipo aleatória (informe um UUID).",
                _ => "Chave PIX incompatível com o tipo informado."
            };
    }
}
