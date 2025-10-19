
namespace Estac.Domain.Models
{
    public class Livro : Base
    {
        public string Autor { get; set; }
        public DateTime DataLancamento { get; set; }
        public decimal Valor { get; set; }
    }
}
