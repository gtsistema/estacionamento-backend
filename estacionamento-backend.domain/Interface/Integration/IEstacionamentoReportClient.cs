using Estac.Domain.Integration.Http;
using Estac.Domain.Models.Enuns;

namespace Estac.Domain.Interface.Integration
{
    public interface IEstacionamentoReportClient
    {
        /// <summary>GET Relatorios/Gerar — retorna o arquivo do relatório (PDF/Excel).</summary>
        Task<HttpApiFileResponse> GerarRelatorioAsync(
            string tipo,
            int id,
            FormatoRelatorio formato,
            CancellationToken cancellationToken = default);
    }
}
