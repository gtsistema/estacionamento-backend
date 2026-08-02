using System.Net;

namespace Estac.Domain.Integration.Http
{
    public sealed class HttpApiFileResponse
    {
        public bool Success { get; init; }
        public HttpStatusCode StatusCode { get; init; }
        public byte[] Content { get; init; } = Array.Empty<byte>();
        public string ContentType { get; init; } = "application/octet-stream";
        public string FileName { get; init; }
        public string ErrorBody { get; init; }

        public static HttpApiFileResponse Ok(
            byte[] content,
            string contentType,
            string fileName,
            HttpStatusCode statusCode) =>
            new()
            {
                Success = true,
                StatusCode = statusCode,
                Content = content ?? Array.Empty<byte>(),
                ContentType = string.IsNullOrWhiteSpace(contentType) ? "application/octet-stream" : contentType,
                FileName = fileName
            };

        public static HttpApiFileResponse Fail(HttpStatusCode statusCode, string errorBody) =>
            new() { Success = false, StatusCode = statusCode, ErrorBody = errorBody };
    }
}
