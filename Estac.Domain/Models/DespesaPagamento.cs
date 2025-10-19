namespace Estac.Domain.Models
{
    public class DespesaPagamento : Base
    {
        public decimal ValorTotal { get; set; }
        public long DespesaId { get; set; }
        public virtual Despesa Despesa { get; set; }
    }
}
