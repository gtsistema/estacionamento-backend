using Estac.Domain.Extensions;

namespace Estac.Domain.Validators
{
    /// <summary>
    /// Validação de dígitos verificadores de CPF/CNPJ (numérico ou alfanumérico).
    /// CNPJ: algoritmo módulo 11 da Receita Federal (valor de cada caractere = ASCII - 48).
    /// </summary>
    internal static class DocumentoBrasil
    {
        public static bool CpfValido(string valor)
        {
            if (string.IsNullOrWhiteSpace(valor))
                return false;

            var cpf = valor.SomenteDigitos();
            if (cpf.Length != 11 || cpf.All(c => c == cpf[0]))
                return false;

            var numeros = cpf.Select(c => c - '0').ToArray();

            var soma = 0;
            for (var i = 0; i < 9; i++)
                soma += numeros[i] * (10 - i);

            var resto = soma % 11;
            var digito1 = resto < 2 ? 0 : 11 - resto;

            soma = 0;
            for (var i = 0; i < 9; i++)
                soma += numeros[i] * (11 - i);

            soma += digito1 * 2;
            resto = soma % 11;
            var digito2 = resto < 2 ? 0 : 11 - resto;

            return numeros[9] == digito1 && numeros[10] == digito2;
        }

        public static bool CnpjValido(string valor)
        {
            if (string.IsNullOrWhiteSpace(valor))
                return false;

            var d = valor.SomenteAlfanumericos();
            if (d.Length != 14 || d.All(c => c == d[0]))
                return false;

            // DVs são sempre numéricos.
            if (!char.IsDigit(d[12]) || !char.IsDigit(d[13]))
                return false;

            var valores = d.Select(c => c - 48).ToArray();

            var soma1 = valores[0] * 5 + valores[1] * 4 + valores[2] * 3 + valores[3] * 2
                + valores[4] * 9 + valores[5] * 8 + valores[6] * 7 + valores[7] * 6
                + valores[8] * 5 + valores[9] * 4 + valores[10] * 3 + valores[11] * 2;
            var r1 = soma1 % 11;
            var dv1 = r1 < 2 ? 0 : 11 - r1;
            if (dv1 != valores[12])
                return false;

            var soma2 = valores[0] * 6 + valores[1] * 5 + valores[2] * 4 + valores[3] * 3
                + valores[4] * 2 + valores[5] * 9 + valores[6] * 8 + valores[7] * 7
                + valores[8] * 6 + valores[9] * 5 + valores[10] * 4 + valores[11] * 3
                + valores[12] * 2;
            var r2 = soma2 % 11;
            var dv2 = r2 < 2 ? 0 : 11 - r2;
            return dv2 == valores[13];
        }
    }
}
