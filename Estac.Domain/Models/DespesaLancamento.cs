using Estac.Domain.Models.ModelEnum;

namespace Estac.Domain.Models
{
    public class DespesaLancamento : Base
    {
        public bool CompraParcelado { get; set; } = false;
        public int QuantidadeParcelado { get; set; }
        public string Pagamento {  get; set; } // Avista // Parcelado
        public Cartao? Cartao { get; set; }
        public decimal Valor { get; set; }
        public long DespesaId { get; set; }
        public virtual Despesa Despesa { get; set; }
    }
}