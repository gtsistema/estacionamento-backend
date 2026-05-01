
namespace Estac.Domain.Extensions
{
    public static class StringExtentions
    {
        public static string FormatarCpf(this string documento)
        {
            if (string.IsNullOrWhiteSpace(documento))
                return string.Empty;

            var cpf = new string(documento.Where(char.IsDigit).ToArray());

            if (!CpfValido(cpf))
                return documento;

            return Convert.ToUInt64(cpf).ToString(@"000\.000\.000\-00");
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
