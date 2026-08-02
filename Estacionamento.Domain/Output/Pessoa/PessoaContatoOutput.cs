namespace Estac.Domain.Output.Pessoa
{
    public class PessoaContatoOutput
    {
        public int Id { get; set; }
        public int PessoaId { get; set; }
        public string Descricao { get; set; }
        public string Cpf { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public bool Principal { get; set; }
        public string Observacao { get; set; }
    }
}
