using Estac.Domain.Extensions;
using Estac.Domain.Models.Enuns;

namespace Estac.Domain.Input.Receita
{
    public class ReceitaLancamentoPostInput
    {
        public string Descricao { get; set; }
        public decimal ValorTotal { get; set; }
        public string DescricaoReceita { get; set; }
        public MesDoAno? MesReferente { get; set; }
        public long? ReceitaId { get; set; }


        public void AtribuirMesReferente()
        {
            if (MesReferente.HasValue)
                return;

            MesReferente = DataExtesions.ObterMesAtualEnum();
        }

        public void AtribuirReceitaId(long id) => ReceitaId = id;
    }
}

