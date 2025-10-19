
using Estac.Domain.Extensions;
using Estac.Domain.Models.Enuns;
using Estac.Domain.Models.ModelEnum;

namespace Estac.Domain.Input.Despesa
{
    public class DespesaLancamentoPostInput
    {
        public string Descricao { get; set; }
        public bool CompraParcelado { get; set; } = false;
        public int QuantidadeParcelado { get; set; }
        public string Pagamento { get; set; } // Avista // Parcelado
        public Cartao? Cartao { get; set; }
        public decimal Valor { get; set; }
        public string DescricaoDespesa { get; set; }
        public MesDoAno? MesReferente { get; set; }
        public long? DespesaId { get; set; }
        public long? OrcamentoId { get; set; }

        public void AtribuirMesReferente()
        {
            if (MesReferente.HasValue)
                return;

            MesReferente = DataExtesions.ObterMesAtualEnum();
        }

        public void AtribuirDespesaId(long id) => DespesaId = id;
    }
}

