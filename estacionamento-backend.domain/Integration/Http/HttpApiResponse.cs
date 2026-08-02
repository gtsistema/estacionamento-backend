using System.Net;

namespace Estac.Domain.Integration.Http
{
    public sealed class HttpApiResponse
    {
        public bool Success { get; init; }
        public HttpStatusCode StatusCode { get; init; }
        public string ErrorBody { get; init; }

        public static HttpApiResponse Ok(HttpStatusCode statusCode) =>
            new() { Success = true, StatusCode = statusCode };

        public static HttpApiResponse Fail(HttpStatusCode statusCode, string errorBody) =>
            new() { Success = false, StatusCode = statusCode, ErrorBody = errorBody };
    }

    public sealed class HttpApiResponse<T>
    {
        public bool Success { get; init; }
        public HttpStatusCode StatusCode { get; init; }
        public T Data { get; init; }
        public string ErrorBody { get; init; }

        public static HttpApiResponse<T> Ok(T data, HttpStatusCode statusCode) =>
            new() { Success = true, StatusCode = statusCode, Data = data };

        public static HttpApiResponse<T> Fail(HttpStatusCode statusCode, string errorBody) =>
            new() { Success = false, StatusCode = statusCode, ErrorBody = errorBody };
    }
}
