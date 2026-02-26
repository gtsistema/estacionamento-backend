using System;

namespace Estac.Domain.Input
{
    public class FilterInput
    {
        public string Search { get; set; }
        public DateTime? DataInicial { get; set; }
        public DateTime? DataFinal { get; set; }
        public int NumeroPagina { get; set; } = 1;
        public int TamanhoPagina { get; set; } = 10;
        public string Propriedade { get; set; }
        public string Sort { get; set; } = "asc";
    }
}
