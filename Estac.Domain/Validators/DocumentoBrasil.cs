namespace Estac.Domain.Validators
{
    /// <summary>
    /// Validação de dígitos verificadores de CNPJ (apenas números ou formatado).
    /// </summary>
    internal static class DocumentoBrasil
    {
        public static bool CnpjValido(string valor)
        {
            if (string.IsNullOrWhiteSpace(valor))
                return false;

            var d = new string(valor.Where(char.IsDigit).ToArray());
            if (d.Length != 14 || d.All(c => c == d[0]))
                return false;

            var digitos = d.Select(c => c - '0').ToArray();

            var soma1 = digitos[0] * 5 + digitos[1] * 4 + digitos[2] * 3 + digitos[3] * 2
                + digitos[4] * 9 + digitos[5] * 8 + digitos[6] * 7 + digitos[7] * 6
                + digitos[8] * 5 + digitos[9] * 4 + digitos[10] * 3 + digitos[11] * 2;
            var r1 = soma1 % 11;
            var dv1 = r1 < 2 ? 0 : 11 - r1;
            if (dv1 != digitos[12])
                return false;

            var soma2 = digitos[0] * 6 + digitos[1] * 5 + digitos[2] * 4 + digitos[3] * 3
                + digitos[4] * 2 + digitos[5] * 9 + digitos[6] * 8 + digitos[7] * 7
                + digitos[8] * 6 + digitos[9] * 5 + digitos[10] * 4 + digitos[11] * 3
                + digitos[12] * 2;
            var r2 = soma2 % 11;
            var dv2 = r2 < 2 ? 0 : 11 - r2;
            return dv2 == digitos[13];
        }
    }
}
