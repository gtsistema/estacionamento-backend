namespace Estac.Domain.Models
{
    public class ReceitaLancamento : Base
    {
        public decimal ValorTotal { get; set; }
        public long ReceitaId { get; set; }
        public virtual Receita Receita { get; set; }
    }
}
