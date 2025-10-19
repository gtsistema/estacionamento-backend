
using Estac.Domain.Extensions;
using Estac.Domain.Models.Enuns;

namespace Estac.Domain.Input.Despesa
{
    public class AtualizaSaldoDoMesPostInput
    {
        public MesDoAno MesReferente { get; set; }
        public TipoDespesa TipoDespesa { get; set; }
    }
}

