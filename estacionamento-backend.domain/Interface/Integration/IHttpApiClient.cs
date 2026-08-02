using Estac.Domain.Integration.Http;

namespace Estac.Domain.Interface.Integration
{
    /// <summary>
    /// Cliente HTTP genérico para APIs externas (GET, POST, PUT, DELETE).
    /// Use o nome do HttpClient registrado no DI (ex.: EstacionamentoReport).
    /// </summary>
    public interface IHttpApiClient
    {
        Task<HttpApiResponse<T>> GetAsync<T>(
            string httpClientName,
            string relativePath,
            CancellationToken cancellationToken = default);

        Task<HttpApiFileResponse> GetFileAsync(
            string httpClientName,
            string relativePath,
            CancellationToken cancellationToken = default);

        Task<HttpApiResponse<TResponse>> PostAsync<TRequest, TResponse>(
            string httpClientName,
            string relativePath,
            TRequest body,
            CancellationToken cancellationToken = default);

        Task<HttpApiResponse<TResponse>> PutAsync<TRequest, TResponse>(
            string httpClientName,
            string relativePath,
            TRequest body,
            CancellationToken cancellationToken = default);

        Task<HttpApiResponse> DeleteAsync(
            string httpClientName,
            string relativePath,
            CancellationToken cancellationToken = default);
    }
}
