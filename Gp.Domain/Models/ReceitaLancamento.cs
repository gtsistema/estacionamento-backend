namespace Gp.Domain.Models
{
    public class ReceitaLancamento : Base
    {
        public string Descricao { get; set; }
        public float ValorTotal { get; set; }
        public int ReceitaId { get; set; }
        public virtual Receita Receita { get; set; }
    }
}
