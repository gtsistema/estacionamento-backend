using System.Net.Http.Json;
using System.Text.Json;
using Estac.Domain.Integration.Workers;
using Estac.Domain.Interface.Integration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Estac.Infra.Integration.Workers
{
    public sealed class EstacionamentoWorkersClient : IEstacionamentoWorkersClient
    {
        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true,
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
        };

        private const string MovimentacaoTempoRealPath = "api/movimento/EntradaSaida/movimentacaoTempoReal";

        private readonly HttpClient _httpClient;
        private readonly IOptions<EstacionamentoWorkersOptions> _options;
        private readonly ILogger<EstacionamentoWorkersClient> _logger;

        public EstacionamentoWorkersClient(
            HttpClient httpClient,
            IOptions<EstacionamentoWorkersOptions> options,
            ILogger<EstacionamentoWorkersClient> logger)
        {
            _httpClient = httpClient;
            _options = options;
            _logger = logger;
        }

        public async Task<MovimentacaoTempoRealResponse?> RegistrarMovimentacaoTempoRealAsync(
            MovimentacaoTempoRealRequest request,
            CancellationToken cancellationToken = default)
        {
      
            var opts = _options.Value;

            _logger.LogWarning(@$"base: {opts.BaseUrl}");
            _logger.LogWarning(@$"opts: {opts}");

            if (!opts.Enabled || string.IsNullOrWhiteSpace(opts.BaseUrl))
                return null;

            if (request is null)
                return null;

            try
            {
                using var response = await _httpClient.PostAsJsonAsync(MovimentacaoTempoRealPath, request, JsonOptions, cancellationToken);
                if (!response.IsSuccessStatusCode)
                {
                    var body = await response.Content.ReadAsStringAsync(cancellationToken);
                    _logger.LogWarning(
                        "estacionamento-workers respondeu {StatusCode} ao registrar movimentação. Corpo: {Body}",
                        (int)response.StatusCode,
                        body.Length > 500 ? body[..500] : body);
                    return null;
                }

                return await response.Content.ReadFromJsonAsync<MovimentacaoTempoRealResponse>(JsonOptions, cancellationToken);
            }
            catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Falha ao chamar estacionamento-workers (movimentação em tempo real). A entrada já foi gravada.");
                return null;
            }
        }
    }
}
