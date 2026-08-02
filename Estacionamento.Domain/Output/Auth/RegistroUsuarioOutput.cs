namespace Estac.Domain.Output.Auth
{
    public class RegistroUsuarioOutput
    {
        public string Mensagem { get; set; }
        public string Email { get; set; }
        /// <summary>Null se desabilitado no appsettings (ex.: produção) ou se Front não estiver configurado.</summary>
        public string LinkConfirmacaoNoFrontend { get; set; }
        /// <summary>True se o e-mail de confirmação foi entregue via SMTP.</summary>
        public bool EmailDeConfirmacaoEnviado { get; set; }
    }
}
