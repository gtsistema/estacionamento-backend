using Estac.Domain.Output.Base;
using Estac.Domain.Output.Motorista;
using Estac.Domain.Output.Transportadora;
using Estac.Domain.Output.Veiculo;

namespace Estac.Domain.Output.Movimento.EntradaSaida
{
    public class EntradaSaidaOutput
    {
        public string Descricao { get; set; }
        public string Status { get; set; }
        public int MotoristaId { get; set; }
        public int? TransportadoraId { get; set; }
        public int VeiculoId { get; set; }
        public DateTime DataHoraEntrada { get; set; }
        public DateTime? DataHoraSaida { get; set; }
        public int UsuarioRegistroEntradaId { get; set; }
        public string UsuarioRegistroEntradaNome { get; set; }
        public int? UsuarioFinalizacaoId { get; set; }
        public string UsuarioFinalizacaoNome { get; set; }
        public DateTime? DataHoraUltimaEntradaPatio { get; set; }
        public int TempoPermanenciaMinutos { get; set; }
        public int TempoTotalSuspensaoMinutos { get; set; }
        public bool PermanenciaSuspensa { get; set; }
        public bool Finalizado { get; set; }
        public bool Faturado { get; set; }
        public DateTime? DataFaturado { get; set; }
        public string Observacao { get; set; }
        public DateTime? DataHoraFinalizacao { get; set; }
        public MotoristaOutput Motorista { get; set; }
        public TransportadoraOutput Transportadora { get; set; }
        public VeiculoOutput Veiculo { get; set; }
        public ICollection<EntradaSaidaSuspensaoOutput> Suspensoes { get; set; } = new List<EntradaSaidaSuspensaoOutput>();
    }
}
