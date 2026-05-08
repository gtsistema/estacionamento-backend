using Estac.Domain.Models.Base;
using Estac.Domain.Models.Enuns;

namespace Estac.Domain.Models
{
    public class EntradaSaida : BaseInt
    {
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
        public EntradaSaidaStatus Status { get; set; }
        public DateTime? DataHoraFinalizacao { get; set; }
        public string Observacao { get; set; }
        public Motorista Motorista { get; set; }
        public Transportadora Transportadora { get; set; }
        public Veiculo Veiculo { get; set; }
        public ICollection<EntradaSaidaSuspensao> Suspensoes { get; set; } = new List<EntradaSaidaSuspensao>();
    }
}
