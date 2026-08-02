using System.Net;
using Estac.Domain.Integration.Http;
using Estac.Domain.Integration.Report;
using Estac.Domain.Interface.Integration;
using Estac.Domain.Models.Enuns;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Estac.Infra.Integration.Report
{
    public sealed class EstacionamentoReportClient : IEstacionamentoReportClient
    {
        private readonly IHttpApiClient _httpApiClient;
        private readonly IOptions<EstacionamentoReportOptions> _options;
        private readonly ILogger<EstacionamentoReportClient> _logger;

        public EstacionamentoReportClient(
            IHttpApiClient httpApiClient,
            IOptions<EstacionamentoReportOptions> options,
            ILogger<EstacionamentoReportClient> logger)
        {
            _httpApiClient = httpApiClient;
            _options = options;
            _logger = logger;
        }

        public async Task<HttpApiFileResponse> GerarRelatorioAsync(
            string tipo,
            int id,
            FormatoRelatorio formato,
            CancellationToken cancellationToken = default)
        {
            var opts = _options.Value;

            if (!opts.Enabled || string.IsNullOrWhiteSpace(opts.BaseUrl))
            {
                _logger.LogWarning("estacionamento-report está desabilitado ou sem BaseUrl configurada.");
                return HttpApiFileResponse.Fail(HttpStatusCode.ServiceUnavailable, "Serviço de relatório indisponível.");
            }

            if (string.IsNullOrWhiteSpace(tipo))
                return HttpApiFileResponse.Fail(HttpStatusCode.BadRequest, "Tipo de relatório é obrigatório.");

            if (id <= 0)
                return HttpApiFileResponse.Fail(HttpStatusCode.BadRequest, "Id do relatório é inválido.");

            if (!Enum.IsDefined(formato))
                return HttpApiFileResponse.Fail(HttpStatusCode.BadRequest, "Formato de relatório inválido.");

            var routeTemplate = opts.Routes?.GerarRelatorio;
            if (string.IsNullOrWhiteSpace(routeTemplate))
            {
                _logger.LogError("Rota EstacionamentoReport:Routes:GerarRelatorio não configurada.");
                return HttpApiFileResponse.Fail(HttpStatusCode.InternalServerError, "Rota de relatório não configurada.");
            }

            var path = BuildPath(routeTemplate, tipo.Trim(), id, formato);

            try
            {
                return await _httpApiClient.GetFileAsync(
                    EstacionamentoReportOptions.HttpClientName,
                    path,
                    cancellationToken);
            }
            catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Falha ao gerar relatório {Tipo}/{Id} no estacionamento-report.", tipo, id);
                return HttpApiFileResponse.Fail(HttpStatusCode.BadGateway, "Falha ao comunicar com o serviço de relatório.");
            }
        }

        private static string BuildPath(string template, string tipo, int id, FormatoRelatorio formato) =>
            template
                .Replace("{tipo}", Uri.EscapeDataString(tipo), StringComparison.OrdinalIgnoreCase)
                .Replace("{id}", id.ToString(), StringComparison.OrdinalIgnoreCase)
                .Replace("{formato}", Uri.EscapeDataString(formato.ToString()), StringComparison.OrdinalIgnoreCase)
                .TrimStart('/');
    }
}
