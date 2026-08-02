namespace Estac.Domain.Output.Faturamento
{
    /// <summary>
    /// Pacote completo para o job: agendamento + regras + janela de competência + movimentos.
    /// </summary>
    public class ConfiguracaoFaturavelOutput : AgendamentoFaturamentoOutput
    {
        public DateTime PeriodoInicio { get; set; }
        public DateTime PeriodoFim { get; set; }
        public IList<EntradaSaidaFaturavelOutput> Movimentos { get; set; } = new List<EntradaSaidaFaturavelOutput>();
    }
}
