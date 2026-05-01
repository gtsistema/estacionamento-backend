namespace Estac.Domain.Output.Transportadora
{
    public class TransportadoraSearchOutput
    {
        public int Id { get; set; }
        public string RazaoSocial { get; set; }
        public string Fantasia { get; set; }
        public string Cnpj {  get; set; }
        public string Email { get; set; }
        public bool ativo { get; set; }
        public int PessoaId { get; set; }
        public string Contato { get; set; }
    }
}