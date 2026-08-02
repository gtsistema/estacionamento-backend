
namespace Estac.Domain.Extensions
{
    public static class StringExtentions
    {
        /// <summary>Remove pontuação; formato persistido para CPF e campos somente numéricos.</summary>
        public static string SomenteDigitos(this string valor)
        {
            if (valor == null)
                return null;
            return new string(valor.Where(char.IsDigit).ToArray());
        }

        /// <summary>
        /// Remove pontuação e mantém letras/números em maiúsculo.
        /// Formato persistido para CNPJ (numérico ou alfanumérico, 14 posições).
        /// </summary>
        public static string SomenteAlfanumericos(this string valor)
        {
            if (valor == null)
                return null;

            return new string(valor
                .Where(char.IsLetterOrDigit)
                .Select(char.ToUpperInvariant)
                .ToArray());
        }

        /// <summary>
        /// Normaliza CPF (11 dígitos) ou CNPJ (14 alfanuméricos) para persistência sem máscara.
        /// </summary>
        public static string NormalizarCpfOuCnpj(this string valor)
        {
            if (valor == null)
                return null;

            var alfanumerico = valor.SomenteAlfanumericos();
            if (string.IsNullOrEmpty(alfanumerico))
                return alfanumerico;

            if (alfanumerico.Length == 11 && alfanumerico.All(char.IsDigit))
                return alfanumerico;

            if (alfanumerico.Length == 14)
                return alfanumerico;

            return alfanumerico;
        }

        /// <summary>
        /// Máscara de CNPJ (AA.AAA.AAA/AAAA-DV) quando há 14 caracteres alfanuméricos.
        /// Compatível com CNPJ numérico e alfanumérico (Receita Federal, a partir de jul/2026).
        /// </summary>
        public static string FormatarCnpj(this string valor)
        {
            if (string.IsNullOrWhiteSpace(valor))
                return string.Empty;

            var d = valor.SomenteAlfanumericos();
            if (d.Length != 14)
                return valor.Trim();

            return $"{d[..2]}.{d[2..5]}.{d[5..8]}/{d[8..12]}-{d[12..14]}";
        }

        /// <summary>
        /// Aplica máscara de CPF (somente numérico, 11 dígitos) ou CNPJ (14 alfanuméricos).
        /// </summary>
        public static string FormatarCpfOuCnpj(this string valor)
        {
            if (string.IsNullOrWhiteSpace(valor))
                return valor;

            var alfanumerico = valor.SomenteAlfanumericos();

            if (alfanumerico.Length == 11 && alfanumerico.All(char.IsDigit))
                return alfanumerico.FormatarCpf();

            if (alfanumerico.Length == 14)
                return alfanumerico.FormatarCnpj();

            return valor.Trim();
        }

        public static string FormatarCpf(this string documento)
        {
            if (string.IsNullOrWhiteSpace(documento))
                return string.Empty;

            var cpf = new string(documento.Where(char.IsDigit).ToArray());

            if (!CpfValido(cpf))
                return documento;

            return Convert.ToUInt64(cpf).ToString(@"000\.000\.000\-00");
        }

        /// <summary>
        /// Formata telefone fixo/celular com DDD.
        /// 11: (DD) 9XXXX-XXXX | 10: (DD) XXXX-XXXX.
        /// </summary>
        public static string FormatarTelefone(this string telefone)
        {
            if (string.IsNullOrWhiteSpace(telefone))
                return string.Empty;

            var d = telefone.SomenteDigitos();
            if (string.IsNullOrWhiteSpace(d))
                return string.Empty;

            if (d.Length == 11)
                return $"({d[..2]}) {d[2..7]}-{d[7..11]}";

            if (d.Length == 10)
                return $"({d[..2]}) {d[2..6]}-{d[6..10]}";

            return telefone.Trim();
        }

        /// <summary>
        /// Telefone válido precisa ter DDD e comprimento de fixo/celular brasileiro.
        /// </summary>
        public static bool TelefoneComDddValido(this string telefone)
        {
            if (string.IsNullOrWhiteSpace(telefone))
                return false;

            var d = telefone.SomenteDigitos();
            if (d.Length is not (10 or 11))
                return false;

            var ddd = int.Parse(d[..2]);
            return ddd is >= 11 and <= 99;
        }

        public static string GerarCpf()
        {
            var random = new Random();

            var numeros = new int[9];

            for (int i = 0; i < 9; i++)
                numeros[i] = random.Next(0, 10);

            int soma = 0;
            for (int i = 0; i < 9; i++)
                soma += numeros[i] * (10 - i);

            int resto = soma % 11;
            int digito1 = resto < 2 ? 0 : 11 - resto;

            soma = 0;
            for (int i = 0; i < 9; i++)
                soma += numeros[i] * (11 - i);

            soma += digito1 * 2;

            resto = soma % 11;
            int digito2 = resto < 2 ? 0 : 11 - resto;

            return string.Concat(numeros) + digito1 + digito2;
        }

        private static bool CpfValido(string cpf)
        {
            if (cpf.Length != 11)
                return false;

            if (cpf.All(c => c == cpf[0]))
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
    }
}
