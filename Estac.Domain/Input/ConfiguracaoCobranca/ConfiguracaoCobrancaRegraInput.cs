namespace Estac.Domain.Input.ConfiguracaoCobranca
{
    public class ConfiguracaoCobrancaRegraInput
    {
        public int Id { get; set; }
        public bool CobrarDiaria { get; set; }
        public bool CobrarSemanal { get; set; }
        public bool CobrarQuinzenal { get; set; }
        public bool CobrarMensal { get; set; }
        public bool CobrarDataPersonalizada { get; set; }
        public bool CobrarLavagem { get; set; }
        public bool CobrarPernoite { get; set; }
        public bool CobrarServicosExtras { get; set; }
        public bool ConsiderarBeneficioAbastecimento { get; set; }
    }
}
