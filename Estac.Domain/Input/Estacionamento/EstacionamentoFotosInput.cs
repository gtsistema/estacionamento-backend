using Microsoft.AspNetCore.Http;

namespace Estac.Domain.Input.Estacionamento
{
    public class EstacionamentoFotosInput
    {
        public int EstacionamentoId { get; set; }
        public List<IFormFile> Fotos { get; set; } = new();
    }
}
