namespace Gp.Domain.Shared
{
    public class PagedQuery<T> where T : class
    {
        public int PaginaAtual { get; set; }
        public int PaginaTotal { get; set; }
        public int TamanhoPagina { get; set; }
        public string Sort { get; set; }
        public IEnumerable<T> Dados { get; set; }
    }
}
