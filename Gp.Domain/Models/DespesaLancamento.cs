
namespace Gp.Domain.Models
{
    public class DespesaLancamento : Base
    {
        public string Descricao { get; set; }
        public float ValorTotal { get; set; }
        public int DespesaId { get; set; }
        public virtual Despesa Despesa { get; set; }
    }
}
