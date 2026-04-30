namespace Estac.Domain.Output.EntradaSaida
{
    public class EntradaSaidaSuspensaoOutput
    {
        public int Id { get; set; }
        public DateTime DataHoraInicioSuspensao { get; set; }
        public DateTime? DataHoraFimSuspensao { get; set; }
        public int TempoSuspensaoMinutos { get; set; }
        public int UsuarioSuspensaoId { get; set; }
        public string UsuarioSuspensaoNome { get; set; }
    }
}
