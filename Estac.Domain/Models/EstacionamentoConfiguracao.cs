namespace Estac.Domain.Models
{
    /// <summary>
    /// Configuração 1:1 do estacionamento.
    /// Datas de movimento em UTC; TimeZoneId (IANA) define a conversão local.
    /// </summary>
    public class EstacionamentoConfiguracao
    {
        public int Id { get; set; }
        public int EstacionamentoId { get; set; }

        /// <summary>Identificador IANA do fuso (ex.: America/Cuiaba, America/Sao_Paulo).</summary>
        public string TimeZoneId { get; set; }

        public string Cultura { get; set; } = "pt-BR";
        public bool Ativo { get; set; } = true;
        public DateTime DataCriacao { get; set; }
        public DateTime? DataAtualizacao { get; set; }

        public Estacionamento Estacionamento { get; set; }
    }
}
