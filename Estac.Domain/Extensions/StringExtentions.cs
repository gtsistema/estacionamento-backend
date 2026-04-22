
namespace Estac.Domain.Extensions
{
    public static class StringExtentions
    {
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
    }
}
