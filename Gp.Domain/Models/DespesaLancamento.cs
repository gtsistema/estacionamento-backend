
namespace Gp.Domain.Models
{
    public class DespesaLancamento : Base
    {
        public decimal ValorTotal { get; set; }
        public long DespesaId { get; set; }
        public virtual Despesa Despesa { get; set; }
    }
}
