using Estac.Domain.Models.Enuns;

namespace Estac.Domain.Input.Receita
{
    public class ReceitaFilterInput
    {
        public long? Id { get; set; }
        public string Search { get; set; }
        public DateTime? DataInicial { get; set; }
        public DateTime? DataFinal { get; set; }
        public MesDoAno? Mes { get; set; }
        public int NumeroPagina { get; set; } = 1;
        public int TamanhoPagina { get; set; } = 10;
        public int PaginaAtual { get; set; }
    }
}
