using Estac.Domain.Models.Enuns;

namespace Estac.Domain.Output.Cursos
{
    public class LivroFilterOutput
    {
        public string Search { get; set; }
        public DateTime? DataInicial { get; set; }
        public DateTime? DataFinal { get; set; }
        public int NumeroPagina { get; set; } = 1;
        public int TamanhoPagina { get; set; } = 10;
        public int PaginaAtual { get; set; }
        public string Sort { get; set; } 
        public IList<LivroOutput> Livros { get; set; }
    }
}
