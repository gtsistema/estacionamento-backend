namespace Estac.Domain.Input.Fatura
{
    public class FaturaItemPostInput
    {
        public int EntradaSaidaId { get; set; }
        public string Placa { get; set; }
        public DateTime DataHoraEntrada { get; set; }
        public DateTime DataHoraSaida { get; set; }
        public int TempoPermanenciaMinutos { get; set; }
        public decimal ValorEstacionamento { get; set; }
        public decimal ValorLavagem { get; set; }
        public decimal ValorPernoite { get; set; }
        public decimal ValorServicosExtras { get; set; }
        public decimal ValorBeneficioAbastecimento { get; set; }
        public decimal ValorTotal { get; set; }
        public string Descricao { get; set; }
    }
}
