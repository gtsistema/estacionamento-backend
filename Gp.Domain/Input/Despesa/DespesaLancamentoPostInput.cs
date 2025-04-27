
using Gp.Domain.Extensions;
using Gp.Domain.Models.Enuns;

namespace Gp.Domain.Input.Despesa
{
    public class DespesaLancamentoPostInput
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

