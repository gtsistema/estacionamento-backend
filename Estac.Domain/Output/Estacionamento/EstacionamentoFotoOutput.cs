
namespace Estac.Domain.Output.Estacionamento
{
    public class MenuFotoOutput
    {
        public int Id { get; set; }
        public string NomeArquivo { get; set; }
        public bool EhPrincipal { get; set; }
        public int Ordem { get; set; }
        public string ContentType { get; set; }
        public string FotoBase64 { get; set; }
    }
}
