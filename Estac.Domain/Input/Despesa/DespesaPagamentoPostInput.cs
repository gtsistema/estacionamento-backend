using Estac.Domain.Extensions;
using Estac.Domain.Models.Enuns;

namespace Estac.Domain.Input.Despesa
{
    public class DespesaPagamentoPostInput
    {
        public string Descricao { get; set; }
        public decimal ValorTotal { get; set; }
        public string DescricaoDespesa { get; set; }
        public MesDoAno? MesReferente { get; set; }
        public long? DespesaId { get; set; }

        public void AtribuirMesReferente()
        {
            if (MesReferente.HasValue)
                return;

            MesReferente = DataExtesions.ObterMesAtualEnum();
        }

        public void AtribuirDespesaId(long id) => DespesaId = id;
    }
}

