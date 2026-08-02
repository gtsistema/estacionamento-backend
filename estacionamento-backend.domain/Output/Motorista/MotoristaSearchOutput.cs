namespace Estac.Domain.Output.Motorista
{
    public class MotoristaSearchOutput
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public string CNH { get; set; }
        public string Cpf { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
        public DateTime? ValidadeCNH { get; set; }
        public int PessoaId { get; set; }
        public int? TransportadoraId { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataAtualizacao { get; set; }
        public bool Ativo { get; set; }
    }
}
