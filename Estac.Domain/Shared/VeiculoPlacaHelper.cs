using System.Text.RegularExpressions;

namespace Estac.Domain.Shared
{
    /// <summary>
    /// Placas brasileiras: persistência sem caracteres especiais (somente letras e números, maiúsculas);
    /// exibição no padrão mercosul (ABC-1D23) ou antigo (ABC-1234).
    /// </summary>
    public static class VeiculoPlacaHelper
    {
        public static string Normalizar(string placa)
        {
            if (string.IsNullOrWhiteSpace(placa))
                return placa;

            var apenas = Regex.Replace(placa, @"[^A-Za-z0-9]", string.Empty);
            return apenas.ToUpperInvariant();
        }

        /// <summary>
        /// Formata placa já normalizada (armazenada na base) para exibição.
        /// </summary>
        public static string FormatarExibicao(string placaArmazenada)
        {
            if (string.IsNullOrWhiteSpace(placaArmazenada))
                return placaArmazenada;

            var p = Normalizar(placaArmazenada);
            if (p.Length == 0)
                return placaArmazenada;

            if (p.Length == 7
                && char.IsLetter(p[0]) && char.IsLetter(p[1]) && char.IsLetter(p[2]))
            {
                // Mercosul: quinto caractere é letra (ex.: ABC1D23)
                if (char.IsLetter(p[4]))
                    return p[..3] + "-" + p[3..];

                // Antigo: quatro dígitos finais (ex.: ABC1234)
                if (char.IsDigit(p[3]) && char.IsDigit(p[4]) && char.IsDigit(p[5]) && char.IsDigit(p[6]))
                    return p[..3] + "-" + p[3..];
            }

            if (p.Length > 3 && char.IsLetter(p[0]) && char.IsLetter(p[1]) && char.IsLetter(p[2]))
                return p[..3] + "-" + p[3..];

            return p;
        }
    }
}
