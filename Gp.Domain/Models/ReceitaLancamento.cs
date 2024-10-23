namespace Gp.Domain.Models
{
    public class ReceitaLancamento : Base
    {
        public string Descricao { get; set; }
        public decimal ValorTotal { get; set; }
        public long ReceitaId { get; set; }
        public virtual Receita Receita { get; set; }
    }
}
