namespace Estac.Domain.Input.Movimento.Entrada
{
    public class EntradaTransportadoraInput
    {
        public int? Id { get; set; }
        public string Cnpj { get; set; }
        public string RazaoSocial { get; set; }
        public string ResponsavelLegal { get; set; }
        public string ResponsavelCpf { get; set; }
        public string ResponsavelEmail { get; set; }
        public string ResponsavelTelefone { get; set; }
    }
}
