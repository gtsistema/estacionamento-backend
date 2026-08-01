namespace Estac.Domain.Shared
{
    /// <summary>
    /// Fusos IANA oficiais utilizados no território brasileiro.
    /// </summary>
    public static class FusoHorarioBrasilPadroes
    {
        public static readonly (string TimeZoneId, string Nome, string UtcOffset)[] Todos =
        {
            ("America/Noronha", "Fernando de Noronha (UTC-02)", "-02:00"),
            ("America/Sao_Paulo", "Horário de Brasília (UTC-03)", "-03:00"),
            ("America/Araguaina", "Tocantins / Araguaína (UTC-03)", "-03:00"),
            ("America/Bahia", "Bahia (UTC-03)", "-03:00"),
            ("America/Belem", "Pará oriental / Belém (UTC-03)", "-03:00"),
            ("America/Fortaleza", "Nordeste / Fortaleza (UTC-03)", "-03:00"),
            ("America/Maceio", "Alagoas e Sergipe / Maceió (UTC-03)", "-03:00"),
            ("America/Recife", "Pernambuco / Recife (UTC-03)", "-03:00"),
            ("America/Santarem", "Pará ocidental / Santarém (UTC-03)", "-03:00"),
            ("America/Cuiaba", "Mato Grosso / Cuiabá (UTC-04)", "-04:00"),
            ("America/Campo_Grande", "Mato Grosso do Sul / Campo Grande (UTC-04)", "-04:00"),
            ("America/Manaus", "Amazonas / Manaus (UTC-04)", "-04:00"),
            ("America/Porto_Velho", "Rondônia / Porto Velho (UTC-04)", "-04:00"),
            ("America/Boa_Vista", "Roraima / Boa Vista (UTC-04)", "-04:00"),
            ("America/Rio_Branco", "Acre / Rio Branco (UTC-05)", "-05:00"),
            ("America/Eirunepe", "Amazonas sudoeste / Eirunepé (UTC-05)", "-05:00"),
        };
    }
}
