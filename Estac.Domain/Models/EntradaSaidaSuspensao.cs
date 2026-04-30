namespace Estac.Domain.Models
{
    public class EntradaSaidaSuspensao
    {
        public int Id { get; set; }
        public int EntradaSaidaId { get; set; }
        public DateTime DataHoraInicioSuspensao { get; set; }
        public DateTime? DataHoraFimSuspensao { get; set; }
        public int TempoSuspensaoMinutos { get; set; }
        public int UsuarioSuspensaoId { get; set; }
        public string UsuarioSuspensaoNome { get; set; }

        public EntradaSaida EntradaSaida { get; set; }
    }
}
