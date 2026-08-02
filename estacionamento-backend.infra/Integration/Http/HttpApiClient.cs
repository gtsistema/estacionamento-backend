using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Estac.Domain.Integration.Http;
using Estac.Domain.Interface.Integration;
using Microsoft.Extensions.Logging;

namespace Estac.Infra.Integration.Http
{
    public sealed class HttpApiClient : IHttpApiClient
    {
        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true,
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
        };

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<HttpApiClient> _logger;

        public HttpApiClient(IHttpClientFactory httpClientFactory, ILogger<HttpApiClient> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<HttpApiResponse<T>> GetAsync<T>(
            string httpClientName,
            string relativePath,
            CancellationToken cancellationToken = default)
        {
            var client = CreateClient(httpClientName);
            using var response = await client.GetAsync(relativePath, cancellationToken);
            return await ReadJsonAsync<T>(response, cancellationToken);
        }

        public async Task<HttpApiFileResponse> GetFileAsync(
            string httpClientName,
            string relativePath,
            CancellationToken cancellationToken = default)
        {
            var client = CreateClient(httpClientName);
            using var response = await client.GetAsync(relativePath, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                var errorBody = await response.Content.ReadAsStringAsync(cancellationToken);
                LogFailure("GET file", httpClientName, relativePath, response.StatusCode, errorBody);
                return HttpApiFileResponse.Fail(response.StatusCode, Truncate(errorBody));
            }

            var bytes = await response.Content.ReadAsByteArrayAsync(cancellationToken);
            var contentType = response.Content.Headers.ContentType?.ToString() ?? "application/octet-stream";
            var fileName = ResolveFileName(response.Content.Headers.ContentDisposition);

            return HttpApiFileResponse.Ok(bytes, contentType, fileName, response.StatusCode);
        }

        public async Task<HttpApiResponse<TResponse>> PostAsync<TRequest, TResponse>(
            string httpClientName,
            string relativePath,
            TRequest body,
            CancellationToken cancellationToken = default)
        {
            var client = CreateClient(httpClientName);
            using var response = await client.PostAsJsonAsync(relativePath, body, JsonOptions, cancellationToken);
            return await ReadJsonAsync<TResponse>(response, cancellationToken);
        }

        public async Task<HttpApiResponse<TResponse>> PutAsync<TRequest, TResponse>(
            string httpClientName,
            string relativePath,
            TRequest body,
            CancellationToken cancellationToken = default)
        {
            var client = CreateClient(httpClientName);
            using var response = await client.PutAsJsonAsync(relativePath, body, JsonOptions, cancellationToken);
            return await ReadJsonAsync<TResponse>(response, cancellationToken);
        }

        public async Task<HttpApiResponse> DeleteAsync(
            string httpClientName,
            string relativePath,
            CancellationToken cancellationToken = default)
        {
            var client = CreateClient(httpClientName);
            using var response = await client.DeleteAsync(relativePath, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                var errorBody = await response.Content.ReadAsStringAsync(cancellationToken);
                LogFailure("DELETE", httpClientName, relativePath, response.StatusCode, errorBody);
                return HttpApiResponse.Fail(response.StatusCode, Truncate(errorBody));
            }

            return HttpApiResponse.Ok(response.StatusCode);
        }

        private HttpClient CreateClient(string httpClientName)
        {
            if (string.IsNullOrWhiteSpace(httpClientName))
                throw new ArgumentException("Nome do HttpClient é obrigatório.", nameof(httpClientName));

            return _httpClientFactory.CreateClient(httpClientName);
        }

        private async Task<HttpApiResponse<T>> ReadJsonAsync<T>(
            HttpResponseMessage response,
            CancellationToken cancellationToken)
        {
            if (!response.IsSuccessStatusCode)
            {
                var errorBody = await response.Content.ReadAsStringAsync(cancellationToken);
                LogFailure(
                    response.RequestMessage?.Method.Method ?? "HTTP",
                    response.RequestMessage?.RequestUri?.Host ?? "unknown",
                    response.RequestMessage?.RequestUri?.PathAndQuery ?? string.Empty,
                    response.StatusCode,
                    errorBody);
                return HttpApiResponse<T>.Fail(response.StatusCode, Truncate(errorBody));
            }

            if (response.StatusCode == HttpStatusCode.NoContent)
                return HttpApiResponse<T>.Ok(default, response.StatusCode);

            var data = await response.Content.ReadFromJsonAsync<T>(JsonOptions, cancellationToken);
            return HttpApiResponse<T>.Ok(data, response.StatusCode);
        }

        private void LogFailure(
            string method,
            string httpClientName,
            string relativePath,
            HttpStatusCode statusCode,
            string errorBody)
        {
            _logger.LogWarning(
                "HttpApiClient {Method} [{Client}] {Path} respondeu {StatusCode}. Corpo: {Body}",
                method,
                httpClientName,
                relativePath,
                (int)statusCode,
                Truncate(errorBody));
        }

        private static string ResolveFileName(ContentDispositionHeaderValue contentDisposition)
        {
            if (contentDisposition is null)
                return null;

            var fileName = contentDisposition.FileNameStar ?? contentDisposition.FileName;
            return string.IsNullOrWhiteSpace(fileName) ? null : fileName.Trim('"');
        }

        private static string Truncate(string value)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            return value.Length > 500 ? value[..500] : value;
        }
    }
}
