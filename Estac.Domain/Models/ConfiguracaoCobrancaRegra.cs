using Estac.Domain.Models.Base;

namespace Estac.Domain.Models
{
    public class ConfiguracaoCobrancaRegra : BaseInt
    {
        public int ConfiguracaoCobrancaId { get; set; }
        public bool CobrarDiaria { get; set; }
        public bool CobrarSemanal { get; set; }
        public bool CobrarQuinzenal { get; set; }
        public bool CobrarMensal { get; set; }
        public bool CobrarDataPersonalizada { get; set; }
        public bool CobrarLavagem { get; set; }
        public bool CobrarPernoite { get; set; }
        public bool CobrarServicosExtras { get; set; }
        public bool ConsiderarBeneficioAbastecimento { get; set; }

        public ConfiguracaoCobranca ConfiguracaoCobranca { get; set; }
    }
}
