
namespace Estac.Domain.Output.Cursos
{
    public class LivroOutput
    {
        public string Autor { get; set; }
        public string Descricao { get; set; }
        public DateTime? DataLancamento { get; set; } = DateTime.Now;
        public decimal Valor { get; set; }
    }
}
