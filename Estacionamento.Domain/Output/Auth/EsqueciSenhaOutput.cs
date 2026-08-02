namespace Estac.Domain.Output.Auth
{
    public class EsqueciSenhaOutput
    {
        public string Mensagem { get; set; }
        public string LinkRedefinicaoNoFrontend { get; set; }
        public bool EmailEnviado { get; set; }
    }
}
