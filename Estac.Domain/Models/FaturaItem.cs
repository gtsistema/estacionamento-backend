using Estac.Domain.Models.Base;

namespace Estac.Domain.Models
{
    /// <summary>
    /// Vínculo entre a fatura e cada movimento cobrado. É o que torna a consulta de
    /// elegíveis idempotente: movimento já vinculado a uma fatura não cancelada não retorna.
    /// </summary>
    public class FaturaItem : BaseInt
    {
        public int FaturaId { get; set; }
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
        public Fatura Fatura { get; set; }
        public EntradaSaida EntradaSaida { get; set; }
    }
}
