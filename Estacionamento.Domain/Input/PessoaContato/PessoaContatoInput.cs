namespace Estac.Domain.Input.PessoaContato
{
    /// <summary>
    /// Mesmos escalares que <c>PessoaContato</c> na camada de modelo (sem navegação para pessoa).
    /// </summary>
    public class PessoaContatoInput
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
