namespace Estac.Domain.Integration.Report
{
    /// <summary>Configuração da API estacionamento-report.</summary>
    public sealed class EstacionamentoReportOptions
    {
        public const string SectionName = "EstacionamentoReport";
        public const string HttpClientName = "EstacionamentoReport";

        public bool Enabled { get; set; } = true;

        /// <summary>URL base do report, ex.: https://localhost:7223</summary>
        public string BaseUrl { get; set; } = string.Empty;

        public int TimeoutSeconds { get; set; } = 60;

        /// <summary>
        /// Rotas relativas (placeholders: {tipo}, {id}, {formato}).
        /// Ex.: estacreport/api/Relatorios/{tipo}/{id}?formato={formato}
        /// </summary>
        public EstacionamentoReportRoutes Routes { get; set; } = new();
    }

    public sealed class EstacionamentoReportRoutes
    {
        public string GerarRelatorio { get; set; } = "estacreport/api/Relatorios/{tipo}/{id}?formato={formato}";
    }
}
