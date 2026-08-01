namespace Estac.Domain.Output.Estacionamento
{
    /// <summary>Item do dropdown de fusos do Brasil.</summary>
    public class FusoHorarioPadraoOutput
    {
        public string TimeZoneId { get; set; }
        public string Nome { get; set; }
        public string UtcOffset { get; set; }
    }

    public class EstacionamentoConfiguracaoOutput
    {
        public int Id { get; set; }
        public int EstacionamentoId { get; set; }

        /// <summary>Valor selecionado no dropdown (IANA).</summary>
        public string TimeZoneId { get; set; }

        /// <summary>Nome amigável resolvido do catálogo (ex.: Mato Grosso / Cuiabá (UTC-04)).</summary>
        public string Nome { get; set; }

        public string UtcOffset { get; set; }
        public string Cultura { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataAtualizacao { get; set; }
    }
}
