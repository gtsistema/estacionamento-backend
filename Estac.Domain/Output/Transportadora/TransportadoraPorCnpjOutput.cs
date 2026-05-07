namespace Estac.Domain.Output.Transportadora
{
    public class TransportadoraPorCnpjOutput
    {
        public int Id { get; set; }
        public string Cnpj { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string NomeResponsavel { get; set; }
        public string CpfResponsavel { get; set; }
        public string TelefoneResponsavel { get; set; }
    }
}
